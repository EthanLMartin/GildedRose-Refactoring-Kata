using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                int qualityIncrease = 0;

                if (IsLegendary(Items[i]))
                {
                    continue;
                }

                if (IsBackStagePass(Items[i]))
                {
                    int sellIn = Items[i].SellIn;

                    if (sellIn > 10)
                    {
                        qualityIncrease = 1;
                    }
                    else if (sellIn > 5)
                    {
                        qualityIncrease = 2;
                    }
                    else if (sellIn > 0)
                    {
                        qualityIncrease = 3;
                    }
                    else
                    {
                        Items[i].Quality = 0;
                        Items[i].SellIn--;
                        continue;
                    }
                }

                if (IsBrie(Items[i]))
                {
                    qualityIncrease = 1;

                    if (!IsInDate(Items[i]))
                    {
                        qualityIncrease = 2;
                    }
                }

                if (IsNonSpecialItem(Items[i]))
                {
                    qualityIncrease = -1;

                    if (!IsInDate(Items[i]))
                    {
                        qualityIncrease = -2;
                    }

                }

                if (IsConjured(Items[i]))
                {
                    if (qualityIncrease < 0)
                    {
                        qualityIncrease = qualityIncrease * 2;
                    }
                }

                Items[i].SellIn--;
                Items[i] = ChangeQuality(Items[i], qualityIncrease);
            }
        }

        private bool IsNonSpecialItem(Item item)
        {
            return !IsLegendary(item) &&
                   !IsBackStagePass(item) &&
                   !IsBrie(item);
        }

        private bool IsLegendary(Item item)
        {
            return item.Name == "Sulfuras, Hand of Ragnaros";
        }

        private bool IsBackStagePass(Item item)
        {
            return item.Name == "Backstage passes to a TAFKAL80ETC concert";
        }

        private bool IsBrie(Item item)
        {
            return item.Name == "Aged Brie";
        }

        private bool IsConjured(Item item)
        {
            return item.Name.StartsWith("Conjured");
        }

        private bool IsInDate(Item item)
        {
            return item.SellIn > 0;
        }

        private Item ChangeQuality(Item item, int change)
        {
            item.Quality += change;

            if (item.Quality < 0)
            {
                item.Quality = 0;
            }

            if (item.Quality > 50)
            {
                item.Quality = 50;
            }

            return item;
        }
    }
}
