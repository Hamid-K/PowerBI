using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B4 RID: 436
	public sealed class ResourceRangeVariable : RangeVariable
	{
		// Token: 0x06001156 RID: 4438 RVA: 0x00030808 File Offset: 0x0002EA08
		public ResourceRangeVariable(string name, IEdmStructuredTypeReference structuredType, CollectionResourceNode collectionResourceNode)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredTypeReference>(structuredType, "structuredType");
			this.name = name;
			this.structuredTypeReference = structuredType;
			this.collectionResourceNode = collectionResourceNode;
			this.navigationSource = ((collectionResourceNode != null) ? collectionResourceNode.NavigationSource : null);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0003085A File Offset: 0x0002EA5A
		public ResourceRangeVariable(string name, IEdmStructuredTypeReference structuredType, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredTypeReference>(structuredType, "structuredType");
			this.name = name;
			this.structuredTypeReference = structuredType;
			this.collectionResourceNode = null;
			this.navigationSource = navigationSource;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x00030896 File Offset: 0x0002EA96
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0003089E File Offset: 0x0002EA9E
		public CollectionResourceNode CollectionResourceNode
		{
			get
			{
				return this.collectionResourceNode;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x000308A6 File Offset: 0x0002EAA6
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x000308AE File Offset: 0x0002EAAE
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x000308AE File Offset: 0x0002EAAE
		public IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x00002500 File Offset: 0x00000700
		public override int Kind
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x040008DC RID: 2268
		private readonly string name;

		// Token: 0x040008DD RID: 2269
		private readonly CollectionResourceNode collectionResourceNode;

		// Token: 0x040008DE RID: 2270
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x040008DF RID: 2271
		private readonly IEdmStructuredTypeReference structuredTypeReference;
	}
}
