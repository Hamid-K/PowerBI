using System;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl
{
	// Token: 0x02000819 RID: 2073
	internal sealed class ContextUrlSelectListQualifiedWildcardItem : ContextUrlSelectListItem
	{
		// Token: 0x06003BE8 RID: 15336 RVA: 0x000C28C5 File Offset: 0x000C0AC5
		public ContextUrlSelectListQualifiedWildcardItem(string namespaceName)
		{
			this.namespaceName = namespaceName;
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x00002139 File Offset: 0x00000339
		public override SelectListItemKind Kind
		{
			get
			{
				return SelectListItemKind.QualifiedWildcard;
			}
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x000C28D4 File Offset: 0x000C0AD4
		public string NamespaceName
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x04001F35 RID: 7989
		private readonly string namespaceName;
	}
}
