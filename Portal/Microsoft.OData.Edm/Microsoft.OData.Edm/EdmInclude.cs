using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000039 RID: 57
	public class EdmInclude : IEdmInclude
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00003FA3 File Offset: 0x000021A3
		public EdmInclude(string alias, string namespaceIncluded)
		{
			this.alias = alias;
			this.namespaceIncluded = namespaceIncluded;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003FB9 File Offset: 0x000021B9
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00003FC1 File Offset: 0x000021C1
		public string Namespace
		{
			get
			{
				return this.namespaceIncluded;
			}
		}

		// Token: 0x04000064 RID: 100
		private readonly string alias;

		// Token: 0x04000065 RID: 101
		private readonly string namespaceIncluded;
	}
}
