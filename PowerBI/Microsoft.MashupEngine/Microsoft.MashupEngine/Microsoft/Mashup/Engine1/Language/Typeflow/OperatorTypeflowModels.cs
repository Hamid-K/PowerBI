using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Typeflow
{
	// Token: 0x020017B5 RID: 6069
	internal static class OperatorTypeflowModels
	{
		// Token: 0x06009975 RID: 39285 RVA: 0x001FB5D4 File Offset: 0x001F97D4
		public static TypeValue Binary(BinaryOperator2 op, TypeValue type1, TypeValue type2)
		{
			if (type1.IsNullable && OperatorTypeflowModels.ArgDeterminesNullability(op, 0))
			{
				return OperatorTypeflowModels.BinaryCore(op, TypeAlgebra.StripNullable(type1), type2).Nullable;
			}
			if (type2.IsNullable && OperatorTypeflowModels.ArgDeterminesNullability(op, 1))
			{
				return OperatorTypeflowModels.BinaryCore(op, type1, TypeAlgebra.StripNullable(type2)).Nullable;
			}
			return OperatorTypeflowModels.BinaryCore(op, type1, type2);
		}

		// Token: 0x06009976 RID: 39286 RVA: 0x001FB634 File Offset: 0x001F9834
		private static TypeValue BinaryCore(BinaryOperator2 op, TypeValue type1, TypeValue type2)
		{
			switch (op)
			{
			case BinaryOperator2.Add:
				if (type1.TypeKind == ValueKind.Null || type2.TypeKind == ValueKind.Null)
				{
					return TypeValue.Null;
				}
				switch (type2.TypeKind)
				{
				case ValueKind.Any:
					switch (type1.TypeKind)
					{
					case ValueKind.Any:
					case ValueKind.Duration:
						return type2;
					case ValueKind.Time:
						return NullableTypeValue.Time;
					case ValueKind.DateTime:
						return NullableTypeValue.DateTime;
					case ValueKind.DateTimeZone:
						return NullableTypeValue.DateTimeZone;
					case ValueKind.Number:
						return NullableTypeValue.Number;
					}
					break;
				case ValueKind.Time:
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any)
					{
						return NullableTypeValue.Time;
					}
					if (valueKind == ValueKind.Duration)
					{
						return type2;
					}
					break;
				}
				case ValueKind.DateTime:
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any)
					{
						return NullableTypeValue.DateTime;
					}
					if (valueKind == ValueKind.Duration)
					{
						return type2;
					}
					break;
				}
				case ValueKind.DateTimeZone:
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any)
					{
						return NullableTypeValue.DateTimeZone;
					}
					if (valueKind == ValueKind.Duration)
					{
						return type2;
					}
					break;
				}
				case ValueKind.Duration:
					switch (type1.TypeKind)
					{
					case ValueKind.Any:
					case ValueKind.Time:
					case ValueKind.Date:
					case ValueKind.DateTime:
					case ValueKind.DateTimeZone:
						return type1;
					case ValueKind.Duration:
						return type2;
					}
					break;
				case ValueKind.Number:
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any)
					{
						return NullableTypeValue.Number;
					}
					if (valueKind == ValueKind.Number)
					{
						return TypeValue.Number;
					}
					break;
				}
				}
				break;
			case BinaryOperator2.Subtract:
				if (type1.TypeKind == ValueKind.Null || type2.TypeKind == ValueKind.Null)
				{
					return TypeValue.Null;
				}
				switch (type2.TypeKind)
				{
				case ValueKind.Any:
					switch (type1.TypeKind)
					{
					case ValueKind.Any:
						return type1;
					case ValueKind.Time:
						return NullableTypeValue.Time;
					case ValueKind.DateTime:
						return NullableTypeValue.DateTime;
					case ValueKind.DateTimeZone:
						return NullableTypeValue.DateTimeZone;
					case ValueKind.Duration:
						return NullableTypeValue.Duration;
					case ValueKind.Number:
						return NullableTypeValue.Number;
					}
					break;
				case ValueKind.Time:
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any)
					{
						return NullableTypeValue.Time;
					}
					if (valueKind == ValueKind.Time)
					{
						return TypeValue.Duration;
					}
					break;
				}
				case ValueKind.Date:
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any)
					{
						return NullableTypeValue.Date;
					}
					if (valueKind == ValueKind.Date)
					{
						return TypeValue.Duration;
					}
					break;
				}
				case ValueKind.DateTime:
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any)
					{
						return NullableTypeValue.DateTime;
					}
					if (valueKind == ValueKind.DateTime)
					{
						return TypeValue.Duration;
					}
					break;
				}
				case ValueKind.DateTimeZone:
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any)
					{
						return NullableTypeValue.DateTimeZone;
					}
					if (valueKind == ValueKind.DateTimeZone)
					{
						return TypeValue.Duration;
					}
					break;
				}
				case ValueKind.Duration:
					switch (type1.TypeKind)
					{
					case ValueKind.Any:
					case ValueKind.Time:
					case ValueKind.Date:
					case ValueKind.DateTime:
					case ValueKind.DateTimeZone:
						return type1;
					case ValueKind.Duration:
						return type2;
					}
					break;
				case ValueKind.Number:
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any)
					{
						return NullableTypeValue.Number;
					}
					if (valueKind == ValueKind.Number)
					{
						return TypeValue.Number;
					}
					break;
				}
				}
				break;
			case BinaryOperator2.Multiply:
			{
				if (type1.TypeKind == ValueKind.Null || type2.TypeKind == ValueKind.Null)
				{
					return TypeValue.Null;
				}
				ValueKind valueKind2 = type2.TypeKind;
				if (valueKind2 == ValueKind.Any)
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind != ValueKind.Any)
					{
						if (valueKind == ValueKind.Duration)
						{
							return NullableTypeValue.Duration;
						}
						if (valueKind != ValueKind.Number)
						{
							break;
						}
					}
					return type2;
				}
				if (valueKind2 != ValueKind.Duration)
				{
					if (valueKind2 == ValueKind.Number)
					{
						ValueKind valueKind = type1.TypeKind;
						if (valueKind == ValueKind.Any)
						{
							return type1;
						}
						if (valueKind == ValueKind.Duration)
						{
							return type1;
						}
						if (valueKind == ValueKind.Number)
						{
							return TypeValue.Number;
						}
					}
				}
				else
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any)
					{
						return NullableTypeValue.Duration;
					}
					if (valueKind == ValueKind.Number)
					{
						return type2;
					}
				}
				break;
			}
			case BinaryOperator2.Divide:
			{
				if (type1.TypeKind == ValueKind.Null || type2.TypeKind == ValueKind.Null)
				{
					return TypeValue.Null;
				}
				ValueKind valueKind2 = type2.TypeKind;
				if (valueKind2 != ValueKind.Any)
				{
					if (valueKind2 != ValueKind.Duration)
					{
						if (valueKind2 == ValueKind.Number)
						{
							ValueKind valueKind = type1.TypeKind;
							if (valueKind == ValueKind.Any)
							{
								return type1;
							}
							if (valueKind == ValueKind.Duration)
							{
								return type1;
							}
							if (valueKind == ValueKind.Number)
							{
								return TypeValue.Number;
							}
						}
					}
					else
					{
						ValueKind valueKind = type1.TypeKind;
						if (valueKind == ValueKind.Any)
						{
							return NullableTypeValue.Duration;
						}
						if (valueKind == ValueKind.Duration)
						{
							return TypeValue.Number;
						}
					}
				}
				else
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any || valueKind == ValueKind.Duration)
					{
						return type2;
					}
					if (valueKind == ValueKind.Number)
					{
						return NullableTypeValue.Number;
					}
				}
				break;
			}
			case BinaryOperator2.GreaterThan:
			case BinaryOperator2.LessThan:
			case BinaryOperator2.GreaterThanOrEquals:
			case BinaryOperator2.LessThanOrEquals:
				if (type1.TypeKind == ValueKind.Null || type2.TypeKind == ValueKind.Null)
				{
					return TypeValue.Null;
				}
				switch (type2.TypeKind)
				{
				case ValueKind.Any:
					switch (type1.TypeKind)
					{
					case ValueKind.Any:
					case ValueKind.Time:
					case ValueKind.Date:
					case ValueKind.DateTime:
					case ValueKind.DateTimeZone:
					case ValueKind.Duration:
					case ValueKind.Number:
					case ValueKind.Text:
						return NullableTypeValue.Logical;
					}
					break;
				case ValueKind.Time:
				case ValueKind.Date:
				case ValueKind.DateTime:
				case ValueKind.DateTimeZone:
				case ValueKind.Duration:
				case ValueKind.Number:
				case ValueKind.Text:
					if (type1.TypeKind == type2.TypeKind)
					{
						return TypeValue.Logical;
					}
					if (type1.TypeKind == ValueKind.Any)
					{
						return NullableTypeValue.Logical;
					}
					break;
				}
				break;
			case BinaryOperator2.Equals:
			case BinaryOperator2.NotEquals:
				return TypeAlgebra.PreferredResult(TypeValue.Logical, type1, type2);
			case BinaryOperator2.And:
			{
				ValueKind valueKind2 = type2.TypeKind;
				if (valueKind2 != ValueKind.Any)
				{
					if (valueKind2 != ValueKind.Null)
					{
						if (valueKind2 == ValueKind.Logical)
						{
							ValueKind valueKind = type1.TypeKind;
							if (valueKind == ValueKind.Any)
							{
								return NullableTypeValue.Logical;
							}
							if (valueKind == ValueKind.Null)
							{
								return NullableTypeValue.Logical;
							}
							if (valueKind == ValueKind.Logical)
							{
								return type2;
							}
						}
					}
					else
					{
						ValueKind valueKind = type1.TypeKind;
						if (valueKind == ValueKind.Any)
						{
							return NullableTypeValue.Logical;
						}
						if (valueKind == ValueKind.Null)
						{
							return type2;
						}
						if (valueKind == ValueKind.Logical)
						{
							return NullableTypeValue.Logical;
						}
					}
				}
				else
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind - ValueKind.Any <= 1 || valueKind == ValueKind.Logical)
					{
						return NullableTypeValue.Logical;
					}
				}
				break;
			}
			case BinaryOperator2.Or:
			{
				ValueKind valueKind2 = type2.TypeKind;
				if (valueKind2 != ValueKind.Any)
				{
					if (valueKind2 != ValueKind.Null)
					{
						if (valueKind2 == ValueKind.Logical)
						{
							ValueKind valueKind = type1.TypeKind;
							if (valueKind == ValueKind.Any)
							{
								return NullableTypeValue.Logical;
							}
							if (valueKind == ValueKind.Null)
							{
								return NullableTypeValue.Logical;
							}
							if (valueKind == ValueKind.Logical)
							{
								return type2;
							}
						}
					}
					else
					{
						ValueKind valueKind = type1.TypeKind;
						if (valueKind == ValueKind.Any)
						{
							return NullableTypeValue.Logical;
						}
						if (valueKind == ValueKind.Null)
						{
							return type2;
						}
						if (valueKind == ValueKind.Logical)
						{
							return NullableTypeValue.Logical;
						}
					}
				}
				else
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind - ValueKind.Any <= 1 || valueKind == ValueKind.Logical)
					{
						return NullableTypeValue.Logical;
					}
				}
				break;
			}
			case BinaryOperator2.MetadataAdd:
			{
				ValueKind valueKind2 = type2.TypeKind;
				if (valueKind2 == ValueKind.Any || valueKind2 == ValueKind.Record)
				{
					return type1;
				}
				break;
			}
			case BinaryOperator2.Range:
			{
				ValueKind valueKind2 = type2.TypeKind;
				if (valueKind2 != ValueKind.Any)
				{
					if (valueKind2 != ValueKind.Number)
					{
						if (valueKind2 == ValueKind.Text)
						{
							ValueKind valueKind = type1.TypeKind;
							if (valueKind == ValueKind.Any || valueKind == ValueKind.Text)
							{
								return ListTypeValue.New(type2);
							}
						}
					}
					else
					{
						ValueKind valueKind = type1.TypeKind;
						if (valueKind == ValueKind.Any || valueKind == ValueKind.Number)
						{
							return ListTypeValue.New(type2);
						}
					}
				}
				else
				{
					ValueKind valueKind = type1.TypeKind;
					if (valueKind == ValueKind.Any)
					{
						return ListTypeValue.New(type2);
					}
					if (valueKind == ValueKind.Number || valueKind == ValueKind.Text)
					{
						return ListTypeValue.New(type1);
					}
				}
				break;
			}
			case BinaryOperator2.Concatenate:
				return TypeAlgebra.Concatenate(type1, type2);
			case BinaryOperator2.As:
			{
				ValueKind valueKind2 = type2.TypeKind;
				if (valueKind2 == ValueKind.Any)
				{
					return TypeAlgebra.PreferredResult(TypeValue.Any, type1);
				}
				if (valueKind2 == ValueKind.Type)
				{
					return type1;
				}
				break;
			}
			case BinaryOperator2.Is:
			{
				ValueKind valueKind2 = type2.TypeKind;
				if (valueKind2 == ValueKind.Any || valueKind2 == ValueKind.Type)
				{
					return TypeAlgebra.PreferredResult(TypeValue.Logical, type1, type2);
				}
				break;
			}
			case BinaryOperator2.Coalesce:
				if (!type1.IsNullable)
				{
					return type1;
				}
				return TypeAlgebra.Union(type1, type2);
			default:
				throw new InvalidOperationException();
			}
			return TypeAlgebra.PreferredResult(TypeValue.None, type1, type2);
		}

		// Token: 0x06009977 RID: 39287 RVA: 0x001FBCD8 File Offset: 0x001F9ED8
		public static TypeValue Unary(UnaryOperator2 op, TypeValue type)
		{
			if (type.IsNullable)
			{
				return OperatorTypeflowModels.UnaryCore(op, TypeAlgebra.StripNullable(type)).Nullable;
			}
			return OperatorTypeflowModels.UnaryCore(op, type);
		}

		// Token: 0x06009978 RID: 39288 RVA: 0x001FBCFC File Offset: 0x001F9EFC
		private static TypeValue UnaryCore(UnaryOperator2 op, TypeValue type)
		{
			if (op != UnaryOperator2.Not)
			{
				if (op - UnaryOperator2.Negative > 1)
				{
					throw new InvalidOperationException();
				}
				ValueKind valueKind = type.TypeKind;
				if (valueKind - ValueKind.Any <= 1 || valueKind - ValueKind.Duration <= 1)
				{
					return type;
				}
			}
			else
			{
				ValueKind valueKind = type.TypeKind;
				if (valueKind == ValueKind.Any)
				{
					return NullableTypeValue.Logical;
				}
				if (valueKind == ValueKind.Null || valueKind == ValueKind.Logical)
				{
					return type;
				}
			}
			return TypeAlgebra.PreferredResult(TypeValue.None, type);
		}

		// Token: 0x06009979 RID: 39289 RVA: 0x001FBD54 File Offset: 0x001F9F54
		private static bool ArgDeterminesNullability(BinaryOperator2 op, int arg)
		{
			switch (op)
			{
			case BinaryOperator2.Add:
			case BinaryOperator2.Subtract:
			case BinaryOperator2.Multiply:
			case BinaryOperator2.Divide:
			case BinaryOperator2.GreaterThan:
			case BinaryOperator2.LessThan:
			case BinaryOperator2.GreaterThanOrEquals:
			case BinaryOperator2.LessThanOrEquals:
			case BinaryOperator2.And:
			case BinaryOperator2.Or:
			case BinaryOperator2.Range:
			case BinaryOperator2.Concatenate:
				return true;
			case BinaryOperator2.Equals:
			case BinaryOperator2.NotEquals:
				return false;
			case BinaryOperator2.MetadataAdd:
			case BinaryOperator2.As:
			case BinaryOperator2.Is:
				return arg == 0;
			case BinaryOperator2.Coalesce:
				return false;
			default:
				throw new InvalidOperationException();
			}
		}
	}
}
