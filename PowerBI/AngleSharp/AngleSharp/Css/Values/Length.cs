using System;
using AngleSharp.Extensions;

namespace AngleSharp.Css.Values
{
	// Token: 0x0200011C RID: 284
	public struct Length : IEquatable<Length>, IComparable<Length>, IFormattable
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x0003DF68 File Offset: 0x0003C168
		public Length(float value, Length.Unit unit)
		{
			this._value = value;
			this._unit = unit;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0003DF78 File Offset: 0x0003C178
		public bool IsAbsolute
		{
			get
			{
				return this._unit == Length.Unit.In || this._unit == Length.Unit.Mm || this._unit == Length.Unit.Pc || this._unit == Length.Unit.Px || this._unit == Length.Unit.Pt || this._unit == Length.Unit.Cm;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0003DFB2 File Offset: 0x0003C1B2
		public bool IsRelative
		{
			get
			{
				return !this.IsAbsolute;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0003DFBD File Offset: 0x0003C1BD
		public Length.Unit Type
		{
			get
			{
				return this._unit;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0003DFC5 File Offset: 0x0003C1C5
		public float Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0003DFD0 File Offset: 0x0003C1D0
		public string UnitString
		{
			get
			{
				switch (this._unit)
				{
				case Length.Unit.Px:
					return UnitNames.Px;
				case Length.Unit.Em:
					return UnitNames.Em;
				case Length.Unit.Ex:
					return UnitNames.Ex;
				case Length.Unit.Cm:
					return UnitNames.Cm;
				case Length.Unit.Mm:
					return UnitNames.Mm;
				case Length.Unit.In:
					return UnitNames.In;
				case Length.Unit.Pt:
					return UnitNames.Pt;
				case Length.Unit.Pc:
					return UnitNames.Pc;
				case Length.Unit.Ch:
					return UnitNames.Ch;
				case Length.Unit.Rem:
					return UnitNames.Rem;
				case Length.Unit.Vw:
					return UnitNames.Vw;
				case Length.Unit.Vh:
					return UnitNames.Vh;
				case Length.Unit.Vmin:
					return UnitNames.Vmin;
				case Length.Unit.Vmax:
					return UnitNames.Vmax;
				case Length.Unit.Percent:
					return UnitNames.Percent;
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0003E08C File Offset: 0x0003C28C
		public static bool operator >=(Length a, Length b)
		{
			int num = a.CompareTo(b);
			return num == 0 || num == 1;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0003E0AB File Offset: 0x0003C2AB
		public static bool operator >(Length a, Length b)
		{
			return a.CompareTo(b) == 1;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0003E0B8 File Offset: 0x0003C2B8
		public static bool operator <=(Length a, Length b)
		{
			int num = a.CompareTo(b);
			return num == 0 || num == -1;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0003E0D7 File Offset: 0x0003C2D7
		public static bool operator <(Length a, Length b)
		{
			return a.CompareTo(b) == -1;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0003E0E4 File Offset: 0x0003C2E4
		public int CompareTo(Length other)
		{
			if (this._unit == other._unit)
			{
				return this._value.CompareTo(other._value);
			}
			if (this.IsAbsolute && other.IsAbsolute)
			{
				return this.ToPixel().CompareTo(other.ToPixel());
			}
			return 0;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0003E13C File Offset: 0x0003C33C
		public static bool TryParse(string s, out Length result)
		{
			float num = 0f;
			Length.Unit unit = Length.GetUnit(s.CssUnit(out num));
			if (unit != Length.Unit.None)
			{
				result = new Length(num, unit);
				return true;
			}
			if (num == 0f)
			{
				result = Length.Zero;
				return true;
			}
			result = default(Length);
			return false;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0003E18C File Offset: 0x0003C38C
		public static Length.Unit GetUnit(string s)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(s);
			if (num <= 1313756516U)
			{
				if (num <= 1075471351U)
				{
					if (num != 537692064U)
					{
						if (num != 1028242803U)
						{
							if (num == 1075471351U)
							{
								if (s == "em")
								{
									return Length.Unit.Em;
								}
							}
						}
						else if (s == "vh")
						{
							return Length.Unit.Vh;
						}
					}
					else if (s == "%")
					{
						return Length.Unit.Percent;
					}
				}
				else if (num <= 1240764167U)
				{
					if (num != 1094220446U)
					{
						if (num == 1240764167U)
						{
							if (s == "rem")
							{
								return Length.Unit.Rem;
							}
						}
					}
					else if (s == "in")
					{
						return Length.Unit.In;
					}
				}
				else if (num != 1260025160U)
				{
					if (num == 1313756516U)
					{
						if (s == "pc")
						{
							return Length.Unit.Pc;
						}
					}
				}
				else if (s == "ex")
				{
					return Length.Unit.Ex;
				}
			}
			else if (num <= 1596563278U)
			{
				if (num <= 1514793754U)
				{
					if (num != 1498310325U)
					{
						if (num == 1514793754U)
						{
							if (s == "vw")
							{
								return Length.Unit.Vw;
							}
						}
					}
					else if (s == "px")
					{
						return Length.Unit.Px;
					}
				}
				else if (num != 1565420801U)
				{
					if (num == 1596563278U)
					{
						if (s == "ch")
						{
							return Length.Unit.Ch;
						}
					}
				}
				else if (s == "pt")
				{
					return Length.Unit.Pt;
				}
			}
			else if (num <= 1680451373U)
			{
				if (num != 1613635087U)
				{
					if (num == 1680451373U)
					{
						if (s == "cm")
						{
							return Length.Unit.Cm;
						}
					}
				}
				else if (s == "mm")
				{
					return Length.Unit.Mm;
				}
			}
			else if (num != 2406713693U)
			{
				if (num == 2573209955U)
				{
					if (s == "vmin")
					{
						return Length.Unit.Vmin;
					}
				}
			}
			else if (s == "vmax")
			{
				return Length.Unit.Vmax;
			}
			return Length.Unit.None;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0003E3C8 File Offset: 0x0003C5C8
		public float ToPixel()
		{
			switch (this._unit)
			{
			case Length.Unit.Px:
				return this._value;
			case Length.Unit.Cm:
				return this._value * 50f * 96f / 127f;
			case Length.Unit.Mm:
				return this._value * 5f * 96f / 127f;
			case Length.Unit.In:
				return this._value * 96f;
			case Length.Unit.Pt:
				return this._value * 96f / 72f;
			case Length.Unit.Pc:
				return this._value * 12f * 96f / 72f;
			}
			throw new InvalidOperationException("A relative unit cannot be converted.");
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0003E484 File Offset: 0x0003C684
		public float To(Length.Unit unit)
		{
			float num = this.ToPixel();
			switch (unit)
			{
			case Length.Unit.Px:
				return num;
			case Length.Unit.Cm:
				return num * 127f / 4800f;
			case Length.Unit.Mm:
				return num * 127f / 480f;
			case Length.Unit.In:
				return num / 96f;
			case Length.Unit.Pt:
				return num * 72f / 96f;
			case Length.Unit.Pc:
				return num * 72f / 1152f;
			}
			throw new InvalidOperationException("An absolute unit cannot be converted to a relative one.");
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0003E50E File Offset: 0x0003C70E
		public bool Equals(Length other)
		{
			return this._value == other._value && this._unit == other._unit;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0003E52E File Offset: 0x0003C72E
		public static bool operator ==(Length a, Length b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0003E538 File Offset: 0x0003C738
		public static bool operator !=(Length a, Length b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0003E548 File Offset: 0x0003C748
		public override bool Equals(object obj)
		{
			Length? length = obj as Length?;
			return length != null && this.Equals(length.Value);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0003E57C File Offset: 0x0003C77C
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0003E598 File Offset: 0x0003C798
		public override string ToString()
		{
			string text = ((this._value == 0f) ? string.Empty : this.UnitString);
			return this._value.ToString() + text;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0003E5D4 File Offset: 0x0003C7D4
		public string ToString(string format, IFormatProvider formatProvider)
		{
			string text = ((this._value == 0f) ? string.Empty : this.UnitString);
			return this._value.ToString(format, formatProvider) + text;
		}

		// Token: 0x040008A3 RID: 2211
		public static readonly Length Zero = new Length(0f, Length.Unit.Px);

		// Token: 0x040008A4 RID: 2212
		public static readonly Length Half = new Length(50f, Length.Unit.Percent);

		// Token: 0x040008A5 RID: 2213
		public static readonly Length Full = new Length(100f, Length.Unit.Percent);

		// Token: 0x040008A6 RID: 2214
		public static readonly Length Thin = new Length(1f, Length.Unit.Px);

		// Token: 0x040008A7 RID: 2215
		public static readonly Length Medium = new Length(3f, Length.Unit.Px);

		// Token: 0x040008A8 RID: 2216
		public static readonly Length Thick = new Length(5f, Length.Unit.Px);

		// Token: 0x040008A9 RID: 2217
		public static readonly Length Missing = new Length(-1f, Length.Unit.Ch);

		// Token: 0x040008AA RID: 2218
		private readonly float _value;

		// Token: 0x040008AB RID: 2219
		private readonly Length.Unit _unit;

		// Token: 0x020004AD RID: 1197
		public enum Unit : byte
		{
			// Token: 0x04001108 RID: 4360
			None,
			// Token: 0x04001109 RID: 4361
			Px,
			// Token: 0x0400110A RID: 4362
			Em,
			// Token: 0x0400110B RID: 4363
			Ex,
			// Token: 0x0400110C RID: 4364
			Cm,
			// Token: 0x0400110D RID: 4365
			Mm,
			// Token: 0x0400110E RID: 4366
			In,
			// Token: 0x0400110F RID: 4367
			Pt,
			// Token: 0x04001110 RID: 4368
			Pc,
			// Token: 0x04001111 RID: 4369
			Ch,
			// Token: 0x04001112 RID: 4370
			Rem,
			// Token: 0x04001113 RID: 4371
			Vw,
			// Token: 0x04001114 RID: 4372
			Vh,
			// Token: 0x04001115 RID: 4373
			Vmin,
			// Token: 0x04001116 RID: 4374
			Vmax,
			// Token: 0x04001117 RID: 4375
			Percent
		}
	}
}
