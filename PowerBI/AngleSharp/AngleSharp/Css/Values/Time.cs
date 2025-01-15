using System;
using AngleSharp.Extensions;

namespace AngleSharp.Css.Values
{
	// Token: 0x0200012B RID: 299
	public struct Time : IEquatable<Time>, IComparable<Time>, IFormattable
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x0003F129 File Offset: 0x0003D329
		public Time(float value, Time.Unit unit)
		{
			this._value = value;
			this._unit = unit;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0003F139 File Offset: 0x0003D339
		public float Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0003F141 File Offset: 0x0003D341
		public Time.Unit Type
		{
			get
			{
				return this._unit;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0003F14C File Offset: 0x0003D34C
		public string UnitString
		{
			get
			{
				Time.Unit unit = this._unit;
				if (unit == Time.Unit.Ms)
				{
					return UnitNames.Ms;
				}
				if (unit != Time.Unit.S)
				{
					return string.Empty;
				}
				return UnitNames.S;
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0003F17C File Offset: 0x0003D37C
		public static bool operator >=(Time a, Time b)
		{
			int num = a.CompareTo(b);
			return num == 0 || num == 1;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0003F19B File Offset: 0x0003D39B
		public static bool operator >(Time a, Time b)
		{
			return a.CompareTo(b) == 1;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0003F1A8 File Offset: 0x0003D3A8
		public static bool operator <=(Time a, Time b)
		{
			int num = a.CompareTo(b);
			return num == 0 || num == -1;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0003F1C7 File Offset: 0x0003D3C7
		public static bool operator <(Time a, Time b)
		{
			return a.CompareTo(b) == -1;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0003F1D4 File Offset: 0x0003D3D4
		public int CompareTo(Time other)
		{
			return this.ToMilliseconds().CompareTo(other.ToMilliseconds());
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0003F1F8 File Offset: 0x0003D3F8
		public static bool TryParse(string s, out Time result)
		{
			float num = 0f;
			Time.Unit unit = Time.GetUnit(s.CssUnit(out num));
			if (unit != Time.Unit.None)
			{
				result = new Time(num, unit);
				return true;
			}
			result = default(Time);
			return false;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0003F233 File Offset: 0x0003D433
		public static Time.Unit GetUnit(string s)
		{
			if (s == "s")
			{
				return Time.Unit.S;
			}
			if (!(s == "ms"))
			{
				return Time.Unit.None;
			}
			return Time.Unit.Ms;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0003F256 File Offset: 0x0003D456
		public float ToMilliseconds()
		{
			if (this._unit != Time.Unit.S)
			{
				return this._value;
			}
			return this._value * 1000f;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0003F274 File Offset: 0x0003D474
		public bool Equals(Time other)
		{
			return this.ToMilliseconds() == other.ToMilliseconds();
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0003F285 File Offset: 0x0003D485
		public static bool operator ==(Time a, Time b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0003F28F File Offset: 0x0003D48F
		public static bool operator !=(Time a, Time b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0003F29C File Offset: 0x0003D49C
		public override bool Equals(object obj)
		{
			Time? time = obj as Time?;
			return time != null && this.Equals(time.Value);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0003F2D0 File Offset: 0x0003D4D0
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0003F2EC File Offset: 0x0003D4EC
		public override string ToString()
		{
			return this._value.ToString() + this.UnitString;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0003F314 File Offset: 0x0003D514
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return this._value.ToString(format, formatProvider) + this.UnitString;
		}

		// Token: 0x040008DF RID: 2271
		public static readonly Time Zero = new Time(0f, Time.Unit.Ms);

		// Token: 0x040008E0 RID: 2272
		private readonly float _value;

		// Token: 0x040008E1 RID: 2273
		private readonly Time.Unit _unit;

		// Token: 0x020004B1 RID: 1201
		public enum Unit : byte
		{
			// Token: 0x04001127 RID: 4391
			None,
			// Token: 0x04001128 RID: 4392
			Ms,
			// Token: 0x04001129 RID: 4393
			S
		}
	}
}
