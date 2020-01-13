using System.Linq;
using System.Collections.Generic;

namespace Kata
{
	// TODO: Refactor calculations to another entity
	public class Basket
	{
		List<string> itemList = new List<string>();
		IList<Item> itemCatalog;
		IList<SpecialOffer> specialOffers;
		public Basket(IList<Item> itemCatalog, IList<SpecialOffer> specialOffers)
		{
			this.itemCatalog = itemCatalog;
			this.specialOffers = specialOffers;
		}

		public void Add(string sku)
		{
			this.itemList.Add(sku);
		}

		public decimal CalculateTotal()
		{
			decimal result = 0m;

			var groupedItems = this.itemList
				.GroupBy(x => x)
				.Select(z => new
				{
					z.Key,
					Count = z.Count()
				});

			foreach (var item in groupedItems)
			{
				result += CalculatePriceWithOffers(item.Key, item.Count);
			}

			return result;
		}

		private decimal CalculatePriceWithOffers(string sku, int quantity, decimal price = 0)
		{
			var offer = this.specialOffers.Where(x => x.SKU == sku).SingleOrDefault();

			if (offer != null && offer.Quantity <= quantity)
			{
				return CalculatePriceWithOffers(sku, quantity - offer.Quantity, price + offer.OfferPrice);
			}

			if (quantity > 0)
			{
				var item = this.itemCatalog.Where(x => x.SKU == sku).SingleOrDefault();
				if (item != null)
					return CalculatePriceWithOffers(sku, quantity - 1, price + item.UnitPrice);
			}

			return price;
		}
	}
}
