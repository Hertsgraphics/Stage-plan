using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stage_Plan.Dal;

namespace RemoveOldPosts
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Deleting old stage plans from stage-plan.com");
            var dc = new Stage_Plan.Dal.StageplanEntities();

            Console.WriteLine("Deprecate older than 28 days");

            var dateNow = DateTime.Now.AddDays(-28);
            var allEntriesCount = dc.Stageplans.Count();
            var oldEntires = (from s in dc.Stageplans
                              where dateNow > s.CreationDate
                              where s.IsDeprecated == false
                              select s
                              ).ToList();
            
            foreach (var item in oldEntires)
            {
                item.IsDeprecated = true;
                try
                {
                    dc.SaveChanges();
                }
                catch (Exception ex)
                {
                    var error = ex.ToString();
                    throw;
                }
            }


            Console.WriteLine("Deleting older than 3 months (84 days)");
            dateNow = DateTime.Now.AddDays(-84);

            oldEntires = (from s in dc.Stageplans
                              where dateNow > s.CreationDate
                              select s
                              ).ToList();

            foreach (var item in oldEntires)
            {
                dc.StageplanInstruments.RemoveRange(item.StageplanInstruments);
                dc.Stageplans.Remove(item);
                try
                {
                    dc.SaveChanges();
                }
                catch (Exception ex)
                {
                    var error = ex.ToString();
                    throw;
                }
            }
            Console.WriteLine(allEntriesCount - dc.Stageplans.Count()+ " were deleted.");
            Console.WriteLine("Press a key to close");
            Console.ReadKey();
        }
    }
}
