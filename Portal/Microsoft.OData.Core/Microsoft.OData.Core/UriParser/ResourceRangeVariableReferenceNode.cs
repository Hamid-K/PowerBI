using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017F RID: 383
	public sealed class ResourceRangeVariableReferenceNode : SingleResourceNode
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x00038DFC File Offset: 0x00036FFC
		public ResourceRangeVariableReferenceNode(string name, ResourceRangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<ResourceRangeVariable>(rangeVariable, "rangeVariable");
			this.name = name;
			this.navigationSource = rangeVariable.NavigationSource;
			this.structuredTypeReference = rangeVariable.StructuredTypeReference;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00038E4D File Offset: 0x0003704D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00038E55 File Offset: 0x00037055
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00038E5D File Offset: 0x0003705D
		public ResourceRangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00038E65 File Offset: 0x00037065
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00038E55 File Offset: 0x00037055
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00038E6D File Offset: 0x0003706D
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.ResourceRangeVariableReference;
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00038E71 File Offset: 0x00037071
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400087F RID: 2175
		private readonly string name;

		// Token: 0x04000880 RID: 2176
		private readonly IEdmStructuredTypeReference structuredTypeReference;

		// Token: 0x04000881 RID: 2177
		private readonly ResourceRangeVariable rangeVariable;

		// Token: 0x04000882 RID: 2178
		private readonly IEdmNavigationSource navigationSource;
	}
}
