using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Parquet.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Parquet;
using ParquetSharp;
using ParquetSharp.Schema;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001F7B RID: 8059
	internal static class ParquetTypeErrors
	{
		// Token: 0x06010E7A RID: 69242 RVA: 0x003A421E File Offset: 0x003A241E
		public static ValueException UnmappedTypeError(TypeValue typeValue, string propertyName, Value value)
		{
			return ParquetTypeErrors.UnmappedTypeError(typeValue, new NamedValue[]
			{
				new NamedValue(propertyName, value)
			});
		}

		// Token: 0x06010E7B RID: 69243 RVA: 0x003A423C File Offset: 0x003A243C
		public static ValueException UnmappedTypeError(TypeValue typeValue, params NamedValue[] properties)
		{
			Value value;
			if (properties.Length == 0)
			{
				value = typeValue ?? Value.Null;
			}
			else if (typeValue != null)
			{
				properties = properties.Add(new NamedValue("Type", typeValue));
				value = RecordValue.New(properties);
			}
			else
			{
				value = RecordValue.New(properties);
			}
			return ValueException.NewExpressionError<Message0>(Resources.UnmappedType, value, null);
		}

		// Token: 0x06010E7C RID: 69244 RVA: 0x003A428C File Offset: 0x003A248C
		public static ValueException UnrecognizedLogicalType(Node node, TypeValue typeValue, SchemaConfig config)
		{
			if (typeValue != null && (typeValue.TypeKind != ValueKind.Any || !typeValue.Facets.IsEmpty))
			{
				throw ParquetTypeErrors.UnmappedTypeError(typeValue, Array.Empty<NamedValue>());
			}
			string text;
			using (ColumnPath path = node.Path)
			{
				text = path.ToDotString();
			}
			string physicalTypeName = ParquetTypeMap.GetPhysicalTypeName(ParquetTypeMap.GetPhysicalTypeOrSentinel(node));
			Keys unrecognizedLogicalTypeDetailKeys = ParquetTypeErrors.UnrecognizedLogicalTypeDetailKeys;
			Value[] array = new Value[3];
			array[0] = TextValue.New(text);
			array[1] = TextValue.New(physicalTypeName);
			array[2] = TextValue.New(node.WithLogicalType((LogicalType logicalType) => logicalType.ToString()));
			Value value = RecordValue.New(unrecognizedLogicalTypeDetailKeys, array);
			return ValueException.NewDataFormatError<Message0>(Resources.UnrecognizedLogicalType, value, null);
		}

		// Token: 0x06010E7D RID: 69245 RVA: 0x003A4350 File Offset: 0x003A2550
		public static ValueException IncompatibleTypeError(TypeValue typeValue, string mismatchedPropertyName, Value expected, Value actual)
		{
			RecordValue recordValue = RecordValue.New(new NamedValue[]
			{
				new NamedValue("Type", typeValue ?? Value.Null),
				new NamedValue("Expected" + mismatchedPropertyName, expected),
				new NamedValue("Actual" + mismatchedPropertyName, actual)
			});
			return ValueException.NewExpressionError<Message0>(Resources.IncompatibleType, recordValue, null);
		}

		// Token: 0x06010E7E RID: 69246 RVA: 0x003A43C0 File Offset: 0x003A25C0
		public static ValueException InsufficientTypeError(TypeValue typeValue, params string[] required)
		{
			Value value;
			if (required.Length == 0)
			{
				value = typeValue;
			}
			else
			{
				value = RecordValue.New(Keys.New("Type", "Required"), new Value[]
				{
					typeValue,
					(required.Length == 1) ? TextValue.New(required[0]) : ListValue.New(required)
				});
			}
			return ValueException.NewDataFormatError<Message0>(Resources.InsufficientType, value, null);
		}

		// Token: 0x06010E7F RID: 69247 RVA: 0x003A4419 File Offset: 0x003A2619
		public static ValueException InsufficientTypeError()
		{
			return ValueException.NewDataFormatError<Message0>(Resources.InsufficientType, Value.Null, null);
		}

		// Token: 0x06010E80 RID: 69248 RVA: 0x003A442B File Offset: 0x003A262B
		public static ValueException UnusableFacet(string facet, string message, Value value)
		{
			return ValueException.NewExpressionError<Message2>(Resources.UnusableFacet(facet, message), RecordValue.New(new NamedValue[]
			{
				new NamedValue(facet, value)
			}), null);
		}

		// Token: 0x06010E81 RID: 69249 RVA: 0x003A4453 File Offset: 0x003A2653
		public static ValueException LengthOutOfRange(long length)
		{
			return ParquetTypeErrors.UnusableFacet("MaxLength", Strings.NumberOutOfRangeInt32, NumberValue.New(length));
		}

		// Token: 0x06010E82 RID: 69250 RVA: 0x003A446F File Offset: 0x003A266F
		public static ValueException MaxDepthExceeded(int maxDepth)
		{
			return ValueException.NewExpressionError<Message1>(Resources.MaxDepthExceededError(maxDepth), null, null);
		}

		// Token: 0x06010E83 RID: 69251 RVA: 0x003A4483 File Offset: 0x003A2683
		public static ValueException CyclicType(string path)
		{
			return ValueException.NewExpressionError<Message1>(Resources.CyclicTypeError(path), null, null);
		}

		// Token: 0x06010E84 RID: 69252 RVA: 0x003A4492 File Offset: 0x003A2692
		public static ValueException NotAGuid(Value value)
		{
			return ValueException.NewDataFormatError<Message0>(Strings.Guid_FromFunction_NotConvertibleToGuid, value, null);
		}

		// Token: 0x040065D0 RID: 26064
		private static readonly Keys UnrecognizedLogicalTypeDetailKeys = Keys.New("SchemaElement", "PhysicalType", "LogicalType");
	}
}
