using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.Teradata
{
	// Token: 0x0200001C RID: 28
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class TdSqlSQDataReader : SqlSQDataReader
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00006D7C File Offset: 0x00004F7C
		internal TdSqlSQDataReader(CompiledSql compiledSql, bool? dataIsCaseSensitive, IDataReader targetDataReader, ITraceLog traceLog)
			: base(compiledSql, dataIsCaseSensitive, targetDataReader, traceLog)
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006D8C File Offset: 0x00004F8C
		internal override bool IsAllowedTypeMismatch(DataType modelDataType, Type targetSystemType, out Converter<object, object> convertValueType, out Type newTargetSystemType)
		{
			if (modelDataType == DataType.Boolean && targetSystemType == typeof(decimal))
			{
				convertValueType = new Converter<object, object>(TdSqlSQDataReader.ConvertDecimalToBoolean);
				newTargetSystemType = typeof(bool);
				return true;
			}
			if (modelDataType == DataType.Boolean && targetSystemType == typeof(int))
			{
				convertValueType = new Converter<object, object>(TdSqlSQDataReader.ConvertIntToBoolean);
				newTargetSystemType = typeof(bool);
				return true;
			}
			return base.IsAllowedTypeMismatch(modelDataType, targetSystemType, out convertValueType, out newTargetSystemType);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006E0A File Offset: 0x0000500A
		private static object ConvertDecimalToBoolean(object objValue)
		{
			if (objValue is decimal)
			{
				return (decimal)objValue != 0m;
			}
			return objValue;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006E2B File Offset: 0x0000502B
		private static object ConvertIntToBoolean(object objValue)
		{
			if (objValue is int)
			{
				return (int)objValue != 0;
			}
			return objValue;
		}
	}
}
