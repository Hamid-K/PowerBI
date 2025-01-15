using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.EngineHost.Services.OAuth;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200004B RID: 75
	public static class OAuthProviderFactory
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000D918 File Offset: 0x0000BB18
		internal static bool EnableOAuthContainer
		{
			get
			{
				object obj = OAuthProviderFactory.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = OAuthProviderFactory.enableContainers;
				}
				return flag2;
			}
			set
			{
				object obj = OAuthProviderFactory.syncRoot;
				lock (obj)
				{
					OAuthProviderFactory.enableContainers = value;
				}
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000D958 File Offset: 0x0000BB58
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000D998 File Offset: 0x0000BB98
		internal static string OAuthContainerPool
		{
			get
			{
				object obj = OAuthProviderFactory.syncRoot;
				string text;
				lock (obj)
				{
					text = OAuthProviderFactory.connectionPoolIdentity;
				}
				return text;
			}
			set
			{
				OAuthProviderFactory.connectionPoolIdentity = value;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		private static bool ShouldUseContainer(out string connectionPoolIdentity)
		{
			object obj = OAuthProviderFactory.syncRoot;
			bool flag2;
			lock (obj)
			{
				connectionPoolIdentity = OAuthProviderFactory.connectionPoolIdentity;
				flag2 = OAuthProviderFactory.enableContainers;
			}
			return flag2;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		public static IOAuthProvider GetOAuthProvider(this DataSource dataSource)
		{
			return dataSource.GetOAuthProvider(null, EventSource.CurrentThreadActivityId);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D9F6 File Offset: 0x0000BBF6
		public static IOAuthProvider GetOAuthProvider(this DataSource dataSource, Guid activityId)
		{
			return dataSource.GetOAuthProvider(null, activityId);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000DA00 File Offset: 0x0000BC00
		public static IOAuthProvider GetOAuthProvider(this DataSource dataSource, OAuthClientApplication application)
		{
			return dataSource.GetOAuthProvider(application, EventSource.CurrentThreadActivityId);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000DA0E File Offset: 0x0000BC0E
		public static IOAuthProvider GetOAuthProvider(this DataSource dataSource, OAuthClientApplication application, Guid activityId)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			return OAuthProviderFactory.CreateOAuthProvider(dataSource.NormalizedResource, application, activityId, null);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		public static IOAuthProvider GetOAuthProvider(this DataSourceReference dataSourceReference)
		{
			return dataSourceReference.GetOAuthProvider(null, EventSource.CurrentThreadActivityId);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000DA3A File Offset: 0x0000BC3A
		public static IOAuthProvider GetOAuthProvider(this DataSourceReference dataSourceReference, Guid activityId)
		{
			return dataSourceReference.GetOAuthProvider(null, activityId);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public static IOAuthProvider GetOAuthProvider(this DataSourceReference dataSourceReference, OAuthClientApplication application)
		{
			return dataSourceReference.GetOAuthProvider(application, EventSource.CurrentThreadActivityId);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000DA52 File Offset: 0x0000BC52
		public static IOAuthProvider GetOAuthProvider(this DataSourceReference dataSourceReference, OAuthClientApplication application, Guid activityId)
		{
			if (dataSourceReference == null)
			{
				throw new ArgumentNullException("dataSourceReference");
			}
			return OAuthProviderFactory.CreateOAuthProvider(dataSourceReference.NonNormalizedResource, application, activityId, null);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000DA70 File Offset: 0x0000BC70
		public static bool TryGetOAuthResource(this DataSourceReference dataSourceReference, Guid activityId, out OAuthResource resource)
		{
			if (dataSourceReference == null)
			{
				throw new ArgumentNullException("dataSourceReference");
			}
			return OAuthProviderFactory.TryCreateOAuthResource(dataSourceReference.NonNormalizedResource, activityId, string.Empty, out resource);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000DA92 File Offset: 0x0000BC92
		public static IOAuthProvider GetOAuthProvider(this DataSource dataSource, IOAuthTracingService tracingService)
		{
			return dataSource.GetOAuthProvider(null, tracingService);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000DA9C File Offset: 0x0000BC9C
		public static IOAuthProvider GetOAuthProvider(this DataSource dataSource, OAuthClientApplication application, IOAuthTracingService tracingService)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			OAuthProviderFactory.TracingService tracingService2 = (OAuthProviderFactory.TracingService)tracingService;
			return OAuthProviderFactory.CreateOAuthProvider(dataSource.NormalizedResource, application, tracingService2.ActivityId, tracingService2.CorrelationId);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000DAD6 File Offset: 0x0000BCD6
		public static IOAuthProvider GetOAuthProvider(this DataSourceReference dataSourceReference, IOAuthTracingService tracingService)
		{
			return dataSourceReference.GetOAuthProvider(null, tracingService);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		public static IOAuthProvider GetOAuthProvider(this DataSourceReference dataSourceReference, OAuthClientApplication application, IOAuthTracingService tracingService)
		{
			if (dataSourceReference == null)
			{
				throw new ArgumentNullException("dataSourceReference");
			}
			OAuthProviderFactory.TracingService tracingService2 = (OAuthProviderFactory.TracingService)tracingService;
			return OAuthProviderFactory.CreateOAuthProvider(dataSourceReference.NonNormalizedResource, application, tracingService2.ActivityId, tracingService2.CorrelationId);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000DB1C File Offset: 0x0000BD1C
		public static bool TryGetOAuthResource(this DataSourceReference dataSourceReference, IOAuthTracingService tracingService, out OAuthResource resource)
		{
			if (dataSourceReference == null)
			{
				throw new ArgumentNullException("dataSourceReference");
			}
			OAuthProviderFactory.TracingService tracingService2 = (OAuthProviderFactory.TracingService)tracingService;
			return OAuthProviderFactory.TryCreateOAuthResource(dataSourceReference.NonNormalizedResource, tracingService2.ActivityId, tracingService2.CorrelationId, out resource);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000DB56 File Offset: 0x0000BD56
		public static IOAuthTracingService NewTracingService()
		{
			return new OAuthProviderFactory.TracingService(EventSource.CurrentThreadActivityId);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000DB62 File Offset: 0x0000BD62
		public static IOAuthTracingService NewTracingService(Guid activityId)
		{
			return new OAuthProviderFactory.TracingService(activityId);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000DB6A File Offset: 0x0000BD6A
		public static IOAuthTracingService NewTracingService(string correlationId)
		{
			return new OAuthProviderFactory.TracingService(EventSource.CurrentThreadActivityId, correlationId);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000DB77 File Offset: 0x0000BD77
		public static IOAuthTracingService NewTracingService(Guid activityId, string correlationId)
		{
			return new OAuthProviderFactory.TracingService(activityId, correlationId);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000DB80 File Offset: 0x0000BD80
		private static IOAuthProvider CreateOAuthProvider(IResource resource, OAuthClientApplication application, Guid activityId, string correlationId)
		{
			string text;
			if (!OAuthProviderFactory.ShouldUseContainer(out text))
			{
				OAuthSettings.InitializeTls12And13();
				return OAuthProviderFactory.GetOAuthProvider(resource, application, new Guid?(activityId), correlationId);
			}
			return OAuthProviderFactory.CreateProvider(text, resource, application, activityId, correlationId);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		private static bool TryCreateOAuthResource(IResource resource, Guid activityId, string correlationId, out OAuthResource oAuthResource)
		{
			string text;
			if (!OAuthProviderFactory.ShouldUseContainer(out text))
			{
				OAuthSettings.InitializeTls12And13();
				return OAuthProviderFactory.TryGetOAuthResource(resource, activityId, correlationId, out oAuthResource);
			}
			return OAuthProviderFactory.CreateProvider(text, resource, null, activityId, correlationId).TryCreateResource(out oAuthResource);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000DBEC File Offset: 0x0000BDEC
		private static OAuthProviderFactory.IsolatedOAuthProvider CreateProvider(string poolIdentity, IResource resource, OAuthClientApplication application, Guid activityId, string correlationId)
		{
			ResourceKindInfo resourceKindInfo;
			string text;
			if (!MashupEngines.Version1.TryLookupResourceKind(resource.Kind, out resourceKindInfo, out text))
			{
				throw new NotSupportedException(ProviderErrorStrings.UnsupportedDataSourceKind(resource.Kind));
			}
			return new OAuthProviderFactory.IsolatedOAuthProvider(poolIdentity, text, resource, application, activityId, correlationId);
		}

		// Token: 0x040001CF RID: 463
		private static readonly ITracingService tracingService = ProviderTracing.Service;

		// Token: 0x040001D0 RID: 464
		private static readonly object syncRoot = new object();

		// Token: 0x040001D1 RID: 465
		internal static bool enableContainers;

		// Token: 0x040001D2 RID: 466
		internal static string connectionPoolIdentity;

		// Token: 0x02000082 RID: 130
		private class TracingService : IOAuthTracingService
		{
			// Token: 0x060004E7 RID: 1255 RVA: 0x00012024 File Offset: 0x00010224
			public TracingService(Guid activityId)
			{
				this.evaluationConstants = new EvaluationConstants(activityId, string.Empty, null).AddTraceConstant("HostProcessId", Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture), false);
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x0001206C File Offset: 0x0001026C
			public TracingService(Guid activityId, string correlationId)
			{
				this.evaluationConstants = new EvaluationConstants(activityId, correlationId, null).AddTraceConstant("HostProcessId", Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture), false);
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x000120B0 File Offset: 0x000102B0
			public void WriteTrace(string traceName, TraceEventType severityLevel, Dictionary<string, object> traceValues, bool isPii)
			{
				using (IHostTrace hostTrace = OAuthProviderFactory.tracingService.CreateTrace(this.evaluationConstants, traceName, severityLevel, false, null))
				{
					bool flag = false;
					foreach (KeyValuePair<string, object> keyValuePair in traceValues)
					{
						Exception ex = keyValuePair.Value as Exception;
						if (ex != null)
						{
							if (!flag)
							{
								hostTrace.Add(ex, true);
							}
							flag = true;
						}
						else
						{
							hostTrace.Add(keyValuePair.Key, keyValuePair.Value, isPii);
						}
					}
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001215C File Offset: 0x0001035C
			public Guid ActivityId
			{
				get
				{
					return this.evaluationConstants.ActivityId;
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x060004EB RID: 1259 RVA: 0x00012169 File Offset: 0x00010369
			public string CorrelationId
			{
				get
				{
					return this.evaluationConstants.CorrelationId;
				}
			}

			// Token: 0x0400029B RID: 667
			private readonly IEvaluationConstants evaluationConstants;
		}

		// Token: 0x02000083 RID: 131
		private sealed class IsolatedOAuthProvider : IOAuthProvider
		{
			// Token: 0x060004EC RID: 1260 RVA: 0x00012176 File Offset: 0x00010376
			public IsolatedOAuthProvider(string poolIdentity, string moduleName, IResource resource, OAuthClientApplication application, Guid activityId, string correlationId)
			{
				this.connectionString = OAuthProviderFactory.IsolatedOAuthProvider.MakeConnectionString(poolIdentity, moduleName, resource, application, activityId, correlationId);
				this.poolIdentity = poolIdentity;
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x0001219C File Offset: 0x0001039C
			public OAuthBrowserNavigation StartLogin(string state, string display)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Length = 0;
				stringBuilder.Append("StartLogin(");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(stringBuilder, state);
				stringBuilder.Append(", ");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(stringBuilder, display);
				stringBuilder.Append(")");
				OAuthBrowserNavigation oauthBrowserNavigation;
				try
				{
					oauthBrowserNavigation = this.Evaluate<OAuthBrowserNavigation>(stringBuilder.ToString(), delegate(MashupReader reader)
					{
						byte[] array = null;
						if (!reader.IsDBNull(4))
						{
							array = (byte[])reader.GetValue(4);
						}
						return new OAuthBrowserNavigation(new Uri(reader.GetString(0)), new Uri(reader.GetString(1)), reader.GetInt32(2), reader.GetInt32(3), array);
					});
				}
				catch (MashupValueException ex)
				{
					throw new OAuthException(ex.Message);
				}
				return oauthBrowserNavigation;
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x00012234 File Offset: 0x00010434
			public TokenCredential FinishLogin(byte[] serializedContext, Uri callbackUri, string state)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Length = 0;
				stringBuilder.Append("FinishLogin(");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(stringBuilder, serializedContext);
				stringBuilder.Append(", ");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(stringBuilder, callbackUri.AbsoluteUri);
				stringBuilder.Append(", ");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(stringBuilder, state);
				stringBuilder.Append(")");
				TokenCredential tokenCredential;
				try
				{
					tokenCredential = this.Evaluate<TokenCredential>(stringBuilder.ToString(), (MashupReader reader) => new TokenCredential(reader.GetString(0), reader.GetString(1), reader.GetString(2), OAuthProviderFactory.IsolatedOAuthProvider.ReadDictionary(reader, 3)));
				}
				catch (MashupValueException ex)
				{
					throw new OAuthException(ex.Message);
				}
				return tokenCredential;
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x000122E4 File Offset: 0x000104E4
			public TokenCredential Refresh(TokenCredential credential)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Length = 0;
				stringBuilder.Append("Refresh(");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(stringBuilder, credential);
				stringBuilder.Append(")");
				TokenCredential tokenCredential;
				try
				{
					tokenCredential = this.Evaluate<TokenCredential>(stringBuilder.ToString(), (MashupReader reader) => new TokenCredential(reader.GetString(0), reader.GetString(1), reader.GetString(2), OAuthProviderFactory.IsolatedOAuthProvider.ReadDictionary(reader, 3)));
				}
				catch (MashupValueException ex)
				{
					throw new OAuthException(ex.Message);
				}
				return tokenCredential;
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x00012368 File Offset: 0x00010568
			public Uri Logout(string accessToken)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Length = 0;
				stringBuilder.Append("Logout(");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(stringBuilder, accessToken);
				stringBuilder.Append(")");
				Uri uri;
				try
				{
					uri = this.Evaluate<Uri>(stringBuilder.ToString(), (MashupReader reader) => new Uri(reader.GetString(0)));
				}
				catch (MashupValueException ex)
				{
					throw new OAuthException(ex.Message);
				}
				return uri;
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x000123EC File Offset: 0x000105EC
			public bool TryCreateResource(out OAuthResource resource)
			{
				bool flag;
				try
				{
					resource = this.Evaluate<OAuthResource>("GetResource()", (MashupReader reader) => new OAuthResource(reader.GetString(0), reader.GetString(1), new SecureTokenService(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)), OAuthProviderFactory.IsolatedOAuthProvider.ReadDictionary(reader, 6)));
					flag = true;
				}
				catch (Exception ex) when (ProviderTracing.TraceIsSafeException("IsolatedOAuthProvider/TryCreateResource", ex, null, null))
				{
					resource = null;
					flag = false;
				}
				return flag;
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x00012464 File Offset: 0x00010664
			private static Dictionary<string, string> ReadDictionary(MashupReader reader, int startPosition)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				for (int i = startPosition; i < reader.FieldCount; i++)
				{
					dictionary[reader.GetName(i)] = reader.GetString(i);
				}
				return dictionary;
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x000124A0 File Offset: 0x000106A0
			private static string MakeConnectionString(string poolIdentity, string moduleName, IResource resource, OAuthClientApplication application, Guid activityId, string correlationId)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("[Requires=[Core=\"[0.0,)\", Web=\"[0.0,)\", OAuth=\"[0.0,)\"");
				if (!string.IsNullOrEmpty(moduleName) && moduleName != "Web")
				{
					stringBuilder.Append(", ");
					OAuthProviderFactory.IsolatedOAuthProvider.AppendFieldIdentifier(stringBuilder, moduleName);
					stringBuilder.Append("=\"[0.0,)\"");
				}
				stringBuilder.AppendLine("]] section Section1;");
				stringBuilder.AppendLine();
				stringBuilder.Append("Resource = ");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(stringBuilder, resource);
				stringBuilder.AppendLine(";");
				stringBuilder.Append("ClientApplication = ");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(stringBuilder, application);
				stringBuilder.AppendLine(";");
				stringBuilder.Append("\r\nFormat = (r, f) => Record.RemoveFields(r & (Record.FieldOrDefault(r, f) ?? []), f, MissingField.Ignore);\r\n\r\nshared StartLogin = (state, display) => Table.FromRecords({OAuth.StartLogin(Resource, ClientApplication, state, display)});\r\n\r\nshared FinishLogin = (context, callbackUri, state) => Table.FromRecords({OAuth.FinishLogin(Resource, ClientApplication, context, callbackUri, state)});\r\n\r\nshared Refresh = (token) => Table.FromRecords({OAuth.Refresh(Resource, ClientApplication, token)});\r\n\r\nshared Logout = (accessToken) => OAuth.Logout(Resource, ClientApplication, accessToken);\r\n\r\nshared GetResource = () => Table.ExpandRecordColumn(Table.FromRecords({Format(OAuth.Resource(Resource), \"Properties\")}), \"TokenService\", {\"Authority\", \"AuthorizationUri\", \"TokenUri\", \"LogoutUri\"});\r\n");
				return new MashupConnectionStringBuilder
				{
					Mashup = stringBuilder.ToString(),
					DataSourceSettings = DataSourceSettings.Create(DataSource.DefaultForKind("Web"), DataSourceSetting.CreateAnonymousCredential()),
					ActivityId = new Guid?(activityId),
					CorrelationId = correlationId,
					FastCombine = true,
					ConfigurationProperties = "{\"AadSettings\":" + OAuthProviderFactory.IsolatedOAuthProvider.FormatJson(AadOAuthProvider.DefaultSettings.Serialize()) + "}"
				}.ToString();
			}

			// Token: 0x060004F4 RID: 1268 RVA: 0x000125C0 File Offset: 0x000107C0
			private T Evaluate<T>(string expression, Func<MashupReader, T> loader)
			{
				T t2;
				using (MashupConnection mashupConnection = new MashupConnection(this.connectionString))
				{
					mashupConnection.Pool = this.poolIdentity;
					mashupConnection.Open();
					using (MashupCommand mashupCommand = new MashupCommand(expression, mashupConnection))
					{
						using (MashupReader mashupReader = mashupCommand.ExecuteReader())
						{
							if (!mashupReader.Read())
							{
								throw new InvalidOperationException("Unreachable code path: no record found");
							}
							T t = loader(mashupReader);
							if (mashupReader.Read())
							{
								throw new InvalidOperationException("Unreachable code path: Unexpected record");
							}
							t2 = t;
						}
					}
				}
				return t2;
			}

			// Token: 0x060004F5 RID: 1269 RVA: 0x00012674 File Offset: 0x00010874
			private static void AppendLiteral(StringBuilder builder, IResource resource)
			{
				builder.Append("[Kind=");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(builder, resource.Kind);
				builder.Append(", Path=");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(builder, resource.NonNormalizedPath);
				builder.Append("]");
			}

			// Token: 0x060004F6 RID: 1270 RVA: 0x000126B4 File Offset: 0x000108B4
			private static void AppendLiteral(StringBuilder builder, OAuthClientApplication clientApp)
			{
				if (clientApp == null)
				{
					builder.Append("null");
					return;
				}
				builder.Append("[ClientId=");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(builder, clientApp.Id);
				builder.Append(", ClientSecret=");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(builder, clientApp.Secret);
				builder.Append(", CallbackUrl=");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(builder, clientApp.CallbackUrl);
				if (clientApp.SecretType != ClientApplicationSecretType.Default)
				{
					builder.Append(", ClientSecretType=");
					OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(builder, clientApp.SecretType);
				}
				builder.Append("]");
			}

			// Token: 0x060004F7 RID: 1271 RVA: 0x00012748 File Offset: 0x00010948
			private static void AppendLiteral(StringBuilder builder, TokenCredential credential)
			{
				builder.Append("[access_token=");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(builder, credential.AccessToken);
				builder.Append(", refresh_token=");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(builder, credential.RefreshToken);
				builder.Append(", expires=");
				OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(builder, credential.Expires);
				if (credential.Properties != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in credential.Properties)
					{
						builder.Append(", ");
						OAuthProviderFactory.IsolatedOAuthProvider.AppendFieldIdentifier(builder, keyValuePair.Key);
						builder.Append("=");
						OAuthProviderFactory.IsolatedOAuthProvider.AppendLiteral(builder, keyValuePair.Value);
					}
				}
				builder.Append("]");
			}

			// Token: 0x060004F8 RID: 1272 RVA: 0x00012824 File Offset: 0x00010A24
			private static void AppendLiteral(StringBuilder builder, string s)
			{
				if (s == null)
				{
					builder.Append("null");
					return;
				}
				builder.Append(MashupEngines.Version1.EscapeString(s));
			}

			// Token: 0x060004F9 RID: 1273 RVA: 0x00012848 File Offset: 0x00010A48
			private static void AppendLiteral(StringBuilder builder, byte[] b)
			{
				if (b == null)
				{
					builder.Append("null");
					return;
				}
				builder.Append("Binary.FromText(");
				builder.Append(MashupEngines.Version1.EscapeString(Convert.ToBase64String(b)));
				builder.Append(", BinaryEncoding.Base64)");
			}

			// Token: 0x060004FA RID: 1274 RVA: 0x00012894 File Offset: 0x00010A94
			private static void AppendLiteral(StringBuilder builder, ClientApplicationSecretType value)
			{
				switch (value)
				{
				case ClientApplicationSecretType.Default:
					builder.Append("ClientApplicationSecretType.Default");
					return;
				case ClientApplicationSecretType.Literal:
					builder.Append("ClientApplicationSecretType.Literal");
					return;
				case ClientApplicationSecretType.Thumbprint:
					builder.Append("ClientApplicationSecretType.Thumbprint");
					return;
				case ClientApplicationSecretType.SubjectNameIssuer:
					builder.Append("ClientApplicationSecretType.SubjectNameIssuer");
					return;
				default:
					return;
				}
			}

			// Token: 0x060004FB RID: 1275 RVA: 0x000128EB File Offset: 0x00010AEB
			private static void AppendFieldIdentifier(StringBuilder builder, string identifier)
			{
				builder.Append(MashupEngines.Version1.EscapeFieldIdentifier(identifier));
			}

			// Token: 0x060004FC RID: 1276 RVA: 0x00012900 File Offset: 0x00010B00
			private static string FormatJson(string s)
			{
				StringBuilder stringBuilder = new StringBuilder(s.Length + 20);
				stringBuilder.Append('"');
				foreach (char c in s)
				{
					if (c == '"' || c == '\\' || c == '/')
					{
						stringBuilder.Append('\\');
						stringBuilder.Append(c);
					}
					else
					{
						if (c < ' ' || c > '\u007f')
						{
							throw new NotSupportedException(c.ToString());
						}
						stringBuilder.Append(c);
					}
				}
				stringBuilder.Append('"');
				return stringBuilder.ToString();
			}

			// Token: 0x0400029C RID: 668
			private readonly string connectionString;

			// Token: 0x0400029D RID: 669
			private readonly string poolIdentity;
		}
	}
}
