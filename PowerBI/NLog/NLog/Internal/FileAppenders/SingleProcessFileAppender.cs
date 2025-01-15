using System;
using System.IO;
using System.Security;
using NLog.Common;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000166 RID: 358
	[SecuritySafeCritical]
	internal class SingleProcessFileAppender : BaseFileAppender
	{
		// Token: 0x060010D0 RID: 4304 RVA: 0x0002BF4E File Offset: 0x0002A14E
		public SingleProcessFileAppender(string fileName, ICreateFileParameters parameters)
			: base(fileName, parameters)
		{
			this._file = base.CreateFileStream(false, 0);
			this._enableFileDeleteSimpleMonitor = parameters.EnableFileDeleteSimpleMonitor;
			this._lastSimpleMonitorCheckTimeUtc = base.OpenTimeUtc;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0002BF80 File Offset: 0x0002A180
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
			}
			this._file.Write(bytes, offset, count);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0002BFD8 File Offset: 0x0002A1D8
		public override void Flush()
		{
			if (this._file == null)
			{
				return;
			}
			this._file.Flush();
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0002BFF0 File Offset: 0x0002A1F0
		public override void Close()
		{
			if (this._file == null)
			{
				return;
			}
			InternalLogger.Trace<string>("Closing '{0}'", base.FileName);
			try
			{
				this._file.Close();
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Failed to close file '{0}'", new object[] { base.FileName });
				AsyncHelpers.WaitForDelay(TimeSpan.FromMilliseconds(1.0));
			}
			finally
			{
				this._file = null;
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0002C078 File Offset: 0x0002A278
		public override DateTime? GetFileCreationTimeUtc()
		{
			return new DateTime?(base.CreationTimeUtc);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0002C088 File Offset: 0x0002A288
		public override long? GetFileLength()
		{
			if (this._file == null)
			{
				return null;
			}
			return new long?(this._file.Length);
		}

		// Token: 0x0400048B RID: 1163
		public static readonly IFileAppenderFactory TheFactory = new SingleProcessFileAppender.Factory();

		// Token: 0x0400048C RID: 1164
		private FileStream _file;

		// Token: 0x0400048D RID: 1165
		private readonly bool _enableFileDeleteSimpleMonitor;

		// Token: 0x0400048E RID: 1166
		private DateTime _lastSimpleMonitorCheckTimeUtc;

		// Token: 0x02000298 RID: 664
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x060016D2 RID: 5842 RVA: 0x0003BEE7 File Offset: 0x0003A0E7
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new SingleProcessFileAppender(fileName, parameters);
			}
		}
	}
}
