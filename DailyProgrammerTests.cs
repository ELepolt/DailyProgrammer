using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyProgrammerTestProject
{
    [TestClass]
    public class DailyProgrammerTests
    {
        [TestMethod]
        public void FriendlyDatesTest()
        {
            var friendlyDate = DailyProgrammer.Program.FriendlyDates(new DateTime(2015, 07, 01), new DateTime(2015, 07, 04));
            Assert.AreEqual("July 1st - 4th", friendlyDate);
            friendlyDate = DailyProgrammer.Program.FriendlyDates(new DateTime(2015, 12, 01), new DateTime(2016, 02, 03));
            Assert.AreEqual("December 1st - February 3rd", friendlyDate);
            friendlyDate = DailyProgrammer.Program.FriendlyDates(new DateTime(2015, 12, 01), new DateTime(2017, 02, 03));
            Assert.AreEqual("December 1st, 2015 - February 3rd, 2017", friendlyDate);
            friendlyDate = DailyProgrammer.Program.FriendlyDates(new DateTime(2016, 03, 01), new DateTime(2016, 05, 05));
            Assert.AreEqual("March 1st - May 5th, 2016", friendlyDate);
            friendlyDate = DailyProgrammer.Program.FriendlyDates(new DateTime(2017, 01, 01), new DateTime(2017, 01, 01));
            Assert.AreEqual("January 1st, 2017", friendlyDate);
            friendlyDate = DailyProgrammer.Program.FriendlyDates(new DateTime(2022, 09, 05), new DateTime(2023, 09, 04));
            Assert.AreEqual("September 5th, 2022 - September 4th, 2023", friendlyDate);
        }
    }
}
