using System;
using System.Collections.Generic;

namespace DataAuthority.Domain
{
    /// <summary>
    /// Enitity represents diff result
    /// </summary>
    public class DataDiffInsight
    {
        public string Result { get; internal set; }
        public List<Difference> Diffs { get; internal set; }
        
        public Difference CreateDifference()
        {
            return new Difference();
        }

        public void AddDifference(Difference diff)
        {
            if (Diffs == null)
                Diffs = new List<Difference>();

            Diffs.Add(new Difference() { OffSet = diff.OffSet, OffSetDataLength = diff.OffSetDataLength });
        }
    }
}
