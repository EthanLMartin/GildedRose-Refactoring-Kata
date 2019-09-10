using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

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
                Items[i] = UpdateItemQuality(Items[i]);
            }
        }

        private Item UpdateItemQuality(Item item)
        {
            int qualityChange = 0;

            if (IsLegendary(item))
            {
                return item;
            }

            if (IsBackStagePass(item))
            {
                if (IsInDate(item))
                {
                    qualityChange = GetBackstagePassQualityChange(item);
                }
                else
                {
                    item.Quality = 0;
                    item.SellIn--;
                    return item;
                }
            }
            else if (IsBrie(item))
            {
                qualityChange = 1;
            }
            else
            {
                qualityChange = -1;
            }

            if (IsConjured(item))
            {
                qualityChange *= 2;
            }

            if (!IsInDate(item))
            {
                qualityChange *= 2;
            }

            item.SellIn--;
            item = ChangeBoundedQuality(item, qualityChange);

            return item;
        }

        private int GetBackstagePassQualityChange(Item item)
        {
            int sellIn = item.SellIn;

            if (sellIn > 10)
            {
                return 1;
            }
            else if (sellIn > 5)
            {
                return 2;
            }
            else
            {
                return 3;
            }
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

        private Item ChangeBoundedQuality(Item item, int change)
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
