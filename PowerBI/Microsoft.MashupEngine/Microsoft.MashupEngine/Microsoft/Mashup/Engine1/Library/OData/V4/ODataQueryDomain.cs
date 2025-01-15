using System;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000884 RID: 2180
	internal sealed class ODataQueryDomain : IQueryDomain
	{
		// Token: 0x06003EB9 RID: 16057 RVA: 0x000CD39F File Offset: 0x000CB59F
		public ODataQueryDomain(string metadataUrl)
		{
			this.metadataUrl = metadataUrl;
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x06003EBA RID: 16058 RVA: 0x000CD3AE File Offset: 0x000CB5AE
		public string MetadataUrl
		{
			get
			{
				return this.metadataUrl;
			}
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x000CD3B8 File Offset: 0x000CB5B8
		public bool IsCompatibleWith(IQueryDomain domain)
		{
			ODataQueryDomain odataQueryDomain = domain as ODataQueryDomain;
			return odataQueryDomain != null && odataQueryDomain.MetadataUrl == this.MetadataUrl;
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x00002139 File Offset: 0x00000339
		public bool CanIndex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x000CD3E2 File Offset: 0x000CB5E2
		public Query Optimize(Query query)
		{
			return new ODataOptimizingQueryVisitor().Optimize(query);
		}

		// Token: 0x040020E7 RID: 8423
		private readonly string metadataUrl;
	}
}
