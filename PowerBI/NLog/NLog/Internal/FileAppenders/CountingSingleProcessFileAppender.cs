using System;
using System.IO;
using System.Security;
using NLog.Common;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x0200015F RID: 351
	[SecuritySafeCritical]
	internal class CountingSingleProcessFileAppender : BaseFileAppender
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x0002B110 File Offset: 0x00029310
		public CountingSingleProcessFileAppender(string fileName, ICreateFileParameters parameters)
			: base(fileName, parameters)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			this._currentFileLength = (fileInfo.Exists ? fileInfo.Length : 0L);
			this._file = base.CreateFileStream(false, 0);
			this._enableFileDeleteSimpleMonitor = parameters.EnableFileDeleteSimpleMonitor;
			this._lastSimpleMonitorCheckTimeUtc = base.OpenTimeUtc;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0002B16C File Offset: 0x0002936C
		public override void Close()
		{
			if (this._file != null)
			{
				InternalLogger.Trace<string>("Closing '{0}'", base.FileName);
				try
				{
					this._file.Close();
				}
				catch (Exception ex)
				{
					InternalLogger.Warn(ex, "Failed to close file: '{0}'", new object[] { base.FileName });
					AsyncHelpers.WaitForDelay(TimeSpan.FromMilliseconds(1.0));
				}
				finally
				{
					this._file = null;
				}
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0002B1F0 File Offset: 0x000293F0
		public override void Flush()
		{
			if (this._file == null)
			{
				return;
			}
			this._file.Flush();
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0002B206 File Offset: 0x00029406
		public override DateTime? GetFileCreationTimeUtc()
		{
			return new DateTime?(base.CreationTimeUtc);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0002B213 File Offset: 0x00029413
		public override long? GetFileLength()
		{
			return new long?(this._currentFileLength);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0002B220 File Offset: 0x00029420
		public override void Write(byte[] bytes, int offset, int count)
		{
			if (this._file == null)
			{
				return;
			}
			if (this._enableFileDeleteSimpleMonitor && BaseFileAppender.MonitorForEnableFileDeleteEvent(base.FileName, ref this._lastSimpleMonitorCheckTimeUtc))
			{
				this._file.Dispose();
				this._file = base.CreateFileStream(false, 0);
				this._currentFileLength = this._file.Length;
			}
			this._currentFileLength += (long)count;
			this._file.Write(bytes, offset, count);
		}

		// Token: 0x04000476 RID: 1142
		public static readonly IFileAppenderFactory TheFactory = new CountingSingleProcessFileAppender.Factory();

		// Token: 0x04000477 RID: 1143
		private FileStream _file;

		// Token: 0x04000478 RID: 1144
		private long _currentFileLength;

		// Token: 0x04000479 RID: 1145
		private readonly bool _enableFileDeleteSimpleMonitor;

		// Token: 0x0400047A RID: 1146
		private DateTime _lastSimpleMonitorCheckTimeUtc;

		// Token: 0x02000292 RID: 658
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x060016C2 RID: 5826 RVA: 0x0003BE55 File Offset: 0x0003A055
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new CountingSingleProcessFileAppender(fileName, parameters);
			}
		}
	}
}
