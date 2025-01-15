using System;
using System.Data.Common;
using System.Security.Permissions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQLADW;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.MSSQLADW
{
	// Token: 0x02000020 RID: 32
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class MSSqlAdwSQCommand : SqlSQCommand
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000070C2 File Offset: 0x000052C2
		public override string LocalizedName
		{
			get
			{
				return "SMQL translator to MSSQLADW";
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000070C9 File Offset: 0x000052C9
		internal override SqlBatch CreateSqlBatch(SemanticModel model, DbConnection dbConnection)
		{
			return new MsSqlAdwBatch(model, dbConnection.ServerVersion, base.EnableMathOpCasting);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000070DD File Offset: 0x000052DD
		internal override SqlSQDataReader CreateSqlSQDataReader(CompiledSql compiledSql, bool? dataIsCaseSensitive, IDataReader targetDataReader, ITraceLog traceLog)
		{
			return new MSSqlAdwSQDataReader(compiledSql, dataIsCaseSensitive, targetDataReader, traceLog);
		}
	}
}
