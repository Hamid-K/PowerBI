using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200018E RID: 398
	public sealed class NonResourceRangeVariableReferenceNode : SingleValueNode
	{
		// Token: 0x06001366 RID: 4966 RVA: 0x00039776 File Offset: 0x00037976
		public NonResourceRangeVariableReferenceNode(string name, NonResourceRangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<NonResourceRangeVariable>(rangeVariable, "rangeVariable");
			this.name = name;
			this.typeReference = rangeVariable.TypeReference;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x000397B0 File Offset: 0x000379B0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x000397B8 File Offset: 0x000379B8
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x000397C0 File Offset: 0x000379C0
		public NonResourceRangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x000397C8 File Offset: 0x000379C8
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.NonResourceRangeVariableReference;
			}
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000397CB File Offset: 0x000379CB
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008A9 RID: 2217
		private readonly string name;

		// Token: 0x040008AA RID: 2218
		private readonly IEdmTypeReference typeReference;

		// Token: 0x040008AB RID: 2219
		private readonly NonResourceRangeVariable rangeVariable;
	}
}
