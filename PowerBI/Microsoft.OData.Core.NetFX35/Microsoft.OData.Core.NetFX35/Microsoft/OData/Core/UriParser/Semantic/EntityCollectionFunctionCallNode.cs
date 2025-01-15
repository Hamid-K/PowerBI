using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000234 RID: 564
	public sealed class EntityCollectionFunctionCallNode : EntityCollectionNode
	{
		// Token: 0x0600144C RID: 5196 RVA: 0x0004957C File Offset: 0x0004777C
		public EntityCollectionFunctionCallNode(string name, IEnumerable<IEdmFunction> functions, IEnumerable<QueryNode> parameters, IEdmCollectionTypeReference returnedCollectionTypeReference, IEdmEntitySetBase navigationSource, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(returnedCollectionTypeReference, "returnedCollectionTypeReference");
			this.name = name;
			this.functions = new ReadOnlyCollection<IEdmFunction>((functions == null) ? new List<IEdmFunction>() : Enumerable.ToList<IEdmFunction>(functions));
			this.parameters = new ReadOnlyCollection<QueryNode>((parameters == null) ? new List<QueryNode>() : Enumerable.ToList<QueryNode>(parameters));
			this.returnedCollectionTypeReference = returnedCollectionTypeReference;
			this.navigationSource = navigationSource;
			this.entityTypeReference = returnedCollectionTypeReference.ElementType().AsEntityOrNull();
			if (this.entityTypeReference == null)
			{
				throw new ArgumentException(Strings.Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity);
			}
			this.source = source;
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00049620 File Offset: 0x00047820
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x00049628 File Offset: 0x00047828
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00049630 File Offset: 0x00047830
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00049638 File Offset: 0x00047838
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x00049640 File Offset: 0x00047840
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.returnedCollectionTypeReference;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x00049648 File Offset: 0x00047848
		public override IEdmEntityTypeReference EntityItemType
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00049650 File Offset: 0x00047850
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00049658 File Offset: 0x00047858
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00049660 File Offset: 0x00047860
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.EntityCollectionFunctionCall;
			}
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00049664 File Offset: 0x00047864
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000888 RID: 2184
		private readonly string name;

		// Token: 0x04000889 RID: 2185
		private readonly ReadOnlyCollection<IEdmFunction> functions;

		// Token: 0x0400088A RID: 2186
		private readonly ReadOnlyCollection<QueryNode> parameters;

		// Token: 0x0400088B RID: 2187
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x0400088C RID: 2188
		private readonly IEdmCollectionTypeReference returnedCollectionTypeReference;

		// Token: 0x0400088D RID: 2189
		private readonly IEdmEntitySetBase navigationSource;

		// Token: 0x0400088E RID: 2190
		private readonly QueryNode source;
	}
}
