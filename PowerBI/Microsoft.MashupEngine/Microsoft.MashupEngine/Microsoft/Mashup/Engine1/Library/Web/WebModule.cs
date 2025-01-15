using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Lines;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.SharePoint;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Web
{
	// Token: 0x020002A2 RID: 674
	internal sealed class WebModule : Module
	{
		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000378A3 File Offset: 0x00035AA3
		public override string Name
		{
			get
			{
				return "Web";
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000378AA File Offset: 0x00035AAA
		public override Keys ExportKeys
		{
			get
			{
				if (WebModule.exportKeys == null)
				{
					WebModule.exportKeys = Keys.New(11, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Web.Contents";
						case 1:
							return "Web.Headers";
						case 2:
							return "Soda.Feed";
						case 3:
							return WebModule.WebMethod.Type.GetName();
						case 4:
							return WebModule.WebMethod.Delete.GetName();
						case 5:
							return WebModule.WebMethod.Get.GetName();
						case 6:
							return WebModule.WebMethod.Head.GetName();
						case 7:
							return WebModule.WebMethod.Patch.GetName();
						case 8:
							return WebModule.WebMethod.Post.GetName();
						case 9:
							return WebModule.WebMethod.Put.GetName();
						case 10:
							return "WebAction.Request";
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					});
				}
				return WebModule.exportKeys;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000378E3 File Offset: 0x00035AE3
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[]
				{
					WebModule.webResourceKindInfo,
					WebModule.ftpResourceKindInfo
				};
			}
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x000378FC File Offset: 0x00035AFC
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new WebModule.WebContentsFunctionValue(hostEnvironment);
				case 1:
					return new WebModule.WebHeadersFunctionValue(hostEnvironment);
				case 2:
					return new WebModule.SodaFeedFunctionValue(hostEnvironment);
				case 3:
					return WebModule.WebMethod.Type;
				case 4:
					return WebModule.WebMethod.Delete;
				case 5:
					return WebModule.WebMethod.Get;
				case 6:
					return WebModule.WebMethod.Head;
				case 7:
					return WebModule.WebMethod.Patch;
				case 8:
					return WebModule.WebMethod.Post;
				case 9:
					return WebModule.WebMethod.Put;
				case 10:
					return new WebModule.WebActionFunctionValue(hostEnvironment);
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00037930 File Offset: 0x00035B30
		private static bool TryGetLocation(ExpressionPattern pattern, IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions, out Dictionary<string, IExpression> captures)
		{
			Value value;
			string text;
			string text2;
			if (pattern.TryMatch(expression, out captures) && captures.TryGetConstant("url", out value) && value.IsText && Resource.TryNormalizeWebUri(value.AsString, out text, out text2))
			{
				HttpDataSourceLocation httpDataSourceLocation = new HttpDataSourceLocation
				{
					Address = new Dictionary<string, object> { { "url", text2 } }
				};
				Value value2 = Value.Null;
				IExpression expression2;
				if (captures.TryGetValue("options", out expression2))
				{
					value2 = ExpressionAnalysis.GetValue(expression2);
				}
				if (value2.IsRecord)
				{
					foundOptions = ExpressionAnalysis.RemovePlaceholders(value2.AsRecord, out unknownOptions);
					Value @null = Value.Null;
					if (unknownOptions.Contains("ApiKeyName") || (foundOptions.TryGetValue("ApiKeyName", out @null) && !@null.IsText && !@null.IsNull))
					{
						foundOptions = null;
					}
					if (@null.IsText)
					{
						httpDataSourceLocation.Authentication = "query-string-key:" + @null.AsString;
						foundOptions = Library.Record.RemoveFields.Invoke(foundOptions, TextValue.New("ApiKeyName")).AsRecord;
					}
				}
				else if (value2.IsNull)
				{
					foundOptions = RecordValue.Empty;
					unknownOptions = Keys.Empty;
				}
				else
				{
					foundOptions = null;
					unknownOptions = null;
				}
				if (foundOptions != null)
				{
					location = httpDataSourceLocation;
					return true;
				}
			}
			location = null;
			foundOptions = null;
			unknownOptions = null;
			return false;
		}

		// Token: 0x04000839 RID: 2105
		public const string WebContents = "Web.Contents";

		// Token: 0x0400083A RID: 2106
		public const string WebHeaders = "Web.Headers";

		// Token: 0x0400083B RID: 2107
		public const string WebActionRequest = "WebAction.Request";

		// Token: 0x0400083C RID: 2108
		private static readonly OptionRecordDefinition baseOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Query", NullableTypeValue.Record, RecordValue.Empty, OptionItemOption.None, null, "Web"),
			new OptionItem("ApiKeyName", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "Web"),
			new OptionItem("Headers", NullableTypeValue.Record, RecordValue.Empty, OptionItemOption.None, null, "Web"),
			new OptionItem("Timeout", NullableTypeValue.Duration, DurationValue.New(TimeSpan.FromSeconds(100.0)), OptionItemOption.None, null, "Web"),
			new OptionItem("ExcludedFromCacheKey", NullableTypeValue.List, Value.Null, OptionItemOption.None, null, "Web"),
			new OptionItem("IsRetry", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, "Web"),
			new OptionItem("ManualStatusHandling", NullableTypeValue.List, Value.Null, OptionItemOption.None, null, "Web"),
			new OptionItem("RelativePath", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "Web")
		});

		// Token: 0x0400083D RID: 2109
		public static readonly OptionRecordDefinition WebContentsOptionRecord = WebModule.baseOptionRecord.Concatenate(new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Content", NullableTypeValue.Binary, Value.Null, OptionItemOption.None, null, "Web")
		}));

		// Token: 0x0400083E RID: 2110
		public static readonly OptionRecordDefinition WebHeadersOptionRecord = WebModule.baseOptionRecord;

		// Token: 0x0400083F RID: 2111
		public static readonly OptionRecordDefinition WebActionRequestOptionRecord = WebModule.baseOptionRecord.Concatenate(new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Content", NullableTypeValue.Binary, Value.Null, OptionItemOption.None, null, "WebAction")
		}));

		// Token: 0x04000840 RID: 2112
		private static readonly ResourceKindInfo webResourceKindInfo = new UriResourceKindInfo("Web", null, new AuthenticationInfo[]
		{
			ResourceHelpers.AnonymousAuth,
			ResourceHelpers.WindowsAuthAlternateCredentials,
			ResourceHelpers.BasicAuth,
			new WebApiAuthenticationInfo(),
			new AadAuthenticationInfo
			{
				ClientApplicationType = OAuthClientApplicationType.Optional,
				ProviderFactory = new OAuthFactory((OAuthServices services, string url) => AadOAuthProvider.CreateResourceForUrl(services, url, null), (OAuthServices services, OAuthClientApplication app, string url) => new AadOAuthProvider(services, app, url, null))
			}
		}, null, false, false, false, null, new DataSourceLocationFactory[] { HttpDataSourceLocation.Factory });

		// Token: 0x04000841 RID: 2113
		private static readonly ResourceKindInfo ftpResourceKindInfo = new FtpResourceKindInfo();

		// Token: 0x04000842 RID: 2114
		private static Keys exportKeys;

		// Token: 0x020002A3 RID: 675
		private enum Exports
		{
			// Token: 0x04000844 RID: 2116
			WebContents,
			// Token: 0x04000845 RID: 2117
			WebHeaders,
			// Token: 0x04000846 RID: 2118
			SodaFeed,
			// Token: 0x04000847 RID: 2119
			WebMethod_Type,
			// Token: 0x04000848 RID: 2120
			WebMethod_Delete,
			// Token: 0x04000849 RID: 2121
			WebMethod_Get,
			// Token: 0x0400084A RID: 2122
			WebMethod_Head,
			// Token: 0x0400084B RID: 2123
			WebMethod_Patch,
			// Token: 0x0400084C RID: 2124
			WebMethod_Post,
			// Token: 0x0400084D RID: 2125
			WebMethod_Put,
			// Token: 0x0400084E RID: 2126
			WebAction_Request,
			// Token: 0x0400084F RID: 2127
			Count
		}

		// Token: 0x020002A4 RID: 676
		public sealed class WebContentsFunctionValue : NativeFunctionValue2<BinaryValue, TextValue, Value>
		{
			// Token: 0x06001B12 RID: 6930 RVA: 0x00037CAA File Offset: 0x00035EAA
			public WebContentsFunctionValue(IEngineHost host)
				: this(host, "Web")
			{
			}

			// Token: 0x06001B13 RID: 6931 RVA: 0x00037CB8 File Offset: 0x00035EB8
			public WebContentsFunctionValue(IEngineHost host, string resourceKind)
				: base(TypeValue.Binary, 1, "url", TypeValue.Text, "options", WebModule.WebContentsFunctionValue.optionsType)
			{
				this.host = host;
				this.resourceKind = resourceKind;
			}

			// Token: 0x06001B14 RID: 6932 RVA: 0x00037CE8 File Offset: 0x00035EE8
			public override BinaryValue TypedInvoke(TextValue url, Value options)
			{
				bool flag = false;
				string text = null;
				IExtensibilityService extensibilityService = this.host.QueryService<IExtensibilityService>();
				if (extensibilityService != null)
				{
					if (extensibilityService.CurrentResource != null && extensibilityService.ImpersonateResource)
					{
						this.resourceKind = extensibilityService.CurrentResource.Kind;
						text = extensibilityService.CurrentResource.NonNormalizedPath;
					}
					flag = true;
				}
				Action<MashupHttpWebResponse, IHostTrace> action = null;
				if (this.resourceKind == "SharePoint")
				{
					action = new Action<MashupHttpWebResponse, IHostTrace>(SharePointUtil.HandleResponseError);
				}
				Request request = WebOptionsHelper.GetRequest(this.host, this.resourceKind, text, url, options, flag, action);
				ResourceCredentialCollection resourceCredentialCollection;
				request.VerifyPermissionAndGetCredentials(out resourceCredentialCollection);
				string text2 = url.AsString;
				if (resourceCredentialCollection.IsSharePointOnline())
				{
					string text3;
					if (SharePointFile.IsFile(this.host, url.AsString))
					{
						TextValue textValue = TextValue.New(SharePointFile.GetCsomFileUrl(this.host, this.resourceKind, url, resourceCredentialCollection));
						request = WebOptionsHelper.GetRequest(this.host, this.resourceKind, flag ? text : url.AsString, textValue, options, flag, new Action<MashupHttpWebResponse, IHostTrace>(SharePointUtil.HandleResponseError));
					}
					else if (SharePointFile.TryGetFilePath(url, out text3))
					{
						text2 = text3;
					}
				}
				Value value;
				if (options.IsRecord && options.AsRecord.TryGetValue("ManualCredentials", out value) && value.AsLogical.AsBoolean)
				{
					resourceCredentialCollection = new ResourceCredentialCollection(request.RequestResource, Array.Empty<IResourceCredential>());
				}
				bool flag2 = !request.Headers.IsNull;
				bool flag3 = !request.Content.IsNull;
				bool flag4 = !request.Timeout.IsNull;
				bool flag5 = request.NonErrors != null;
				if ((request is FtpRequest || request is FileRequest) && (flag2 || flag3 || flag4 || flag5))
				{
					string text4;
					if (flag2)
					{
						text4 = "Headers";
					}
					else if (flag3)
					{
						text4 = "Content";
					}
					else if (flag4)
					{
						text4 = "Timeout";
					}
					else
					{
						text4 = "ManualStatusHandling";
					}
					throw ValueException.NewExpressionError<Message1>(Strings.WebContentsInvalidOption(text4), url, null);
				}
				if (resourceCredentialCollection.Count > 0 && request is FileRequest)
				{
					WindowsCredential windowsCredential = resourceCredentialCollection[0] as WindowsCredential;
					if (windowsCredential != null && windowsCredential.OverrideCurrentUser)
					{
						throw DataSourceException.NewDataSourceError<Message0>(this.host, Strings.NoWebContentsWithFileAndImpersonation, request.RequestResource, null, null);
					}
				}
				if (!flag && resourceCredentialCollection.Count > 0 && request.Method == "POST")
				{
					throw DataSourceException.NewDataSourceError<Message0>(this.host, Strings.WebContentsRequestWithCredentialsError, request.RequestResource, null, null);
				}
				if (!flag && resourceCredentialCollection.Count > 0 && flag2)
				{
					RequestHeaders.ThrowIfHeaderNotAllowed(request.Headers.AsRecord);
				}
				return WebContentsBinaryValue.New(request, resourceCredentialCollection, options, flag, text2);
			}

			// Token: 0x17000D05 RID: 3333
			// (get) Token: 0x06001B15 RID: 6933 RVA: 0x000378A3 File Offset: 0x00035AA3
			public override string PrimaryResourceKind
			{
				get
				{
					return "Web";
				}
			}

			// Token: 0x06001B16 RID: 6934 RVA: 0x00037F88 File Offset: 0x00036188
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				return WebModule.TryGetLocation(WebModule.WebContentsFunctionValue.pattern, expression, out location, out foundOptions, out unknownOptions, out dictionary);
			}

			// Token: 0x04000850 RID: 2128
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__url, _o_options)" });

			// Token: 0x04000851 RID: 2129
			private static readonly TypeValue optionsType = WebModule.WebContentsOptionRecord.CreateRecordType().Nullable;

			// Token: 0x04000852 RID: 2130
			private readonly IEngineHost host;

			// Token: 0x04000853 RID: 2131
			private string resourceKind;
		}

		// Token: 0x020002A5 RID: 677
		public sealed class WebHeadersFunctionValue : NativeFunctionValue2<RecordValue, TextValue, Value>
		{
			// Token: 0x06001B18 RID: 6936 RVA: 0x00037FD4 File Offset: 0x000361D4
			public WebHeadersFunctionValue(IEngineHost host)
				: base(TypeValue.Record, 1, "url", TypeValue.Text, "options", WebModule.WebHeadersFunctionValue.optionsType)
			{
				this.host = host;
				this.resourceKind = "Web";
			}

			// Token: 0x06001B19 RID: 6937 RVA: 0x00038008 File Offset: 0x00036208
			public override RecordValue TypedInvoke(TextValue url, Value options)
			{
				Uri uri = UriHelper.CreateAbsoluteUriFromValue(url);
				if (!UriHelper.IsHttpUri(uri) && !UriHelper.IsHttpsUri(uri))
				{
					throw ValueException.NewExpressionError<Message1>(Strings.WebActionSchemeUnsupported(uri.Scheme), url, null);
				}
				bool flag = false;
				string text = null;
				IExtensibilityService extensibilityService = this.host.QueryService<IExtensibilityService>();
				if (extensibilityService != null)
				{
					if (extensibilityService.CurrentResource != null && extensibilityService.ImpersonateResource)
					{
						this.resourceKind = extensibilityService.CurrentResource.Kind;
						text = extensibilityService.CurrentResource.NonNormalizedPath;
					}
					flag = true;
				}
				Action<MashupHttpWebResponse, IHostTrace> action = null;
				if (this.resourceKind == "SharePoint")
				{
					action = new Action<MashupHttpWebResponse, IHostTrace>(SharePointUtil.HandleResponseError);
				}
				Request request = WebOptionsHelper.GetRequest(this.host, this.resourceKind, text, url, options, flag, action);
				ResourceCredentialCollection resourceCredentialCollection;
				request.VerifyPermissionAndGetCredentials(out resourceCredentialCollection);
				if (resourceCredentialCollection.IsSharePointOnline() && SharePointFile.IsFile(this.host, url.AsString))
				{
					TextValue textValue = TextValue.New(SharePointFile.GetCsomFileUrl(this.host, this.resourceKind, url, resourceCredentialCollection));
					request = WebOptionsHelper.GetRequest(this.host, this.resourceKind, flag ? text : url.AsString, textValue, options, flag, new Action<MashupHttpWebResponse, IHostTrace>(SharePointUtil.HandleResponseError));
				}
				Value value;
				if (options.IsRecord && options.AsRecord.TryGetValue("ManualCredentials", out value) && value.AsLogical.AsBoolean)
				{
					resourceCredentialCollection = new ResourceCredentialCollection(request.RequestResource, Array.Empty<IResourceCredential>());
				}
				if (!request.Content.IsNull)
				{
					throw ValueException.NewExpressionError<Message1>(Strings.GenericInvalidOption("Content"), request.Content, null);
				}
				if (!flag && resourceCredentialCollection.Count > 0 && !request.Headers.IsNull)
				{
					RequestHeaders.ThrowIfHeaderNotAllowed(request.Headers.AsRecord);
				}
				request.Method = "HEAD";
				RecordValue metaValue = WebContentsBinaryValue.New(request, resourceCredentialCollection, options, flag, string.Empty).MetaValue;
				RecordValue asRecord = Library.Record.SelectFields.Invoke(metaValue, TextValue.New("Response.Status"), Library.MissingField.Ignore).AsRecord;
				return metaValue["Headers"].NewMeta(asRecord).AsRecord;
			}

			// Token: 0x17000D06 RID: 3334
			// (get) Token: 0x06001B1A RID: 6938 RVA: 0x000378A3 File Offset: 0x00035AA3
			public override string PrimaryResourceKind
			{
				get
				{
					return "Web";
				}
			}

			// Token: 0x06001B1B RID: 6939 RVA: 0x00038220 File Offset: 0x00036420
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				if (WebModule.TryGetLocation(WebModule.WebHeadersFunctionValue.pattern, expression, out location, out foundOptions, out unknownOptions, out dictionary))
				{
					foundOptions = foundOptions.Concatenate(RecordValue.New(Keys.New("WebMethod"), new Value[] { WebModule.WebMethod.Head })).AsRecord;
					return true;
				}
				return false;
			}

			// Token: 0x04000854 RID: 2132
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__url, _o_options)" });

			// Token: 0x04000855 RID: 2133
			private static readonly TypeValue optionsType = WebModule.baseOptionRecord.CreateRecordType().Nullable;

			// Token: 0x04000856 RID: 2134
			private readonly IEngineHost host;

			// Token: 0x04000857 RID: 2135
			private string resourceKind;
		}

		// Token: 0x020002A6 RID: 678
		private sealed class SodaFeedFunctionValue : NativeFunctionValue1<TableValue, TextValue>
		{
			// Token: 0x06001B1D RID: 6941 RVA: 0x0003829C File Offset: 0x0003649C
			public SodaFeedFunctionValue(IEngineHost host)
				: base(TypeValue.Table, "url", TypeValue.Text)
			{
				this.webContents = new WebModule.WebContentsFunctionValue(host);
				this.csvDocument = new LinesModule.Csv.DocumentFunctionValue(host);
				this.promoteHeaders = new TableModule.Table.PromoteHeadersFunctionValue(host);
			}

			// Token: 0x06001B1E RID: 6942 RVA: 0x000382D7 File Offset: 0x000364D7
			public override TableValue TypedInvoke(TextValue url)
			{
				if (Uri.IsWellFormedUriString(url.String, UriKind.Absolute))
				{
					return new SodaTableValue(new Uri(url.String), new Func<string, TableValue>(this.ExtractTable), null, RowRange.All);
				}
				throw ValueException.NewDataFormatError<Message0>(Strings.UriInvalidArgument, url, null);
			}

			// Token: 0x06001B1F RID: 6943 RVA: 0x00038318 File Offset: 0x00036518
			private TableValue ExtractTable(string url)
			{
				byte[] asBytes = this.webContents.Invoke(TextValue.New(url)).AsBinary.AsBytes;
				return this.promoteHeaders.Invoke(this.csvDocument.Invoke(BinaryValue.New(asBytes))).AsTable;
			}

			// Token: 0x17000D07 RID: 3335
			// (get) Token: 0x06001B20 RID: 6944 RVA: 0x000378A3 File Offset: 0x00035AA3
			public override string PrimaryResourceKind
			{
				get
				{
					return "Web";
				}
			}

			// Token: 0x06001B21 RID: 6945 RVA: 0x00038364 File Offset: 0x00036564
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				string text;
				string text2;
				if (argumentValues != null && argumentValues.Length == 1 && argumentValues[0].IsText && Resource.TryNormalizeWebUri(argumentValues[0].AsString, out text, out text2))
				{
					location = new SodaDataSourceLocation
					{
						Address = new Dictionary<string, object> { { "url", text2 } }
					};
					foundOptions = RecordValue.Empty;
					unknownOptions = Keys.Empty;
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x04000858 RID: 2136
			private readonly FunctionValue csvDocument;

			// Token: 0x04000859 RID: 2137
			private readonly FunctionValue promoteHeaders;

			// Token: 0x0400085A RID: 2138
			private readonly WebModule.WebContentsFunctionValue webContents;
		}

		// Token: 0x020002A7 RID: 679
		public sealed class WebMethod
		{
			// Token: 0x0400085B RID: 2139
			public static readonly TextEnumTypeValue Type = new TextEnumTypeValue("WebMethod.Type");

			// Token: 0x0400085C RID: 2140
			public static readonly TextValue Delete = WebModule.WebMethod.Type.NewEnumValue("WebMethod.Delete", "DELETE", null);

			// Token: 0x0400085D RID: 2141
			public static readonly TextValue Get = WebModule.WebMethod.Type.NewEnumValue("WebMethod.Get", "GET", null);

			// Token: 0x0400085E RID: 2142
			public static readonly TextValue Head = WebModule.WebMethod.Type.NewEnumValue("WebMethod.Head", "HEAD", null);

			// Token: 0x0400085F RID: 2143
			public static readonly TextValue Patch = WebModule.WebMethod.Type.NewEnumValue("WebMethod.Patch", "PATCH", null);

			// Token: 0x04000860 RID: 2144
			public static readonly TextValue Post = WebModule.WebMethod.Type.NewEnumValue("WebMethod.Post", "POST", null);

			// Token: 0x04000861 RID: 2145
			public static readonly TextValue Put = WebModule.WebMethod.Type.NewEnumValue("WebMethod.Put", "PUT", null);
		}

		// Token: 0x020002A8 RID: 680
		public sealed class WebActionFunctionValue : NativeFunctionValue3<ActionValue, TextValue, TextValue, Value>
		{
			// Token: 0x06001B24 RID: 6948 RVA: 0x00038490 File Offset: 0x00036690
			public WebActionFunctionValue(IEngineHost host)
				: this(host, null)
			{
			}

			// Token: 0x06001B25 RID: 6949 RVA: 0x0003849C File Offset: 0x0003669C
			public WebActionFunctionValue(IEngineHost host, IResource callerResource)
				: base(TypeValue.Action, 2, "method", TypeValue.Text, "url", TypeValue.Text, "options", WebModule.WebActionFunctionValue.optionsType)
			{
				this.host = host;
				this.callerResource = callerResource;
			}

			// Token: 0x06001B26 RID: 6950 RVA: 0x000384E4 File Offset: 0x000366E4
			public override ActionValue TypedInvoke(TextValue method, TextValue url, Value options)
			{
				Uri uri = UriHelper.CreateAbsoluteUriFromValue(url);
				if (!UriHelper.IsHttpUri(uri) && !UriHelper.IsHttpsUri(uri))
				{
					throw ValueException.NewExpressionError<Message1>(Strings.WebActionSchemeUnsupported(uri.Scheme), url, null);
				}
				string text = "Web";
				string text2 = null;
				bool flag = false;
				IExtensibilityService extensibilityService = this.host.QueryService<IExtensibilityService>();
				if (extensibilityService != null)
				{
					if (extensibilityService.CurrentResource != null && extensibilityService.ImpersonateResource)
					{
						text = extensibilityService.CurrentResource.Kind;
						text2 = extensibilityService.CurrentResource.NonNormalizedPath;
					}
					flag = true;
				}
				else if (this.callerResource != null)
				{
					text = this.callerResource.Kind;
					text2 = this.callerResource.Path;
				}
				Request request = WebOptionsHelper.GetRequest(this.host, text, text2, url, options, flag, null);
				this.host.VerifyActionPermitted(request.RequestResource);
				ResourceCredentialCollection resourceCredentialCollection;
				request.VerifyPermissionAndGetCredentials(out resourceCredentialCollection);
				Value value;
				if (options.IsRecord && options.AsRecord.TryGetValue("ManualCredentials", out value) && value.AsLogical.AsBoolean)
				{
					resourceCredentialCollection = new ResourceCredentialCollection(request.RequestResource, Array.Empty<IResourceCredential>());
				}
				if (!flag && resourceCredentialCollection.Count > 0 && request.Headers.IsRecord && request.Headers.AsRecord.Count > 0)
				{
					RequestHeaders.ThrowIfHeaderNotAllowed(request.Headers.AsRecord);
				}
				request.Method = method.String;
				request.UseCache = false;
				return new WebModule.WebActionValue(request, resourceCredentialCollection, options, flag).ClearCache(this.host);
			}

			// Token: 0x17000D08 RID: 3336
			// (get) Token: 0x06001B27 RID: 6951 RVA: 0x000378A3 File Offset: 0x00035AA3
			public override string PrimaryResourceKind
			{
				get
				{
					return "Web";
				}
			}

			// Token: 0x06001B28 RID: 6952 RVA: 0x0003865C File Offset: 0x0003685C
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (WebModule.TryGetLocation(WebModule.WebActionFunctionValue.pattern, expression, out location, out foundOptions, out unknownOptions, out dictionary) && dictionary.TryGetConstant("method", out value) && value.IsText)
				{
					RecordValue recordValue = RecordValue.New(Keys.New("WebMethod", "IsAction"), new Value[]
					{
						value,
						LogicalValue.True
					});
					foundOptions = foundOptions.Concatenate(recordValue).AsRecord;
					return true;
				}
				return false;
			}

			// Token: 0x04000862 RID: 2146
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__method, __url, _o_options)" });

			// Token: 0x04000863 RID: 2147
			private static readonly TypeValue optionsType = WebModule.WebActionRequestOptionRecord.CreateRecordType().Nullable;

			// Token: 0x04000864 RID: 2148
			private readonly IEngineHost host;

			// Token: 0x04000865 RID: 2149
			private readonly IResource callerResource;
		}

		// Token: 0x020002A9 RID: 681
		private sealed class WebActionValue : ActionValue
		{
			// Token: 0x06001B2A RID: 6954 RVA: 0x000386FA File Offset: 0x000368FA
			public WebActionValue(Request request, ResourceCredentialCollection credentials, Value options, bool privilegedMode)
			{
				this.request = request;
				this.credentials = credentials;
				this.options = options;
				this.privilegedMode = privilegedMode;
			}

			// Token: 0x06001B2B RID: 6955 RVA: 0x00038720 File Offset: 0x00036920
			public override Value Execute()
			{
				BinaryValue binaryValue = WebContentsBinaryValue.New(this.request, this.credentials, this.options, this.privilegedMode, this.request.InitialUri.String);
				MemoryStream memoryStream = new MemoryStream();
				using (Stream stream = binaryValue.Open())
				{
					stream.CopyTo(memoryStream);
				}
				return BinaryValue.New(memoryStream.ToArray()).NewMeta(binaryValue.MetaValue);
			}

			// Token: 0x04000866 RID: 2150
			private readonly Request request;

			// Token: 0x04000867 RID: 2151
			private readonly ResourceCredentialCollection credentials;

			// Token: 0x04000868 RID: 2152
			private readonly Value options;

			// Token: 0x04000869 RID: 2153
			private readonly bool privilegedMode;
		}
	}
}
