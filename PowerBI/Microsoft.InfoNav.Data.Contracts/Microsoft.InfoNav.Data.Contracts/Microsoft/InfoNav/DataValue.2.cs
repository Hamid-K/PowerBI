using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000063 RID: 99
	[ImmutableObject(true)]
	public abstract class DataValue<T> : DataValue
	{
		// Token: 0x060001EB RID: 491 RVA: 0x00005AA7 File Offset: 0x00003CA7
		protected DataValue(T value)
		{
			this._value = value;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00005AB6 File Offset: 0x00003CB6
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public override bool Equals(DataValue other)
		{
			DataValue<T> dataValue = other as DataValue<T>;
			if (dataValue == null)
			{
				return false;
			}
			if (this.Type == dataValue.Type)
			{
				T value = this._value;
				return value.Equals(dataValue._value);
			}
			return false;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005B08 File Offset: 0x00003D08
		public override string ToString()
		{
			if (this._value == null)
			{
				return "(null)";
			}
			T value = this._value;
			return value.ToString();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005B3C File Offset: 0x00003D3C
		protected override int GetHashCodeCore()
		{
			T value = this._value;
			return value.GetHashCode();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00005B5D File Offset: 0x00003D5D
		internal override object GetValueAsObject()
		{
			return this._value;
		}

		// Token: 0x04000138 RID: 312
		private readonly T _value;
	}
}
