using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.BusinessIntelligence;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200004C RID: 76
	internal interface ILuciaSessionFactory : IDisposable
	{
		// Token: 0x060001AF RID: 431
		ILuciaSession CreateLuciaSession(IConnectionFactory connectionFactory, IConnectionPool connectionPool, IDataSourceInfo dataSourceInfo, Func<string> getConceptualSchemaXml, Func<string, IReadOnlyDictionary<string, object>, DataSet> getSchemaDataSet, LuciaSessionParameters luciaSessionParameters, FeatureSwitches featureSwitches);
	}
}
