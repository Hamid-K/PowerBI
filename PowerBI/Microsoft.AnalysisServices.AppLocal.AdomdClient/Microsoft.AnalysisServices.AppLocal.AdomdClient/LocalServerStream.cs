using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200002E RID: 46
	internal sealed class LocalServerStream : ClearTextXmlaStream
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000CCB1 File Offset: 0x0000AEB1
		public LocalServerStream()
			: base(true)
		{
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000CCBA File Offset: 0x0000AEBA
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamWrite(buffer, offset, size);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000CCD3 File Offset: 0x0000AED3
		public override void WriteEndOfMessage()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamWriteEndOfMessage();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000CCE9 File Offset: 0x0000AEE9
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			return LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamRead(buffer, offset, size);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000CD02 File Offset: 0x0000AF02
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamFlush();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000CD18 File Offset: 0x0000AF18
		public override void Skip()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamSkip();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000CD2E File Offset: 0x0000AF2E
		public override void Close()
		{
			LocalServerStream.LocalServerNativeMethods.MSMDLocalStreamClose();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000CD38 File Offset: 0x0000AF38
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
			// Token: 0x06001219 RID: 4633
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamClose();

			// Token: 0x0600121A RID: 4634
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamCloseBase();

			// Token: 0x0600121B RID: 4635
			[DllImport("msmgdsrv.dll")]
			public static extern int MSMDLocalStreamRead([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] [Out] byte[] buffer, [MarshalAs(UnmanagedType.I4)] [In] int offset, [MarshalAs(UnmanagedType.I4)] [In] int size);

			// Token: 0x0600121C RID: 4636
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamWrite([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] buffer, [MarshalAs(UnmanagedType.I4)] [In] int offset, [MarshalAs(UnmanagedType.I4)] [In] int size);

			// Token: 0x0600121D RID: 4637
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamWriteEndOfMessage();

			// Token: 0x0600121E RID: 4638
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamSkip();

			// Token: 0x0600121F RID: 4639
			[DllImport("msmgdsrv.dll")]
			public static extern void MSMDLocalStreamFlush();
		}
	}
}
