using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000228 RID: 552
	public sealed class CollectionFunctionCallNode : CollectionNode
	{
		// Token: 0x060013EF RID: 5103 RVA: 0x00048E6C File Offset: 0x0004706C
		public CollectionFunctionCallNode(string name, IEnumerable<IEdmFunction> functions, IEnumerable<QueryNode> parameters, IEdmCollectionTypeReference returnedCollectionType, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(returnedCollectionType, "returnedCollectionType");
			this.name = name;
			this.functions = new ReadOnlyCollection<IEdmFunction>((functions == null) ? new List<IEdmFunction>() : Enumerable.ToList<IEdmFunction>(functions));
			this.parameters = new ReadOnlyCollection<QueryNode>((parameters == null) ? new List<QueryNode>() : Enumerable.ToList<QueryNode>(parameters));
			this.returnedCollectionType = returnedCollectionType;
			this.itemType = returnedCollectionType.ElementType();
			if (!this.itemType.IsPrimitive() && !this.itemType.IsComplex() && !this.itemType.IsEnum())
			{
				throw new ArgumentException(Strings.Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum);
			}
			this.source = source;
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00048F22 File Offset: 0x00047122
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00048F2A File Offset: 0x0004712A
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00048F32 File Offset: 0x00047132
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00048F3A File Offset: 0x0004713A
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00048F42 File Offset: 0x00047142
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.returnedCollectionType;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00048F4A File Offset: 0x0004714A
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00048F52 File Offset: 0x00047152
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionFunctionCall;
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00048F56 File Offset: 0x00047156
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000869 RID: 2153
		private readonly string name;

		// Token: 0x0400086A RID: 2154
		private readonly ReadOnlyCollection<IEdmFunction> functions;

		// Token: 0x0400086B RID: 2155
		private readonly ReadOnlyCollection<QueryNode> parameters;

		// Token: 0x0400086C RID: 2156
		private readonly IEdmTypeReference itemType;

		// Token: 0x0400086D RID: 2157
		private readonly IEdmCollectionTypeReference returnedCollectionType;

		// Token: 0x0400086E RID: 2158
		private readonly QueryNode source;
	}
}
