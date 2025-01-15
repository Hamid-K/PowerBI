using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AD RID: 429
	public sealed class CollectionResourceFunctionCallNode : CollectionResourceNode
	{
		// Token: 0x06001134 RID: 4404 RVA: 0x00030598 File Offset: 0x0002E798
		public CollectionResourceFunctionCallNode(string name, IEnumerable<IEdmFunction> functions, IEnumerable<QueryNode> parameters, IEdmCollectionTypeReference returnedCollectionTypeReference, IEdmEntitySetBase navigationSource, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(returnedCollectionTypeReference, "returnedCollectionTypeReference");
			this.name = name;
			this.functions = new ReadOnlyCollection<IEdmFunction>((functions == null) ? new List<IEdmFunction>() : Enumerable.ToList<IEdmFunction>(functions));
			this.parameters = new ReadOnlyCollection<QueryNode>((parameters == null) ? new List<QueryNode>() : Enumerable.ToList<QueryNode>(parameters));
			this.returnedCollectionTypeReference = returnedCollectionTypeReference;
			this.navigationSource = navigationSource;
			this.structuredTypeReference = returnedCollectionTypeReference.ElementType().AsStructuredOrNull();
			if (this.structuredTypeReference == null)
			{
				throw new ArgumentException(Strings.Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity);
			}
			this.source = source;
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0003063E File Offset: 0x0002E83E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00030646 File Offset: 0x0002E846
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0003064E File Offset: 0x0002E84E
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00030656 File Offset: 0x0002E856
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0003065E File Offset: 0x0002E85E
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.returnedCollectionTypeReference;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00030656 File Offset: 0x0002E856
		public override IEdmStructuredTypeReference ItemStructuredType
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00030666 File Offset: 0x0002E866
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0003066E File Offset: 0x0002E86E
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0002BB90 File Offset: 0x00029D90
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionResourceFunctionCall;
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00030676 File Offset: 0x0002E876
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008CB RID: 2251
		private readonly string name;

		// Token: 0x040008CC RID: 2252
		private readonly ReadOnlyCollection<IEdmFunction> functions;

		// Token: 0x040008CD RID: 2253
		private readonly ReadOnlyCollection<QueryNode> parameters;

		// Token: 0x040008CE RID: 2254
		private readonly IEdmStructuredTypeReference structuredTypeReference;

		// Token: 0x040008CF RID: 2255
		private readonly IEdmCollectionTypeReference returnedCollectionTypeReference;

		// Token: 0x040008D0 RID: 2256
		private readonly IEdmEntitySetBase navigationSource;

		// Token: 0x040008D1 RID: 2257
		private readonly QueryNode source;
	}
}
