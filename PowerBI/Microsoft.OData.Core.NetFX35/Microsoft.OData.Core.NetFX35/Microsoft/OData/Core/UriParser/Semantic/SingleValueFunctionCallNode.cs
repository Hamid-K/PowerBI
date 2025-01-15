using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000263 RID: 611
	public sealed class SingleValueFunctionCallNode : SingleValueNode
	{
		// Token: 0x06001589 RID: 5513 RVA: 0x0004BB04 File Offset: 0x00049D04
		public SingleValueFunctionCallNode(string name, IEnumerable<QueryNode> parameters, IEdmTypeReference returnedTypeReference)
			: this(name, null, parameters, returnedTypeReference, null)
		{
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0004BB14 File Offset: 0x00049D14
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

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x0004BBA7 File Offset: 0x00049DA7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0004BBAF File Offset: 0x00049DAF
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x0004BBB7 File Offset: 0x00049DB7
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0004BBBF File Offset: 0x00049DBF
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.returnedTypeReference;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0004BBC7 File Offset: 0x00049DC7
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0004BBCF File Offset: 0x00049DCF
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueFunctionCall;
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0004BBD2 File Offset: 0x00049DD2
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008F6 RID: 2294
		private readonly string name;

		// Token: 0x040008F7 RID: 2295
		private readonly ReadOnlyCollection<IEdmFunction> functions;

		// Token: 0x040008F8 RID: 2296
		private readonly IEnumerable<QueryNode> parameters;

		// Token: 0x040008F9 RID: 2297
		private readonly IEdmTypeReference returnedTypeReference;

		// Token: 0x040008FA RID: 2298
		private readonly QueryNode source;
	}
}
