using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AnalysisServices.PlatformHost;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200000D RID: 13
	internal sealed class SQLDirectQueryConnectionStringBuilder : DirectQueryConnectionStringBuilder
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002866 File Offset: 0x00000A66
		public SQLDirectQueryConnectionStringBuilder(string serverversion)
		{
			this.version = serverversion;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002875 File Offset: 0x00000A75
		public override void AddCredential(Credential credential)
		{
			if (credential != null)
			{
				base.AddCredentialCommon(credential);
				base.AddEncryptionCommon(credential);
			}
			this.AddEncryptionOptionForTrustedSqlServers(credential);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002890 File Offset: 0x00000A90
		protected override void AddOptions(Dictionary<string, object> options)
		{
			object obj;
			if (options != null && options.TryGetValue("MultiSubnetFailover", out obj) && obj.ToString().Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				this["MultiSubnetFailover"] = "True";
				this["ApplicationIntent"] = "ReadOnly";
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028E2 File Offset: 0x00000AE2
		protected override void InferProviderAndDriver()
		{
			base.ManagedProvider = ((MInteropHelperImpl.UseMicrosoftDataSqlClient && SQLDirectQueryConnectionStringBuilder.microsoftDataSqlClientProviderAvaialable.Value) ? "Microsoft.Data.SqlClient" : "System.Data.SqlClient");
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000290C File Offset: 0x00000B0C
		protected override void AddEncryptionOptionForTrustedSqlServers(Credential credential)
		{
			object obj;
			if (credential == null || !credential.AuthenticationProperties.TryGetValue("EncryptConnection", out obj) || obj.ToString().Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				string environmentVariable = Environment.GetEnvironmentVariable("PBI_SQL_TRUSTED_SERVERS", EnvironmentVariableTarget.Process);
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					bool flag = false;
					string text = (string)this["Data Source"];
					foreach (string text2 in from s in environmentVariable.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
						select s.Trim())
					{
						if (string.Equals(text, text2, StringComparison.OrdinalIgnoreCase) || (text2.Contains('*') && Regex.IsMatch(text, "^" + Regex.Escape(text2).Replace("\\*", ".*") + "$", RegexOptions.IgnoreCase)))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						this["Encrypt"] = true;
						this["TrustServerCertificate"] = true;
					}
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002A4C File Offset: 0x00000C4C
		private bool IsSQLAzure()
		{
			return this.version != null && this.version.Contains("SQL Azure");
		}

		// Token: 0x04000063 RID: 99
		private static Lazy<bool> microsoftDataSqlClientProviderAvaialable = new Lazy<bool>(delegate
		{
			bool flag = false;
			try
			{
				flag = DbProviderFactories.GetFactory("Microsoft.Data.SqlClient") != null;
			}
			catch (Exception ex)
			{
				IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
				if (engineTracer != null)
				{
					engineTracer.LogMessage("Trying to activate Microsoft.Data.SqlClient resulted in error  " + ex.Message + ". Falling back to System.Data.SqlClient.");
				}
			}
			return flag;
		});

		// Token: 0x04000064 RID: 100
		private string version;
	}
}
