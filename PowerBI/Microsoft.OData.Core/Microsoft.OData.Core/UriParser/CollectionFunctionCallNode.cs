using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000170 RID: 368
	public sealed class CollectionFunctionCallNode : CollectionNode
	{
		// Token: 0x06001278 RID: 4728 RVA: 0x000384D4 File Offset: 0x000366D4
		public CollectionFunctionCallNode(string name, IEnumerable<IEdmFunction> functions, IEnumerable<QueryNode> parameters, IEdmCollectionTypeReference returnedCollectionType, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(returnedCollectionType, "returnedCollectionType");
			this.name = name;
			this.functions = new ReadOnlyCollection<IEdmFunction>((functions == null) ? new List<IEdmFunction>() : functions.ToList<IEdmFunction>());
			this.parameters = new ReadOnlyCollection<QueryNode>((parameters == null) ? new List<QueryNode>() : parameters.ToList<QueryNode>());
			this.returnedCollectionType = returnedCollectionType;
			this.itemType = returnedCollectionType.ElementType();
			if (!this.itemType.IsPrimitive() && !this.itemType.IsComplex() && !this.itemType.IsEnum())
			{
				throw new ArgumentException(Strings.Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum);
			}
			this.source = source;
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x0003858C File Offset: 0x0003678C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00038594 File Offset: 0x00036794
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0003859C File Offset: 0x0003679C
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x000385A4 File Offset: 0x000367A4
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x000385AC File Offset: 0x000367AC
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.returnedCollectionType;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x000385B4 File Offset: 0x000367B4
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x000385BC File Offset: 0x000367BC
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionFunctionCall;
			}
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x000385C0 File Offset: 0x000367C0
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000855 RID: 2133
		private readonly string name;

		// Token: 0x04000856 RID: 2134
		private readonly ReadOnlyCollection<IEdmFunction> functions;

		// Token: 0x04000857 RID: 2135
		private readonly ReadOnlyCollection<QueryNode> parameters;

		// Token: 0x04000858 RID: 2136
		private readonly IEdmTypeReference itemType;

		// Token: 0x04000859 RID: 2137
		private readonly IEdmCollectionTypeReference returnedCollectionType;

		// Token: 0x0400085A RID: 2138
		private readonly QueryNode source;
	}
}
