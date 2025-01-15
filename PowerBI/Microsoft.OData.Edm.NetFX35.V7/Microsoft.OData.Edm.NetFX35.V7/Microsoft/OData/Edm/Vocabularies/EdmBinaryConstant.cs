using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000115 RID: 277
	public class EdmBinaryConstant : EdmValue, IEdmBinaryConstantExpression, IEdmExpression, IEdmElement, IEdmBinaryValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x00013C4F File Offset: 0x00011E4F
		public EdmBinaryConstant(byte[] value)
			: this(null, value)
		{
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00013C59 File Offset: 0x00011E59
		public EdmBinaryConstant(IEdmBinaryTypeReference type, byte[] value)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<byte[]>(value, "value");
			this.value = value;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00013C75 File Offset: 0x00011E75
		public byte[] Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BinaryConstant;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00008D76 File Offset: 0x00006F76
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Binary;
			}
		}

		// Token: 0x04000423 RID: 1059
		private readonly byte[] value;
	}
}
