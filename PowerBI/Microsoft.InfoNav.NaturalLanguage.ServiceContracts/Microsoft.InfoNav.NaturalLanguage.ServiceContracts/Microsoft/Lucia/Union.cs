using System;
using System.Collections.Generic;

namespace Microsoft.Lucia
{
	// Token: 0x02000017 RID: 23
	public abstract class Union<T1, T2> : UnionBase, IEquatable<Union<T1, T2>>
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002A73 File Offset: 0x00000C73
		private Union()
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000052 RID: 82
		public abstract Type Type { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002A7B File Offset: 0x00000C7B
		public TypeCode TypeCode
		{
			get
			{
				return Type.GetTypeCode(this.Type);
			}
		}

		// Token: 0x06000054 RID: 84
		public abstract bool TryAs(out T1 value);

		// Token: 0x06000055 RID: 85
		public abstract bool TryAs(out T2 value);

		// Token: 0x06000056 RID: 86
		protected abstract bool EqualsCore(Union<T1, T2> other);

		// Token: 0x06000057 RID: 87
		protected abstract int GetHashCodeCore();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002A88 File Offset: 0x00000C88
		private static IEqualityComparer<T1> T1Comparer
		{
			get
			{
				return EqualityComparer<T1>.Default;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002A8F File Offset: 0x00000C8F
		private static IEqualityComparer<T2> T2Comparer
		{
			get
			{
				return EqualityComparer<T2>.Default;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A96 File Offset: 0x00000C96
		public bool Equals(Union<T1, T2> other)
		{
			return other != null && !(this.Type != other.Type) && this.EqualsCore(other);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002AB7 File Offset: 0x00000CB7
		public sealed override bool Equals(object obj)
		{
			return this.Equals(obj as Union<T1, T2>);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002AC5 File Offset: 0x00000CC5
		public sealed override int GetHashCode()
		{
			return this.GetHashCodeCore();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002ACD File Offset: 0x00000CCD
		public static implicit operator Union<T1, T2>(T1 value)
		{
			return new Union<T1, T2>.T1Container(value);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002AD5 File Offset: 0x00000CD5
		public static implicit operator Union<T1, T2>(T2 value)
		{
			return new Union<T1, T2>.T2Container(value);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public static explicit operator T1(Union<T1, T2> union)
		{
			T1 t;
			if (!union.TryAs(out t))
			{
				throw new InvalidCastException();
			}
			return t;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B00 File Offset: 0x00000D00
		public static explicit operator T2(Union<T1, T2> union)
		{
			T2 t;
			if (!union.TryAs(out t))
			{
				throw new InvalidCastException();
			}
			return t;
		}

		// Token: 0x020001E2 RID: 482
		private sealed class T1Container : Union<T1, T2>
		{
			// Token: 0x06000A85 RID: 2693 RVA: 0x00013730 File Offset: 0x00011930
			internal T1Container(T1 value)
			{
				this._value = value;
			}

			// Token: 0x17000323 RID: 803
			// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0001373F File Offset: 0x0001193F
			public override Type Type
			{
				get
				{
					return typeof(T1);
				}
			}

			// Token: 0x06000A87 RID: 2695 RVA: 0x0001374B File Offset: 0x0001194B
			public override object AsObject()
			{
				return this._value;
			}

			// Token: 0x06000A88 RID: 2696 RVA: 0x00013758 File Offset: 0x00011958
			public override bool TryAs(out T1 value)
			{
				value = this._value;
				return true;
			}

			// Token: 0x06000A89 RID: 2697 RVA: 0x00013767 File Offset: 0x00011967
			public override bool TryAs(out T2 value)
			{
				value = default(T2);
				return false;
			}

			// Token: 0x06000A8A RID: 2698 RVA: 0x00013771 File Offset: 0x00011971
			protected override bool EqualsCore(Union<T1, T2> other)
			{
				return Union<T1, T2>.T1Comparer.Equals(this._value, (T1)other);
			}

			// Token: 0x06000A8B RID: 2699 RVA: 0x00013789 File Offset: 0x00011989
			protected override int GetHashCodeCore()
			{
				return Union<T1, T2>.T1Comparer.GetHashCode(this._value);
			}

			// Token: 0x06000A8C RID: 2700 RVA: 0x0001379C File Offset: 0x0001199C
			public override string ToString()
			{
				T1 value = this._value;
				return ((value != null) ? value.ToString() : null) ?? string.Empty;
			}

			// Token: 0x040007FB RID: 2043
			private readonly T1 _value;
		}

		// Token: 0x020001E3 RID: 483
		private sealed class T2Container : Union<T1, T2>
		{
			// Token: 0x06000A8D RID: 2701 RVA: 0x000137D7 File Offset: 0x000119D7
			internal T2Container(T2 value)
			{
				this._value = value;
			}

			// Token: 0x17000324 RID: 804
			// (get) Token: 0x06000A8E RID: 2702 RVA: 0x000137E6 File Offset: 0x000119E6
			public override Type Type
			{
				get
				{
					return typeof(T2);
				}
			}

			// Token: 0x06000A8F RID: 2703 RVA: 0x000137F2 File Offset: 0x000119F2
			public override object AsObject()
			{
				return this._value;
			}

			// Token: 0x06000A90 RID: 2704 RVA: 0x000137FF File Offset: 0x000119FF
			public override bool TryAs(out T1 value)
			{
				value = default(T1);
				return false;
			}

			// Token: 0x06000A91 RID: 2705 RVA: 0x00013809 File Offset: 0x00011A09
			public override bool TryAs(out T2 value)
			{
				value = this._value;
				return true;
			}

			// Token: 0x06000A92 RID: 2706 RVA: 0x00013818 File Offset: 0x00011A18
			protected override bool EqualsCore(Union<T1, T2> other)
			{
				return Union<T1, T2>.T2Comparer.Equals(this._value, (T2)other);
			}

			// Token: 0x06000A93 RID: 2707 RVA: 0x00013830 File Offset: 0x00011A30
			protected override int GetHashCodeCore()
			{
				return Union<T1, T2>.T2Comparer.GetHashCode(this._value);
			}

			// Token: 0x06000A94 RID: 2708 RVA: 0x00013844 File Offset: 0x00011A44
			public override string ToString()
			{
				T2 value = this._value;
				return ((value != null) ? value.ToString() : null) ?? string.Empty;
			}

			// Token: 0x040007FC RID: 2044
			private readonly T2 _value;
		}
	}
}
