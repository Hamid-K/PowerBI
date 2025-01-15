using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000377 RID: 887
	internal static class CastValue
	{
		// Token: 0x06001F5B RID: 8027 RVA: 0x000511D6 File Offset: 0x0004F3D6
		public static RecordValue CreateCastOptions(Value culture = null, Value roundingMode = null)
		{
			if (culture == null)
			{
				culture = Value.Null;
			}
			if (roundingMode == null)
			{
				roundingMode = Value.Null;
			}
			return RecordValue.New(CastValue.CastOptionKeys, new Value[] { culture, roundingMode });
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00051204 File Offset: 0x0004F404
		public static Func<TypeValue, Value, Value> Transformer(IEngineHost host)
		{
			return CastValue.Transformer(host, CastValue.CreateCastOptions(null, null));
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00051213 File Offset: 0x0004F413
		public static Func<TypeValue, Value, Value> Transformer(IEngineHost host, RecordValue options)
		{
			return (TypeValue type, Value value) => type.Cast(value, host, options);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00051233 File Offset: 0x0004F433
		public static Value Cast(this TypeValue type, Value value, IEngineHost host)
		{
			return type.Cast(value, host, CastValue.CreateCastOptions(null, null));
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00051244 File Offset: 0x0004F444
		public static Value Cast(this TypeValue type, Value value, IEngineHost host, RecordValue options)
		{
			if (value.Type == type)
			{
				return value;
			}
			switch (type.TypeKind)
			{
			case ValueKind.Any:
				return value;
			case ValueKind.Null:
				if (type.IsNullable)
				{
					return Value.Null;
				}
				throw ValueException.CastTypeMismatch(value, type);
			case ValueKind.Time:
				return new Library.Time.FromFunctionValue(host, null).Invoke(value, CastValue.GetCulture(options));
			case ValueKind.Date:
				return new Library.Date.FromFunctionValue(host, null).Invoke(value, CastValue.GetCulture(options));
			case ValueKind.DateTime:
				return new Library.DateTime.FromFunctionValue(host, null).Invoke(value, CastValue.GetCulture(options));
			case ValueKind.DateTimeZone:
				return new Library.DateTimeZone.FromFunctionValue(host, null).Invoke(value, CastValue.GetCulture(options));
			case ValueKind.Duration:
				return Library.Duration.From.Invoke(value);
			case ValueKind.Number:
				return CastValue.CastToNumber(type, value, host, options);
			case ValueKind.Logical:
				return Library.Logical.From.Invoke(value);
			case ValueKind.Text:
				return new Library.Text.FromFunctionValue(host, null).Invoke(value, CastValue.GetCulture(options));
			case ValueKind.List:
				return type.AsListType.Cast(value, host, options);
			case ValueKind.Record:
				return type.AsRecordType.Cast(value, host, options);
			case ValueKind.Table:
				return type.AsTableType.Cast(value, host, options);
			}
			throw ValueException.CastTypeMismatch(value, type);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0005138B File Offset: 0x0004F58B
		public static Value Cast(this RecordTypeValue type, Value value, IEngineHost host, RecordValue options)
		{
			if (value.Type == type)
			{
				return value;
			}
			return RecordValueTransformer.Transform(value.AsRecord, type, CastValue.Transformer(host, options));
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x000513AC File Offset: 0x0004F5AC
		public static Value Cast(this TableTypeValue type, Value value, IEngineHost host, RecordValue options)
		{
			if (value.Type == type)
			{
				return value;
			}
			if (value.IsTable)
			{
				return TableValueTransformer.Transform(value.AsTable, type, CastValue.Transformer(host, options));
			}
			if (value.IsList)
			{
				return TableValueTransformer.Transform(value.AsList.ToTable(type), type, CastValue.Transformer(host, options));
			}
			throw ValueException.CastTypeMismatch(value, type);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x00051409 File Offset: 0x0004F609
		public static Value Cast(this ListTypeValue type, Value value, IEngineHost host, RecordValue options)
		{
			if (value.Type == type)
			{
				return value;
			}
			if (value.IsList)
			{
				return LanguageLibrary.List.Transform.Invoke(value.AsList, new TransfromValue(type.ItemType, CastValue.Transformer(host, options)));
			}
			throw ValueException.CastTypeMismatch(value, type);
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00051448 File Offset: 0x0004F648
		private static bool AreSameType(TypeValue lhs, TypeValue rhs)
		{
			return lhs == rhs.Nullable || lhs == rhs.NonNullable;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x00051460 File Offset: 0x0004F660
		private static Value CastToNumber(TypeValue type, Value value, IEngineHost host, RecordValue options)
		{
			if (CastValue.AreSameType(type, TypeValue.Number) || CastValue.AreSameType(type, TypeValue.Double))
			{
				return new Library.NumberAliasTypes.FromNonIntType(host, TypeCode.Double, null).Invoke(value, CastValue.GetCulture(options));
			}
			if (CastValue.AreSameType(type, TypeValue.Int32))
			{
				return new Library.NumberAliasTypes.FromIntType(host, TypeCode.Int32, null).Invoke(value, CastValue.GetCulture(options));
			}
			if (CastValue.AreSameType(type, TypeValue.Int64))
			{
				return new Library.NumberAliasTypes.FromIntType(host, TypeCode.Int64, null).Invoke(value, CastValue.GetCulture(options));
			}
			if (CastValue.AreSameType(type, TypeValue.Decimal))
			{
				return new Library.NumberAliasTypes.FromNonIntType(host, TypeCode.Decimal, null).Invoke(value, CastValue.GetCulture(options));
			}
			if (CastValue.AreSameType(type, TypeValue.Byte))
			{
				return new Library.NumberAliasTypes.FromIntType(host, TypeCode.Byte, null).Invoke(value, CastValue.GetCulture(options));
			}
			if (CastValue.AreSameType(type, TypeValue.Int8))
			{
				return new Library.NumberAliasTypes.FromIntType(host, TypeCode.SByte, null).Invoke(value, CastValue.GetCulture(options));
			}
			if (CastValue.AreSameType(type, TypeValue.Int16))
			{
				return new Library.NumberAliasTypes.FromIntType(host, TypeCode.Int16, null).Invoke(value, CastValue.GetCulture(options));
			}
			if (CastValue.AreSameType(type, TypeValue.Single))
			{
				return new Library.NumberAliasTypes.FromNonIntType(host, TypeCode.Single, null).Invoke(value, CastValue.GetCulture(options));
			}
			if (CastValue.AreSameType(type, TypeValue.Currency))
			{
				return new Library.Currency.FromFunctionValue(host, null).Invoke(value, CastValue.GetCulture(options), CastValue.GetRoundingMode(options));
			}
			if (CastValue.AreSameType(type, TypeValue.Percentage))
			{
				return new Library.Percentage.FromFunctionValue(host, null).Invoke(value, CastValue.GetCulture(options));
			}
			if (value.Type.IsCompatibleWith(type))
			{
				return value;
			}
			throw ValueException.CastTypeMismatch(value, type);
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000515F0 File Offset: 0x0004F7F0
		private static Value GetCulture(RecordValue options)
		{
			Value value;
			if (options.TryGetValue("Culture", out value))
			{
				return value;
			}
			return Value.Null;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x00051614 File Offset: 0x0004F814
		private static Value GetRoundingMode(RecordValue options)
		{
			Value value;
			if (options.TryGetValue("RoundingMode", out value))
			{
				return value;
			}
			return Value.Null;
		}

		// Token: 0x04000B65 RID: 2917
		private const string OptionCulture = "Culture";

		// Token: 0x04000B66 RID: 2918
		private const string OptionRoundingMode = "RoundingMode";

		// Token: 0x04000B67 RID: 2919
		private static readonly Keys CastOptionKeys = Keys.New("Culture", "RoundingMode");
	}
}
