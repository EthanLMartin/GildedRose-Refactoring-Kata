using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void Item_NeverChangesName_OnUpdateQuality()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual("foo", Items[0].Name);
        }

        [Test]
        public void NonLegendaryItems_HaveSellInReducedByOne()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "One Regular Item", SellIn = 20, Quality = 0 },
                new Item { Name = "Regular Item Two", SellIn = -2, Quality = 0 },
                new Item { Name = "Aged Brie", SellIn = 0, Quality = 0 },
                new Item { Name = "Conjured Mana Cake", SellIn = -419, Quality = 0 },
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual( 19, Items[0].SellIn);
            Assert.AreEqual(-3, Items[1].SellIn);
            Assert.AreEqual(-1, Items[2].SellIn);
            Assert.AreEqual(-420, Items[3].SellIn);
        }

        [Test]
        public void NonSpecialItem_DegradesByOne_IfNewDayAndNotPastSellInDate()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Regular Item", SellIn = 1, Quality = 10 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(9, Items[0].Quality);
        }

        [Test]
        public void NonSpecialItem_DegradesByTwo_IfNewDayAndPastSellInDate()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Regular Item", SellIn = 0, Quality = 10 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(8, Items[0].Quality);
        }

        [Test]
        public void Item_NeverDegrades_ToBelowZero()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Regular Item", SellIn = -2, Quality = 1 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
        }

        [Test]
        public void SpecialNonLegendaryItems_NeverUpdatesQuality_ToAboveFifty()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Aged Brie", SellIn = 5, Quality = 50 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 50 },
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
        }

        [Test]
        public void BackStagePass_IncreasesQualityByOne_IfSellInDateAboveTen()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 10 },
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(11, Items[0].Quality);
        }

        [Test]
        public void BackStagePass_IncreasesQualityByTwo_IfSellInDateTenOrLess()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 10 },
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(12, Items[0].Quality);
        }

        [Test]
        public void BackStagePass_IncreasesQualityByThree_IfSellInDateFiveOrLess()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 10 },
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(13, Items[0].Quality);
        }

        [Test]
        public void BackStagePass_DegradesToZero_IfSellInDateLessThanZero()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 10 },
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
        }

        [Test]
        public void LegendaryItem_NeverChangesQuality()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 2, Quality = 80 },
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(80, Items[0].Quality);
            Assert.AreEqual(80, Items[1].Quality);
        }

        [Test]
        public void LegendaryItem_NeverChangesSellIn()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 2, Quality = 80 },
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(-1, Items[0].SellIn);
            Assert.AreEqual(2, Items[1].SellIn);
        }

        [Test]
        public void ConjuredNonSpecialItem_DegradesByTwo_IfBeforeSellInDate()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 30 },
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(28, Items[0].Quality);
        }

        [Test]
        public void Brie_IncreasesQualityByOne_IfBeforeSellInDate()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Aged Brie", SellIn = 5, Quality = 40 },
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(41, Items[0].Quality);
        }

        [Test]
        public void ConjuredNonSpecialItem_DegradesByFour_IfPastSellInDate()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Conjured Mana Cake", SellIn = -1, Quality = 30 },
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(26, Items[0].Quality);
        }
    }
}
