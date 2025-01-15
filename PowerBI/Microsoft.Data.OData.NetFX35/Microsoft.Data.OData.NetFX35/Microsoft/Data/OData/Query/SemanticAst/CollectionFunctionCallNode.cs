using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200001E RID: 30
	public sealed class CollectionFunctionCallNode : CollectionNode
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00003F20 File Offset: 0x00002120
		public CollectionFunctionCallNode(string name, IEnumerable<IEdmFunctionImport> functionImports, IEnumerable<QueryNode> parameters, IEdmCollectionTypeReference returnedCollectionType, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(returnedCollectionType, "returnedCollectionType");
			this.name = name;
			this.functionImports = new ReadOnlyCollection<IEdmFunctionImport>((functionImports == null) ? new List<IEdmFunctionImport>() : Enumerable.ToList<IEdmFunctionImport>(functionImports));
			this.parameters = new ReadOnlyCollection<QueryNode>((parameters == null) ? new List<QueryNode>() : Enumerable.ToList<QueryNode>(parameters));
			this.returnedCollectionType = returnedCollectionType;
			this.itemType = returnedCollectionType.ElementType();
			if (!this.itemType.IsPrimitive() && !this.itemType.IsComplex())
			{
				throw new ArgumentException(Strings.Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplex);
			}
			this.source = source;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003FC9 File Offset: 0x000021C9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003FD1 File Offset: 0x000021D1
		public IEnumerable<IEdmFunctionImport> FunctionImports
		{
			get
			{
				return this.functionImports;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003FD9 File Offset: 0x000021D9
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003FE1 File Offset: 0x000021E1
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003FE9 File Offset: 0x000021E9
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.returnedCollectionType;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003FF1 File Offset: 0x000021F1
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003FF9 File Offset: 0x000021F9
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionFunctionCall;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003FFD File Offset: 0x000021FD
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000047 RID: 71
		private readonly string name;

		// Token: 0x04000048 RID: 72
		private readonly ReadOnlyCollection<IEdmFunctionImport> functionImports;

		// Token: 0x04000049 RID: 73
		private readonly ReadOnlyCollection<QueryNode> parameters;

		// Token: 0x0400004A RID: 74
		private readonly IEdmTypeReference itemType;

		// Token: 0x0400004B RID: 75
		private readonly IEdmCollectionTypeReference returnedCollectionType;

		// Token: 0x0400004C RID: 76
		private readonly QueryNode source;
	}
}
