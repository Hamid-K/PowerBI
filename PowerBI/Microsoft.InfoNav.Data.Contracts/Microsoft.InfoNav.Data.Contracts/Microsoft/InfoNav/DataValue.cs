using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000062 RID: 98
	[ImmutableObject(true)]
	public abstract class DataValue : IEquatable<DataValue>
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001D5 RID: 469
		internal abstract DataType Type { get; }

		// Token: 0x060001D6 RID: 470
		public abstract bool Equals(DataValue other);

		// Token: 0x060001D7 RID: 471
		protected abstract int GetHashCodeCore();

		// Token: 0x060001D8 RID: 472
		internal abstract object GetValueAsObject();

		// Token: 0x060001D9 RID: 473 RVA: 0x000053AE File Offset: 0x000035AE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataValue);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000053BC File Offset: 0x000035BC
		public sealed override int GetHashCode()
		{
			return this.GetHashCodeCore();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000053C4 File Offset: 0x000035C4
		public static bool TryCreateFromObject(object value, out DataValue dataValue)
		{
			if (value == null)
			{
				dataValue = null;
				return true;
			}
			if (value is long)
			{
				long num = (long)value;
				dataValue = new IntegerValue(num);
				return true;
			}
			if (value is int)
			{
				int num2 = (int)value;
				dataValue = new IntegerValue((long)num2);
				return true;
			}
			if (value is decimal)
			{
				decimal num3 = (decimal)value;
				dataValue = new NumberValue<decimal>(num3);
				return true;
			}
			if (value is double)
			{
				double num4 = (double)value;
				try
				{
					dataValue = new NumberValue<decimal>((decimal)num4);
					return true;
				}
				catch (OverflowException)
				{
					dataValue = null;
					return false;
				}
			}
			if (value is bool)
			{
				dataValue = (((bool)value) ? BooleanValue.True : BooleanValue.False);
				return true;
			}
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				dataValue = new DateItemValue(dateTime);
				return true;
			}
			string text = value as string;
			if (text != null)
			{
				dataValue = new StringValue(text);
				return true;
			}
			dataValue = null;
			return false;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000054C4 File Offset: 0x000036C4
		public static DataValue FromObject(object value)
		{
			DataValue dataValue;
			if (!DataValue.TryCreateFromObject(value, out dataValue))
			{
				throw new ArgumentException();
			}
			return dataValue;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000054E2 File Offset: 0x000036E2
		public static implicit operator DataValue(long value)
		{
			return new IntegerValue(value);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000054EA File Offset: 0x000036EA
		public static implicit operator DataValue(decimal value)
		{
			return new NumberValue<decimal>(value);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000054F2 File Offset: 0x000036F2
		public static implicit operator DataValue(DateItem value)
		{
			return new DateItemValue(value);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000054FA File Offset: 0x000036FA
		public static implicit operator DataValue(bool value)
		{
			if (!value)
			{
				return BooleanValue.False;
			}
			return BooleanValue.True;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000550A File Offset: 0x0000370A
		public static implicit operator DataValue(DateTime value)
		{
			return new DateItemValue(value);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005518 File Offset: 0x00003718
		public static implicit operator DataValue(TimeSpan value)
		{
			int? num = new int?(value.Hours);
			int? num2 = new int?(value.Minutes);
			int? num3 = new int?(value.Seconds);
			int? num4 = new int?(value.Milliseconds);
			return new DateItemValue(null, null, null, num, num2, num3, num4);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005584 File Offset: 0x00003784
		public static implicit operator DataValue(string value)
		{
			return new StringValue(value);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000558C File Offset: 0x0000378C
		public static DataValue operator -(DataValue value)
		{
			if (value == null)
			{
				return null;
			}
			IntegerValue integerValue = value as IntegerValue;
			if (integerValue != null)
			{
				return new IntegerValue(-integerValue.Value);
			}
			NumberValue<decimal> numberValue = value as NumberValue<decimal>;
			if (numberValue != null)
			{
				return new NumberValue<decimal>(-numberValue.Value);
			}
			throw Contract.ExceptNotSupported(StringUtil.FormatInvariant("Cannot apply negation operation to a value of type {0}", value.GetType()));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000055E5 File Offset: 0x000037E5
		public static DataValue operator +(DataValue left, DataValue right)
		{
			if (left == null)
			{
				return right;
			}
			if (right == null)
			{
				return left;
			}
			return DataValue.Compute(left, right, DataValue.Operator.Add);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000055F9 File Offset: 0x000037F9
		public static DataValue operator -(DataValue left, DataValue right)
		{
			if (left == null)
			{
				return null;
			}
			if (right == null)
			{
				return left;
			}
			return DataValue.Compute(left, right, DataValue.Operator.Subtract);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000560D File Offset: 0x0000380D
		public static DataValue operator *(DataValue left, DataValue right)
		{
			if (left == null || right == null)
			{
				return null;
			}
			return DataValue.Compute(left, right, DataValue.Operator.Multiplication);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000561F File Offset: 0x0000381F
		public static DataValue operator /(DataValue left, DataValue right)
		{
			if (left == null || right == null)
			{
				return null;
			}
			return DataValue.Compute(left, right, DataValue.Operator.Division);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005634 File Offset: 0x00003834
		private static DataValue Compute(DataValue value, DataValue otherValue, DataValue.Operator op)
		{
			IntegerValue integerValue = value as IntegerValue;
			if (integerValue != null)
			{
				IntegerValue integerValue2 = otherValue as IntegerValue;
				if (integerValue2 != null)
				{
					switch (op)
					{
					case DataValue.Operator.Add:
						return integerValue.Value + integerValue2.Value;
					case DataValue.Operator.Subtract:
						return integerValue.Value - integerValue2.Value;
					case DataValue.Operator.Multiplication:
						return integerValue.Value * integerValue2.Value;
					case DataValue.Operator.Division:
						return integerValue.Value / integerValue2.Value;
					default:
						throw Contract.ExceptNotSupported("Unsupported operator " + op.ToString());
					}
				}
				else
				{
					NumberValue<decimal> numberValue = otherValue as NumberValue<decimal>;
					if (numberValue == null)
					{
						throw Contract.ExceptNotSupported(StringUtil.FormatInvariant("Cannot perform math operations between a value of type {0} and an integer", otherValue.GetType()));
					}
					switch (op)
					{
					case DataValue.Operator.Add:
						return integerValue.Value + numberValue.Value;
					case DataValue.Operator.Subtract:
						return integerValue.Value - numberValue.Value;
					case DataValue.Operator.Multiplication:
						return integerValue.Value * numberValue.Value;
					case DataValue.Operator.Division:
						return integerValue.Value / numberValue.Value;
					default:
						throw Contract.ExceptNotSupported("Unsupported operator " + op.ToString());
					}
				}
			}
			else
			{
				NumberValue<decimal> numberValue2 = value as NumberValue<decimal>;
				if (numberValue2 != null)
				{
					IntegerValue integerValue3 = otherValue as IntegerValue;
					if (integerValue3 != null)
					{
						switch (op)
						{
						case DataValue.Operator.Add:
							return numberValue2.Value + integerValue3.Value;
						case DataValue.Operator.Subtract:
							return numberValue2.Value - integerValue3.Value;
						case DataValue.Operator.Multiplication:
							return numberValue2.Value * integerValue3.Value;
						case DataValue.Operator.Division:
							return numberValue2.Value / integerValue3.Value;
						default:
							throw Contract.ExceptNotSupported("Unsupported operator " + op.ToString());
						}
					}
					else
					{
						NumberValue<decimal> numberValue3 = otherValue as NumberValue<decimal>;
						if (numberValue3 == null)
						{
							throw Contract.ExceptNotSupported(StringUtil.FormatInvariant("Cannot perform math operations between a value of type {0} and a decimal", otherValue.GetType()));
						}
						switch (op)
						{
						case DataValue.Operator.Add:
							return numberValue2.Value + numberValue3.Value;
						case DataValue.Operator.Subtract:
							return numberValue2.Value - numberValue3.Value;
						case DataValue.Operator.Multiplication:
							return integerValue.Value * numberValue3.Value;
						case DataValue.Operator.Division:
							return numberValue2.Value / numberValue3.Value;
						default:
							throw Contract.ExceptNotSupported("Unsupported operator " + op.ToString());
						}
					}
				}
				else
				{
					DateItemValue dateItemValue = value as DateItemValue;
					if (dateItemValue == null)
					{
						throw Contract.ExceptNotSupported(StringUtil.FormatInvariant("Cannot perform math operations between a value of type {0} and value of type {1}", value.GetType(), otherValue.GetType()));
					}
					DateItemValue dateItemValue2 = otherValue as DateItemValue;
					if (dateItemValue2 != null)
					{
						int? num = dateItemValue.Value.Year;
						int? num2 = ((num != null) ? num : dateItemValue2.Value.Year);
						num = dateItemValue.Value.Month;
						int? num3 = ((num != null) ? num : dateItemValue2.Value.Month);
						num = dateItemValue.Value.Day;
						int? num4 = ((num != null) ? num : dateItemValue2.Value.Day);
						num = dateItemValue.Value.Hour;
						int? num5 = ((num != null) ? num : dateItemValue2.Value.Hour);
						num = dateItemValue.Value.Minute;
						int? num6 = ((num != null) ? num : dateItemValue2.Value.Minute);
						num = dateItemValue.Value.Second;
						int? num7 = ((num != null) ? num : dateItemValue2.Value.Second);
						num = dateItemValue.Value.Millisecond;
						return new DateItemValue(num2, num3, num4, num5, num6, num7, (num != null) ? num : dateItemValue2.Value.Millisecond);
					}
					throw Contract.ExceptNotSupported(StringUtil.FormatInvariant("Cannot perform math operations between a value of type {0} and value of type {1}", value.GetType(), otherValue.GetType()));
				}
			}
		}

		// Token: 0x020002F5 RID: 757
		private enum Operator
		{
			// Token: 0x04000939 RID: 2361
			Add,
			// Token: 0x0400093A RID: 2362
			Subtract,
			// Token: 0x0400093B RID: 2363
			Multiplication,
			// Token: 0x0400093C RID: 2364
			Division
		}
	}
}
