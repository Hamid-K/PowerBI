using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015D RID: 349
	public sealed class SingleValueFunctionCallNode : SingleValueNode
	{
		// Token: 0x06000F0F RID: 3855 RVA: 0x0002B69A File Offset: 0x0002989A
		public SingleValueFunctionCallNode(string name, IEnumerable<QueryNode> parameters, IEdmTypeReference returnedTypeReference)
			: this(name, null, parameters, returnedTypeReference, null)
		{
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0002B6A8 File Offset: 0x000298A8
		public SingleValueFunctionCallNode(string name, IEnumerable<IEdmFunction> functions, IEnumerable<QueryNode> parameters, IEdmTypeReference returnedTypeReference, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			this.name = name;
			this.functions = new ReadOnlyCollection<IEdmFunction>((functions != null) ? Enumerable.ToList<IEdmFunction>(functions) : new List<IEdmFunction>());
			this.parameters = parameters ?? Enumerable.Empty<QueryNode>();
			if (returnedTypeReference != null && (returnedTypeReference.IsCollection() || (!returnedTypeReference.IsComplex() && !returnedTypeReference.IsPrimitive() && !returnedTypeReference.IsEnum())))
			{
				throw new ArgumentException(Strings.Nodes_SingleValueFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum);
			}
			this.returnedTypeReference = returnedTypeReference;
			this.source = source;
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0002B73C File Offset: 0x0002993C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0002B744 File Offset: 0x00029944
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0002B74C File Offset: 0x0002994C
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0002B754 File Offset: 0x00029954
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.returnedTypeReference;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0002B75C File Offset: 0x0002995C
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0002B764 File Offset: 0x00029964
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueFunctionCall;
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0002B767 File Offset: 0x00029967
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400079D RID: 1949
		private readonly string name;

		// Token: 0x0400079E RID: 1950
		private readonly ReadOnlyCollection<IEdmFunction> functions;

		// Token: 0x0400079F RID: 1951
		private readonly IEnumerable<QueryNode> parameters;

		// Token: 0x040007A0 RID: 1952
		private readonly IEdmTypeReference returnedTypeReference;

		// Token: 0x040007A1 RID: 1953
		private readonly QueryNode source;
	}
}
