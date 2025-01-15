using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.Packaging.Host;
using Microsoft.PowerBI.Packaging.Project.Artifacts;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000074 RID: 116
	public static class PBIProjectShredder
	{
		// Token: 0x0600033B RID: 827 RVA: 0x00008FE8 File Offset: 0x000071E8
		public static async Task<PBIProject> OpenProjectAsync(IProjectFilesReader filesReader, string pbipPath, HostContext context)
		{
			PBIProjectShredder.<>c__DisplayClass4_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass4_0();
			CS$<>8__locals1.filesReader = filesReader;
			CS$<>8__locals1.pbipPath = pbipPath;
			PBIProject pbiproject3;
			using (new PBIProjectShredder.ContextScope(context))
			{
				string pbipDirectory = Path.GetDirectoryName(CS$<>8__locals1.pbipPath);
				PBIProject project = new PBIProject();
				ArtifactShortcut artifactShortcut = await PBIProjectShredder.CallAndConvertIntoPathExceptionsAsync<ArtifactShortcut>(CS$<>8__locals1.pbipPath, delegate
				{
					PBIProjectShredder.<>c__DisplayClass4_0.<<OpenProjectAsync>b__0>d <<OpenProjectAsync>b__0>d;
					<<OpenProjectAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactShortcut>.Create();
					<<OpenProjectAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<OpenProjectAsync>b__0>d.<>1__state = -1;
					<<OpenProjectAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass4_0.<<OpenProjectAsync>b__0>d>(ref <<OpenProjectAsync>b__0>d);
					return <<OpenProjectAsync>b__0>d.<>t__builder.Task;
				});
				ArtifactShortcut artifact = artifactShortcut;
				PBIProject pbiproject = project;
				ArtifactShortcut artifactShortcut2 = artifact;
				string text;
				if (artifactShortcut2 == null)
				{
					text = null;
				}
				else
				{
					NonNulls<ArtifactShortcutContainer> artifacts = artifactShortcut2.Artifacts;
					if (artifacts == null)
					{
						text = null;
					}
					else
					{
						ArtifactShortcutContainer artifactShortcutContainer = artifacts.FirstOrDefault<ArtifactShortcutContainer>();
						if (artifactShortcutContainer == null)
						{
							text = null;
						}
						else
						{
							ArtifactShortcutReportReference report = artifactShortcutContainer.Report;
							text = ((report != null) ? report.Path : null);
						}
					}
				}
				pbiproject.ReportPath = text;
				if (string.IsNullOrWhiteSpace(project.ReportPath))
				{
					string text2 = "Expected 'artifacts' array containing 'report' artifact with valid 'path' property.";
					throw new PBIProjectReadException(CS$<>8__locals1.pbipPath, text2, true, PBIProjectException.PBIProjectErrorCode.ReportPathInvalid, null, null);
				}
				if (Path.IsPathRooted(project.ReportPath))
				{
					string text3 = "Report Path must be a relative path.";
					throw new PBIProjectReadException(project.ReportPath, text3, true, PBIProjectException.PBIProjectErrorCode.ReportPathNotRelative, null, null);
				}
				string localPath = new Uri(Path.Combine(pbipDirectory, project.ReportPath)).LocalPath;
				PBIProject pbiproject2 = project;
				pbiproject2.Report = await PBIProjectShredder.OpenReportAndDatasetAsync(CS$<>8__locals1.filesReader, Path.Combine(localPath, "definition.pbir"));
				pbiproject2 = null;
				project.Settings = artifact.Settings;
				project.DollarVeryUniqueSchemaProperty = artifact.DollarVeryUniqueSchemaProperty;
				pbiproject3 = project;
			}
			return pbiproject3;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000903C File Offset: 0x0000723C
		public static async Task<PBIProjectReport> OpenReportAndDatasetAsync(IProjectFilesReader filesReader, string pbirPath, HostContext context)
		{
			PBIProjectReport pbiprojectReport;
			using (new PBIProjectShredder.ContextScope(context))
			{
				pbiprojectReport = await PBIProjectShredder.OpenReportAndDatasetAsync(filesReader, pbirPath);
			}
			return pbiprojectReport;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00009090 File Offset: 0x00007290
		public static async Task<PBIProjectReport> OpenReportAsync(IProjectFilesReader filesReader, string pbirPath, bool loadArtifactDetails, HostContext context)
		{
			PBIProjectReport pbiprojectReport;
			using (new PBIProjectShredder.ContextScope(context))
			{
				ReportDefinition reportDefinition2 = await PBIProjectShredder.OpenReportDefinitionAsync(filesReader, pbirPath);
				ReportDefinition reportDefinition = reportDefinition2;
				PBIProjectShredder.ArtifactDetailsParts artifactDetailsParts = default(PBIProjectShredder.ArtifactDetailsParts);
				if (loadArtifactDetails)
				{
					artifactDetailsParts = await PBIProjectShredder.OpenReportArtifactDetailsPartsAsync(filesReader, pbirPath);
				}
				pbiprojectReport = await PBIProjectShredder.OpenReportAsync(filesReader, reportDefinition, artifactDetailsParts);
			}
			return pbiprojectReport;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000090EC File Offset: 0x000072EC
		public static async Task<PBIProjectDataset> OpenDatasetAsync(IProjectFilesReader filesReader, string datasetDirectory, bool loadArtifactDetails, HostContext context)
		{
			PBIProjectDataset pbiprojectDataset;
			using (new PBIProjectShredder.ContextScope(context))
			{
				PBIProjectShredder.ArtifactDetailsParts artifactDetailsParts = default(PBIProjectShredder.ArtifactDetailsParts);
				if (loadArtifactDetails)
				{
					artifactDetailsParts = await PBIProjectShredder.OpenDatasetArtifactDetailsPartsAsync(filesReader, datasetDirectory);
				}
				pbiprojectDataset = await PBIProjectShredder.OpenDatasetAsync(filesReader, datasetDirectory, artifactDetailsParts);
			}
			return pbiprojectDataset;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00009147 File Offset: 0x00007347
		private static IFeatureSwitches FeatureSwitches
		{
			get
			{
				HostContext value = PBIProjectShredder.AsyncLocalContext.Value;
				IFeatureSwitches featureSwitches = ((value != null) ? value.FeatureSwitches : null);
				if (featureSwitches == null)
				{
					throw new InvalidOperationException("ContextScope has not been set up for the current async context.");
				}
				return featureSwitches;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00009170 File Offset: 0x00007370
		private static async Task<T> MakeWithPath<T>(string filePath, Func<Task<T>> func) where T : IFromPBIProjectFile
		{
			T t = await PBIProjectShredder.CallAndConvertIntoPathExceptionsAsync<T>(filePath, func);
			if (t != null)
			{
				t.FileName = filePath;
			}
			return t;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000091BC File Offset: 0x000073BC
		private static async Task<T> CallAndConvertIntoPathExceptionsAsync<T>(string filePath, Func<Task<T>> func)
		{
			T t;
			try
			{
				t = await func();
			}
			catch (Exception ex) when (ex is PBIProjectException || ex is JsonException || ex is ProjectFilesReaderException)
			{
				if (ex is PBIProjectReadException)
				{
					throw;
				}
				PBIProjectException ex2 = ex as PBIProjectException;
				PBIProjectException.PBIProjectErrorCode? pbiprojectErrorCode = ((ex2 != null) ? new PBIProjectException.PBIProjectErrorCode?(ex2.ErrorCode) : null);
				PBIProjectException ex3 = ex as PBIProjectException;
				string text = ((ex3 != null) ? ex3.LearnMoreLinkUrl : null);
				bool flag = true;
				ProjectFilesReaderFileException ex4 = ex as ProjectFilesReaderFileException;
				if (ex4 != null)
				{
					pbiprojectErrorCode = ((ex4.InnerException is PathTooLongException) ? new PBIProjectException.PBIProjectErrorCode?(PBIProjectException.PBIProjectErrorCode.FilePathTooLongError) : pbiprojectErrorCode);
					flag = ex4.FileName != filePath;
				}
				throw new PBIProjectReadException(filePath, ex.Message, flag, pbiprojectErrorCode.GetValueOrDefault(PBIProjectException.PBIProjectErrorCode.ProjectShredderError), ex, text);
			}
			return t;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00009208 File Offset: 0x00007408
		private static async Task<PBIProjectReport> OpenReportAndDatasetAsync(IProjectFilesReader filesReader, string pbirPath)
		{
			ReportDefinition reportDefinition2 = await PBIProjectShredder.OpenReportDefinitionAsync(filesReader, pbirPath);
			ReportDefinition reportDefinition = reportDefinition2;
			IProjectFilesReader projectFilesReader = filesReader;
			ReportDefinition reportDefinition3 = reportDefinition;
			PBIProjectReport pbiprojectReport = await PBIProjectShredder.OpenReportAsync(projectFilesReader, reportDefinition3, await PBIProjectShredder.OpenReportArtifactDetailsPartsAsync(filesReader, pbirPath));
			projectFilesReader = null;
			reportDefinition3 = null;
			PBIProjectReport report = pbiprojectReport;
			string datasetDirectory = PBIProjectShredder.GetDatasetDirFromReportDefinition(reportDefinition);
			if (datasetDirectory != null)
			{
				PBIProjectShredder.ArtifactDetailsParts artifactDetailsParts = await PBIProjectShredder.OpenDatasetArtifactDetailsPartsByPbirAsync(filesReader, pbirPath, reportDefinition);
				PBIProjectReport pbiprojectReport2 = report;
				pbiprojectReport2.Dataset = await PBIProjectShredder.OpenDatasetAsync(filesReader, datasetDirectory, artifactDetailsParts);
				pbiprojectReport2 = null;
			}
			else
			{
				report.Dataset = new PBIProjectDataset();
			}
			return report;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00009254 File Offset: 0x00007454
		private static async Task<PBIProjectShredder.ArtifactDetailsParts> OpenReportArtifactDetailsPartsAsync(IProjectFilesReader filesReader, string pbirPath)
		{
			PBIProjectShredder.<>c__DisplayClass15_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass15_0();
			CS$<>8__locals1.filesReader = filesReader;
			CS$<>8__locals1.pbirPath = pbirPath;
			return await PBIProjectShredder.OpenArtifactDetailsPartsAsync(delegate(bool probeFileOnly)
			{
				PBIProjectShredder.<>c__DisplayClass15_0.<<OpenReportArtifactDetailsPartsAsync>b__0>d <<OpenReportArtifactDetailsPartsAsync>b__0>d;
				<<OpenReportArtifactDetailsPartsAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactConfig>.Create();
				<<OpenReportArtifactDetailsPartsAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<OpenReportArtifactDetailsPartsAsync>b__0>d.probeFileOnly = probeFileOnly;
				<<OpenReportArtifactDetailsPartsAsync>b__0>d.<>1__state = -1;
				<<OpenReportArtifactDetailsPartsAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass15_0.<<OpenReportArtifactDetailsPartsAsync>b__0>d>(ref <<OpenReportArtifactDetailsPartsAsync>b__0>d);
				return <<OpenReportArtifactDetailsPartsAsync>b__0>d.<>t__builder.Task;
			}, delegate(bool probeFileOnly)
			{
				PBIProjectShredder.<>c__DisplayClass15_0.<<OpenReportArtifactDetailsPartsAsync>b__1>d <<OpenReportArtifactDetailsPartsAsync>b__1>d;
				<<OpenReportArtifactDetailsPartsAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactMetadata>.Create();
				<<OpenReportArtifactDetailsPartsAsync>b__1>d.<>4__this = CS$<>8__locals1;
				<<OpenReportArtifactDetailsPartsAsync>b__1>d.probeFileOnly = probeFileOnly;
				<<OpenReportArtifactDetailsPartsAsync>b__1>d.<>1__state = -1;
				<<OpenReportArtifactDetailsPartsAsync>b__1>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass15_0.<<OpenReportArtifactDetailsPartsAsync>b__1>d>(ref <<OpenReportArtifactDetailsPartsAsync>b__1>d);
				return <<OpenReportArtifactDetailsPartsAsync>b__1>d.<>t__builder.Task;
			}, delegate(bool probeFileOnly)
			{
				PBIProjectShredder.<>c__DisplayClass15_0.<<OpenReportArtifactDetailsPartsAsync>b__2>d <<OpenReportArtifactDetailsPartsAsync>b__2>d;
				<<OpenReportArtifactDetailsPartsAsync>b__2>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactDetails>.Create();
				<<OpenReportArtifactDetailsPartsAsync>b__2>d.<>4__this = CS$<>8__locals1;
				<<OpenReportArtifactDetailsPartsAsync>b__2>d.probeFileOnly = probeFileOnly;
				<<OpenReportArtifactDetailsPartsAsync>b__2>d.<>1__state = -1;
				<<OpenReportArtifactDetailsPartsAsync>b__2>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass15_0.<<OpenReportArtifactDetailsPartsAsync>b__2>d>(ref <<OpenReportArtifactDetailsPartsAsync>b__2>d);
				return <<OpenReportArtifactDetailsPartsAsync>b__2>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000092A0 File Offset: 0x000074A0
		private static async Task<T> ProbePathAsync<T>(IProjectFilesReader filesReader, string path) where T : IFromPBIProjectFile, new()
		{
			TaskAwaiter<bool> taskAwaiter = filesReader.ExistsAsync(path).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<bool> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			T t2;
			if (taskAwaiter.GetResult())
			{
				T t = new T();
				t.FileName = path;
				t2 = t;
			}
			else
			{
				t2 = default(T);
			}
			return t2;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000092EC File Offset: 0x000074EC
		private static async Task<ArtifactConfig> OpenReportArtifactConfigAsync(IProjectFilesReader filesReader, string pbirPath, bool probeFileOnly)
		{
			PBIProjectShredder.<>c__DisplayClass17_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass17_0();
			CS$<>8__locals1.filesReader = filesReader;
			string directoryName = Path.GetDirectoryName(pbirPath);
			CS$<>8__locals1.configPath = Path.Combine(directoryName, "item.config.json");
			ArtifactConfig artifactConfig = await PBIProjectShredder.ProbePathAsync<ArtifactConfig>(CS$<>8__locals1.filesReader, CS$<>8__locals1.configPath);
			ArtifactConfig artifactConfig2;
			if (artifactConfig == null || probeFileOnly)
			{
				artifactConfig2 = artifactConfig;
			}
			else
			{
				artifactConfig = await PBIProjectShredder.MakeWithPath<ArtifactConfig>(CS$<>8__locals1.configPath, delegate
				{
					PBIProjectShredder.<>c__DisplayClass17_0.<<OpenReportArtifactConfigAsync>b__0>d <<OpenReportArtifactConfigAsync>b__0>d;
					<<OpenReportArtifactConfigAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactConfig>.Create();
					<<OpenReportArtifactConfigAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<OpenReportArtifactConfigAsync>b__0>d.<>1__state = -1;
					<<OpenReportArtifactConfigAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass17_0.<<OpenReportArtifactConfigAsync>b__0>d>(ref <<OpenReportArtifactConfigAsync>b__0>d);
					return <<OpenReportArtifactConfigAsync>b__0>d.<>t__builder.Task;
				});
				artifactConfig2 = artifactConfig;
			}
			return artifactConfig2;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00009340 File Offset: 0x00007540
		private static async Task<ArtifactMetadata> OpenReportArtifactMetadataAsync(IProjectFilesReader filesReader, string pbirPath, bool probeFileOnly)
		{
			PBIProjectShredder.<>c__DisplayClass18_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass18_0();
			CS$<>8__locals1.filesReader = filesReader;
			string directoryName = Path.GetDirectoryName(pbirPath);
			CS$<>8__locals1.metadataPath = Path.Combine(directoryName, "item.metadata.json");
			ArtifactMetadata artifactMetadata = await PBIProjectShredder.ProbePathAsync<ArtifactMetadata>(CS$<>8__locals1.filesReader, CS$<>8__locals1.metadataPath);
			ArtifactMetadata artifactMetadata2;
			if (artifactMetadata == null || probeFileOnly)
			{
				artifactMetadata2 = artifactMetadata;
			}
			else
			{
				artifactMetadata = await PBIProjectShredder.MakeWithPath<ArtifactMetadata>(CS$<>8__locals1.metadataPath, delegate
				{
					PBIProjectShredder.<>c__DisplayClass18_0.<<OpenReportArtifactMetadataAsync>b__0>d <<OpenReportArtifactMetadataAsync>b__0>d;
					<<OpenReportArtifactMetadataAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactMetadata>.Create();
					<<OpenReportArtifactMetadataAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<OpenReportArtifactMetadataAsync>b__0>d.<>1__state = -1;
					<<OpenReportArtifactMetadataAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass18_0.<<OpenReportArtifactMetadataAsync>b__0>d>(ref <<OpenReportArtifactMetadataAsync>b__0>d);
					return <<OpenReportArtifactMetadataAsync>b__0>d.<>t__builder.Task;
				});
				artifactMetadata2 = artifactMetadata;
			}
			return artifactMetadata2;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00009394 File Offset: 0x00007594
		private static async Task<ArtifactDetails> OpenReportArtifactDetailsAsync(IProjectFilesReader filesReader, string pbirPath, bool probeFileOnly)
		{
			PBIProjectShredder.<>c__DisplayClass19_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass19_0();
			CS$<>8__locals1.filesReader = filesReader;
			string directoryName = Path.GetDirectoryName(pbirPath);
			CS$<>8__locals1.detailsPath = Path.Combine(directoryName, ".platform");
			ArtifactDetails artifactDetails = await PBIProjectShredder.ProbePathAsync<ArtifactDetails>(CS$<>8__locals1.filesReader, CS$<>8__locals1.detailsPath);
			ArtifactDetails artifactDetails2;
			if (artifactDetails == null || probeFileOnly)
			{
				artifactDetails2 = artifactDetails;
			}
			else
			{
				artifactDetails = await PBIProjectShredder.MakeWithPath<ArtifactDetails>(CS$<>8__locals1.detailsPath, delegate
				{
					PBIProjectShredder.<>c__DisplayClass19_0.<<OpenReportArtifactDetailsAsync>b__0>d <<OpenReportArtifactDetailsAsync>b__0>d;
					<<OpenReportArtifactDetailsAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactDetails>.Create();
					<<OpenReportArtifactDetailsAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<OpenReportArtifactDetailsAsync>b__0>d.<>1__state = -1;
					<<OpenReportArtifactDetailsAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass19_0.<<OpenReportArtifactDetailsAsync>b__0>d>(ref <<OpenReportArtifactDetailsAsync>b__0>d);
					return <<OpenReportArtifactDetailsAsync>b__0>d.<>t__builder.Task;
				});
				artifactDetails2 = artifactDetails;
			}
			return artifactDetails2;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000093E8 File Offset: 0x000075E8
		private static async Task<PBIProjectShredder.ArtifactDetailsParts> OpenDatasetArtifactDetailsPartsAsync(IProjectFilesReader filesReader, string datasetDirectory)
		{
			PBIProjectShredder.<>c__DisplayClass20_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass20_0();
			CS$<>8__locals1.filesReader = filesReader;
			CS$<>8__locals1.configPath = Path.Combine(datasetDirectory, "item.config.json");
			CS$<>8__locals1.metadataPath = Path.Combine(datasetDirectory, "item.metadata.json");
			CS$<>8__locals1.detailsPath = Path.Combine(datasetDirectory, ".platform");
			return await PBIProjectShredder.OpenArtifactDetailsPartsAsync(delegate(bool probeFileOnly)
			{
				PBIProjectShredder.<>c__DisplayClass20_0.<<OpenDatasetArtifactDetailsPartsAsync>b__0>d <<OpenDatasetArtifactDetailsPartsAsync>b__0>d;
				<<OpenDatasetArtifactDetailsPartsAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactConfig>.Create();
				<<OpenDatasetArtifactDetailsPartsAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<OpenDatasetArtifactDetailsPartsAsync>b__0>d.probeFileOnly = probeFileOnly;
				<<OpenDatasetArtifactDetailsPartsAsync>b__0>d.<>1__state = -1;
				<<OpenDatasetArtifactDetailsPartsAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass20_0.<<OpenDatasetArtifactDetailsPartsAsync>b__0>d>(ref <<OpenDatasetArtifactDetailsPartsAsync>b__0>d);
				return <<OpenDatasetArtifactDetailsPartsAsync>b__0>d.<>t__builder.Task;
			}, delegate(bool probeFileOnly)
			{
				PBIProjectShredder.<>c__DisplayClass20_0.<<OpenDatasetArtifactDetailsPartsAsync>b__1>d <<OpenDatasetArtifactDetailsPartsAsync>b__1>d;
				<<OpenDatasetArtifactDetailsPartsAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactMetadata>.Create();
				<<OpenDatasetArtifactDetailsPartsAsync>b__1>d.<>4__this = CS$<>8__locals1;
				<<OpenDatasetArtifactDetailsPartsAsync>b__1>d.probeFileOnly = probeFileOnly;
				<<OpenDatasetArtifactDetailsPartsAsync>b__1>d.<>1__state = -1;
				<<OpenDatasetArtifactDetailsPartsAsync>b__1>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass20_0.<<OpenDatasetArtifactDetailsPartsAsync>b__1>d>(ref <<OpenDatasetArtifactDetailsPartsAsync>b__1>d);
				return <<OpenDatasetArtifactDetailsPartsAsync>b__1>d.<>t__builder.Task;
			}, delegate(bool probeFileOnly)
			{
				PBIProjectShredder.<>c__DisplayClass20_0.<<OpenDatasetArtifactDetailsPartsAsync>b__2>d <<OpenDatasetArtifactDetailsPartsAsync>b__2>d;
				<<OpenDatasetArtifactDetailsPartsAsync>b__2>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactDetails>.Create();
				<<OpenDatasetArtifactDetailsPartsAsync>b__2>d.<>4__this = CS$<>8__locals1;
				<<OpenDatasetArtifactDetailsPartsAsync>b__2>d.probeFileOnly = probeFileOnly;
				<<OpenDatasetArtifactDetailsPartsAsync>b__2>d.<>1__state = -1;
				<<OpenDatasetArtifactDetailsPartsAsync>b__2>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass20_0.<<OpenDatasetArtifactDetailsPartsAsync>b__2>d>(ref <<OpenDatasetArtifactDetailsPartsAsync>b__2>d);
				return <<OpenDatasetArtifactDetailsPartsAsync>b__2>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00009434 File Offset: 0x00007634
		private static Task<PBIProjectShredder.ArtifactDetailsParts> OpenArtifactDetailsPartsAsync(Func<bool, Task<ArtifactConfig>> getConfig, Func<bool, Task<ArtifactMetadata>> getMetadata, Func<bool, Task<ArtifactDetails>> getDetails)
		{
			PBIProjectShredder.<OpenArtifactDetailsPartsAsync>d__21 <OpenArtifactDetailsPartsAsync>d__;
			<OpenArtifactDetailsPartsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<PBIProjectShredder.ArtifactDetailsParts>.Create();
			<OpenArtifactDetailsPartsAsync>d__.getConfig = getConfig;
			<OpenArtifactDetailsPartsAsync>d__.getMetadata = getMetadata;
			<OpenArtifactDetailsPartsAsync>d__.getDetails = getDetails;
			<OpenArtifactDetailsPartsAsync>d__.<>1__state = -1;
			<OpenArtifactDetailsPartsAsync>d__.<>t__builder.Start<PBIProjectShredder.<OpenArtifactDetailsPartsAsync>d__21>(ref <OpenArtifactDetailsPartsAsync>d__);
			return <OpenArtifactDetailsPartsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00009488 File Offset: 0x00007688
		private static async Task<PBIProjectShredder.ArtifactDetailsParts> OpenDatasetArtifactDetailsPartsByPbirAsync(IProjectFilesReader filesReader, string pbirPath, ReportDefinition reportDefinition = null)
		{
			PBIProjectShredder.<>c__DisplayClass22_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass22_0();
			CS$<>8__locals1.filesReader = filesReader;
			CS$<>8__locals1.pbirPath = pbirPath;
			CS$<>8__locals1.reportDefinition = reportDefinition;
			return await PBIProjectShredder.OpenArtifactDetailsPartsAsync(delegate(bool probeFileOnly)
			{
				PBIProjectShredder.<>c__DisplayClass22_0.<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__0>d <<OpenDatasetArtifactDetailsPartsByPbirAsync>b__0>d;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactConfig>.Create();
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__0>d.probeFileOnly = probeFileOnly;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__0>d.<>1__state = -1;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass22_0.<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__0>d>(ref <<OpenDatasetArtifactDetailsPartsByPbirAsync>b__0>d);
				return <<OpenDatasetArtifactDetailsPartsByPbirAsync>b__0>d.<>t__builder.Task;
			}, delegate(bool probeFileOnly)
			{
				PBIProjectShredder.<>c__DisplayClass22_0.<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__1>d <<OpenDatasetArtifactDetailsPartsByPbirAsync>b__1>d;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactMetadata>.Create();
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__1>d.<>4__this = CS$<>8__locals1;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__1>d.probeFileOnly = probeFileOnly;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__1>d.<>1__state = -1;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__1>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass22_0.<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__1>d>(ref <<OpenDatasetArtifactDetailsPartsByPbirAsync>b__1>d);
				return <<OpenDatasetArtifactDetailsPartsByPbirAsync>b__1>d.<>t__builder.Task;
			}, delegate(bool probeFileOnly)
			{
				PBIProjectShredder.<>c__DisplayClass22_0.<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__2>d <<OpenDatasetArtifactDetailsPartsByPbirAsync>b__2>d;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__2>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactDetails>.Create();
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__2>d.<>4__this = CS$<>8__locals1;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__2>d.probeFileOnly = probeFileOnly;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__2>d.<>1__state = -1;
				<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__2>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass22_0.<<OpenDatasetArtifactDetailsPartsByPbirAsync>b__2>d>(ref <<OpenDatasetArtifactDetailsPartsByPbirAsync>b__2>d);
				return <<OpenDatasetArtifactDetailsPartsByPbirAsync>b__2>d.<>t__builder.Task;
			});
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000094DC File Offset: 0x000076DC
		private static async Task<ArtifactConfig> OpenDatasetArtifactConfigByPbirAsync(IProjectFilesReader filesReader, string pbirPath, bool probeFileOnly, ReportDefinition reportDefinition = null)
		{
			ReportDefinition reportDefinition2;
			if (reportDefinition != null)
			{
				reportDefinition2 = reportDefinition;
			}
			else
			{
				reportDefinition2 = await PBIProjectShredder.OpenReportDefinitionAsync(filesReader, pbirPath);
			}
			string datasetDirFromReportDefinition = PBIProjectShredder.GetDatasetDirFromReportDefinition(reportDefinition2);
			ArtifactConfig artifactConfig;
			if (datasetDirFromReportDefinition == null)
			{
				artifactConfig = null;
			}
			else
			{
				string text = Path.Combine(datasetDirFromReportDefinition, "item.config.json");
				artifactConfig = await PBIProjectShredder.OpenDatasetArtifactConfigDirectlyAsync(filesReader, text, probeFileOnly);
			}
			return artifactConfig;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00009538 File Offset: 0x00007738
		private static async Task<ArtifactConfig> OpenDatasetArtifactConfigDirectlyAsync(IProjectFilesReader filesReader, string configPath, bool probeFileOnly)
		{
			PBIProjectShredder.<>c__DisplayClass24_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass24_0();
			CS$<>8__locals1.filesReader = filesReader;
			CS$<>8__locals1.configPath = configPath;
			ArtifactConfig artifactConfig = await PBIProjectShredder.ProbePathAsync<ArtifactConfig>(CS$<>8__locals1.filesReader, CS$<>8__locals1.configPath);
			ArtifactConfig artifactConfig2;
			if (artifactConfig == null || probeFileOnly)
			{
				artifactConfig2 = artifactConfig;
			}
			else
			{
				artifactConfig = await PBIProjectShredder.MakeWithPath<ArtifactConfig>(CS$<>8__locals1.configPath, delegate
				{
					PBIProjectShredder.<>c__DisplayClass24_0.<<OpenDatasetArtifactConfigDirectlyAsync>b__0>d <<OpenDatasetArtifactConfigDirectlyAsync>b__0>d;
					<<OpenDatasetArtifactConfigDirectlyAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactConfig>.Create();
					<<OpenDatasetArtifactConfigDirectlyAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<OpenDatasetArtifactConfigDirectlyAsync>b__0>d.<>1__state = -1;
					<<OpenDatasetArtifactConfigDirectlyAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass24_0.<<OpenDatasetArtifactConfigDirectlyAsync>b__0>d>(ref <<OpenDatasetArtifactConfigDirectlyAsync>b__0>d);
					return <<OpenDatasetArtifactConfigDirectlyAsync>b__0>d.<>t__builder.Task;
				});
				artifactConfig2 = artifactConfig;
			}
			return artifactConfig2;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000958C File Offset: 0x0000778C
		private static async Task<ArtifactMetadata> OpenDatasetArtifactMetadataByPbirAsync(IProjectFilesReader filesReader, string pbirPath, bool probeFileOnly, ReportDefinition reportDefinition = null)
		{
			ReportDefinition reportDefinition2;
			if (reportDefinition != null)
			{
				reportDefinition2 = reportDefinition;
			}
			else
			{
				reportDefinition2 = await PBIProjectShredder.OpenReportDefinitionAsync(filesReader, pbirPath);
			}
			string datasetDirFromReportDefinition = PBIProjectShredder.GetDatasetDirFromReportDefinition(reportDefinition2);
			ArtifactMetadata artifactMetadata;
			if (datasetDirFromReportDefinition == null)
			{
				artifactMetadata = null;
			}
			else
			{
				string text = Path.Combine(datasetDirFromReportDefinition, "item.metadata.json");
				artifactMetadata = await PBIProjectShredder.OpenDatasetArtifactMetadataDirectlyAsync(filesReader, text, probeFileOnly);
			}
			return artifactMetadata;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000095E8 File Offset: 0x000077E8
		private static async Task<ArtifactDetails> OpenDatasetArtifactDetailsByPbirAsync(IProjectFilesReader filesReader, string pbirPath, bool probeFileOnly, ReportDefinition reportDefinition = null)
		{
			ReportDefinition reportDefinition2;
			if (reportDefinition != null)
			{
				reportDefinition2 = reportDefinition;
			}
			else
			{
				reportDefinition2 = await PBIProjectShredder.OpenReportDefinitionAsync(filesReader, pbirPath);
			}
			string datasetDirFromReportDefinition = PBIProjectShredder.GetDatasetDirFromReportDefinition(reportDefinition2);
			ArtifactDetails artifactDetails;
			if (datasetDirFromReportDefinition == null)
			{
				artifactDetails = null;
			}
			else
			{
				string text = Path.Combine(datasetDirFromReportDefinition, ".platform");
				artifactDetails = await PBIProjectShredder.OpenDatasetArtifactDetailsDirectlyAsync(filesReader, text, probeFileOnly);
			}
			return artifactDetails;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00009644 File Offset: 0x00007844
		private static async Task<ArtifactMetadata> OpenDatasetArtifactMetadataDirectlyAsync(IProjectFilesReader filesReader, string artifactPath, bool probeFileOnly)
		{
			PBIProjectShredder.<>c__DisplayClass27_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass27_0();
			CS$<>8__locals1.filesReader = filesReader;
			CS$<>8__locals1.artifactPath = artifactPath;
			ArtifactMetadata artifactMetadata = await PBIProjectShredder.ProbePathAsync<ArtifactMetadata>(CS$<>8__locals1.filesReader, CS$<>8__locals1.artifactPath);
			ArtifactMetadata artifactMetadata2;
			if (artifactMetadata == null || probeFileOnly)
			{
				artifactMetadata2 = artifactMetadata;
			}
			else
			{
				artifactMetadata = await PBIProjectShredder.MakeWithPath<ArtifactMetadata>(CS$<>8__locals1.artifactPath, delegate
				{
					PBIProjectShredder.<>c__DisplayClass27_0.<<OpenDatasetArtifactMetadataDirectlyAsync>b__0>d <<OpenDatasetArtifactMetadataDirectlyAsync>b__0>d;
					<<OpenDatasetArtifactMetadataDirectlyAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactMetadata>.Create();
					<<OpenDatasetArtifactMetadataDirectlyAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<OpenDatasetArtifactMetadataDirectlyAsync>b__0>d.<>1__state = -1;
					<<OpenDatasetArtifactMetadataDirectlyAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass27_0.<<OpenDatasetArtifactMetadataDirectlyAsync>b__0>d>(ref <<OpenDatasetArtifactMetadataDirectlyAsync>b__0>d);
					return <<OpenDatasetArtifactMetadataDirectlyAsync>b__0>d.<>t__builder.Task;
				});
				artifactMetadata2 = artifactMetadata;
			}
			return artifactMetadata2;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00009698 File Offset: 0x00007898
		private static async Task<ArtifactDetails> OpenDatasetArtifactDetailsDirectlyAsync(IProjectFilesReader filesReader, string artifactPath, bool probeFileOnly)
		{
			PBIProjectShredder.<>c__DisplayClass28_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass28_0();
			CS$<>8__locals1.filesReader = filesReader;
			CS$<>8__locals1.artifactPath = artifactPath;
			ArtifactDetails artifactDetails = await PBIProjectShredder.ProbePathAsync<ArtifactDetails>(CS$<>8__locals1.filesReader, CS$<>8__locals1.artifactPath);
			ArtifactDetails artifactDetails2;
			if (artifactDetails == null || probeFileOnly)
			{
				artifactDetails2 = artifactDetails;
			}
			else
			{
				artifactDetails = await PBIProjectShredder.MakeWithPath<ArtifactDetails>(CS$<>8__locals1.artifactPath, delegate
				{
					PBIProjectShredder.<>c__DisplayClass28_0.<<OpenDatasetArtifactDetailsDirectlyAsync>b__0>d <<OpenDatasetArtifactDetailsDirectlyAsync>b__0>d;
					<<OpenDatasetArtifactDetailsDirectlyAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ArtifactDetails>.Create();
					<<OpenDatasetArtifactDetailsDirectlyAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<OpenDatasetArtifactDetailsDirectlyAsync>b__0>d.<>1__state = -1;
					<<OpenDatasetArtifactDetailsDirectlyAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass28_0.<<OpenDatasetArtifactDetailsDirectlyAsync>b__0>d>(ref <<OpenDatasetArtifactDetailsDirectlyAsync>b__0>d);
					return <<OpenDatasetArtifactDetailsDirectlyAsync>b__0>d.<>t__builder.Task;
				});
				artifactDetails2 = artifactDetails;
			}
			return artifactDetails2;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000096EC File Offset: 0x000078EC
		private static async Task<ReportDefinition> OpenReportDefinitionAsync(IProjectFilesReader filesReader, string pbirPath)
		{
			PBIProjectShredder.<>c__DisplayClass29_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass29_0();
			CS$<>8__locals1.filesReader = filesReader;
			CS$<>8__locals1.pbirPath = pbirPath;
			return await PBIProjectShredder.MakeWithPath<ReportDefinition>(CS$<>8__locals1.pbirPath, delegate
			{
				PBIProjectShredder.<>c__DisplayClass29_0.<<OpenReportDefinitionAsync>b__0>d <<OpenReportDefinitionAsync>b__0>d;
				<<OpenReportDefinitionAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ReportDefinition>.Create();
				<<OpenReportDefinitionAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<OpenReportDefinitionAsync>b__0>d.<>1__state = -1;
				<<OpenReportDefinitionAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass29_0.<<OpenReportDefinitionAsync>b__0>d>(ref <<OpenReportDefinitionAsync>b__0>d);
				return <<OpenReportDefinitionAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009738 File Offset: 0x00007938
		private static async Task<PBIProjectReport> OpenReportAsync(IProjectFilesReader filesReader, ReportDefinition reportDefinition, PBIProjectShredder.ArtifactDetailsParts detailsParts)
		{
			PBIProjectShredder.<>c__DisplayClass30_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass30_0();
			CS$<>8__locals1.filesReader = filesReader;
			string reportDirectory = Path.GetDirectoryName(reportDefinition.FileName);
			CS$<>8__locals1.report = new PBIProjectReport();
			CS$<>8__locals1.report.ArtifactConfig = detailsParts.Config;
			CS$<>8__locals1.report.ArtifactMetadata = detailsParts.Metadata;
			CS$<>8__locals1.report.ArtifactDetails = detailsParts.Details;
			CS$<>8__locals1.report.ReportDefinition = reportDefinition;
			CS$<>8__locals1.v1ReportPath = Path.Combine(reportDirectory, "report.json");
			CS$<>8__locals1.v1MobileStatePath = Path.Combine(reportDirectory, "mobileState.json");
			Func<Task> loadReportV = delegate
			{
				PBIProjectShredder.<>c__DisplayClass30_0.<<OpenReportAsync>b__0>d <<OpenReportAsync>b__0>d;
				<<OpenReportAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<OpenReportAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<OpenReportAsync>b__0>d.<>1__state = -1;
				<<OpenReportAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass30_0.<<OpenReportAsync>b__0>d>(ref <<OpenReportAsync>b__0>d);
				return <<OpenReportAsync>b__0>d.<>t__builder.Task;
			};
			TaskAwaiter<bool> taskAwaiter2;
			if (new Version(reportDefinition.Version) < ReportDefinition.FirstEnhancedReportVersion)
			{
				await loadReportV();
			}
			else
			{
				string enhancedReportFolderPath = Path.Combine(reportDirectory, "definition");
				IDictionary<Uri, IStreamablePowerBIProjectPartContent> explorationContent = await PBIProjectShredder.ReadAllRelativeContentAsync(CS$<>8__locals1.filesReader, enhancedReportFolderPath, PBIProjectShredder.EnhancedReportFormatMatcher);
				if (explorationContent == null)
				{
					await loadReportV();
				}
				else
				{
					if (!PBIProjectShredder.FeatureSwitches.EnhancedReportFormat)
					{
						throw new PBIProjectReadException(enhancedReportFolderPath, "Report is using the PBIR format. Please enable the preview feature 'Store reports using enhanced metadata format' before opening it again.".ToString(CultureInfo.CurrentCulture), false, PBIProjectException.PBIProjectErrorCode.FeatureNotEnabled, null, "https://go.microsoft.com/fwlink/?linkid=2249793");
					}
					TaskAwaiter<bool> taskAwaiter = CS$<>8__locals1.filesReader.ExistsAsync(CS$<>8__locals1.v1ReportPath).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						throw new PBIProjectReadException(enhancedReportFolderPath, ("You cannot have both PBIR and PBIR-Legacy formats in the same PBIP. Please remove the '" + CS$<>8__locals1.v1ReportPath + "' file.").ToString(CultureInfo.CurrentCulture), false, PBIProjectException.PBIProjectErrorCode.AmbiguityResolvingReport, null, null);
					}
					CS$<>8__locals1.report.Exploration = explorationContent;
				}
				enhancedReportFolderPath = null;
				explorationContent = null;
			}
			string diagramLayoutPathV = Path.Combine(reportDirectory, "datasetDiagramLayout.json");
			string diagramLayoutPathV2 = Path.Combine(reportDirectory, "semanticModelDiagramLayout.json");
			if (new Version(reportDefinition.Version) < ReportDefinition.ArtifactDetailsVersion)
			{
				CS$<>8__locals1.report.DiagramLayout = PBIProjectShredder.AsStreamableContent(CS$<>8__locals1.filesReader, diagramLayoutPathV);
			}
			else
			{
				TaskAwaiter<bool> taskAwaiter = CS$<>8__locals1.filesReader.ExistsAsync(diagramLayoutPathV2).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					if (!PBIProjectShredder.FeatureSwitches.DatasetToSemanticModelRename)
					{
						throw new PBIProjectReadException(diagramLayoutPathV2, "Report is using the new format. The transition to new semantic model and platform format has not been enabled. The feature is controlled by 'DatasetToSemanticModelRename' keyword.".ToString(CultureInfo.CurrentCulture), false, PBIProjectException.PBIProjectErrorCode.FeatureNotEnabled, null, null);
					}
					taskAwaiter = CS$<>8__locals1.filesReader.ExistsAsync(diagramLayoutPathV).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						throw new PBIProjectReadException(diagramLayoutPathV2, string.Concat(new string[] { "You cannot have both '", diagramLayoutPathV2, "' and '", diagramLayoutPathV, "' formats. Please remove '", diagramLayoutPathV, "' file." }).ToString(CultureInfo.CurrentCulture), false, PBIProjectException.PBIProjectErrorCode.AmbiguityResolvingReportDiagramLayout, null, null);
					}
					CS$<>8__locals1.report.DiagramLayout = PBIProjectShredder.AsStreamableContent(CS$<>8__locals1.filesReader, diagramLayoutPathV2);
				}
				else
				{
					CS$<>8__locals1.report.DiagramLayout = PBIProjectShredder.AsStreamableContent(CS$<>8__locals1.filesReader, diagramLayoutPathV);
				}
			}
			PBIProjectReport pbiprojectReport = CS$<>8__locals1.report;
			pbiprojectReport.Thumbnail = await PBIProjectShredder.AsStreamableThumbnailContentAsync(CS$<>8__locals1.filesReader, reportDirectory);
			pbiprojectReport = null;
			string customVisualsFolder = Path.Combine(reportDirectory, "CustomVisuals");
			pbiprojectReport = CS$<>8__locals1.report;
			pbiprojectReport.CustomVisuals = await PBIProjectShredder.ReadAllRelativeContentAsync(CS$<>8__locals1.filesReader, customVisualsFolder, null);
			pbiprojectReport = null;
			string staticResourcesFolder = Path.Combine(reportDirectory, "StaticResources");
			pbiprojectReport = CS$<>8__locals1.report;
			pbiprojectReport.StaticResources = await PBIProjectShredder.ReadAllRelativeContentAsync(CS$<>8__locals1.filesReader, staticResourcesFolder, null);
			pbiprojectReport = null;
			CS$<>8__locals1.localSettingsPath = Path.Combine(reportDirectory, ".pbi", "localSettings.json");
			pbiprojectReport = CS$<>8__locals1.report;
			pbiprojectReport.LocalSettings = await PBIProjectShredder.MakeWithPath<ReportLocalSettings>(CS$<>8__locals1.localSettingsPath, delegate
			{
				PBIProjectShredder.<>c__DisplayClass30_0.<<OpenReportAsync>b__1>d <<OpenReportAsync>b__1>d;
				<<OpenReportAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder<ReportLocalSettings>.Create();
				<<OpenReportAsync>b__1>d.<>4__this = CS$<>8__locals1;
				<<OpenReportAsync>b__1>d.<>1__state = -1;
				<<OpenReportAsync>b__1>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass30_0.<<OpenReportAsync>b__1>d>(ref <<OpenReportAsync>b__1>d);
				return <<OpenReportAsync>b__1>d.<>t__builder.Task;
			});
			pbiprojectReport = null;
			if (new Version(reportDefinition.Version) >= ReportDefinition.DaxQueryViewVersion)
			{
				string enhancedReportFolderPath = Path.Combine(reportDirectory, "DAXQueries");
				pbiprojectReport = CS$<>8__locals1.report;
				pbiprojectReport.DaxQueryView = await PBIProjectShredder.ReadAllRelativeContentAsync(CS$<>8__locals1.filesReader, enhancedReportFolderPath, PBIProjectShredder.DaxQueryViewMatcher);
				pbiprojectReport = null;
				PBIProjectShredder.CheckRequiredArtifact(CS$<>8__locals1.report.DaxQueryView, enhancedReportFolderPath, "DAXQueries", false);
				enhancedReportFolderPath = null;
			}
			await PBIProjectShredder.CheckRequiredArtifactAsync(CS$<>8__locals1.filesReader, CS$<>8__locals1.report.Thumbnail, "thumbnail", false);
			PBIProjectShredder.CheckRequiredArtifact(CS$<>8__locals1.report.CustomVisuals, customVisualsFolder, "CustomVisuals", false);
			PBIProjectShredder.CheckRequiredArtifact(CS$<>8__locals1.report.StaticResources, staticResourcesFolder, "StaticResources", false);
			return CS$<>8__locals1.report;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000978C File Offset: 0x0000798C
		private static async Task<PBIProjectDataset> OpenDatasetAsync(IProjectFilesReader filesReader, string datasetDirectory, PBIProjectShredder.ArtifactDetailsParts artifactDetailsParts)
		{
			PBIProjectShredder.<>c__DisplayClass31_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass31_0();
			CS$<>8__locals1.filesReader = filesReader;
			PBIProjectDataset dataset = new PBIProjectDataset();
			dataset.ArtifactConfig = artifactDetailsParts.Config;
			dataset.ArtifactMetadata = artifactDetailsParts.Metadata;
			dataset.ArtifactDetails = artifactDetailsParts.Details;
			string semanticModelDefinitionPath = Path.Combine(datasetDirectory, "definition.pbism");
			string datasetDefinitionPath = Path.Combine(datasetDirectory, "definition.pbidataset");
			TaskAwaiter<bool> taskAwaiter2;
			PBIProjectDataset pbiprojectDataset;
			if (PBIProjectShredder.FeatureSwitches.DatasetToSemanticModelRename)
			{
				TaskAwaiter<bool> taskAwaiter = CS$<>8__locals1.filesReader.ExistsAsync(semanticModelDefinitionPath).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					taskAwaiter = CS$<>8__locals1.filesReader.ExistsAsync(datasetDefinitionPath).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						string text = semanticModelDefinitionPath;
						string text2 = datasetDefinitionPath;
						throw new PBIProjectReadException(semanticModelDefinitionPath, string.Concat(new string[] { "You cannot have both '", text, "' and '", text2, "' formats. Please remove '", text2, "' file." }).ToString(CultureInfo.CurrentCulture), false, PBIProjectException.PBIProjectErrorCode.AmbiguityResolvingModelSchema, null, null);
					}
					pbiprojectDataset = dataset;
					pbiprojectDataset.Definition = await PBIProjectShredder.OpenDatasetDefinitionDirectlyAsync(CS$<>8__locals1.filesReader, semanticModelDefinitionPath);
					pbiprojectDataset = null;
				}
			}
			if (dataset.Definition == null)
			{
				pbiprojectDataset = dataset;
				pbiprojectDataset.Definition = await PBIProjectShredder.OpenDatasetDefinitionDirectlyAsync(CS$<>8__locals1.filesReader, datasetDefinitionPath);
				pbiprojectDataset = null;
			}
			string bimPath = Path.Combine(datasetDirectory, "model.bim");
			string tmdlFolder = Path.Combine(datasetDirectory, "definition");
			if (new Version(dataset.Definition.Version) < DatasetDefinition.FirstTmdlVersion)
			{
				dataset.DataModelSchema = PBIProjectShredder.AsStreamableContent(CS$<>8__locals1.filesReader, bimPath);
			}
			else
			{
				IDictionary<Uri, IStreamablePowerBIProjectPartContent> tmdlContent = await PBIProjectShredder.ReadAllRelativeContentAsync(CS$<>8__locals1.filesReader, tmdlFolder, PBIProjectShredder.TMDLFormatMatcher);
				if (tmdlContent == null)
				{
					dataset.DataModelSchema = PBIProjectShredder.AsStreamableContent(CS$<>8__locals1.filesReader, bimPath);
				}
				else
				{
					if (!PBIProjectShredder.FeatureSwitches.Tmdl)
					{
						throw new PBIProjectReadException(tmdlFolder, "Semantic model is using TMDL format. Please enable the preview feature 'Store semantic model using TMDL format'.".ToString(CultureInfo.CurrentCulture), false, PBIProjectException.PBIProjectErrorCode.AmbiguityResolvingModelSchema, null, "https://go.microsoft.com/fwlink/?linkid=2249272");
					}
					TaskAwaiter<bool> taskAwaiter = CS$<>8__locals1.filesReader.ExistsAsync(bimPath).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						throw new PBIProjectReadException(bimPath, ("You cannot have both TMDL and TMSL formats in the same PBIP. Please remove the '" + bimPath + "' file.").ToString(CultureInfo.CurrentCulture), false, PBIProjectException.PBIProjectErrorCode.AmbiguityResolvingModelSchema, null, null);
					}
					dataset.DataModelSchemaTmdl = tmdlContent;
				}
				tmdlContent = null;
			}
			dataset.DataModel = PBIProjectShredder.AsStreamableContent(CS$<>8__locals1.filesReader, Path.Combine(datasetDirectory, ".pbi", "cache.abf"));
			dataset.DiagramLayout = PBIProjectShredder.AsStreamableContent(CS$<>8__locals1.filesReader, Path.Combine(datasetDirectory, "diagramLayout.json"));
			CS$<>8__locals1.modelRefPath = Path.Combine(datasetDirectory, "modelReference.json");
			pbiprojectDataset = dataset;
			pbiprojectDataset.ModelReference = await PBIProjectShredder.MakeWithPath<DatasetModelReference>(CS$<>8__locals1.modelRefPath, delegate
			{
				PBIProjectShredder.<>c__DisplayClass31_0.<<OpenDatasetAsync>b__0>d <<OpenDatasetAsync>b__0>d;
				<<OpenDatasetAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<DatasetModelReference>.Create();
				<<OpenDatasetAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<OpenDatasetAsync>b__0>d.<>1__state = -1;
				<<OpenDatasetAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass31_0.<<OpenDatasetAsync>b__0>d>(ref <<OpenDatasetAsync>b__0>d);
				return <<OpenDatasetAsync>b__0>d.<>t__builder.Task;
			});
			pbiprojectDataset = null;
			CS$<>8__locals1.editorSettingsPath = Path.Combine(datasetDirectory, ".pbi", "editorSettings.json");
			pbiprojectDataset = dataset;
			pbiprojectDataset.EditorSettings = await PBIProjectShredder.MakeWithPath<DatasetEditorSettings>(CS$<>8__locals1.editorSettingsPath, delegate
			{
				PBIProjectShredder.<>c__DisplayClass31_0.<<OpenDatasetAsync>b__1>d <<OpenDatasetAsync>b__1>d;
				<<OpenDatasetAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder<DatasetEditorSettings>.Create();
				<<OpenDatasetAsync>b__1>d.<>4__this = CS$<>8__locals1;
				<<OpenDatasetAsync>b__1>d.<>1__state = -1;
				<<OpenDatasetAsync>b__1>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass31_0.<<OpenDatasetAsync>b__1>d>(ref <<OpenDatasetAsync>b__1>d);
				return <<OpenDatasetAsync>b__1>d.<>t__builder.Task;
			});
			pbiprojectDataset = null;
			CS$<>8__locals1.localSettingsPath = Path.Combine(datasetDirectory, ".pbi", "localSettings.json");
			pbiprojectDataset = dataset;
			pbiprojectDataset.LocalSettings = await PBIProjectShredder.MakeWithPath<DatasetLocalSettings>(CS$<>8__locals1.localSettingsPath, delegate
			{
				PBIProjectShredder.<>c__DisplayClass31_0.<<OpenDatasetAsync>b__2>d <<OpenDatasetAsync>b__2>d;
				<<OpenDatasetAsync>b__2>d.<>t__builder = AsyncTaskMethodBuilder<DatasetLocalSettings>.Create();
				<<OpenDatasetAsync>b__2>d.<>4__this = CS$<>8__locals1;
				<<OpenDatasetAsync>b__2>d.<>1__state = -1;
				<<OpenDatasetAsync>b__2>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass31_0.<<OpenDatasetAsync>b__2>d>(ref <<OpenDatasetAsync>b__2>d);
				return <<OpenDatasetAsync>b__2>d.<>t__builder.Task;
			});
			pbiprojectDataset = null;
			CS$<>8__locals1.unappliedChangesPath = Path.Combine(datasetDirectory, ".pbi", "unappliedChanges.json");
			pbiprojectDataset = dataset;
			pbiprojectDataset.UnappliedChanges = await PBIProjectShredder.MakeWithPath<UnappliedChanges>(CS$<>8__locals1.unappliedChangesPath, delegate
			{
				PBIProjectShredder.<>c__DisplayClass31_0.<<OpenDatasetAsync>b__3>d <<OpenDatasetAsync>b__3>d;
				<<OpenDatasetAsync>b__3>d.<>t__builder = AsyncTaskMethodBuilder<UnappliedChanges>.Create();
				<<OpenDatasetAsync>b__3>d.<>4__this = CS$<>8__locals1;
				<<OpenDatasetAsync>b__3>d.<>1__state = -1;
				<<OpenDatasetAsync>b__3>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass31_0.<<OpenDatasetAsync>b__3>d>(ref <<OpenDatasetAsync>b__3>d);
				return <<OpenDatasetAsync>b__3>d.<>t__builder.Task;
			});
			pbiprojectDataset = null;
			if (new Version(dataset.Definition.Version) >= DatasetDefinition.DaxQueryViewVersion)
			{
				string daxQueriesFolder = Path.Combine(datasetDirectory, "DAXQueries");
				pbiprojectDataset = dataset;
				pbiprojectDataset.DaxQueryView = await PBIProjectShredder.ReadAllRelativeContentAsync(CS$<>8__locals1.filesReader, daxQueriesFolder, PBIProjectShredder.DaxQueryViewMatcher);
				pbiprojectDataset = null;
				PBIProjectShredder.CheckRequiredArtifact(dataset.DaxQueryView, daxQueriesFolder, "DAXQueries", false);
				daxQueriesFolder = null;
			}
			if (dataset.ModelReference != null)
			{
				if (dataset.IsDatasetInTmdlFormat())
				{
					throw new PBIProjectReadException(tmdlFolder, ("You cannot have '" + CS$<>8__locals1.modelRefPath + "' and TMDL content in the same PBIP.").ToString(CultureInfo.CurrentCulture), false, PBIProjectException.PBIProjectErrorCode.AmbiguityResolvingModelSchema, null, null);
				}
				TaskAwaiter<bool> taskAwaiter = CS$<>8__locals1.filesReader.ExistsAsync(bimPath).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					throw new PBIProjectReadException(bimPath, string.Concat(new string[] { "You cannot have '", CS$<>8__locals1.modelRefPath, "' and TMSL content '", bimPath, "' in the same PBIP." }).ToString(CultureInfo.CurrentCulture), false, PBIProjectException.PBIProjectErrorCode.AmbiguityResolvingModelSchema, null, null);
				}
			}
			else if (dataset.DataModelSchema != null)
			{
				await PBIProjectShredder.CheckRequiredArtifactAsync(CS$<>8__locals1.filesReader, dataset.DataModelSchema, "model.bim", true);
			}
			else if (!dataset.IsDatasetInTmdlFormat())
			{
				throw new PBIProjectReadException(tmdlFolder, ("Missing content in '" + tmdlFolder + "'.").ToString(CultureInfo.CurrentCulture), false, PBIProjectException.PBIProjectErrorCode.RequiredArtifactMissing, null, null);
			}
			await PBIProjectShredder.CheckRequiredArtifactAsync(CS$<>8__locals1.filesReader, dataset.DataModel, "cache.abf", false);
			await PBIProjectShredder.CheckRequiredArtifactAsync(CS$<>8__locals1.filesReader, dataset.DiagramLayout, "diagramLayout.json", false);
			await PBIProjectShredder.CheckRequiredArtifactAsync(CS$<>8__locals1.filesReader, dataset.UnappliedChanges, "unappliedChanges.json", false);
			return dataset;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000097E0 File Offset: 0x000079E0
		private static async Task<DatasetDefinition> OpenDatasetDefinitionDirectlyAsync(IProjectFilesReader filesReader, string definitionPath)
		{
			PBIProjectShredder.<>c__DisplayClass32_0 CS$<>8__locals1 = new PBIProjectShredder.<>c__DisplayClass32_0();
			CS$<>8__locals1.filesReader = filesReader;
			CS$<>8__locals1.definitionPath = definitionPath;
			return await PBIProjectShredder.MakeWithPath<DatasetDefinition>(CS$<>8__locals1.definitionPath, delegate
			{
				PBIProjectShredder.<>c__DisplayClass32_0.<<OpenDatasetDefinitionDirectlyAsync>b__0>d <<OpenDatasetDefinitionDirectlyAsync>b__0>d;
				<<OpenDatasetDefinitionDirectlyAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<DatasetDefinition>.Create();
				<<OpenDatasetDefinitionDirectlyAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<OpenDatasetDefinitionDirectlyAsync>b__0>d.<>1__state = -1;
				<<OpenDatasetDefinitionDirectlyAsync>b__0>d.<>t__builder.Start<PBIProjectShredder.<>c__DisplayClass32_0.<<OpenDatasetDefinitionDirectlyAsync>b__0>d>(ref <<OpenDatasetDefinitionDirectlyAsync>b__0>d);
				return <<OpenDatasetDefinitionDirectlyAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000982C File Offset: 0x00007A2C
		private static string GetDatasetDirFromReportDefinition(ReportDefinition definition)
		{
			string text = null;
			if (definition.DatasetReference.ByPath != null)
			{
				string directoryName = Path.GetDirectoryName(definition.FileName);
				string path = definition.DatasetReference.ByPath.Path;
				if (Path.IsPathRooted(path))
				{
					string text2 = "ByPath property must be a relative path.";
					throw new PBIProjectReadException(path, text2, true, PBIProjectException.PBIProjectErrorCode.ByPathNotRelative, null, null);
				}
				text = new Uri(Path.Combine(directoryName, path)).LocalPath;
			}
			else if (definition.DatasetReference.ByConnection == null)
			{
				string text3 = "Object 'datasetReference' must either have 'byPath' or 'byConnection' defined.";
				throw new PBIProjectReadException(definition.FileName, text3, true, PBIProjectException.PBIProjectErrorCode.ByPathAndByConnectionMissing, null, null);
			}
			return text;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000098B8 File Offset: 0x00007AB8
		private static async Task CheckRequiredArtifactAsync(IProjectFilesReader reader, IFromPBIProjectFile artifact, string name, bool isRequired)
		{
			bool flag = isRequired;
			if (flag)
			{
				bool flag2 = artifact == null;
				if (!flag2)
				{
					TaskAwaiter<bool> taskAwaiter = reader.ExistsAsync(artifact.FileName).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					flag2 = !taskAwaiter.GetResult();
				}
				flag = flag2;
			}
			if (flag)
			{
				throw new PBIProjectReadException(artifact.FileName, ("Missing required artifact '" + name + "'.").ToString(CultureInfo.CurrentCulture), true, PBIProjectException.PBIProjectErrorCode.RequiredArtifactMissing, null, null);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009913 File Offset: 0x00007B13
		private static void CheckRequiredArtifact(object artifact, string fromFileName, string name, bool isRequired)
		{
			if (isRequired && artifact == null)
			{
				throw new PBIProjectReadException(fromFileName, ("Missing required artifact '" + name + "'.").ToString(CultureInfo.CurrentCulture), true, PBIProjectException.PBIProjectErrorCode.RequiredArtifactMissing, null, null);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00009940 File Offset: 0x00007B40
		internal static async Task<string> ReadAllTextFromProjectFileAsync(IProjectFilesReader filesReader, string path)
		{
			Stream stream2 = await filesReader.GetAsync(path);
			string text;
			using (Stream stream = stream2)
			{
				if (stream == null)
				{
					text = null;
				}
				else
				{
					using (StreamReader streamReader = PBIProjectUtils.MakeStreamReader(stream))
					{
						text = await streamReader.ReadToEndAsync();
					}
				}
			}
			return text;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000998C File Offset: 0x00007B8C
		private static async Task<IStreamablePowerBIProjectPartContent> AsStreamableThumbnailContentAsync(IProjectFilesReader filesReader, string reportDirectory)
		{
			IStreamablePowerBIProjectPartContent streamablePowerBIProjectPartContent;
			try
			{
				string[] array = await filesReader.GetAllPathsAsync(reportDirectory);
				List<string> list;
				if (array == null)
				{
					list = null;
				}
				else
				{
					IEnumerable<string> enumerable = array.Where((string p) => Path.GetFileNameWithoutExtension(p).Equals("thumbnail"));
					list = ((enumerable != null) ? enumerable.ToList<string>() : null);
				}
				List<string> list2 = list;
				if (list2 != null && list2.Count == 1)
				{
					streamablePowerBIProjectPartContent = new PBIProjectShredder.PbipContent(filesReader, list2[0]);
				}
				else
				{
					if (list2 != null && list2.Count > 1)
					{
						throw new PBIProjectParseException(("Found more than one thumbnail file in directory '" + reportDirectory + "'.").ToString(CultureInfo.CurrentCulture), PBIProjectException.PBIProjectErrorCode.MultipleThumbnailsException);
					}
					streamablePowerBIProjectPartContent = new PBIProjectShredder.PbipContent(filesReader, Path.Combine(reportDirectory, "thumbnail.png"));
				}
			}
			catch (ProjectFilesReaderFolderException ex)
			{
				throw new PBIProjectParseException(ex.Message, PBIProjectException.PBIProjectErrorCode.ThumbnailsFolderError);
			}
			return streamablePowerBIProjectPartContent;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000099D7 File Offset: 0x00007BD7
		private static IStreamablePowerBIProjectPartContent AsStreamableContent(IProjectFilesReader filesReader, string path)
		{
			return new PBIProjectShredder.PbipContent(filesReader, path);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000099E0 File Offset: 0x00007BE0
		private static async Task<IDictionary<Uri, IStreamablePowerBIProjectPartContent>> ReadAllRelativeContentAsync(IProjectFilesReader filesReader, string folderPath, IFileFormatMatcher matcher = null)
		{
			string[] array;
			try
			{
				array = await filesReader.GetAllPathsAsync(folderPath);
			}
			catch (ProjectFilesReaderFolderException ex)
			{
				throw new PBIProjectReadException(folderPath, ex.Message, false, PBIProjectException.PBIProjectErrorCode.RelativeContentError, ex, null);
			}
			IDictionary<Uri, IStreamablePowerBIProjectPartContent> dictionary;
			if (array == null)
			{
				dictionary = null;
			}
			else
			{
				string text = folderPath.Replace("\\", "/");
				Dictionary<Uri, IStreamablePowerBIProjectPartContent> dictionary2 = new Dictionary<Uri, IStreamablePowerBIProjectPartContent>();
				foreach (string text2 in array)
				{
					string text3 = text2.Replace("\\", "/").Substring(text.Length + 1);
					if (matcher == null || matcher.IsMatch(text3))
					{
						dictionary2.Add(new Uri(text3, UriKind.Relative), PBIProjectShredder.AsStreamableContent(filesReader, text2));
					}
				}
				dictionary = ((dictionary2.Count > 0) ? dictionary2 : null);
			}
			return dictionary;
		}

		// Token: 0x040001C9 RID: 457
		private static readonly AsyncLocal<HostContext> AsyncLocalContext = new AsyncLocal<HostContext>();

		// Token: 0x040001CA RID: 458
		public static readonly IFileFormatMatcher EnhancedReportFormatMatcher = new EnhancedReportFormatMatcher();

		// Token: 0x040001CB RID: 459
		public static readonly IFileFormatMatcher TMDLFormatMatcher = new TMDLFormatMatcher();

		// Token: 0x040001CC RID: 460
		public static readonly IFileFormatMatcher DaxQueryViewMatcher = new DaxQueryViewFileMatcher();

		// Token: 0x020000D8 RID: 216
		private class ContextScope : IDisposable
		{
			// Token: 0x06000506 RID: 1286 RVA: 0x0000E46B File Offset: 0x0000C66B
			public ContextScope(HostContext context)
			{
				this.previous = PBIProjectShredder.AsyncLocalContext.Value;
				PBIProjectShredder.AsyncLocalContext.Value = context;
				HostSetup.SetHostApplication(context.Application);
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x0000E499 File Offset: 0x0000C699
			public void Dispose()
			{
				PBIProjectShredder.AsyncLocalContext.Value = this.previous;
			}

			// Token: 0x04000364 RID: 868
			private HostContext previous;
		}

		// Token: 0x020000D9 RID: 217
		private struct ArtifactDetailsParts
		{
			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000E4AB File Offset: 0x0000C6AB
			// (set) Token: 0x06000509 RID: 1289 RVA: 0x0000E4B3 File Offset: 0x0000C6B3
			public ArtifactConfig Config { get; set; }

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000E4BC File Offset: 0x0000C6BC
			// (set) Token: 0x0600050B RID: 1291 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
			public ArtifactMetadata Metadata { get; set; }

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000E4CD File Offset: 0x0000C6CD
			// (set) Token: 0x0600050D RID: 1293 RVA: 0x0000E4D5 File Offset: 0x0000C6D5
			public ArtifactDetails Details { get; set; }
		}

		// Token: 0x020000DA RID: 218
		private class PbipContent : IStreamablePowerBIProjectPartContent, IFromPBIProjectFile
		{
			// Token: 0x0600050E RID: 1294 RVA: 0x0000E4DE File Offset: 0x0000C6DE
			public PbipContent(IProjectFilesReader filesReader, string filename)
			{
				this.filesReader = filesReader;
				this.fileName = filename;
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
			// (set) Token: 0x06000510 RID: 1296 RVA: 0x0000E4FC File Offset: 0x0000C6FC
			public string FileName
			{
				get
				{
					return this.fileName;
				}
				set
				{
					this.fileName = value;
				}
			}

			// Token: 0x06000511 RID: 1297 RVA: 0x0000E505 File Offset: 0x0000C705
			public Task<Stream> GetStreamAsync()
			{
				return PBIProjectShredder.CallAndConvertIntoPathExceptionsAsync<Stream>(this.fileName, async () => await this.filesReader.GetAsync(this.fileName));
			}

			// Token: 0x04000368 RID: 872
			private IProjectFilesReader filesReader;

			// Token: 0x04000369 RID: 873
			private string fileName;
		}
	}
}
