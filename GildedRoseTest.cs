using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        private const string BrieName = "Aged Brie";
        private const string LegendaryName = "Sulfuras, Hand of Ragnaros";
        private const string ConjuredName = "Conjured Mana Cake";
        private const string BackstagePassName = "Backstage passes to a TAFKAL80ETC concert";

        [Test]
        public void NonLegendaryItems_HaveSellInReducedByOne()
        {
            var oneSellIn = 20;
            var twoSellIn = -2;
            var threeSellIn = 0;
            var fourSellIn = -419;

            IList<Item> Items = new List<Item>
            {
                new Item { Name = "One Regular Item", SellIn = oneSellIn, Quality = 0 },
                new Item { Name = "Regular Item Two", SellIn = twoSellIn, Quality = 0 },
                new Item { Name = BrieName, SellIn = threeSellIn, Quality = 0 },
                new Item { Name = BackstagePassName, SellIn = fourSellIn, Quality = 0 },
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual( oneSellIn - 1, Items[0].SellIn);
            Assert.AreEqual(twoSellIn - 1, Items[1].SellIn);
            Assert.AreEqual(threeSellIn - 1, Items[2].SellIn);
            Assert.AreEqual(fourSellIn - 1, Items[3].SellIn);
        }

        [Test]
        public void NonSpecialItem_DegradesByOne_IfNewDayAndNotPastSellInDate()
        {
            var quality = 10;

            IList<Item> Items = new List<Item> { new Item { Name = "Regular Item", SellIn = 1, Quality = quality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(quality - 1, Items[0].Quality);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void NonSpecialItem_DegradesByTwo_IfNewDayAndPastSellInDate(int sellIn)
        {
            var quality = 10;

            IList<Item> Items = new List<Item> { new Item { Name = "Regular Item", SellIn = sellIn, Quality = quality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(quality - 2, Items[0].Quality);
        }

        [TestCase(2)]
        [TestCase(1)]
        [TestCase(0)]
        public void Item_NeverDegrades_ToBelowZero(int quality)
        {
            var sellIn = -2;

            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Regular Item", SellIn = sellIn, Quality = quality },
                new Item { Name = ConjuredName, SellIn = sellIn, Quality = quality }
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(0, Items[0].Quality);
        }

        [TestCase(50)]
        [TestCase(49)]
        public void SpecialNonLegendaryItems_NeverUpdatesQuality_ToAboveFifty(int quality)
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = BrieName, SellIn = 5, Quality = quality },
                new Item { Name = BackstagePassName, SellIn = 5, Quality = quality },
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(50, Items[0].Quality);
            Assert.AreEqual(50, Items[1].Quality);
        }

        [TestCase(13)]
        [TestCase(11)]
        public void BackStagePass_IncreasesQualityByOne_IfSellInDateAboveTen(int sellIn)
        {
            var quality = 10;

            IList<Item> Items = new List<Item>
            {
                new Item { Name = BackstagePassName, SellIn = sellIn, Quality = quality },
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(quality + 1, Items[0].Quality);
        }

        [TestCase(10)]
        [TestCase(6)]
        public void BackStagePass_IncreasesQualityByTwo_IfSellInDateTenOrLess(int sellIn)
        {
            var quality = 10;

            IList<Item> Items = new List<Item>
            {
                new Item { Name = BackstagePassName, SellIn = sellIn, Quality = quality },
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(quality + 2, Items[0].Quality);
        }

        [TestCase(5)]
        [TestCase(1)]
        public void BackStagePass_IncreasesQualityByThree_IfSellInDateFiveOrLess(int sellIn)
        {
            var quality = 10;

            IList<Item> Items = new List<Item>
            {
                new Item { Name = BackstagePassName, SellIn = sellIn, Quality = quality },
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(quality + 3, Items[0].Quality);
        }

        [TestCase(0)]
        [TestCase(-2)]
        public void BackStagePass_DegradesToZero_IfSellInDateLessThanZero(int sellIn)
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = BackstagePassName, SellIn = sellIn, Quality = 10 },
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(0, Items[0].Quality);
        }

        [TestCase(2)]
        [TestCase(-1)]
        public void LegendaryItem_NeverChangesQuality(int sellIn)
        {
            var quality = 80;

            IList<Item> Items = new List<Item>
            {
                new Item { Name = LegendaryName, SellIn = sellIn, Quality = quality },
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(quality, Items[0].Quality);
        }

        [TestCase(2)]
        [TestCase(-1)]
        public void LegendaryItem_NeverChangesSellIn(int sellIn)
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = LegendaryName, SellIn = sellIn, Quality = 80 },
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(sellIn, Items[0].SellIn);
        }

        [TestCase(5)]
        [TestCase(1)]
        public void ConjuredNonSpecialItem_DegradesByTwo_IfBeforeSellInDate(int sellIn)
        {
            var quality = 30;

            IList<Item> Items = new List<Item>
            {
                new Item { Name = ConjuredName, SellIn = sellIn, Quality = quality },
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(quality - 2, Items[0].Quality);
        }

        [TestCase(-1)]
        [TestCase(-5)]
        public void ConjuredNonSpecialItem_DegradesByFour_IfPastSellInDate(int sellIn)
        {
            var quality = 30; 

            IList<Item> Items = new List<Item>
            {
                new Item { Name = ConjuredName, SellIn = sellIn, Quality = quality },
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(quality - 4, Items[0].Quality);
        }

        [TestCase(5)]
        [TestCase(1)]
        public void Brie_IncreasesQualityByOne_IfBeforeSellInDate(int sellIn)
        {
            var quality = 40;

            IList<Item> Items = new List<Item>
            {
                new Item { Name = BrieName, SellIn = sellIn, Quality = quality},
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(quality + 1, Items[0].Quality);
        }

        [TestCase(-1)]
        [TestCase(-5)]
        public void Brie_IncreasesQualityByTwo_IfAfterSellInDate(int sellIn)
        {
            var quality = 40;

            IList<Item> Items = new List<Item>
            {
                new Item { Name = BrieName, SellIn = sellIn, Quality = quality },
            };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(quality + 2, Items[0].Quality);
        }
    }
}
