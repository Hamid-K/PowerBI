using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x020001C3 RID: 451
	public class EdmBinaryConstant : EdmValue, IEdmBinaryConstantExpression, IEdmExpression, IEdmBinaryValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x00019637 File Offset: 0x00017837
		public EdmBinaryConstant(byte[] value)
			: this(null, value)
		{
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00019641 File Offset: 0x00017841
		public EdmBinaryConstant(IEdmBinaryTypeReference type, byte[] value)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<byte[]>(value, "value");
			this.value = value;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001965D File Offset: 0x0001785D
		public byte[] Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00019665 File Offset: 0x00017865
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BinaryConstant;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00019668 File Offset: 0x00017868
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Binary;
			}
		}

		// Token: 0x040004A8 RID: 1192
		private readonly byte[] value;
	}
}
