using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000085 RID: 133
	public sealed class SingleEntityFunctionCallNode : SingleEntityNode
	{
		// Token: 0x06000324 RID: 804 RVA: 0x0000B39B File Offset: 0x0000959B
		public SingleEntityFunctionCallNode(string name, IEnumerable<QueryNode> arguments, IEdmEntityTypeReference entityTypeReference, IEdmEntitySet entitySet)
			: this(name, null, arguments, entityTypeReference, entitySet, null)
		{
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000B3AC File Offset: 0x000095AC
		public SingleEntityFunctionCallNode(string name, IEnumerable<IEdmFunctionImport> functionImports, IEnumerable<QueryNode> arguments, IEdmEntityTypeReference entityTypeReference, IEdmEntitySet entitySet, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityTypeReference>(entityTypeReference, "typeReference");
			this.name = name;
			this.functionImports = new ReadOnlyCollection<IEdmFunctionImport>((functionImports != null) ? Enumerable.ToList<IEdmFunctionImport>(functionImports) : new List<IEdmFunctionImport>());
			this.arguments = arguments;
			this.entityTypeReference = entityTypeReference;
			this.entitySet = entitySet;
			this.source = source;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000B417 File Offset: 0x00009617
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000B41F File Offset: 0x0000961F
		public IEnumerable<IEdmFunctionImport> FunctionImports
		{
			get
			{
				return this.functionImports;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B427 File Offset: 0x00009627
		public IEnumerable<QueryNode> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000B42F File Offset: 0x0000962F
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B437 File Offset: 0x00009637
		public override IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000B43F File Offset: 0x0000963F
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000B447 File Offset: 0x00009647
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000B44F File Offset: 0x0000964F
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleEntityFunctionCall;
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000B453 File Offset: 0x00009653
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040000EC RID: 236
		private readonly string name;

		// Token: 0x040000ED RID: 237
		private readonly ReadOnlyCollection<IEdmFunctionImport> functionImports;

		// Token: 0x040000EE RID: 238
		private readonly IEnumerable<QueryNode> arguments;

		// Token: 0x040000EF RID: 239
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x040000F0 RID: 240
		private readonly IEdmEntitySet entitySet;

		// Token: 0x040000F1 RID: 241
		private readonly QueryNode source;
	}
}
