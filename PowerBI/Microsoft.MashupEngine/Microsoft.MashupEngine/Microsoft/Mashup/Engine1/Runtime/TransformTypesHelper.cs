using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001677 RID: 5751
	internal struct TransformTypesHelper
	{
		// Token: 0x06009190 RID: 37264 RVA: 0x001E3810 File Offset: 0x001E1A10
		public TransformTypesHelper(IEngineHost engineHost, ICulture culture)
		{
			this.numberFrom = new Library.Number.FromFunctionValue(engineHost, culture);
			this.textFrom = new Library.Text.FromFunctionValue(engineHost, culture);
			this.dateFrom = new Library.Date.FromFunctionValue(engineHost, culture);
			this.dateTimeFrom = new Library.DateTime.FromFunctionValue(engineHost, culture);
			this.dateTimeZoneFrom = new Library.DateTimeZone.FromFunctionValue(engineHost, culture);
			this.timeFrom = new Library.Time.FromFunctionValue(engineHost, culture);
			this.byteFrom = new Library.NumberAliasTypes.FromIntType(engineHost, TypeCode.Byte, culture);
			this.int8From = new Library.NumberAliasTypes.FromIntType(engineHost, TypeCode.SByte, culture);
			this.int16From = new Library.NumberAliasTypes.FromIntType(engineHost, TypeCode.Int16, culture);
			this.int32From = new Library.NumberAliasTypes.FromIntType(engineHost, TypeCode.Int32, culture);
			this.int64From = new Library.NumberAliasTypes.FromIntType(engineHost, TypeCode.Int64, culture);
			this.singleFrom = new Library.NumberAliasTypes.FromNonIntType(engineHost, TypeCode.Single, culture);
			this.decimalFrom = new Library.NumberAliasTypes.FromNonIntType(engineHost, TypeCode.Decimal, culture);
			this.doubleFrom = new Library.NumberAliasTypes.FromNonIntType(engineHost, TypeCode.Double, culture);
			this.currencyFrom = new Library.Currency.FromFunctionValue(engineHost, culture);
			this.percentageFrom = new Library.Percentage.FromFunctionValue(engineHost, culture);
		}

		// Token: 0x06009191 RID: 37265 RVA: 0x001E38FC File Offset: 0x001E1AFC
		public FunctionValue GetFunctionValueFromType(TypeValue typeValue, TypeValue columnType, bool supportListAndRecordKinds = false)
		{
			switch (typeValue.TypeKind)
			{
			case ValueKind.Any:
				return TransformTypesHelper.IdentityFunctionValue.Instance;
			case ValueKind.Time:
				return this.timeFrom;
			case ValueKind.Date:
				return this.dateFrom;
			case ValueKind.DateTime:
			{
				Value value;
				if (columnType.TryGetMetaField("WindowsNTFileTimeFormat", out value) && value.AsLogical.Equals(LogicalValue.True))
				{
					return Library.DateTime.FromFileTime;
				}
				return this.dateTimeFrom;
			}
			case ValueKind.DateTimeZone:
			{
				Value value;
				if (columnType.TryGetMetaField("WindowsNTFileTimeFormat", out value) && value.AsLogical.Equals(LogicalValue.True))
				{
					return Library.DateTimeZone.FromFileTime;
				}
				return this.dateTimeZoneFrom;
			}
			case ValueKind.Duration:
				return Library.Duration.From;
			case ValueKind.Number:
				typeValue = typeValue.NonNullable;
				if (typeValue.Equals(TypeValue.Byte))
				{
					return this.byteFrom;
				}
				if (typeValue.Equals(TypeValue.Int8))
				{
					return this.int8From;
				}
				if (typeValue.Equals(TypeValue.Int16))
				{
					return this.int16From;
				}
				if (typeValue.Equals(TypeValue.Int32))
				{
					return this.int32From;
				}
				if (typeValue.Equals(TypeValue.Int64))
				{
					return this.int64From;
				}
				if (typeValue.Equals(TypeValue.Single))
				{
					return this.singleFrom;
				}
				if (typeValue.Equals(TypeValue.Decimal))
				{
					return this.decimalFrom;
				}
				if (typeValue.Equals(TypeValue.Double))
				{
					return this.doubleFrom;
				}
				if (typeValue.Equals(TypeValue.Currency))
				{
					return this.currencyFrom;
				}
				if (typeValue.Equals(TypeValue.Percentage))
				{
					return this.percentageFrom;
				}
				return this.numberFrom;
			case ValueKind.Logical:
				return Library.Logical.From;
			case ValueKind.Text:
				if (typeValue.NonNullable.Equals(TypeValue.Guid))
				{
					return Library._Guid.From;
				}
				return this.textFrom;
			case ValueKind.Binary:
				return Library.Binary.From;
			case ValueKind.List:
				if (supportListAndRecordKinds)
				{
					return new TransformTypesHelper.ValueAsFunctionValue(NullableTypeValue.List);
				}
				break;
			case ValueKind.Record:
				if (supportListAndRecordKinds)
				{
					return new TransformTypesHelper.ValueAsFunctionValue(NullableTypeValue.Record);
				}
				break;
			}
			throw ValueException.NewExpressionError<Message0>(Strings.Table_TransformColumnTypes_UnrecognizedType, TextValue.New(typeValue.TypeKind.ToString()), null);
		}

		// Token: 0x04004E1C RID: 19996
		private readonly FunctionValue numberFrom;

		// Token: 0x04004E1D RID: 19997
		private readonly FunctionValue textFrom;

		// Token: 0x04004E1E RID: 19998
		private readonly FunctionValue dateFrom;

		// Token: 0x04004E1F RID: 19999
		private readonly FunctionValue dateTimeFrom;

		// Token: 0x04004E20 RID: 20000
		private readonly FunctionValue dateTimeZoneFrom;

		// Token: 0x04004E21 RID: 20001
		private readonly FunctionValue timeFrom;

		// Token: 0x04004E22 RID: 20002
		private readonly FunctionValue byteFrom;

		// Token: 0x04004E23 RID: 20003
		private readonly FunctionValue int8From;

		// Token: 0x04004E24 RID: 20004
		private readonly FunctionValue int16From;

		// Token: 0x04004E25 RID: 20005
		private readonly FunctionValue int32From;

		// Token: 0x04004E26 RID: 20006
		private readonly FunctionValue int64From;

		// Token: 0x04004E27 RID: 20007
		private readonly FunctionValue singleFrom;

		// Token: 0x04004E28 RID: 20008
		private readonly FunctionValue decimalFrom;

		// Token: 0x04004E29 RID: 20009
		private readonly FunctionValue doubleFrom;

		// Token: 0x04004E2A RID: 20010
		private readonly FunctionValue currencyFrom;

		// Token: 0x04004E2B RID: 20011
		private readonly FunctionValue percentageFrom;

		// Token: 0x02001678 RID: 5752
		private sealed class IdentityFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06009192 RID: 37266 RVA: 0x0000A6A5 File Offset: 0x000088A5
			public override Value Invoke(Value arg0)
			{
				return arg0;
			}

			// Token: 0x17002613 RID: 9747
			// (get) Token: 0x06009193 RID: 37267 RVA: 0x001E3B09 File Offset: 0x001E1D09
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new ImplicitIdentifierExpressionSyntaxNode());
					}
					return this.expression;
				}
			}

			// Token: 0x04004E2C RID: 20012
			public static TransformTypesHelper.IdentityFunctionValue Instance = new TransformTypesHelper.IdentityFunctionValue();

			// Token: 0x04004E2D RID: 20013
			private IExpression expression;
		}

		// Token: 0x02001679 RID: 5753
		private class ValueAsFunctionValue : NativeFunctionValue1<Value, Value>
		{
			// Token: 0x06009196 RID: 37270 RVA: 0x001E3B3A File Offset: 0x001E1D3A
			public ValueAsFunctionValue(TypeValue type)
				: base(TypeValue.Any, "value", TypeValue.Any)
			{
				this.type = type;
			}

			// Token: 0x06009197 RID: 37271 RVA: 0x001E3B58 File Offset: 0x001E1D58
			public override Value TypedInvoke(Value value)
			{
				return value.As(this.type);
			}

			// Token: 0x04004E2E RID: 20014
			private readonly TypeValue type;
		}
	}
}
