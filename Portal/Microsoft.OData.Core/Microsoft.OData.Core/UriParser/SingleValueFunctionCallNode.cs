using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AB RID: 427
	public sealed class SingleValueFunctionCallNode : SingleValueNode
	{
		// Token: 0x06001446 RID: 5190 RVA: 0x0003B746 File Offset: 0x00039946
		public SingleValueFunctionCallNode(string name, IEnumerable<QueryNode> parameters, IEdmTypeReference returnedTypeReference)
			: this(name, null, parameters, returnedTypeReference, null)
		{
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0003B754 File Offset: 0x00039954
		public SingleValueFunctionCallNode(string name, IEnumerable<IEdmFunction> functions, IEnumerable<QueryNode> parameters, IEdmTypeReference returnedTypeReference, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			this.name = name;
			this.functions = new ReadOnlyCollection<IEdmFunction>((functions != null) ? functions.ToList<IEdmFunction>() : new List<IEdmFunction>());
			this.parameters = parameters ?? Enumerable.Empty<QueryNode>();
			if (returnedTypeReference != null && (returnedTypeReference.IsCollection() || (!returnedTypeReference.IsComplex() && !returnedTypeReference.IsPrimitive() && !returnedTypeReference.IsEnum())))
			{
				throw new ArgumentException(Strings.Nodes_SingleValueFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum);
			}
			this.returnedTypeReference = returnedTypeReference;
			this.source = source;
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0003B7E8 File Offset: 0x000399E8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0003B7F0 File Offset: 0x000399F0
		public IEnumerable<IEdmFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0003B7F8 File Offset: 0x000399F8
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0003B800 File Offset: 0x00039A00
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.returnedTypeReference;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0003B808 File Offset: 0x00039A08
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0003B810 File Offset: 0x00039A10
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueFunctionCall;
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0003B813 File Offset: 0x00039A13
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
		private readonly IEdmTypeReference returnedTypeReference;

		// Token: 0x040008EE RID: 2286
		private readonly QueryNode source;
	}
}
