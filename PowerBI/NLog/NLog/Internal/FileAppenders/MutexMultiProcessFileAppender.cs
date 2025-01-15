using System;
using System.IO;
using System.Security;
using System.Threading;
using NLog.Common;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000163 RID: 355
	[SecuritySafeCritical]
	internal class MutexMultiProcessFileAppender : BaseMutexFileAppender
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x0002BBF8 File Offset: 0x00029DF8
		public MutexMultiProcessFileAppender(string fileName, ICreateFileParameters parameters)
			: base(fileName, parameters)
		{
			try
			{
				this._mutex = base.CreateSharableMutex("FileLock");
				this._fileStream = base.CreateFileStream(true, 0);
				this._fileCharacteristicsHelper = FileCharacteristicsHelper.CreateHelper(parameters.ForceManaged);
			}
			catch
			{
				if (this._mutex != null)
				{
					this._mutex.Close();
					this._mutex = null;
				}
				if (this._fileStream != null)
				{
					this._fileStream.Close();
					this._fileStream = null;
				}
				throw;
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0002BC88 File Offset: 0x00029E88
		public override void Write(byte[] bytes, int offset, int count)
		{
			if (this._mutex == null || this._fileStream == null)
			{
				return;
			}
			try
			{
				this._mutex.WaitOne();
			}
			catch (AbandonedMutexException)
			{
			}
			try
			{
				this._fileStream.Seek(0L, SeekOrigin.End);
				this._fileStream.Write(bytes, offset, count);
				this._fileStream.Flush();
			}
			finally
			{
				this._mutex.ReleaseMutex();
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0002BD0C File Offset: 0x00029F0C
		public override void Close()
		{
			if (this._mutex == null && this._fileStream == null)
			{
				return;
			}
			InternalLogger.Trace<string>("Closing '{0}'", base.FileName);
			try
			{
				Mutex mutex = this._mutex;
				if (mutex != null)
				{
					mutex.Close();
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Failed to close mutex: '{0}'", new object[] { base.FileName });
			}
			finally
			{
				this._mutex = null;
			}
			try
			{
				FileStream fileStream = this._fileStream;
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			catch (Exception ex2)
			{
				InternalLogger.Warn(ex2, "Failed to close file: '{0}'", new object[] { base.FileName });
				AsyncHelpers.WaitForDelay(TimeSpan.FromMilliseconds(1.0));
			}
			finally
			{
				this._fileStream = null;
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0002BDF0 File Offset: 0x00029FF0
		public override void Flush()
		{
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0002BDF2 File Offset: 0x00029FF2
		public override DateTime? GetFileCreationTimeUtc()
		{
			return new DateTime?(base.CreationTimeUtc);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0002BDFF File Offset: 0x00029FFF
		public override long? GetFileLength()
		{
			return new long?(this.GetFileCharacteristics().FileLength);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0002BE11 File Offset: 0x0002A011
		private FileCharacteristics GetFileCharacteristics()
		{
			return this._fileCharacteristicsHelper.GetFileCharacteristics(base.FileName, this._fileStream);
		}

		// Token: 0x04000485 RID: 1157
		public static readonly IFileAppenderFactory TheFactory = new MutexMultiProcessFileAppender.Factory();

		// Token: 0x04000486 RID: 1158
		private FileStream _fileStream;

		// Token: 0x04000487 RID: 1159
		private readonly FileCharacteristicsHelper _fileCharacteristicsHelper;

		// Token: 0x04000488 RID: 1160
		private Mutex _mutex;

		// Token: 0x02000295 RID: 661
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x060016CC RID: 5836 RVA: 0x0003BEB4 File Offset: 0x0003A0B4
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new MutexMultiProcessFileAppender(fileName, parameters);
			}
		}
	}
}
