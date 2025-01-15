using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017E RID: 382
	public sealed class ResourceRangeVariable : RangeVariable
	{
		// Token: 0x060012E9 RID: 4841 RVA: 0x00038D4C File Offset: 0x00036F4C
		public ResourceRangeVariable(string name, IEdmStructuredTypeReference structuredType, CollectionResourceNode collectionResourceNode)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredTypeReference>(structuredType, "structuredType");
			this.name = name;
			this.structuredTypeReference = structuredType;
			this.collectionResourceNode = collectionResourceNode;
			this.navigationSource = ((collectionResourceNode != null) ? collectionResourceNode.NavigationSource : null);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00038D9E File Offset: 0x00036F9E
		public ResourceRangeVariable(string name, IEdmStructuredTypeReference structuredType, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredTypeReference>(structuredType, "structuredType");
			this.name = name;
			this.structuredTypeReference = structuredType;
			this.collectionResourceNode = null;
			this.navigationSource = navigationSource;
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x00038DDA File Offset: 0x00036FDA
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00038DE2 File Offset: 0x00036FE2
		public CollectionResourceNode CollectionResourceNode
		{
			get
			{
				return this.collectionResourceNode;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00038DEA File Offset: 0x00036FEA
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00038DF2 File Offset: 0x00036FF2
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x00038DF2 File Offset: 0x00036FF2
		public IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00002390 File Offset: 0x00000590
		public override int Kind
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0400087B RID: 2171
		private readonly string name;

		// Token: 0x0400087C RID: 2172
		private readonly CollectionResourceNode collectionResourceNode;

		// Token: 0x0400087D RID: 2173
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x0400087E RID: 2174
		private readonly IEdmStructuredTypeReference structuredTypeReference;
	}
}
