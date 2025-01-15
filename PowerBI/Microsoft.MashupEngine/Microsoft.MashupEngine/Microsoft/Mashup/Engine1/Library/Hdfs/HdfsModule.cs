using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Hdfs
{
	// Token: 0x02000ADD RID: 2781
	internal sealed class HdfsModule : Module
	{
		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x06004D71 RID: 19825 RVA: 0x0010086A File Offset: 0x000FEA6A
		public override string Name
		{
			get
			{
				return "Hdfs";
			}
		}

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x06004D72 RID: 19826 RVA: 0x00100871 File Offset: 0x000FEA71
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Hdfs.Contents";
						}
						if (index != 1)
						{
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
						return "Hdfs.Files";
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x06004D73 RID: 19827 RVA: 0x001008AC File Offset: 0x000FEAAC
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { HdfsModule.resourceKindInfo };
			}
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x001008BC File Offset: 0x000FEABC
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new HdfsModule.HdfsContentsFunctionValue(hostEnvironment);
				}
				if (index != 1)
				{
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				return new HdfsModule.HdfsFilesFunctionValue(hostEnvironment);
			});
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x001008F0 File Offset: 0x000FEAF0
		public static string GetHttpUrl(TextValue url)
		{
			string text = url.String;
			if (text.Length == 0)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.HdfsEmptyUrl, url, null);
			}
			if (!HdfsModule.UsesSupportedHdfsSchema(text))
			{
				text = "webhdfs" + Uri.SchemeDelimiter + text;
			}
			UriBuilder uriBuilder;
			if (!UriHelper.TryCreateAbsoluteUriBuilder(text, out uriBuilder))
			{
				throw ValueException.NewExpressionError<Message1>(Strings.HdfsInvalidUrl(url), null, null);
			}
			string scheme = uriBuilder.Scheme;
			if (!(scheme == "hdfs") && !(scheme == "webhdfs"))
			{
				if (!(scheme == "adl") && !(scheme == "swebhdfs"))
				{
					if (uriBuilder.Scheme != Uri.UriSchemeHttp && uriBuilder.Scheme != Uri.UriSchemeHttps)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.HdfsInvalidUrl(url), null, null);
					}
				}
				else
				{
					if (uriBuilder.Uri.IsDefaultPort)
					{
						uriBuilder.Port = 443;
					}
					uriBuilder.Scheme = Uri.UriSchemeHttps;
					uriBuilder.Path = "/webhdfs/v1" + uriBuilder.Path;
				}
			}
			else
			{
				if (uriBuilder.Uri.IsDefaultPort || uriBuilder.Scheme == "hdfs")
				{
					uriBuilder.Port = 50070;
				}
				uriBuilder.Scheme = Uri.UriSchemeHttp;
				uriBuilder.Path = "/webhdfs/v1" + uriBuilder.Path;
			}
			Uri uri;
			if (UriHelper.TryCreateAbsoluteUri(uriBuilder.ToString(), out uri))
			{
				return Uri.UnescapeDataString(uri.ToString());
			}
			throw ValueException.NewExpressionError<Message1>(Strings.HdfsInvalidUrl(url), null, null);
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x00100A6C File Offset: 0x000FEC6C
		private static bool UsesSupportedHdfsSchema(string urlString)
		{
			return urlString.StartsWith(Uri.UriSchemeHttp + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase) || urlString.StartsWith(Uri.UriSchemeHttps + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase) || urlString.StartsWith("hdfs" + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase) || urlString.StartsWith("webhdfs" + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase) || urlString.StartsWith("swebhdfs" + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase) || urlString.StartsWith("adl" + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04002957 RID: 10583
		private const StringComparison PathStringComparison = StringComparison.Ordinal;

		// Token: 0x04002958 RID: 10584
		public const string HdfsContents = "Hdfs.Contents";

		// Token: 0x04002959 RID: 10585
		public const string HdfsFiles = "Hdfs.Files";

		// Token: 0x0400295A RID: 10586
		public const string DataSourceNameString = "HDFS";

		// Token: 0x0400295B RID: 10587
		private const string HdfsScheme = "hdfs";

		// Token: 0x0400295C RID: 10588
		private const string WebHdfsScheme = "webhdfs";

		// Token: 0x0400295D RID: 10589
		private const string SWebHdfsScheme = "swebhdfs";

		// Token: 0x0400295E RID: 10590
		private const string ADLScheme = "adl";

		// Token: 0x0400295F RID: 10591
		private const string HttpPathPrefix = "/webhdfs/v1";

		// Token: 0x04002960 RID: 10592
		private const int HdfsDefaultPort = 50070;

		// Token: 0x04002961 RID: 10593
		private const int SWebHdfsDefaultPort = 443;

		// Token: 0x04002962 RID: 10594
		private const string AccessTimeKey = "accessTime";

		// Token: 0x04002963 RID: 10595
		private const string BlockSizeKey = "blockSize";

		// Token: 0x04002964 RID: 10596
		private const string GroupKey = "group";

		// Token: 0x04002965 RID: 10597
		private const string LengthKey = "length";

		// Token: 0x04002966 RID: 10598
		private const string ModificationTimeKey = "modificationTime";

		// Token: 0x04002967 RID: 10599
		private const string OwnerKey = "owner";

		// Token: 0x04002968 RID: 10600
		private const string PathSuffixKey = "pathSuffix";

		// Token: 0x04002969 RID: 10601
		private const string PermissionKey = "permission";

		// Token: 0x0400296A RID: 10602
		private const string ReplicationKey = "replication";

		// Token: 0x0400296B RID: 10603
		private const string TypeKey = "type";

		// Token: 0x0400296C RID: 10604
		private const string FileStatusKey = "FileStatus";

		// Token: 0x0400296D RID: 10605
		private const string FileStatusesKey = "FileStatuses";

		// Token: 0x0400296E RID: 10606
		private const string RemoteExceptionKey = "RemoteException";

		// Token: 0x0400296F RID: 10607
		private const string OpQueryKey = "Op";

		// Token: 0x04002970 RID: 10608
		private const string LengthQueryKey = "Length";

		// Token: 0x04002971 RID: 10609
		private const string ExceptionKey = "exception";

		// Token: 0x04002972 RID: 10610
		private const string FileNotFoundExceptionValue = "FileNotFoundException";

		// Token: 0x04002973 RID: 10611
		private const string FileStatusQueryValue = "GETFILESTATUS";

		// Token: 0x04002974 RID: 10612
		private const string ListStatusQueryValue = "LISTSTATUS";

		// Token: 0x04002975 RID: 10613
		private const string OpenQueryValue = "OPEN";

		// Token: 0x04002976 RID: 10614
		private const string FolderType = "DIRECTORY";

		// Token: 0x04002977 RID: 10615
		private static readonly Keys QueryKeys = Keys.New("Op");

		// Token: 0x04002978 RID: 10616
		private static readonly Keys ContentTypeQueryKeys = Keys.New("Op", "Length");

		// Token: 0x04002979 RID: 10617
		private static readonly Keys OpenHeaderKeys = Keys.New("Allow-Auto-Redirect", "Automatic-Decompression", "Pipelined");

		// Token: 0x0400297A RID: 10618
		private static readonly Keys StatusHeaderKeys = Keys.New("Accept", "Allow-Auto-Redirect", "Automatic-Decompression", "Connection");

		// Token: 0x0400297B RID: 10619
		private static readonly RecordValue OpenHeaders = RecordValue.New(HdfsModule.OpenHeaderKeys, new Value[]
		{
			LogicalValue.True,
			NumberValue.New(3),
			LogicalValue.False
		});

		// Token: 0x0400297C RID: 10620
		private static readonly RecordValue StatusHeaders = RecordValue.New(HdfsModule.StatusHeaderKeys, new Value[]
		{
			TextValue.New("application/json"),
			LogicalValue.True,
			NumberValue.New(3),
			TextValue.New("Keep-Alive")
		});

		// Token: 0x0400297D RID: 10621
		private static readonly RecordValue ContentTypeQuery = RecordValue.New(HdfsModule.ContentTypeQueryKeys, new Value[]
		{
			TextValue.New("OPEN"),
			TextValue.New(4096.ToString(CultureInfo.InvariantCulture))
		});

		// Token: 0x0400297E RID: 10622
		private static readonly RecordValue FileStatusQuery = RecordValue.New(HdfsModule.QueryKeys, new Value[] { TextValue.New("GETFILESTATUS") });

		// Token: 0x0400297F RID: 10623
		private static readonly RecordValue ListStatusQuery = RecordValue.New(HdfsModule.QueryKeys, new Value[] { TextValue.New("LISTSTATUS") });

		// Token: 0x04002980 RID: 10624
		private static readonly RecordValue OpenQuery = RecordValue.New(HdfsModule.QueryKeys, new Value[] { TextValue.New("OPEN") });

		// Token: 0x04002981 RID: 10625
		private static readonly TextValue ApplicationOctetStream = TextValue.New("application/octet-stream");

		// Token: 0x04002982 RID: 10626
		private static readonly ResourceKindInfo resourceKindInfo = new UriResourceKindInfo("Hdfs", null, new AuthenticationInfo[]
		{
			ResourceHelpers.AnonymousAuth,
			ResourceHelpers.WindowsAuth,
			new AadAuthenticationInfo
			{
				ClientApplicationType = OAuthClientApplicationType.Required,
				ProviderFactory = new OAuthFactory(new Func<OAuthServices, string, OAuthResource>(ResourceHelpers.GetAdlsResource), (OAuthServices services, OAuthClientApplication app, string url) => new AadOAuthProvider(services, app, ResourceHelpers.GetAdlsResource(services, url)))
			}
		}, null, false, false, false, null, new DataSourceLocationFactory[] { HdfsDataSourceLocation.Factory });

		// Token: 0x04002983 RID: 10627
		private Keys exportKeys;

		// Token: 0x02000ADE RID: 2782
		private enum Exports
		{
			// Token: 0x04002985 RID: 10629
			HdfsContents,
			// Token: 0x04002986 RID: 10630
			HdfsFiles,
			// Token: 0x04002987 RID: 10631
			Count
		}

		// Token: 0x02000ADF RID: 2783
		private sealed class HdfsBinaryValue : HttpBinaryValue
		{
			// Token: 0x06004D79 RID: 19833 RVA: 0x00100D04 File Offset: 0x000FEF04
			public static BinaryValue New(IEngineHost host, string fileUrl, ResourceCredentialCollection credentials)
			{
				Request request = Request.Create(host, "Hdfs", fileUrl, TextValue.New(fileUrl), HdfsModule.OpenQuery, Value.Null, null, HdfsModule.OpenHeaders, null, null, null, null, null, null);
				request.AllowUnpermittedRedirects = true;
				HdfsModule.HdfsBinaryValue hdfsBinaryValue = new HdfsModule.HdfsBinaryValue(host, request, credentials);
				Request request2 = Request.Create(host, "Hdfs", request.ResourcePath, request.InitialUri, HdfsModule.ContentTypeQuery, Value.Null, null, HdfsModule.OpenHeaders, null, null, null, null, null, null);
				request2.AllowUnpermittedRedirects = true;
				return hdfsBinaryValue.NewMeta(DataSource.CreateDataSourceRecordValue(hdfsBinaryValue.GetContentType(request2, null), RecordValue.Empty, request, Value.Null, null, null)).AsBinary.WithExpressionFromValue(hdfsBinaryValue);
			}

			// Token: 0x06004D7A RID: 19834 RVA: 0x00100DAA File Offset: 0x000FEFAA
			private HdfsBinaryValue(IEngineHost engineHost, Request request, ResourceCredentialCollection credentials)
			{
				this.engineHost = engineHost;
				this.request = request;
				this.credentials = credentials;
			}

			// Token: 0x06004D7B RID: 19835 RVA: 0x00100DC8 File Offset: 0x000FEFC8
			public override bool TryGetLength(out long length)
			{
				long? num = this.RetrieveLength();
				if (num != null)
				{
					length = num.Value;
					return true;
				}
				return base.TryGetLength(out length);
			}

			// Token: 0x17001844 RID: 6212
			// (get) Token: 0x06004D7C RID: 19836 RVA: 0x00100DF8 File Offset: 0x000FEFF8
			public override long Length
			{
				get
				{
					long? num = this.RetrieveLength();
					if (num == null)
					{
						return base.Length;
					}
					return num.Value;
				}
			}

			// Token: 0x06004D7D RID: 19837 RVA: 0x00100E24 File Offset: 0x000FF024
			protected override Response GetResponse(Request request)
			{
				Response response2;
				try
				{
					Response response = request.GetResponse(this.credentials, null, false);
					if (request.IsFailedStatusCode(response))
					{
						Message3 message = Strings.HdfsFailed(request.InitialUri, PiiFree.New(response.StatusCode), response.StatusDescription);
						throw DataSourceException.NewDataSourceError<Message3>(this.engineHost, message, request.RequestResource, "Url", request.InitialUri, TypeValue.Text, null);
					}
					response2 = response;
				}
				catch (ResponseException ex)
				{
					throw DataSourceException.NewDataSourceError<Message2>(this.engineHost, Strings.HdfsFailedToResolveServer(request.Uri.Host, ex.Message), request.RequestResource, "Url", request.InitialUri, TypeValue.Text, ex.InnerException);
				}
				return response2;
			}

			// Token: 0x06004D7E RID: 19838 RVA: 0x00100EE0 File Offset: 0x000FF0E0
			public override Stream Open()
			{
				return this.GetResponse(this.request).GetResponseStream();
			}

			// Token: 0x06004D7F RID: 19839 RVA: 0x00100EF4 File Offset: 0x000FF0F4
			private long? RetrieveLength()
			{
				Request request = Request.Create(this.request.Host, this.request.ResourceKind, this.request.ResourcePath, this.request.InitialUri, HdfsModule.FileStatusQuery, Value.Null, null, HdfsModule.StatusHeaders, null, null, null, null, null, null);
				request.AllowUnpermittedRedirects = true;
				try
				{
					using (Response response = this.GetResponse(request))
					{
						Encoding encoding = Encoding.UTF8;
						if (!string.IsNullOrEmpty(response.CharacterSet))
						{
							encoding = Encoding.GetEncoding(response.CharacterSet);
						}
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), encoding))
						{
							Value value = JsonParser.Parse(streamReader, null);
							Value value2;
							Value value3;
							if (value.IsRecord && value.AsRecord.TryGetValue("FileStatus", out value2) && value2.IsRecord && value2.AsRecord.TryGetValue("length", out value3) && value3.IsNumber)
							{
								return new long?(value3.AsNumber.AsInteger64);
							}
						}
					}
				}
				catch (ResourceAccessAuthorizationException)
				{
				}
				catch (ResourceAccessForbiddenException)
				{
				}
				return null;
			}

			// Token: 0x04002988 RID: 10632
			private readonly IEngineHost engineHost;

			// Token: 0x04002989 RID: 10633
			private readonly ResourceCredentialCollection credentials;

			// Token: 0x0400298A RID: 10634
			private readonly Request request;
		}

		// Token: 0x02000AE0 RID: 2784
		private sealed class HdfsContentsFunctionValue : NativeFunctionValue1<TableValue, TextValue>
		{
			// Token: 0x06004D80 RID: 19840 RVA: 0x00101048 File Offset: 0x000FF248
			public HdfsContentsFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles), 1, "url", TypeValue.Text)
			{
				this.host = host;
			}

			// Token: 0x06004D81 RID: 19841 RVA: 0x00101068 File Offset: 0x000FF268
			public override TableValue TypedInvoke(TextValue url)
			{
				IResource resource = HdfsModule.HdfsContentsFunctionValue.GetResource(url);
				string nonNormalizedPath = resource.NonNormalizedPath;
				ResourceCredentialCollection resourceCredentialCollection;
				HttpServices.VerifyPermissionAndGetCredentials(this.host, resource, out resourceCredentialCollection);
				return new HdfsModule.HdfsTableValue(this.host, nonNormalizedPath, nonNormalizedPath, resourceCredentialCollection, FileHelper.FolderOptions.EnumerateFoldersAndFiles);
			}

			// Token: 0x17001845 RID: 6213
			// (get) Token: 0x06004D82 RID: 19842 RVA: 0x0010086A File Offset: 0x000FEA6A
			public override string PrimaryResourceKind
			{
				get
				{
					return "Hdfs";
				}
			}

			// Token: 0x06004D83 RID: 19843 RVA: 0x001010A0 File Offset: 0x000FF2A0
			private static IResource GetResource(TextValue url)
			{
				return Resource.New("Hdfs", HdfsModule.GetHttpUrl(url));
			}

			// Token: 0x06004D84 RID: 19844 RVA: 0x001010B4 File Offset: 0x000FF2B4
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (HdfsModule.HdfsContentsFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetConstant("url", out value))
				{
					string text = HdfsModule.GetHttpUrl(value.AsText);
					Value value2;
					if (dictionary.TryGetConstant("folderName", out value2))
					{
						text = UriHelper.Combine(new Uri(text), value2.AsText).ToString();
					}
					IResource resource = Resource.New("Hdfs", text);
					if (HdfsDataSourceLocation.Factory.TryCreateFromResource(resource, false, out location))
					{
						foundOptions = RecordValue.New(Keys.New("HierarchicalNavigation"), new Value[] { LogicalValue.False });
						unknownOptions = Keys.Empty;
						return true;
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x0400298B RID: 10635
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__url)", "__func(__url){[Name=__folderName]}[Content]", "__func(__url){[Name=__folderName] meta [Is File=__isFile]}[Content]" });

			// Token: 0x0400298C RID: 10636
			private readonly IEngineHost host;
		}

		// Token: 0x02000AE1 RID: 2785
		private sealed class HdfsFilesFunctionValue : NativeFunctionValue1<TableValue, TextValue>
		{
			// Token: 0x06004D86 RID: 19846 RVA: 0x0010118F File Offset: 0x000FF38F
			public HdfsFilesFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFilesDeep), 1, "url", TypeValue.Text)
			{
				this.host = host;
			}

			// Token: 0x06004D87 RID: 19847 RVA: 0x001011B0 File Offset: 0x000FF3B0
			public override TableValue TypedInvoke(TextValue url)
			{
				IResource resource = Resource.New("Hdfs", HdfsModule.GetHttpUrl(url));
				string nonNormalizedPath = resource.NonNormalizedPath;
				ResourceCredentialCollection resourceCredentialCollection;
				HttpServices.VerifyPermissionAndGetCredentials(this.host, resource, out resourceCredentialCollection);
				return new HdfsModule.HdfsTableValue(this.host, nonNormalizedPath, nonNormalizedPath, resourceCredentialCollection, FileHelper.FolderOptions.EnumerateFilesDeep);
			}

			// Token: 0x17001846 RID: 6214
			// (get) Token: 0x06004D88 RID: 19848 RVA: 0x0010086A File Offset: 0x000FEA6A
			public override string PrimaryResourceKind
			{
				get
				{
					return "Hdfs";
				}
			}

			// Token: 0x06004D89 RID: 19849 RVA: 0x001011F4 File Offset: 0x000FF3F4
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues != null && argumentValues.Length == 1 && argumentValues[0].IsText)
				{
					location = new HdfsDataSourceLocation();
					location.Address["url"] = HdfsModule.GetHttpUrl(argumentValues[0].AsText);
					foundOptions = RecordValue.New(Keys.New("HierarchicalNavigation"), new Value[] { LogicalValue.True });
					unknownOptions = Keys.Empty;
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x0400298D RID: 10637
			private readonly IEngineHost host;
		}

		// Token: 0x02000AE2 RID: 2786
		private static class HdfsRecordValue
		{
			// Token: 0x06004D8A RID: 19850 RVA: 0x00101274 File Offset: 0x000FF474
			private static RecordValue CreateAttributesRecordValue(RecordValue record, Value size, Func<TextValue> kindProvider, Func<TextValue> contentType)
			{
				return RecordValue.New(HdfsModule.HdfsRecordValue.AttributeKeys, delegate(int i)
				{
					string text = HdfsModule.HdfsRecordValue.AttributeKeys[i];
					if (text != null)
					{
						switch (text.Length)
						{
						case 4:
						{
							char c = text[0];
							if (c != 'K')
							{
								if (c == 'S')
								{
									if (text == "Size")
									{
										return size;
									}
								}
							}
							else if (text == "Kind")
							{
								return kindProvider();
							}
							break;
						}
						case 5:
						{
							char c = text[0];
							if (c != 'G')
							{
								if (c == 'O')
								{
									if (text == "Owner")
									{
										return record["owner"];
									}
								}
							}
							else if (text == "Group")
							{
								return record["group"];
							}
							break;
						}
						case 9:
							if (text == "BlockSize")
							{
								return record["blockSize"];
							}
							break;
						case 10:
							if (text == "Permission")
							{
								return record["permission"];
							}
							break;
						case 11:
							if (text == "Replication")
							{
								return record["replication"];
							}
							break;
						case 12:
							if (text == "Content Type")
							{
								return contentType();
							}
							break;
						}
					}
					throw new InvalidOperationException();
				});
			}

			// Token: 0x06004D8B RID: 19851 RVA: 0x001012BC File Offset: 0x000FF4BC
			public static RecordValue New(IEngineHost host, string folderUrl, ResourceCredentialCollection credentials, RecordValue record, TextValue kind)
			{
				TextValue asText = record["pathSuffix"].AsText;
				UriBuilder uriBuilder = new UriBuilder(folderUrl);
				uriBuilder.Path = uriBuilder.Path.TrimEnd(new char[] { '/' }) + "/" + asText.String;
				string text = Uri.UnescapeDataString(uriBuilder.Uri.AbsoluteUri);
				string text2 = UriHelper.GetDirectoryName(text) + "/";
				Value content;
				Value value;
				TextValue textValue;
				Func<TextValue> func;
				Func<TextValue> func2;
				if (kind.Equals(FileHelper.FolderKind))
				{
					content = new HdfsModule.HdfsTableValue(host, text, text, credentials);
					value = Value.Null;
					textValue = TextValue.Empty;
					func = () => kind;
					func2 = () => TextValue.Empty;
				}
				else
				{
					content = HdfsModule.HdfsBinaryValue.New(host, text, credentials);
					value = record["length"].AsNumber;
					try
					{
						textValue = FileHelper.GetFileExtension(text);
					}
					catch (ValueException)
					{
						textValue = TextValue.Empty;
					}
					func = delegate
					{
						TextValue textValue2;
						try
						{
							textValue2 = FileHelper.GetFileKind(content.MetaValue["Content.Type"].AsString);
						}
						catch (ValueException)
						{
							textValue2 = FileHelper.FileKind;
						}
						return textValue2;
					};
					func2 = delegate
					{
						TextValue textValue3;
						try
						{
							textValue3 = content.MetaValue["Content.Type"].AsText;
						}
						catch (ValueException)
						{
							textValue3 = TextValue.Empty;
						}
						return textValue3;
					};
				}
				return RecordValue.New(FileHelper.FileEntryKeys, new Value[]
				{
					content,
					asText,
					textValue,
					HdfsModule.HdfsRecordValue.ToDateTimeValue(record["accessTime"].AsNumber),
					HdfsModule.HdfsRecordValue.ToDateTimeValue(record["modificationTime"].AsNumber),
					Value.Null,
					HdfsModule.HdfsRecordValue.CreateAttributesRecordValue(record, value, func, func2),
					TextValue.New(text2)
				});
			}

			// Token: 0x06004D8C RID: 19852 RVA: 0x00101474 File Offset: 0x000FF674
			private static Value ToDateTimeValue(NumberValue value)
			{
				if (value.IsNull)
				{
					return Value.Null;
				}
				double asDouble = value.AsDouble;
				if (asDouble <= 0.0)
				{
					return Value.Null;
				}
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
				return DateTimeValue.New(dateTime.AddMilliseconds(asDouble));
			}

			// Token: 0x0400298E RID: 10638
			private const string CapBlockSizeKey = "BlockSize";

			// Token: 0x0400298F RID: 10639
			private const string CapGroupKey = "Group";

			// Token: 0x04002990 RID: 10640
			private const string CapOwnerKey = "Owner";

			// Token: 0x04002991 RID: 10641
			private const string CapPermissionKey = "Permission";

			// Token: 0x04002992 RID: 10642
			private const string CapReplicationKey = "Replication";

			// Token: 0x04002993 RID: 10643
			private static readonly Keys AttributeKeys = Keys.New(new string[] { "Content Type", "Kind", "Size", "BlockSize", "Group", "Owner", "Permission", "Replication" });
		}

		// Token: 0x02000AE6 RID: 2790
		private sealed class HdfsTableValue : TableValue
		{
			// Token: 0x06004D97 RID: 19863 RVA: 0x0010175C File Offset: 0x000FF95C
			public HdfsTableValue(IEngineHost host, string sourceUrl, string folderUrl, ResourceCredentialCollection credentials)
				: this(host, sourceUrl, folderUrl, null, credentials, FileHelper.FolderOptions.EnumerateFoldersAndFiles)
			{
			}

			// Token: 0x06004D98 RID: 19864 RVA: 0x0010176B File Offset: 0x000FF96B
			public HdfsTableValue(IEngineHost host, string sourceUrl, string folderUrl, ResourceCredentialCollection credentials, FileHelper.FolderOptions folderOptions)
				: this(host, sourceUrl, folderUrl, null, credentials, folderOptions)
			{
			}

			// Token: 0x06004D99 RID: 19865 RVA: 0x0010177C File Offset: 0x000FF97C
			private HdfsTableValue(IEngineHost host, string sourceUrl, string folderUrl, string fileNamePrefix, ResourceCredentialCollection credentials, FileHelper.FolderOptions folderOptions)
			{
				this.host = host;
				this.sourceUrl = sourceUrl;
				this.folderUrl = (UriHelper.EndsWithPathSeparator(folderUrl) ? folderUrl : (folderUrl + "/"));
				this.fileNamePrefix = fileNamePrefix;
				this.credentials = credentials;
				this.folderOptions = folderOptions;
			}

			// Token: 0x17001847 RID: 6215
			// (get) Token: 0x06004D9A RID: 19866 RVA: 0x001017D1 File Offset: 0x000FF9D1
			public override TypeValue Type
			{
				get
				{
					return FileHelper.FolderResultTypeValue(this.folderOptions);
				}
			}

			// Token: 0x06004D9B RID: 19867 RVA: 0x001017E0 File Offset: 0x000FF9E0
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				ListValue listValue = this.LoadList();
				HdfsModule.HdfsTableValue.HdfsListEnumerator hdfsListEnumerator = null;
				HdfsModule.HdfsTableValue.HdfsListEnumerator hdfsListEnumerator2 = null;
				if (listValue != null)
				{
					hdfsListEnumerator = new HdfsModule.HdfsTableValue.HdfsListEnumerator(this.host, this.folderUrl, this.fileNamePrefix, listValue.GetEnumerator(), this.credentials, this.folderOptions);
					if (FileHelper.EnumerateDeep(this.folderOptions))
					{
						hdfsListEnumerator2 = new HdfsModule.HdfsTableValue.HdfsListEnumerator(this.host, this.folderUrl, this.fileNamePrefix, listValue.GetEnumerator(), this.credentials, FileHelper.FolderOptions.EnumeratingSubfolders);
					}
				}
				return new HdfsModule.HdfsTableValue.HdfsTableEnumerator(this.host, this.folderUrl, hdfsListEnumerator, hdfsListEnumerator2, this.credentials, this.folderOptions);
			}

			// Token: 0x06004D9C RID: 19868 RVA: 0x00101878 File Offset: 0x000FFA78
			private Response GetResponse(Request request, out bool isFileNotFound)
			{
				Response response2;
				try
				{
					Response response = request.GetResponse(this.credentials, null, false);
					if (request.IsFailedStatusCode(response))
					{
						this.HandleSourceUrlNotFound(request, response);
						if (!this.IsFileNotFoundResponse(request, response))
						{
							throw this.CreateResponseException(request, response);
						}
						isFileNotFound = true;
					}
					else
					{
						isFileNotFound = false;
					}
					response2 = response;
				}
				catch (ResponseException ex)
				{
					throw DataSourceException.NewDataSourceError<Message2>(this.host, Strings.HdfsFailedToResolveServer(request.Uri.Host, ex.Message), request.RequestResource, "Url", request.InitialUri, TypeValue.Text, ex.InnerException);
				}
				return response2;
			}

			// Token: 0x06004D9D RID: 19869 RVA: 0x00101914 File Offset: 0x000FFB14
			private ValueException CreateResponseException(Request request, Response response)
			{
				Message3 message = Strings.HdfsFailed(request.InitialUri, PiiFree.New(response.StatusCode), response.StatusDescription);
				return DataSourceException.NewDataSourceError<Message3>(this.host, message, Resource.New(request.ResourceKind, request.ResourcePath), "Url", request.InitialUri, TypeValue.Text, null);
			}

			// Token: 0x06004D9E RID: 19870 RVA: 0x0010196C File Offset: 0x000FFB6C
			private void HandleSourceUrlNotFound(Request request, Response response)
			{
				if (response.StatusCode == 404 && this.sourceUrl != null)
				{
					Request request2 = Request.Create(this.host, "Hdfs", this.sourceUrl, TextValue.New(this.sourceUrl), HdfsModule.FileStatusQuery, Value.Null, null, HdfsModule.StatusHeaders, null, null, null, null, null, null);
					request2.AllowUnpermittedRedirects = true;
					if (request2.GetResponse(this.credentials, null, false).StatusCode == 404)
					{
						throw this.CreateResponseException(request2, response);
					}
				}
			}

			// Token: 0x06004D9F RID: 19871 RVA: 0x001019F0 File Offset: 0x000FFBF0
			private bool IsFileNotFoundResponse(Request request, Response response)
			{
				if (response.StatusCode == 404)
				{
					using (Stream responseStream = response.GetResponseStream())
					{
						Value value;
						try
						{
							value = JsonParser.Parse(new StreamReader(responseStream), null);
						}
						catch (ValueException)
						{
							value = null;
						}
						Value value2 = null;
						Value value3;
						if (value != null && value.IsRecord && value.AsRecord.TryGetValue("RemoteException", out value3) && value3.IsRecord && value3.AsRecord.TryGetValue("exception", out value2) && value2.IsText && value2.AsString == "FileNotFoundException")
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x06004DA0 RID: 19872 RVA: 0x00101AB0 File Offset: 0x000FFCB0
			private ListValue LoadList()
			{
				Request request = Request.Create(this.host, "Hdfs", this.folderUrl, TextValue.New(this.folderUrl), HdfsModule.ListStatusQuery, Value.Null, null, HdfsModule.StatusHeaders, null, null, null, null, null, null);
				request.AllowUnpermittedRedirects = true;
				IResource requestResource = request.RequestResource;
				ListValue listValue;
				try
				{
					bool flag;
					using (Response response = this.GetResponse(request, out flag))
					{
						if (flag)
						{
							listValue = ListValue.Empty;
						}
						else
						{
							Encoding encoding = Encoding.UTF8;
							if (!string.IsNullOrEmpty(response.CharacterSet))
							{
								encoding = Encoding.GetEncoding(response.CharacterSet);
							}
							using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), encoding))
							{
								Value value = JsonParser.Parse(streamReader, null);
								Value value2;
								if (!value.IsRecord || !value.AsRecord.TryGetValue("FileStatuses", out value2))
								{
									Message1 message = Strings.HdfsFileStatusExpected(this.folderUrl);
									throw DataSourceException.NewDataSourceError<Message1>(this.host, message, requestResource, "Url", request.InitialUri, TypeValue.Text, null);
								}
								Value value3;
								if (!value2.IsRecord || !value2.AsRecord.TryGetValue("FileStatus", out value3))
								{
									Message1 message2 = Strings.HdfsFileStatusExpected(this.folderUrl);
									throw DataSourceException.NewDataSourceError<Message1>(this.host, message2, requestResource, "Url", request.InitialUri, TypeValue.Text, null);
								}
								if (!value3.IsList)
								{
									Message1 message3 = Strings.HdfsFileStatusExpected(this.folderUrl);
									throw DataSourceException.NewDataSourceError<Message1>(this.host, message3, requestResource, "Url", request.InitialUri, TypeValue.Text, null);
								}
								listValue = value3.AsList;
							}
						}
					}
				}
				catch (ResourceAccessAuthorizationException)
				{
					if (!FileHelper.EnumeratingSubEntries(this.folderOptions))
					{
						throw;
					}
					listValue = null;
				}
				catch (ResourceAccessForbiddenException)
				{
					if (!FileHelper.EnumeratingSubEntries(this.folderOptions))
					{
						throw;
					}
					listValue = null;
				}
				return listValue;
			}

			// Token: 0x06004DA1 RID: 19873 RVA: 0x00101CD4 File Offset: 0x000FFED4
			public override TableValue SelectRows(FunctionValue condition)
			{
				TableValue tableValue;
				if (this.TrySelectRows(condition, out tableValue))
				{
					return tableValue;
				}
				return base.SelectRows(condition);
			}

			// Token: 0x06004DA2 RID: 19874 RVA: 0x00101CF8 File Offset: 0x000FFEF8
			private bool TryGetColumnText(string columnName, QueryExpression query1, QueryExpression query2, out string text)
			{
				ColumnAccessQueryExpression columnAccessQueryExpression = query1 as ColumnAccessQueryExpression;
				if (columnAccessQueryExpression != null && this.Columns[columnAccessQueryExpression.Column] == columnName)
				{
					ConstantQueryExpression constantQueryExpression = query2 as ConstantQueryExpression;
					if (constantQueryExpression != null && constantQueryExpression.Value.IsText)
					{
						text = constantQueryExpression.Value.AsString;
						return true;
					}
				}
				text = null;
				return false;
			}

			// Token: 0x06004DA3 RID: 19875 RVA: 0x00101D54 File Offset: 0x000FFF54
			private bool TrySelectRows(FunctionValue condition, out TableValue tableValue)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this.Type.AsTableType.ItemType, condition);
				InvocationQueryExpression invocationQueryExpression = queryExpression as InvocationQueryExpression;
				if (invocationQueryExpression != null)
				{
					ConstantQueryExpression constantQueryExpression = invocationQueryExpression.Function as ConstantQueryExpression;
					string text;
					if (constantQueryExpression != null && constantQueryExpression.Value.Equals(Library.Text.StartsWith) && this.TryGetColumnText("Folder Path", invocationQueryExpression.Arguments[0], invocationQueryExpression.Arguments[1], out text) && text.StartsWith(this.folderUrl, StringComparison.Ordinal))
					{
						if (text.Length == this.folderUrl.Length)
						{
							tableValue = this;
						}
						else if (UriHelper.EndsWithPathSeparator(text))
						{
							tableValue = new HdfsModule.HdfsTableValue(this.host, this.sourceUrl, text, this.credentials, this.folderOptions);
						}
						else
						{
							string directoryName = UriHelper.GetDirectoryName(text);
							string fileName = HdfsModule.HdfsTableValue.GetFileName(text);
							tableValue = new QueryTableValue(new HdfsModule.HdfsTableValue(this.host, this.sourceUrl, directoryName, fileName, this.credentials, this.folderOptions));
						}
						return true;
					}
				}
				BinaryQueryExpression binaryQueryExpression = queryExpression as BinaryQueryExpression;
				if (binaryQueryExpression != null)
				{
					string text;
					if (binaryQueryExpression.Operator == BinaryOperator2.Equals && (this.TryGetColumnText("Folder Path", binaryQueryExpression.Left, binaryQueryExpression.Right, out text) || this.TryGetColumnText("Folder Path", binaryQueryExpression.Right, binaryQueryExpression.Left, out text)) && text.StartsWith(this.folderUrl, StringComparison.Ordinal) && UriHelper.EndsWithPathSeparator(text))
					{
						string directoryName2 = UriHelper.GetDirectoryName(text);
						FileHelper.FolderOptions folderOptions = this.folderOptions & ~FileHelper.FolderOptions.EnumerateDeep;
						tableValue = new HdfsModule.HdfsTableValue(this.host, this.sourceUrl, directoryName2, this.credentials, folderOptions);
						return true;
					}
					if (this.TrySelectRowsOverTableKeys(binaryQueryExpression, condition, out tableValue))
					{
						return true;
					}
				}
				tableValue = null;
				return false;
			}

			// Token: 0x06004DA4 RID: 19876 RVA: 0x00101F0C File Offset: 0x0010010C
			private bool TrySelectRowsOverTableKeys(BinaryQueryExpression binary, FunctionValue condition, out TableValue tableValue)
			{
				if (binary.Operator == BinaryOperator2.And)
				{
					BinaryQueryExpression binaryQueryExpression = binary.Left as BinaryQueryExpression;
					BinaryQueryExpression binaryQueryExpression2 = binary.Right as BinaryQueryExpression;
					string text;
					string text2;
					if (binaryQueryExpression != null && binaryQueryExpression2 != null && this.TryGetColumnTextInBinary("Folder Path", binaryQueryExpression, binaryQueryExpression2, out text) && this.TryGetColumnTextInBinary("Name", binaryQueryExpression, binaryQueryExpression2, out text2) && UriHelper.EndsWithPathSeparator(text) && text.StartsWith(this.folderUrl, StringComparison.Ordinal))
					{
						tableValue = new QueryTableValue(new HdfsModule.HdfsTableValue(this.host, this.sourceUrl, text.Substring(0, text.Length - 1), this.credentials, this.folderOptions)).SelectRows(condition);
						return true;
					}
				}
				tableValue = null;
				return false;
			}

			// Token: 0x06004DA5 RID: 19877 RVA: 0x00101FBC File Offset: 0x001001BC
			private bool TryGetColumnTextInBinary(string columnName, BinaryQueryExpression leftExpression, BinaryQueryExpression rightExpression, out string value)
			{
				return this.TryGetColumnText(columnName, leftExpression.Left, leftExpression.Right, out value) || this.TryGetColumnText(columnName, leftExpression.Right, leftExpression.Left, out value) || this.TryGetColumnText(columnName, rightExpression.Left, rightExpression.Right, out value) || this.TryGetColumnText(columnName, rightExpression.Right, rightExpression.Left, out value);
			}

			// Token: 0x06004DA6 RID: 19878 RVA: 0x00102028 File Offset: 0x00100228
			private static string GetFileName(string url)
			{
				UriBuilder uriBuilder = new UriBuilder(url);
				int num = uriBuilder.Path.LastIndexOf('/') + 1;
				return uriBuilder.Path.Substring(num, uriBuilder.Path.Length - num);
			}

			// Token: 0x0400299C RID: 10652
			private readonly ResourceCredentialCollection credentials;

			// Token: 0x0400299D RID: 10653
			private readonly FileHelper.FolderOptions folderOptions;

			// Token: 0x0400299E RID: 10654
			private readonly string sourceUrl;

			// Token: 0x0400299F RID: 10655
			private readonly string folderUrl;

			// Token: 0x040029A0 RID: 10656
			private readonly IEngineHost host;

			// Token: 0x040029A1 RID: 10657
			private readonly string fileNamePrefix;

			// Token: 0x02000AE7 RID: 2791
			private sealed class HdfsTableEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06004DA7 RID: 19879 RVA: 0x00102065 File Offset: 0x00100265
				public HdfsTableEnumerator(IEngineHost host, string folderUrl, IEnumerator<IValueReference> fileEnumerator, IEnumerator<IValueReference> folderEnumerator, ResourceCredentialCollection credentials, FileHelper.FolderOptions folderOptions)
				{
					this.host = host;
					this.folderUrl = folderUrl;
					this.fileEnumerator = fileEnumerator;
					this.folderEnumerator = folderEnumerator;
					this.credentials = credentials;
					this.folderOptions = folderOptions;
				}

				// Token: 0x17001848 RID: 6216
				// (get) Token: 0x06004DA8 RID: 19880 RVA: 0x0010209A File Offset: 0x0010029A
				public IValueReference Current
				{
					get
					{
						return this.current;
					}
				}

				// Token: 0x17001849 RID: 6217
				// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x001020A2 File Offset: 0x001002A2
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06004DAA RID: 19882 RVA: 0x001020AA File Offset: 0x001002AA
				public void Dispose()
				{
					this.current = null;
					this.DisposeFileEnumerator();
					this.DisposeFolderEnumerator();
				}

				// Token: 0x06004DAB RID: 19883 RVA: 0x001020BF File Offset: 0x001002BF
				private void DisposeFileEnumerator()
				{
					if (this.fileEnumerator != null)
					{
						this.fileEnumerator.Dispose();
						this.fileEnumerator = null;
					}
				}

				// Token: 0x06004DAC RID: 19884 RVA: 0x001020DB File Offset: 0x001002DB
				private void DisposeFolderEnumerator()
				{
					if (this.folderEnumerator != null)
					{
						this.folderEnumerator.Dispose();
						this.folderEnumerator = null;
					}
				}

				// Token: 0x06004DAD RID: 19885 RVA: 0x001020F8 File Offset: 0x001002F8
				public bool MoveNext()
				{
					while (this.fileEnumerator != null)
					{
						if (this.fileEnumerator.MoveNext())
						{
							this.current = this.fileEnumerator.Current.Value.AsRecord;
							return true;
						}
						this.DisposeFileEnumerator();
						if (this.folderEnumerator != null && this.folderEnumerator.MoveNext())
						{
							this.fileEnumerator = new HdfsModule.HdfsTableValue(this.host, null, HdfsModule.HdfsTableValue.HdfsTableEnumerator.GetFolderUrl(this.folderUrl, this.folderEnumerator.Current.Value.AsRecord), this.credentials, this.folderOptions | FileHelper.FolderOptions.EnumeratingSubEntries).GetEnumerator();
						}
					}
					this.current = null;
					return false;
				}

				// Token: 0x06004DAE RID: 19886 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x06004DAF RID: 19887 RVA: 0x001021A8 File Offset: 0x001003A8
				private static string GetFolderUrl(string baseUrl, RecordValue record)
				{
					TextValue asText = record["Name"].AsText;
					UriBuilder uriBuilder = new UriBuilder(baseUrl);
					uriBuilder.Path = uriBuilder.Path.TrimEnd(new char[] { '/' }) + "/" + asText.String;
					return Uri.UnescapeDataString(uriBuilder.Uri.AbsoluteUri);
				}

				// Token: 0x040029A2 RID: 10658
				private readonly ResourceCredentialCollection credentials;

				// Token: 0x040029A3 RID: 10659
				private readonly string folderUrl;

				// Token: 0x040029A4 RID: 10660
				private readonly FileHelper.FolderOptions folderOptions;

				// Token: 0x040029A5 RID: 10661
				private readonly IEngineHost host;

				// Token: 0x040029A6 RID: 10662
				private RecordValue current;

				// Token: 0x040029A7 RID: 10663
				private IEnumerator<IValueReference> fileEnumerator;

				// Token: 0x040029A8 RID: 10664
				private IEnumerator<IValueReference> folderEnumerator;
			}

			// Token: 0x02000AE8 RID: 2792
			private sealed class HdfsListEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06004DB0 RID: 19888 RVA: 0x00102209 File Offset: 0x00100409
				public HdfsListEnumerator(IEngineHost host, string folderUrl, string fileNamePrefix, IEnumerator<IValueReference> listEnumerator, ResourceCredentialCollection credentials, FileHelper.FolderOptions folderOptions)
				{
					this.host = host;
					this.folderUrl = folderUrl;
					this.fileNamePrefix = fileNamePrefix;
					this.listEnumerator = listEnumerator;
					this.credentials = credentials;
					this.folderOptions = folderOptions;
				}

				// Token: 0x1700184A RID: 6218
				// (get) Token: 0x06004DB1 RID: 19889 RVA: 0x0010223E File Offset: 0x0010043E
				public IValueReference Current
				{
					get
					{
						return this.current;
					}
				}

				// Token: 0x1700184B RID: 6219
				// (get) Token: 0x06004DB2 RID: 19890 RVA: 0x00102246 File Offset: 0x00100446
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06004DB3 RID: 19891 RVA: 0x0010224E File Offset: 0x0010044E
				public void Dispose()
				{
					this.current = null;
					this.DisposeListEnumerator();
				}

				// Token: 0x06004DB4 RID: 19892 RVA: 0x0010225D File Offset: 0x0010045D
				private void DisposeListEnumerator()
				{
					if (this.listEnumerator != null)
					{
						this.listEnumerator.Dispose();
						this.listEnumerator = null;
					}
				}

				// Token: 0x06004DB5 RID: 19893 RVA: 0x0010227C File Offset: 0x0010047C
				public bool MoveNext()
				{
					while (this.listEnumerator.MoveNext())
					{
						RecordValue asRecord = this.listEnumerator.Current.Value.AsRecord;
						if (string.IsNullOrEmpty(this.fileNamePrefix) || asRecord["pathSuffix"].AsString.StartsWith(this.fileNamePrefix, StringComparison.Ordinal))
						{
							if ("DIRECTORY".Equals(asRecord["type"].AsString, StringComparison.OrdinalIgnoreCase))
							{
								if (FileHelper.EnumerateFolders(this.folderOptions))
								{
									this.current = HdfsModule.HdfsRecordValue.New(this.host, this.folderUrl, this.credentials, asRecord, FileHelper.FolderKind);
									return true;
								}
							}
							else if (FileHelper.EnumerateFiles(this.folderOptions))
							{
								this.current = HdfsModule.HdfsRecordValue.New(this.host, this.folderUrl, this.credentials, asRecord, FileHelper.FileKind);
								return true;
							}
						}
					}
					this.current = null;
					return false;
				}

				// Token: 0x06004DB6 RID: 19894 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x040029A9 RID: 10665
				private readonly ResourceCredentialCollection credentials;

				// Token: 0x040029AA RID: 10666
				private readonly string folderUrl;

				// Token: 0x040029AB RID: 10667
				private readonly string fileNamePrefix;

				// Token: 0x040029AC RID: 10668
				private readonly FileHelper.FolderOptions folderOptions;

				// Token: 0x040029AD RID: 10669
				private readonly IEngineHost host;

				// Token: 0x040029AE RID: 10670
				private RecordValue current;

				// Token: 0x040029AF RID: 10671
				private IEnumerator<IValueReference> listEnumerator;
			}
		}
	}
}
