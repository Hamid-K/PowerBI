using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017B RID: 379
	public sealed class CollectionResourceFunctionCallNode : CollectionResourceNode
	{
		// Token: 0x060012D8 RID: 4824 RVA: 0x00038C2C File Offset: 0x00036E2C
		public CollectionResourceFunctionCallNode(string name, IEnumerable<IEdmFunction> functions, IEnumerable<QueryNode> parameters, IEdmCollectionTypeReference returnedCollectionTypeReference, IEdmEntitySetBase navigationSource, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(returnedCollectionTypeReference, "returnedCollectionTypeReference");
			this.name = name;
			this.functions = new ReadOnlyCollection<IEdmFunction>((functions == null) ? new List<IEdmFunction>() : functions.ToList<IEdmFunction>());
			this.parameters = new ReadOnlyCollection<QueryNode>((parameters == null) ? new List<QueryNode>() : parameters.ToList<QueryNode>());
			this.returnedCollectionTypeReference = returnedCollectionTypeReference;
			this.navigationSource = navigationSource;
			this.structuredTypeReference = returnedCollectionTypeReference.ElementType().AsStructuredOrNull();
			if (this.structuredTypeReference == null)
			{
				throw new ArgumentException(Strings.Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity);
			}
			this.source = source;
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00038CD2 File Offset: 0x00036ED2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00038CDA File Offset: 0x00036EDA
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00038CE2 File Offset: 0x00036EE2
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00038CEA File Offset: 0x00036EEA
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x00038CF2 File Offset: 0x00036EF2
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.returnedCollectionTypeReference;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00038CEA File Offset: 0x00036EEA
		public override IEdmStructuredTypeReference ItemStructuredType
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x00038CFA File Offset: 0x00036EFA
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00038D02 File Offset: 0x00036F02
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x00038D0A File Offset: 0x00036F0A
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionResourceFunctionCall;
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00038D0E File Offset: 0x00036F0E
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000873 RID: 2163
		private readonly string name;

		// Token: 0x04000874 RID: 2164
		private readonly ReadOnlyCollection<IEdmFunction> functions;

		// Token: 0x04000875 RID: 2165
		private readonly ReadOnlyCollection<QueryNode> parameters;

		// Token: 0x04000876 RID: 2166
		private readonly IEdmStructuredTypeReference structuredTypeReference;

		// Token: 0x04000877 RID: 2167
		private readonly IEdmCollectionTypeReference returnedCollectionTypeReference;

		// Token: 0x04000878 RID: 2168
		private readonly IEdmEntitySetBase navigationSource;

		// Token: 0x04000879 RID: 2169
		private readonly QueryNode source;
	}
}
