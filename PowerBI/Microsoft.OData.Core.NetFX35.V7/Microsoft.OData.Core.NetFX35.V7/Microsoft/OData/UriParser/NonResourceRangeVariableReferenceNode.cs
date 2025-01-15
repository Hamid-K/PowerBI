using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B3 RID: 435
	public sealed class NonResourceRangeVariableReferenceNode : SingleValueNode
	{
		// Token: 0x06001150 RID: 4432 RVA: 0x0003079E File Offset: 0x0002E99E
		public NonResourceRangeVariableReferenceNode(string name, NonResourceRangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<NonResourceRangeVariable>(rangeVariable, "rangeVariable");
			this.name = name;
			this.typeReference = rangeVariable.TypeReference;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x000307D8 File Offset: 0x0002E9D8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x000307E0 File Offset: 0x0002E9E0
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x000307E8 File Offset: 0x0002E9E8
		public NonResourceRangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0002BBDF File Offset: 0x00029DDF
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.NonResourceRangeVariableReference;
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000307F0 File Offset: 0x0002E9F0
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008D9 RID: 2265
		private readonly string name;

		// Token: 0x040008DA RID: 2266
		private readonly IEdmTypeReference typeReference;

		// Token: 0x040008DB RID: 2267
		private readonly NonResourceRangeVariable rangeVariable;
	}
}
