using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AnalysisServices.Extensions;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200016E RID: 366
	internal sealed class FolderMetadataSerializationManager : MetadataSerializationManager
	{
		// Token: 0x06001753 RID: 5971 RVA: 0x000A249B File Offset: 0x000A069B
		public FolderMetadataSerializationManager(string folderPath, string extension, params string[] additionalExtensions)
			: base(extension)
		{
			this.folder = FilesHelper.GetDirectoryInfoWithNormalizedPath(folderPath);
			this.additionalExtensions = ((additionalExtensions != null && additionalExtensions.Length != 0) ? FolderMetadataSerializationManager.BuildAdditionalExtensions(additionalExtensions) : null);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000A24C6 File Offset: 0x000A06C6
		public FolderMetadataSerializationManager(DirectoryInfo folder, string extension, params string[] additionalExtensions)
			: base(extension)
		{
			this.folder = FilesHelper.GetDirectoryInfoWithNormalizedPath(folder.FullName);
			this.additionalExtensions = ((additionalExtensions != null && additionalExtensions.Length != 0) ? FolderMetadataSerializationManager.BuildAdditionalExtensions(additionalExtensions) : null);
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x000A24F6 File Offset: 0x000A06F6
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x000A24FE File Offset: 0x000A06FE
		public Func<DirectoryInfo, FileInfo, bool> FilesFilter { get; set; }

		// Token: 0x06001757 RID: 5975 RVA: 0x000A2508 File Offset: 0x000A0708
		private protected override void OnSerializationStart(object userContext, IReadOnlyCollection<string> logicalPaths, out object operationContext)
		{
			this.folder.Refresh();
			this.previousSerializationFiles = new HashSet<string>(from fi in this.folder.EnumerateFiles("*.*", SearchOption.AllDirectories)
				where string.Compare(fi.Extension, this.extension, StringComparison.InvariantCultureIgnoreCase) == 0 || (this.additionalExtensions != null && this.additionalExtensions.Contains(fi.Extension))
				where this.FilesFilter == null || this.FilesFilter(this.folder, fi)
				select fi.FullName);
			operationContext = null;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000A2588 File Offset: 0x000A0788
		private protected override void OnDocumentSerializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document)
		{
			string text;
			string text2;
			if (!MetadataSerializationManager.ParseLogicalPath(logicalPath, out text, out text2))
			{
				throw new ArgumentException(TomSR.Exception_InvalidLogicalPath(logicalPath), "logicalPath");
			}
			FileInfo fileInfo = new FileInfo(Path.Combine(((text == null) ? this.folder : this.folder.CreateSubdirectory(text)).FullName, string.Format("{0}{1}", text2, this.extension)));
			this.previousSerializationFiles.Remove(fileInfo.FullName);
			documentContext = null;
			document = fileInfo.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x000A260C File Offset: 0x000A080C
		private protected override void OnDocumentSerializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulSerialization)
		{
			document.Close();
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000A2618 File Offset: 0x000A0818
		private protected override void OnSerializationEnd(object userContext, object operationContext, bool isSuccessfulSerialization)
		{
			if (isSuccessfulSerialization)
			{
				foreach (string text in this.previousSerializationFiles)
				{
					try
					{
						File.Delete(text);
					}
					catch (IOException)
					{
					}
				}
				this.folder.CleanupEmptySubDirectories();
			}
			this.previousSerializationFiles = null;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000A2690 File Offset: 0x000A0890
		private protected override IReadOnlyCollection<string> GetDocumentsForDeserialization(object userContext)
		{
			this.folder.Refresh();
			return new List<string>(from fi in this.folder.EnumerateFiles("*.*", SearchOption.AllDirectories)
				where string.Compare(fi.Extension, this.extension, StringComparison.InvariantCultureIgnoreCase) == 0 || (this.additionalExtensions != null && this.additionalExtensions.Contains(fi.Extension))
				where this.FilesFilter == null || this.FilesFilter(this.folder, fi)
				select FolderMetadataSerializationManager.BuildLogicalPath(this.folder, fi, this.extension));
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x000A26F4 File Offset: 0x000A08F4
		private protected override void OnDocumentDeserializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document)
		{
			string text;
			string text2;
			if (!MetadataSerializationManager.ParseLogicalPath(logicalPath, out text, out text2))
			{
				throw new ArgumentException(TomSR.Exception_InvalidLogicalPath(logicalPath), "logicalPath");
			}
			DirectoryInfo directoryInfo = ((text == null) ? this.folder : this.folder.EnsureSubdirectory(text));
			FileInfo fileInfo = directoryInfo.EnumerateFiles(string.Format("{0}{1}", text2, this.extension), SearchOption.TopDirectoryOnly).FirstOrDefault<FileInfo>();
			if (fileInfo == null)
			{
				fileInfo = directoryInfo.EnumerateFiles(text2, SearchOption.TopDirectoryOnly).Single<FileInfo>();
			}
			documentContext = null;
			document = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x000A2775 File Offset: 0x000A0975
		private protected override void OnDocumentDeserializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulDeserialization)
		{
			document.Close();
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x000A2780 File Offset: 0x000A0980
		internal static string BuildLogicalPath(DirectoryInfo rootFolder, FileInfo file, string extension)
		{
			string text = ((string.Compare(file.Extension, extension, StringComparison.InvariantCultureIgnoreCase) == 0) ? Path.GetFileNameWithoutExtension(file.Name) : file.Name);
			string text2 = ((string.Compare(file.Directory.FullName, rootFolder.FullName, StringComparison.CurrentCultureIgnoreCase) == 0) ? null : file.Directory.FullName.Substring(rootFolder.FullName.Length + 1).Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
			if (!string.IsNullOrEmpty(text2))
			{
				return string.Format(".{0}{1}{2}{3}", new object[]
				{
					Path.AltDirectorySeparatorChar,
					text2,
					Path.AltDirectorySeparatorChar,
					text
				});
			}
			return string.Format(".{0}{1}", Path.AltDirectorySeparatorChar, text);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x000A284C File Offset: 0x000A0A4C
		private static ICollection<string> BuildAdditionalExtensions(string[] additionalExtensions)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			for (int i = 0; i < additionalExtensions.Length; i++)
			{
				string extension = Path.GetExtension(additionalExtensions[i]);
				if (!string.IsNullOrEmpty(extension))
				{
					hashSet.Add(extension);
				}
			}
			return hashSet;
		}

		// Token: 0x04000449 RID: 1097
		private readonly DirectoryInfo folder;

		// Token: 0x0400044A RID: 1098
		private readonly ICollection<string> additionalExtensions;

		// Token: 0x0400044B RID: 1099
		private HashSet<string> previousSerializationFiles;
	}
}
