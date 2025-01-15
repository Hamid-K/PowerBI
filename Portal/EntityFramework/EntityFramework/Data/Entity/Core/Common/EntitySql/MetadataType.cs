using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000661 RID: 1633
	internal sealed class MetadataType : MetadataMember
	{
		// Token: 0x06004E0A RID: 19978 RVA: 0x001187E2 File Offset: 0x001169E2
		internal MetadataType(string name, TypeUsage typeUsage)
			: base(MetadataMemberClass.Type, name)
		{
			this.TypeUsage = typeUsage;
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06004E0B RID: 19979 RVA: 0x001187F3 File Offset: 0x001169F3
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataType.TypeClassName;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06004E0C RID: 19980 RVA: 0x001187FA File Offset: 0x001169FA
		internal static string TypeClassName
		{
			get
			{
				return Strings.LocalizedType;
			}
		}

		// Token: 0x04001C52 RID: 7250
		internal readonly TypeUsage TypeUsage;
	}
}
