using System;
using System.Linq;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000005 RID: 5
	public static class EnumExtensions
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000274C File Offset: 0x0000094C
		public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			string name = Enum.GetName(type, value);
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(string.Format("Cannot find the name of the constant in enum {0} with the following value: {1}", type.ToString(), value));
			}
			return type.GetField(name).GetCustomAttributes(false).OfType<TAttribute>()
				.SingleOrDefault<TAttribute>();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027AC File Offset: 0x000009AC
		public static ProductDetails GetDetails(this ProductType product)
		{
			return product.GetAttribute<ProductDetails>();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027B9 File Offset: 0x000009B9
		public static SkuDetails GetDetails(this SkuType sku)
		{
			return sku.GetAttribute<SkuDetails>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027C6 File Offset: 0x000009C6
		public static SkuStrings GetStrings(this SkuType sku)
		{
			return sku.GetAttribute<SkuStrings>();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000027D3 File Offset: 0x000009D3
		public static SkuStrings GetStrings(this ProductType product)
		{
			return product.GetAttribute<SkuStrings>();
		}
	}
}
