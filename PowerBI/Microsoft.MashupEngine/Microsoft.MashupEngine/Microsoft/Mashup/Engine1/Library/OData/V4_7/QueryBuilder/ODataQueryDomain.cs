using System;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007E9 RID: 2025
	internal sealed class ODataQueryDomain : IQueryDomain
	{
		// Token: 0x06003AAC RID: 15020 RVA: 0x000BE1F6 File Offset: 0x000BC3F6
		public ODataQueryDomain(string metadataUrl)
		{
			this.metadataUrl = metadataUrl;
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x000BE205 File Offset: 0x000BC405
		public string MetadataUrl
		{
			get
			{
				return this.metadataUrl;
			}
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000BE210 File Offset: 0x000BC410
		public bool IsCompatibleWith(IQueryDomain domain)
		{
			ODataQueryDomain odataQueryDomain = domain as ODataQueryDomain;
			return odataQueryDomain != null && odataQueryDomain.MetadataUrl == this.MetadataUrl;
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x00002139 File Offset: 0x00000339
		public bool CanIndex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000BE23A File Offset: 0x000BC43A
		public Query Optimize(Query query)
		{
			return new ODataOptimizingQueryVisitor().Optimize(query);
		}

		// Token: 0x04001E71 RID: 7793
		private readonly string metadataUrl;
	}
}
