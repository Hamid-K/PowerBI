using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.Data.Mashup;
using Microsoft.Data.Mashup.Preview;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000009 RID: 9
	internal class Credential
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002048 File Offset: 0x00000248
		public ImpersonationMode ImpersonationMode
		{
			get
			{
				Credential.ASWindowsAuthKind aswindowsAuthKind;
				if (!Enum.TryParse<Credential.ASWindowsAuthKind>(this.AuthenticationKind, out aswindowsAuthKind))
				{
					return ImpersonationMode.Default;
				}
				switch (aswindowsAuthKind)
				{
				case Credential.ASWindowsAuthKind.Windows:
					return ImpersonationMode.Account;
				case Credential.ASWindowsAuthKind.Unattended:
					return ImpersonationMode.UnattendedAccount;
				case Credential.ASWindowsAuthKind.ServiceAccount:
					return ImpersonationMode.ServiceAccount;
				case Credential.ASWindowsAuthKind.CurrentUser:
					return ImpersonationMode.CurrentUser;
				case Credential.ASWindowsAuthKind.KerberosS4U:
					return ImpersonationMode.KerberosS4U;
				default:
					throw new NotImplementedException(aswindowsAuthKind.ToString());
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000209E File Offset: 0x0000029E
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020A6 File Offset: 0x000002A6
		public string AuthenticationKind { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020AF File Offset: 0x000002AF
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020B7 File Offset: 0x000002B7
		public IDictionary<string, object> AuthenticationProperties
		{
			get
			{
				return this.authenticationProperties;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.authenticationProperties = new Dictionary<string, object>(value, StringComparer.OrdinalIgnoreCase);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D8 File Offset: 0x000002D8
		public void SetOrRemoveProperty(string key, string value)
		{
			if (value != null)
			{
				this.authenticationProperties[key] = value;
				return;
			}
			this.authenticationProperties.Remove(key);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F8 File Offset: 0x000002F8
		public string GetPropertyOrNull(string key)
		{
			object obj;
			if (this.authenticationProperties.TryGetValue(key, out obj))
			{
				return (string)obj;
			}
			return null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000211D File Offset: 0x0000031D
		public DataSourceSetting ToDss()
		{
			return DataSourceSettingsHelper.Create(this.AuthenticationKind, this.authenticationProperties, true);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002131 File Offset: 0x00000331
		public void StripKindAndPath()
		{
			this.authenticationProperties.Remove("kind");
			this.authenticationProperties.Remove("path");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002158 File Offset: 0x00000358
		public static Credential FromJson(string credentialJson, string dataSourceName = "")
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new Credential.CredentialJsonConverter()
			});
			Credential credential = null;
			try
			{
				credential = javaScriptSerializer.Deserialize<Credential>(credentialJson);
			}
			catch (Exception ex)
			{
				throw EngineException.PFE_M_ENGINE_CREDENTIAL_JSON_IS_MALFORMED(dataSourceName, ex.Message, ex);
			}
			return credential;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021AC File Offset: 0x000003AC
		public string ToJson()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new Credential.CredentialJsonConverter()
			});
			return javaScriptSerializer.Serialize(this);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021DC File Offset: 0x000003DC
		public static Credential FromDataSourceProperties(ImpersonationMode impersonationMode, string dsUsername, string dsPassword)
		{
			string text = null;
			string text2 = null;
			Credential.ASWindowsAuthKind aswindowsAuthKind;
			switch (impersonationMode)
			{
			case ImpersonationMode.Account:
				text = dsUsername;
				if (!string.IsNullOrEmpty(dsPassword))
				{
					aswindowsAuthKind = Credential.ASWindowsAuthKind.Windows;
					text2 = dsPassword;
					goto IL_0055;
				}
				aswindowsAuthKind = Credential.ASWindowsAuthKind.KerberosS4U;
				goto IL_0055;
			case ImpersonationMode.CurrentUser:
				aswindowsAuthKind = Credential.ASWindowsAuthKind.CurrentUser;
				goto IL_0055;
			case ImpersonationMode.ServiceAccount:
				aswindowsAuthKind = Credential.ASWindowsAuthKind.ServiceAccount;
				goto IL_0055;
			case ImpersonationMode.UnattendedAccount:
				aswindowsAuthKind = Credential.ASWindowsAuthKind.Unattended;
				goto IL_0055;
			}
			throw new NotImplementedException(impersonationMode.ToString());
			IL_0055:
			Credential credential = new Credential();
			credential.AuthenticationKind = aswindowsAuthKind.ToString();
			credential.SetOrRemoveProperty("Username", text);
			credential.SetOrRemoveProperty("Password", text2);
			return credential;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002270 File Offset: 0x00000470
		public string ToMJson(string unattendedUsername, string unattendedPassword, string dataSourceName)
		{
			string text = null;
			string text2 = null;
			Credential.IdentitySource identitySource;
			switch (this.ImpersonationMode)
			{
			case ImpersonationMode.Default:
				return this.ToJson();
			case ImpersonationMode.Account:
				identitySource = Credential.IdentitySource.Explicit;
				text = this.GetPropertyOrNull("Username") ?? "";
				text2 = this.GetPropertyOrNull("Password") ?? "";
				if (string.IsNullOrEmpty(text))
				{
					throw EngineException.PFE_M_ENGINE_CREDENTIAL_MISSING_USERNAME(dataSourceName);
				}
				goto IL_00D0;
			case ImpersonationMode.CurrentUser:
				identitySource = Credential.IdentitySource.Thread;
				goto IL_00D0;
			case ImpersonationMode.ServiceAccount:
				identitySource = Credential.IdentitySource.Process;
				goto IL_00D0;
			case ImpersonationMode.UnattendedAccount:
				identitySource = Credential.IdentitySource.Explicit;
				text = unattendedUsername ?? "";
				text2 = unattendedPassword ?? "";
				goto IL_00D0;
			case ImpersonationMode.KerberosS4U:
				identitySource = Credential.IdentitySource.Explicit;
				text = this.GetPropertyOrNull("Username");
				if (string.IsNullOrEmpty(text))
				{
					throw EngineException.PFE_M_ENGINE_CREDENTIAL_MISSING_USERNAME(dataSourceName);
				}
				goto IL_00D0;
			}
			throw new NotImplementedException(this.ImpersonationMode.ToString());
			IL_00D0:
			Credential credential = Credential.FromJson(this.ToJson(), "");
			credential.AuthenticationKind = "Windows";
			credential.SetOrRemoveProperty("IdentitySource", identitySource.ToString());
			credential.SetOrRemoveProperty("Username", text);
			credential.SetOrRemoveProperty("Password", text2);
			return credential.ToJson();
		}

		// Token: 0x04000053 RID: 83
		public const string authCredProperty = "ConnectionString";

		// Token: 0x04000054 RID: 84
		public const string authTypeProperty = "AuthenticationKind";

		// Token: 0x04000055 RID: 85
		public const string credUsernameProperty = "Username";

		// Token: 0x04000056 RID: 86
		public const string credPasswordProperty = "Password";

		// Token: 0x04000057 RID: 87
		public const string AuthenticationKindKeyword = "AuthenticationKind";

		// Token: 0x04000058 RID: 88
		public const string IdentitySourceKeyword = "IdentitySource";

		// Token: 0x04000059 RID: 89
		private const string ResourceKindKeyword = "kind";

		// Token: 0x0400005A RID: 90
		private const string ResourcePathKeyword = "path";

		// Token: 0x0400005B RID: 91
		private IDictionary<string, object> authenticationProperties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x02000035 RID: 53
		public enum ASWindowsAuthKind
		{
			// Token: 0x0400124A RID: 4682
			Windows,
			// Token: 0x0400124B RID: 4683
			Unattended,
			// Token: 0x0400124C RID: 4684
			ServiceAccount,
			// Token: 0x0400124D RID: 4685
			CurrentUser,
			// Token: 0x0400124E RID: 4686
			KerberosS4U
		}

		// Token: 0x02000036 RID: 54
		public static class AuthenticationKinds
		{
			// Token: 0x0400124F RID: 4687
			public const string Windows = "Windows";

			// Token: 0x04001250 RID: 4688
			public const string Anonymous = "Anonymous";
		}

		// Token: 0x02000037 RID: 55
		public enum IdentitySource
		{
			// Token: 0x04001252 RID: 4690
			Thread,
			// Token: 0x04001253 RID: 4691
			Process,
			// Token: 0x04001254 RID: 4692
			Explicit
		}

		// Token: 0x02000038 RID: 56
		private class CredentialJsonConverter : JavaScriptConverter
		{
			// Token: 0x06002100 RID: 8448 RVA: 0x0004DC4C File Offset: 0x0004BE4C
			public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
			{
				Credential credential = (Credential)obj;
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				if (credential.AuthenticationKind != null)
				{
					dictionary.Add("AuthenticationKind", credential.AuthenticationKind);
				}
				foreach (KeyValuePair<string, object> keyValuePair in credential.AuthenticationProperties)
				{
					dictionary.Add(keyValuePair);
				}
				return dictionary;
			}

			// Token: 0x06002101 RID: 8449 RVA: 0x0004DCC0 File Offset: 0x0004BEC0
			public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
			{
				string text = null;
				IDictionary<string, object> dictionary2 = new Dictionary<string, object>();
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					if ("AuthenticationKind".Equals(keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
					{
						text = Credential.CredentialJsonConverter.Get<string>(keyValuePair.Value);
					}
					else
					{
						dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				return new Credential
				{
					AuthenticationKind = text,
					AuthenticationProperties = dictionary2
				};
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06002102 RID: 8450 RVA: 0x0004DD54 File Offset: 0x0004BF54
			public override IEnumerable<Type> SupportedTypes
			{
				get
				{
					return new Type[] { typeof(Credential) };
				}
			}

			// Token: 0x06002103 RID: 8451 RVA: 0x0004DD69 File Offset: 0x0004BF69
			private static T Get<T>(object value) where T : class
			{
				T t = value as T;
				if (t == null)
				{
					throw new InvalidOperationException("Credential JSON is malformed.");
				}
				return t;
			}
		}
	}
}
