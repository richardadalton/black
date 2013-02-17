using NUnit.Framework;
using com.devjoy.black;

namespace black.core.tests
{
    public class SharedTableTests
    {
        [Test]
        public void CanGet_AReference_ToASharedTable_ByType()
        {
            var table = SharedTables.Table<AnyClass>();
            table.Add(new AnyClass {AnyProperty = "First Table"});

            var otherRef = SharedTables.Table<AnyClass>();
            Assert.AreEqual(table, otherRef);
        }

        [Test]
        public void CanHold_References_ToMultipleTables_OfSameType_BySpecifying_AName()
        {
            var first = SharedTables.Table<AnyClass>("First");
            first.Add(new AnyClass { AnyProperty = "First Table" });

            var second = SharedTables.Table<AnyClass>("Second");
            second.Add(new AnyClass { AnyProperty = "Second Table" });

            var firstOtherRef = SharedTables.Table<AnyClass>("First");
            var secondOtherRef = SharedTables.Table<AnyClass>("Second");
            
            Assert.AreEqual(firstOtherRef, first);
            Assert.AreEqual(secondOtherRef, second);
            Assert.AreNotEqual(firstOtherRef, secondOtherRef);
        }

        [Test]
        public void CanDisposeOf_ATable_ByType()
        {
            var table = SharedTables.Table<AnyClass>();
            table.Add(new AnyClass { AnyProperty = "First Table" });
            Assert.AreEqual(1, table.Count);

            SharedTables.DisposeOf<AnyClass>();

            var otherRef = SharedTables.Table<AnyClass>();
            Assert.AreEqual(0, otherRef.Count);
        }

        [Test]
        public void CanDisposeOf_ATable_ByName()
        {
            var table = SharedTables.Table<AnyClass>("AnyTableName");
            table.Add(new AnyClass { AnyProperty = "Any Table" });
            Assert.AreEqual(1, table.Count);

            SharedTables.DisposeOf("AnyTableName");

            var otherRef = SharedTables.Table<AnyClass>("AnyTableName");
            Assert.AreEqual(0, otherRef.Count);
        }
    }
}
