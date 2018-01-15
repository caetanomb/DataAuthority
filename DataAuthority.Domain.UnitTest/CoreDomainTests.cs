using DataAuthority.Domain.Exceptions;
using Xunit;

namespace DataAuthority.Domain.UnitTest
{
    public class CoreDomainTests
    {
        // I know I have to receive TWO Base64 data param
        // The provided data need to be diff-ed and the resultS returned
        // If paramA equals paramB then return 'Equal'
        // if paramA Not Equals paramB in size then return 'ParamsSizeNotEqual'
        // if paramA equals paramB in size then return offsets + offSetDataLength
        /* Json format 
         * { 
         *  result: string, 
         *  diffs : [
         *           {
         *            offset : int, 
         *            dataDiffLenght : int
         *           }
         *          ] 
         * } */

        [Fact]
        public void Create_Payload_Succes()
        {
            PayLoad payLoad = new PayLoad(1, "dGVzdGU=");
            Assert.NotNull(payLoad);
        }

        [Fact]
        public void Invalid_PayLoad()
        {
            Assert.Throws<DataAuthorityDomainException>(() => {
                PayLoad payLoad = new PayLoad(1, "sjdhfjsdf");
            });
        }

        [Fact]
        public void Should_Prepare_DomainEvent_For_Differ()
        {
            int expectedResult = 1;

            PayLoad payLoad = new PayLoad(1, "dGVzdGU=");

            Assert.Equal(expectedResult, payLoad.DomainEvents.Count);
        }

        [Fact]
        public void Create_DataValidator_Succes()
        {
            DataValidator dataValidator = new DataValidator("sdfsdf", "sdfsfd");

            Assert.NotNull(dataValidator);
        }

        [Theory]
        [InlineData("asdasd", "dGVzdGU=")] //Throws erro for paramA
        [InlineData("dGVzdGU=", "asdasd")] //Throws erro for paramB
        public void Invalid_Base64_data(string paramA, string paramB)
        {            
            Assert.Throws<DataAuthorityDomainException>(() =>
            {
                new DataValidator(paramA, paramB);
            });
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("dfsdfsdf", "dfsdfsdf")]
        [InlineData("VGVzdGluZ0VxdWFsaXR5", "VGVzdGluZ0VxdWFsaXR5")]
        public void If_Params_equal_return_that(string paramA, string paramB)
        {             
            DataValidator dataValidator = new DataValidator(paramA, paramB);
            DataDiffInsight dataDiffInsight = dataValidator.Diff();

            Assert.Equal("Equal", dataDiffInsight.Result);
        }

        [Theory]
        [InlineData("  ", "a")]
        [InlineData("VGVluZ0VxdWFsaXR5", "VGVzdGluaaaxdWFsaXR5")]
        [InlineData("ParamAValue", "ParameterBValue")]
        public void If_Params_Not_Equal_in_Size_Return_That(string paramA, string paramB)
        {            
            DataValidator dataValidator = new DataValidator(paramA, paramB);
            DataDiffInsight dataDiffInsight = dataValidator.Diff();

            Assert.Equal("NotEqualSize", dataDiffInsight.Result);
        }        

        [Theory]
        [InlineData("paramA", "PARAMB", 0, 6)] //paramA - PARAMB
        [InlineData("paramA", "parama", 5, 1)] //paramA - parama
        [InlineData("paRama", "parama", 2, 1)] //paRama - parama
        [InlineData("paRAMa", "parama", 2, 3)] //paRAMa - parama
        public void If_Params_Of_Same_Size_Return_Insight_OffSet_OffSetDataLength(string paramA, string paramB, int offSet, int offSetDataLength)
        {            
            DataValidator dataValidator = new DataValidator(paramA, paramB);
            DataDiffInsight dataDiffInsight = dataValidator.Diff();

            Assert.Equal(offSet, dataDiffInsight.Diffs[0].OffSet);
            Assert.Equal(offSetDataLength, dataDiffInsight.Diffs[0].OffSetDataLength);
        }

        [Fact]
        public void If_Params_Of_Same_Size_Return_Check_Total_Differences()
        {
            string paramA = "PaRamA";
            string paramB = "paramb";

            DataValidator dataValidator = new DataValidator(paramA, paramB);
            DataDiffInsight dataDiffInsight = dataValidator.Diff();

            Assert.Equal(3, dataDiffInsight.Diffs.Count);
        }        

        [Fact]
        public void Check_DifferenceList_After_NewDiference_Added()
        {
            DataDiffInsight dataDiffInsight = new DataDiffInsight();
            Difference diff = dataDiffInsight.CreateDifference();
            dataDiffInsight.AddDifference(diff);

            Assert.Equal(1, dataDiffInsight.Diffs.Count);
        }

        [Fact]
        public void ValidateOffSetDataLengthIncrement()
        {
            Difference diff = new Difference();
            int aux = 1;
            diff.IncrementOffSetLength();

            Assert.Equal(aux + 1, diff.OffSetDataLength);
        }
    }    
}
