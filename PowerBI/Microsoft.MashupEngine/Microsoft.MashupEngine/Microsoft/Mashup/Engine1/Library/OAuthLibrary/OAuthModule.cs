using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.OAuthLibrary
{
	// Token: 0x020008FC RID: 2300
	internal sealed class OAuthModule : Module
	{
		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x060041BA RID: 16826 RVA: 0x000DD636 File Offset: 0x000DB836
		public override string Name
		{
			get
			{
				return "OAuth";
			}
		}

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x060041BB RID: 16827 RVA: 0x000DD63D File Offset: 0x000DB83D
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(14, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "OAuth.StartLogin";
						case 1:
							return "OAuth.FinishLogin";
						case 2:
							return "OAuth.Refresh";
						case 3:
							return "OAuth.Logout";
						case 4:
							return "OAuth.Resource";
						case 5:
							return "OAuth.ResourceType";
						case 6:
							return "OAuth.BrowserNavigationType";
						case 7:
							return "OAuth.ClientApplicationType";
						case 8:
							return "OAuth.TokenType";
						case 9:
							return OAuthModule.ClientApplicationSecretTypeEnum.Type.GetName();
						case 10:
							return OAuthModule.ClientApplicationSecretTypeEnum.Default.GetName();
						case 11:
							return OAuthModule.ClientApplicationSecretTypeEnum.Literal.GetName();
						case 12:
							return OAuthModule.ClientApplicationSecretTypeEnum.Thumbprint.GetName();
						case 13:
							return OAuthModule.ClientApplicationSecretTypeEnum.SubjectNameIssuer.GetName();
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x000DD67C File Offset: 0x000DB87C
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new OAuthModule.OAuth.StartLoginFunctionValue(hostEnvironment);
				case 1:
					return new OAuthModule.OAuth.FinishLoginFunctionValue(hostEnvironment);
				case 2:
					return new OAuthModule.OAuth.RefreshFunctionValue(hostEnvironment);
				case 3:
					return new OAuthModule.OAuth.LogoutFunctionValue(hostEnvironment);
				case 4:
					return new OAuthModule.OAuth.ResourceFunctionValue(hostEnvironment);
				case 5:
					return OAuthModule.OAuth.ResourceType;
				case 6:
					return OAuthModule.OAuth.BrowserNavigationType;
				case 7:
					return OAuthModule.OAuth.ClientApplicationType;
				case 8:
					return OAuthModule.OAuth.TokenType;
				case 9:
					return OAuthModule.ClientApplicationSecretTypeEnum.Type;
				case 10:
					return OAuthModule.ClientApplicationSecretTypeEnum.Default;
				case 11:
					return OAuthModule.ClientApplicationSecretTypeEnum.Literal;
				case 12:
					return OAuthModule.ClientApplicationSecretTypeEnum.Thumbprint;
				case 13:
					return OAuthModule.ClientApplicationSecretTypeEnum.SubjectNameIssuer;
				default:
					throw new InvalidOperationException();
				}
			});
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x000DD6B0 File Offset: 0x000DB8B0
		public static RecordValue CredentialAsRecord(TokenCredential value)
		{
			List<NamedValue> list = new List<NamedValue>(value.Properties.Count + 3);
			list.Add(new NamedValue("access_token", TextValue.New(value.AccessToken)));
			list.Add(new NamedValue("expires", TextValue.NewOrNull(value.Expires)));
			list.Add(new NamedValue("refresh_token", TextValue.New(value.RefreshToken)));
			foreach (KeyValuePair<string, string> keyValuePair in value.Properties)
			{
				list.Add(new NamedValue(keyValuePair.Key, TextValue.New(keyValuePair.Value)));
			}
			return RecordValue.New(list.ToArray());
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x000DD78C File Offset: 0x000DB98C
		public static TokenCredential CredentialFromRecord(Value value)
		{
			RecordValue asRecord = value.AsRecord;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (NamedValue namedValue in asRecord.GetFields())
			{
				if (namedValue.Value.IsText)
				{
					dictionary[namedValue.Key] = namedValue.Value.AsString;
				}
				else if (namedValue.Value.IsNumber)
				{
					dictionary[namedValue.Key] = namedValue.Value.AsNumber.ToObject().ToString();
				}
			}
			string text = OAuthModule.TryRemove(dictionary, "access_token") ?? asRecord["access_token"].AsText.String;
			string text2 = OAuthModule.TryRemove(dictionary, "expires_in");
			string text3 = OAuthModule.TryRemove(dictionary, "expires");
			string text4 = OAuthModule.TryRemove(dictionary, "refresh_token");
			if (text3 == null && text2 != null)
			{
				text3 = TokenCredential.GetExpiresAtFromExpiresIn(text2);
			}
			return new TokenCredential(text, text3, text4, dictionary);
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x000DD8A4 File Offset: 0x000DBAA4
		public static Value ClientApplicationAsRecord(OAuthClientApplication application)
		{
			if (application == null)
			{
				return Value.Null;
			}
			return RecordValue.New(OAuthModule.OAuth.ClientApplicationType.FieldKeys, new Value[]
			{
				TextValue.New(application.Id),
				TextValue.New(application.Secret),
				TextValue.New(application.CallbackUrl),
				OAuthModule.ClientApplicationSecretTypeEnum.Type.GetEnum(application.SecretType)
			});
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x000DD90C File Offset: 0x000DBB0C
		public static OAuthClientApplication ClientApplicationFromRecord(Value clientApplicationRecord)
		{
			if (clientApplicationRecord.IsNull)
			{
				return null;
			}
			Value zero;
			if (!clientApplicationRecord.TryGetValue("ClientSecretType", out zero))
			{
				zero = NumberValue.Zero;
			}
			return new OAuthClientApplication(clientApplicationRecord["ClientId"].AsString, clientApplicationRecord["ClientSecret"].AsString, clientApplicationRecord["CallbackUrl"].AsString, OAuthModule.ClientApplicationSecretTypeEnum.Type.GetValue(zero.AsNumber));
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x000DD980 File Offset: 0x000DBB80
		private static string TryRemove(Dictionary<string, string> properties, string key)
		{
			string text;
			if (properties.TryGetValue(key, out text))
			{
				properties.Remove(key);
			}
			return text;
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x000DD9A4 File Offset: 0x000DBBA4
		private static IOAuthProvider GetOAuthProvider(IEngineHost engineHost, RecordValue resourceRecord, Value clientApplicationRecord)
		{
			IOAuthProvider ioauthProvider;
			using (OAuthModule.OAuthEngineHost oauthEngineHost = new OAuthModule.OAuthEngineHost(engineHost, resourceRecord))
			{
				if (oauthEngineHost.ClientApplicationType == OAuthClientApplicationType.Required && clientApplicationRecord.IsNull)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.OAuth_RequiresClientApplication, null, null);
				}
				OAuthClientApplication oauthClientApplication = OAuthModule.ClientApplicationFromRecord(clientApplicationRecord);
				ioauthProvider = (IOAuthProvider)oauthEngineHost.ProviderFactory.CreateProvider(oauthEngineHost, oauthEngineHost.QueryService<IEngine>(), oauthClientApplication, oauthEngineHost.CurrentResource.NonNormalizedPath);
			}
			return ioauthProvider;
		}

		// Token: 0x0400226F RID: 8815
		private const string AccessToken = "access_token";

		// Token: 0x04002270 RID: 8816
		private const string RefreshToken = "refresh_token";

		// Token: 0x04002271 RID: 8817
		private const string Expires = "expires";

		// Token: 0x04002272 RID: 8818
		private const string ExpiresIn = "expires_in";

		// Token: 0x04002273 RID: 8819
		private const string ClientId = "ClientId";

		// Token: 0x04002274 RID: 8820
		private const string ClientSecret = "ClientSecret";

		// Token: 0x04002275 RID: 8821
		private const string ClientSecretType = "ClientSecretType";

		// Token: 0x04002276 RID: 8822
		private const string CallbackUrl = "CallbackUrl";

		// Token: 0x04002277 RID: 8823
		private Keys exportKeys;

		// Token: 0x020008FD RID: 2301
		private enum Exports
		{
			// Token: 0x04002279 RID: 8825
			OAuthStartLogin,
			// Token: 0x0400227A RID: 8826
			OAuthFinishLogin,
			// Token: 0x0400227B RID: 8827
			OAuthRefresh,
			// Token: 0x0400227C RID: 8828
			OAuthLogout,
			// Token: 0x0400227D RID: 8829
			OAuthResource,
			// Token: 0x0400227E RID: 8830
			OAuthResourceType,
			// Token: 0x0400227F RID: 8831
			OAuthBrowserNavigationType,
			// Token: 0x04002280 RID: 8832
			OAuthClientApplicationType,
			// Token: 0x04002281 RID: 8833
			OAuthTokenType,
			// Token: 0x04002282 RID: 8834
			ClientApplicationSecretTypeType,
			// Token: 0x04002283 RID: 8835
			ClientApplicationSecretType_Default,
			// Token: 0x04002284 RID: 8836
			ClientApplicationSecretType_Literal,
			// Token: 0x04002285 RID: 8837
			ClientApplicationSecretType_Thumbprint,
			// Token: 0x04002286 RID: 8838
			ClientApplicationSecretType_SubjectNameIssuer,
			// Token: 0x04002287 RID: 8839
			Count
		}

		// Token: 0x020008FE RID: 2302
		public static class ClientApplicationSecretTypeEnum
		{
			// Token: 0x04002288 RID: 8840
			public static readonly IntEnumTypeValue<ClientApplicationSecretType> Type = new IntEnumTypeValue<ClientApplicationSecretType>("ClientApplicationSecretType.Type");

			// Token: 0x04002289 RID: 8841
			public static readonly NumberValue Default = OAuthModule.ClientApplicationSecretTypeEnum.Type.NewEnumValue("ClientApplicationSecretType.Default", 0, ClientApplicationSecretType.Default, null);

			// Token: 0x0400228A RID: 8842
			public static readonly NumberValue Literal = OAuthModule.ClientApplicationSecretTypeEnum.Type.NewEnumValue("ClientApplicationSecretType.Literal", 1, ClientApplicationSecretType.Literal, null);

			// Token: 0x0400228B RID: 8843
			public static readonly NumberValue Thumbprint = OAuthModule.ClientApplicationSecretTypeEnum.Type.NewEnumValue("ClientApplicationSecretType.Thumbprint", 2, ClientApplicationSecretType.Thumbprint, null);

			// Token: 0x0400228C RID: 8844
			public static readonly NumberValue SubjectNameIssuer = OAuthModule.ClientApplicationSecretTypeEnum.Type.NewEnumValue("ClientApplicationSecretType.SubjectNameIssuer", 3, ClientApplicationSecretType.SubjectNameIssuer, null);
		}

		// Token: 0x020008FF RID: 2303
		private class OAuth
		{
			// Token: 0x0400228D RID: 8845
			public static readonly RecordTypeValue BrowserNavigationType = RecordTypeValue.New(RecordValue.New(Keys.New(new string[] { "LoginUri", "CallbackUri", "WindowHeight", "WindowWidth", "Context" }), new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Uri, false),
				RecordTypeAlgebra.NewField(TypeValue.Uri, false),
				RecordTypeAlgebra.NewField(TypeValue.Int32, false),
				RecordTypeAlgebra.NewField(TypeValue.Int32, false),
				RecordTypeAlgebra.NewField(NullableTypeValue.Binary, true)
			}), false);

			// Token: 0x0400228E RID: 8846
			public static readonly RecordTypeValue ClientApplicationType = RecordTypeValue.New(RecordValue.New(Keys.New("ClientId", "ClientSecret", "CallbackUrl", "ClientSecretType"), new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Uri, false),
				RecordTypeAlgebra.NewField(OAuthModule.ClientApplicationSecretTypeEnum.Type, true)
			}), false);

			// Token: 0x0400228F RID: 8847
			public static readonly RecordTypeValue TokenType = RecordTypeValue.New(RecordValue.New(Keys.New("access_token", "expires", "refresh_token"), new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Text, true),
				RecordTypeAlgebra.NewField(TypeValue.Text, true)
			}), true);

			// Token: 0x04002290 RID: 8848
			private static readonly RecordTypeValue TokenServiceType = RecordTypeValue.New(RecordValue.New(Keys.New("Authority", "AuthorizationUri", "TokenUri", "LogoutUri"), new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Uri, false),
				RecordTypeAlgebra.NewField(TypeValue.Uri, false),
				RecordTypeAlgebra.NewField(TypeValue.Uri, false)
			}), true);

			// Token: 0x04002291 RID: 8849
			public static readonly RecordTypeValue ResourceType = RecordTypeValue.New(RecordValue.New(Keys.New("Resource", "Scope", "TokenService", "Properties"), new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(OAuthModule.OAuth.TokenServiceType, false),
				RecordTypeAlgebra.NewField(NullableTypeValue.Record, true)
			}), true);

			// Token: 0x02000900 RID: 2304
			public sealed class StartLoginFunctionValue : NativeFunctionValue4<RecordValue, RecordValue, Value, TextValue, TextValue>
			{
				// Token: 0x060041C7 RID: 16839 RVA: 0x000DDCC0 File Offset: 0x000DBEC0
				public StartLoginFunctionValue(IEngineHost engineHost)
					: base(OAuthModule.OAuth.BrowserNavigationType, "dataSource", TypeValue.Record, "clientApplication", OAuthModule.OAuth.ClientApplicationType.Nullable, "state", TypeValue.Text, "display", TypeValue.Text)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x060041C8 RID: 16840 RVA: 0x000DDD0C File Offset: 0x000DBF0C
				public override RecordValue TypedInvoke(RecordValue dataSource, Value clientApplication, TextValue state, TextValue display)
				{
					RecordValue recordValue;
					try
					{
						OAuthBrowserNavigation oauthBrowserNavigation = OAuthModule.GetOAuthProvider(this.engineHost, dataSource, clientApplication).StartLogin(state.String, display.String);
						recordValue = RecordValue.New(OAuthModule.OAuth.BrowserNavigationType, new Value[]
						{
							TextValue.New(oauthBrowserNavigation.LoginUri.AbsoluteUri),
							TextValue.New(oauthBrowserNavigation.CallbackUri.AbsoluteUri),
							NumberValue.New(oauthBrowserNavigation.WindowHeight),
							NumberValue.New(oauthBrowserNavigation.WindowWidth),
							(oauthBrowserNavigation.SerializedContext == null) ? Value.Null : BinaryValue.New(oauthBrowserNavigation.SerializedContext)
						});
					}
					catch (OAuthException ex)
					{
						throw ValueException.NewDataSourceError(ex.Message, Value.Null, null);
					}
					return recordValue;
				}

				// Token: 0x04002292 RID: 8850
				private readonly IEngineHost engineHost;
			}

			// Token: 0x02000901 RID: 2305
			public sealed class FinishLoginFunctionValue : NativeFunctionValue5<RecordValue, RecordValue, Value, Value, TextValue, TextValue>
			{
				// Token: 0x060041C9 RID: 16841 RVA: 0x000DDDD0 File Offset: 0x000DBFD0
				public FinishLoginFunctionValue(IEngineHost engineHost)
					: base(OAuthModule.OAuth.TokenType, "dataSource", TypeValue.Record, "clientApplication", OAuthModule.OAuth.ClientApplicationType.Nullable, "context", NullableTypeValue.Binary, "callbackUri", TypeValue.Uri, "state", TypeValue.Text)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x060041CA RID: 16842 RVA: 0x000DDE28 File Offset: 0x000DC028
				public override RecordValue TypedInvoke(RecordValue dataSource, Value clientApplication, Value context, TextValue callbackUri, TextValue state)
				{
					RecordValue recordValue;
					try
					{
						IOAuthProvider oauthProvider = OAuthModule.GetOAuthProvider(this.engineHost, dataSource, clientApplication);
						byte[] array = (context.IsNull ? null : context.AsBinary.AsBytes);
						recordValue = OAuthModule.CredentialAsRecord(oauthProvider.FinishLogin(array, UriHelper.CreateUriFromValue(callbackUri), state.String));
					}
					catch (OAuthException ex)
					{
						throw ValueException.NewDataSourceError(ex.Message, Value.Null, null);
					}
					return recordValue;
				}

				// Token: 0x04002293 RID: 8851
				private readonly IEngineHost engineHost;
			}

			// Token: 0x02000902 RID: 2306
			public sealed class RefreshFunctionValue : NativeFunctionValue3<RecordValue, RecordValue, Value, RecordValue>
			{
				// Token: 0x060041CB RID: 16843 RVA: 0x000DDE98 File Offset: 0x000DC098
				public RefreshFunctionValue(IEngineHost engineHost)
					: base(OAuthModule.OAuth.TokenType, "dataSource", TypeValue.Record, "clientApplication", OAuthModule.OAuth.ClientApplicationType.Nullable, "token", OAuthModule.OAuth.TokenType)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x060041CC RID: 16844 RVA: 0x000DDED0 File Offset: 0x000DC0D0
				public override RecordValue TypedInvoke(RecordValue dataSource, Value clientApplication, RecordValue token)
				{
					RecordValue recordValue;
					try
					{
						IOAuthProvider oauthProvider = OAuthModule.GetOAuthProvider(this.engineHost, dataSource, clientApplication);
						TokenCredential tokenCredential = OAuthModule.CredentialFromRecord(token);
						tokenCredential = oauthProvider.Refresh(tokenCredential);
						recordValue = OAuthModule.CredentialAsRecord(tokenCredential);
					}
					catch (OAuthException ex)
					{
						throw ValueException.NewDataSourceError(ex.Message, Value.Null, null);
					}
					return recordValue;
				}

				// Token: 0x04002294 RID: 8852
				private readonly IEngineHost engineHost;
			}

			// Token: 0x02000903 RID: 2307
			public sealed class LogoutFunctionValue : NativeFunctionValue3<TextValue, RecordValue, Value, TextValue>
			{
				// Token: 0x060041CD RID: 16845 RVA: 0x000DDF24 File Offset: 0x000DC124
				public LogoutFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Text, "dataSource", TypeValue.Record, "clientApplication", OAuthModule.OAuth.ClientApplicationType.Nullable, "accessToken", TypeValue.Text)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x060041CE RID: 16846 RVA: 0x000DDF5C File Offset: 0x000DC15C
				public override TextValue TypedInvoke(RecordValue dataSource, Value clientApplication, TextValue accessToken)
				{
					TextValue textValue;
					try
					{
						textValue = TextValue.New(OAuthModule.GetOAuthProvider(this.engineHost, dataSource, clientApplication).Logout(accessToken.String).AbsoluteUri);
					}
					catch (OAuthException ex)
					{
						throw ValueException.NewDataSourceError(ex.Message, Value.Null, null);
					}
					return textValue;
				}

				// Token: 0x04002295 RID: 8853
				private readonly IEngineHost engineHost;
			}

			// Token: 0x02000904 RID: 2308
			public sealed class ResourceFunctionValue : NativeFunctionValue1<RecordValue, RecordValue>
			{
				// Token: 0x060041CF RID: 16847 RVA: 0x000DDFB0 File Offset: 0x000DC1B0
				public ResourceFunctionValue(IEngineHost engineHost)
					: base(OAuthModule.OAuth.ResourceType, "dataSource", TypeValue.Record)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x060041D0 RID: 16848 RVA: 0x000DDFD0 File Offset: 0x000DC1D0
				public override RecordValue TypedInvoke(RecordValue dataSource)
				{
					RecordValue recordValue;
					try
					{
						OAuthResource oauthResource;
						using (OAuthModule.OAuthEngineHost oauthEngineHost = new OAuthModule.OAuthEngineHost(this.engineHost, dataSource))
						{
							oauthResource = (OAuthResource)oauthEngineHost.ProviderFactory.CreateResource(oauthEngineHost, oauthEngineHost.QueryService<IEngine>(), oauthEngineHost.CurrentResource.NonNormalizedPath);
						}
						recordValue = RecordValue.New(OAuthModule.OAuth.ResourceType, new Value[]
						{
							TextValue.New(oauthResource.Resource),
							TextValue.New(oauthResource.Scope),
							RecordValue.New(OAuthModule.OAuth.TokenServiceType, new Value[]
							{
								TextValue.New(oauthResource.TokenService.AuthorityId),
								TextValue.New(oauthResource.TokenService.GetAuthorizeUri(string.Empty).AbsoluteUri),
								TextValue.New(oauthResource.TokenService.GetTokenUri(string.Empty).AbsoluteUri),
								TextValue.New(oauthResource.TokenService.GetLogoutUri(string.Empty).AbsoluteUri)
							}),
							ValueMarshaller.MarshalFromClrDictionary(oauthResource.Properties)
						});
					}
					catch (OAuthException ex)
					{
						throw ValueException.NewDataSourceError(ex.Message, Value.Null, null);
					}
					return recordValue;
				}

				// Token: 0x04002296 RID: 8854
				private readonly IEngineHost engineHost;
			}
		}

		// Token: 0x02000905 RID: 2309
		private sealed class OAuthEngineHost : IEngineHost, IExtensibilityService, IDisposable
		{
			// Token: 0x060041D1 RID: 16849 RVA: 0x000DE104 File Offset: 0x000DC304
			public OAuthEngineHost(IEngineHost engineHost, RecordValue resourceRecord)
			{
				this.engineHost = engineHost;
				this.oldSettings = AadOAuthProvider.DefaultSettings;
				Value value = resourceRecord["Kind"];
				ResourceKindInfo resourceKindInfo;
				if (!ResourceKinds.Lookup(value.AsString, out resourceKindInfo))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Resource_Invalid, value, null);
				}
				this.authInfo = resourceKindInfo.AuthenticationInfo.Where((AuthenticationInfo a) => a.AuthenticationKind == AuthenticationKind.OAuth2).Cast<OAuth2AuthenticationInfo>().FirstOrDefault<OAuth2AuthenticationInfo>();
				OAuth2AuthenticationInfo oauth2AuthenticationInfo = this.authInfo;
				if (((oauth2AuthenticationInfo != null) ? oauth2AuthenticationInfo.ProviderFactory : null) == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.OAuth_NotSupported, value, null);
				}
				Value value2 = resourceRecord["Path"];
				string text = null;
				if (!resourceKindInfo.Validate(value2.AsString, out this.resource, out text))
				{
					throw ValueException.NewExpressionError(text, value2, null);
				}
				object obj;
				if (engineHost.TryGetConfigurationProperty("AadSettings", out obj) && obj is string)
				{
					try
					{
						AadOAuthProvider.DefaultSettings = OAuthSettings.Deserialize((string)obj);
					}
					catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.OAuth_InvalidAadSettings, Value.Null, ex);
					}
				}
			}

			// Token: 0x17001507 RID: 5383
			// (get) Token: 0x060041D2 RID: 16850 RVA: 0x000DE244 File Offset: 0x000DC444
			public IResource CurrentResource
			{
				get
				{
					return this.resource;
				}
			}

			// Token: 0x17001508 RID: 5384
			// (get) Token: 0x060041D3 RID: 16851 RVA: 0x000DE24C File Offset: 0x000DC44C
			public ResourceCredentialCollection CurrentCredentials
			{
				get
				{
					return new ResourceCredentialCollection(this.resource, Array.Empty<IResourceCredential>());
				}
			}

			// Token: 0x17001509 RID: 5385
			// (get) Token: 0x060041D4 RID: 16852 RVA: 0x00002105 File Offset: 0x00000305
			public bool ImpersonateResource
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700150A RID: 5386
			// (get) Token: 0x060041D5 RID: 16853 RVA: 0x000DE25E File Offset: 0x000DC45E
			public IOAuthFactory ProviderFactory
			{
				get
				{
					return this.authInfo.ProviderFactory;
				}
			}

			// Token: 0x1700150B RID: 5387
			// (get) Token: 0x060041D6 RID: 16854 RVA: 0x000DE26B File Offset: 0x000DC46B
			public OAuthClientApplicationType ClientApplicationType
			{
				get
				{
					return this.authInfo.ClientApplicationType;
				}
			}

			// Token: 0x060041D7 RID: 16855 RVA: 0x000DE278 File Offset: 0x000DC478
			public T QueryService<T>() where T : class
			{
				if (typeof(T) == typeof(IExtensibilityService))
				{
					return (T)((object)this);
				}
				return this.engineHost.QueryService<T>();
			}

			// Token: 0x060041D8 RID: 16856 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void RefreshCredential(bool forceRefresh)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060041D9 RID: 16857 RVA: 0x000DE2A7 File Offset: 0x000DC4A7
			public void Dispose()
			{
				if (this.oldSettings != null)
				{
					AadOAuthProvider.DefaultSettings = this.oldSettings;
					this.oldSettings = null;
				}
			}

			// Token: 0x04002297 RID: 8855
			private readonly IEngineHost engineHost;

			// Token: 0x04002298 RID: 8856
			private readonly IResource resource;

			// Token: 0x04002299 RID: 8857
			private readonly OAuth2AuthenticationInfo authInfo;

			// Token: 0x0400229A RID: 8858
			private OAuthSettings oldSettings;
		}
	}
}
