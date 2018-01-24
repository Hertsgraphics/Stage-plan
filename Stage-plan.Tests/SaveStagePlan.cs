using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Stage_Plan.Tests
{
    [TestClass]
    public class SaveStagePlan
    {
        private string _bandName = "MyTest^Band_which-Is-un1u2j3uque";
        //[TestMethod]
        //public void SaveBandByName()
        //{
        //    Bll.Save save = new Bll.Save();
        //    var id = save.SaveBand(this._bandName);

        //    Assert.IsTrue(id > 0);               

        //    var dc = new Dal.StageplanEntities();
        //    var b = (from d in dc.Stageplans
        //             where d.Id == id
        //             select d).SingleOrDefault();


        //    Assert.IsTrue(b != null);

        //    DeleteBandById(id);
        //}

        private void DeleteBandById(int id)
        {
            var dc = new Dal.StageplanEntities();
            var r = (from d in dc.Stageplans
                     where d.Id == id
                     select d).SingleOrDefault();

            if (r == null)
                return;

            dc.Stageplans.Remove(r);
            dc.SaveChanges();
        }
    }
}
