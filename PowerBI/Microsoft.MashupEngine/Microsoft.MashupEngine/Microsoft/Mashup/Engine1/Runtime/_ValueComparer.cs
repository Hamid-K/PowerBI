using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200169E RID: 5790
	public class _ValueComparer : IComparer<Value>, IEqualityComparer<Value>
	{
		// Token: 0x170026CB RID: 9931
		// (get) Token: 0x06009305 RID: 37637 RVA: 0x001E5D47 File Offset: 0x001E3F47
		public static _ValueComparer StrictDefault
		{
			get
			{
				return _ValueComparer.StrictDouble;
			}
		}

		// Token: 0x170026CC RID: 9932
		// (get) Token: 0x06009306 RID: 37638 RVA: 0x001E5D4E File Offset: 0x001E3F4E
		public static _ValueComparer StrictDefaultIgnoreCase
		{
			get
			{
				return _ValueComparer.StrictDoubleIgnoreCase;
			}
		}

		// Token: 0x170026CD RID: 9933
		// (get) Token: 0x06009307 RID: 37639 RVA: 0x001D763A File Offset: 0x001D583A
		public static _ValueComparer LaxDefault
		{
			get
			{
				return _ValueComparer.LaxDouble;
			}
		}

		// Token: 0x170026CE RID: 9934
		// (get) Token: 0x06009308 RID: 37640 RVA: 0x001E5D55 File Offset: 0x001E3F55
		public static _ValueComparer LaxDefaultIgnoreCase
		{
			get
			{
				return _ValueComparer.LaxDoubleIgnoreCase;
			}
		}

		// Token: 0x06009309 RID: 37641 RVA: 0x001E5D5C File Offset: 0x001E3F5C
		public _ValueComparer(StringComparer textComparer, CultureInfo culture, bool ignoreCase, NumberComparer numberComparer, bool strict)
		{
			this.textComparer = textComparer;
			this.culture = culture;
			this.ignoreCase = ignoreCase;
			this.numberComparer = numberComparer;
			this.strict = strict;
		}

		// Token: 0x170026CF RID: 9935
		// (get) Token: 0x0600930A RID: 37642 RVA: 0x001E5D89 File Offset: 0x001E3F89
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x170026D0 RID: 9936
		// (get) Token: 0x0600930B RID: 37643 RVA: 0x001E5D91 File Offset: 0x001E3F91
		public bool IgnoreCase
		{
			get
			{
				return this.ignoreCase;
			}
		}

		// Token: 0x0600930C RID: 37644 RVA: 0x001E5D99 File Offset: 0x001E3F99
		public bool Equals(Value x, Value y)
		{
			return x.Equals(y, this);
		}

		// Token: 0x0600930D RID: 37645 RVA: 0x001E5DA3 File Offset: 0x001E3FA3
		public int GetHashCode(Value value)
		{
			return value.GetHashCode(this);
		}

		// Token: 0x0600930E RID: 37646 RVA: 0x001E5DAC File Offset: 0x001E3FAC
		public int Compare(Value x, Value y)
		{
			return x.CompareTo(y, this);
		}

		// Token: 0x0600930F RID: 37647 RVA: 0x001E5DB6 File Offset: 0x001E3FB6
		public bool Equals(string x, string y)
		{
			return this.textComparer.Equals(x, y);
		}

		// Token: 0x06009310 RID: 37648 RVA: 0x001E5DC5 File Offset: 0x001E3FC5
		public int GetHashCode(string value)
		{
			return this.textComparer.GetHashCode(value);
		}

		// Token: 0x06009311 RID: 37649 RVA: 0x001E5DD3 File Offset: 0x001E3FD3
		public int Compare(string x, string y)
		{
			return this.textComparer.Compare(x, y);
		}

		// Token: 0x06009312 RID: 37650 RVA: 0x001E5DE2 File Offset: 0x001E3FE2
		public bool Equals(NumberValue x, NumberValue y)
		{
			return this.numberComparer.Equals(x, y);
		}

		// Token: 0x06009313 RID: 37651 RVA: 0x001E5DF1 File Offset: 0x001E3FF1
		public int GetHashCode(NumberValue value)
		{
			return this.numberComparer.GetHashCode(value);
		}

		// Token: 0x06009314 RID: 37652 RVA: 0x001E5DFF File Offset: 0x001E3FFF
		public int Compare(NumberValue x, NumberValue y)
		{
			return this.numberComparer.Compare(x, y);
		}

		// Token: 0x06009315 RID: 37653 RVA: 0x001E5E10 File Offset: 0x001E4010
		public int CompareKinds(Value x, Value y)
		{
			if (this.strict)
			{
				throw ValueException.BinaryOperatorTypeMismatch("<", x, y);
			}
			return x.Kind.CompareTo(y.Kind);
		}

		// Token: 0x04004E87 RID: 20103
		public static readonly _ValueComparer LaxDouble = new _ValueComparer(StringComparer.Ordinal, null, false, NumberComparer.Double, false);

		// Token: 0x04004E88 RID: 20104
		public static readonly _ValueComparer LaxDoubleIgnoreCase = new _ValueComparer(StringComparer.OrdinalIgnoreCase, null, true, NumberComparer.Double, false);

		// Token: 0x04004E89 RID: 20105
		public static readonly _ValueComparer StrictDouble = new _ValueComparer(StringComparer.Ordinal, null, false, NumberComparer.Double, true);

		// Token: 0x04004E8A RID: 20106
		public static readonly _ValueComparer StrictDoubleIgnoreCase = new _ValueComparer(StringComparer.OrdinalIgnoreCase, null, true, NumberComparer.Double, true);

		// Token: 0x04004E8B RID: 20107
		public static readonly _ValueComparer LaxDecimal = new _ValueComparer(StringComparer.Ordinal, null, false, NumberComparer.Decimal, false);

		// Token: 0x04004E8C RID: 20108
		private StringComparer textComparer;

		// Token: 0x04004E8D RID: 20109
		private CultureInfo culture;

		// Token: 0x04004E8E RID: 20110
		private bool ignoreCase;

		// Token: 0x04004E8F RID: 20111
		private NumberComparer numberComparer;

		// Token: 0x04004E90 RID: 20112
		private bool strict;
	}
}
