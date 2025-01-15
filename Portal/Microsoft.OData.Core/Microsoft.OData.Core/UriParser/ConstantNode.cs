using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000175 RID: 373
	public sealed class ConstantNode : SingleValueNode
	{
		// Token: 0x060012A3 RID: 4771 RVA: 0x00038863 File Offset: 0x00036A63
		public ConstantNode(object constantValue, string literalText)
			: this(constantValue)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalText, "literalText");
			this.LiteralText = literalText;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0003887E File Offset: 0x00036A7E
		public ConstantNode(object constantValue, string literalText, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalText, "literalText");
			this.constantValue = constantValue;
			this.LiteralText = literalText;
			this.typeReference = typeReference;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000388A6 File Offset: 0x00036AA6
		public ConstantNode(object constantValue)
		{
			this.constantValue = constantValue;
			this.typeReference = ((constantValue == null) ? null : EdmLibraryExtensions.GetPrimitiveTypeReference(constantValue.GetType()));
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x000388CC File Offset: 0x00036ACC
		public object Value
		{
			get
			{
				return this.constantValue;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x000388D4 File Offset: 0x00036AD4
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x000388DC File Offset: 0x00036ADC
		public string LiteralText { get; private set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x000388E5 File Offset: 0x00036AE5
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00002393 File Offset: 0x00000593
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Constant;
			}
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000388ED File Offset: 0x00036AED
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000868 RID: 2152
		private readonly object constantValue;

		// Token: 0x04000869 RID: 2153
		private readonly IEdmTypeReference typeReference;
	}
}
