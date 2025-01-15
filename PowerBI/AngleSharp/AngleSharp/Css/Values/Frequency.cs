using System;
using AngleSharp.Extensions;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000116 RID: 278
	public struct Frequency : IEquatable<Frequency>, IComparable<Frequency>, IFormattable
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x0003DD28 File Offset: 0x0003BF28
		public Frequency(float value, Frequency.Unit unit)
		{
			this._value = value;
			this._unit = unit;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0003DD38 File Offset: 0x0003BF38
		public float Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x0003DD40 File Offset: 0x0003BF40
		public Frequency.Unit Type
		{
			get
			{
				return this._unit;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0003DD48 File Offset: 0x0003BF48
		public string UnitString
		{
			get
			{
				Frequency.Unit unit = this._unit;
				if (unit == Frequency.Unit.Hz)
				{
					return UnitNames.Hz;
				}
				if (unit == Frequency.Unit.Khz)
				{
					return UnitNames.Khz;
				}
				return string.Empty;
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0003DD78 File Offset: 0x0003BF78
		public static bool operator >=(Frequency a, Frequency b)
		{
			int num = a.CompareTo(b);
			return num == 0 || num == 1;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0003DD97 File Offset: 0x0003BF97
		public static bool operator >(Frequency a, Frequency b)
		{
			return a.CompareTo(b) == 1;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0003DDA4 File Offset: 0x0003BFA4
		public static bool operator <=(Frequency a, Frequency b)
		{
			int num = a.CompareTo(b);
			return num == 0 || num == -1;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0003DDC3 File Offset: 0x0003BFC3
		public static bool operator <(Frequency a, Frequency b)
		{
			return a.CompareTo(b) == -1;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0003DDD0 File Offset: 0x0003BFD0
		public int CompareTo(Frequency other)
		{
			return this.ToHertz().CompareTo(other.ToHertz());
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0003DDF4 File Offset: 0x0003BFF4
		public static bool TryParse(string s, out Frequency result)
		{
			float num = 0f;
			Frequency.Unit unit = Frequency.GetUnit(s.CssUnit(out num));
			if (unit != Frequency.Unit.None)
			{
				result = new Frequency(num, unit);
				return true;
			}
			result = default(Frequency);
			return false;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0003DE2F File Offset: 0x0003C02F
		public static Frequency.Unit GetUnit(string s)
		{
			if (s == "hz")
			{
				return Frequency.Unit.Hz;
			}
			if (!(s == "khz"))
			{
				return Frequency.Unit.None;
			}
			return Frequency.Unit.Khz;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0003DE52 File Offset: 0x0003C052
		public float ToHertz()
		{
			if (this._unit != Frequency.Unit.Khz)
			{
				return this._value;
			}
			return this._value * 1000f;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0003DE70 File Offset: 0x0003C070
		public bool Equals(Frequency other)
		{
			return this._value == other._value && this._unit == other._unit;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0003DE90 File Offset: 0x0003C090
		public static bool operator ==(Frequency a, Frequency b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0003DE9A File Offset: 0x0003C09A
		public static bool operator !=(Frequency a, Frequency b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0003DEA8 File Offset: 0x0003C0A8
		public override bool Equals(object obj)
		{
			Frequency? frequency = obj as Frequency?;
			return frequency != null && this.Equals(frequency.Value);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0003DEDC File Offset: 0x0003C0DC
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0003DEF8 File Offset: 0x0003C0F8
		public override string ToString()
		{
			return this._value.ToString() + this.UnitString;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0003DF20 File Offset: 0x0003C120
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return this._value.ToString(format, formatProvider) + this.UnitString;
		}

		// Token: 0x0400089F RID: 2207
		private readonly float _value;

		// Token: 0x040008A0 RID: 2208
		private readonly Frequency.Unit _unit;

		// Token: 0x020004AC RID: 1196
		public enum Unit : byte
		{
			// Token: 0x04001104 RID: 4356
			None,
			// Token: 0x04001105 RID: 4357
			Hz,
			// Token: 0x04001106 RID: 4358
			Khz
		}
	}
}
