using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000047 RID: 71
	internal sealed class LocalServerStream : ClearTextXmlaStream
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000FBD5 File Offset: 0x0000DDD5
		public LocalServerStream()
			: base(true)
		{
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000FBDE File Offset: 0x0000DDDE
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamWrite(buffer, offset, size);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000FBF7 File Offset: 0x0000DDF7
		public override void WriteEndOfMessage()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamWriteEndOfMessage();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000FC0D File Offset: 0x0000DE0D
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			return LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamRead(buffer, offset, size);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000FC26 File Offset: 0x0000DE26
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamFlush();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000FC3C File Offset: 0x0000DE3C
		public override void Skip()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamSkip();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000FC52 File Offset: 0x0000DE52
		public override void Close()
		{
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamClose();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000FC5C File Offset: 0x0000DE5C
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

		// Token: 0x02000186 RID: 390
		private static class LocalServerNativeMethods
		{
			// Token: 0x060012B7 RID: 4791
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamClose();

			// Token: 0x060012B8 RID: 4792
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamCloseBase();

			// Token: 0x060012B9 RID: 4793
			[DllImport("msmgdsrv.dll")]
			public static extern int MSMDLocalStreamRead([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] [Out] byte[] buffer, [MarshalAs(UnmanagedType.I4)] [In] int offset, [MarshalAs(UnmanagedType.I4)] [In] int size);

			// Token: 0x060012BA RID: 4794
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamWrite([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] buffer, [MarshalAs(UnmanagedType.I4)] [In] int offset, [MarshalAs(UnmanagedType.I4)] [In] int size);

			// Token: 0x060012BB RID: 4795
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamWriteEndOfMessage();

			// Token: 0x060012BC RID: 4796
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamSkip();

			// Token: 0x060012BD RID: 4797
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamFlush();
		}
	}
}
