using System;
using AngleSharp.Extensions;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000124 RID: 292
	public struct Resolution : IEquatable<Resolution>, IComparable<Resolution>, IFormattable
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x0003EB77 File Offset: 0x0003CD77
		public Resolution(float value, Resolution.Unit unit)
		{
			this._value = value;
			this._unit = unit;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0003EB87 File Offset: 0x0003CD87
		public float Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0003EB8F File Offset: 0x0003CD8F
		public Resolution.Unit Type
		{
			get
			{
				return this._unit;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0003EB98 File Offset: 0x0003CD98
		public string UnitString
		{
			get
			{
				switch (this._unit)
				{
				case Resolution.Unit.Dpi:
					return UnitNames.Dpi;
				case Resolution.Unit.Dpcm:
					return UnitNames.Dpcm;
				case Resolution.Unit.Dppx:
					return UnitNames.Dppx;
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0003EBDC File Offset: 0x0003CDDC
		public static bool TryParse(string s, out Resolution result)
		{
			float num = 0f;
			Resolution.Unit unit = Resolution.GetUnit(s.CssUnit(out num));
			if (unit != Resolution.Unit.None)
			{
				result = new Resolution(num, unit);
				return true;
			}
			result = default(Resolution);
			return false;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0003EC17 File Offset: 0x0003CE17
		public static Resolution.Unit GetUnit(string s)
		{
			if (s == "dpcm")
			{
				return Resolution.Unit.Dpcm;
			}
			if (s == "dpi")
			{
				return Resolution.Unit.Dpi;
			}
			if (!(s == "dppx"))
			{
				return Resolution.Unit.None;
			}
			return Resolution.Unit.Dppx;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0003EC49 File Offset: 0x0003CE49
		public float ToDotsPerPixel()
		{
			if (this._unit == Resolution.Unit.Dpi)
			{
				return this._value / 96f;
			}
			if (this._unit == Resolution.Unit.Dpcm)
			{
				return this._value * 127f / 4800f;
			}
			return this._value;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0003EC84 File Offset: 0x0003CE84
		public float To(Resolution.Unit unit)
		{
			float num = this.ToDotsPerPixel();
			if (unit == Resolution.Unit.Dpi)
			{
				return num * 96f;
			}
			if (unit == Resolution.Unit.Dpcm)
			{
				return num * 50f * 96f / 127f;
			}
			return num;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0003ECBD File Offset: 0x0003CEBD
		public bool Equals(Resolution other)
		{
			return this._value == other._value && this._unit == other._unit;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0003ECE0 File Offset: 0x0003CEE0
		public int CompareTo(Resolution other)
		{
			return this.ToDotsPerPixel().CompareTo(other.ToDotsPerPixel());
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0003ED04 File Offset: 0x0003CF04
		public override bool Equals(object obj)
		{
			Resolution? resolution = obj as Resolution?;
			return resolution != null && this.Equals(resolution.Value);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0003ED38 File Offset: 0x0003CF38
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0003ED54 File Offset: 0x0003CF54
		public override string ToString()
		{
			return this._value.ToString() + this.UnitString;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0003ED7C File Offset: 0x0003CF7C
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return this._value.ToString(format, formatProvider) + this.UnitString;
		}

		// Token: 0x040008C8 RID: 2248
		private readonly float _value;

		// Token: 0x040008C9 RID: 2249
		private readonly Resolution.Unit _unit;

		// Token: 0x020004B0 RID: 1200
		public enum Unit : byte
		{
			// Token: 0x04001122 RID: 4386
			None,
			// Token: 0x04001123 RID: 4387
			Dpi,
			// Token: 0x04001124 RID: 4388
			Dpcm,
			// Token: 0x04001125 RID: 4389
			Dppx
		}
	}
}
