using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000169 RID: 361
	internal static class EnumTypeExtensions
	{
		// Token: 0x06001686 RID: 5766 RVA: 0x0003B828 File Offset: 0x00039A28
		public static Type GetClrType(this EnumType enumType)
		{
			return enumType.Annotations.GetClrType();
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0003B835 File Offset: 0x00039A35
		public static void SetClrType(this EnumType enumType, Type type)
		{
			enumType.GetMetadataProperties().SetClrType(type);
		}
	}
}
