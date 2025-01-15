using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000193 RID: 403
	internal sealed class ZipMetadataSerializationManager : MetadataSerializationManager
	{
		// Token: 0x0600186A RID: 6250 RVA: 0x000A3CEB File Offset: 0x000A1EEB
		public ZipMetadataSerializationManager(string zipFilePath, string extension)
			: base(extension)
		{
			this.zipFile = new FileInfo(zipFilePath);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x000A3D00 File Offset: 0x000A1F00
		public ZipMetadataSerializationManager(FileInfo zipFile, string extension)
			: base(extension)
		{
			this.zipFile = new FileInfo(zipFile.FullName);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x000A3D1C File Offset: 0x000A1F1C
		private protected override void OnSerializationStart(object userContext, IReadOnlyCollection<string> logicalPaths, out object operationContext)
		{
			this.zipFile.Refresh();
			Stream stream = this.zipFile.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.None);
			ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, true);
			operationContext = new Tuple<ZipArchive, Stream>(zipArchive, stream);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x000A3D58 File Offset: 0x000A1F58
		private protected override void OnDocumentSerializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document)
		{
			if (operationContext != null)
			{
				Tuple<ZipArchive, Stream> tuple = operationContext as Tuple<ZipArchive, Stream>;
				if (tuple != null)
				{
					string text;
					string text2;
					if (!MetadataSerializationManager.ParseLogicalPath(logicalPath, out text, out text2))
					{
						throw new ArgumentException(TomSR.Exception_InvalidLogicalPath(logicalPath), "logicalPath");
					}
					ZipArchiveEntry zipArchiveEntry = tuple.Item1.CreateEntry(string.IsNullOrEmpty(text) ? string.Format("{0}{1}", text2, this.extension) : string.Format("{0}{1}{2}{3}", new object[]
					{
						text,
						Path.AltDirectorySeparatorChar,
						text2,
						this.extension
					}));
					document = zipArchiveEntry.Open();
					documentContext = zipArchiveEntry;
					return;
				}
			}
			throw new ArgumentException(TomSR.Exception_InvalidOpContextOnZipController, "operationContext");
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x000A3E01 File Offset: 0x000A2001
		private protected override void OnDocumentSerializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulSerialization)
		{
			document.Close();
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x000A3E0C File Offset: 0x000A200C
		private protected override void OnSerializationEnd(object userContext, object operationContext, bool isSuccessfulSerialization)
		{
			if (operationContext != null)
			{
				Tuple<ZipArchive, Stream> tuple = operationContext as Tuple<ZipArchive, Stream>;
				if (tuple != null)
				{
					tuple.Item1.Dispose();
					tuple.Item2.Close();
					return;
				}
			}
			throw new ArgumentException(TomSR.Exception_InvalidOpContextOnZipController, "operationContext");
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x000A3E4C File Offset: 0x000A204C
		private protected override IReadOnlyCollection<string> GetDocumentsForDeserialization(object userContext)
		{
			throw TomInternalException.Create("This method should never be called directly!", Array.Empty<object>());
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x000A3E60 File Offset: 0x000A2060
		private protected override void OnDeserializationStart(object userContext, out object operationContext, out IReadOnlyCollection<string> logicalPaths)
		{
			this.zipFile.Refresh();
			Stream stream = this.zipFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
			ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Read, true);
			operationContext = new Tuple<ZipArchive, Stream>(zipArchive, stream);
			logicalPaths = new List<string>(from e in zipArchive.Entries
				where !string.IsNullOrEmpty(e.Name)
				select ZipMetadataSerializationManager.BuildLogicalPath(e, this.extension));
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000A3EDC File Offset: 0x000A20DC
		private protected override void OnDocumentDeserializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document)
		{
			if (operationContext != null)
			{
				Tuple<ZipArchive, Stream> tuple = operationContext as Tuple<ZipArchive, Stream>;
				if (tuple != null)
				{
					string text;
					string text2;
					if (!MetadataSerializationManager.ParseLogicalPath(logicalPath, out text, out text2))
					{
						throw new ArgumentException(TomSR.Exception_InvalidLogicalPath(logicalPath), "logicalPath");
					}
					ZipArchiveEntry entry = tuple.Item1.GetEntry(string.IsNullOrEmpty(text) ? string.Format("{0}{1}", text2, this.extension) : string.Format("{0}{1}{2}{3}", new object[]
					{
						text,
						Path.AltDirectorySeparatorChar,
						text2,
						this.extension
					}));
					document = entry.Open();
					documentContext = entry;
					return;
				}
			}
			throw new ArgumentException(TomSR.Exception_InvalidOpContextOnZipController, "operationContext");
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000A3F85 File Offset: 0x000A2185
		private protected override void OnDocumentDeserializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulDeserialization)
		{
			document.Close();
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000A3F90 File Offset: 0x000A2190
		private protected override void OnDeserializationEnd(object userContext, object operationContext, bool isSuccessfulDeserialization)
		{
			if (operationContext != null)
			{
				Tuple<ZipArchive, Stream> tuple = operationContext as Tuple<ZipArchive, Stream>;
				if (tuple != null)
				{
					tuple.Item1.Dispose();
					tuple.Item2.Close();
					return;
				}
			}
			throw new ArgumentException(TomSR.Exception_InvalidOpContextOnZipController, "operationContext");
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x000A3FD0 File Offset: 0x000A21D0
		private static string BuildLogicalPath(ZipArchiveEntry entry, string extension)
		{
			string text = ((string.Compare(Path.GetExtension(entry.Name), extension, StringComparison.InvariantCultureIgnoreCase) == 0) ? Path.GetFileNameWithoutExtension(entry.Name) : entry.Name);
			string text2 = ((string.Compare(entry.FullName, entry.Name, StringComparison.CurrentCultureIgnoreCase) == 0) ? null : entry.FullName.Remove(entry.FullName.Length - entry.Name.Length - 1).Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
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

		// Token: 0x040004B0 RID: 1200
		private readonly FileInfo zipFile;
	}
}
