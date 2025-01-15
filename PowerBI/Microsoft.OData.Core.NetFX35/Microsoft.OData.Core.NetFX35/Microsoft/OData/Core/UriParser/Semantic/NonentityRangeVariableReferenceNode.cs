using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200024A RID: 586
	public sealed class NonentityRangeVariableReferenceNode : SingleValueNode
	{
		// Token: 0x060014D6 RID: 5334 RVA: 0x0004A010 File Offset: 0x00048210
		public NonentityRangeVariableReferenceNode(string name, NonentityRangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<NonentityRangeVariable>(rangeVariable, "rangeVariable");
			this.name = name;
			this.typeReference = rangeVariable.TypeReference;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0004A048 File Offset: 0x00048248
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0004A050 File Offset: 0x00048250
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0004A058 File Offset: 0x00048258
		public NonentityRangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0004A060 File Offset: 0x00048260
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.NonentityRangeVariableReference;
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0004A063 File Offset: 0x00048263
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008BB RID: 2235
		private readonly string name;

		// Token: 0x040008BC RID: 2236
		private readonly IEdmTypeReference typeReference;

		// Token: 0x040008BD RID: 2237
		private readonly NonentityRangeVariable rangeVariable;
	}
}
