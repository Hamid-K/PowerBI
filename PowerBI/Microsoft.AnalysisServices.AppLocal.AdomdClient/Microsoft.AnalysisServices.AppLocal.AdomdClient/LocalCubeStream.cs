using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200002D RID: 45
	internal sealed class LocalCubeStream : ClearTextXmlaStream
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000C83C File Offset: 0x0000AA3C
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

		// Token: 0x06000299 RID: 665 RVA: 0x0000C8CC File Offset: 0x0000AACC
		~LocalCubeStream()
		{
			this.Dispose(false);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000C8FC File Offset: 0x0000AAFC
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

		// Token: 0x0600029B RID: 667 RVA: 0x0000C94C File Offset: 0x0000AB4C
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

		// Token: 0x0600029C RID: 668 RVA: 0x0000CA28 File Offset: 0x0000AC28
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

		// Token: 0x0600029D RID: 669 RVA: 0x0000CA80 File Offset: 0x0000AC80
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

		// Token: 0x0600029E RID: 670 RVA: 0x0000CB34 File Offset: 0x0000AD34
		public override void Flush()
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000CB38 File Offset: 0x0000AD38
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

		// Token: 0x060002A0 RID: 672 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
		public override void Close()
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000CBA2 File Offset: 0x0000ADA2
		public override void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000CBAC File Offset: 0x0000ADAC
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

		// Token: 0x060002A3 RID: 675 RVA: 0x0000CC70 File Offset: 0x0000AE70
		private void ResetRequest()
		{
			if (this.hLocalRequest != IntPtr.Zero)
			{
				this.msmdlocalWraper.MSMDCloseHandle(this.hLocalRequest);
				this.hLocalRequest = IntPtr.Zero;
			}
		}

		// Token: 0x04000204 RID: 516
		private static byte[] bufferForSkip = new byte[1024];

		// Token: 0x04000205 RID: 517
		private string cubeFile;

		// Token: 0x04000206 RID: 518
		private IntPtr hLocalServer;

		// Token: 0x04000207 RID: 519
		private IntPtr hLocalRequest;

		// Token: 0x04000208 RID: 520
		private LocalCubeStream.MsmdlocalWrapper msmdlocalWraper;

		// Token: 0x02000189 RID: 393
		private sealed class MsmdlocalWrapper : LibraryHandle
		{
			// Token: 0x0600120C RID: 4620 RVA: 0x0003F294 File Offset: 0x0003D494
			private MsmdlocalWrapper()
			{
			}

			// Token: 0x1700065D RID: 1629
			// (get) Token: 0x0600120D RID: 4621 RVA: 0x0003F29C File Offset: 0x0003D49C
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

			// Token: 0x0600120E RID: 4622 RVA: 0x0003F318 File Offset: 0x0003D518
			public IntPtr MSMDOpenLocal(string pszPathToFile, LocalCubeStream.MsmdlocalWrapper.OpenFlags mskSettings, string pszPassword, string serverName)
			{
				return LibraryHandle.CheckEmptyHandle(this.msmdOpenLocalDelegate(pszPathToFile, (uint)mskSettings, pszPassword, serverName));
			}

			// Token: 0x0600120F RID: 4623 RVA: 0x0003F32F File Offset: 0x0003D52F
			public void MSMDCloseHandle(IntPtr hLocal)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdCloseHandleDelegate(hLocal));
			}

			// Token: 0x06001210 RID: 4624 RVA: 0x0003F342 File Offset: 0x0003D542
			public IntPtr MSMDOpenRequest(IntPtr hLocal, LocalCubeStream.MsmdlocalWrapper.MSMDLOCAL_REQUEST_ENCODING encoding, uint cTimeout)
			{
				return LibraryHandle.CheckEmptyHandle(this.msmdOpenRequestDelegate(hLocal, (int)encoding, cTimeout));
			}

			// Token: 0x06001211 RID: 4625 RVA: 0x0003F357 File Offset: 0x0003D557
			public void MSMDSendRequest(IntPtr hLocal)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdSendRequestDelegate(hLocal, false));
			}

			// Token: 0x06001212 RID: 4626 RVA: 0x0003F36B File Offset: 0x0003D56B
			public void MSMDWriteDataEx(IntPtr hLocal, byte[] buffer, int offset, int bytesAvailable, out int bytesWritten)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdWriteDataExDelegate(hLocal, buffer, offset, bytesAvailable, out bytesWritten));
			}

			// Token: 0x06001213 RID: 4627 RVA: 0x0003F384 File Offset: 0x0003D584
			public void MSMDReceiveResponse(IntPtr hLocal)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdReceiveResponseDelegate(hLocal));
			}

			// Token: 0x06001214 RID: 4628 RVA: 0x0003F397 File Offset: 0x0003D597
			public void MSMDReadDataEx(IntPtr hLocal, byte[] buffer, int offset, int bytes, out int bytesRead)
			{
				LocalCubeStream.MsmdlocalWrapper.CheckFalse(this.msmdReadDataExDelegate(hLocal, buffer, offset, bytes, out bytesRead));
			}

			// Token: 0x06001215 RID: 4629 RVA: 0x0003F3B0 File Offset: 0x0003D5B0
			private static void CheckFalse(bool result)
			{
				if (!result)
				{
					LibraryHandle.ThrowOnError();
				}
			}

			// Token: 0x06001216 RID: 4630
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
			private static extern LocalCubeStream.MsmdlocalWrapper LoadLibrary([MarshalAs(UnmanagedType.LPTStr)] [In] string fileName);

			// Token: 0x06001217 RID: 4631 RVA: 0x0003F3BC File Offset: 0x0003D5BC
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

			// Token: 0x04000C47 RID: 3143
			private const int S_OK = 0;

			// Token: 0x04000C48 RID: 3144
			private const int S_FALSE = 1;

			// Token: 0x04000C49 RID: 3145
			private static readonly object LockForCreatingWrapper = new object();

			// Token: 0x04000C4A RID: 3146
			private static LocalCubeStream.MsmdlocalWrapper msmdlocalWrapper = null;

			// Token: 0x04000C4B RID: 3147
			private LocalCubeStream.MsmdlocalWrapper.MSMDOpenLocalDelegate msmdOpenLocalDelegate;

			// Token: 0x04000C4C RID: 3148
			private LocalCubeStream.MsmdlocalWrapper.MSMDCloseHandleDelegate msmdCloseHandleDelegate;

			// Token: 0x04000C4D RID: 3149
			private LocalCubeStream.MsmdlocalWrapper.MSMDOpenRequestDelegate msmdOpenRequestDelegate;

			// Token: 0x04000C4E RID: 3150
			private LocalCubeStream.MsmdlocalWrapper.MSMDSendRequestDelegate msmdSendRequestDelegate;

			// Token: 0x04000C4F RID: 3151
			private LocalCubeStream.MsmdlocalWrapper.MSMDWriteDataExDelegate msmdWriteDataExDelegate;

			// Token: 0x04000C50 RID: 3152
			private LocalCubeStream.MsmdlocalWrapper.MSMDReceiveResponseDelegate msmdReceiveResponseDelegate;

			// Token: 0x04000C51 RID: 3153
			private LocalCubeStream.MsmdlocalWrapper.MSMDReadDataExDelegate msmdReadDataExDelegate;

			// Token: 0x04000C52 RID: 3154
			private LocalCubeStream.MsmdlocalWrapper.MSMDCanUnloadNowDelegate msmdCanUnloadNowDelegate;

			// Token: 0x02000236 RID: 566
			// (Invoke) Token: 0x06001588 RID: 5512
			private delegate IntPtr MSMDOpenLocalDelegate([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPathToFile, [MarshalAs(UnmanagedType.U4)] [In] uint mskSettings, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszPassword, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszServerName);

			// Token: 0x02000237 RID: 567
			// (Invoke) Token: 0x0600158C RID: 5516
			private delegate bool MSMDCloseHandleDelegate([In] IntPtr hLocal);

			// Token: 0x02000238 RID: 568
			// (Invoke) Token: 0x06001590 RID: 5520
			private delegate IntPtr MSMDOpenRequestDelegate([In] IntPtr hLocal, [MarshalAs(UnmanagedType.I4)] [In] int encoding, [MarshalAs(UnmanagedType.U4)] [In] uint cTimeout);

			// Token: 0x02000239 RID: 569
			// (Invoke) Token: 0x06001594 RID: 5524
			private delegate bool MSMDSendRequestDelegate([In] IntPtr hLocal, [In] bool binaryXml);

			// Token: 0x0200023A RID: 570
			// (Invoke) Token: 0x06001598 RID: 5528
			private delegate bool MSMDWriteDataExDelegate([In] IntPtr hLocal, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] buffer, [MarshalAs(UnmanagedType.U4)] [In] int iOffset, [MarshalAs(UnmanagedType.U4)] [In] int bytesAvailable, [MarshalAs(UnmanagedType.U4)] out int bytesWritten);

			// Token: 0x0200023B RID: 571
			// (Invoke) Token: 0x0600159C RID: 5532
			private delegate bool MSMDReceiveResponseDelegate([In] IntPtr hLocal);

			// Token: 0x0200023C RID: 572
			// (Invoke) Token: 0x060015A0 RID: 5536
			private delegate bool MSMDReadDataExDelegate([In] IntPtr hLocal, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] [Out] byte[] buffer, [MarshalAs(UnmanagedType.U4)] [In] int iOffset, [MarshalAs(UnmanagedType.U4)] [In] int bytes, [MarshalAs(UnmanagedType.U4)] out int bytesRead);

			// Token: 0x0200023D RID: 573
			// (Invoke) Token: 0x060015A4 RID: 5540
			private delegate int MSMDCanUnloadNowDelegate();

			// Token: 0x0200023E RID: 574
			[Flags]
			public enum OpenFlags : uint
			{
				// Token: 0x04000F6B RID: 3947
				OpenExisting = 1U,
				// Token: 0x04000F6C RID: 3948
				OpenOrCreate = 2U,
				// Token: 0x04000F6D RID: 3949
				CreateAlways = 4U,
				// Token: 0x04000F6E RID: 3950
				UseImbi = 16U
			}

			// Token: 0x0200023F RID: 575
			public enum MSMDLOCAL_REQUEST_ENCODING
			{
				// Token: 0x04000F70 RID: 3952
				MSMDLOCAL_REQUEST_DEFAULT,
				// Token: 0x04000F71 RID: 3953
				MSMDLOCAL_REQUEST_UTF16,
				// Token: 0x04000F72 RID: 3954
				MSMDLOCAL_REQUEST_UTF8,
				// Token: 0x04000F73 RID: 3955
				MSMDLOCAL_REQUEST_US_ASCII
			}
		}
	}
}
