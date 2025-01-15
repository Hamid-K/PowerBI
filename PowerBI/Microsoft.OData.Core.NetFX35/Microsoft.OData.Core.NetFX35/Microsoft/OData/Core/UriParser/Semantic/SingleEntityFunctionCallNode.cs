using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000260 RID: 608
	public sealed class SingleEntityFunctionCallNode : SingleEntityNode
	{
		// Token: 0x0600156F RID: 5487 RVA: 0x0004B89D File Offset: 0x00049A9D
		public SingleEntityFunctionCallNode(string name, IEnumerable<QueryNode> parameters, IEdmEntityTypeReference returnedEntityTypeReference, IEdmNavigationSource navigationSource)
			: this(name, null, parameters, returnedEntityTypeReference, navigationSource, null)
		{
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0004B8AC File Offset: 0x00049AAC
		public SingleEntityFunctionCallNode(string name, IEnumerable<IEdmFunction> functions, IEnumerable<QueryNode> parameters, IEdmEntityTypeReference returnedEntityTypeReference, IEdmNavigationSource navigationSource, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityTypeReference>(returnedEntityTypeReference, "returnedEntityTypeReference");
			this.name = name;
			this.functions = new ReadOnlyCollection<IEdmFunction>((functions != null) ? Enumerable.ToList<IEdmFunction>(functions) : new List<IEdmFunction>());
			this.parameters = new ReadOnlyCollection<QueryNode>((parameters == null) ? new List<QueryNode>() : Enumerable.ToList<QueryNode>(parameters));
			this.returnedEntityTypeReference = returnedEntityTypeReference;
			this.navigationSource = navigationSource;
			this.source = source;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0004B92B File Offset: 0x00049B2B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0004B933 File Offset: 0x00049B33
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0004B93B File Offset: 0x00049B3B
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0004B943 File Offset: 0x00049B43
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.returnedEntityTypeReference;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0004B94B File Offset: 0x00049B4B
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x0004B953 File Offset: 0x00049B53
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.returnedEntityTypeReference;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0004B95B File Offset: 0x00049B5B
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0004B963 File Offset: 0x00049B63
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleEntityFunctionCall;
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0004B967 File Offset: 0x00049B67
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008EA RID: 2282
		private readonly string name;

		// Token: 0x040008EB RID: 2283
		private readonly ReadOnlyCollection<IEdmFunction> functions;

		// Token: 0x040008EC RID: 2284
		private readonly IEnumerable<QueryNode> parameters;

		// Token: 0x040008ED RID: 2285
		private readonly IEdmEntityTypeReference returnedEntityTypeReference;

		// Token: 0x040008EE RID: 2286
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x040008EF RID: 2287
		private readonly QueryNode source;
	}
}
