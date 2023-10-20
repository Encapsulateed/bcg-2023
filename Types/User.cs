using bcg_bot.Models;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcg_bot.Types
{
    internal class User
    {
        public Models.User? user { get; set; }

        

        public Task Add()
        {
            Console.WriteLine("GAY");

            return Task.Run(async () =>
            {
                Console.WriteLine("GAY");

                using (BcgContext db = new BcgContext())
                {
                    try
                    {
                        Console.WriteLine(this.user.ChatId);
                        await db.Users.AddAsync(this.user);
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine($"Exeption in User.cs\nFunction: User.Add()\n\n {ex}\n\n");
                    }
                }
            });
        }

        public Task Update()
        {
            return Task.Run(async () =>
            {
                using (BcgContext db = new BcgContext())
                {
                    try
                    {
                        var user = db.Users.Where(usr => usr.ChatId == this.user.ChatId).FirstOrDefault();
                        db.Update(user).CurrentValues.SetValues(this.user.ChatId);

                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine($"Exeption in User.cs\nFunction: User.Update()\n\n {ex}\n\n");
                    }
                }
            });
        }

        public Task Get()
        {
            return Task.Run(() => 
            {
                using (BcgContext db = new BcgContext())
                {
                    try
                    {
                        var usr = db.Users.Where(usr => user.ChatId == this.user.ChatId).FirstOrDefault();
                        if(usr is not null)
                        {
                            user = usr;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"No such user exeption in User.cs\nFunction: User.Get()\n\n {ex}\n\n");
                    }

                }
            });
           
        }

        
    }
}
