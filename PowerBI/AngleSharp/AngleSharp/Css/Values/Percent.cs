using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000120 RID: 288
	public struct Percent : IEquatable<Percent>, IComparable<Percent>, IFormattable
	{
		// Token: 0x0600093C RID: 2364 RVA: 0x0003E873 File Offset: 0x0003CA73
		public Percent(float value)
		{
			this._value = value;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0003E87C File Offset: 0x0003CA7C
		public float NormalizedValue
		{
			get
			{
				return this._value * 0.01f;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0003E88A File Offset: 0x0003CA8A
		public float Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0003E892 File Offset: 0x0003CA92
		public static bool operator >=(Percent a, Percent b)
		{
			return a._value >= b._value;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0003E8A5 File Offset: 0x0003CAA5
		public static bool operator >(Percent a, Percent b)
		{
			return a._value > b._value;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0003E8B5 File Offset: 0x0003CAB5
		public static bool operator <=(Percent a, Percent b)
		{
			return a._value <= b._value;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0003E8C8 File Offset: 0x0003CAC8
		public static bool operator <(Percent a, Percent b)
		{
			return a._value < b._value;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0003E8D8 File Offset: 0x0003CAD8
		public int CompareTo(Percent other)
		{
			return this._value.CompareTo(other._value);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0003E8F9 File Offset: 0x0003CAF9
		public bool Equals(Percent other)
		{
			return this._value == other._value;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0003E909 File Offset: 0x0003CB09
		public static bool operator ==(Percent a, Percent b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0003E913 File Offset: 0x0003CB13
		public static bool operator !=(Percent a, Percent b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0003E920 File Offset: 0x0003CB20
		public override bool Equals(object obj)
		{
			Percent? percent = obj as Percent?;
			return percent != null && this.Equals(percent.Value);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0003E954 File Offset: 0x0003CB54
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0003E970 File Offset: 0x0003CB70
		public override string ToString()
		{
			return this._value.ToString() + "%";
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0003E998 File Offset: 0x0003CB98
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return this._value.ToString(format, formatProvider) + "%";
		}

		// Token: 0x040008B5 RID: 2229
		public static readonly Percent Zero = new Percent(0f);

		// Token: 0x040008B6 RID: 2230
		public static readonly Percent Fifty = new Percent(50f);

		// Token: 0x040008B7 RID: 2231
		public static readonly Percent Hundred = new Percent(100f);

		// Token: 0x040008B8 RID: 2232
		private readonly float _value;
	}
}
