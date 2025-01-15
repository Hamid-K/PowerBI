using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;
using NLog.Common;
using NLog.Time;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x0200015D RID: 349
	[SecuritySafeCritical]
	internal abstract class BaseFileAppender : IDisposable
	{
		// Token: 0x0600106C RID: 4204 RVA: 0x0002AAA6 File Offset: 0x00028CA6
		protected BaseFileAppender(string fileName, ICreateFileParameters createParameters)
		{
			this.CreateFileParameters = createParameters;
			this.FileName = fileName;
			this.OpenTimeUtc = DateTime.UtcNow;
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x0002AAD2 File Offset: 0x00028CD2
		public string FileName { get; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0002AADA File Offset: 0x00028CDA
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x0002AAE2 File Offset: 0x00028CE2
		public DateTime CreationTimeUtc
		{
			get
			{
				return this._creationTimeUtc;
			}
			internal set
			{
				this._creationTimeUtc = value;
				this.CreationTimeSource = TimeSource.Current.FromSystemTime(value);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0002AAFC File Offset: 0x00028CFC
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0002AB04 File Offset: 0x00028D04
		public DateTime CreationTimeSource { get; private set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0002AB0D File Offset: 0x00028D0D
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x0002AB15 File Offset: 0x00028D15
		public DateTime OpenTimeUtc { get; private set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0002AB1E File Offset: 0x00028D1E
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x0002AB26 File Offset: 0x00028D26
		public ICreateFileParameters CreateFileParameters { get; private set; }

		// Token: 0x06001076 RID: 4214 RVA: 0x0002AB2F File Offset: 0x00028D2F
		public void Write(byte[] bytes)
		{
			this.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06001077 RID: 4215
		public abstract void Write(byte[] bytes, int offset, int count);

		// Token: 0x06001078 RID: 4216
		public abstract void Flush();

		// Token: 0x06001079 RID: 4217
		public abstract void Close();

		// Token: 0x0600107A RID: 4218
		public abstract DateTime? GetFileCreationTimeUtc();

		// Token: 0x0600107B RID: 4219
		public abstract long? GetFileLength();

		// Token: 0x0600107C RID: 4220 RVA: 0x0002AB3C File Offset: 0x00028D3C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0002AB4B File Offset: 0x00028D4B
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0002AB58 File Offset: 0x00028D58
		protected FileStream CreateFileStream(bool allowFileSharedWriting, int overrideBufferSize = 0)
		{
			int num = this.CreateFileParameters.ConcurrentWriteAttemptDelay;
			InternalLogger.Trace<string, bool>("Opening {0} with allowFileSharedWriting={1}", this.FileName, allowFileSharedWriting);
			for (int i = 0; i < this.CreateFileParameters.ConcurrentWriteAttempts; i++)
			{
				try
				{
					try
					{
						return this.TryCreateFileStream(allowFileSharedWriting, overrideBufferSize);
					}
					catch (DirectoryNotFoundException)
					{
						if (!this.CreateFileParameters.CreateDirs)
						{
							throw;
						}
						string directoryName = Path.GetDirectoryName(this.FileName);
						try
						{
							Directory.CreateDirectory(directoryName);
						}
						catch (DirectoryNotFoundException)
						{
							throw new NLogRuntimeException("Could not create directory {0}", new object[] { directoryName });
						}
						return this.TryCreateFileStream(allowFileSharedWriting, overrideBufferSize);
					}
				}
				catch (IOException)
				{
					if (!this.CreateFileParameters.ConcurrentWrites || i + 1 == this.CreateFileParameters.ConcurrentWriteAttempts)
					{
						throw;
					}
					int num2 = this._random.Next(num);
					InternalLogger.Warn<int, string, int>("Attempt #{0} to open {1} failed. Sleeping for {2}ms", i, this.FileName, num2);
					num *= 2;
					AsyncHelpers.WaitForDelay(TimeSpan.FromMilliseconds((double)num2));
				}
			}
			throw new InvalidOperationException("Should not be reached.");
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0002AC7C File Offset: 0x00028E7C
		private FileStream WindowsCreateFile(string fileName, bool allowFileSharedWriting, int overrideBufferSize)
		{
			int num = 1;
			if (allowFileSharedWriting)
			{
				num |= 2;
			}
			if (this.CreateFileParameters.EnableFileDelete && PlatformDetector.CurrentOS != RuntimeOS.Windows)
			{
				num |= 4;
			}
			SafeFileHandle safeFileHandle = null;
			FileStream fileStream = null;
			FileStream fileStream2;
			try
			{
				safeFileHandle = Win32FileNativeMethods.CreateFile(fileName, Win32FileNativeMethods.FileAccess.GenericWrite, num, IntPtr.Zero, Win32FileNativeMethods.CreationDisposition.OpenAlways, this.CreateFileParameters.FileAttributes, IntPtr.Zero);
				if (safeFileHandle.IsInvalid)
				{
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
				fileStream = new FileStream(safeFileHandle, FileAccess.Write, (overrideBufferSize > 0) ? overrideBufferSize : this.CreateFileParameters.BufferSize);
				fileStream.Seek(0L, SeekOrigin.End);
				fileStream2 = fileStream;
			}
			catch
			{
				if (fileStream != null)
				{
					fileStream.Dispose();
				}
				if (safeFileHandle != null && !safeFileHandle.IsClosed)
				{
					safeFileHandle.Close();
				}
				throw;
			}
			return fileStream2;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0002AD3C File Offset: 0x00028F3C
		private FileStream TryCreateFileStream(bool allowFileSharedWriting, int overrideBufferSize)
		{
			this.UpdateCreationTime();
			try
			{
				if (!this.CreateFileParameters.ForceManaged && PlatformDetector.IsWin32 && !PlatformDetector.IsMono)
				{
					return this.WindowsCreateFile(this.FileName, allowFileSharedWriting, overrideBufferSize);
				}
			}
			catch (SecurityException)
			{
				InternalLogger.Debug<string>("Could not use native Windows create file, falling back to managed filestream: {0}", this.FileName);
			}
			FileShare fileShare = (allowFileSharedWriting ? FileShare.ReadWrite : FileShare.Read);
			if (this.CreateFileParameters.EnableFileDelete)
			{
				fileShare |= FileShare.Delete;
			}
			return new FileStream(this.FileName, FileMode.Append, FileAccess.Write, fileShare, (overrideBufferSize > 0) ? overrideBufferSize : this.CreateFileParameters.BufferSize);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0002ADDC File Offset: 0x00028FDC
		private void UpdateCreationTime()
		{
			FileInfo fileInfo = new FileInfo(this.FileName);
			if (fileInfo.Exists)
			{
				this.CreationTimeUtc = FileCharacteristicsHelper.ValidateFileCreationTime<FileInfo>(fileInfo, (FileInfo f) => new DateTime?(f.GetCreationTimeUtc()), (FileInfo f) => new DateTime?(f.GetLastWriteTimeUtc()), null).Value;
				return;
			}
			File.Create(this.FileName).Dispose();
			this.CreationTimeUtc = DateTime.UtcNow;
			File.SetCreationTimeUtc(this.FileName, this.CreationTimeUtc);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0002AE80 File Offset: 0x00029080
		protected static bool MonitorForEnableFileDeleteEvent(string fileName, ref DateTime lastSimpleMonitorCheckTimeUtc)
		{
			long num = DateTime.UtcNow.Ticks - lastSimpleMonitorCheckTimeUtc.Ticks;
			if (num > 10000000L || num < -10000000L)
			{
				lastSimpleMonitorCheckTimeUtc = DateTime.UtcNow;
				try
				{
					if (!File.Exists(fileName))
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Failed to check if File.Exists {0}", new object[] { fileName });
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x0400046F RID: 1135
		private readonly Random _random = new Random();

		// Token: 0x04000471 RID: 1137
		private DateTime _creationTimeUtc;
	}
}
