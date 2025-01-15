using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016A5 RID: 5797
	internal static class ValueHelper
	{
		// Token: 0x06009359 RID: 37721 RVA: 0x001E6F74 File Offset: 0x001E5174
		public static TypeValue AdjustEnumTypeMetavalues<T>(TypeValue type, T[] allowedValuesData) where T : Value
		{
			RecordValue recordValue = RecordValue.New(ValueHelper.descriptionKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return type.MetaValue["Documentation.Name"];
				case 1:
					return type.MetaValue["Documentation.Description"];
				case 2:
					return type.MetaValue["Documentation.LongDescription"];
				default:
				{
					Value[] allowedValuesData2 = allowedValuesData;
					return ListValue.New(allowedValuesData2);
				}
				}
			});
			return type.NewMeta(recordValue).AsType;
		}

		// Token: 0x0600935A RID: 37722 RVA: 0x001E6FBD File Offset: 0x001E51BD
		public static NumberValue CreateEnumValue(int value)
		{
			return NumberValue.New(value);
		}

		// Token: 0x0600935B RID: 37723 RVA: 0x001E6FC5 File Offset: 0x001E51C5
		public static NumberValue ComparisonResultToValue(int i)
		{
			if (i < 0)
			{
				return NumberValue.NegativeOne;
			}
			if (i > 0)
			{
				return NumberValue.One;
			}
			return NumberValue.Zero;
		}

		// Token: 0x0600935C RID: 37724 RVA: 0x001E6FE0 File Offset: 0x001E51E0
		public static char[] GetCharArrayFromTextOrList(Value value)
		{
			List<char> list = new List<char>();
			if (!value.IsText)
			{
				if (value.IsList)
				{
					using (IEnumerator<IValueReference> enumerator = value.AsList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IValueReference valueReference = enumerator.Current;
							list.Add(valueReference.Value.AsCharacter);
						}
						goto IL_006B;
					}
				}
				throw ValueException.NewExpressionError<Message0>(Strings.InvalidArgument, value, null);
			}
			list.Add(value.AsCharacter);
			IL_006B:
			return list.ToArray();
		}

		// Token: 0x0600935D RID: 37725 RVA: 0x001E7070 File Offset: 0x001E5270
		public static ValueFlags Flags(Value value)
		{
			return (value.MetaValue.IsEmpty ? ValueFlags.None : ValueFlags.HasMeta) | (value.IsDefaultType ? ValueFlags.None : ValueFlags.HasType);
		}

		// Token: 0x0600935E RID: 37726 RVA: 0x001E7094 File Offset: 0x001E5294
		public static NumberKind NumberKind(NumberValue number)
		{
			int num;
			if (number.TryGetInt32(out num))
			{
				return Microsoft.Mashup.Engine1.Runtime.NumberKind.Int32;
			}
			return Microsoft.Mashup.Engine1.Runtime.NumberKind.Double;
		}

		// Token: 0x0600935F RID: 37727 RVA: 0x001E70B0 File Offset: 0x001E52B0
		public static TypeValue PrimitiveType(ValueKind kind, bool nullable)
		{
			switch (kind)
			{
			case ValueKind.None:
				if (!nullable)
				{
					return TypeValue.None;
				}
				return NullableTypeValue.None;
			case ValueKind.Any:
				if (!nullable)
				{
					return TypeValue.Any;
				}
				return NullableTypeValue.Any;
			case ValueKind.Null:
				if (!nullable)
				{
					return TypeValue.Null;
				}
				return NullableTypeValue.Null;
			case ValueKind.Time:
				if (!nullable)
				{
					return TypeValue.Time;
				}
				return NullableTypeValue.Time;
			case ValueKind.Date:
				if (!nullable)
				{
					return TypeValue.Date;
				}
				return NullableTypeValue.Date;
			case ValueKind.DateTime:
				if (!nullable)
				{
					return TypeValue.DateTime;
				}
				return NullableTypeValue.DateTime;
			case ValueKind.DateTimeZone:
				if (!nullable)
				{
					return TypeValue.DateTimeZone;
				}
				return NullableTypeValue.DateTimeZone;
			case ValueKind.Duration:
				if (!nullable)
				{
					return TypeValue.Duration;
				}
				return NullableTypeValue.Duration;
			case ValueKind.Number:
				if (!nullable)
				{
					return TypeValue.Number;
				}
				return NullableTypeValue.Number;
			case ValueKind.Logical:
				if (!nullable)
				{
					return TypeValue.Logical;
				}
				return NullableTypeValue.Logical;
			case ValueKind.Text:
				if (!nullable)
				{
					return TypeValue.Text;
				}
				return NullableTypeValue.Text;
			case ValueKind.Binary:
				if (!nullable)
				{
					return TypeValue.Binary;
				}
				return NullableTypeValue.Binary;
			case ValueKind.Type:
				if (!nullable)
				{
					return TypeValue._Type;
				}
				return NullableTypeValue._Type;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x04004EAD RID: 20141
		private static readonly Keys descriptionKeys = Keys.New("Documentation.Name", "Documentation.Description", "Documentation.LongDescription", "Documentation.AllowedValues");
	}
}
