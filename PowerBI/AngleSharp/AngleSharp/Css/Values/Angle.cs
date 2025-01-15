using System;
using AngleSharp.Extensions;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000112 RID: 274
	public struct Angle : IEquatable<Angle>, IComparable<Angle>, IFormattable
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x0003D07D File Offset: 0x0003B27D
		public Angle(float value, Angle.Unit unit)
		{
			this._value = value;
			this._unit = unit;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0003D08D File Offset: 0x0003B28D
		public float Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0003D095 File Offset: 0x0003B295
		public Angle.Unit Type
		{
			get
			{
				return this._unit;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0003D0A0 File Offset: 0x0003B2A0
		public string UnitString
		{
			get
			{
				switch (this._unit)
				{
				case Angle.Unit.Deg:
					return UnitNames.Deg;
				case Angle.Unit.Rad:
					return UnitNames.Rad;
				case Angle.Unit.Grad:
					return UnitNames.Grad;
				case Angle.Unit.Turn:
					return UnitNames.Turn;
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0003D0EC File Offset: 0x0003B2EC
		public static bool operator >=(Angle a, Angle b)
		{
			int num = a.CompareTo(b);
			return num == 0 || num == 1;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0003D10B File Offset: 0x0003B30B
		public static bool operator >(Angle a, Angle b)
		{
			return a.CompareTo(b) == 1;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0003D118 File Offset: 0x0003B318
		public static bool operator <=(Angle a, Angle b)
		{
			int num = a.CompareTo(b);
			return num == 0 || num == -1;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0003D137 File Offset: 0x0003B337
		public static bool operator <(Angle a, Angle b)
		{
			return a.CompareTo(b) == -1;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0003D144 File Offset: 0x0003B344
		public int CompareTo(Angle other)
		{
			return this.ToRadian().CompareTo(other.ToRadian());
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0003D168 File Offset: 0x0003B368
		public static bool TryParse(string s, out Angle result)
		{
			float num = 0f;
			Angle.Unit unit = Angle.GetUnit(s.CssUnit(out num));
			if (unit != Angle.Unit.None)
			{
				result = new Angle(num, unit);
				return true;
			}
			result = default(Angle);
			return false;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0003D1A4 File Offset: 0x0003B3A4
		public static Angle.Unit GetUnit(string s)
		{
			if (s == "deg")
			{
				return Angle.Unit.Deg;
			}
			if (s == "grad")
			{
				return Angle.Unit.Grad;
			}
			if (s == "turn")
			{
				return Angle.Unit.Turn;
			}
			if (!(s == "rad"))
			{
				return Angle.Unit.None;
			}
			return Angle.Unit.Rad;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0003D1F0 File Offset: 0x0003B3F0
		public float ToRadian()
		{
			switch (this._unit)
			{
			case Angle.Unit.Deg:
				return (float)(0.017453292519943295 * (double)this._value);
			case Angle.Unit.Grad:
				return (float)(0.015707963267948967 * (double)this._value);
			case Angle.Unit.Turn:
				return (float)(6.283185307179586 * (double)this._value);
			}
			return this._value;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0003D260 File Offset: 0x0003B460
		public float ToTurns()
		{
			switch (this._unit)
			{
			case Angle.Unit.Deg:
				return (float)((double)this._value / 360.0);
			case Angle.Unit.Rad:
				return (float)((double)this._value / 6.283185307179586);
			case Angle.Unit.Grad:
				return (float)((double)this._value / 400.0);
			default:
				return this._value;
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0003D2C9 File Offset: 0x0003B4C9
		public bool Equals(Angle other)
		{
			return this.ToRadian() == other.ToRadian();
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0003D2DA File Offset: 0x0003B4DA
		public static bool operator ==(Angle a, Angle b)
		{
			return a.Equals(b);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0003D2E4 File Offset: 0x0003B4E4
		public static bool operator !=(Angle a, Angle b)
		{
			return !a.Equals(b);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0003D2F4 File Offset: 0x0003B4F4
		public override bool Equals(object obj)
		{
			Angle? angle = obj as Angle?;
			return angle != null && this.Equals(angle.Value);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0003D325 File Offset: 0x0003B525
		public override int GetHashCode()
		{
			return (int)this._value;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0003D330 File Offset: 0x0003B530
		public override string ToString()
		{
			return this._value.ToString() + this.UnitString;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0003D358 File Offset: 0x0003B558
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return this._value.ToString(format, formatProvider) + this.UnitString;
		}

		// Token: 0x04000884 RID: 2180
		public static readonly Angle Zero = new Angle(0f, Angle.Unit.Rad);

		// Token: 0x04000885 RID: 2181
		public static readonly Angle HalfQuarter = new Angle(45f, Angle.Unit.Deg);

		// Token: 0x04000886 RID: 2182
		public static readonly Angle Quarter = new Angle(90f, Angle.Unit.Deg);

		// Token: 0x04000887 RID: 2183
		public static readonly Angle TripleHalfQuarter = new Angle(135f, Angle.Unit.Deg);

		// Token: 0x04000888 RID: 2184
		public static readonly Angle Half = new Angle(180f, Angle.Unit.Deg);

		// Token: 0x04000889 RID: 2185
		private readonly float _value;

		// Token: 0x0400088A RID: 2186
		private readonly Angle.Unit _unit;

		// Token: 0x020004AB RID: 1195
		public enum Unit : byte
		{
			// Token: 0x040010FE RID: 4350
			None,
			// Token: 0x040010FF RID: 4351
			Deg,
			// Token: 0x04001100 RID: 4352
			Rad,
			// Token: 0x04001101 RID: 4353
			Grad,
			// Token: 0x04001102 RID: 4354
			Turn
		}
	}
}
