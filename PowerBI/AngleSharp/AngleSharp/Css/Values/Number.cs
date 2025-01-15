using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x0200011F RID: 287
	public struct Number : IEquatable<Number>, IComparable<Number>, IFormattable
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x0003E6EA File Offset: 0x0003C8EA
		public Number(float value, Number.Unit unit)
		{
			this._value = value;
			this._unit = unit;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0003E6FA File Offset: 0x0003C8FA
		public float Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0003E702 File Offset: 0x0003C902
		public bool IsInteger
		{
			get
			{
				return this._unit == Number.Unit.Integer;
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0003E70D File Offset: 0x0003C90D
		public static bool operator >=(Number a, Number b)
		{
			return a._value >= b._value;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0003E720 File Offset: 0x0003C920
		public static bool operator >(Number a, Number b)
		{
			return a._value > b._value;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0003E730 File Offset: 0x0003C930
		public static bool operator <=(Number a, Number b)
		{
			return a._value <= b._value;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0003E743 File Offset: 0x0003C943
		public static bool operator <(Number a, Number b)
		{
			return a._value < b._value;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0003E754 File Offset: 0x0003C954
		public int CompareTo(Number other)
		{
			return this._value.CompareTo(other._value);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0003E775 File Offset: 0x0003C975
		public bool Equals(Number other)
		{
			return this._value == other._value && this._unit == other._unit;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0003E795 File Offset: 0x0003C995
		public static bool operator ==(Number a, Number b)
		{
			return a._value == b._value;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0003E7A5 File Offset: 0x0003C9A5
		public static bool operator !=(Number a, Number b)
		{
			return a._value != b._value;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0003E7B8 File Offset: 0x0003C9B8
		public override bool Equals(object obj)
		{
			Number? number = obj as Number?;
			return number != null && this.Equals(number.Value);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0003E7EC File Offset: 0x0003C9EC
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0003E808 File Offset: 0x0003CA08
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0003E824 File Offset: 0x0003CA24
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return this._value.ToString(format, formatProvider);
		}

		// Token: 0x040008B0 RID: 2224
		public static readonly Number Zero = new Number(0f, Number.Unit.Integer);

		// Token: 0x040008B1 RID: 2225
		public static readonly Number Infinite = new Number(float.PositiveInfinity, Number.Unit.Float);

		// Token: 0x040008B2 RID: 2226
		public static readonly Number One = new Number(1f, Number.Unit.Integer);

		// Token: 0x040008B3 RID: 2227
		private readonly float _value;

		// Token: 0x040008B4 RID: 2228
		private readonly Number.Unit _unit;

		// Token: 0x020004AE RID: 1198
		public enum Unit : byte
		{
			// Token: 0x04001119 RID: 4377
			Integer,
			// Token: 0x0400111A RID: 4378
			Float
		}
	}
}
