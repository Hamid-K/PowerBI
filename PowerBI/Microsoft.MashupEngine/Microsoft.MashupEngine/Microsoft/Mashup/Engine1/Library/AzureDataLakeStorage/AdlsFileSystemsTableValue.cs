using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.AzureBase;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000ED1 RID: 3793
	internal sealed class AdlsFileSystemsTableValue : TableValue
	{
		// Token: 0x060064C6 RID: 25798 RVA: 0x00158F20 File Offset: 0x00157120
		public AdlsFileSystemsTableValue(IEngineHost host, List<AdlsEndpoint> endpoints, string resourceKind, OptionsRecord options)
		{
			this.host = host;
			this.endpoints = endpoints;
			this.resourceKind = resourceKind;
			this.options = options;
		}

		// Token: 0x17001D5C RID: 7516
		// (get) Token: 0x060064C7 RID: 25799 RVA: 0x00158F45 File Offset: 0x00157145
		public override TypeValue Type
		{
			get
			{
				return FileSystemTableHelper.AddTypeMetadata(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles), AdlsEndpoint.GetFolderUrl(this.endpoints), "/", true);
			}
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x00158F63 File Offset: 0x00157163
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return this.GetFileSystemsNavTable().GetEnumerator();
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x00158F70 File Offset: 0x00157170
		private TableValue GetFileSystemsNavTable()
		{
			List<Value> list = new List<Value>();
			foreach (AdlsEndpoint adlsEndpoint in this.endpoints)
			{
				list.Add(RecordValue.New(FileHelper.FileEntryKeys, new Value[]
				{
					new AdlsTableValue(this.host, new List<AdlsEndpoint> { adlsEndpoint }, this.resourceKind, this.options, true, null),
					TextValue.New(adlsEndpoint.FileSystem),
					Value.Null,
					Value.Null,
					Value.Null,
					Value.Null,
					Value.Null,
					TextValue.New(AzureBaseHelper.FormatContainerString(adlsEndpoint.BaseEndpoint))
				}));
			}
			return ListValue.New(list.ToArray()).ToTable(this.Type.AsTableType);
		}

		// Token: 0x060064CA RID: 25802 RVA: 0x0015906C File Offset: 0x0015726C
		public static TableValue CreateView(IEngineHost host, TextValue urlTextValue, string resourceKind, OptionsRecord optionsRecord, bool isHierarchical)
		{
			bool flag = true;
			List<AdlsEndpoint> list = new List<AdlsEndpoint>();
			AdlsEndpoint adlsEndpoint;
			if (AdlsEndpoint.TryCreateWithFileSystem(urlTextValue, out adlsEndpoint))
			{
				flag = false;
				list.Add(adlsEndpoint);
			}
			else
			{
				string @string = AdlsHelper.GetHttpUri(urlTextValue, null).String;
				IResource resource = Resource.New(resourceKind, @string);
				bool @bool = optionsRecord.GetBool("IsOneLake", false);
				Request request = AdlsHelper.CreateRequest(host, resource, TextValue.New(@string), AdlsHelper.ListFilesystems, null, null, @bool);
				string text = null;
				int num = 0;
				for (;;)
				{
					using (Response response = AzureBaseHelper.GetResponse(request, null, null))
					{
						Encoding encoding = Encoding.UTF8;
						if (!string.IsNullOrEmpty(response.CharacterSet))
						{
							encoding = Encoding.GetEncoding(response.CharacterSet);
						}
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), encoding))
						{
							Value value = null;
							try
							{
								value = JsonParser.Parse(streamReader, null);
							}
							catch (ValueException ex)
							{
								Message3 message = Strings.HDInsightFailedJsonException(resourceKind, request.InitialUri, ex.Message);
								throw HttpServices.NewDataSourceError<Message3>(host, message, request.RequestResource, request.InitialUri);
							}
							ListValue asList = value.AsRecord["filesystems"].AsList;
							int num2 = 0;
							while ((long)num2 < asList.LargeCount)
							{
								TextValue asText = asList[num2].AsRecord["name"].AsText;
								AdlsEndpoint adlsEndpoint2;
								if (AdlsEndpoint.TryCreateWithFileSystem(AdlsHelper.GetHttpUri(TextValue.New(@string), asText.AsText), out adlsEndpoint2))
								{
									list.Add(adlsEndpoint2);
								}
								num2++;
							}
							string text2 = response.Headers["x-ms-continuation"];
							if (!string.IsNullOrEmpty(text2))
							{
								RecordValue recordValue = RecordValue.New(Keys.New("resource", "continuation"), new Value[]
								{
									TextValue.New("account"),
									TextValue.New(text2)
								});
								request = AdlsHelper.CreateRequest(host, resource, TextValue.New(@string), recordValue, null, null, @bool);
								if (string.Equals(text2, text, StringComparison.Ordinal))
								{
									using (IHostTrace hostTrace = TracingService.CreateTrace(host, "Engine/IO/AzureStorage/GetDirectoryListing/DuplicateToken", TraceEventType.Error, null))
									{
										hostTrace.Add("continuationToken", text2, true);
										hostTrace.Add("previousContinuationToken", text, true);
										num++;
										if (num == 1)
										{
											throw DataSourceException.NewDataSourceError<Message0>(host, Strings.DuplicateContinuationToken, resource, null, null);
										}
										request.IsRetry = true;
										continue;
									}
								}
								text = text2;
								num = 0;
								continue;
							}
						}
					}
					break;
				}
			}
			if (flag && isHierarchical)
			{
				return new AdlsFileSystemsTableValue(host, list, resourceKind, optionsRecord);
			}
			return new AdlsTableValue(host, list, resourceKind, optionsRecord, isHierarchical, null);
		}

		// Token: 0x040036FF RID: 14079
		private readonly IEngineHost host;

		// Token: 0x04003700 RID: 14080
		private readonly List<AdlsEndpoint> endpoints;

		// Token: 0x04003701 RID: 14081
		private readonly string resourceKind;

		// Token: 0x04003702 RID: 14082
		private readonly OptionsRecord options;
	}
}
