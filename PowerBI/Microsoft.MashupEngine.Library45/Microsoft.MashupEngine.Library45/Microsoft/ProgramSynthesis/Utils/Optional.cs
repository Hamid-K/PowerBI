using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004D8 RID: 1240
	[DataContract]
	public struct Optional<T> : IOptional, IEquatable<Optional<T>>
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x00053054 File Offset: 0x00051254
		public T Value
		{
			get
			{
				if (!this.HasValue)
				{
					throw new InvalidOperationException("value is not present");
				}
				return this._value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x0005306F File Offset: 0x0005126F
		object IOptional.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0005307C File Offset: 0x0005127C
		public bool HasValue
		{
			get
			{
				return this._hasValue;
			}
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00053084 File Offset: 0x00051284
		public override string ToString()
		{
			if (!this.HasValue)
			{
				return "<Nothing>";
			}
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x000530B3 File Offset: 0x000512B3
		public static implicit operator Optional<T>(Optional<Optional<T>> doubleOptional)
		{
			if (!doubleOptional.HasValue)
			{
				return Optional<T>.Nothing;
			}
			return doubleOptional.Value;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x000530CB File Offset: 0x000512CB
		internal Optional(T value)
		{
			this._value = value;
			this._hasValue = true;
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000530DC File Offset: 0x000512DC
		public bool Equals(Optional<T> other)
		{
			return EqualityComparer<T>.Default.Equals(this._value, other._value) && this._hasValue.Equals(other._hasValue);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00053117 File Offset: 0x00051317
		public override bool Equals(object obj)
		{
			return obj != null && obj is Optional<T> && this.Equals((Optional<T>)obj);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00053134 File Offset: 0x00051334
		public override int GetHashCode()
		{
			return (EqualityComparer<T>.Default.GetHashCode(this._value) * 13024181) ^ this._hasValue.GetHashCode();
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00053166 File Offset: 0x00051366
		public static bool operator ==(Optional<T> left, Optional<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x00053170 File Offset: 0x00051370
		public static bool operator !=(Optional<T> left, Optional<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x00053180 File Offset: 0x00051380
		public static bool operator ==(Optional<T> left, T right)
		{
			T value = left.Value;
			return value.Equals(right);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000531A8 File Offset: 0x000513A8
		public static bool operator !=(Optional<T> left, T right)
		{
			T value = left.Value;
			return !value.Equals(right);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000531D4 File Offset: 0x000513D4
		public static bool operator ==(T left, Optional<T> right)
		{
			T value = right.Value;
			return value.Equals(left);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x000531FC File Offset: 0x000513FC
		public static bool operator !=(T left, Optional<T> right)
		{
			T value = right.Value;
			return !value.Equals(left);
		}

		// Token: 0x04000D7F RID: 3455
		[DataMember]
		private readonly T _value;

		// Token: 0x04000D80 RID: 3456
		[DataMember]
		private readonly bool _hasValue;

		// Token: 0x04000D81 RID: 3457
		public static readonly Optional<T> Nothing;
	}
}
