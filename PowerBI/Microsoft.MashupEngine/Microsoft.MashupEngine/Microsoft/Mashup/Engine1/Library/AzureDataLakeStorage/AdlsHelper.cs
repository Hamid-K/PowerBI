using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.AzureBase;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000ED2 RID: 3794
	internal static class AdlsHelper
	{
		// Token: 0x060064CB RID: 25803 RVA: 0x00159344 File Offset: 0x00157544
		public static Request CreateRequest(IEngineHost engineHost, IResource resource, TextValue url, Value query = null, Value headers = null, Value content = null, bool isOneLake = false)
		{
			TextValue textValue = AdlsHelper.DetermineVersionToUse(engineHost, AdlsHelper.June2018Version);
			return AzureBaseHelper.CreateRequest(engineHost, resource, url, textValue, query, headers, content, isOneLake);
		}

		// Token: 0x060064CC RID: 25804 RVA: 0x00159370 File Offset: 0x00157570
		public static Value GetResponse(Request request, int[] expectedStatusCodes, bool ignoreErrors, out string continuation)
		{
			Value value;
			using (Response response = AzureBaseHelper.GetResponse(request, null, expectedStatusCodes))
			{
				continuation = response.Headers["x-ms-continuation"];
				if (response.ContentLength == 0L)
				{
					value = Value.Null;
				}
				else
				{
					Encoding encoding = ((!string.IsNullOrEmpty(response.CharacterSet)) ? Encoding.GetEncoding(response.CharacterSet) : Encoding.UTF8);
					RecordValue asRecord;
					using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), encoding))
					{
						try
						{
							asRecord = JsonParser.Parse(streamReader, null).AsRecord;
						}
						catch (ValueException ex)
						{
							Message3 message = Strings.HDInsightFailedJsonException(request.ResourceKind, request.InitialUri, ex.Message);
							throw HttpServices.NewDataSourceError<Message3>(request.Host, message, request.RequestResource, request.InitialUri);
						}
					}
					try
					{
						AdlsHelper.CheckResponseForErrors(request.Host, asRecord, request.RequestResource);
					}
					catch (ValueException obj) when (ignoreErrors)
					{
					}
					value = asRecord;
				}
			}
			return value;
		}

		// Token: 0x060064CD RID: 25805 RVA: 0x0015949C File Offset: 0x0015769C
		public static void CheckResponseForErrors(IEngineHost engineHost, RecordValue responseDocument, IResource resource)
		{
			if (responseDocument.Keys.Contains("error"))
			{
				RecordValue asRecord = responseDocument["error"].AsRecord;
				string asString = asRecord["message"].AsString;
				throw DataSourceException.NewDataSourceError(engineHost, string.Format(CultureInfo.CurrentCulture, "{0}. {1}", asString, asRecord["code"].AsString), resource, null, null);
			}
		}

		// Token: 0x060064CE RID: 25806 RVA: 0x00159508 File Offset: 0x00157708
		public static TextValue GetHttpUri(TextValue baseEndpoint, TextValue fileSystemName = null)
		{
			Uri uri;
			if (!Uri.TryCreate(baseEndpoint.String, UriKind.Absolute, out uri))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.AzureInvalidAccountURL, baseEndpoint, null);
			}
			UriBuilder uriBuilder = new UriBuilder(uri);
			uriBuilder.Scheme = Uri.UriSchemeHttps;
			if (fileSystemName != null)
			{
				uriBuilder.Path = fileSystemName.String + "/";
			}
			return TextValue.New(uriBuilder.Uri.ToString());
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x00159570 File Offset: 0x00157770
		internal static TextValue DetermineVersionToUse(IEngineHost engineHost, TextValue defaultVersion)
		{
			object obj;
			if (engineHost.TryGetConfigurationProperty("MashupFlight_EnableAdlsVersion2021", out obj))
			{
				bool? flag = obj as bool?;
				if (flag != null && flag.Value)
				{
					return AdlsHelper.December2021Version;
				}
			}
			return defaultVersion;
		}

		// Token: 0x04003703 RID: 14083
		public static readonly TextValue June2018Version = TextValue.New("2018-06-17");

		// Token: 0x04003704 RID: 14084
		public static readonly TextValue August2020Version = TextValue.New("2020-08-04");

		// Token: 0x04003705 RID: 14085
		public static readonly TextValue December2021Version = TextValue.New("2021-12-02");

		// Token: 0x04003706 RID: 14086
		public const string RecursiveQueryKey = "recursive";

		// Token: 0x04003707 RID: 14087
		public const string ContinuationQueryKey = "continuation";

		// Token: 0x04003708 RID: 14088
		public const string FileQueryKey = "file";

		// Token: 0x04003709 RID: 14089
		public const string DirectoryQueryKey = "directory";

		// Token: 0x0400370A RID: 14090
		public const string ResourceQueryKey = "resource";

		// Token: 0x0400370B RID: 14091
		public const string MaxResultsQueryKey = "maxResults";

		// Token: 0x0400370C RID: 14092
		public const string PositionQueryKey = "position";

		// Token: 0x0400370D RID: 14093
		public const string ActionQueryKey = "action";

		// Token: 0x0400370E RID: 14094
		public const string FilesystemKey = "filesystem";

		// Token: 0x0400370F RID: 14095
		public const string FilesystemsKey = "filesystems";

		// Token: 0x04003710 RID: 14096
		public const string AccountKey = "account";

		// Token: 0x04003711 RID: 14097
		public const string ListEntryNameKey = "name";

		// Token: 0x04003712 RID: 14098
		public const string ListEntryIsDirectoryKey = "isDirectory";

		// Token: 0x04003713 RID: 14099
		public const string ListEntryLastModifiedKey = "lastModified";

		// Token: 0x04003714 RID: 14100
		public const string ListEntryContentLengthKey = "contentLength";

		// Token: 0x04003715 RID: 14101
		public const string CalculatedFolderPathKey = "folderPath";

		// Token: 0x04003716 RID: 14102
		public const string ListEntryGroupKey = "group";

		// Token: 0x04003717 RID: 14103
		public const string ListEntryOwnerKey = "owner";

		// Token: 0x04003718 RID: 14104
		public const string ListEntryPermissionsKey = "permissions";

		// Token: 0x04003719 RID: 14105
		public const string AttributeGroupKey = "Group";

		// Token: 0x0400371A RID: 14106
		public const string AttributeOwnerKey = "Owner";

		// Token: 0x0400371B RID: 14107
		public const string AttributePermissionsKey = "Permissions";

		// Token: 0x0400371C RID: 14108
		public const string RecursiveTrueKey = "true";

		// Token: 0x0400371D RID: 14109
		public const string RecursiveFalseKey = "false";

		// Token: 0x0400371E RID: 14110
		public const string ResponsePathsKey = "paths";

		// Token: 0x0400371F RID: 14111
		public const string ResponseErrorKey = "error";

		// Token: 0x04003720 RID: 14112
		public const string ResponseErrorMessageKey = "message";

		// Token: 0x04003721 RID: 14113
		public const string ResponseErrorCodeKey = "code";

		// Token: 0x04003722 RID: 14114
		public const string ActionFlushKey = "flush";

		// Token: 0x04003723 RID: 14115
		public const string ActionAppendKey = "append";

		// Token: 0x04003724 RID: 14116
		public static readonly Keys ListEntryRecordKeys = Keys.New(new string[] { "Name", "Url", "Last-Modified", "Content-Length", "FolderPath", "group", "owner", "permissions", "isDirectory" });

		// Token: 0x04003725 RID: 14117
		public static readonly Keys ListEntryAttributeKeys = Keys.New(new string[] { "Content Type", "Kind", "Size", "Group", "Owner", "Permissions" });

		// Token: 0x04003726 RID: 14118
		public static readonly Keys PatchBlockListKeys = Keys.New("action", "position");

		// Token: 0x04003727 RID: 14119
		public static readonly Keys FlushBlockListKeys = Keys.New("action", "position", "x-ms-content-type");

		// Token: 0x04003728 RID: 14120
		public static readonly RecordValue ListFilesystems = RecordValue.New(Keys.New("resource"), new Value[] { TextValue.New("account") });

		// Token: 0x04003729 RID: 14121
		public static readonly RecordValue ListBlobs = RecordValue.New(Keys.New("resource", "recursive"), new Value[]
		{
			TextValue.New("filesystem"),
			TextValue.New("false")
		});

		// Token: 0x0400372A RID: 14122
		public static readonly RecordValue ListBlobsRecursive = RecordValue.New(Keys.New("resource", "recursive"), new Value[]
		{
			TextValue.New("filesystem"),
			TextValue.New("true")
		});
	}
}
