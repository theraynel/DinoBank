using DinoBank.Domain.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoBank.Persistence.Database
{
    public class DatabaseService: IDatabaseService
    {
        private static string route = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), "DinoBank.Persistence", "Files");

        public List<UserEntity> GetAll()
        {
            var file = Path.Combine(route, "User.JSON");
            using(var reader = new StreamReader(file)) 
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<UserEntity>>(json) ?? new List<UserEntity>();
            }
        }

        public bool Create(UserEntity user) 
        {
            var file = Path.Combine(route, "User.JSON");
            using (var reader = new StreamReader(file))
            {
                var json = reader.ReadToEnd();
                reader.Close();
                var listUser =  JsonConvert.DeserializeObject<List<UserEntity>>(json) ?? new List<UserEntity>();

                user.Id = listUser.Count + 1;

                listUser.Add(user);

                json = JsonConvert.SerializeObject(listUser, Formatting.Indented);

                File.WriteAllText(file, json);
                return true;
            }
        }

        public bool update(UserEntity user)
        {
            var file = Path.Combine(route, "User.JSON");
            using (var reader = new StreamReader(file))
            {
                var json = reader.ReadToEnd();
                reader.Close();

                var listUser = JsonConvert.DeserializeObject<List<UserEntity>>(json);

                if (listUser == null || listUser.Count == 0)
                    return false;

                var index = listUser.FindIndex(x => x.Id == user.Id);

                if (index != -1 )
                {
                    listUser[index] = user;

                    json = JsonConvert.SerializeObject(listUser, Formatting.Indented);
                    File.WriteAllText(json, file);
                    return true;
                }

                return false;

            }
        }

        public bool delete(UserEntity user)
        {
            var file = Path.Combine(route, "User.JSON");

            using (var r = new StreamReader(file))
            {
                var json = r.ReadToEnd();
                r.Close();

                var listUser = JsonConvert.DeserializeObject<List<UserEntity>>(json);

                if (listUser == null || listUser.Count == 0)
                    return false;

                bool remove = listUser.RemoveAll(x => x.Id == user.Id) > 0;

                if (remove) 
                {
                    json = JsonConvert.SerializeObject(listUser, Formatting.Indented);
                    File.WriteAllText(json, file);
                }

                return remove;
            }
        }
    }
}
