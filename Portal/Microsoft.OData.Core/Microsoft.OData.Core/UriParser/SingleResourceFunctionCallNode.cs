using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A8 RID: 424
	public sealed class SingleResourceFunctionCallNode : SingleResourceNode
	{
		// Token: 0x0600142B RID: 5163 RVA: 0x0003B4D6 File Offset: 0x000396D6
		public SingleResourceFunctionCallNode(string name, IEnumerable<QueryNode> parameters, IEdmStructuredTypeReference returnedStructuredTypeReference, IEdmNavigationSource navigationSource)
			: this(name, null, parameters, returnedStructuredTypeReference, navigationSource, null)
		{
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0003B4E8 File Offset: 0x000396E8
		public SingleResourceFunctionCallNode(string name, IEnumerable<IEdmFunction> functions, IEnumerable<QueryNode> parameters, IEdmStructuredTypeReference returnedStructuredTypeReference, IEdmNavigationSource navigationSource, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredTypeReference>(returnedStructuredTypeReference, "returnedStructuredTypeReference");
			this.name = name;
			this.functions = new ReadOnlyCollection<IEdmFunction>((functions != null) ? functions.ToList<IEdmFunction>() : new List<IEdmFunction>());
			this.parameters = new ReadOnlyCollection<QueryNode>((parameters == null) ? new List<QueryNode>() : parameters.ToList<QueryNode>());
			this.returnedStructuredTypeReference = returnedStructuredTypeReference;
			this.navigationSource = navigationSource;
			this.source = source;
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0003B569 File Offset: 0x00039769
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0003B571 File Offset: 0x00039771
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0003B579 File Offset: 0x00039779
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0003B581 File Offset: 0x00039781
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.returnedStructuredTypeReference;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0003B589 File Offset: 0x00039789
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0003B581 File Offset: 0x00039781
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.returnedStructuredTypeReference;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0003B591 File Offset: 0x00039791
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0003B599 File Offset: 0x00039799
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleResourceFunctionCall;
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0003B59D File Offset: 0x0003979D
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008DE RID: 2270
		private readonly string name;

		// Token: 0x040008DF RID: 2271
		private readonly ReadOnlyCollection<IEdmFunction> functions;

		// Token: 0x040008E0 RID: 2272
		private readonly IEnumerable<QueryNode> parameters;

		// Token: 0x040008E1 RID: 2273
		private readonly IEdmStructuredTypeReference returnedStructuredTypeReference;

		// Token: 0x040008E2 RID: 2274
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x040008E3 RID: 2275
		private readonly QueryNode source;
	}
}
