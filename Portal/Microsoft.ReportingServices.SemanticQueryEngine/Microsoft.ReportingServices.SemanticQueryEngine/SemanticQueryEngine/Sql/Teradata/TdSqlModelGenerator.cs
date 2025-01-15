using System;
using System.Data;
using System.Security.Permissions;
using Microsoft.ReportingServices.Modeling.ModelGeneration;
using Microsoft.ReportingServices.Modeling.ModelGeneration.Teradata;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.Teradata
{
	// Token: 0x0200001A RID: 26
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class TdSqlModelGenerator : SqlModelGenerator
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00006C91 File Offset: 0x00004E91
		internal override SqlDsvGenerator GetSqlDsvGenerator(IDbConnection connection)
		{
			return new TdSqlDsvGenerator(connection);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006C99 File Offset: 0x00004E99
		internal override SqlDsvStatisticsProvider GetSqlDsvStatisticsProvider(IDbConnection connection)
		{
			return new TdSqlDsvStatisticsProvider(connection);
		}
	}
}
