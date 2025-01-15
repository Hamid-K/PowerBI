using System;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.Oracle
{
	// Token: 0x0200001F RID: 31
	public sealed class OraSqlSQDataReader : SqlSQDataReader
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00006D7C File Offset: 0x00004F7C
		internal OraSqlSQDataReader(CompiledSql compiledSql, bool? dataIsCaseSensitive, IDataReader targetDataReader, ITraceLog traceLog)
			: base(compiledSql, dataIsCaseSensitive, targetDataReader, traceLog)
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006F90 File Offset: 0x00005190
		internal override bool IsAllowedTypeMismatch(DataType modelDataType, Type targetSystemType, out Converter<object, object> convertValueType, out Type newTargetSystemType)
		{
			if (modelDataType == DataType.Boolean && targetSystemType == typeof(decimal))
			{
				convertValueType = new Converter<object, object>(OraSqlSQDataReader.ConvertDecimalToBoolean);
				newTargetSystemType = typeof(bool);
				return true;
			}
			if (modelDataType == DataType.Integer && targetSystemType == typeof(decimal))
			{
				convertValueType = new Converter<object, object>(OraSqlSQDataReader.ConvertDecimalToInteger);
				newTargetSystemType = typeof(long);
				return true;
			}
			if (modelDataType == DataType.Float && targetSystemType == typeof(decimal))
			{
				convertValueType = new Converter<object, object>(OraSqlSQDataReader.ConvertDecimalToDouble);
				newTargetSystemType = typeof(double);
				return true;
			}
			return base.IsAllowedTypeMismatch(modelDataType, targetSystemType, out convertValueType, out newTargetSystemType);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006E0A File Offset: 0x0000500A
		private static object ConvertDecimalToBoolean(object objValue)
		{
			if (objValue is decimal)
			{
				return (decimal)objValue != 0m;
			}
			return objValue;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007044 File Offset: 0x00005244
		private static object ConvertDecimalToInteger(object objValue)
		{
			if (!(objValue is decimal))
			{
				return objValue;
			}
			decimal num = (decimal)objValue;
			if (num >= -2147483648m && num <= 2147483647m)
			{
				return (int)num;
			}
			return (long)num;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000709D File Offset: 0x0000529D
		private static object ConvertDecimalToDouble(object objValue)
		{
			if (objValue is decimal)
			{
				return (double)((decimal)objValue);
			}
			return objValue;
		}
	}
}
