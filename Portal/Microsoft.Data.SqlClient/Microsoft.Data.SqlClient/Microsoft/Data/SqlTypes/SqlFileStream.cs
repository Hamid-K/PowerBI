using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Data.SqlTypes
{
	// Token: 0x02000017 RID: 23
	public sealed class SqlFileStream : Stream
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x0000B6BF File Offset: 0x000098BF
		public SqlFileStream(string path, byte[] transactionContext, FileAccess access)
			: this(path, transactionContext, access, FileOptions.None, 0L)
		{
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000B6D0 File Offset: 0x000098D0
		public SqlFileStream(string path, byte[] transactionContext, FileAccess access, FileOptions options, long allocationSize)
		{
			using (TryEventScope.Create(SqlClientEventSource.Log.TryScopeEnterEvent<int, int, int, string>("<sc.SqlFileStream.ctor|API> {0} access={1} options={2} path='{3}'", this.ObjectID, (int)access, (int)options, path)))
			{
				if (transactionContext == null)
				{
					throw ADP.ArgumentNull("transactionContext");
				}
				if (path == null)
				{
					throw ADP.ArgumentNull("path");
				}
				this.m_disposed = false;
				this.m_fs = null;
				this.OpenSqlFileStream(path, transactionContext, access, options, allocationSize);
				this.Name = path;
				this.TransactionContext = transactionContext;
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0000B774 File Offset: 0x00009974
		~SqlFileStream()
		{
			this.Dispose(false);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000B7A4 File Offset: 0x000099A4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.m_disposed)
				{
					try
					{
						if (disposing && this.m_fs != null)
						{
							this.m_fs.Close();
							this.m_fs = null;
						}
					}
					finally
					{
						this.m_disposed = true;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000B808 File Offset: 0x00009A08
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x0000B810 File Offset: 0x00009A10
		public string Name
		{
			get
			{
				return this.m_path;
			}
			private set
			{
				this.m_path = SqlFileStream.GetFullPathInternal(value);
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000B81E File Offset: 0x00009A1E
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x0000B83A File Offset: 0x00009A3A
		public byte[] TransactionContext
		{
			get
			{
				if (this.m_txn == null)
				{
					return null;
				}
				return (byte[])this.m_txn.Clone();
			}
			private set
			{
				this.m_txn = (byte[])value.Clone();
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0000B84D File Offset: 0x00009A4D
		public override bool CanRead
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.CanRead;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0000B869 File Offset: 0x00009A69
		public override bool CanSeek
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.CanSeek;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0000B885 File Offset: 0x00009A85
		[ComVisible(false)]
		public override bool CanTimeout
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.CanTimeout;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0000B8A1 File Offset: 0x00009AA1
		public override bool CanWrite
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.CanWrite;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0000B8BD File Offset: 0x00009ABD
		public override long Length
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.Length;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0000B8D9 File Offset: 0x00009AD9
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0000B8F5 File Offset: 0x00009AF5
		public override long Position
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.Position;
			}
			set
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				this.m_fs.Position = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0000B912 File Offset: 0x00009B12
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x0000B92E File Offset: 0x00009B2E
		[ComVisible(false)]
		public override int ReadTimeout
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.ReadTimeout;
			}
			set
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				this.m_fs.ReadTimeout = value;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0000B94B File Offset: 0x00009B4B
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x0000B967 File Offset: 0x00009B67
		[ComVisible(false)]
		public override int WriteTimeout
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.WriteTimeout;
			}
			set
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				this.m_fs.WriteTimeout = value;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000B984 File Offset: 0x00009B84
		public override void Flush()
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.Flush();
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000B9C3 File Offset: 0x00009BC3
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.EndRead(asyncResult);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000B9E0 File Offset: 0x00009BE0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			IAsyncResult asyncResult = this.m_fs.BeginWrite(buffer, offset, count, callback, state);
			if (count == 1)
			{
				this.m_fs.Flush();
			}
			return asyncResult;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000BA1F File Offset: 0x00009C1F
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.EndWrite(asyncResult);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000BA3C File Offset: 0x00009C3C
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.Seek(offset, origin);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000BA5A File Offset: 0x00009C5A
		public override void SetLength(long value)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.SetLength(value);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0000BA77 File Offset: 0x00009C77
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.Read(buffer, offset, count);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000BA96 File Offset: 0x00009C96
		public override int ReadByte()
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.ReadByte();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000BAB2 File Offset: 0x00009CB2
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.Write(buffer, offset, count);
			if (count == 1)
			{
				this.m_fs.Flush();
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		public override void WriteByte(byte value)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.WriteByte(value);
			this.m_fs.Flush();
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0000BB08 File Offset: 0x00009D08
		[Conditional("DEBUG")]
		private static void AssertPathFormat(string path)
		{
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0000BB0C File Offset: 0x00009D0C
		private static string GetFullPathInternal(string path)
		{
			path = path.Trim();
			if (path.Length == 0)
			{
				throw ADP.Argument(StringsHelper.GetString(Strings.SqlFileStream_InvalidPath, Array.Empty<object>()), "path");
			}
			if (path.Length > 32766)
			{
				throw ADP.Argument(StringsHelper.GetString(Strings.SqlFileStream_InvalidPath, Array.Empty<object>()), "path");
			}
			if (path.IndexOfAny(SqlFileStream.InvalidPathChars) >= 0)
			{
				throw ADP.Argument(StringsHelper.GetString(Strings.SqlFileStream_InvalidPath, Array.Empty<object>()), "path");
			}
			if (!path.StartsWith("\\\\", StringComparison.OrdinalIgnoreCase))
			{
				throw ADP.Argument(StringsHelper.GetString(Strings.SqlFileStream_InvalidPath, Array.Empty<object>()), "path");
			}
			path = UnsafeNativeMethods.SafeGetFullPathName(path);
			if (path.StartsWith("\\\\.\\", StringComparison.Ordinal))
			{
				throw ADP.Argument(StringsHelper.GetString(Strings.SqlFileStream_PathNotValidDiskResource, Array.Empty<object>()), "path");
			}
			return path;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000BBEC File Offset: 0x00009DEC
		private static void DemandAccessPermission(string path, FileAccess access)
		{
			FileIOPermissionAccess fileIOPermissionAccess;
			switch (access)
			{
			case FileAccess.Read:
				fileIOPermissionAccess = FileIOPermissionAccess.Read;
				goto IL_0020;
			case FileAccess.Write:
				fileIOPermissionAccess = FileIOPermissionAccess.Write;
				goto IL_0020;
			}
			fileIOPermissionAccess = FileIOPermissionAccess.Read | FileIOPermissionAccess.Write;
			IL_0020:
			bool flag = false;
			try
			{
				FileIOPermission fileIOPermission = new FileIOPermission(fileIOPermissionAccess, path);
				fileIOPermission.Demand();
			}
			catch (PathTooLongException ex)
			{
				flag = true;
				ADP.TraceExceptionWithoutRethrow(ex);
			}
			if (flag)
			{
				new FileIOPermission(PermissionState.Unrestricted)
				{
					AllFiles = fileIOPermissionAccess
				}.Demand();
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000BC60 File Offset: 0x00009E60
		private void OpenSqlFileStream(string path, byte[] transactionContext, FileAccess access, FileOptions options, long allocationSize)
		{
			if (access != FileAccess.Read && access != FileAccess.Write && access != FileAccess.ReadWrite)
			{
				throw ADP.ArgumentOutOfRange("access");
			}
			if ((options & (FileOptions)671088639) != FileOptions.None)
			{
				throw ADP.ArgumentOutOfRange("options");
			}
			path = SqlFileStream.GetFullPathInternal(path);
			SqlFileStream.DemandAccessPermission(path, access);
			FileFullEaInformation fileFullEaInformation = null;
			SecurityQualityOfService securityQualityOfService = null;
			UnicodeString unicodeString = null;
			SafeFileHandle safeFileHandle = null;
			int num = 1048704;
			uint num2 = 0U;
			FileShare fileShare;
			uint num3;
			switch (access)
			{
			case FileAccess.Read:
				num |= 1;
				fileShare = FileShare.Read | FileShare.Write | FileShare.Delete;
				num3 = 1U;
				goto IL_0091;
			case FileAccess.Write:
				num |= 2;
				fileShare = FileShare.Read | FileShare.Delete;
				num3 = 4U;
				goto IL_0091;
			}
			num |= 3;
			fileShare = FileShare.Read | FileShare.Delete;
			num3 = 4U;
			IL_0091:
			if ((options & FileOptions.WriteThrough) != FileOptions.None)
			{
				num2 |= 2U;
			}
			if ((options & FileOptions.Asynchronous) == FileOptions.None)
			{
				num2 |= 32U;
			}
			if ((options & FileOptions.SequentialScan) != FileOptions.None)
			{
				num2 |= 4U;
			}
			if ((options & FileOptions.RandomAccess) != FileOptions.None)
			{
				num2 |= 2048U;
			}
			try
			{
				fileFullEaInformation = new FileFullEaInformation(transactionContext);
				securityQualityOfService = new SecurityQualityOfService(UnsafeNativeMethods.SecurityImpersonationLevel.SecurityAnonymous, false, false);
				string text = SqlFileStream.InitializeNtPath(path);
				unicodeString = new UnicodeString(text);
				UnsafeNativeMethods.OBJECT_ATTRIBUTES object_ATTRIBUTES;
				object_ATTRIBUTES.length = Marshal.SizeOf(typeof(UnsafeNativeMethods.OBJECT_ATTRIBUTES));
				object_ATTRIBUTES.rootDirectory = IntPtr.Zero;
				object_ATTRIBUTES.attributes = 64;
				object_ATTRIBUTES.securityDescriptor = IntPtr.Zero;
				object_ATTRIBUTES.securityQualityOfService = securityQualityOfService;
				object_ATTRIBUTES.objectName = unicodeString;
				uint num4 = 0U;
				uint num5;
				UnsafeNativeMethods.SetErrorModeWrapper(1U, out num5);
				try
				{
					SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int, long, int, int, uint, uint>("<sc.SqlFileStream.OpenSqlFileStream|ADV> {0}, desiredAccess=0x{1}, allocationSize={2}, fileAttributes=0x{3}, shareAccess=0x{4}, dwCreateDisposition=0x{5}, createOptions=0x{6}", this.ObjectID, num, allocationSize, 0, (int)fileShare, num3, num2);
					UnsafeNativeMethods.IO_STATUS_BLOCK io_STATUS_BLOCK;
					num4 = UnsafeNativeMethods.NtCreateFile(out safeFileHandle, num, ref object_ATTRIBUTES, out io_STATUS_BLOCK, ref allocationSize, 0U, fileShare, num3, num2, fileFullEaInformation, (uint)fileFullEaInformation.Length);
				}
				finally
				{
					UnsafeNativeMethods.SetErrorModeWrapper(num5, out num5);
				}
				if (num4 <= 3221225485U)
				{
					if (num4 != 0U)
					{
						if (num4 == 3221225485U)
						{
							throw ADP.Argument(StringsHelper.GetString(Strings.SqlFileStream_InvalidParameter, Array.Empty<object>()));
						}
					}
					else
					{
						if (safeFileHandle.IsInvalid)
						{
							Win32Exception ex = new Win32Exception(6);
							ADP.TraceExceptionAsReturnValue(ex);
							throw ex;
						}
						UnsafeNativeMethods.FileType fileType = UnsafeNativeMethods.GetFileType(safeFileHandle);
						if (fileType != UnsafeNativeMethods.FileType.Disk)
						{
							safeFileHandle.Dispose();
							throw ADP.Argument(StringsHelper.GetString(Strings.SqlFileStream_PathNotValidDiskResource, Array.Empty<object>()));
						}
						if (access == FileAccess.ReadWrite)
						{
							uint num6 = UnsafeNativeMethods.CTL_CODE(9, 2392, 0, 0);
							uint num7 = 0U;
							if (!UnsafeNativeMethods.DeviceIoControl(safeFileHandle, num6, IntPtr.Zero, 0U, IntPtr.Zero, 0U, out num7, IntPtr.Zero))
							{
								Win32Exception ex2 = new Win32Exception(Marshal.GetLastWin32Error());
								ADP.TraceExceptionAsReturnValue(ex2);
								throw ex2;
							}
						}
						bool flag = false;
						try
						{
							SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
							securityPermission.Assert();
							flag = true;
							this.m_fs = new FileStream(safeFileHandle, access, 1, (options & FileOptions.Asynchronous) > FileOptions.None);
						}
						finally
						{
							if (flag)
							{
								CodeAccessPermission.RevertAssert();
							}
						}
						return;
					}
				}
				else
				{
					if (num4 == 3221225524U)
					{
						DirectoryNotFoundException ex3 = new DirectoryNotFoundException();
						ADP.TraceExceptionAsReturnValue(ex3);
						throw ex3;
					}
					if (num4 == 3221225539U)
					{
						throw ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlFileStream_FileAlreadyInTransaction, Array.Empty<object>()));
					}
				}
				uint num8 = UnsafeNativeMethods.RtlNtStatusToDosError(num4);
				if (num8 == 317U)
				{
					num8 = num4;
				}
				Win32Exception ex4 = new Win32Exception((int)num8);
				ADP.TraceExceptionAsReturnValue(ex4);
				throw ex4;
			}
			catch
			{
				if (safeFileHandle != null && !safeFileHandle.IsInvalid)
				{
					safeFileHandle.Dispose();
				}
				throw;
			}
			finally
			{
				if (fileFullEaInformation != null)
				{
					fileFullEaInformation.Dispose();
					fileFullEaInformation = null;
				}
				if (securityQualityOfService != null)
				{
					securityQualityOfService.Dispose();
					securityQualityOfService = null;
				}
				if (unicodeString != null)
				{
					unicodeString.Dispose();
					unicodeString = null;
				}
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000BFF4 File Offset: 0x0000A1F4
		private static string InitializeNtPath(string path)
		{
			string text = "\\??\\UNC\\{0}\\{1}";
			string text2 = Guid.NewGuid().ToString("N");
			return string.Format(CultureInfo.InvariantCulture, text, path.Trim(new char[] { '\\' }), text2);
		}

		// Token: 0x0400003A RID: 58
		private static int _objectTypeCount;

		// Token: 0x0400003B RID: 59
		internal readonly int ObjectID = Interlocked.Increment(ref SqlFileStream._objectTypeCount);

		// Token: 0x0400003C RID: 60
		internal const int DefaultBufferSize = 1;

		// Token: 0x0400003D RID: 61
		private const ushort IoControlCodeFunctionCode = 2392;

		// Token: 0x0400003E RID: 62
		private FileStream m_fs;

		// Token: 0x0400003F RID: 63
		private string m_path;

		// Token: 0x04000040 RID: 64
		private byte[] m_txn;

		// Token: 0x04000041 RID: 65
		private bool m_disposed;

		// Token: 0x04000042 RID: 66
		private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();

		// Token: 0x04000043 RID: 67
		private const int MaxWin32PathLength = 32766;
	}
}
