using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200022E RID: 558
	public sealed class ConstantNode : SingleValueNode
	{
		// Token: 0x0600141B RID: 5147 RVA: 0x0004921E File Offset: 0x0004741E
		public ConstantNode(object constantValue, string literalText)
			: this(constantValue)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalText, "literalText");
			this.LiteralText = literalText;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00049239 File Offset: 0x00047439
		public ConstantNode(object constantValue, string literalText, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalText, "literalText");
			this.constantValue = constantValue;
			this.LiteralText = literalText;
			this.typeReference = typeReference;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00049261 File Offset: 0x00047461
		public ConstantNode(object constantValue)
		{
			this.constantValue = constantValue;
			this.typeReference = ((constantValue == null) ? null : EdmLibraryExtensions.GetPrimitiveTypeReference(constantValue.GetType()));
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00049287 File Offset: 0x00047487
		public object Value
		{
			get
			{
				return this.constantValue;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0004928F File Offset: 0x0004748F
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x00049297 File Offset: 0x00047497
		public string LiteralText { get; private set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x000492A0 File Offset: 0x000474A0
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x000492A8 File Offset: 0x000474A8
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Constant;
			}
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x000492AB File Offset: 0x000474AB
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400087D RID: 2173
		private readonly object constantValue;

		// Token: 0x0400087E RID: 2174
		private readonly IEdmTypeReference typeReference;
	}
}
