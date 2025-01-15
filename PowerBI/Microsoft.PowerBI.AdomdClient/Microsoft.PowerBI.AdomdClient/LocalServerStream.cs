using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200002E RID: 46
	internal sealed class LocalServerStream : ClearTextXmlaStream
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000C981 File Offset: 0x0000AB81
		public LocalServerStream()
			: base(true)
		{
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C98A File Offset: 0x0000AB8A
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamWrite(buffer, offset, size);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000C9A3 File Offset: 0x0000ABA3
		public override void WriteEndOfMessage()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamWriteEndOfMessage();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C9B9 File Offset: 0x0000ABB9
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			return LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamRead(buffer, offset, size);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C9D2 File Offset: 0x0000ABD2
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamFlush();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		public override void Skip()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamSkip();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C9FE File Offset: 0x0000ABFE
		public override void Close()
		{
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamClose();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000CA08 File Offset: 0x0000AC08
		public override void Dispose()
		{
			try
			{
				LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamCloseBase();
				this.disposed = true;
			}
			finally
			{
				base.Dispose(true);
			}
		}

		// Token: 0x0200018A RID: 394
		private static class LocalServerNativeMethods
		{
			// Token: 0x0600120C RID: 4620
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamClose();

			// Token: 0x0600120D RID: 4621
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamCloseBase();

			// Token: 0x0600120E RID: 4622
			[DllImport("msmgdsrv.dll")]
			public static extern int MSMDLocalStreamRead([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] [Out] byte[] buffer, [MarshalAs(UnmanagedType.I4)] [In] int offset, [MarshalAs(UnmanagedType.I4)] [In] int size);

			// Token: 0x0600120F RID: 4623
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamWrite([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] buffer, [MarshalAs(UnmanagedType.I4)] [In] int offset, [MarshalAs(UnmanagedType.I4)] [In] int size);

			// Token: 0x06001210 RID: 4624
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamWriteEndOfMessage();

			// Token: 0x06001211 RID: 4625
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamSkip();

			// Token: 0x06001212 RID: 4626
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamFlush();
		}
	}
}
