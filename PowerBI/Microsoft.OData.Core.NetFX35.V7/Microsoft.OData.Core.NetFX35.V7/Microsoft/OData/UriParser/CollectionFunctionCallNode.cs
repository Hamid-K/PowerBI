using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012B RID: 299
	public sealed class CollectionFunctionCallNode : CollectionNode
	{
		// Token: 0x06000DA0 RID: 3488 RVA: 0x00028BA0 File Offset: 0x00026DA0
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

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00028C58 File Offset: 0x00026E58
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00028C60 File Offset: 0x00026E60
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00028C68 File Offset: 0x00026E68
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00028C70 File Offset: 0x00026E70
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00028C78 File Offset: 0x00026E78
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.returnedCollectionType;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00028C80 File Offset: 0x00026E80
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00028C88 File Offset: 0x00026E88
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionFunctionCall;
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00028C8C File Offset: 0x00026E8C
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000734 RID: 1844
		private readonly string name;

		// Token: 0x04000735 RID: 1845
		private readonly ReadOnlyCollection<IEdmFunction> functions;

		// Token: 0x04000736 RID: 1846
		private readonly ReadOnlyCollection<QueryNode> parameters;

		// Token: 0x04000737 RID: 1847
		private readonly IEdmTypeReference itemType;

		// Token: 0x04000738 RID: 1848
		private readonly IEdmCollectionTypeReference returnedCollectionType;

		// Token: 0x04000739 RID: 1849
		private readonly QueryNode source;
	}
}
