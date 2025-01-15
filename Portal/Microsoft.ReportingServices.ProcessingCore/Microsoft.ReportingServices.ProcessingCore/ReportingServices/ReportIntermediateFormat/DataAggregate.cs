using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200045C RID: 1116
	internal abstract class DataAggregate : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600334E RID: 13134
		internal abstract void Init();

		// Token: 0x0600334F RID: 13135
		internal abstract void Update(object[] expressions, IErrorContext iErrorContext);

		// Token: 0x06003350 RID: 13136
		internal abstract object Result();

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x06003351 RID: 13137
		internal abstract DataAggregateInfo.AggregateTypes AggregateType { get; }

		// Token: 0x06003352 RID: 13138
		internal abstract DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef);

		// Token: 0x06003353 RID: 13139 RVA: 0x000E3F08 File Offset: 0x000E2108
		internal static DataAggregate.DataTypeCode GetTypeCode(object o)
		{
			bool flag;
			return DataAggregate.GetTypeCode(o, true, out flag);
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000E3F20 File Offset: 0x000E2120
		internal static DataAggregate.DataTypeCode GetTypeCode(object o, bool throwException, out bool valid)
		{
			valid = true;
			if (o is string)
			{
				return DataAggregate.DataTypeCode.String;
			}
			if (o is int)
			{
				return DataAggregate.DataTypeCode.Int32;
			}
			if (o is double)
			{
				return DataAggregate.DataTypeCode.Double;
			}
			if (o == null || DBNull.Value == o)
			{
				return DataAggregate.DataTypeCode.Null;
			}
			if (o is ushort)
			{
				return DataAggregate.DataTypeCode.UInt16;
			}
			if (o is short)
			{
				return DataAggregate.DataTypeCode.Int16;
			}
			if (o is long)
			{
				return DataAggregate.DataTypeCode.Int64;
			}
			if (o is decimal)
			{
				return DataAggregate.DataTypeCode.Decimal;
			}
			if (o is uint)
			{
				return DataAggregate.DataTypeCode.UInt32;
			}
			if (o is ulong)
			{
				return DataAggregate.DataTypeCode.UInt64;
			}
			if (o is byte)
			{
				return DataAggregate.DataTypeCode.Byte;
			}
			if (o is sbyte)
			{
				return DataAggregate.DataTypeCode.SByte;
			}
			if (o is DateTime)
			{
				return DataAggregate.DataTypeCode.DateTime;
			}
			if (o is char)
			{
				return DataAggregate.DataTypeCode.Char;
			}
			if (o is bool)
			{
				return DataAggregate.DataTypeCode.Boolean;
			}
			if (o is TimeSpan)
			{
				return DataAggregate.DataTypeCode.TimeSpan;
			}
			if (o is DateTimeOffset)
			{
				return DataAggregate.DataTypeCode.DateTimeOffset;
			}
			if (o is float)
			{
				return DataAggregate.DataTypeCode.Single;
			}
			if (o is byte[])
			{
				return DataAggregate.DataTypeCode.ByteArray;
			}
			if (o is SqlGeography)
			{
				return DataAggregate.DataTypeCode.SqlGeography;
			}
			if (o is SqlGeometry)
			{
				return DataAggregate.DataTypeCode.SqlGeometry;
			}
			valid = false;
			if (throwException)
			{
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			return DataAggregate.DataTypeCode.Null;
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000E4023 File Offset: 0x000E2223
		protected static bool IsNull(DataAggregate.DataTypeCode typeCode)
		{
			return typeCode == DataAggregate.DataTypeCode.Null;
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000E4029 File Offset: 0x000E2229
		protected static bool IsVariant(DataAggregate.DataTypeCode typeCode)
		{
			return DataAggregate.DataTypeCode.ByteArray != typeCode;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000E4033 File Offset: 0x000E2233
		protected static void ConvertToDoubleOrDecimal(DataAggregate.DataTypeCode numericType, object numericData, out DataAggregate.DataTypeCode doubleOrDecimalType, out object doubleOrDecimalData)
		{
			if (DataAggregate.DataTypeCode.Decimal == numericType)
			{
				doubleOrDecimalType = DataAggregate.DataTypeCode.Decimal;
				doubleOrDecimalData = numericData;
				return;
			}
			doubleOrDecimalType = DataAggregate.DataTypeCode.Double;
			doubleOrDecimalData = DataTypeUtility.ConvertToDouble(numericType, numericData);
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000E4054 File Offset: 0x000E2254
		protected static object Add(DataAggregate.DataTypeCode xType, object x, DataAggregate.DataTypeCode yType, object y)
		{
			if (DataAggregate.DataTypeCode.Double == xType && DataAggregate.DataTypeCode.Double == yType)
			{
				return (double)x + (double)y;
			}
			if (DataAggregate.DataTypeCode.Decimal == xType && DataAggregate.DataTypeCode.Decimal == yType)
			{
				return (decimal)x + (decimal)y;
			}
			Global.Tracer.Assert(false);
			throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000E40B4 File Offset: 0x000E22B4
		protected static object Square(DataAggregate.DataTypeCode xType, object x)
		{
			if (DataAggregate.DataTypeCode.Double == xType)
			{
				return (double)x * (double)x;
			}
			if (DataAggregate.DataTypeCode.Decimal == xType)
			{
				return (decimal)x * (decimal)x;
			}
			Global.Tracer.Assert(false);
			throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
		}

		// Token: 0x0600335A RID: 13146
		public abstract void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer);

		// Token: 0x0600335B RID: 13147
		public abstract void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader);

		// Token: 0x0600335C RID: 13148
		public abstract void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems);

		// Token: 0x0600335D RID: 13149
		public abstract Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType();

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x0600335E RID: 13150
		public abstract int Size { get; }
	}
}
