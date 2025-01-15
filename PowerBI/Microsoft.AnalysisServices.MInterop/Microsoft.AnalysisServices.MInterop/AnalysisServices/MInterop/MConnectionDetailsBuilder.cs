using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Web.Script.Serialization;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200002E RID: 46
	internal sealed class MConnectionDetailsBuilder
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00009664 File Offset: 0x00007864
		public MConnectionDetailsBuilder(string connectionString, string provider)
		{
			this.connectionString = connectionString;
			this.provider = provider;
			this.address.Add("options", this.options);
			string text = (this.IsAdoNet() ? this.ParseConnectionStringForAdoDotNet() : this.ParseConnectionStringForOleDB());
			this.connectionDetailsScheme = new Dictionary<string, object>
			{
				{ "address", this.address },
				{ "protocol", text }
			};
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000971A File Offset: 0x0000791A
		public override string ToString()
		{
			return new JavaScriptSerializer().Serialize(this.connectionDetailsScheme);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000972C File Offset: 0x0000792C
		public Credential GetCredential()
		{
			if (this.credentials.Count == 0)
			{
				return null;
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.credentials)
			{
				if (MConnectionDetailsBuilder.winAuthProperties.Contains(keyValuePair.Key))
				{
					if (!MConnectionDetailsBuilder.winAuthTrueValues.Contains(keyValuePair.Value.ToString()))
					{
						break;
					}
					string text = null;
					string text2 = null;
					foreach (KeyValuePair<string, object> keyValuePair2 in this.credentials)
					{
						if (MConnectionDetailsBuilder.usernameProperties.Contains(keyValuePair2.Key))
						{
							text = keyValuePair2.Value.ToString();
						}
						if (MConnectionDetailsBuilder.passwordProperties.Contains(keyValuePair2.Key))
						{
							text2 = keyValuePair2.Value.ToString();
						}
					}
					if (!string.IsNullOrEmpty(text))
					{
						Credential credential = new Credential();
						credential.AuthenticationKind = Credential.ASWindowsAuthKind.Windows.ToString();
						credential.SetOrRemoveProperty("Username", text);
						credential.SetOrRemoveProperty("Password", text2);
						return credential;
					}
					return null;
				}
			}
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder(this.connectionDetailsScheme["protocol"].ToString() == "odbc");
			foreach (KeyValuePair<string, object> keyValuePair3 in this.credentials)
			{
				dbConnectionStringBuilder.Add(keyValuePair3.Key, keyValuePair3.Value);
			}
			Credential credential2 = new Credential();
			credential2.AuthenticationKind = "Anonymous";
			credential2.AuthenticationProperties["ConnectionString"] = dbConnectionStringBuilder.ConnectionString;
			return credential2;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009950 File Offset: 0x00007B50
		private bool IsAdoNet()
		{
			return !string.IsNullOrEmpty(this.provider);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00009960 File Offset: 0x00007B60
		private bool IsFakeOdbc(OleDbConnectionStringBuilder builder)
		{
			return MInteropHelperImpl.IsOLEDBForODBCProvider(builder.Provider);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000996D File Offset: 0x00007B6D
		private static bool IsCredentialProperty(string key)
		{
			return MConnectionDetailsBuilder.usernameProperties.Contains(key) || MConnectionDetailsBuilder.passwordProperties.Contains(key) || MConnectionDetailsBuilder.winAuthProperties.Contains(key);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00009998 File Offset: 0x00007B98
		private void AppendOptionsFromConnStringBuilder(DbConnectionStringBuilder builder, HashSet<string> skippedKeys = null)
		{
			foreach (object obj in builder.Keys)
			{
				string text = (string)obj;
				if (builder.ShouldSerialize(text) && (skippedKeys == null || !skippedKeys.Contains(text)) && !MConnectionDetailsBuilder.forbiddenAuthOptions.Contains(text))
				{
					if (MConnectionDetailsBuilder.IsCredentialProperty(text))
					{
						this.credentials[text] = builder[text];
					}
					else
					{
						this.options[text] = builder[text];
					}
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00009A3C File Offset: 0x00007C3C
		private string TrimCurlyBraces(string str)
		{
			if (string.IsNullOrEmpty(str) || str.Length < 2)
			{
				return str;
			}
			if (str[0] == '{' && str[str.Length - 1] == '}')
			{
				return str.Substring(1, str.Length - 2);
			}
			return str;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00009A8C File Offset: 0x00007C8C
		private string ParseConnectionStringForAdoDotNet()
		{
			this.AppendOptionsFromConnStringBuilder(new DbConnectionStringBuilder(false)
			{
				ConnectionString = this.connectionString
			}, null);
			this.address.Add("provider", this.provider);
			return "ado-dot-net";
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009AD0 File Offset: 0x00007CD0
		private string ParseConnectionStringForFakeODBC()
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder(false);
			dbConnectionStringBuilder.ConnectionString = this.connectionString;
			this.AppendOptionsFromConnStringBuilder(dbConnectionStringBuilder, MConnectionDetailsBuilder.strippedOledbOptionsForODBC);
			if (dbConnectionStringBuilder.ContainsKey("Extended Properties"))
			{
				string text = dbConnectionStringBuilder["Extended Properties"].ToString();
				this.AppendOptionsFromConnStringBuilder(new OdbcConnectionStringBuilder(text), MConnectionDetailsBuilder.forbiddenODBCOptions);
			}
			foreach (string text2 in new string[] { "driver", "dsn" })
			{
				if (this.options.ContainsKey(text2))
				{
					this.options[text2] = this.TrimCurlyBraces(this.options[text2].ToString());
				}
			}
			return "odbc";
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00009B90 File Offset: 0x00007D90
		private string ParseConnectionStringForOleDB()
		{
			OleDbConnectionStringBuilder oleDbConnectionStringBuilder = new OleDbConnectionStringBuilder(this.connectionString);
			if (this.IsFakeOdbc(oleDbConnectionStringBuilder))
			{
				return this.ParseConnectionStringForFakeODBC();
			}
			this.AppendOptionsFromConnStringBuilder(oleDbConnectionStringBuilder, null);
			return "ole-db";
		}

		// Token: 0x04000130 RID: 304
		private static readonly StringComparer stringComparer = StringComparer.InvariantCultureIgnoreCase;

		// Token: 0x04000131 RID: 305
		private readonly Dictionary<string, object> address = new Dictionary<string, object>(MConnectionDetailsBuilder.stringComparer);

		// Token: 0x04000132 RID: 306
		private readonly Dictionary<string, object> options = new Dictionary<string, object>(MConnectionDetailsBuilder.stringComparer);

		// Token: 0x04000133 RID: 307
		private readonly Dictionary<string, object> credentials = new Dictionary<string, object>(MConnectionDetailsBuilder.stringComparer);

		// Token: 0x04000134 RID: 308
		private readonly Dictionary<string, object> connectionDetailsScheme = new Dictionary<string, object>(MConnectionDetailsBuilder.stringComparer);

		// Token: 0x04000135 RID: 309
		private readonly string connectionString;

		// Token: 0x04000136 RID: 310
		private readonly string provider;

		// Token: 0x04000137 RID: 311
		private const string protocolAdoDotNet = "ado-dot-net";

		// Token: 0x04000138 RID: 312
		private const string protocolOleDb = "ole-db";

		// Token: 0x04000139 RID: 313
		private const string protocolOdbc = "odbc";

		// Token: 0x0400013A RID: 314
		private static readonly HashSet<string> strippedOledbOptionsForODBC = new HashSet<string>(MConnectionDetailsBuilder.stringComparer) { "Extended Properties", "Provider" };

		// Token: 0x0400013B RID: 315
		private static readonly HashSet<string> forbiddenAuthOptions = new HashSet<string>(MConnectionDetailsBuilder.stringComparer) { "Persist Security Info" };

		// Token: 0x0400013C RID: 316
		private static readonly HashSet<string> forbiddenODBCOptions = new HashSet<string>(MConnectionDetailsBuilder.stringComparer) { "FileDsn", "SaveFile" };

		// Token: 0x0400013D RID: 317
		private static readonly HashSet<string> usernameProperties = new HashSet<string>(MConnectionDetailsBuilder.stringComparer) { "uid", "userid", "username", "user", "user id" };

		// Token: 0x0400013E RID: 318
		private static readonly HashSet<string> passwordProperties = new HashSet<string>(MConnectionDetailsBuilder.stringComparer) { "pwd", "password" };

		// Token: 0x0400013F RID: 319
		private static readonly HashSet<string> winAuthProperties = new HashSet<string>(MConnectionDetailsBuilder.stringComparer) { "Integrated Security", "Trusted_Connection" };

		// Token: 0x04000140 RID: 320
		private static readonly HashSet<string> winAuthTrueValues = new HashSet<string>(MConnectionDetailsBuilder.stringComparer) { "SSPI", "yes", "true" };
	}
}
