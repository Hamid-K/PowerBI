using System;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Threading;
using NLog.Common;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000167 RID: 359
	[SecuritySafeCritical]
	internal class WindowsMultiProcessFileAppender : BaseMutexFileAppender
	{
		// Token: 0x060010D7 RID: 4311 RVA: 0x0002C0C4 File Offset: 0x0002A2C4
		public WindowsMultiProcessFileAppender(string fileName, ICreateFileParameters parameters)
			: base(fileName, parameters)
		{
			try
			{
				this.CreateAppendOnlyFile(fileName);
				this._fileCharacteristicsHelper = FileCharacteristicsHelper.CreateHelper(parameters.ForceManaged);
			}
			catch
			{
				if (this._fileStream != null)
				{
					this._fileStream.Dispose();
				}
				this._fileStream = null;
				throw;
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0002C120 File Offset: 0x0002A320
		private void CreateAppendOnlyFile(string fileName)
		{
			string directoryName = Path.GetDirectoryName(fileName);
			if (!Directory.Exists(directoryName))
			{
				if (!base.CreateFileParameters.CreateDirs)
				{
					throw new DirectoryNotFoundException(directoryName);
				}
				Directory.CreateDirectory(directoryName);
			}
			FileShare fileShare = FileShare.ReadWrite;
			if (base.CreateFileParameters.EnableFileDelete)
			{
				fileShare |= FileShare.Delete;
			}
			try
			{
				bool flag = File.Exists(fileName);
				this._fileStream = new FileStream(fileName, FileMode.Append, FileSystemRights.AppendData | FileSystemRights.Synchronize, fileShare, 1, FileOptions.None);
				long position = this._fileStream.Position;
				if (flag || position > 0L)
				{
					base.CreationTimeUtc = File.GetCreationTimeUtc(base.FileName);
					if (base.CreationTimeUtc < DateTime.UtcNow - TimeSpan.FromSeconds(2.0) && position == 0L)
					{
						Thread.Sleep(50);
						base.CreationTimeUtc = File.GetCreationTimeUtc(base.FileName);
					}
				}
				else
				{
					base.CreationTimeUtc = DateTime.UtcNow;
					File.SetCreationTimeUtc(base.FileName, base.CreationTimeUtc);
				}
			}
			catch
			{
				if (this._fileStream != null)
				{
					this._fileStream.Dispose();
				}
				this._fileStream = null;
				throw;
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0002C238 File Offset: 0x0002A438
		public override void Write(byte[] bytes, int offset, int count)
		{
			if (this._fileStream != null)
			{
				this._fileStream.Write(bytes, offset, count);
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0002C250 File Offset: 0x0002A450
		public override void Close()
		{
			if (this._fileStream == null)
			{
				return;
			}
			InternalLogger.Trace<string>("Closing '{0}'", base.FileName);
			try
			{
				FileStream fileStream = this._fileStream;
				if (fileStream != null)
				{
					fileStream.Dispose();
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Failed to close file '{0}'", new object[] { base.FileName });
				Thread.Sleep(1);
			}
			finally
			{
				this._fileStream = null;
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0002C2D0 File Offset: 0x0002A4D0
		public override void Flush()
		{
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0002C2D2 File Offset: 0x0002A4D2
		public override DateTime? GetFileCreationTimeUtc()
		{
			return new DateTime?(base.CreationTimeUtc);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0002C2E0 File Offset: 0x0002A4E0
		public override long? GetFileLength()
		{
			FileCharacteristics fileCharacteristics = this.GetFileCharacteristics();
			if (fileCharacteristics == null)
			{
				return null;
			}
			return new long?(fileCharacteristics.FileLength);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0002C30B File Offset: 0x0002A50B
		private FileCharacteristics GetFileCharacteristics()
		{
			return this._fileCharacteristicsHelper.GetFileCharacteristics(base.FileName, this._fileStream);
		}

		// Token: 0x0400048F RID: 1167
		public static readonly IFileAppenderFactory TheFactory = new WindowsMultiProcessFileAppender.Factory();

		// Token: 0x04000490 RID: 1168
		private FileStream _fileStream;

		// Token: 0x04000491 RID: 1169
		private readonly FileCharacteristicsHelper _fileCharacteristicsHelper;

		// Token: 0x02000299 RID: 665
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x060016D4 RID: 5844 RVA: 0x0003BEF8 File Offset: 0x0003A0F8
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new WindowsMultiProcessFileAppender(fileName, parameters);
			}
		}
	}
}
