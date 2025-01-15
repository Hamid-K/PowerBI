using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000B8 RID: 184
	public sealed class SingleValueFunctionCallNode : SingleValueNode
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x0000EAF2 File Offset: 0x0000CCF2
		public SingleValueFunctionCallNode(string name, IEnumerable<QueryNode> arguments, IEdmTypeReference typeReference)
			: this(name, null, arguments, typeReference, null)
		{
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000EB00 File Offset: 0x0000CD00
		public SingleValueFunctionCallNode(string name, IEnumerable<IEdmFunctionImport> functionImports, IEnumerable<QueryNode> arguments, IEdmTypeReference typeReference, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			this.name = name;
			this.functionImports = new ReadOnlyCollection<IEdmFunctionImport>((functionImports != null) ? Enumerable.ToList<IEdmFunctionImport>(functionImports) : new List<IEdmFunctionImport>());
			this.arguments = arguments;
			this.typeReference = typeReference;
			this.source = source;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000EB57 File Offset: 0x0000CD57
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000EB5F File Offset: 0x0000CD5F
		public IEnumerable<IEdmFunctionImport> FunctionImports
		{
			get
			{
				return this.functionImports;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000EB67 File Offset: 0x0000CD67
		public IEnumerable<QueryNode> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000EB6F File Offset: 0x0000CD6F
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000EB77 File Offset: 0x0000CD77
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000EB7F File Offset: 0x0000CD7F
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueFunctionCall;
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000EB82 File Offset: 0x0000CD82
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000181 RID: 385
		private readonly string name;

		// Token: 0x04000182 RID: 386
		private readonly ReadOnlyCollection<IEdmFunctionImport> functionImports;

		// Token: 0x04000183 RID: 387
		private readonly IEnumerable<QueryNode> arguments;

		// Token: 0x04000184 RID: 388
		private readonly IEdmTypeReference typeReference;

		// Token: 0x04000185 RID: 389
		private readonly QueryNode source;
	}
}
