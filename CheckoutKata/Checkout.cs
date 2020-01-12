using System;
using System.Linq;
using System.Collections.Generic;

namespace Kata
{
	public class Checkout
	{
		decimal total = 0m;
		IList<Item> itemCatalog;
		IList<SpecialOffer> specialOffers;
		IList<string> basket = new List<string>();

		public Checkout(IList<Item> itemCatalog, IList<SpecialOffer> specialOffers)
		{
			this.itemCatalog = itemCatalog;
			this.specialOffers = specialOffers;
		}
		public decimal Total()
		{
			return total;
		}

		public void Scan(string sku)
		{
			this.basket.Add(sku);
			this.total = 0;

			var groupedItems = this.basket.GroupBy(x => x).Select(z => new
			{
				z.Key,
				Count = z.Count()
			});

			foreach (var item in groupedItems)
			{
				total += CalculatePriceWithOffers(item.Key, item.Count);
			}
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

		public void EndPurchase()
		{
			this.basket = default;
		}

	}
	public class Item
	{
		public string SKU { get; set; }
		public decimal UnitPrice { get; set; }
		public Item(string sku, decimal unitPrice)
		{
			this.SKU = sku;
			this.UnitPrice = unitPrice;
		}
		public Item(string sku)
		{
			this.SKU = sku;
			this.UnitPrice = 0;
		}
	}

	public class SpecialOffer
	{
		public string SKU { get; set; }
		public int Quantity { get; set; }
		public decimal OfferPrice { get; set; }
		public SpecialOffer(string sku, int quantity, decimal offerPrice)
		{
			this.SKU = sku;
			this.Quantity = quantity;
			this.OfferPrice = offerPrice;
		}
	}

}
