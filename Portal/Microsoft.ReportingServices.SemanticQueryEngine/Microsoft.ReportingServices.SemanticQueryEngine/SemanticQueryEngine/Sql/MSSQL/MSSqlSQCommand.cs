using System;
using System.Data.Common;
using System.Security.Permissions;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.MSSQL
{
	// Token: 0x02000023 RID: 35
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class MSSqlSQCommand : SqlSQCommand
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00007153 File Offset: 0x00005353
		public override string LocalizedName
		{
			get
			{
				return "SMQL translator to MSSQL";
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000715A File Offset: 0x0000535A
		internal override SqlBatch CreateSqlBatch(SemanticModel model, DbConnection dbConnection)
		{
			return new MsSqlBatch(model, dbConnection.ServerVersion, base.EnableMathOpCasting);
		}
	}
}
