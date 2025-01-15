using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200002D RID: 45
	internal sealed class LocalCubeStream : ClearTextXmlaStream
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000C50C File Offset: 0x0000A70C
		private LocalCubeStream(string cubeFile, LocalCubeStream.MsmdlocalWrapper.OpenFlags settings, int timeout, string password, string serverName)
			: base(true)
		{
			try
			{
				this.cubeFile = cubeFile;
				this.msmdlocalWraper = LocalCubeStream.MsmdlocalWrapper.LocalWrapper;
				this.hLocalServer = this.msmdlocalWraper.MSMDOpenLocal(cubeFile, settings, password, serverName);
			}
			catch (Win32Exception ex)
			{
				this.msmdlocalWraper = null;
				this.hLocalServer = IntPtr.Zero;
				throw new XmlaStreamException(XmlaSR.LocalCube_FileNotOpened(cubeFile), ex);
			}
			catch
			{
				this.msmdlocalWraper = null;
				this.hLocalServer = IntPtr.Zero;
				throw;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000C59C File Offset: 0x0000A79C
		~LocalCubeStream()
		{
			this.Dispose(false);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		public static LocalCubeStream Create(ConnectionInfo info)
		{
			string text;
			string text2;
			LocalCubeStream.MsmdlocalWrapper.OpenFlags openFlags;
			if (info.IsEmbedded)
			{
				text = null;
				text2 = info.Location;
				openFlags = LocalCubeStream.MsmdlocalWrapper.OpenFlags.OpenExisting | LocalCubeStream.MsmdlocalWrapper.OpenFlags.UseImbi;
			}
			else
			{
				text = info.Server;
				text2 = null;
				openFlags = (info.UseExistingFile ? LocalCubeStream.MsmdlocalWrapper.OpenFlags.OpenExisting : LocalCubeStream.MsmdlocalWrapper.OpenFlags.OpenOrCreate);
			}
			return new LocalCubeStream(text, openFlags, info.Timeout, info.EncryptionPassword, text2);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000C61C File Offset: 0x0000A81C
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size + offset > buffer.Length)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument, "buffer");
			}
			try
			{
				if (this.hLocalRequest == IntPtr.Zero)
				{
					this.hLocalRequest = this.msmdlocalWraper.MSMDOpenRequest(this.hLocalServer, LocalCubeStream.MsmdlocalWrapper.MSMDLOCAL_REQUEST_ENCODING.MSMDLOCAL_REQUEST_DEFAULT, 0U);
					this.msmdlocalWraper.MSMDSendRequest(this.hLocalRequest);
				}
				int num;
				for (int i = 0; i < size; i += num)
				{
					num = 0;
					this.msmdlocalWraper.MSMDWriteDataEx(this.hLocalRequest, buffer, offset + i, size, out num);
				}
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		public override void WriteEndOfMessage()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				if (this.hLocalRequest != IntPtr.Zero)
				{
					this.msmdlocalWraper.MSMDReceiveResponse(this.hLocalRequest);
				}
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C750 File Offset: 0x0000A950
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size + offset > buffer.Length)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument, "buffer");
			}
			int num;
			try
			{
				if (this.hLocalRequest == IntPtr.Zero)
				{
					num = 0;
				}
				else
				{
					int num2 = 0;
					this.msmdlocalWraper.MSMDReadDataEx(this.hLocalRequest, buffer, offset, size, out num2);
					if (num2 == 0)
					{
						this.ResetRequest();
					}
					num = num2;
				}
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
			return num;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C804 File Offset: 0x0000AA04
		public override void Flush()
		{
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000C808 File Offset: 0x0000AA08
		public override void Skip()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				if (!(this.hLocalRequest == IntPtr.Zero))
				{
					int num;
					do
					{
						num = this.Read(LocalCubeStream.bufferForSkip, 0, LocalCubeStream.bufferForSkip.Length);
					}
					while (num > 0);
					this.ResetRequest();
				}
			}
			catch (Win32Exception ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C870 File Offset: 0x0000AA70
		public override void Close()
		{
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C872 File Offset: 0x0000AA72
		public override void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C87C File Offset: 0x0000AA7C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.msmdlocalWraper != null)
				{
					if (this.hLocalRequest != IntPtr.Zero)
					{
						try
						{
							this.msmdlocalWraper.MSMDCloseHandle(this.hLocalRequest);
						}
						catch (Win32Exception)
						{
						}
						this.hLocalRequest = IntPtr.Zero;
					}
					if (this.hLocalServer != IntPtr.Zero)
					{
						try
						{
							this.msmdlocalWraper.MSMDCloseHandle(this.hLocalServer);
						}
						catch (Win32Exception)
						{
						}
						this.hLocalServer = IntPtr.Zero;
					}
					this.msmdlocalWraper = null;
				}
				this.disposed = true;
				if (disposing)
				{
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C940 File Offset: 0x0000AB40
		private void ResetRequest()
		{
			if (this.hLocalRequest != IntPtr.Zero)
			{
				this.msmdlocalWraper.MSMDCloseHandle(this.hLocalRequest);
				this.hLocalRequest = IntPtr.Zero;
			}
		}

		// Token: 0x040001F7 RID: 503
		private static byte[] bufferForSkip = new byte[1024];

		// Token: 0x040001F8 RID: 504
		private string cubeFile;

		// Token: 0x040001F9 RID: 505
		private IntPtr hLocalServer;

		// Token: 0x040001FA RID: 506
		private IntPtr hLocalRequest;

		// Token: 0x040001FB RID: 507
		private LocalCubeStream.MsmdlocalWrapper msmdlocalWraper;

		// Token: 0x02000189 RID: 393
		private sealed class MsmdlocalWrapper : LibraryHandle
		{
			// Token: 0x060011FF RID: 4607 RVA: 0x0003ED64 File Offset: 0x0003CF64
			private MsmdlocalWrapper()
			{
			}

			// Token: 0x17000657 RID: 1623
			// (get) Token: 0x06001200 RID: 4608 RVA: 0x0003ED6C File Offset: 0x0003CF6C
			public static LocalCubeStream.MsmdlocalWrapper LocalWrapper
			{
				get
				{
					object lockForCreatingWrapper = LocalCubeStream.MsmdlocalWrapper.LockForCreatingWrapper;
					LocalCubeStream.MsmdlocalWrapper msmdlocalWrapper;
					lock (lockForCreatingWrapper)
					{
						if (LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper == null || LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.IsInvalid)
						{
							string text = LocalExcelVar.MSMDLOCAL_PATH;
							if (!File.Exists(text))
							{
								text = LocalExcelVar.MSMDLOCAL_FALLBACK_PATH;
							}
							LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper = LocalCubeStream.MsmdlocalWrapper.LoadLibrary(text);
							LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.SetDelegates();
						}
						msmdlocalWrapper = LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper;
					}
					return msmdlocalWrapper;
				}
			}

			// Token: 0x06001201 RID: 4609 RVA: 0x0003EDE8 File Offset: 0x0003CFE8
			public IntPtr MSMDOpenLocal(string pszPathToFile, LocalCubeStream.MsmdlocalWrapper.OpenFlags mskSettings, string pszPassword, string serverName)
			{
				return LibraryHandle.CheckEmptyHandle(this.msmdOpenLocalDelegate(pszPathToFile, (uint)mskSettings, pszPassword, serverName));
			}

			// Token: 0x06001202 RID: 4610 RVA: 0x0003EDFF File Offset: 0x0003CFFF
			public void MSMDCloseHandle(IntPtr hLocal)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdCloseHandleDelegate(hLocal));
			}

			// Token: 0x06001203 RID: 4611 RVA: 0x0003EE12 File Offset: 0x0003D012
			public IntPtr MSMDOpenRequest(IntPtr hLocal, LocalCubeStream.MsmdlocalWrapper.MSMDLOCAL_REQUEST_ENCODING encoding, uint cTimeout)
			{
				return LibraryHandle.CheckEmptyHandle(this.msmdOpenRequestDelegate(hLocal, (int)encoding, cTimeout));
			}

			// Token: 0x06001204 RID: 4612 RVA: 0x0003EE27 File Offset: 0x0003D027
			public void MSMDSendRequest(IntPtr hLocal)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdSendRequestDelegate(hLocal, false));
			}

			// Token: 0x06001205 RID: 4613 RVA: 0x0003EE3B File Offset: 0x0003D03B
			public void MSMDWriteDataEx(IntPtr hLocal, byte[] buffer, int offset, int bytesAvailable, out int bytesWritten)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdWriteDataExDelegate(hLocal, buffer, offset, bytesAvailable, out bytesWritten));
			}

			// Token: 0x06001206 RID: 4614 RVA: 0x0003EE54 File Offset: 0x0003D054
			public void MSMDReceiveResponse(IntPtr hLocal)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdReceiveResponseDelegate(hLocal));
			}

			// Token: 0x06001207 RID: 4615 RVA: 0x0003EE67 File Offset: 0x0003D067
			public void MSMDReadDataEx(IntPtr hLocal, byte[] buffer, int offset, int bytes, out int bytesRead)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdReadDataExDelegate(hLocal, buffer, offset, bytes, out bytesRead));
			}

			// Token: 0x06001208 RID: 4616 RVA: 0x0003EE80 File Offset: 0x0003D080
			private static void CheckFalse(bool result)
			{
				if (!result)
				{
					LibraryHandle.ThrowOnError();
				}
			}

			// Token: 0x06001209 RID: 4617
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
			private static extern LocalCubeStream.MsmdlocalWrapper LoadLibrary([MarshalAs(UnmanagedType.LPTStr)] [In] string fileName);

			// Token: 0x0600120A RID: 4618 RVA: 0x0003EE8C File Offset: 0x0003D08C
			private void SetDelegates()
			{
				if (LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.IsInvalid)
				{
					throw new Win32Exception(Marshal.GetHRForLastWin32Error());
				}
				try
				{
					this.msmdOpenLocalDelegate = (LocalCubeStream.MsmdlocalWrapper.MSMDOpenLocalDelegate)LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.GetDelegate("MSMDOpenLocal", typeof(LocalCubeStream.MsmdlocalWrapper.MSMDOpenLocalDelegate));
					this.msmdCloseHandleDelegate = (LocalCubeStream.MsmdlocalWrapper.MSMDCloseHandleDelegate)LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.GetDelegate("MSMDCloseHandle", typeof(LocalCubeStream.MsmdlocalWrapper.MSMDCloseHandleDelegate));
					this.msmdOpenRequestDelegate = (LocalCubeStream.MsmdlocalWrapper.MSMDOpenRequestDelegate)LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.GetDelegate("MSMDOpenRequest", typeof(LocalCubeStream.MsmdlocalWrapper.MSMDOpenRequestDelegate));
					this.msmdSendRequestDelegate = (LocalCubeStream.MsmdlocalWrapper.MSMDSendRequestDelegate)LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.GetDelegate("MSMDSendRequest", typeof(LocalCubeStream.MsmdlocalWrapper.MSMDSendRequestDelegate));
					this.msmdWriteDataExDelegate = (LocalCubeStream.MsmdlocalWrapper.MSMDWriteDataExDelegate)LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.GetDelegate("MSMDWriteDataEx", typeof(LocalCubeStream.MsmdlocalWrapper.MSMDWriteDataExDelegate));
					this.msmdReceiveResponseDelegate = (LocalCubeStream.MsmdlocalWrapper.MSMDReceiveResponseDelegate)LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.GetDelegate("MSMDReceiveResponse", typeof(LocalCubeStream.MsmdlocalWrapper.MSMDReceiveResponseDelegate));
					this.msmdReadDataExDelegate = (LocalCubeStream.MsmdlocalWrapper.MSMDReadDataExDelegate)LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.GetDelegate("MSMDReadDataEx", typeof(LocalCubeStream.MsmdlocalWrapper.MSMDReadDataExDelegate));
					this.msmdCanUnloadNowDelegate = (LocalCubeStream.MsmdlocalWrapper.MSMDCanUnloadNowDelegate)LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.GetDelegate("MSMDCanUnloadNow", typeof(LocalCubeStream.MsmdlocalWrapper.MSMDCanUnloadNowDelegate));
				}
				catch
				{
					LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.Close();
					LocalCubeStream.MsmdlocalWrapper.msmdlocalWrapper.SetHandleAsInvalid();
					throw;
				}
			}

			// Token: 0x04000C36 RID: 3126
			private const int S_OK = 0;

			// Token: 0x04000C37 RID: 3127
			private const int S_FALSE = 1;

			// Token: 0x04000C38 RID: 3128
			private static readonly object LockForCreatingWrapper = new object();

			// Token: 0x04000C39 RID: 3129
			private static LocalCubeStream.MsmdlocalWrapper msmdlocalWrapper = null;

			// Token: 0x04000C3A RID: 3130
			private LocalCubeStream.MsmdlocalWrapper.MSMDOpenLocalDelegate msmdOpenLocalDelegate;

			// Token: 0x04000C3B RID: 3131
			private LocalCubeStream.MsmdlocalWrapper.MSMDCloseHandleDelegate msmdCloseHandleDelegate;

			// Token: 0x04000C3C RID: 3132
			private LocalCubeStream.MsmdlocalWrapper.MSMDOpenRequestDelegate msmdOpenRequestDelegate;

			// Token: 0x04000C3D RID: 3133
			private LocalCubeStream.MsmdlocalWrapper.MSMDSendRequestDelegate msmdSendRequestDelegate;

			// Token: 0x04000C3E RID: 3134
			private LocalCubeStream.MsmdlocalWrapper.MSMDWriteDataExDelegate msmdWriteDataExDelegate;

			// Token: 0x04000C3F RID: 3135
			private LocalCubeStream.MsmdlocalWrapper.MSMDReceiveResponseDelegate msmdReceiveResponseDelegate;

			// Token: 0x04000C40 RID: 3136
			private LocalCubeStream.MsmdlocalWrapper.MSMDReadDataExDelegate msmdReadDataExDelegate;

			// Token: 0x04000C41 RID: 3137
			private LocalCubeStream.MsmdlocalWrapper.MSMDCanUnloadNowDelegate msmdCanUnloadNowDelegate;

			// Token: 0x02000236 RID: 566
			// (Invoke) Token: 0x0600157A RID: 5498
			private delegate IntPtr MSMDOpenLocalDelegate([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPathToFile, [MarshalAs(UnmanagedType.U4)] [In] uint mskSettings, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszPassword, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszServerName);

			// Token: 0x02000237 RID: 567
			// (Invoke) Token: 0x0600157E RID: 5502
			private delegate bool MSMDCloseHandleDelegate([In] IntPtr hLocal);

			// Token: 0x02000238 RID: 568
			// (Invoke) Token: 0x06001582 RID: 5506
			private delegate IntPtr MSMDOpenRequestDelegate([In] IntPtr hLocal, [MarshalAs(UnmanagedType.I4)] [In] int encoding, [MarshalAs(UnmanagedType.U4)] [In] uint cTimeout);

			// Token: 0x02000239 RID: 569
			// (Invoke) Token: 0x06001586 RID: 5510
			private delegate bool MSMDSendRequestDelegate([In] IntPtr hLocal, [In] bool binaryXml);

			// Token: 0x0200023A RID: 570
			// (Invoke) Token: 0x0600158A RID: 5514
			private delegate bool MSMDWriteDataExDelegate([In] IntPtr hLocal, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] buffer, [MarshalAs(UnmanagedType.U4)] [In] int iOffset, [MarshalAs(UnmanagedType.U4)] [In] int bytesAvailable, [MarshalAs(UnmanagedType.U4)] out int bytesWritten);

			// Token: 0x0200023B RID: 571
			// (Invoke) Token: 0x0600158E RID: 5518
			private delegate bool MSMDReceiveResponseDelegate([In] IntPtr hLocal);

			// Token: 0x0200023C RID: 572
			// (Invoke) Token: 0x06001592 RID: 5522
			private delegate bool MSMDReadDataExDelegate([In] IntPtr hLocal, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] [Out] byte[] buffer, [MarshalAs(UnmanagedType.U4)] [In] int iOffset, [MarshalAs(UnmanagedType.U4)] [In] int bytes, [MarshalAs(UnmanagedType.U4)] out int bytesRead);

			// Token: 0x0200023D RID: 573
			// (Invoke) Token: 0x06001596 RID: 5526
			private delegate int MSMDCanUnloadNowDelegate();

			// Token: 0x0200023E RID: 574
			[Flags]
			public enum OpenFlags : uint
			{
				// Token: 0x04000F53 RID: 3923
				OpenExisting = 1U,
				// Token: 0x04000F54 RID: 3924
				OpenOrCreate = 2U,
				// Token: 0x04000F55 RID: 3925
				CreateAlways = 4U,
				// Token: 0x04000F56 RID: 3926
				UseImbi = 16U
			}

			// Token: 0x0200023F RID: 575
			public enum MSMDLOCAL_REQUEST_ENCODING
			{
				// Token: 0x04000F58 RID: 3928
				MSMDLOCAL_REQUEST_DEFAULT,
				// Token: 0x04000F59 RID: 3929
				MSMDLOCAL_REQUEST_UTF16,
				// Token: 0x04000F5A RID: 3930
				MSMDLOCAL_REQUEST_UTF8,
				// Token: 0x04000F5B RID: 3931
				MSMDLOCAL_REQUEST_US_ASCII
			}
		}
	}
}
