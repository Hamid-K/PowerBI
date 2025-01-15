using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000084 RID: 132
	internal class PartitionManager
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00016B5D File Offset: 0x00014D5D
		public static string SnapshotConversionFolder
		{
			[DebuggerStepThrough]
			get
			{
				return PartitionManager._SnapshotConversionFolder;
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00016B64 File Offset: 0x00014D64
		public PartitionManager(string[] folders)
		{
			PartitionManager <>4__this = this;
			RevertImpersonationContext.Run(delegate
			{
				foreach (string text in folders)
				{
					try
					{
						if (!Directory.Exists(text))
						{
							Directory.CreateDirectory(text);
						}
						if ((File.GetAttributes(text) & FileAttributes.Directory) == (FileAttributes)0)
						{
							throw new ServerConfigurationErrorException(null, "Path for directory is not a directory");
						}
						<>4__this.m_fileFolder = text;
						if (RSTrace.CatalogTrace.TraceInfo && !<>4__this.m_traceWriteOnce.TraceWritten(text))
						{
							RSTrace.CatalogTrace.Trace("Using folder {0} for temporary files.", new object[] { <>4__this.m_fileFolder });
						}
						break;
					}
					catch (Exception ex)
					{
						if (RSTrace.CatalogTrace.TraceError)
						{
							RSTrace.CatalogTrace.Trace("Error create temp folder: {0}", new object[] { ex.Message });
						}
					}
				}
			});
			if (this.m_fileFolder == null)
			{
				throw new ServerConfigurationErrorException(null, "Could not create the temp folder");
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00016BBC File Offset: 0x00014DBC
		public PartitionFileStream CreateFile(Stream data, bool deleteOnClose)
		{
			PartitionFileStream partitionFileStream = this.CreateFile(deleteOnClose);
			if (data != null)
			{
				data.Seek(0L, SeekOrigin.Begin);
				int num = 1024;
				byte[] array = new byte[num];
				int num2;
				while ((num2 = data.Read(array, 0, num)) > 0)
				{
					partitionFileStream.Write(array, 0, num2);
				}
			}
			return partitionFileStream;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00016C08 File Offset: 0x00014E08
		private static string GetNewFileName()
		{
			Guid guid = Guid.NewGuid();
			return PartitionManager._FileNamePrefix + guid.ToString();
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00016C32 File Offset: 0x00014E32
		public PartitionFileStream CreateFile(bool deleteOnClose)
		{
			return this.CreateFile(this.GetPath(null, PartitionManager.GetNewFileName()), deleteOnClose, false);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00016C48 File Offset: 0x00014E48
		public PartitionFileStream CreateFile(string folder, string filename, bool openExisting)
		{
			string path = this.GetFolder(folder);
			RevertImpersonationContext.Run(delegate
			{
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
			});
			return this.CreateFile(this.GetPath(folder, filename), false, openExisting);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00016C7C File Offset: 0x00014E7C
		public PartitionFileStream CreateFile(string fileName, bool openExisting)
		{
			return this.CreateFile(this.GetPath(null, fileName), false, openExisting);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00016C90 File Offset: 0x00014E90
		public PartitionFileStream CreateChunkFile(Guid chunkId, bool openExisting)
		{
			PartitionFileStream partitionFileStream = null;
			try
			{
				partitionFileStream = this.CreateFile(PartitionManager._ChunkFolderName, PartitionManager.ConvertGuidToFileName(chunkId, PartitionManager._ChunkFileExtension), openExisting);
			}
			catch (Exception ex)
			{
				if (RSTrace.ChunkTracer.TraceWarning)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Warning, "Failed to open file system chunk '{0}' (openExisting={1}): {2}", new object[] { chunkId, openExisting, ex.Message });
				}
			}
			return partitionFileStream;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00016D0C File Offset: 0x00014F0C
		public PartitionFileStream CopyChunkFile(Guid oldChunkId, Guid newChunkId)
		{
			RSTrace.CatalogTrace.Assert(oldChunkId != newChunkId, "oldChunkId != newChunkId");
			string oldPath = this.GetPath(PartitionManager._ChunkFolderName, PartitionManager.ConvertGuidToFileName(oldChunkId, PartitionManager._ChunkFileExtension));
			string text = PartitionManager.ConvertGuidToFileName(newChunkId, PartitionManager._ChunkFileExtension);
			string newPath = this.GetPath(PartitionManager._ChunkFolderName, text);
			PartitionFileStream partitionFileStream = null;
			try
			{
				bool copied = false;
				RevertImpersonationContext.Run(delegate
				{
					if (File.Exists(oldPath))
					{
						File.Copy(oldPath, newPath);
						copied = true;
					}
				});
				if (copied)
				{
					partitionFileStream = this.CreateFile(PartitionManager._ChunkFolderName, text, true);
				}
			}
			catch (Exception ex)
			{
				if (RSTrace.ChunkTracer.TraceWarning)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Warning, "Error trying to copy file chunk '{0}' to '{1}': ", new object[] { oldChunkId, newChunkId, ex.Message });
				}
			}
			return partitionFileStream;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00016E00 File Offset: 0x00015000
		private PartitionFileStream CreateFile(string path, bool deleteOnClose, bool openExisting)
		{
			FileMode mode = FileMode.CreateNew;
			if (openExisting)
			{
				mode = FileMode.Open;
			}
			PartitionFileStream partitionStream = null;
			RevertImpersonationContext.Run(delegate
			{
				FileStream fileStream = File.Open(path, mode, FileAccess.ReadWrite, FileShare.Read);
				partitionStream = new PartitionFileStream(fileStream, this, deleteOnClose);
			});
			return partitionStream;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00016E57 File Offset: 0x00015057
		public bool DeleteFile(PartitionFileStream stream)
		{
			return this.DeleteFileFromPartition(stream.FullFileName);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00016E65 File Offset: 0x00015065
		public bool DeleteFile(string folder, string fileName)
		{
			return this.DeleteFileFromPartition(this.GetPath(folder, fileName));
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00016E75 File Offset: 0x00015075
		public bool DeleteFile(string fileName)
		{
			return this.DeleteFileFromPartition(this.GetPath(null, fileName));
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00016E88 File Offset: 0x00015088
		public bool DeleteChunkFile(Guid chunkId)
		{
			string chunkPath = this.GetChunkPath(chunkId);
			return this.DeleteFileFromPartition(chunkPath);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00016EA4 File Offset: 0x000150A4
		public void DeleteMultipleChunkFiles(IEnumerable<Guid> chunkIds)
		{
			this.DeleteMultipleFilesFromPartition(Microsoft.ReportingServices.Common.EnumeratorMapping.Map<Guid, string>(chunkIds, new Converter<Guid, string>(this.GetChunkPath)));
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00016EBE File Offset: 0x000150BE
		private bool DeleteFileFromPartition(string path)
		{
			bool foundFile = false;
			RevertImpersonationContext.RunFromRestrictedCasContext(delegate
			{
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				if (File.Exists(path))
				{
					foundFile = true;
					File.Delete(path);
				}
			});
			return foundFile;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00016EE9 File Offset: 0x000150E9
		private void DeleteMultipleFilesFromPartition(IEnumerable<string> paths)
		{
			RevertImpersonationContext.Run(delegate
			{
				foreach (string text in paths)
				{
					File.Delete(text);
				}
			});
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00016F07 File Offset: 0x00015107
		private string GetChunkPath(Guid chunkId)
		{
			return this.GetPath(PartitionManager._ChunkFolderName, PartitionManager.ConvertGuidToFileName(chunkId, PartitionManager._ChunkFileExtension));
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00016F1F File Offset: 0x0001511F
		public void DeleteFolder(string folder)
		{
			RevertImpersonationContext.Run(delegate
			{
				if (Directory.Exists(this.GetPath(null, folder)))
				{
					Directory.Delete(this.GetPath(null, folder), true);
				}
			});
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00016F44 File Offset: 0x00015144
		public PartitionFileStream GetFile(string fileName)
		{
			return this.GetFileFromPartition(this.GetPath(null, fileName));
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00016F54 File Offset: 0x00015154
		public PartitionFileStream GetFile(string folder, string fileName)
		{
			return this.GetFileFromPartition(this.GetPath(folder, fileName));
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00016F64 File Offset: 0x00015164
		private PartitionFileStream GetFileFromPartition(string path)
		{
			PartitionFileStream stream = null;
			RevertImpersonationContext.Run(delegate
			{
				if (File.Exists(path))
				{
					stream = new PartitionFileStream(path, this, false);
				}
			});
			return stream;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00016F96 File Offset: 0x00015196
		internal string GetPath(string folder, string fileName)
		{
			RSTrace.CatalogTrace.Assert(fileName != null, "Calling Get Path incorrectly");
			return Path.Combine(this.GetFolder(folder), fileName);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00016FB8 File Offset: 0x000151B8
		public bool Exists(string folder, string fileName)
		{
			string path = this.GetPath(folder, fileName);
			bool result = false;
			RevertImpersonationContext.Run(delegate
			{
				result = File.Exists(path);
			});
			return result;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00016FEC File Offset: 0x000151EC
		private string GetFolder(string folder)
		{
			string text = this.m_fileFolder;
			if (folder != null)
			{
				text = Path.Combine(text, folder);
			}
			return text;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001700C File Offset: 0x0001520C
		public int CopyDirectory(string source, string target)
		{
			RSTrace.CatalogTrace.Assert(source != null, "source");
			RSTrace.CatalogTrace.Assert(target != null, "target");
			RSTrace.CatalogTrace.Assert(string.CompareOrdinal(source, target) != 0, "source != target");
			string sourcePath = this.GetFolder(source);
			string targetPath = this.GetFolder(target);
			int fileCount = 0;
			if (RSTrace.CatalogTrace.TraceVerbose)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Copying files from '{0}' to '{1}'", new object[] { sourcePath, targetPath });
			}
			RevertImpersonationContext.Run(delegate
			{
				if (!Directory.Exists(sourcePath))
				{
					return;
				}
				if (!Directory.Exists(targetPath))
				{
					Directory.CreateDirectory(targetPath);
				}
				foreach (string text in Directory.GetFiles(sourcePath, "*", SearchOption.TopDirectoryOnly))
				{
					string text2 = Path.Combine(targetPath, text);
					string text3 = Path.Combine(sourcePath, text);
					if (RSTrace.CatalogTrace.TraceVerbose)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Copying '{0} to '{1}'", new object[] { text3, text2 });
					}
					File.Copy(text3, text2);
					int fileCount2 = fileCount;
					fileCount = fileCount2 + 1;
				}
			});
			return fileCount;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000170CC File Offset: 0x000152CC
		public int PerformTimeBasedCleanup(TimeSpan expiration)
		{
			DateTime now = DateTime.Now;
			DateTime expirationDateTime = now - expiration;
			int cleanupCount = 0;
			RevertImpersonationContext.Run(delegate
			{
				cleanupCount += PartitionManager.DoSingleDirectoryCleanup(expirationDateTime, this.GetFolder(null), PartitionManager._FileNamePrefix + "*");
				cleanupCount += PartitionManager.DoSingleDirectoryCleanup(expirationDateTime, this.GetFolder(PartitionManager.SnapshotConversionFolder), "*");
			});
			return cleanupCount;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00017115 File Offset: 0x00015315
		public IEnumerable<Guid> GetFileChunkCleanupCandidates(TimeSpan expiration)
		{
			IEnumerable<FileInfo> cleanupCandidates = PartitionManager.GetCleanupCandidates(DateTime.Now - expiration, this.GetFolder(PartitionManager._ChunkFolderName), "*." + PartitionManager._ChunkFileExtension);
			foreach (FileInfo fileInfo in cleanupCandidates)
			{
				Guid guid = Guid.Empty;
				bool flag = false;
				try
				{
					guid = new Guid(Path.GetFileNameWithoutExtension(fileInfo.Name));
					flag = true;
				}
				catch (FormatException)
				{
				}
				catch (Exception ex)
				{
					if (!PartitionManager.HandleFileCleanupException(ex))
					{
						throw;
					}
				}
				if (flag)
				{
					yield return guid;
				}
			}
			IEnumerator<FileInfo> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001712C File Offset: 0x0001532C
		private static IEnumerable<FileInfo> GetCleanupCandidates(DateTime expirationDateTime, string cleanupPath, string searchString)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(cleanupPath);
			if (directoryInfo.Exists)
			{
				foreach (FileInfo fileInfo in directoryInfo.GetFiles(searchString))
				{
					DateTime dateTime = DateTime.MaxValue;
					try
					{
						dateTime = fileInfo.CreationTime;
					}
					catch (Exception ex)
					{
						if (!PartitionManager.HandleFileCleanupException(ex))
						{
							throw;
						}
					}
					if (dateTime < expirationDateTime)
					{
						yield return fileInfo;
					}
				}
				FileInfo[] array = null;
			}
			yield break;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001714C File Offset: 0x0001534C
		private static int DoSingleDirectoryCleanup(DateTime expirationDateTime, string cleanupPath, string searchString)
		{
			int num = 0;
			foreach (FileInfo fileInfo in PartitionManager.GetCleanupCandidates(expirationDateTime, cleanupPath, searchString))
			{
				try
				{
					fileInfo.Delete();
					num++;
				}
				catch (Exception ex)
				{
					if (!PartitionManager.HandleFileCleanupException(ex))
					{
						throw;
					}
				}
			}
			return num;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000171BC File Offset: 0x000153BC
		public static bool HandleFileCleanupException(Exception e)
		{
			if (ExceptionUtils.IsStoppingException(e))
			{
				return false;
			}
			if (e is IOException || e is SecurityException || e is UnauthorizedAccessException)
			{
				if (RSTrace.CleanupTracer.TraceVerbose)
				{
					RSTrace.CleanupTracer.TraceException(TraceLevel.Verbose, "Exception trying to clean up file, ex={0}", new object[] { e.ToString() });
				}
			}
			else if (RSTrace.CleanupTracer.TraceWarning)
			{
				RSTrace.CleanupTracer.TraceException(TraceLevel.Warning, "Exception trying to clean up file, ex={0}", new object[] { e.ToString() });
			}
			return true;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00017244 File Offset: 0x00015444
		public static string ConvertGuidToDirectoryName(Guid guid)
		{
			return guid.ToString();
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00017253 File Offset: 0x00015453
		public static string ConvertGuidToFileName(Guid guid, string extension)
		{
			return guid.ToString() + "." + extension;
		}

		// Token: 0x040002F1 RID: 753
		private static readonly string _FileNamePrefix = "RSFile_";

		// Token: 0x040002F2 RID: 754
		private static readonly string _ChunkFolderName = "Chunks";

		// Token: 0x040002F3 RID: 755
		private static readonly string _ChunkFileExtension = "chunk";

		// Token: 0x040002F4 RID: 756
		private static readonly string _SnapshotConversionFolder = "_Conversion";

		// Token: 0x040002F5 RID: 757
		private string m_fileFolder;

		// Token: 0x040002F6 RID: 758
		private RSTrace.WriteOnce m_traceWriteOnce = new RSTrace.WriteOnce();
	}
}
