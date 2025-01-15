using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000166 RID: 358
	internal static class EdmTypeExtensions
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x0003B3C8 File Offset: 0x000395C8
		public static Type GetClrType(this EdmType item)
		{
			EntityType entityType = item as EntityType;
			if (entityType != null)
			{
				return entityType.GetClrType();
			}
			EnumType enumType = item as EnumType;
			if (enumType != null)
			{
				return enumType.GetClrType();
			}
			ComplexType complexType = item as ComplexType;
			if (complexType != null)
			{
				return complexType.GetClrType();
			}
			return null;
		}
	}
}
