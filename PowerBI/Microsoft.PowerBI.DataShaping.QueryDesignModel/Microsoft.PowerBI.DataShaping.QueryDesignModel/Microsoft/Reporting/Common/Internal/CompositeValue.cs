using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x02000283 RID: 643
	public sealed class CompositeValue : IComparable<CompositeValue>, IComparable, ICustomComparable, IEquatable<CompositeValue>
	{
		// Token: 0x06001B87 RID: 7047 RVA: 0x0004D31A File Offset: 0x0004B51A
		private CompositeValue(ScalarValue[] values)
		{
			this._values = values;
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0004D329 File Offset: 0x0004B529
		public static CompositeValue Empty
		{
			get
			{
				return CompositeValue.EmptyInstance;
			}
		}

		// Token: 0x170007B6 RID: 1974
		internal ScalarValue this[int index]
		{
			get
			{
				return this._values[index];
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x0004D33E File Offset: 0x0004B53E
		public int Count
		{
			get
			{
				return this._values.Length;
			}
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0004D348 File Offset: 0x0004B548
		public static CompositeValue CreateFromSingleValue(object value)
		{
			return CompositeValue.CreateFromSingleValue(new ScalarValue(value));
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0004D355 File Offset: 0x0004B555
		public static CompositeValue CreateFromSingleValue(ScalarValue value)
		{
			return CompositeValue.Create(new ScalarValue[] { value });
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0004D36A File Offset: 0x0004B56A
		public static CompositeValue Create(params ScalarValue[] values)
		{
			if (values.Length == 0)
			{
				return CompositeValue.EmptyInstance;
			}
			return new CompositeValue(values);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0004D37C File Offset: 0x0004B57C
		public static CompositeValue Create(IEnumerable<ScalarValue> values)
		{
			return CompositeValue.Create(values.ToArray<ScalarValue>());
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0004D389 File Offset: 0x0004B589
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CompositeValue);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0004D398 File Offset: 0x0004B598
		public bool Equals(CompositeValue other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.Count != other.Count)
			{
				return false;
			}
			for (int i = 0; i < this._values.Length; i++)
			{
				if (this._values[i] != other._values[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0004D3EF File Offset: 0x0004B5EF
		public static bool operator ==(CompositeValue compositeValue1, CompositeValue compositeValue2)
		{
			return compositeValue1 == compositeValue2 || (compositeValue1 != null && compositeValue1.Equals(compositeValue2));
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0004D403 File Offset: 0x0004B603
		public static bool operator !=(CompositeValue compositeValue1, CompositeValue compositeValue2)
		{
			return !(compositeValue1 == compositeValue2);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0004D40F File Offset: 0x0004B60F
		public int GetHashCode(IEqualityComparer comparer)
		{
			return Hashing.CombineHashWithComparer<ScalarValue>(this._values, comparer);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0004D41D File Offset: 0x0004B61D
		public override int GetHashCode()
		{
			return Hashing.CombineHash<ScalarValue>(this._values, null);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0004D42B File Offset: 0x0004B62B
		public int CompareTo(CompositeValue other)
		{
			return this.CompareTo(other, null);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0004D438 File Offset: 0x0004B638
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			CompositeValue compositeValue = obj as CompositeValue;
			if (compositeValue == null)
			{
				throw new ArgumentException("Cannot compare CompositeValue objects with objects of a different type.");
			}
			return this.CompareTo(compositeValue, null);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0004D470 File Offset: 0x0004B670
		internal int CompareTo(CompositeValue other, IComparer<object> comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (this._values.Length == other._values.Length)
			{
				int num = 0;
				int num2 = 0;
				while (num == 0 && num2 < this._values.Length)
				{
					ScalarValue[] values = this._values;
					ScalarValue[] values2 = other._values;
					num = this._values[num2].CompareTo(other._values[num2], comparer);
					num2++;
				}
				return num;
			}
			if (other._values.Length == 0)
			{
				return 1;
			}
			if (this._values.Length == 0)
			{
				return -1;
			}
			throw new ArgumentException("Cannot compare CompositeValue objects with different numbers of values.");
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0004D510 File Offset: 0x0004B710
		int ICustomComparable.CompareTo(ICustomComparable obj, IComparer<object> comparer)
		{
			CompositeValue compositeValue = obj as CompositeValue;
			if (compositeValue == null)
			{
				throw new ArgumentException("Cannot compare CompositeValue objects with objects of a different type.");
			}
			return this.CompareTo(compositeValue, comparer);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0004D540 File Offset: 0x0004B740
		public CompositeValue Append(CompositeValue other)
		{
			ScalarValue[] array = new ScalarValue[this._values.Length + other._values.Length];
			this._values.CopyTo(array, 0);
			other._values.CopyTo(array, this._values.Length);
			return new CompositeValue(array);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0004D58B File Offset: 0x0004B78B
		internal static bool IsNullOrEmpty(CompositeValue value)
		{
			return value == null || value._values.Length == 0;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0004D59C File Offset: 0x0004B79C
		public override string ToString()
		{
			return "[" + string.Join(", ", this._values.Select((ScalarValue v) => v.ToString() ?? "null").ToArray<string>()) + "]";
		}

		// Token: 0x04000F0E RID: 3854
		private static readonly CompositeValue EmptyInstance = new CompositeValue(new ScalarValue[0]);

		// Token: 0x04000F0F RID: 3855
		private readonly ScalarValue[] _values;
	}
}
