using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000046 RID: 70
	internal sealed class LocalCubeStream : ClearTextXmlaStream
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000F760 File Offset: 0x0000D960
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

		// Token: 0x0600032F RID: 815 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		~LocalCubeStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000F820 File Offset: 0x0000DA20
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

		// Token: 0x06000331 RID: 817 RVA: 0x0000F870 File Offset: 0x0000DA70
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

		// Token: 0x06000332 RID: 818 RVA: 0x0000F94C File Offset: 0x0000DB4C
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

		// Token: 0x06000333 RID: 819 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
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

		// Token: 0x06000334 RID: 820 RVA: 0x0000FA58 File Offset: 0x0000DC58
		public override void Flush()
		{
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000FA5C File Offset: 0x0000DC5C
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

		// Token: 0x06000336 RID: 822 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
		public override void Close()
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000FAC6 File Offset: 0x0000DCC6
		public override void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
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

		// Token: 0x06000339 RID: 825 RVA: 0x0000FB94 File Offset: 0x0000DD94
		private void ResetRequest()
		{
			if (this.hLocalRequest != IntPtr.Zero)
			{
				this.msmdlocalWraper.MSMDCloseHandle(this.hLocalRequest);
				this.hLocalRequest = IntPtr.Zero;
			}
		}

		// Token: 0x04000249 RID: 585
		private static byte[] bufferForSkip = new byte[1024];

		// Token: 0x0400024A RID: 586
		private string cubeFile;

		// Token: 0x0400024B RID: 587
		private IntPtr hLocalServer;

		// Token: 0x0400024C RID: 588
		private IntPtr hLocalRequest;

		// Token: 0x0400024D RID: 589
		private LocalCubeStream.MsmdlocalWrapper msmdlocalWraper;

		// Token: 0x02000185 RID: 389
		private sealed class MsmdlocalWrapper : LibraryHandle
		{
			// Token: 0x060012AA RID: 4778 RVA: 0x000418D0 File Offset: 0x0003FAD0
			private MsmdlocalWrapper()
			{
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x060012AB RID: 4779 RVA: 0x000418D8 File Offset: 0x0003FAD8
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

			// Token: 0x060012AC RID: 4780 RVA: 0x00041954 File Offset: 0x0003FB54
			public IntPtr MSMDOpenLocal(string pszPathToFile, LocalCubeStream.MsmdlocalWrapper.OpenFlags mskSettings, string pszPassword, string serverName)
			{
				return LibraryHandle.CheckEmptyHandle(this.msmdOpenLocalDelegate(pszPathToFile, (uint)mskSettings, pszPassword, serverName));
			}

			// Token: 0x060012AD RID: 4781 RVA: 0x0004196B File Offset: 0x0003FB6B
			public void MSMDCloseHandle(IntPtr hLocal)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdCloseHandleDelegate(hLocal));
			}

			// Token: 0x060012AE RID: 4782 RVA: 0x0004197E File Offset: 0x0003FB7E
			public IntPtr MSMDOpenRequest(IntPtr hLocal, LocalCubeStream.MsmdlocalWrapper.MSMDLOCAL_REQUEST_ENCODING encoding, uint cTimeout)
			{
				return LibraryHandle.CheckEmptyHandle(this.msmdOpenRequestDelegate(hLocal, (int)encoding, cTimeout));
			}

			// Token: 0x060012AF RID: 4783 RVA: 0x00041993 File Offset: 0x0003FB93
			public void MSMDSendRequest(IntPtr hLocal)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdSendRequestDelegate(hLocal, false));
			}

			// Token: 0x060012B0 RID: 4784 RVA: 0x000419A7 File Offset: 0x0003FBA7
			public void MSMDWriteDataEx(IntPtr hLocal, byte[] buffer, int offset, int bytesAvailable, out int bytesWritten)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdWriteDataExDelegate(hLocal, buffer, offset, bytesAvailable, out bytesWritten));
			}

			// Token: 0x060012B1 RID: 4785 RVA: 0x000419C0 File Offset: 0x0003FBC0
			public void MSMDReceiveResponse(IntPtr hLocal)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdReceiveResponseDelegate(hLocal));
			}

			// Token: 0x060012B2 RID: 4786 RVA: 0x000419D3 File Offset: 0x0003FBD3
			public void MSMDReadDataEx(IntPtr hLocal, byte[] buffer, int offset, int bytes, out int bytesRead)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdReadDataExDelegate(hLocal, buffer, offset, bytes, out bytesRead));
			}

			// Token: 0x060012B3 RID: 4787 RVA: 0x000419EC File Offset: 0x0003FBEC
			private static void CheckFalse(bool result)
			{
				if (!result)
				{
					LibraryHandle.ThrowOnError();
				}
			}

			// Token: 0x060012B4 RID: 4788
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
			private static extern LocalCubeStream.MsmdlocalWrapper LoadLibrary([MarshalAs(UnmanagedType.LPTStr)] [In] string fileName);

			// Token: 0x060012B5 RID: 4789 RVA: 0x000419F8 File Offset: 0x0003FBF8
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

			// Token: 0x04000C01 RID: 3073
			private const int S_OK = 0;

			// Token: 0x04000C02 RID: 3074
			private const int S_FALSE = 1;

			// Token: 0x04000C03 RID: 3075
			private static readonly object LockForCreatingWrapper = new object();

			// Token: 0x04000C04 RID: 3076
			private static LocalCubeStream.MsmdlocalWrapper msmdlocalWrapper = null;

			// Token: 0x04000C05 RID: 3077
			private LocalCubeStream.MsmdlocalWrapper.MSMDOpenLocalDelegate msmdOpenLocalDelegate;

			// Token: 0x04000C06 RID: 3078
			private LocalCubeStream.MsmdlocalWrapper.MSMDCloseHandleDelegate msmdCloseHandleDelegate;

			// Token: 0x04000C07 RID: 3079
			private LocalCubeStream.MsmdlocalWrapper.MSMDOpenRequestDelegate msmdOpenRequestDelegate;

			// Token: 0x04000C08 RID: 3080
			private LocalCubeStream.MsmdlocalWrapper.MSMDSendRequestDelegate msmdSendRequestDelegate;

			// Token: 0x04000C09 RID: 3081
			private LocalCubeStream.MsmdlocalWrapper.MSMDWriteDataExDelegate msmdWriteDataExDelegate;

			// Token: 0x04000C0A RID: 3082
			private LocalCubeStream.MsmdlocalWrapper.MSMDReceiveResponseDelegate msmdReceiveResponseDelegate;

			// Token: 0x04000C0B RID: 3083
			private LocalCubeStream.MsmdlocalWrapper.MSMDReadDataExDelegate msmdReadDataExDelegate;

			// Token: 0x04000C0C RID: 3084
			private LocalCubeStream.MsmdlocalWrapper.MSMDCanUnloadNowDelegate msmdCanUnloadNowDelegate;

			// Token: 0x02000215 RID: 533
			// (Invoke) Token: 0x060014E2 RID: 5346
			private delegate IntPtr MSMDOpenLocalDelegate([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPathToFile, [MarshalAs(UnmanagedType.U4)] [In] uint mskSettings, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszPassword, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszServerName);

			// Token: 0x02000216 RID: 534
			// (Invoke) Token: 0x060014E6 RID: 5350
			private delegate bool MSMDCloseHandleDelegate([In] IntPtr hLocal);

			// Token: 0x02000217 RID: 535
			// (Invoke) Token: 0x060014EA RID: 5354
			private delegate IntPtr MSMDOpenRequestDelegate([In] IntPtr hLocal, [MarshalAs(UnmanagedType.I4)] [In] int encoding, [MarshalAs(UnmanagedType.U4)] [In] uint cTimeout);

			// Token: 0x02000218 RID: 536
			// (Invoke) Token: 0x060014EE RID: 5358
			private delegate bool MSMDSendRequestDelegate([In] IntPtr hLocal, [In] bool binaryXml);

			// Token: 0x02000219 RID: 537
			// (Invoke) Token: 0x060014F2 RID: 5362
			private delegate bool MSMDWriteDataExDelegate([In] IntPtr hLocal, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] buffer, [MarshalAs(UnmanagedType.U4)] [In] int iOffset, [MarshalAs(UnmanagedType.U4)] [In] int bytesAvailable, [MarshalAs(UnmanagedType.U4)] out int bytesWritten);

			// Token: 0x0200021A RID: 538
			// (Invoke) Token: 0x060014F6 RID: 5366
			private delegate bool MSMDReceiveResponseDelegate([In] IntPtr hLocal);

			// Token: 0x0200021B RID: 539
			// (Invoke) Token: 0x060014FA RID: 5370
			private delegate bool MSMDReadDataExDelegate([In] IntPtr hLocal, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] [Out] byte[] buffer, [MarshalAs(UnmanagedType.U4)] [In] int iOffset, [MarshalAs(UnmanagedType.U4)] [In] int bytes, [MarshalAs(UnmanagedType.U4)] out int bytesRead);

			// Token: 0x0200021C RID: 540
			// (Invoke) Token: 0x060014FE RID: 5374
			private delegate int MSMDCanUnloadNowDelegate();

			// Token: 0x0200021D RID: 541
			[Flags]
			public enum OpenFlags : uint
			{
				// Token: 0x0400121F RID: 4639
				OpenExisting = 1U,
				// Token: 0x04001220 RID: 4640
				OpenOrCreate = 2U,
				// Token: 0x04001221 RID: 4641
				CreateAlways = 4U,
				// Token: 0x04001222 RID: 4642
				UseImbi = 16U
			}

			// Token: 0x0200021E RID: 542
			public enum MSMDLOCAL_REQUEST_ENCODING
			{
				// Token: 0x04001224 RID: 4644
				MSMDLOCAL_REQUEST_DEFAULT,
				// Token: 0x04001225 RID: 4645
				MSMDLOCAL_REQUEST_UTF16,
				// Token: 0x04001226 RID: 4646
				MSMDLOCAL_REQUEST_UTF8,
				// Token: 0x04001227 RID: 4647
				MSMDLOCAL_REQUEST_US_ASCII
			}
		}
	}
}
