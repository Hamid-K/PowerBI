using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.AnalysisServices.PlatformHost;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000013 RID: 19
	internal class DbConnectionStringBuilderEx : DbConnectionStringBuilder
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00003090 File Offset: 0x00001290
		private bool LoadDMTSConnectionDetails()
		{
			object obj;
			if (this.TryGetValue("DMTSConnectionDetails", out obj))
			{
				string @string = Encoding.UTF8.GetString(Convert.FromBase64String((string)obj));
				this.DMTSConnectionDetails = this.Serializer.Deserialize<Dictionary<string, object>>(@string);
				return true;
			}
			return false;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000030D7 File Offset: 0x000012D7
		public bool IsDMTSConnection()
		{
			return this.DMTSConnectionDetails != null || this.LoadDMTSConnectionDetails();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000030EC File Offset: 0x000012EC
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000031C0 File Offset: 0x000013C0
		public string GatewayConnectionString
		{
			get
			{
				if (!this.IsDMTSConnection())
				{
					return null;
				}
				if (this.DMTSConnectionDetails.ContainsKey("discoverMonikerSystemResponse") && this.DMTSConnectionDetails["discoverMonikerSystemResponse"] != null)
				{
					return (string)((Dictionary<string, object>)((ArrayList)((Dictionary<string, object>)this.DMTSConnectionDetails["discoverMonikerSystemResponse"])["monikers"])[0])["connectionString"];
				}
				if (this.DMTSConnectionDetails.ContainsKey("rawTransferConnectionString") && this.DMTSConnectionDetails["rawTransferConnectionString"] != null)
				{
					return (string)((Dictionary<string, object>)this.DMTSConnectionDetails["rawTransferConnectionString"])["connectionString"];
				}
				throw EngineException.PFE_INVALIDARG(new Exception("Cannot read invalid DMTS connection string."));
			}
			set
			{
				if (!this.IsDMTSConnection())
				{
					throw EngineException.PFE_INVALIDARG();
				}
				if (this.DMTSConnectionDetails.ContainsKey("discoverMonikerSystemResponse") && this.DMTSConnectionDetails["discoverMonikerSystemResponse"] != null)
				{
					((Dictionary<string, object>)((ArrayList)((Dictionary<string, object>)this.DMTSConnectionDetails["discoverMonikerSystemResponse"])["monikers"])[0])["connectionString"] = value;
				}
				else
				{
					if (!this.DMTSConnectionDetails.ContainsKey("rawTransferConnectionString") || this.DMTSConnectionDetails["rawTransferConnectionString"] == null)
					{
						throw EngineException.PFE_INVALIDARG(new Exception("Cannot write invalid DMTS connection string."));
					}
					((Dictionary<string, object>)this.DMTSConnectionDetails["rawTransferConnectionString"])["connectionString"] = value;
				}
				string text = this.Serializer.Serialize(this.DMTSConnectionDetails);
				this["DMTSConnectionDetails"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
			}
		}

		// Token: 0x04000069 RID: 105
		private const string DMTSConnectionDetailsToken = "DMTSConnectionDetails";

		// Token: 0x0400006A RID: 106
		private const string DiscoverMonikerSystemResponseToken = "discoverMonikerSystemResponse";

		// Token: 0x0400006B RID: 107
		private const string RawTransferConnectionStringToken = "rawTransferConnectionString";

		// Token: 0x0400006C RID: 108
		private const string MonikersToken = "monikers";

		// Token: 0x0400006D RID: 109
		private const string ConnectionStringToken = "connectionString";

		// Token: 0x0400006E RID: 110
		private readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

		// Token: 0x0400006F RID: 111
		private Dictionary<string, object> DMTSConnectionDetails;
	}
}
