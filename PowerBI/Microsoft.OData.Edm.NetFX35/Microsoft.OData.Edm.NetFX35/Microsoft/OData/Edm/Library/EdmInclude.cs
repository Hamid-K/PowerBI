using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000104 RID: 260
	public class EdmInclude : IEdmInclude
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x0000D553 File Offset: 0x0000B753
		public EdmInclude(string alias, string namespaceIncluded)
		{
			this.alias = alias;
			this.namespaceIncluded = namespaceIncluded;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0000D569 File Offset: 0x0000B769
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000D571 File Offset: 0x0000B771
		public string Namespace
		{
			get
			{
				return this.namespaceIncluded;
			}
		}

		// Token: 0x040001EE RID: 494
		private readonly string alias;

		// Token: 0x040001EF RID: 495
		private readonly string namespaceIncluded;
	}
}
