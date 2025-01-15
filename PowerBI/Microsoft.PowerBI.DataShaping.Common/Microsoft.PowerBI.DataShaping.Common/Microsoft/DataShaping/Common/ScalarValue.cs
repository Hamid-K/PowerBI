using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x02000018 RID: 24
	public struct ScalarValue : IComparable<ScalarValue>, IComparable, IEquatable<ScalarValue>, ICustomComparable
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00003B4E File Offset: 0x00001D4E
		public ScalarValue(object value)
		{
			this = new ScalarValue((IComparable)value);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003B5C File Offset: 0x00001D5C
		public ScalarValue(IComparable value)
		{
			this.Value = value;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003B68 File Offset: 0x00001D68
		public static ScalarValue Null
		{
			get
			{
				return default(ScalarValue);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003B7E File Offset: 0x00001D7E
		public readonly IComparable Value { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003B86 File Offset: 0x00001D86
		public Type Type
		{
			get
			{
				IComparable value = this.Value;
				if (value == null)
				{
					return null;
				}
				return value.GetType();
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003B99 File Offset: 0x00001D99
		public T CastValue<T>()
		{
			return (T)((object)this.Value);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003BA6 File Offset: 0x00001DA6
		public bool IsOfType<T>()
		{
			return this.Value is IComparable<T>;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public bool IsNaN()
		{
			return (this.Value is double && double.IsNaN((double)this.Value)) || (this.Value is float && float.IsNaN((float)this.Value));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003C05 File Offset: 0x00001E05
		public static bool operator ==(ScalarValue left, ScalarValue right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003C0F File Offset: 0x00001E0F
		public static bool operator !=(ScalarValue left, ScalarValue right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003C1C File Offset: 0x00001E1C
		public static bool operator <=(ScalarValue left, ScalarValue right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003C2C File Offset: 0x00001E2C
		public static bool operator >=(ScalarValue left, ScalarValue right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003C3C File Offset: 0x00001E3C
		public bool Equals(ScalarValue other)
		{
			return object.Equals(this.Value, other.Value);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003C55 File Offset: 0x00001E55
		public override bool Equals(object obj)
		{
			return obj is ScalarValue && this.Equals((ScalarValue)obj);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003C6D File Offset: 0x00001E6D
		public override int GetHashCode()
		{
			return this.GetHashCode(null);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003C76 File Offset: 0x00001E76
		public int GetHashCode(IEqualityComparer comparer)
		{
			if (comparer != null)
			{
				return comparer.GetHashCode(this.Value);
			}
			if (this.Value != null)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003C9D File Offset: 0x00001E9D
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is ScalarValue))
			{
				throw new ArgumentException("Cannot compare ScalarValue objects with objects of a different type.");
			}
			return this.CompareTo((ScalarValue)obj, null);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public int CompareTo(ScalarValue other)
		{
			return this.CompareTo(other, null);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003CD0 File Offset: 0x00001ED0
		internal int CompareTo(ScalarValue other, IComparer<object> comparer)
		{
			if (comparer != null)
			{
				return comparer.Compare(this.Value, other.Value);
			}
			if (this.Value == null)
			{
				if (other.Value == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (other.Value == null)
				{
					return 1;
				}
				return this.Value.CompareTo(other.Value);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003D26 File Offset: 0x00001F26
		int ICustomComparable.CompareTo(ICustomComparable obj, IComparer<object> comparer)
		{
			if (!(obj is ScalarValue))
			{
				throw new ArgumentException("Cannot compare ScalarValue objects with objects of a different type.");
			}
			return this.CompareTo((ScalarValue)obj, comparer);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003D48 File Offset: 0x00001F48
		public override string ToString()
		{
			if (this.Value == null)
			{
				return null;
			}
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			if (this.Value is DateTime && currentCulture.Calendar is UmAlQuraCalendar)
			{
				return Convert.ToString(this.Value, CultureInfo.InvariantCulture);
			}
			return this.Value.ToString();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003D9B File Offset: 0x00001F9B
		public static implicit operator ScalarValue(byte value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public static implicit operator ScalarValue(bool value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003DB5 File Offset: 0x00001FB5
		public static implicit operator ScalarValue(DateTime value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003DC2 File Offset: 0x00001FC2
		public static implicit operator ScalarValue(DateTimeOffset value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003DCF File Offset: 0x00001FCF
		public static implicit operator ScalarValue(decimal value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003DDC File Offset: 0x00001FDC
		public static implicit operator ScalarValue(double value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003DE9 File Offset: 0x00001FE9
		public static implicit operator ScalarValue(float value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public static implicit operator ScalarValue(Guid value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003E03 File Offset: 0x00002003
		public static implicit operator ScalarValue(int value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003E10 File Offset: 0x00002010
		public static implicit operator ScalarValue(long value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003E1D File Offset: 0x0000201D
		public static implicit operator ScalarValue(sbyte value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003E2A File Offset: 0x0000202A
		public static implicit operator ScalarValue(short value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003E37 File Offset: 0x00002037
		public static implicit operator ScalarValue(string value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003E3F File Offset: 0x0000203F
		public static implicit operator ScalarValue(TimeSpan value)
		{
			return new ScalarValue(value);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003E4C File Offset: 0x0000204C
		[Conditional("DEBUG")]
		private static void AssertScalarDataTypeOrNull(object value)
		{
		}

		// Token: 0x04000045 RID: 69
		internal const string ComparisonErrorCannotCompareOtherType = "Cannot compare ScalarValue objects with objects of a different type.";
	}
}
