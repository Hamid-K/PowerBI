using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B9 RID: 441
	public sealed class SingleResourceFunctionCallNode : SingleResourceNode
	{
		// Token: 0x0600117B RID: 4475 RVA: 0x00030AB0 File Offset: 0x0002ECB0
		public SingleResourceFunctionCallNode(string name, IEnumerable<QueryNode> parameters, IEdmStructuredTypeReference returnedStructuredTypeReference, IEdmNavigationSource navigationSource)
			: this(name, null, parameters, returnedStructuredTypeReference, navigationSource, null)
		{
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00030AC0 File Offset: 0x0002ECC0
		public SingleResourceFunctionCallNode(string name, IEnumerable<IEdmFunction> functions, IEnumerable<QueryNode> parameters, IEdmStructuredTypeReference returnedStructuredTypeReference, IEdmNavigationSource navigationSource, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredTypeReference>(returnedStructuredTypeReference, "returnedStructuredTypeReference");
			this.name = name;
			this.functions = new ReadOnlyCollection<IEdmFunction>((functions != null) ? Enumerable.ToList<IEdmFunction>(functions) : new List<IEdmFunction>());
			this.parameters = new ReadOnlyCollection<QueryNode>((parameters == null) ? new List<QueryNode>() : Enumerable.ToList<QueryNode>(parameters));
			this.returnedStructuredTypeReference = returnedStructuredTypeReference;
			this.navigationSource = navigationSource;
			this.source = source;
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00030B41 File Offset: 0x0002ED41
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00030B49 File Offset: 0x0002ED49
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00030B51 File Offset: 0x0002ED51
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00030B59 File Offset: 0x0002ED59
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.returnedStructuredTypeReference;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00030B61 File Offset: 0x0002ED61
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00030B59 File Offset: 0x0002ED59
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.returnedStructuredTypeReference;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00030B69 File Offset: 0x0002ED69
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0002BD04 File Offset: 0x00029F04
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleResourceFunctionCall;
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00030B71 File Offset: 0x0002ED71
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008ED RID: 2285
		private readonly string name;

		// Token: 0x040008EE RID: 2286
		private readonly ReadOnlyCollection<IEdmFunction> functions;

		// Token: 0x040008EF RID: 2287
		private readonly IEnumerable<QueryNode> parameters;

		// Token: 0x040008F0 RID: 2288
		private readonly IEdmStructuredTypeReference returnedStructuredTypeReference;

		// Token: 0x040008F1 RID: 2289
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x040008F2 RID: 2290
		private readonly QueryNode source;
	}
}
