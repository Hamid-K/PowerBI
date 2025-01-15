using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000130 RID: 304
	public sealed class ConstantNode : SingleValueNode
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x00028F2F File Offset: 0x0002712F
		public ConstantNode(object constantValue, string literalText)
			: this(constantValue)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalText, "literalText");
			this.LiteralText = literalText;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00028F4A File Offset: 0x0002714A
		public ConstantNode(object constantValue, string literalText, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalText, "literalText");
			this.constantValue = constantValue;
			this.LiteralText = literalText;
			this.typeReference = typeReference;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00028F72 File Offset: 0x00027172
		public ConstantNode(object constantValue)
		{
			this.constantValue = constantValue;
			this.typeReference = ((constantValue == null) ? null : EdmLibraryExtensions.GetPrimitiveTypeReference(constantValue.GetType()));
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00028F98 File Offset: 0x00027198
		public object Value
		{
			get
			{
				return this.constantValue;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00028FA0 File Offset: 0x000271A0
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x00028FA8 File Offset: 0x000271A8
		public string LiteralText { get; private set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00028FB1 File Offset: 0x000271B1
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00002503 File Offset: 0x00000703
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Constant;
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00028FB9 File Offset: 0x000271B9
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000747 RID: 1863
		private readonly object constantValue;

		// Token: 0x04000748 RID: 1864
		private readonly IEdmTypeReference typeReference;
	}
}
