using System;
using System.Collections.Generic;
using System.Text;

namespace DataAuthority.Domain
{
    /// <summary>
    /// Entity represents the difference offset and length
    /// </summary>
    public class Difference
    {
        public int OffSet { get; internal set; }
        public int OffSetDataLength { get; internal set; }

        public Difference()
        {
            OffSetDataLength = 1;
        }

        public void SetOffSet(int offSet)
        {
            OffSet = offSet;
        }

        public void SetOffSetLength(int offSetDataLength)
        {
            OffSetDataLength = offSetDataLength;
        }

        public void IncrementOffSetLength()
        {
            OffSetDataLength++;
        }
    }
}
