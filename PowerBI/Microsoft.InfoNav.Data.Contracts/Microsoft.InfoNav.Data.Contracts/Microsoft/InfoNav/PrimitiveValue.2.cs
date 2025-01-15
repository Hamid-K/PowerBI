using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x0200005B RID: 91
	[ImmutableObject(true)]
	public abstract class PrimitiveValue<T> : PrimitiveValue where T : IEquatable<T>
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00003112 File Offset: 0x00001312
		protected PrimitiveValue(T value)
		{
			this._value = value;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00003121 File Offset: 0x00001321
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000312C File Offset: 0x0000132C
		public override bool Equals(PrimitiveValue other)
		{
			PrimitiveValue<T> primitiveValue = other as PrimitiveValue<T>;
			if (primitiveValue == null)
			{
				return false;
			}
			if (this.Type == primitiveValue.Type)
			{
				T value = this._value;
				return value.Equals(primitiveValue._value);
			}
			return false;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00003170 File Offset: 0x00001370
		public sealed override int GetHashCode()
		{
			T value = this._value;
			return value.GetHashCode();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00003191 File Offset: 0x00001391
		public override object GetValueAsObject()
		{
			return this._value;
		}

		// Token: 0x04000112 RID: 274
		private readonly T _value;
	}
}
