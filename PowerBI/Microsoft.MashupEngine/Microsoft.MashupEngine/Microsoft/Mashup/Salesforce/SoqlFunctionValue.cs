using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001F7 RID: 503
	internal static class SoqlFunctionValue
	{
		// Token: 0x0400060A RID: 1546
		public static readonly FunctionValue CalendarMonth = new SoqlFunctionValue.PrimitiveFunctionValue(NullableTypeValue.Number, new TypeValue[] { NullableTypeValue.Date });

		// Token: 0x0400060B RID: 1547
		public static readonly FunctionValue CalendarQuarter = new SoqlFunctionValue.PrimitiveFunctionValue(NullableTypeValue.Number, new TypeValue[] { NullableTypeValue.Date });

		// Token: 0x0400060C RID: 1548
		public static readonly FunctionValue CalendarYear = new SoqlFunctionValue.PrimitiveFunctionValue(NullableTypeValue.Number, new TypeValue[] { NullableTypeValue.Date });

		// Token: 0x0400060D RID: 1549
		public static readonly FunctionValue DayInMonth = new SoqlFunctionValue.PrimitiveFunctionValue(NullableTypeValue.Number, new TypeValue[] { NullableTypeValue.Date });

		// Token: 0x0400060E RID: 1550
		public static readonly FunctionValue DayInWeek = new SoqlFunctionValue.PrimitiveFunctionValue(NullableTypeValue.Number, new TypeValue[] { NullableTypeValue.Date });

		// Token: 0x0400060F RID: 1551
		public static readonly FunctionValue DayInYear = new SoqlFunctionValue.PrimitiveFunctionValue(NullableTypeValue.Number, new TypeValue[] { NullableTypeValue.Date });

		// Token: 0x04000610 RID: 1552
		public static readonly FunctionValue DayOnly = new SoqlFunctionValue.PrimitiveFunctionValue(NullableTypeValue.Date, new TypeValue[] { NullableTypeValue.DateTime });

		// Token: 0x04000611 RID: 1553
		public static readonly FunctionValue WeekInMonth = new SoqlFunctionValue.PrimitiveFunctionValue(NullableTypeValue.Number, new TypeValue[] { NullableTypeValue.Date });

		// Token: 0x04000612 RID: 1554
		public static readonly FunctionValue WeekInYear = new SoqlFunctionValue.PrimitiveFunctionValue(NullableTypeValue.Number, new TypeValue[] { NullableTypeValue.Date });

		// Token: 0x04000613 RID: 1555
		public static readonly FunctionValue HourInDay = new SoqlFunctionValue.PrimitiveFunctionValue(NullableTypeValue.Number, new TypeValue[] { NullableTypeValue.DateTime });

		// Token: 0x04000614 RID: 1556
		public static readonly FunctionValue Avg = new SoqlFunctionValue.NumericAggregateFunctionValue();

		// Token: 0x04000615 RID: 1557
		public static readonly FunctionValue Max = new SoqlFunctionValue.AnyAggregateFunctionValue();

		// Token: 0x04000616 RID: 1558
		public static readonly FunctionValue Min = new SoqlFunctionValue.AnyAggregateFunctionValue();

		// Token: 0x04000617 RID: 1559
		public static readonly FunctionValue Sum = new SoqlFunctionValue.NumericAggregateFunctionValue();

		// Token: 0x04000618 RID: 1560
		public static readonly FunctionValue ListContains = new SoqlFunctionValue.ListContainsFunctionValue();

		// Token: 0x04000619 RID: 1561
		public static readonly FunctionValue TextContains = new SoqlFunctionValue.PrimitiveFunctionValue(TypeValue.Logical, new TypeValue[]
		{
			NullableTypeValue.Text,
			NullableTypeValue.Text
		});

		// Token: 0x0400061A RID: 1562
		public static readonly FunctionValue TextStartsWith = new SoqlFunctionValue.PrimitiveFunctionValue(TypeValue.Logical, new TypeValue[]
		{
			NullableTypeValue.Text,
			NullableTypeValue.Text
		});

		// Token: 0x0400061B RID: 1563
		public static readonly FunctionValue TextEndsWith = new SoqlFunctionValue.PrimitiveFunctionValue(TypeValue.Logical, new TypeValue[]
		{
			NullableTypeValue.Text,
			NullableTypeValue.Text
		});

		// Token: 0x0400061C RID: 1564
		public static readonly FunctionValue TotalCount = new SoqlFunctionValue.PrimitiveFunctionValue(TypeValue.Number, new TypeValue[] { TypeValue.Table });

		// Token: 0x0400061D RID: 1565
		public static readonly FunctionValue Count = new SoqlFunctionValue.PrimitiveFunctionValue(TypeValue.Number, new TypeValue[] { TypeValue.Table });

		// Token: 0x0400061E RID: 1566
		public static readonly FunctionValue DateTimeToDate = new SoqlFunctionValue.PrimitiveFunctionValue(TypeValue.Date, new TypeValue[] { TypeValue.DateTime });

		// Token: 0x020001F8 RID: 504
		private class PrimitiveFunctionValue : NativeFunctionValueN
		{
			// Token: 0x06000A1B RID: 2587 RVA: 0x000168D7 File Offset: 0x00014AD7
			public PrimitiveFunctionValue(TypeValue returnType, params TypeValue[] parameters)
				: base(parameters.Length, SoqlFunctionValue.PrimitiveFunctionValue.args[parameters.Length])
			{
				this.parameters = parameters;
				this.returnType = returnType;
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x000168FC File Offset: 0x00014AFC
			protected override Value InvokeN(Value[] args)
			{
				for (int i = 0; i < args.Length; i++)
				{
					TypeValue typeValue = this.parameters[i];
					TypeValue asType = args[i].AsType;
					if (asType.TypeKind != typeValue.TypeKind || (asType.IsNullable && !typeValue.IsNullable))
					{
						return TypeValue.None;
					}
				}
				return this.returnType;
			}

			// Token: 0x0400061F RID: 1567
			private static readonly string[][] args = new string[][]
			{
				new string[0],
				new string[] { "arg0" },
				new string[] { "arg0", "arg1" }
			};

			// Token: 0x04000620 RID: 1568
			private readonly TypeValue[] parameters;

			// Token: 0x04000621 RID: 1569
			private readonly TypeValue returnType;
		}

		// Token: 0x020001F9 RID: 505
		private class ListContainsFunctionValue : NativeFunctionValue2, IArgumentValuesChecker
		{
			// Token: 0x06000A1E RID: 2590 RVA: 0x00016993 File Offset: 0x00014B93
			public override Value Invoke(Value list, Value value)
			{
				if (list.AsType.TypeKind != ValueKind.List || !SalesforceTypes.IsScalar(value.Type))
				{
					return TypeValue.None;
				}
				return TypeValue.Logical;
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x000169BC File Offset: 0x00014BBC
			public void CheckArguments(TypedQueryExpression[] args)
			{
				Value value;
				if (!args[0].Expression.TryGetConstant(out value) || !value.IsList)
				{
					throw new NotSupportedException();
				}
				ValueKind typeKind = args[1].Type.TypeKind;
				foreach (IValueReference valueReference in value.AsList)
				{
					if (typeKind != valueReference.Value.Kind)
					{
						throw new NotSupportedException();
					}
				}
			}
		}

		// Token: 0x020001FA RID: 506
		private class AnyAggregateFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06000A21 RID: 2593 RVA: 0x00016A54 File Offset: 0x00014C54
			public override Value Invoke(Value arg0)
			{
				TypeValue asType = arg0.AsType;
				if (asType.TypeKind != ValueKind.List)
				{
					return TypeValue.None;
				}
				return asType.AsListType.ItemType.Nullable;
			}
		}

		// Token: 0x020001FB RID: 507
		private class NumericAggregateFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06000A23 RID: 2595 RVA: 0x00016A90 File Offset: 0x00014C90
			public override Value Invoke(Value arg0)
			{
				TypeValue asType = arg0.AsType;
				if (asType.TypeKind != ValueKind.List)
				{
					return TypeValue.None;
				}
				TypeValue itemType = asType.AsListType.ItemType;
				if (itemType.Kind != ValueKind.Number)
				{
					return TypeValue.None;
				}
				return itemType.Nullable;
			}
		}
	}
}
