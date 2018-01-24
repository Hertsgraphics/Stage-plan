using System.Linq;

namespace Stage_Plan.Bll
{
    public class NewPlan : DataAccess
    {
        public NewPlan()
        {
        }

        public void New()
        {
            var generic = base.DataContext.Generics.Single();
            generic.TotalStagePlans++;
            base.SaveToDatabase();
        }
    }
}