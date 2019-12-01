using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP229_301044056_Assignment02.Models
{
    public class EFMeasuresRepository: IMeasureRepository
    {
        private ApplicationDbContext context;

        public EFMeasuresRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Measure> Measures => context.Measures;
        public void SaveMeasure(Measure measure)
        {
            System.Diagnostics.Debug.WriteLine("Save Measure");

            if (measure.MeasureID == 0)
            {
                context.Measures.Add(measure);
            }
            context.SaveChanges();
        }
    }
}
