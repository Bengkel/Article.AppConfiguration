using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;

namespace LINKIT.Core.UnitTest
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void C1_CoreConfigurationTest()
        {
            // Setting from Azure App Configuration
            var version = CoreConfiguration.Current.GetSectionValue("my-secret");

            Assert.IsNotNull(version);

            switch (CoreConfiguration.Current.GetSectionValue("AppConfiguration_Stage"))
            {
                case "local":
                    Assert.IsTrue(version == "S#CR#T");
                    break;
                default:
                    Assert.IsTrue(version == "S#CR#T");
                    break;
            }
        }
    }
}