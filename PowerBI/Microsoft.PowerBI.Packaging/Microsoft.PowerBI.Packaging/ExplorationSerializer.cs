using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.Packaging.Host;
using Microsoft.PowerBI.Packaging.Project;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000019 RID: 25
	public class ExplorationSerializer : IExplorationSerializer
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002BF5 File Offset: 0x00000DF5
		private ExplorationSerializer.PublicEntryDeserializationScope DeserializationScope
		{
			get
			{
				ExplorationSerializer.PublicEntryDeserializationScope publicEntryDeserializationScope = this.deserializationScopeDontGet;
				if (publicEntryDeserializationScope == null)
				{
					throw new InvalidOperationException("Current code is running without deserialization scope.");
				}
				return publicEntryDeserializationScope;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002C0B File Offset: 0x00000E0B
		public ExplorationSerializer(HostContext hostContext, IV2ExplorationSchemas validationSchemas = null, IV2ExplorationSchemaKeyResolver schemaKeyResolver = null)
		{
			HostSetup.SetHostApplication(hostContext.Application);
			this.hostApplication = hostContext.Application;
			this.validationSchemas = validationSchemas;
			this.schemaKeyResolver = schemaKeyResolver ?? new V2ExplorationSchemaKeyResolver();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002C44 File Offset: 0x00000E44
		public IDictionary<Uri, IStreamablePowerBIProjectPartContent> Serialize(string exploration, string mobileState = null)
		{
			return this.SerializeToBytes(exploration, mobileState).ToDictionary((KeyValuePair<Uri, byte[]> kp) => kp.Key, (KeyValuePair<Uri, byte[]> kp) => PBIProjectUtils.AsStreamableContent(kp.Value));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002C9C File Offset: 0x00000E9C
		public IDictionary<Uri, IStreamablePowerBIPackagePartContent> SerializeToPackage(string exploration, string mobileState = null)
		{
			return this.SerializeToBytes(exploration, mobileState).ToDictionary((KeyValuePair<Uri, byte[]> kp) => kp.Key, (KeyValuePair<Uri, byte[]> kp) => PowerBIPackagingUtils.AsStreamableContent(kp.Value));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002CF4 File Offset: 0x00000EF4
		private IDictionary<Uri, byte[]> SerializeToBytes(string exploration, string mobileState)
		{
			ExplorationContract explorationContract = JsonConvert.DeserializeObject<ExplorationContract>(exploration, V2ExplorationUtils.ExplorationSerializerSettings);
			MobileStateContract mobileStateContract = (string.IsNullOrEmpty(mobileState) ? null : JsonConvert.DeserializeObject<MobileStateContract>(mobileState, V2ExplorationUtils.ExplorationSerializerSettings));
			explorationContract.Report.Content = this.NormalizeExplorationResourcePackages(explorationContract.Report.Content);
			return this.Serialize(explorationContract, mobileStateContract);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D48 File Offset: 0x00000F48
		private Dictionary<Uri, byte[]> Serialize(ExplorationContract exploration, MobileStateContract mobileState)
		{
			Dictionary<Uri, byte[]> dictionary = new Dictionary<Uri, byte[]>();
			ExplorationSerializer.AddArtifactToFiles(exploration.Version, dictionary, "");
			ExplorationSerializer.AddArtifactToFiles(exploration.Report, dictionary, "");
			if (exploration.ReportExtensions != null)
			{
				ExplorationSerializer.AddArtifactToFiles(exploration.ReportExtensions, dictionary, "");
			}
			if (exploration.Pages != null)
			{
				if (exploration.Pages.PagesMetadata != null)
				{
					ExplorationSerializer.AddArtifactToFiles(exploration.Pages.PagesMetadata, dictionary, "");
				}
				using (IEnumerator<Page> enumerator = exploration.Pages.PagesList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Page page = enumerator.Current;
						string text = Path.GetDirectoryName(page.FilePath).Replace("\\", "/");
						string text2 = string.Join("/", new string[]
						{
							ExplorationSerializer.PagesFolderName,
							text
						});
						ExplorationSerializer.AddArtifactToFiles(page, dictionary, ExplorationSerializer.PagesFolderName);
						if (page.VisualContainers != null)
						{
							foreach (ExplorationArtifact explorationArtifact in page.VisualContainers)
							{
								if (mobileState == null || !ExplorationSerializer.HasSuffix(explorationArtifact.FilePath, new string[] { ExplorationSerializer.MobileVisualFileNameInFolder }))
								{
									ExplorationSerializer.AddArtifactToFiles(explorationArtifact, dictionary, text2);
								}
							}
							MobileStatePage mobileStatePage;
							if (mobileState == null)
							{
								mobileStatePage = null;
							}
							else
							{
								MobileStatePages pages = mobileState.Pages;
								if (pages == null)
								{
									mobileStatePage = null;
								}
								else
								{
									NonNulls<MobileStatePage> pagesList = pages.PagesList;
									mobileStatePage = ((pagesList != null) ? pagesList.FirstOrDefault((MobileStatePage p) => p.FilePath == page.FilePath) : null);
								}
							}
							MobileStatePage mobileStatePage2 = mobileStatePage;
							if (mobileStatePage2 != null)
							{
								NonNulls<ExplorationArtifact> visualContainers = mobileStatePage2.VisualContainers;
								int? num = ((visualContainers != null) ? new int?(visualContainers.Count) : null);
								int num2 = 0;
								if ((num.GetValueOrDefault() > num2) & (num != null))
								{
									foreach (ExplorationArtifact explorationArtifact2 in mobileStatePage2.VisualContainers.Where((ExplorationArtifact file) => ExplorationSerializer.HasSuffix(file.FilePath, new string[] { ExplorationSerializer.MobileVisualFileNameInFolder })).Where(delegate(ExplorationArtifact mobileFile)
									{
										string text4 = mobileFile.FilePath.Remove(mobileFile.FilePath.Length - ExplorationSerializer.MobileVisualFileName.Length) + ExplorationSerializer.VisualFileName;
										return page.VisualContainers.Select((ExplorationArtifact file) => file.FilePath).Contains(text4);
									}))
									{
										ExplorationSerializer.AddArtifactToFiles(explorationArtifact2, dictionary, text2);
									}
								}
							}
						}
					}
				}
			}
			if (exploration.Bookmarks != null)
			{
				ExplorationSerializer.AddArtifactToFiles(exploration.Bookmarks, dictionary, "");
				string text3 = Path.GetDirectoryName(exploration.Bookmarks.FilePath).Replace("\\", "/");
				foreach (ExplorationArtifact explorationArtifact3 in exploration.Bookmarks.BookmarksList)
				{
					ExplorationSerializer.AddArtifactToFiles(explorationArtifact3, dictionary, text3);
				}
			}
			return dictionary;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003078 File Offset: 0x00001278
		public async Task<ExplorationDeserializationResult<ExplorationContract>> DeserializeAsync(IDictionary<Uri, IStreamablePowerBIProjectPartContent> files, CancellationToken cancellationToken)
		{
			ExplorationSerializer.<>c__DisplayClass30_0 CS$<>8__locals1 = new ExplorationSerializer.<>c__DisplayClass30_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.files = files;
			return await new ExplorationSerializer.PublicEntryDeserializationScope(this, cancellationToken, CS$<>8__locals1.files.Keys).RunAsync<ExplorationContract>(new Func<Task<ExplorationContract>>(CS$<>8__locals1.<DeserializeAsync>g__Body|0));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030CC File Offset: 0x000012CC
		public async Task<ExplorationDeserializationResult<ExplorationContractSplit>> DeserializeWithSeparateMobileStateAsync(IDictionary<Uri, IStreamablePowerBIProjectPartContent> files, CancellationToken cancellationToken)
		{
			ExplorationSerializer.<>c__DisplayClass31_0 CS$<>8__locals1 = new ExplorationSerializer.<>c__DisplayClass31_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.files = files;
			return await new ExplorationSerializer.PublicEntryDeserializationScope(this, cancellationToken, CS$<>8__locals1.files.Keys).RunAsync<ExplorationContractSplit>(new Func<Task<ExplorationContractSplit>>(CS$<>8__locals1.<DeserializeWithSeparateMobileStateAsync>g__Body|0));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003120 File Offset: 0x00001320
		public async Task<ExplorationDeserializationResult<ExplorationContractSplit>> DeserializeWithSeparateMobileStateAsync(IDictionary<Uri, IStreamablePowerBIPackagePartContent> files, CancellationToken cancellationToken)
		{
			ExplorationSerializer.<>c__DisplayClass32_0 CS$<>8__locals1 = new ExplorationSerializer.<>c__DisplayClass32_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.files = files;
			return await new ExplorationSerializer.PublicEntryDeserializationScope(this, cancellationToken, CS$<>8__locals1.files.Keys).RunAsync<ExplorationContractSplit>(new Func<Task<ExplorationContractSplit>>(CS$<>8__locals1.<DeserializeWithSeparateMobileStateAsync>g__Body|0));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003174 File Offset: 0x00001374
		private JToken NormalizeExplorationResourcePackages(JToken reportContent)
		{
			JToken jtoken = reportContent[ExplorationSerializer.ResourcePackagesProperty];
			if (jtoken == null)
			{
				return reportContent;
			}
			reportContent = ReportExplorationNormalizer.NormalizeV2Exploration(reportContent as JObject);
			foreach (JObject jobject in jtoken.Children<JObject>())
			{
				JArray jarray = (JArray)jobject["items"];
				if (jarray != null)
				{
					foreach (JToken jtoken2 in jarray)
					{
						JObject jobject2 = (JObject)jtoken2;
						jobject2["path"] = this.GetResourceItemPath(jobject, jobject2);
					}
				}
			}
			return reportContent;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003248 File Offset: 0x00001448
		private string GetResourceItemPath(JObject resourcePackage, JObject item)
		{
			JToken jtoken = item["path"];
			string text = ((jtoken != null) ? jtoken.ToString() : null);
			string text2 = resourcePackage["type"].ToString();
			if (!(text2 == "CustomVisual"))
			{
				if (!(text2 == "RegisteredResources"))
				{
					if (!(text2 == "SharedResources"))
					{
					}
				}
				else
				{
					text = item["name"].ToString();
				}
			}
			else
			{
				text = Path.GetFileName(item["name"].ToString());
			}
			return text;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000032D4 File Offset: 0x000014D4
		public async Task<ExplorationDeserializationResult<string>> DeserializeToStringAsync(IDictionary<Uri, IStreamablePowerBIProjectPartContent> files, CancellationToken cancellationToken)
		{
			ExplorationDeserializationResult<ExplorationContract> explorationDeserializationResult = await this.DeserializeAsync(files, cancellationToken);
			ExplorationDeserializationResult<ExplorationContract> dresult = explorationDeserializationResult;
			TaskAwaiter<string> taskAwaiter = ExplorationSerializer.JsonSerializeObjectAsync<ExplorationContract>(dresult.Value, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<string> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<string>);
			}
			return new ExplorationDeserializationResult<string>(taskAwaiter.GetResult(), dresult.Warnings);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003328 File Offset: 0x00001528
		public async Task<ExplorationDeserializationResult<ExplorationContract>> DeserializeAsync(IDictionary<Uri, IStreamablePowerBIPackagePartContent> files, CancellationToken cancellationToken)
		{
			ExplorationSerializer.<>c__DisplayClass36_0 CS$<>8__locals1 = new ExplorationSerializer.<>c__DisplayClass36_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.files = files;
			return await new ExplorationSerializer.PublicEntryDeserializationScope(this, cancellationToken, CS$<>8__locals1.files.Keys).RunAsync<ExplorationContract>(new Func<Task<ExplorationContract>>(CS$<>8__locals1.<DeserializeAsync>g__Body|0));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000337C File Offset: 0x0000157C
		public async Task<ExplorationDeserializationResult<string>> DeserializeToStringAsync(IDictionary<Uri, IStreamablePowerBIPackagePartContent> files, CancellationToken cancellationToken)
		{
			ExplorationDeserializationResult<ExplorationContract> explorationDeserializationResult = await this.DeserializeAsync(files, cancellationToken);
			ExplorationDeserializationResult<ExplorationContract> dresult = explorationDeserializationResult;
			TaskAwaiter<string> taskAwaiter = ExplorationSerializer.JsonSerializeObjectAsync<ExplorationContract>(dresult.Value, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<string> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<string>);
			}
			return new ExplorationDeserializationResult<string>(taskAwaiter.GetResult(), dresult.Warnings);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000033D0 File Offset: 0x000015D0
		private async Task<ExplorationContractSplit> DeserializeExplorationContractSplit()
		{
			ExplorationContract explorationContract2 = await this.DeserializeAsync(ExplorationSerializer.VisualDeserilizeOption.MainOnly);
			ExplorationContract explorationContract = explorationContract2;
			Pages pages = await this.DeserializeExplorationPagesAsync(ExplorationSerializer.VisualDeserilizeOption.MobileOnly);
			ExplorationContractSplit explorationContractSplit = new ExplorationContractSplit();
			explorationContractSplit.ExplorationContract = explorationContract;
			MobileStateContract mobileStateContract2;
			if (pages.PagesList.Count != 0)
			{
				MobileStateContract mobileStateContract = (mobileStateContract2 = new MobileStateContract());
				MobileStatePages mobileStatePages = new MobileStatePages();
				mobileStatePages.PagesList = new NonNulls<MobileStatePage>(pages.PagesList.Select((Page p) => new MobileStatePage
				{
					FilePath = p.FilePath,
					VisualContainers = p.VisualContainers
				}));
				mobileStateContract.Pages = mobileStatePages;
			}
			else
			{
				mobileStateContract2 = null;
			}
			explorationContractSplit.MobileState = mobileStateContract2;
			return explorationContractSplit;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003414 File Offset: 0x00001614
		private static async Task<string> JsonSerializeObjectAsync<T>(T obj, CancellationToken cancellationToken)
		{
			ExplorationSerializer.<>c__DisplayClass39_0<T> CS$<>8__locals1 = new ExplorationSerializer.<>c__DisplayClass39_0<T>();
			CS$<>8__locals1.obj = obj;
			return await Task.Run<string>(new Func<string>(CS$<>8__locals1.<JsonSerializeObjectAsync>g__Body|0));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003458 File Offset: 0x00001658
		private Task<byte[]> GetBytesFromStreamablePackageContentAsync(IStreamablePowerBIPackagePartContent content)
		{
			ExplorationSerializer.<>c__DisplayClass40_0 CS$<>8__locals1;
			CS$<>8__locals1.content = content;
			this.DeserializationScope.CancellationToken.ThrowIfCancellationRequested();
			return Task.FromResult<byte[]>(ExplorationSerializer.<GetBytesFromStreamablePackageContentAsync>g__Body|40_0(ref CS$<>8__locals1));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000348C File Offset: 0x0000168C
		private async Task<byte[]> GetBytesFromStreamableProjectContentAsync(IStreamablePowerBIProjectPartContent content)
		{
			this.DeserializationScope.CancellationToken.ThrowIfCancellationRequested();
			return await PBIProjectUtils.ReadAllBytesAsync(content, true);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000034D8 File Offset: 0x000016D8
		private async Task<ExplorationContract> DeserializeAsync(ExplorationSerializer.VisualDeserilizeOption visualDeserilizeOption)
		{
			ExplorationContract exploration = new ExplorationContract();
			ExplorationContract explorationContract = exploration;
			ExplorationArtifact explorationArtifact = await this.DeserializeRootExplorationArtifactAsync(ExplorationSerializer.VersionFileName, true);
			explorationContract.Version = explorationArtifact;
			explorationContract = null;
			explorationContract = exploration;
			explorationArtifact = await this.DeserializeRootExplorationArtifactAsync(ExplorationSerializer.ReportFileName, true);
			explorationContract.Report = explorationArtifact;
			explorationContract = null;
			explorationContract = exploration;
			explorationArtifact = await this.DeserializeRootExplorationArtifactAsync(ExplorationSerializer.ReportExtensionsFileName, false);
			explorationContract.ReportExtensions = explorationArtifact;
			explorationContract = null;
			explorationContract = exploration;
			explorationContract.Pages = await this.DeserializeExplorationPagesAsync(visualDeserilizeOption);
			explorationContract = null;
			explorationContract = exploration;
			explorationContract.Bookmarks = await this.DeserializeExplorationBookmarksAsync();
			explorationContract = null;
			V2ExplorationUtils.ValidateUniqueExplorationObjectIdentities(exploration);
			return exploration;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003524 File Offset: 0x00001724
		private async Task<ExplorationArtifact> DeserializeRootExplorationArtifactAsync(string fileName, bool isRequired = false)
		{
			ExplorationSerializer.FileData fileData = ExplorationSerializer.GetFileData(this.DeserializationScope.ExplorationFiles, fileName, null, isRequired);
			ExplorationArtifact explorationArtifact;
			if (fileData == null)
			{
				explorationArtifact = null;
			}
			else
			{
				ExplorationArtifact explorationArtifact2 = new ExplorationArtifact();
				explorationArtifact2.FilePath = fileData.FilePath;
				ExplorationArtifact explorationArtifact3 = explorationArtifact2;
				JToken jtoken = await this.DeserializationScope.DeserializeArtifactAsync(fileData);
				explorationArtifact3.Content = jtoken;
				explorationArtifact = explorationArtifact2;
			}
			return explorationArtifact;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003578 File Offset: 0x00001778
		private async Task<Pages> DeserializeExplorationPagesAsync(ExplorationSerializer.VisualDeserilizeOption visualDeserilizeOption)
		{
			IEnumerable<ExplorationSerializer.FileData> pageFiles = ExplorationSerializer.GetRelativeFilesByFolderPath(this.DeserializationScope.ExplorationFiles, ExplorationSerializer.PagesFolderName);
			Pages pages = new Pages();
			if (!pageFiles.Any<ExplorationSerializer.FileData>())
			{
				throw new ExplorationFormatException("Error while reading ReportDefinition. Definition contains no pages.", ExplorationFormatErrorCode.MissingRequiredArtifact, ExplorationErrorSource.User);
			}
			if (visualDeserilizeOption != ExplorationSerializer.VisualDeserilizeOption.MobileOnly)
			{
				Pages pages2 = pages;
				ExplorationArtifact explorationArtifact = await this.DeserializePagesMetadataAsync();
				pages2.PagesMetadata = explorationArtifact;
				pages2 = null;
			}
			IEnumerable<IGrouping<string, ExplorationSerializer.FileData>> enumerable = from page in pageFiles
				where page.FilePath.Contains("/")
				group page by page.FilePath.Split(new char[] { '/' })[0];
			List<Page> pageList = new List<Page>();
			foreach (IGrouping<string, ExplorationSerializer.FileData> grouping in enumerable)
			{
				string pageFolderName = grouping.Key;
				IEnumerable<ExplorationSerializer.FileData> pageFolderFiles = grouping;
				Page page2 = await this.DeserializePageAsync(pageFolderName, pageFolderFiles, visualDeserilizeOption);
				if (page2 != null)
				{
					IEnumerable<ExplorationSerializer.FileData> enumerable2 = pageFolderFiles.Where((ExplorationSerializer.FileData p) => ExplorationSerializer.HasSuffix(p.FilePath, new string[]
					{
						ExplorationSerializer.VisualFileNameInFolder,
						ExplorationSerializer.MobileVisualFileNameInFolder
					}));
					if (enumerable2.Any<ExplorationSerializer.FileData>())
					{
						ExplorationSerializer.ValidateVisualFiles(enumerable2);
						page2.VisualContainers = new NonNulls<ExplorationArtifact>(await this.CollectVisualFilesAsync(pageFolderName, enumerable2, visualDeserilizeOption));
					}
					if (visualDeserilizeOption == ExplorationSerializer.VisualDeserilizeOption.MobileOnly)
					{
						NonNulls<ExplorationArtifact> visualContainers = page2.VisualContainers;
						if (visualContainers != null && visualContainers.Any<ExplorationArtifact>())
						{
							pageList.Add(page2);
						}
					}
					else
					{
						pageList.Add(page2);
					}
					pageFolderName = null;
					pageFolderFiles = null;
					page2 = null;
				}
			}
			IEnumerator<IGrouping<string, ExplorationSerializer.FileData>> enumerator = null;
			pages.PagesList = new NonNulls<Page>(pageList);
			return pages;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000035C4 File Offset: 0x000017C4
		private async Task<Page> DeserializePageAsync(string pageFolderName, IEnumerable<ExplorationSerializer.FileData> pageFolderFiles, ExplorationSerializer.VisualDeserilizeOption visualDeserilizeOption)
		{
			ExplorationSerializer.FileData fileData = ExplorationSerializer.GetFileData(pageFolderFiles, ExplorationSerializer.PageMetadataFileName, pageFolderName, false);
			Page page;
			if (fileData == null)
			{
				page = null;
			}
			else
			{
				Page page2 = new Page();
				page2.FilePath = fileData.FilePath;
				Page page3 = page2;
				JToken jtoken;
				if (visualDeserilizeOption == ExplorationSerializer.VisualDeserilizeOption.MobileOnly)
				{
					jtoken = null;
				}
				else
				{
					jtoken = await this.DeserializationScope.DeserializeArtifactAsync(fileData);
				}
				page3.Content = jtoken;
				Page page4 = page2;
				page3 = null;
				page2 = null;
				page = page4;
			}
			return page;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003620 File Offset: 0x00001820
		private static void ValidateVisualFiles(IEnumerable<ExplorationSerializer.FileData> visualAndMobileStateFiles)
		{
			ExplorationSerializer.FileData fileData = visualAndMobileStateFiles.Where((ExplorationSerializer.FileData file) => ExplorationSerializer.HasSuffix(file.FilePath, new string[] { ExplorationSerializer.MobileVisualFileNameInFolder })).FirstOrDefault(delegate(ExplorationSerializer.FileData mobileFile)
			{
				string text = mobileFile.FilePath.Remove(mobileFile.FilePath.Length - ExplorationSerializer.MobileVisualFileName.Length);
				return !visualAndMobileStateFiles.Select((ExplorationSerializer.FileData file) => file.FilePath).Contains(text + ExplorationSerializer.VisualFileName);
			});
			if (fileData != null)
			{
				string filePath = fileData.FilePath;
				throw new ExplorationFormatException(("Cannot find file '" + filePath + "'.").ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.MissingRequiredArtifact, ExplorationErrorSource.User);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000036A4 File Offset: 0x000018A4
		private async Task<List<ExplorationArtifact>> CollectVisualFilesAsync(string pageFolderName, IEnumerable<ExplorationSerializer.FileData> visualAndMobileStateFiles, ExplorationSerializer.VisualDeserilizeOption visualDeserilizeOption)
		{
			ExplorationSerializer.<>c__DisplayClass47_0 CS$<>8__locals1 = new ExplorationSerializer.<>c__DisplayClass47_0();
			CS$<>8__locals1.visualDeserilizeOption = visualDeserilizeOption;
			CS$<>8__locals1.pageFolderName = pageFolderName;
			CS$<>8__locals1.<>4__this = this;
			IEnumerable<ExplorationSerializer.FileData> visualFiles = visualAndMobileStateFiles.Where(delegate(ExplorationSerializer.FileData visualFile)
			{
				if (CS$<>8__locals1.visualDeserilizeOption == ExplorationSerializer.VisualDeserilizeOption.IncludeAll)
				{
					return true;
				}
				if (CS$<>8__locals1.visualDeserilizeOption == ExplorationSerializer.VisualDeserilizeOption.MainOnly)
				{
					return !ExplorationSerializer.HasSuffix(visualFile.FilePath, new string[] { ExplorationSerializer.MobileVisualFileNameInFolder });
				}
				return CS$<>8__locals1.visualDeserilizeOption != ExplorationSerializer.VisualDeserilizeOption.MobileOnly || ExplorationSerializer.HasSuffix(visualFile.FilePath, new string[] { ExplorationSerializer.MobileVisualFileNameInFolder });
			});
			CS$<>8__locals1.artifacts = new List<ExplorationArtifact>();
			if (this.hostApplication == HostApplication.Desktop)
			{
				JToken[] array = await Task.WhenAll<JToken>(visualFiles.Select(delegate(ExplorationSerializer.FileData file)
				{
					ExplorationSerializer.<>c__DisplayClass47_0.<<CollectVisualFilesAsync>b__2>d <<CollectVisualFilesAsync>b__2>d;
					<<CollectVisualFilesAsync>b__2>d.<>t__builder = AsyncTaskMethodBuilder<JToken>.Create();
					<<CollectVisualFilesAsync>b__2>d.<>4__this = CS$<>8__locals1;
					<<CollectVisualFilesAsync>b__2>d.file = file;
					<<CollectVisualFilesAsync>b__2>d.<>1__state = -1;
					<<CollectVisualFilesAsync>b__2>d.<>t__builder.Start<ExplorationSerializer.<>c__DisplayClass47_0.<<CollectVisualFilesAsync>b__2>d>(ref <<CollectVisualFilesAsync>b__2>d);
					return <<CollectVisualFilesAsync>b__2>d.<>t__builder.Task;
				}));
				int num = 0;
				using (IEnumerator<ExplorationSerializer.FileData> enumerator = visualFiles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ExplorationSerializer.FileData fileData = enumerator.Current;
						CS$<>8__locals1.<CollectVisualFilesAsync>g__RegisterArtifact|1(fileData, array[num++]);
					}
					goto IL_0241;
				}
			}
			foreach (ExplorationSerializer.FileData fileData2 in visualFiles)
			{
				ExplorationSerializer.FileData fileData4;
				ExplorationSerializer.FileData fileData3 = (fileData4 = fileData2);
				CS$<>8__locals1.<CollectVisualFilesAsync>g__RegisterArtifact|1(fileData4, await this.DeserializationScope.DeserializeArtifactAsync(fileData3));
				fileData4 = null;
			}
			IEnumerator<ExplorationSerializer.FileData> enumerator2 = null;
			IL_0241:
			return CS$<>8__locals1.artifacts;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003700 File Offset: 0x00001900
		private async Task<ExplorationArtifact> DeserializePagesMetadataAsync()
		{
			ExplorationSerializer.FileData fileData = ExplorationSerializer.GetFileData(this.DeserializationScope.ExplorationFiles, ExplorationSerializer.PagesConfigFileName, ExplorationSerializer.PagesFolderName, false);
			ExplorationArtifact explorationArtifact3;
			if (fileData != null)
			{
				ExplorationArtifact explorationArtifact = new ExplorationArtifact();
				explorationArtifact.FilePath = fileData.FilePath;
				ExplorationArtifact explorationArtifact2 = explorationArtifact;
				JToken jtoken = await this.DeserializationScope.DeserializeArtifactAsync(fileData);
				explorationArtifact2.Content = jtoken;
				explorationArtifact3 = explorationArtifact;
			}
			else
			{
				explorationArtifact3 = null;
			}
			return explorationArtifact3;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003744 File Offset: 0x00001944
		private async Task<Bookmarks> DeserializeExplorationBookmarksAsync()
		{
			IEnumerable<ExplorationSerializer.FileData> relativeFilesByFolderPath = ExplorationSerializer.GetRelativeFilesByFolderPath(this.DeserializationScope.ExplorationFiles, ExplorationSerializer.BookmarksFolderName);
			Bookmarks bookmarks2;
			if (!relativeFilesByFolderPath.Any<ExplorationSerializer.FileData>())
			{
				bookmarks2 = null;
			}
			else
			{
				IEnumerable<ExplorationSerializer.FileData> enumerable = relativeFilesByFolderPath.Where((ExplorationSerializer.FileData file) => file.FilePath.Equals(ExplorationSerializer.BookmarksConfigFileName));
				IEnumerable<ExplorationSerializer.FileData> bookmarkFiles = relativeFilesByFolderPath.Where((ExplorationSerializer.FileData file) => ExplorationSerializer.HasSuffix(file.FilePath, new string[] { ExplorationSerializer.BookmarkFileSuffix }));
				Bookmarks bookmarks = new Bookmarks();
				if (!enumerable.Any<ExplorationSerializer.FileData>() && bookmarkFiles.Any<ExplorationSerializer.FileData>())
				{
					string text = string.Join("/", new string[]
					{
						ExplorationSerializer.BookmarksFolderName,
						ExplorationSerializer.BookmarksConfigFileName
					});
					throw new ExplorationFormatException(("Cannot find file '" + text + "'.").ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.MissingRequiredArtifact, ExplorationErrorSource.User);
				}
				bookmarks.FilePath = string.Join("/", new string[]
				{
					ExplorationSerializer.BookmarksFolderName,
					enumerable.First<ExplorationSerializer.FileData>().FilePath
				});
				Bookmarks bookmarks3 = bookmarks;
				JToken jtoken = await this.DeserializationScope.DeserializeArtifactAsync(enumerable.First<ExplorationSerializer.FileData>());
				bookmarks3.Content = jtoken;
				bookmarks3 = null;
				List<ExplorationArtifact> bookmarksList = new List<ExplorationArtifact>();
				foreach (ExplorationSerializer.FileData fileData in bookmarkFiles)
				{
					ExplorationArtifact explorationArtifact = new ExplorationArtifact();
					explorationArtifact.FilePath = fileData.FilePath;
					ExplorationArtifact explorationArtifact2 = explorationArtifact;
					jtoken = await this.DeserializationScope.DeserializeArtifactAsync(fileData);
					explorationArtifact2.Content = jtoken;
					ExplorationArtifact explorationArtifact3 = explorationArtifact;
					explorationArtifact2 = null;
					explorationArtifact = null;
					bookmarksList.Add(explorationArtifact3);
				}
				IEnumerator<ExplorationSerializer.FileData> enumerator = null;
				bookmarks.BookmarksList = new NonNulls<ExplorationArtifact>(bookmarksList);
				bookmarks2 = bookmarks;
			}
			return bookmarks2;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003788 File Offset: 0x00001988
		private static void AddArtifactToFiles(ExplorationArtifact artifact, Dictionary<Uri, byte[]> files, string pathPrefix = "")
		{
			if (artifact == null)
			{
				return;
			}
			Uri uri = PBIProjectUtils.MakeRelativeUri(string.IsNullOrEmpty(pathPrefix) ? artifact.FilePath : string.Join("/", new string[] { pathPrefix, artifact.FilePath }));
			byte[] array = ExplorationSerializer.SerializeArtifact(artifact.Content);
			files.Add(uri, array);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000037E0 File Offset: 0x000019E0
		private static bool HasSuffix(string filePath, params string[] suffixes)
		{
			return suffixes.Any((string suffix) => filePath.EndsWith(suffix, StringComparison.Ordinal));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000380C File Offset: 0x00001A0C
		private static byte[] SerializeArtifact(JToken artifact)
		{
			string text = artifact.ToString();
			return PBIProjectConstants.SafeUtf8NoBom.GetBytes(text);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000382C File Offset: 0x00001A2C
		private static ExplorationSerializer.FileData GetFileData(IEnumerable<ExplorationSerializer.FileData> files, string fileName, string folderPath = null, bool isRequired = false)
		{
			string filePath = (string.IsNullOrEmpty(folderPath) ? fileName : string.Join("/", new string[] { folderPath, fileName }));
			ExplorationSerializer.FileData fileData = files.SingleOrDefault((ExplorationSerializer.FileData file) => string.Equals(file.FilePath, filePath));
			if (fileData != null)
			{
				return fileData;
			}
			if (isRequired)
			{
				throw new ExplorationFormatException(("Cannot find file '" + filePath + "'.").ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.MissingRequiredArtifact, ExplorationErrorSource.User);
			}
			return null;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000038AC File Offset: 0x00001AAC
		private async Task<IEnumerable<ExplorationSerializer.FileData>> MapExplorationFilesToFileData<T>(IDictionary<Uri, T> files, Func<T, Task<byte[]>> getContentBytesFunc, bool forceSequential)
		{
			ExplorationSerializer.<>c__DisplayClass54_0<T> CS$<>8__locals1 = new ExplorationSerializer.<>c__DisplayClass54_0<T>();
			CS$<>8__locals1.getContentBytesFunc = getContentBytesFunc;
			CS$<>8__locals1.<>4__this = this;
			IEnumerable<Task<ExplorationSerializer.FileData>> enumerable = files.Where((KeyValuePair<Uri, T> file) => PBIProjectShredder.EnhancedReportFormatMatcher.IsMatch(file.Key.OriginalString)).Select(delegate(KeyValuePair<Uri, T> file)
			{
				ExplorationSerializer.<>c__DisplayClass54_0<T>.<<MapExplorationFilesToFileData>b__2>d <<MapExplorationFilesToFileData>b__2>d;
				<<MapExplorationFilesToFileData>b__2>d.<>t__builder = AsyncTaskMethodBuilder<ExplorationSerializer.FileData>.Create();
				<<MapExplorationFilesToFileData>b__2>d.<>4__this = CS$<>8__locals1;
				<<MapExplorationFilesToFileData>b__2>d.file = file;
				<<MapExplorationFilesToFileData>b__2>d.<>1__state = -1;
				<<MapExplorationFilesToFileData>b__2>d.<>t__builder.Start<ExplorationSerializer.<>c__DisplayClass54_0<T>.<<MapExplorationFilesToFileData>b__2>d>(ref <<MapExplorationFilesToFileData>b__2>d);
				return <<MapExplorationFilesToFileData>b__2>d.<>t__builder.Task;
			});
			IEnumerable<ExplorationSerializer.FileData> enumerable2;
			if (this.hostApplication == HostApplication.Desktop && !forceSequential)
			{
				enumerable2 = await Task.WhenAll<ExplorationSerializer.FileData>(enumerable);
			}
			else
			{
				List<ExplorationSerializer.FileData> explorationFiles = new List<ExplorationSerializer.FileData>();
				foreach (Task<ExplorationSerializer.FileData> task in enumerable)
				{
					List<ExplorationSerializer.FileData> list = explorationFiles;
					list.Add(await task);
					list = null;
				}
				IEnumerator<Task<ExplorationSerializer.FileData>> enumerator = null;
				enumerable2 = explorationFiles;
			}
			return enumerable2;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003908 File Offset: 0x00001B08
		private static void ValidateExplorationFilesSizeLimits(Uri uri, byte[] bytes)
		{
			int num = (ExplorationSerializer.IsBookmarkFilePath(uri.OriginalString) ? ExplorationSerializer.MaxServiceAllowedBookmarkFileSize : ExplorationSerializer.MaxServiceAllowedFileSize);
			if (bytes.Length > num)
			{
				throw new ExplorationFormatException(string.Format("Maximum file size exceeded. The file '{0}' exceeds the maximum file size limit({1:F1}MB).", uri.OriginalString, (float)num / 1024f / 1024f).ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.FileSizeExceeded, ExplorationErrorSource.User);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000396A File Offset: 0x00001B6A
		private static bool IsBookmarkFilePath(string filePath)
		{
			return filePath.StartsWith(ExplorationSerializer.BookmarksFolderName + "/", StringComparison.Ordinal) && ExplorationSerializer.HasSuffix(filePath, new string[] { ExplorationSerializer.BookmarkFileSuffix });
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000399C File Offset: 0x00001B9C
		private static IEnumerable<ExplorationSerializer.FileData> GetRelativeFilesByFolderPath(IEnumerable<ExplorationSerializer.FileData> files, string folderPath)
		{
			List<ExplorationSerializer.FileData> list = new List<ExplorationSerializer.FileData>();
			foreach (ExplorationSerializer.FileData fileData in files)
			{
				if (fileData.FilePath.StartsWith(folderPath, StringComparison.Ordinal) && fileData.FilePath.Length > folderPath.Length && fileData.FilePath[folderPath.Length] == '/')
				{
					string text = fileData.FilePath.Substring(folderPath.Length).TrimStart(new char[] { '/' });
					list.Add(new ExplorationSerializer.FileData(fileData.FilePath, text, fileData.Content));
				}
			}
			return list;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003B05 File Offset: 0x00001D05
		[CompilerGenerated]
		internal static byte[] <GetBytesFromStreamablePackageContentAsync>g__Body|40_0(ref ExplorationSerializer.<>c__DisplayClass40_0 A_0)
		{
			return PowerBIPackagingUtils.GetContentAsBytes(A_0.content, false);
		}

		// Token: 0x0400002D RID: 45
		private static readonly string PagesFolderName = "pages";

		// Token: 0x0400002E RID: 46
		private static readonly string PagesConfigFileName = "pages.json";

		// Token: 0x0400002F RID: 47
		private static readonly string PageMetadataFileName = "page.json";

		// Token: 0x04000030 RID: 48
		private static readonly string BookmarksFolderName = "bookmarks";

		// Token: 0x04000031 RID: 49
		private static readonly string VisualFileName = "visual.json";

		// Token: 0x04000032 RID: 50
		private static readonly string VisualFileNameInFolder = "/visual.json";

		// Token: 0x04000033 RID: 51
		private static readonly string ReportFileName = "report.json";

		// Token: 0x04000034 RID: 52
		private static readonly string VersionFileName = "version.json";

		// Token: 0x04000035 RID: 53
		private static readonly string ReportExtensionsFileName = "reportExtensions.json";

		// Token: 0x04000036 RID: 54
		private static readonly string BookmarksConfigFileName = "bookmarks.json";

		// Token: 0x04000037 RID: 55
		private static readonly string BookmarkFileSuffix = ".bookmark.json";

		// Token: 0x04000038 RID: 56
		private static readonly string ResourcePackagesProperty = "resourcePackages";

		// Token: 0x04000039 RID: 57
		private const string ResPackageItemsProperty = "items";

		// Token: 0x0400003A RID: 58
		private const bool StreamFilesRequired = true;

		// Token: 0x0400003B RID: 59
		private static readonly int MaxServiceAllowedFileSize = int.MaxValue;

		// Token: 0x0400003C RID: 60
		private static readonly int MaxServiceAllowedBookmarkFileSize = int.MaxValue;

		// Token: 0x0400003D RID: 61
		public static readonly string MobileVisualFileName = "mobile.json";

		// Token: 0x0400003E RID: 62
		public static readonly string MobileVisualFileNameInFolder = "/mobile.json";

		// Token: 0x0400003F RID: 63
		private readonly HostApplication hostApplication;

		// Token: 0x04000040 RID: 64
		private readonly IV2ExplorationSchemaKeyResolver schemaKeyResolver;

		// Token: 0x04000041 RID: 65
		private readonly IV2ExplorationSchemas validationSchemas;

		// Token: 0x04000042 RID: 66
		private ExplorationSerializer.PublicEntryDeserializationScope deserializationScopeDontGet;

		// Token: 0x02000099 RID: 153
		private enum VisualDeserilizeOption
		{
			// Token: 0x0400023C RID: 572
			IncludeAll,
			// Token: 0x0400023D RID: 573
			MainOnly,
			// Token: 0x0400023E RID: 574
			MobileOnly
		}

		// Token: 0x0200009A RID: 154
		private class FileData
		{
			// Token: 0x06000440 RID: 1088 RVA: 0x0000AAB6 File Offset: 0x00008CB6
			public FileData(string fullPath, byte[] content)
			{
				this.FullPath = fullPath;
				this.FilePath = fullPath;
				this.Content = content;
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x0000AAD3 File Offset: 0x00008CD3
			public FileData(string fullPath, string filePath, byte[] content)
			{
				this.FullPath = fullPath;
				this.FilePath = filePath;
				this.Content = content;
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000AAF0 File Offset: 0x00008CF0
			// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000AAF8 File Offset: 0x00008CF8
			public string FullPath { get; private set; }

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000AB01 File Offset: 0x00008D01
			// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000AB09 File Offset: 0x00008D09
			public string FilePath { get; private set; }

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000AB12 File Offset: 0x00008D12
			// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000AB1A File Offset: 0x00008D1A
			public byte[] Content { get; private set; }
		}

		// Token: 0x0200009B RID: 155
		private class PublicEntryDeserializationScope
		{
			// Token: 0x06000448 RID: 1096 RVA: 0x0000AB23 File Offset: 0x00008D23
			public PublicEntryDeserializationScope(ExplorationSerializer parent, CancellationToken cancellationToken, IEnumerable<Uri> files)
			{
				this.parent = parent;
				this.cancellationToken = cancellationToken;
				this.files = files;
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000AB62 File Offset: 0x00008D62
			public CancellationToken CancellationToken
			{
				get
				{
					return this.cancellationToken;
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000AB6A File Offset: 0x00008D6A
			// (set) Token: 0x0600044B RID: 1099 RVA: 0x0000AB72 File Offset: 0x00008D72
			public IEnumerable<ExplorationSerializer.FileData> ExplorationFiles { get; set; } = new ExplorationSerializer.FileData[0];

			// Token: 0x0600044C RID: 1100 RVA: 0x0000AB7C File Offset: 0x00008D7C
			public async Task<ExplorationDeserializationResult<T>> RunAsync<T>(Func<Task<T>> asyncAction)
			{
				if (Interlocked.CompareExchange<ExplorationSerializer.PublicEntryDeserializationScope>(ref this.parent.deserializationScopeDontGet, this, null) != null)
				{
					throw new InvalidOperationException("Unsupported parallel deserialization call on the same instance of ExplorationSerializer");
				}
				ExplorationDeserializationResult<T> explorationDeserializationResult;
				try
				{
					if (this.isComplete)
					{
						throw new InvalidOperationException("Unsupported 2nd RunAsync call on the same instance of PublicEntryDeserializationScope");
					}
					T t = await asyncAction();
					T result = t;
					if (this.parent.validationSchemas != null)
					{
						using (IEnumerator<string> enumerator = (from u in this.files
							select u.OriginalString into f
							where !this.deserializedFileDataTokens.ContainsKey(f)
							select f).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string filePath = enumerator.Current;
								ExplorationSerializer.FileData fileData = this.ExplorationFiles.FirstOrDefault((ExplorationSerializer.FileData f) => f.FullPath == filePath);
								if (fileData != null)
								{
									await this.DeserializeArtifactAsync(fileData);
								}
							}
						}
						IEnumerator<string> enumerator = null;
					}
					List<ExplorationDeserializationWarning> list = this.warnings;
					ExplorationDeserializationWarning[] array;
					lock (list)
					{
						array = this.warnings.ToArray();
					}
					explorationDeserializationResult = new ExplorationDeserializationResult<T>(result, array);
				}
				finally
				{
					this.isComplete = true;
					Interlocked.Exchange<ExplorationSerializer.PublicEntryDeserializationScope>(ref this.parent.deserializationScopeDontGet, null);
				}
				return explorationDeserializationResult;
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x0000ABC8 File Offset: 0x00008DC8
			public async Task<JToken> DeserializeArtifactAsync(ExplorationSerializer.FileData fileData)
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				JToken jtoken;
				JToken jtoken2;
				if (this.deserializedFileDataTokens.TryGetValue(fileData.FullPath, out jtoken))
				{
					jtoken2 = jtoken;
				}
				else
				{
					try
					{
						jtoken = await JToken.LoadAsync(new JsonTextReader(new StringReader(PBIProjectConstants.SafeUtf8NoBom.GetString(fileData.Content))), this.cancellationToken);
					}
					catch (JsonReaderException ex)
					{
						throw ExplorationSerializer.PublicEntryDeserializationScope.FormatJsonReaderException(fileData.FilePath, ex);
					}
					bool flag = this.deserializedFileDataTokens.TryAdd(fileData.FullPath, jtoken);
					if (this.parent.validationSchemas != null && flag)
					{
						List<ExplorationDeserializationWarning> list = ExplorationSerializer.Schema.Validate(fileData.FullPath, jtoken, this.parent.schemaKeyResolver, this.parent.validationSchemas);
						List<ExplorationDeserializationWarning> list2 = this.warnings;
						lock (list2)
						{
							this.warnings.AddRange(list);
						}
					}
					jtoken2 = jtoken;
				}
				return jtoken2;
			}

			// Token: 0x0600044E RID: 1102 RVA: 0x0000AC13 File Offset: 0x00008E13
			private static ExplorationFormatException FormatJsonReaderException(string filePath, JsonReaderException ex)
			{
				return new ExplorationFormatException(("There's a problem with the definition content in your Power BI Project.\n\nA formatting issue was found in report definition file '" + filePath + "':\n\n" + ex.Message).ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.MalformedExplorationJson, ExplorationErrorSource.User);
			}

			// Token: 0x04000242 RID: 578
			private readonly ExplorationSerializer parent;

			// Token: 0x04000243 RID: 579
			private readonly CancellationToken cancellationToken;

			// Token: 0x04000244 RID: 580
			private readonly IEnumerable<Uri> files;

			// Token: 0x04000245 RID: 581
			private readonly ConcurrentDictionary<string, JToken> deserializedFileDataTokens = new ConcurrentDictionary<string, JToken>();

			// Token: 0x04000246 RID: 582
			private readonly List<ExplorationDeserializationWarning> warnings = new List<ExplorationDeserializationWarning>();

			// Token: 0x04000247 RID: 583
			private bool isComplete;
		}

		// Token: 0x0200009C RID: 156
		private class Schema
		{
			// Token: 0x06000450 RID: 1104 RVA: 0x0000AC50 File Offset: 0x00008E50
			public static List<ExplorationDeserializationWarning> Validate(string filePath, JToken token, IV2ExplorationSchemaKeyResolver schemaKeyResolver, IV2ExplorationSchemas schemas)
			{
				JToken jtoken = token["$schema"];
				string text = ((jtoken != null) ? jtoken.ToString() : null);
				if (string.IsNullOrEmpty(text))
				{
					throw new ExplorationFormatException(("Can't get '$schema' property in '" + filePath + "'").ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.SchemaValidationFailed, ExplorationErrorSource.User);
				}
				string schemaVersionString = ExplorationSerializer.Schema.GetSchemaVersionString(text);
				if (string.IsNullOrEmpty(schemaVersionString))
				{
					throw new ExplorationFormatException(string.Concat(new string[] { "Can't get schema version from '", text, "' in '", filePath, "'" }).ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.SchemaValidationFailed, ExplorationErrorSource.User);
				}
				Version version;
				if (!Version.TryParse(schemaVersionString, out version))
				{
					throw new ExplorationFormatException(("Invalid schema version in '" + filePath + "'.").ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.SchemaValidationFailed, ExplorationErrorSource.User);
				}
				string schemaKeyForFile = schemaKeyResolver.GetSchemaKeyForFile(filePath);
				JSchema jschema = null;
				try
				{
					jschema = schemas.GetSchema(schemaKeyForFile, version);
				}
				catch (KeyNotFoundException)
				{
				}
				if (jschema == null)
				{
					throw new ExplorationFormatException(string.Format("Can't resolve schema '{0}' in '{1}'.", version, filePath).ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.SchemaValidationFailed, ExplorationErrorSource.User);
				}
				List<ExplorationDeserializationWarning> list;
				try
				{
					list = V2ExplorationValidator.EnsureJTokenIsPerSchema(token, jschema, filePath, new ErrorType[] { 15 });
				}
				catch (PBIProjectJsonSchemaValidationException ex)
				{
					throw new ExplorationFormatException(ex.Message, ExplorationFormatErrorCode.SchemaValidationFailed, ExplorationErrorSource.User);
				}
				return list;
			}

			// Token: 0x06000451 RID: 1105 RVA: 0x0000AD9C File Offset: 0x00008F9C
			private static string GetSchemaVersionString(string schemaPropString)
			{
				if (schemaPropString.EndsWith("/schema.json"))
				{
					int num = schemaPropString.Length - "/schema.json".Length;
					int num2 = schemaPropString.LastIndexOf('/', num - 1);
					if (num2 >= 0 && ++num2 < num)
					{
						return schemaPropString.Substring(num2, num - num2);
					}
				}
				return null;
			}
		}
	}
}
