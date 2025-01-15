using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000163 RID: 355
	internal static class EdmMemberExtensions
	{
		// Token: 0x06001637 RID: 5687 RVA: 0x0003A6F7 File Offset: 0x000388F7
		public static PropertyInfo GetClrPropertyInfo(this EdmMember property)
		{
			return property.Annotations.GetClrPropertyInfo();
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0003A704 File Offset: 0x00038904
		public static void SetClrPropertyInfo(this EdmMember property, PropertyInfo propertyInfo)
		{
			property.GetMetadataProperties().SetClrPropertyInfo(propertyInfo);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0003A714 File Offset: 0x00038914
		public static IEnumerable<T> GetClrAttributes<T>(this EdmMember property) where T : Attribute
		{
			IList<Attribute> clrAttributes = property.Annotations.GetClrAttributes();
			if (clrAttributes == null)
			{
				return Enumerable.Empty<T>();
			}
			return clrAttributes.OfType<T>();
		}
	}
}
