using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000122 RID: 290
	public class EdmBinaryConstant : EdmValue, IEdmBinaryConstantExpression, IEdmExpression, IEdmElement, IEdmBinaryValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x00012133 File Offset: 0x00010333
		public EdmBinaryConstant(byte[] value)
			: this(null, value)
		{
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001213D File Offset: 0x0001033D
		public EdmBinaryConstant(IEdmBinaryTypeReference type, byte[] value)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<byte[]>(value, "value");
			this.value = value;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00012159 File Offset: 0x00010359
		public byte[] Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BinaryConstant;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0000268E File Offset: 0x0000088E
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Binary;
			}
		}

		// Token: 0x04000328 RID: 808
		private readonly byte[] value;
	}
}
