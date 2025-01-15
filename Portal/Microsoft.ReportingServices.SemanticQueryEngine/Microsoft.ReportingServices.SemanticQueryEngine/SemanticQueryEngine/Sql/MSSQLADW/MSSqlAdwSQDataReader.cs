using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.MSSQLADW
{
	// Token: 0x02000021 RID: 33
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class MSSqlAdwSQDataReader : SqlSQDataReader
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00006D7C File Offset: 0x00004F7C
		internal MSSqlAdwSQDataReader(CompiledSql compiledSql, bool? dataIsCaseSensitive, IDataReader targetDataReader, ITraceLog traceLog)
			: base(compiledSql, dataIsCaseSensitive, targetDataReader, traceLog)
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000070E9 File Offset: 0x000052E9
		internal override bool IsAllowedTypeMismatch(DataType modelDataType, Type targetSystemType, out Converter<object, object> convertValueType, out Type newTargetSystemType)
		{
			if (modelDataType == DataType.Boolean && targetSystemType == typeof(byte))
			{
				convertValueType = new Converter<object, object>(MSSqlAdwSQDataReader.ConvertByteToBoolean);
				newTargetSystemType = typeof(bool);
				return true;
			}
			return base.IsAllowedTypeMismatch(modelDataType, targetSystemType, out convertValueType, out newTargetSystemType);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007129 File Offset: 0x00005329
		private static object ConvertByteToBoolean(object objValue)
		{
			if (objValue is byte)
			{
				return (byte)objValue > 0;
			}
			return objValue;
		}
	}
}
