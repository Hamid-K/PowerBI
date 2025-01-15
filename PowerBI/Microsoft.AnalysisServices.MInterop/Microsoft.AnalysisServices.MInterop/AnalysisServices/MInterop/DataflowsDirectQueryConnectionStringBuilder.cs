using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.Data.Mashup;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000011 RID: 17
	internal sealed class DataflowsDirectQueryConnectionStringBuilder : DirectQueryConnectionStringBuilder
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002E5F File Offset: 0x0000105F
		public DataflowsDirectQueryConnectionStringBuilder(string powerBIGlobalServiceFQDN)
		{
			this.powerBIGlobalServiceFQDN = powerBIGlobalServiceFQDN;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E6E File Offset: 0x0000106E
		public override void AddCredential(Credential credential)
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E70 File Offset: 0x00001070
		public override void AddDataSource(DataSourceReference dsr)
		{
			this["Server"] = dsr.DataSource.Path;
			JObject jobject = JsonConvert.DeserializeObject<JObject>(dsr.ToJson());
			if (string.CompareOrdinal(jobject.Value<string>("protocol"), "powerbi-dataflows") != 0)
			{
				return;
			}
			JObject jobject2 = jobject.Value<JObject>("address");
			if (jobject2 == null)
			{
				throw EngineException.PFE_M_ENGINE_DQ_INVALID_DATA_SOURCE_FORMAT(base.DataSourceName);
			}
			this["WorkspaceId"] = jobject2.Value<string>("workspace");
			this["DataflowId"] = jobject2.Value<string>("dataflow");
			if (!string.IsNullOrEmpty(this.powerBIGlobalServiceFQDN))
			{
				this["PowerBiEndpoint"] = this.powerBIGlobalServiceFQDN;
				return;
			}
			object obj;
			if (DataflowsDirectQueryConnectionStringBuilder.TryGetEnvironmentConfiguration("PowerBiUri", out obj))
			{
				this["PowerBiEndpoint"] = obj as string;
				return;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F3D File Offset: 0x0000113D
		protected override void AddOptions(Dictionary<string, object> options)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F3F File Offset: 0x0000113F
		protected override void InferProviderAndDriver()
		{
			base.ManagedProvider = "Microsoft.PowerBI.Dataflows";
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F4C File Offset: 0x0000114C
		private static bool TryGetEnvironmentConfiguration(string name, out object value)
		{
			string text = name + "Config";
			MashupConnectionStringBuilder mashupConnectionStringBuilder = new MashupConnectionStringBuilder
			{
				Mashup = string.Concat(new string[] { "\r\n[Requires = [Environment= \"[0.0,)\", Core = \"[2.0,)\"]]\r\nsection Section1;\r\nshared ", text, " = Environment.Configuration()[", name, "];" })
			};
			try
			{
				using (MashupConnection mashupConnection = new MashupConnection(mashupConnectionStringBuilder.ToString()))
				{
					mashupConnection.Open();
					using (MashupCommand mashupCommand = mashupConnection.CreateCommand())
					{
						mashupCommand.CommandText = text;
						using (MashupReader mashupReader = mashupCommand.ExecuteReader())
						{
							if (mashupReader.Read())
							{
								value = mashupReader.GetValue(0);
								return true;
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			value = null;
			return false;
		}

		// Token: 0x04000065 RID: 101
		private const string dataflowsProtocolName = "powerbi-dataflows";

		// Token: 0x04000066 RID: 102
		private const string workspaceIdJsonPropertyName = "workspace";

		// Token: 0x04000067 RID: 103
		private const string dataflowIdJsonPropertyName = "dataflow";

		// Token: 0x04000068 RID: 104
		private readonly string powerBIGlobalServiceFQDN;
	}
}
