using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Resources;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012F0 RID: 4848
	internal static class CredentialConversion
	{
		// Token: 0x06008052 RID: 32850 RVA: 0x001B5C0C File Offset: 0x001B3E0C
		public static bool TryConvertToRecord(IEngine engine, IEngineHost engineHost, IResource resource, ResourceCredentialCollection credential, out RecordValue credentialRecord)
		{
			RecordBuilder recordBuilder = new RecordBuilder(10);
			bool? flag = null;
			Dictionary<string, string> dictionary = null;
			string text = null;
			List<IResourceCredential> list = ((credential == null) ? new List<IResourceCredential>() : credential.ToList<IResourceCredential>());
			for (int i = list.Count - 1; i >= 0; i--)
			{
				EncryptedConnectionAdornment encryptedConnectionAdornment = list[i] as EncryptedConnectionAdornment;
				if (encryptedConnectionAdornment != null)
				{
					list.RemoveAt(i);
					flag = new bool?(encryptedConnectionAdornment.RequireEncryption);
				}
				else
				{
					ConnectionStringPropertiesAdornment connectionStringPropertiesAdornment = list[i] as ConnectionStringPropertiesAdornment;
					if (connectionStringPropertiesAdornment != null)
					{
						list.RemoveAt(i);
						dictionary = connectionStringPropertiesAdornment.Properties;
					}
					else
					{
						ConnectionStringAdornment connectionStringAdornment = list[i] as ConnectionStringAdornment;
						if (connectionStringAdornment != null)
						{
							list.RemoveAt(i);
							text = connectionStringAdornment.ConnectionString;
						}
					}
				}
			}
			if (list.Count == 0)
			{
				recordBuilder.Add("AuthenticationKind", TextValue.New("Implicit"), TypeValue.Text);
			}
			else if (list[0] is FeedKeyCredential)
			{
				FeedKeyCredential feedKeyCredential = (FeedKeyCredential)list[0];
				recordBuilder.Add("AuthenticationKind", TextValue.New("Key"), TypeValue.Text);
				recordBuilder.Add("Password", TextValue.New(feedKeyCredential.Password), TypeValue.Text);
				recordBuilder.Add("Key", TextValue.New(feedKeyCredential.Password), TypeValue.Text);
			}
			else if (list[0] is UsernamePasswordCredential)
			{
				UsernamePasswordCredential usernamePasswordCredential = (UsernamePasswordCredential)list[0];
				recordBuilder.Add("AuthenticationKind", TextValue.New("UsernamePassword"), TypeValue.Text);
				recordBuilder.Add("Username", TextValue.New(usernamePasswordCredential.Username), TypeValue.Text);
				recordBuilder.Add("Password", TextValue.New(usernamePasswordCredential.Password), TypeValue.Text);
			}
			else if (list[0] is WindowsCredential)
			{
				WindowsCredential windowsCredential = (WindowsCredential)list[0];
				recordBuilder.Add("AuthenticationKind", TextValue.New("Windows"), TypeValue.Text);
				recordBuilder.Add("Username", TextValue.NewOrNull(windowsCredential.Username), TypeValue.Text);
				recordBuilder.Add("Password", TextValue.NewOrNull(windowsCredential.Password), TypeValue.Text);
				recordBuilder.Add("_validation", CredentialConversion.MakeUnforgeableCredential(engineHost, windowsCredential), TypeValue.Text);
			}
			else
			{
				if (!(list[0] is OAuthCredential))
				{
					if (list[0] is ParameterizedCredential)
					{
						ParameterizedCredential parameterizedCredential = (ParameterizedCredential)list[0];
						recordBuilder.Add("AuthenticationKind", TextValue.New(parameterizedCredential.Name), TypeValue.Text);
						using (Dictionary<string, string>.Enumerator enumerator = parameterizedCredential.Values.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<string, string> keyValuePair = enumerator.Current;
								recordBuilder.Add(keyValuePair.Key, TextValue.New(keyValuePair.Value), TypeValue.Text);
							}
							goto IL_03FD;
						}
					}
					credentialRecord = null;
					return false;
				}
				OAuthCredential oauthCredential = (OAuthCredential)list[0];
				Keys keys = Keys.New(oauthCredential.Properties.Keys.ToArray<string>());
				Value[] array = oauthCredential.Properties.Values.Select((string v) => TextValue.New(v)).ToArray<TextValue>();
				RecordValue recordValue = RecordValue.New(keys, array);
				recordBuilder.Add("AuthenticationKind", TextValue.New("OAuth"), TypeValue.Text);
				recordBuilder.Add("access_token", TextValue.New(oauthCredential.AccessToken), TypeValue.Text);
				recordBuilder.Add("refresh_token", TextValue.NewOrNull(oauthCredential.RefreshToken), NullableTypeValue.Text);
				recordBuilder.Add("expires", TextValue.NewOrNull(oauthCredential.Expires), NullableTypeValue.Text);
				recordBuilder.Add("Properties", recordValue, recordValue.Type);
			}
			IL_03FD:
			ResourceKindInfo resourceKindInfo;
			if (resource == null || !engine.TryLookupResourceKind(resource.Kind, out resourceKindInfo))
			{
				resourceKindInfo = null;
			}
			if (resourceKindInfo != null && resourceKindInfo.SupportsEncryptedConnection)
			{
				Value value = ((flag != null) ? LogicalValue.New(flag.Value) : Value.Null);
				recordBuilder.Add("EncryptConnection", value, NullableTypeValue.Logical);
			}
			if (resourceKindInfo != null && resourceKindInfo.SupportsConnectionString)
			{
				recordBuilder.Add("ConnectionString", TextValue.NewOrNull(text), NullableTypeValue.Text);
			}
			if (dictionary != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair2 in dictionary)
				{
					recordBuilder.Add(keyValuePair2.Key, TextValue.NewOrNull(keyValuePair2.Value), NullableTypeValue.Text);
				}
			}
			credentialRecord = new CredentialConversion.CredentialRecordValue(recordBuilder.ToRecord());
			return true;
		}

		// Token: 0x06008053 RID: 32851 RVA: 0x001B6108 File Offset: 0x001B4308
		public static bool TryConvertFromRecord(IEngine engine, IResource resource, RecordValue credentialRecord, out ResourceCredentialCollection credential)
		{
			credential = null;
			ResourceKindInfo resourceKindInfo;
			if (!engine.TryLookupResourceKind(resource.Kind, out resourceKindInfo))
			{
				return false;
			}
			Dictionary<string, Value> dictionary = credentialRecord.ToDictionary();
			IResourceCredential resourceCredential = null;
			AuthenticationInfo authenticationInfo = null;
			TextValue authKind = dictionary["AuthenticationKind"].AsText;
			dictionary.Remove("AuthenticationKind");
			string @string = authKind.String;
			AuthenticationKind authenticationKind;
			if (@string != null)
			{
				int i = @string.Length;
				switch (i)
				{
				case 3:
				{
					if (!(@string == "Key"))
					{
						goto IL_02C2;
					}
					authenticationKind = AuthenticationKind.Key;
					Value value = dictionary.GetAndRemoveOrDefault("Key", Value.Null);
					if (!value.IsText)
					{
						value = dictionary.GetAndRemoveOrDefault("Password", Value.Null);
					}
					resourceCredential = new FeedKeyCredential(value.AsText.String);
					goto IL_0378;
				}
				case 4:
					goto IL_02C2;
				case 5:
					if (!(@string == "OAuth"))
					{
						goto IL_02C2;
					}
					goto IL_0179;
				case 6:
					if (!(@string == "OAuth2"))
					{
						goto IL_02C2;
					}
					goto IL_0179;
				case 7:
				{
					if (!(@string == "Windows"))
					{
						goto IL_02C2;
					}
					authenticationKind = AuthenticationKind.Windows;
					string text = CredentialConversion.StringValue(dictionary.GetAndRemoveOrDefault("Username", Value.Null));
					string text2 = CredentialConversion.StringValue(dictionary.GetAndRemoveOrDefault("Password", Value.Null));
					Value andRemoveOrDefault = dictionary.GetAndRemoveOrDefault("_validation", Value.Null);
					if (!andRemoveOrDefault.IsText || !CredentialConversion.unforgeable.TryGetValue(andRemoveOrDefault.AsString, out resourceCredential) || !(resourceCredential is WindowsCredential) || !CredentialConversion.Compare(text, ((WindowsCredential)resourceCredential).Username) || !CredentialConversion.Compare(text2, ((WindowsCredential)resourceCredential).Password))
					{
						return false;
					}
					goto IL_0378;
				}
				case 8:
					if (!(@string == "Implicit"))
					{
						goto IL_02C2;
					}
					break;
				case 9:
					if (!(@string == "Anonymous"))
					{
						goto IL_02C2;
					}
					break;
				default:
				{
					if (i != 16)
					{
						goto IL_02C2;
					}
					if (!(@string == "UsernamePassword"))
					{
						goto IL_02C2;
					}
					authenticationKind = AuthenticationKind.UsernamePassword;
					string text = CredentialConversion.StringValue(dictionary.GetAndRemoveOrDefault("Username", Value.Null));
					string text2 = CredentialConversion.StringValue(dictionary.GetAndRemoveOrDefault("Password", Value.Null));
					resourceCredential = new BasicAuthCredential(text, text2);
					goto IL_0378;
				}
				}
				authenticationKind = AuthenticationKind.Implicit;
				goto IL_0378;
				IL_0179:
				authenticationKind = AuthenticationKind.OAuth2;
				string text3 = CredentialConversion.StringValue(dictionary.GetAndRemoveOrDefault("access_token", Value.Null));
				string text4 = CredentialConversion.StringValue(dictionary.GetAndRemoveOrDefault("expires", Value.Null));
				string text5 = CredentialConversion.StringValue(dictionary.GetAndRemoveOrDefault("refresh_token", Value.Null));
				Value andRemoveOrDefault2 = dictionary.GetAndRemoveOrDefault("Properties", Value.Null);
				resourceCredential = new OAuthCredential(text3, text4, text5, CredentialConversion.MakeProperties(andRemoveOrDefault2.ToDictionary()));
				goto IL_0378;
			}
			IL_02C2:
			authenticationInfo = resourceKindInfo.AuthenticationInfo.OfType<ParameterizedAuthenticationInfo>().SingleOrDefault((ParameterizedAuthenticationInfo p) => p.Name == authKind.String);
			if (authenticationInfo == null)
			{
				return false;
			}
			authenticationKind = AuthenticationKind.Parameterized;
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>(dictionary.Count);
			string[] array = dictionary.Keys.ToArray<string>();
			for (int i = 0; i < array.Length; i++)
			{
				string propertyKey = array[i];
				if (authenticationInfo.Properties.SingleOrDefault((CredentialProperty p) => p.Name == propertyKey) != null)
				{
					dictionary2.Add(propertyKey, CredentialConversion.StringValue(dictionary.GetAndRemoveOrDefault(propertyKey, Value.Null)));
				}
			}
			resourceCredential = new ParameterizedCredential(authenticationInfo.Name, dictionary2);
			IL_0378:
			List<IResourceCredential> list = new List<IResourceCredential>();
			if (resourceCredential != null)
			{
				if (authenticationInfo != null || resourceKindInfo.TryGetAuthenticationInfo(authenticationKind, out authenticationInfo))
				{
					resourceCredential = authenticationInfo.Normalize(resource.Kind, resourceCredential);
				}
				list.Add(resourceCredential);
			}
			if (resourceKindInfo.SupportsEncryptedConnection)
			{
				Value andRemoveOrDefault3 = dictionary.GetAndRemoveOrDefault("EncryptConnection", Value.Null);
				if (!andRemoveOrDefault3.IsNull)
				{
					list.Add(new EncryptedConnectionAdornment(andRemoveOrDefault3.AsBoolean));
				}
			}
			if (resourceKindInfo.SupportsConnectionString)
			{
				Value andRemoveOrDefault4 = dictionary.GetAndRemoveOrDefault("ConnectionString", Value.Null);
				if (!andRemoveOrDefault4.IsNull)
				{
					list.Add(new ConnectionStringAdornment(andRemoveOrDefault4.AsText.String));
				}
			}
			if (dictionary.Count > 0 && resourceKindInfo.ConnectionStringProperties.Count > 0)
			{
				Dictionary<string, string> dictionary3 = new Dictionary<string, string>(dictionary.Count);
				foreach (string text6 in resourceKindInfo.ConnectionStringProperties)
				{
					string text7 = CredentialConversion.StringValue(dictionary.GetAndRemoveOrDefault(text6, Value.Null));
					if (text7 != null)
					{
						dictionary3.Add(text6, text7);
					}
				}
				if (dictionary3.Count > 0)
				{
					list.Add(new ConnectionStringPropertiesAdornment(dictionary3));
				}
			}
			if (dictionary.Count > 0 && resourceKindInfo is GenericProviderResourceKindInfo)
			{
				string string2 = ((GenericProviderResourceKindInfo)resourceKindInfo).ConnectionStringHandler.GetString(CredentialConversion.MakeProperties(dictionary));
				list.Add(new ConnectionStringAdornment(string2));
			}
			credential = new ResourceCredentialCollection(resource, list.ToArray());
			return true;
		}

		// Token: 0x06008054 RID: 32852 RVA: 0x00012F24 File Offset: 0x00011124
		private static string StringValue(Value value)
		{
			if (value.IsNull)
			{
				return null;
			}
			return value.AsText.String;
		}

		// Token: 0x06008055 RID: 32853 RVA: 0x001B6618 File Offset: 0x001B4818
		private static Dictionary<string, string> MakeProperties(Dictionary<string, Value> properties)
		{
			if (properties == null)
			{
				return new Dictionary<string, string>();
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>(properties.Count);
			foreach (KeyValuePair<string, Value> keyValuePair in properties)
			{
				string text = CredentialConversion.StringValue(keyValuePair.Value);
				if (text != null)
				{
					dictionary.Add(keyValuePair.Key, text);
				}
			}
			properties.Clear();
			return dictionary;
		}

		// Token: 0x06008056 RID: 32854 RVA: 0x001B669C File Offset: 0x001B489C
		private static bool Compare(string value1, string value2)
		{
			return (value1 == null && value2 == null) || (value1 != null && value2 != null && value1 == value2);
		}

		// Token: 0x06008057 RID: 32855 RVA: 0x001B66B8 File Offset: 0x001B48B8
		private static TextValue MakeUnforgeableCredential(IEngineHost engineHost, IResourceCredential original)
		{
			string text = Guid.NewGuid().ToString();
			CredentialConversion.unforgeable.Add(text, original);
			ILifetimeService lifetimeService = engineHost.QueryService<ILifetimeService>();
			if (lifetimeService != null)
			{
				lifetimeService.Register(new CredentialConversion.TokenCleanup(text));
			}
			return TextValue.New(text);
		}

		// Token: 0x040045E0 RID: 17888
		public const string OAuthKeyOld = "OAuth";

		// Token: 0x040045E1 RID: 17889
		private const string accessTokenKey = "access_token";

		// Token: 0x040045E2 RID: 17890
		private const string authenticationKindKey = "AuthenticationKind";

		// Token: 0x040045E3 RID: 17891
		private const string expiresKey = "expires";

		// Token: 0x040045E4 RID: 17892
		private const string passwordKey = "Password";

		// Token: 0x040045E5 RID: 17893
		private const string propertiesKey = "Properties";

		// Token: 0x040045E6 RID: 17894
		private const string refreshTokenKey = "refresh_token";

		// Token: 0x040045E7 RID: 17895
		private const string usernameKey = "Username";

		// Token: 0x040045E8 RID: 17896
		private const string antiforgeryKey = "_validation";

		// Token: 0x040045E9 RID: 17897
		private static Dictionary<string, IResourceCredential> unforgeable = new Dictionary<string, IResourceCredential>();

		// Token: 0x020012F1 RID: 4849
		private sealed class TokenCleanup : IDisposable
		{
			// Token: 0x06008059 RID: 32857 RVA: 0x001B670D File Offset: 0x001B490D
			public TokenCleanup(string token)
			{
				this.token = token;
			}

			// Token: 0x0600805A RID: 32858 RVA: 0x001B671C File Offset: 0x001B491C
			public void Dispose()
			{
				CredentialConversion.unforgeable.Remove(this.token);
			}

			// Token: 0x040045EA RID: 17898
			private readonly string token;
		}

		// Token: 0x020012F2 RID: 4850
		private sealed class CredentialRecordValue : RecordValue
		{
			// Token: 0x0600805B RID: 32859 RVA: 0x001B672F File Offset: 0x001B492F
			public CredentialRecordValue(RecordValue credential)
			{
				this.credential = credential;
			}

			// Token: 0x170022C8 RID: 8904
			// (get) Token: 0x0600805C RID: 32860 RVA: 0x001B673E File Offset: 0x001B493E
			public override Keys Keys
			{
				get
				{
					return this.credential.Keys;
				}
			}

			// Token: 0x170022C9 RID: 8905
			// (get) Token: 0x0600805D RID: 32861 RVA: 0x001B674B File Offset: 0x001B494B
			public override TypeValue Type
			{
				get
				{
					return this.credential.Type;
				}
			}

			// Token: 0x0600805E RID: 32862 RVA: 0x001B6758 File Offset: 0x001B4958
			public override IValueReference GetReference(int index)
			{
				if (index >= this.Keys.Length)
				{
					throw ValueException.RecordIndexOutOfRange(index, this.CreateRedactedRecord());
				}
				return this.credential.GetReference(index);
			}

			// Token: 0x170022CA RID: 8906
			public override Value this[int index]
			{
				get
				{
					if (index >= this.credential.Count)
					{
						throw ValueException.RecordIndexOutOfRange(index, this.CreateRedactedRecord());
					}
					return this.credential[index];
				}
			}

			// Token: 0x06008060 RID: 32864 RVA: 0x001B67AA File Offset: 0x001B49AA
			public override ValueException MissingField(string fieldName)
			{
				return ValueException.MissingField(fieldName, this.CreateRedactedRecord());
			}

			// Token: 0x06008061 RID: 32865 RVA: 0x001B67B8 File Offset: 0x001B49B8
			private RecordValue CreateRedactedRecord()
			{
				return RecordValue.New(this.Keys, (int i) => TextValue.New("<redacted>"));
			}

			// Token: 0x040045EB RID: 17899
			private readonly RecordValue credential;
		}
	}
}
