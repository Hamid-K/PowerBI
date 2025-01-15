using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000061 RID: 97
	public class EdmInclude : IEdmInclude
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000AF46 File Offset: 0x00009146
		public EdmInclude(string alias, string namespaceIncluded)
		{
			this.alias = alias;
			this.namespaceIncluded = namespaceIncluded;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000AF5C File Offset: 0x0000915C
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000AF64 File Offset: 0x00009164
		public string Namespace
		{
			get
			{
				return this.namespaceIncluded;
			}
		}

		// Token: 0x040000C6 RID: 198
		private readonly string alias;

		// Token: 0x040000C7 RID: 199
		private readonly string namespaceIncluded;
	}
}
