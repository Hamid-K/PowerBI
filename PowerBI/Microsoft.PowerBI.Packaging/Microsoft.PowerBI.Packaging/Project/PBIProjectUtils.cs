using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.Packaging.Project.Artifacts;
using Microsoft.PowerBI.Packaging.Storage;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000075 RID: 117
	public static class PBIProjectUtils
	{
		// Token: 0x0600035D RID: 861 RVA: 0x00009A5D File Offset: 0x00007C5D
		public static bool HasLocalDataset(PBIProjectReport report)
		{
			ReportDefinition reportDefinition = report.ReportDefinition;
			object obj;
			if (reportDefinition == null)
			{
				obj = null;
			}
			else
			{
				DatasetReference datasetReference = reportDefinition.DatasetReference;
				if (datasetReference == null)
				{
					obj = null;
				}
				else
				{
					ReportDatasetReferenceByPath byPath = datasetReference.ByPath;
					obj = ((byPath != null) ? byPath.Path : null);
				}
			}
			return obj != null;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00009A8C File Offset: 0x00007C8C
		public static Version[] GetSupportedVersions<T>()
		{
			PropertyInfo property = typeof(T).GetProperty("SupportedVersions", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (property == null)
			{
				throw new NotImplementedException(string.Format(CultureInfo.CurrentCulture, "The type {0} did not implement {1}.", "T", "SupportedVersions"));
			}
			Version[] array = (Version[])property.GetValue(null, null);
			if (array != null && !array.Any<Version>())
			{
				throw new NotImplementedException(string.Format(CultureInfo.CurrentCulture, "The type {0} did not provide any version with {1} static property.", "T", "SupportedVersions"));
			}
			return array;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009B14 File Offset: 0x00007D14
		public static Version GetLatestVersion<T>()
		{
			return PBIProjectUtils.GetSupportedVersions<T>().Max<Version>();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009B20 File Offset: 0x00007D20
		public static bool IsSafeException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is AccessViolationException) && !(e is SEHException) && !typeof(SecurityException).IsAssignableFrom(e.GetType());
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009B6F File Offset: 0x00007D6F
		public static string[] SplitStringToMultipleLines(string s)
		{
			return s.Replace(PBIProjectUtils.CarriageReturn, string.Empty).Split(new char[] { PBIProjectUtils.LineBreaker }, StringSplitOptions.None);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009B98 File Offset: 0x00007D98
		public static string ConvertMultilineToString(string[] s)
		{
			int num = s.Length - 1;
			if (num < 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append(s[i]);
				stringBuilder.Append(PBIProjectUtils.LineBreaker);
			}
			stringBuilder.Append(s[num]);
			return stringBuilder.ToString();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00009BED File Offset: 0x00007DED
		public static bool IsByPath(ConnectionsSettings connectionSettings)
		{
			return connectionSettings == null || connectionSettings.Connections == null || PBIProjectUtils.NeedsModelReference(connectionSettings);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00009C08 File Offset: 0x00007E08
		public static bool NeedsModelReference(ConnectionsSettings connectionSettings)
		{
			if (connectionSettings != null)
			{
				Dictionary<string, ConnectionProperties> connections = connectionSettings.Connections;
				bool? flag = ((connections != null) ? new bool?(connections.Any<KeyValuePair<string, ConnectionProperties>>()) : null);
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					ConnectionProperties value = connectionSettings.Connections.FirstOrDefault<KeyValuePair<string, ConnectionProperties>>().Value;
					if (value.ConnectionType != "pbiServiceXmlaStyleLive" && value.ConnectionType != "pbiServiceLive")
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00009C8C File Offset: 0x00007E8C
		public static bool TryDetectBom(string utf8String, out string bom)
		{
			bom = null;
			if (utf8String.Length > 5)
			{
				byte[] array = new byte[50];
				Encoding.UTF8.GetBytes(utf8String, 0, 5, array, 0);
				if (array[0] == 0 && array[1] == 0 && array[2] == 254 && array[3] == 255)
				{
					bom = Encoding.GetEncoding("utf-32BE").ToString();
				}
				else if (array[0] == 255 && array[1] == 254 && array[2] == 0 && array[3] == 0)
				{
					bom = Encoding.UTF32.ToString();
				}
				else if (array[0] == 239 && array[1] == 187 && array[2] == 191)
				{
					bom = Encoding.UTF8.ToString();
				}
				else if (array[0] == 254 && array[1] == 255)
				{
					bom = Encoding.BigEndianUnicode.ToString();
				}
				else
				{
					if (array[0] != 255 || array[1] != 254)
					{
						return false;
					}
					bom = Encoding.Unicode.ToString();
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00009D98 File Offset: 0x00007F98
		public static async Task<byte[]> ReadAllBytesAsync(IStreamablePowerBIProjectPartContent partContent, bool isRequired)
		{
			byte[] bytes = null;
			if (partContent != null)
			{
				Stream stream2 = await partContent.GetStreamAsync();
				using (Stream stream = stream2)
				{
					if (stream != null)
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							await stream.CopyToAsync(memoryStream);
							bytes = memoryStream.ToArray();
						}
						MemoryStream memoryStream = null;
					}
				}
				Stream stream = null;
			}
			if (isRequired)
			{
				if (partContent == null)
				{
					throw new ArgumentNullException("partContent");
				}
				string text = null;
				if (bytes == null)
				{
					text = ("'" + partContent.FileName + "' has not been found.").ToString(CultureInfo.CurrentCulture);
				}
				else if (bytes.Length == 0)
				{
					text = ("'" + partContent.FileName + "' can't be empty.").ToString(CultureInfo.CurrentCulture);
				}
				if (text != null)
				{
					throw new PBIProjectValidationException(text, PBIProjectException.TryGetKnownFilenameFromPath(partContent.FileName), PBIProjectException.PBIProjectErrorCode.ReadAllByteRequiredArtifact);
				}
			}
			return bytes;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00009DE3 File Offset: 0x00007FE3
		public static IStreamablePowerBIProjectPartContent AsStreamableContent(byte[] bytes)
		{
			return new StreamablePowerBIProjectPartContent(bytes);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00009DEB File Offset: 0x00007FEB
		public static Uri MakeRelativeUri(string relativePath)
		{
			relativePath = relativePath.Replace("\\", "/");
			return new Uri(relativePath, UriKind.Relative);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00009E08 File Offset: 0x00008008
		public static string RelativePathToTmdlLogicalPath(string relativePath)
		{
			string directoryName = Path.GetDirectoryName(relativePath);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(relativePath);
			return "./" + Path.Combine(directoryName, fileNameWithoutExtension).Replace('\\', '/');
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00009E3D File Offset: 0x0000803D
		public static string TmdlLogicalPathToRelativePath(string logicalPath)
		{
			return logicalPath.Substring("./".Length) + ".tmdl";
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00009E59 File Offset: 0x00008059
		public static StreamReader MakeStreamReader(Stream stream)
		{
			return new StreamReader(stream, PBIProjectConstants.SafeUtf8NoBom, false, 4096, true);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00009E70 File Offset: 0x00008070
		public static void EnsureNotLong(string path, bool isFolder)
		{
			if (string.IsNullOrEmpty(path))
			{
				return;
			}
			int num = (isFolder ? 248 : 260);
			if (path.Length > num)
			{
				throw new PathTooLongException();
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00009EA8 File Offset: 0x000080A8
		public static Version DatasetLatestApplicableVersion(bool hasDaxViewContent, bool isTmdlUsed, bool isArtifactDetailsUsed)
		{
			Version currentVersion = DatasetDefinition.CurrentVersion;
			PBIProjectUtils.RequireVersion(hasDaxViewContent, DatasetDefinition.DaxQueryViewVersion, ref currentVersion);
			PBIProjectUtils.RequireVersion(isTmdlUsed, DatasetDefinition.FirstTmdlVersion, ref currentVersion);
			PBIProjectUtils.RequireVersion(isArtifactDetailsUsed, DatasetDefinition.ArtifactDetailsVersion, ref currentVersion);
			return currentVersion;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00009EE4 File Offset: 0x000080E4
		public static Version ReportLatestApplicableVersion(bool hasDaxViewContent, bool isEnhancedReportFormat, bool isArtifactDetailsUsed)
		{
			Version currentVersion = ReportDefinition.CurrentVersion;
			PBIProjectUtils.RequireVersion(hasDaxViewContent, ReportDefinition.DaxQueryViewVersion, ref currentVersion);
			PBIProjectUtils.RequireVersion(isEnhancedReportFormat, ReportDefinition.FirstEnhancedReportVersion, ref currentVersion);
			PBIProjectUtils.RequireVersion(isArtifactDetailsUsed, ReportDefinition.ArtifactDetailsVersion, ref currentVersion);
			return currentVersion;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00009F1F File Offset: 0x0000811F
		public static bool IsReportFileFormatV2(this PBIProjectReport report)
		{
			return report.Exploration != null && report.Exploration.Any<KeyValuePair<Uri, IStreamablePowerBIProjectPartContent>>();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00009F36 File Offset: 0x00008136
		public static bool IsDatasetInTmdlFormat(this PBIProjectDataset dataset)
		{
			IDictionary<Uri, IStreamablePowerBIProjectPartContent> dataModelSchemaTmdl = dataset.DataModelSchemaTmdl;
			return dataModelSchemaTmdl != null && dataModelSchemaTmdl.Count > 0;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00009F4C File Offset: 0x0000814C
		public static bool MatchesEverything(this IFileFormatMatcher matcher, IEnumerable<string> relativePaths)
		{
			return relativePaths.All((string path) => matcher.IsMatch(path));
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00009F78 File Offset: 0x00008178
		private static void RequireVersion(bool needsVersion, Version requiredVersion, ref Version version)
		{
			if (needsVersion && version < requiredVersion)
			{
				version = requiredVersion;
			}
		}

		// Token: 0x040001CD RID: 461
		private static readonly char LineBreaker = '\n';

		// Token: 0x040001CE RID: 462
		private static readonly string CarriageReturn = "\r";
	}
}
