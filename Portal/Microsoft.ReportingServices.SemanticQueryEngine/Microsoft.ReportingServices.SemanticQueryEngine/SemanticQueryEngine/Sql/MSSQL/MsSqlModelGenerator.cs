﻿using System;
using System.Data;
using System.Security.Permissions;
using Microsoft.ReportingServices.Modeling.ModelGeneration;
using Microsoft.ReportingServices.Modeling.ModelGeneration.MSSQL;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.MSSQL
{
	// Token: 0x02000024 RID: 36
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class MsSqlModelGenerator : SqlModelGenerator
	{
		// Token: 0x06000175 RID: 373 RVA: 0x0000716E File Offset: 0x0000536E
		internal override SqlDsvGenerator GetSqlDsvGenerator(IDbConnection connection)
		{
			return new MsSqlDsvGenerator(connection);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007176 File Offset: 0x00005376
		internal override SqlDsvStatisticsProvider GetSqlDsvStatisticsProvider(IDbConnection connection)
		{
			return new MsSqlDsvStatisticsProvider(connection);
		}
	}
}
