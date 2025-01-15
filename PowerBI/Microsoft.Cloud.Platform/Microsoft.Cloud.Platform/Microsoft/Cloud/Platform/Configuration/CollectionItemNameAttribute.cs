using System;
using System.Reflection;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000421 RID: 1057
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class CollectionItemNameAttribute : Attribute
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x0007AC8D File Offset: 0x00078E8D
		// (set) Token: 0x060020A1 RID: 8353 RVA: 0x0007AC95 File Offset: 0x00078E95
		public string Name { get; private set; }

		// Token: 0x060020A2 RID: 8354 RVA: 0x0007AC9E File Offset: 0x00078E9E
		public CollectionItemNameAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x0007ACB0 File Offset: 0x00078EB0
		public static string GetItemName(PropertyInfo propertyInfo)
		{
			CollectionItemNameAttribute collectionItemNameAttribute = (CollectionItemNameAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(CollectionItemNameAttribute));
			if (collectionItemNameAttribute == null)
			{
				return null;
			}
			return collectionItemNameAttribute.Name;
		}
	}
}
