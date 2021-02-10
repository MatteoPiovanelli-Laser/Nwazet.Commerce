﻿using Orchard.ContentManagement;
using System.Collections.Generic;
using System.Linq;

namespace Nwazet.Commerce.Models {
    public class ShoppingCartQuantityProduct {
        public ShoppingCartQuantityProduct(int quantity, ProductPart product, IDictionary<int, ProductAttributeValueExtended> attributeIdsToValues = null) {
            Quantity = quantity;
            Product = product;
            Price = product.Price;
            AttributeIdsToValues = attributeIdsToValues;
        }

        public int Quantity { get; private set; }
        public ProductPart Product { get; private set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }

        // Captures attribute price adjustments that apply to the line item, not each item
        public decimal LinePriceAdjustment { get; set; }
        public IContent Promotion { get; set; }
        public string Comment { get; set; }
        public IDictionary<int, ProductAttributeValueExtended> AttributeIdsToValues { get; set; }

        public string AttributeDescription {
            get {
                if (AttributeIdsToValues == null || !AttributeIdsToValues.Any()) {
                    return "";
                }
                return "(" + string.Join(", ",
                    AttributeIdsToValues.ToList().Select(kvp => kvp.Key + " " + kvp.Value.ToString())) 
                    + ")";
            }
        }

        public override string ToString() {
            return Quantity + " " + Product.Sku 
                + (string.IsNullOrWhiteSpace(AttributeDescription) ? "" : " " + AttributeDescription) 
                + " at $" + Price;
        }
    }
}
