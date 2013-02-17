using System.Collections.Generic;
using NUnit.Framework;
using com.devjoy.black;

namespace black.core.tests
{
    public class TableTableTests
    {
        [Test]
        public void CanParse_FromTable_BackToSlimFormat()
        {
            var original = new List<List<string>>
                             {
                                 new List<string> {"A", "B", "C"},
                                 new List<string> {"1", "2", "3"},
                             };

            var table = new TableTable(original);

            Assert.That(table.AsSlimTable(), Is.EqualTo(original));
        }


        [Test]
        public void CanParseSourceTableIntoRows()
        {
            var source = new List<List<string>>
                             {
                                 new List<string> {"A", "B", "C"},
                                 new List<string> {"1", "2", "3"},
                             };

            var table = new TableTable(source);
            
            Assert.That(table.Rows.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanParseSourceRowsIntoCells()
        {
            var source = new List<List<string>>
                             {
                                 new List<string> {"A", "B", "C"},
                                 new List<string> {"1", "2", "3"},
                             };

            var row = new TableTable(source).Rows[0];

            var actual = string.Empty;
            foreach (var cell in row.Cells)
                actual += cell.ToString();

            Assert.That(actual, Is.EqualTo("ABC"));
        }

        [Test]
        public void CanAccessAnIndividualCellDirectly()
        {
            var source = new List<List<string>>
                             {
                                 new List<string> {"A", "B", "C"},
                                 new List<string> {"1", "2", "3"},
                             };

            var table = new TableTable(source);

            Assert.That("2", Is.EqualTo(table.Cells(1,1).ToString()));
        }

        [Test]
        public void CanAccessAnIndividualCellValueDirectly()
        {
            var source = new List<List<string>>
                             {
                                 new List<string> {"A", "B", "C"},
                                 new List<string> {"1", "2", "3"},
                             };

            var table = new TableTable(source);

            Assert.That("2", Is.EqualTo(table.CellValues(1, 1)));
        }


        [Test]
        public void When_ACellIsCheckedAgainst_AMatchingValue_CellIsFlaggedAs_Pass()
        {
            var cell = new TableCell("this");
            
            cell.CompareWith("this");
            
            Assert.That(cell.ToString(), Is.EqualTo("pass"));
        }

        [Test]
        public void When_ACellIsCheckedAgainst_ANonMatchingValue_CellIsFlaggedAs_Fail()
        {
            var cell = new TableCell("that");

            cell.CompareWith("this");

            Assert.That(cell.ToString(), Is.EqualTo("fail: Expected:that Actual:this"));
        }

        [Test]
        public void ACell_CanBe_FlaggedAsPass()
        {
            var cell = new TableCell("this");

            cell.Pass();

            Assert.That(cell.ToString(), Is.EqualTo("pass"));
        }

        [Test]
        public void ACell_CanBe_FlaggedAsPass_WithAMessage()
        {
            var cell = new TableCell("this");

            cell.Pass("Any Message");

            Assert.That(cell.ToString(), Is.EqualTo("pass: Any Message"));
        }

        [Test]
        public void ACell_CanBe_FlaggedAsFail()
        {
            var cell = new TableCell("this");

            cell.Fail();

            Assert.That(cell.ToString(), Is.EqualTo("fail"));
        }

        [Test]
        public void ACell_CanBe_FlaggedAsFail_WithAMessage()
        {
            var cell = new TableCell("this");

            cell.Fail("Any Message");

            Assert.That(cell.ToString(), Is.EqualTo("fail: Any Message"));
        }

        [Test]
        public void ACell_CanBe_FlaggedAsIgnore()
        {
            var cell = new TableCell("this");

            cell.Ignore();

            Assert.That(cell.ToString(), Is.EqualTo("ignore"));
        }

        [Test]
        public void ACell_CanBe_FlaggedAsIgnore_WithAMessage()
        {
            var cell = new TableCell("this");

            cell.Ignore("Any Message");

            Assert.That(cell.ToString(), Is.EqualTo("ignore: Any Message"));
        }

        [Test]
        public void ACell_CanBe_FlaggedAsReport_WithAMessage()
        {
            var cell = new TableCell("this");

            cell.Report("Any Message");

            Assert.That(cell.ToString(), Is.EqualTo("report: Any Message"));
        }

        [Test]
        public void ACell_CanBe_ExplicitelyFlaggedAsUnchanged_BySettingItsContentsToEmpty()
        {
            var cell = new TableCell("this");

            cell.NoChange();

            Assert.That(cell.ToString(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void ACell_CanBe_ImplicitelyFlaggedAsUnchanged_ByDoingNothignToIt()
        {
            var cell = new TableCell("this");

            Assert.That(cell.ToString(), Is.EqualTo("this"));
        }

        [Test]
        public void ACell_CanBe_FlaggedAsError_WithAMessage()
        {
            var cell = new TableCell("this");

            cell.Error("Any Message");

            Assert.That(cell.ToString(), Is.EqualTo("error: Any Message"));
        }

        [Test]
        public void ACell_CanBe_OverriddenWith_ANewValue()
        {
            var cell = new TableCell("this");

            cell.SetText("that");

            Assert.That(cell.ToString(), Is.EqualTo("that"));
        }


        [Test]
        public void AnEntireRow_CanBe_Ignored()
        {
            var row = new TableRow(new List<string> {"A", "B", "C"});

            row.Ignore();

            foreach(var cell in row.Cells)
                Assert.That(cell.ToString(), Is.EqualTo("ignore"));
        }

        [Test]
        public void AnEntireRow_CanBe_FlaggedAsUnchanged()
        {
            var row = new TableRow(new List<string> { "A", "B", "C" });

            row.NoChange();

            foreach (var cell in row.Cells)
                Assert.That(cell.ToString(), Is.EqualTo(string.Empty));
        }

    }
}
