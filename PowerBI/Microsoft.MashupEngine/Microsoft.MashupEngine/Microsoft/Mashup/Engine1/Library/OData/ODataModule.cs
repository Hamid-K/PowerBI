using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.OData.V3;
using Microsoft.Mashup.Engine1.Library.OData.V4;
using Microsoft.Mashup.Engine1.Library.OData.V4_7;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000737 RID: 1847
	internal sealed class ODataModule : Module
	{
		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000AF418 File Offset: 0x000AD618
		public override string Name
		{
			get
			{
				return "OData";
			}
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x000AF41F File Offset: 0x000AD61F
		public override Keys ExportKeys
		{
			get
			{
				if (ODataModule.exportKeys == null)
				{
					ODataModule.exportKeys = Keys.New(3, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "OData.Feed";
						case 1:
							return ODataModule.ODataOmitValues.Type.GetName();
						case 2:
							return ODataModule.ODataOmitValues.Nulls.GetName();
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					});
				}
				return ODataModule.exportKeys;
			}
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000AF457 File Offset: 0x000AD657
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { ODataModule.resourceKindInfo };
			}
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x000AF468 File Offset: 0x000AD668
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new ODataModule.FeedFunctionValue(hostEnvironment);
				case 1:
					return ODataModule.ODataOmitValues.Type;
				case 2:
					return ODataModule.ODataOmitValues.Nulls;
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x000AF499 File Offset: 0x000AD699
		public static FunctionValue CreateGetFeedFunction(HttpResource resource, TextValue serviceUri, Value headers, IEngineHost host, ResourceCredentialCollection credentials, ODataSettingsBase settings, ODataUserSettings userSettings = null)
		{
			return new ODataModule.GetFeedFunctionValue(resource, serviceUri, headers, host, credentials, settings, userSettings);
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000AF4AC File Offset: 0x000AD6AC
		public static Value GetFeed(HttpResource resource, TextValue serviceUriValue, TextValue uriValue, Value headers, IEngineHost host, ResourceCredentialCollection credentials, ODataSettingsBase settings, ODataUserSettings userSettings, bool useCachedCredentials)
		{
			Value value;
			using (IHostTrace hostTrace = TracingService.CreateTrace(host, "Engine/IO/OData/Create", TraceEventType.Information, resource.Resource))
			{
				hostTrace.Add("ServiceUri", serviceUriValue, true);
				hostTrace.Add("RequestUrl", uriValue, true);
				hostTrace.Add("RequestHeaders", headers, true);
				try
				{
					Uri uri = ODataUriCommon.ConvertToUri(serviceUriValue);
					ODataUriCommon.ValidateHttpAbsolute(uri);
					Uri uri2 = ODataUriCommon.ConvertToUri(uriValue);
					ODataUriCommon.ValidateHttpAbsolute(uri2);
					headers = (headers.IsNull ? RequestHeaders.DefaultUserAgentHeader : RequestHeaders.DefaultUserAgentHeader.Concatenate(headers));
					ODataServerVersion startingVersion = ODataModule.GetStartingVersion(settings, userSettings);
					StandaloneODataFallbackVersionHandler standaloneODataFallbackVersionHandler = new StandaloneODataFallbackVersionHandler(host, resource, uri2, startingVersion);
					HttpResponseData httpResponseData;
					if (ODataModule.TryDetectODataV4Feed(resource, headers, host, credentials, userSettings, standaloneODataFallbackVersionHandler, uri, uri2, settings, out httpResponseData))
					{
						if (userSettings.UseODataLib7)
						{
							Microsoft.Mashup.Engine1.Library.OData.V4_7.ODataSettings odataSettings = new Microsoft.Mashup.Engine1.Library.OData.V4_7.ODataSettings(host, resource, uri2)
							{
								ServerVersion = standaloneODataFallbackVersionHandler.ServerVersion,
								Cache = settings.Cache
							};
							value = Microsoft.Mashup.Engine1.Library.OData.V4_7.ODataResponse.Create(uri2, httpResponseData, resource, headers, credentials, host, odataSettings, userSettings);
						}
						else
						{
							Microsoft.Mashup.Engine1.Library.OData.V4.ODataSettings odataSettings2 = new Microsoft.Mashup.Engine1.Library.OData.V4.ODataSettings(host, resource, uri2)
							{
								ServerVersion = standaloneODataFallbackVersionHandler.ServerVersion,
								Cache = settings.Cache
							};
							value = Microsoft.Mashup.Engine1.Library.OData.V4.ODataResponse.Create(uri2, httpResponseData, resource, headers, credentials, host, odataSettings2, userSettings);
						}
					}
					else
					{
						Microsoft.Mashup.Engine1.Library.OData.V3.ODataSettings odataSettings3 = new Microsoft.Mashup.Engine1.Library.OData.V3.ODataSettings(host, resource, uri2)
						{
							ServerVersion = standaloneODataFallbackVersionHandler.ServerVersion,
							Cache = settings.Cache
						};
						Microsoft.Mashup.Engine1.Library.OData.V3.ODataResponse odataResponse = Microsoft.Mashup.Engine1.Library.OData.V3.ODataResponse.Create(httpResponseData, uri, resource, uri2, headers, credentials, host, odataSettings3, userSettings);
						if (odataResponse.ServiceDocument != null)
						{
							value = Microsoft.Mashup.Engine1.Library.OData.V3.ODataEnvironment.Create(odataResponse.ServiceDocument, headers, resource, credentials, host, odataSettings3, userSettings, useCachedCredentials).CreateCatalogTableValue(host);
						}
						else
						{
							value = odataResponse.Result;
						}
					}
				}
				catch (IOException ex)
				{
					throw FileErrors.HandleException(ex, uriValue);
				}
				catch (ODataTestConnectionFallbackException ex2)
				{
					value = ex2.ToTestConnectionTable(userSettings, (ODataUserSettings newSettings) => ODataModule.GetFeed(resource, serviceUriValue, uriValue, headers, host, credentials, settings, newSettings, useCachedCredentials));
				}
				catch (Exception ex3)
				{
					SafeExceptions.TraceIsSafeException(hostTrace, ex3);
					throw;
				}
			}
			return value;
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000AF800 File Offset: 0x000ADA00
		private static ODataServerVersion GetStartingVersion(ODataSettingsBase settings, ODataUserSettings userSettings)
		{
			if (userSettings.ODataVersion != ODataServerVersion.All)
			{
				return userSettings.ODataVersion;
			}
			return settings.ServerVersion;
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000AF818 File Offset: 0x000ADA18
		private static bool TryDetectODataV4Feed(HttpResource resource, Value headers, IEngineHost host, ResourceCredentialCollection credentials, ODataUserSettings userSettings, StandaloneODataFallbackVersionHandler fallbackHandler, Uri serviceUri, Uri uri, ODataSettingsBase settings, out HttpResponseData responseData)
		{
			responseData = fallbackHandler.HandleVersionFallback<HttpResponseDataWithContextUri>(delegate
			{
				string text;
				if (fallbackHandler.ServerVersion == ODataServerVersion.All)
				{
					text = "application/json;odata.metadata=minimal;q=1.0,application/json;odata=minimalmetadata;q=0.9,application/atomsvc+xml;q=0.8,application/atom+xml;q=0.8,application/xml;q=0.7,text/plain;q=0.7";
				}
				else if (fallbackHandler.ServerVersion == ODataServerVersion.V4)
				{
					text = "application/json;odata.metadata=minimal";
				}
				else if (fallbackHandler.ServerVersion == ODataServerVersion.V3)
				{
					text = "application/json;odata=minimalmetadata;q=1.0,application/atomsvc+xml;q=0.8,application/atom+xml;q=0.8,application/xml;q=0.7,text/plain;q=0.7";
				}
				else
				{
					text = "application/atomsvc+xml;q=0.8,application/atom+xml;q=0.8,application/xml;q=0.7,text/plain;q=0.7";
				}
				HttpResponseData responseStream = HttpResponseHandler.GetResponseStream(resource, serviceUri, uri, headers, credentials, text, true, false, host, settings, userSettings, fallbackHandler.ServerVersion, false);
				if (!responseStream.ContentType.Contains("application/json") && (fallbackHandler.ServerVersion == ODataServerVersion.V4 || responseStream.Headers.ContainsKey("OData-Version")))
				{
					responseStream.Dispose();
					fallbackHandler.FallbackToOlderVersion(ODataCommonErrors.UnsupportedFormat(responseStream.ContentType));
				}
				return HttpResponseDataWithContextUri.FixupRelativeMetadataUrl(responseStream);
			}, false);
			return responseData.Headers.ContainsKey("OData-Version") && responseData.ContentType.Contains("application/json");
		}

		// Token: 0x04001C3C RID: 7228
		public const string ODataFeedFunc = "OData.Feed";

		// Token: 0x04001C3D RID: 7229
		public const string DataSourceNameString = "OData";

		// Token: 0x04001C3E RID: 7230
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("ApiKeyName", NullableTypeValue.Text),
			new OptionItem("Concurrent", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("EnableBatch", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("ExcludedFromCacheKey", NullableTypeValue.List, ListValue.Empty, OptionItemOption.None, null, null),
			new OptionItem("FunctionOverloads", NullableTypeValue.Logical),
			new OptionItem("Headers", NullableTypeValue.Record, RecordValue.Empty, OptionItemOption.None, null, null),
			new OptionItem("IncludeAnnotations", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("IncludeMetadataAnnotations", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("MaxUriLength", NullableTypeValue.Number, NumberValue.New(1400), OptionItemOption.None, null, null),
			new OptionItem("MoreColumns", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("Query", NullableTypeValue.Record),
			new OptionItem("ODataVersion", NullableTypeValue.Number),
			new OptionItem("OmitValues", ODataModule.ODataOmitValues.Type.Nullable, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("Timeout", NullableTypeValue.Duration, DurationValue.New(TimeSpan.FromSeconds(600.0)), OptionItemOption.None, null, null),
			new OptionItem("Implementation", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null)
		});

		// Token: 0x04001C3F RID: 7231
		private static readonly ResourceKindInfo resourceKindInfo = new UriResourceKindInfo("OData", null, new AuthenticationInfo[]
		{
			ResourceHelpers.AnonymousAuth,
			ResourceHelpers.WindowsAuthAlternateCredentials,
			ResourceHelpers.BasicAuth,
			new WebApiAuthenticationInfo(),
			new KeyAuthenticationInfo(),
			new AadAuthenticationInfo
			{
				ClientApplicationType = OAuthClientApplicationType.Optional,
				ProviderFactory = new OAuthFactory((OAuthServices services, string url) => AadOAuthProvider.CreateResourceForUrl(services, url, null), (OAuthServices services, OAuthClientApplication app, string url) => new AadOAuthProvider(services, app, url, null))
			}
		}, null, false, false, false, null, new DataSourceLocationFactory[] { ODataDataSourceLocation.Factory });

		// Token: 0x04001C40 RID: 7232
		private static Keys exportKeys;

		// Token: 0x02000738 RID: 1848
		private enum Exports
		{
			// Token: 0x04001C42 RID: 7234
			Feed,
			// Token: 0x04001C43 RID: 7235
			ODataOmitValues_Type,
			// Token: 0x04001C44 RID: 7236
			ODataOmitValues_Nulls,
			// Token: 0x04001C45 RID: 7237
			Count
		}

		// Token: 0x02000739 RID: 1849
		private sealed class FeedFunctionValue : NativeFunctionValue3<Value, TextValue, Value, Value>
		{
			// Token: 0x06003704 RID: 14084 RVA: 0x000AFAE4 File Offset: 0x000ADCE4
			public FeedFunctionValue(IEngineHost host)
				: base(TypeValue.Any, 1, "serviceUri", TypeValue.Text, "headers", NullableTypeValue.Record, "options", NullableTypeValue.Any)
			{
				this.host = host;
			}

			// Token: 0x06003705 RID: 14085 RVA: 0x000AFB24 File Offset: 0x000ADD24
			public override Value TypedInvoke(TextValue serviceUri, Value headers, Value options)
			{
				IExtensibilityService extensibilityService = this.host.QueryService<IExtensibilityService>();
				bool flag = extensibilityService != null;
				ODataUserSettings odataUserSettings = ODataUserSettings.BuildUserSettings(headers, options, flag);
				Uri uri = ODataUriCommon.ConvertToUri(serviceUri);
				uri = odataUserSettings.ApplyQueryOptions(uri);
				HttpResource httpResource = ((flag && extensibilityService.ImpersonateResource) ? HttpResource.New(extensibilityService.CurrentResource, false) : HttpResource.New("OData", uri.ToString(), true));
				ResourceCredentialCollection resourceCredentialCollection;
				HttpServices.VerifyPermissionAndGetCredentials(this.host, httpResource.Resource, odataUserSettings.ApiKeyName != null, out resourceCredentialCollection);
				if (odataUserSettings.Headers.IsRecord && resourceCredentialCollection.Count > 0 && !flag)
				{
					RequestHeaders.ThrowIfHeaderNotAllowed(odataUserSettings.Headers.AsRecord);
				}
				Microsoft.Mashup.Engine1.Library.OData.V4.ODataSettings odataSettings = new Microsoft.Mashup.Engine1.Library.OData.V4.ODataSettings(this.host, httpResource, uri)
				{
					ServerVersion = ODataServerVersion.All
				};
				return ODataModule.GetFeed(httpResource, serviceUri, serviceUri, Value.Null, this.host, resourceCredentialCollection, odataSettings, odataUserSettings, false);
			}

			// Token: 0x170012E7 RID: 4839
			// (get) Token: 0x06003706 RID: 14086 RVA: 0x000AF418 File Offset: 0x000AD618
			public override string PrimaryResourceKind
			{
				get
				{
					return "OData";
				}
			}

			// Token: 0x06003707 RID: 14087 RVA: 0x000AFC00 File Offset: 0x000ADE00
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (ODataModule.FeedFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetConstant("url", out value) && value.IsText)
				{
					location = new ODataDataSourceLocation();
					location.Address["url"] = ODataUriCommon.ConvertToUri(value.AsText).ToString();
					location.TrySetAddressString("resource", dictionary, "resource");
					Value value2 = Value.Null;
					IExpression expression2;
					if (dictionary.TryGetValue("headers", out expression2))
					{
						value2 = ExpressionAnalysis.GetValue(expression2);
					}
					Value value3 = Value.Null;
					IExpression expression3;
					if (dictionary.TryGetValue("options", out expression3))
					{
						value3 = ExpressionAnalysis.GetValue(expression3);
					}
					if (!value2.IsNull && value3.IsNull)
					{
						value3 = RecordValue.New(new NamedValue[]
						{
							new NamedValue("Headers", value2)
						});
					}
					bool flag = ExpressionAnalysis.IsPlaceholder(value3);
					if (value3.IsRecord)
					{
						foundOptions = ExpressionAnalysis.RemovePlaceholders(value3.AsRecord, out unknownOptions);
						Value @null = Value.Null;
						if (unknownOptions.Contains("ApiKeyName") || (foundOptions.TryGetValue("ApiKeyName", out @null) && !@null.IsText && !@null.IsNull))
						{
							flag = true;
						}
						if (@null.IsText)
						{
							location.Authentication = "query-string-key:" + @null.AsString;
							foundOptions = Library.Record.RemoveFields.Invoke(foundOptions, TextValue.New("ApiKeyName")).AsRecord;
						}
					}
					else
					{
						foundOptions = RecordValue.Empty;
						unknownOptions = Keys.Empty;
					}
					if (!flag)
					{
						return true;
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x04001C46 RID: 7238
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__url, _o_headers, _o_options)", "__func(__url, _o_headers, _o_options){[Name=__resource]}[Data]", "__func(__url, _o_headers, _o_options){[Name=__resource, Signature=__signature]}[Data]" });

			// Token: 0x04001C47 RID: 7239
			private readonly IEngineHost host;
		}

		// Token: 0x0200073A RID: 1850
		private sealed class GetFeedFunctionValue : NativeFunctionValue1<Value, Value>
		{
			// Token: 0x06003709 RID: 14089 RVA: 0x000AFDCC File Offset: 0x000ADFCC
			public GetFeedFunctionValue(HttpResource resource, TextValue serviceUri, Value headers, IEngineHost host, ResourceCredentialCollection credentials, ODataSettingsBase settings, ODataUserSettings userSettings = null)
				: base(TypeValue.Any, 0, "useConcurrentRequests", NullableTypeValue.Logical)
			{
				this.resource = resource;
				this.serviceUri = serviceUri;
				this.headers = headers;
				this.host = host;
				this.credentials = credentials;
				this.settings = settings;
				this.userSettings = userSettings;
			}

			// Token: 0x0600370A RID: 14090 RVA: 0x000AFE24 File Offset: 0x000AE024
			public override Value TypedInvoke(Value useConcurrentRequests)
			{
				ODataUserSettings odataUserSettings = this.userSettings ?? new ODataUserSettings();
				if (!useConcurrentRequests.IsNull)
				{
					odataUserSettings.UseConcurrentRequests = useConcurrentRequests.AsBoolean;
				}
				return ODataModule.GetFeed(this.resource, this.serviceUri, this.serviceUri, this.headers, this.host, this.credentials, this.settings, odataUserSettings, false);
			}

			// Token: 0x04001C48 RID: 7240
			private readonly HttpResource resource;

			// Token: 0x04001C49 RID: 7241
			private readonly TextValue serviceUri;

			// Token: 0x04001C4A RID: 7242
			private readonly Value headers;

			// Token: 0x04001C4B RID: 7243
			private readonly IEngineHost host;

			// Token: 0x04001C4C RID: 7244
			private readonly ResourceCredentialCollection credentials;

			// Token: 0x04001C4D RID: 7245
			private readonly ODataSettingsBase settings;

			// Token: 0x04001C4E RID: 7246
			private readonly ODataUserSettings userSettings;
		}

		// Token: 0x0200073B RID: 1851
		private static class ODataOmitValues
		{
			// Token: 0x04001C4F RID: 7247
			public static readonly TextEnumTypeValue Type = new TextEnumTypeValue("ODataOmitValues.Type");

			// Token: 0x04001C50 RID: 7248
			public static readonly TextValue Nulls = ODataModule.ODataOmitValues.Type.NewEnumValue("ODataOmitValues.Nulls", "nulls", null);
		}
	}
}
