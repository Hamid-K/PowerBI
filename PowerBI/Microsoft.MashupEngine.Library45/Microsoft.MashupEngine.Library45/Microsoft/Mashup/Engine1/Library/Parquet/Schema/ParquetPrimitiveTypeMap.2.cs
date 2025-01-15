using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001F7F RID: 8063
	internal abstract class ParquetPrimitiveTypeMap<T> : ParquetPrimitiveTypeMap
	{
		// Token: 0x06010EA2 RID: 69282 RVA: 0x003A4B8E File Offset: 0x003A2D8E
		public ParquetPrimitiveTypeMap(LogicalTypeEnum logicalTypeEnum, Func<LogicalType> logicalTypeCtor, TypeValue typeValue, TypeFacets facets = null, int? typeLength = null)
			: base(ParquetTypeMap.GetPhysicalType(typeof(T)), logicalTypeEnum, logicalTypeCtor, typeValue, facets, typeLength)
		{
		}

		// Token: 0x06010EA3 RID: 69283
		public abstract Action<T> GetColumnLoader(Microsoft.OleDb.Column column);

		// Token: 0x06010EA4 RID: 69284
		public abstract T GetFromColumn(IAllocator allocator, IColumn column, int row);

		// Token: 0x06010EA5 RID: 69285
		public abstract Value ToValue(T primitive);

		// Token: 0x06010EA6 RID: 69286
		public abstract T FromValue(IAllocator allocator, Value value);

		// Token: 0x06010EA7 RID: 69287 RVA: 0x003A4BAC File Offset: 0x003A2DAC
		public override Func<object, Value> GetToValue(TypeValue expectedTypeValue)
		{
			return (object obj) => this.ToValue((T)((object)obj));
		}

		// Token: 0x06010EA8 RID: 69288 RVA: 0x003A4BBA File Offset: 0x003A2DBA
		public override Func<IAllocator, Value, object> GetFromValue(TypeValue expectedTypeValue)
		{
			return (IAllocator allocator, Value value) => this.FromValue(allocator, value);
		}
	}
}
