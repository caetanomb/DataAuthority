using DataAuthority.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAuthority.Domain
{
    /// <summary>
    /// Entity knows how to diff two contents
    /// </summary>
    public class DataValidator
    {        
        public string Data1 { get; private set; }
        public string Data2 { get; private set; }

        public DataValidator(string data1, string data2)
        {            
            Data1 = data1;
            Data2 = data2;
        }

        public DataDiffInsight Diff()
        {
            DataDiffInsight _dataDiffInsight = new DataDiffInsight();

            if (Data1 == Data2)
                _dataDiffInsight.Result = "Equal";
            else if (Data1?.Length != Data2?.Length)
                _dataDiffInsight.Result = "NotEqualSize";
            else if (Data1?.Length == Data2?.Length)
            {
                _dataDiffInsight.Result = "EqualSize";

                int length = Data1.Length;
                Difference diff = null;

                for (int i = 0; i < length; i++)
                {
                    if (Data1[i] != Data2[i])
                    {
                        if (diff == null)
                        {
                            diff = _dataDiffInsight.CreateDifference();
                            diff.SetOffSet(i);
                        }
                        else
                        {
                            diff.IncrementOffSetLength();
                        }
                    }
                    else if (diff != null)
                    {
                        _dataDiffInsight.AddDifference(diff);
                        diff = null;
                    }
                }

                if (diff != null)
                    _dataDiffInsight.AddDifference(diff);

            }
            return _dataDiffInsight;
        }
    }
}
