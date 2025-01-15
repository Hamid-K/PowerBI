using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B5 RID: 437
	public sealed class ResourceRangeVariableReferenceNode : SingleResourceNode
	{
		// Token: 0x0600115E RID: 4446 RVA: 0x000308B8 File Offset: 0x0002EAB8
		public ResourceRangeVariableReferenceNode(string name, ResourceRangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<ResourceRangeVariable>(rangeVariable, "rangeVariable");
			this.name = name;
			this.navigationSource = rangeVariable.NavigationSource;
			this.structuredTypeReference = rangeVariable.StructuredTypeReference;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x00030909 File Offset: 0x0002EB09
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00030911 File Offset: 0x0002EB11
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x00030919 File Offset: 0x0002EB19
		public ResourceRangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00030921 File Offset: 0x0002EB21
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x00030911 File Offset: 0x0002EB11
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0002C05A File Offset: 0x0002A25A
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.ResourceRangeVariableReference;
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00030929 File Offset: 0x0002EB29
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008E0 RID: 2272
		private readonly string name;

		// Token: 0x040008E1 RID: 2273
		private readonly IEdmStructuredTypeReference structuredTypeReference;

		// Token: 0x040008E2 RID: 2274
		private readonly ResourceRangeVariable rangeVariable;

		// Token: 0x040008E3 RID: 2275
		private readonly IEdmNavigationSource navigationSource;
	}
}
