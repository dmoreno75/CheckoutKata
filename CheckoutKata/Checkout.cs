using System.Collections.Generic;

namespace Kata
{
	// TODO: Convert itemCatalog and specialOffer in repositories
	public class Checkout
	{
		decimal total = 0m;
	
		Basket basket = null;

		public Checkout(IList<Item> itemCatalog, IList<SpecialOffer> specialOffers)
		{
			this.basket = new Basket(itemCatalog, specialOffers);
		}
		public decimal Total()
		{
			return total;
		}

		public void Scan(string sku)
		{
			this.basket.Add(sku);
			this.total = this.basket.CalculateTotal();
		}

		public decimal EndPurchase()
		{
			this.basket = default;
			return Total();
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
