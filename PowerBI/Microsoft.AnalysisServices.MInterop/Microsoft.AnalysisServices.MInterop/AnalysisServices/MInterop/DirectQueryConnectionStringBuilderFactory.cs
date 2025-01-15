using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.Data.Mashup;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200000B RID: 11
	internal static class DirectQueryConnectionStringBuilderFactory
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002548 File Offset: 0x00000748
		public static DirectQueryConnectionStringBuilder GetDirectQueryConnectionStringBuilder(string dataSourceName, DataSourceReference dsr, Credential credential, string serverversion, string dataSourceOptionsJson, string powerBIGlobalServiceFQDN)
		{
			Dictionary<string, object> dictionary = null;
			if (!string.IsNullOrEmpty(dataSourceOptionsJson))
			{
				dictionary = JObject.Parse(dataSourceOptionsJson).ToObject<Dictionary<string, object>>();
			}
			DirectQueryConnectionStringBuilder directQueryConnectionStringBuilder = null;
			string kind = dsr.DataSource.Kind;
			if (!(kind == "SQL"))
			{
				if (!(kind == "PowerBI"))
				{
					if (!(kind == "Teradata"))
					{
						if (!(kind == "Oracle"))
						{
							if (!(kind == "SapHana"))
							{
								if (kind == "AnalysisServices")
								{
									directQueryConnectionStringBuilder = new ASDirectQueryConnectionStringBuilder();
								}
							}
							else
							{
								directQueryConnectionStringBuilder = new RelationalSAPHANADirectQueryConnectionStringBuilder();
							}
						}
						else
						{
							directQueryConnectionStringBuilder = new OracleDirectQueryConnectionStringBuilder();
						}
					}
					else
					{
						directQueryConnectionStringBuilder = new TeradataDirectQueryConnectionStringBuilder();
					}
				}
				else
				{
					directQueryConnectionStringBuilder = new DataflowsDirectQueryConnectionStringBuilder(powerBIGlobalServiceFQDN);
				}
			}
			else
			{
				directQueryConnectionStringBuilder = new SQLDirectQueryConnectionStringBuilder(serverversion);
			}
			if (directQueryConnectionStringBuilder != null)
			{
				directQueryConnectionStringBuilder.Init(dsr, dataSourceName, credential, dictionary);
			}
			return directQueryConnectionStringBuilder;
		}
	}
}
