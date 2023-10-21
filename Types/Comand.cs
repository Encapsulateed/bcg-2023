using bcg_bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcg_bot.Types
{
    internal class Comand
    {
        public Models.Comand? comand { get; set; }


        public Task Add()
        {
            return Task.Run(async () =>
            {
                using (BcgContext db = new BcgContext())
                {
                    try
                    {
                        await db.Comands.AddAsync(this.comand);
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine($"Exeption in Comand.cs\nFunction: Comand.Add()\n\n {ex}\n\n");
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
                        var comand = db.Comands.Where(comand => comand.Id == this.comand.Id).FirstOrDefault();
                        db.Update(comand).CurrentValues.SetValues(this.comand.Id);

                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine($"Exeption in User.cs\nFunction: User.Update()\n\n {ex}\n\n");
                    }
                }
            });
        }
        void Get(int Id)
        {
            using (BcgContext db = new BcgContext())
            {
                try
                {
                    var comand = db.Comands.Where(comand => comand.Id == Id).FirstOrDefault();
                    if (comand is not null)
                    {
                        this.comand = comand;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No such comand exeption in Comand.cs\nFunction: Comand.Get()\n\n {ex}\n\n");
                }

            }
        }
        public static List<Comand> GetComandListPaginated(int prevId, int Track)
        {


            using (BcgContext db = new BcgContext())
            {
                try
                {
                    var comands = db.Comands.Where(comand => comand.Track == Track).ToList().Where(comand => (comand.Id > prevId)).Take(10);
                    var lst = new List<Comand>();
                    foreach (var com in comands)
                    {
                        lst.Add(new Comand() { comand = com });
                    }
                    return lst;

                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Exeption in User.cs\nFunction: Comand.GetComandListPaginated()\n\n {ex}\n\n");
                }
            }

            return null;

        }
    }

}
