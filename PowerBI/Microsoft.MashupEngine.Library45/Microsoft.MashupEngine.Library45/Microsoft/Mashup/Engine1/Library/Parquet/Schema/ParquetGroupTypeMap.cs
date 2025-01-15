using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001F85 RID: 8069
	internal class ParquetGroupTypeMap : ParquetTypeMap
	{
		// Token: 0x06010ED5 RID: 69333 RVA: 0x003A581B File Offset: 0x003A3A1B
		public ParquetGroupTypeMap(ICollection<ValueKind> typeKinds, LogicalTypeEnum logicalTypeEnum, Func<LogicalType> logicalTypeCtor, Func<TypeValue, Func<RecordValue, Value>> getToValue, Func<TypeValue, Func<IAllocator, Value, RecordValue>> getFromValue, Func<RecordTypeValue, TypeValue> toTypeValue, Func<TypeValue, RecordTypeValue> fromTypeValue, Func<NestedColumnSelection, NestedColumnSelection> mapColumnSelection)
			: base((PhysicalType)(-1), logicalTypeEnum, logicalTypeCtor, null)
		{
			this.typeKinds = typeKinds;
			this.getToValue = getToValue;
			this.getFromValue = getFromValue;
			this.toTypeValue = toTypeValue;
			this.fromTypeValue = fromTypeValue;
			this.mapColumnSelection = mapColumnSelection;
		}

		// Token: 0x17002CD3 RID: 11475
		// (get) Token: 0x06010ED6 RID: 69334 RVA: 0x003A5856 File Offset: 0x003A3A56
		public override ICollection<ValueKind> TypeKinds
		{
			get
			{
				return this.typeKinds;
			}
		}

		// Token: 0x06010ED7 RID: 69335 RVA: 0x003A585E File Offset: 0x003A3A5E
		public override Func<object, Value> GetToValue(TypeValue expectedTypeValue)
		{
			Func<RecordValue, Value> toValue = this.getToValue(expectedTypeValue);
			return (object obj) => toValue((RecordValue)obj);
		}

		// Token: 0x06010ED8 RID: 69336 RVA: 0x003A5882 File Offset: 0x003A3A82
		public override Func<IAllocator, Value, object> GetFromValue(TypeValue expectedTypeValue)
		{
			Func<IAllocator, Value, RecordValue> fromValue = this.getFromValue(expectedTypeValue);
			return (IAllocator allocator, Value value) => fromValue(allocator, value);
		}

		// Token: 0x06010ED9 RID: 69337 RVA: 0x003A58A6 File Offset: 0x003A3AA6
		public TypeValue ToTypeValue(RecordTypeValue recordTypeValue)
		{
			return this.toTypeValue(recordTypeValue);
		}

		// Token: 0x06010EDA RID: 69338 RVA: 0x003A58B4 File Offset: 0x003A3AB4
		public RecordTypeValue FromTypeValue(TypeValue typeValue)
		{
			return this.fromTypeValue(typeValue);
		}

		// Token: 0x06010EDB RID: 69339 RVA: 0x003A58C2 File Offset: 0x003A3AC2
		public NestedColumnSelection MapColumnSelection(NestedColumnSelection columnSelection)
		{
			return this.mapColumnSelection(columnSelection);
		}

		// Token: 0x06010EDC RID: 69340 RVA: 0x003A58D0 File Offset: 0x003A3AD0
		public static Keys GenerateKeys(string[] initialKeys)
		{
			return ColumnLabelGenerator.GenerateKeys(initialKeys, initialKeys.Length);
		}

		// Token: 0x06010EDD RID: 69341 RVA: 0x003A58DB File Offset: 0x003A3ADB
		public static TypeValue MarkIsGenerated(TypeValue typeValue)
		{
			return BinaryOperator.AddMeta.Invoke(typeValue, RecordValue.New(ParquetGroupTypeMap.GeneratedTypeMetadataKeys, new Value[] { ParquetGroupTypeMap.IsGeneratedFunctionValue.Instance })).AsType;
		}

		// Token: 0x06010EDE RID: 69342 RVA: 0x003A5908 File Offset: 0x003A3B08
		public static bool IsGenerated(TypeValue typeValue)
		{
			Value value;
			return typeValue.TryGetMetaField("IsGenerated", out value) && value is ParquetGroupTypeMap.IsGeneratedFunctionValue;
		}

		// Token: 0x040065F5 RID: 26101
		private const string IsGeneratedKey = "IsGenerated";

		// Token: 0x040065F6 RID: 26102
		private static readonly Keys GeneratedTypeMetadataKeys = Keys.New("IsGenerated");

		// Token: 0x040065F7 RID: 26103
		private readonly ICollection<ValueKind> typeKinds;

		// Token: 0x040065F8 RID: 26104
		private readonly Func<TypeValue, Func<RecordValue, Value>> getToValue;

		// Token: 0x040065F9 RID: 26105
		private readonly Func<TypeValue, Func<IAllocator, Value, RecordValue>> getFromValue;

		// Token: 0x040065FA RID: 26106
		private readonly Func<RecordTypeValue, TypeValue> toTypeValue;

		// Token: 0x040065FB RID: 26107
		private readonly Func<TypeValue, RecordTypeValue> fromTypeValue;

		// Token: 0x040065FC RID: 26108
		private readonly Func<NestedColumnSelection, NestedColumnSelection> mapColumnSelection;

		// Token: 0x02001F86 RID: 8070
		private sealed class IsGeneratedFunctionValue : NativeFunctionValue0<LogicalValue>
		{
			// Token: 0x06010EE0 RID: 69344 RVA: 0x003A5940 File Offset: 0x003A3B40
			public IsGeneratedFunctionValue()
				: base(TypeValue.Logical)
			{
			}

			// Token: 0x06010EE1 RID: 69345 RVA: 0x003A594D File Offset: 0x003A3B4D
			public override LogicalValue TypedInvoke()
			{
				return LogicalValue.True;
			}

			// Token: 0x040065FD RID: 26109
			public static readonly ParquetGroupTypeMap.IsGeneratedFunctionValue Instance = new ParquetGroupTypeMap.IsGeneratedFunctionValue();
		}
	}
}
