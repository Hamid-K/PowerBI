using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000053 RID: 83
	[ImmutableObject(true)]
	public sealed class BinaryPrimitiveValue : PrimitiveValue
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00002F71 File Offset: 0x00001171
		internal BinaryPrimitiveValue(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00002F80 File Offset: 0x00001180
		public byte[] Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00002F88 File Offset: 0x00001188
		public override ConceptualPrimitiveType Type
		{
			get
			{
				return ConceptualPrimitiveType.Binary;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00002F8C File Offset: 0x0000118C
		public override bool Equals(PrimitiveValue other)
		{
			BinaryPrimitiveValue binaryPrimitiveValue = other as BinaryPrimitiveValue;
			return binaryPrimitiveValue != null && (this.Type == binaryPrimitiveValue.Type && this._value.Length == binaryPrimitiveValue._value.Length) && this._value.SequenceEqual(binaryPrimitiveValue._value);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00002FD8 File Offset: 0x000011D8
		public sealed override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00002FE5 File Offset: 0x000011E5
		public override object GetValueAsObject()
		{
			return this._value;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00002FED File Offset: 0x000011ED
		public new static implicit operator BinaryPrimitiveValue(byte[] value)
		{
			return new BinaryPrimitiveValue(value);
		}

		// Token: 0x0400010E RID: 270
		private readonly byte[] _value;
	}
}
