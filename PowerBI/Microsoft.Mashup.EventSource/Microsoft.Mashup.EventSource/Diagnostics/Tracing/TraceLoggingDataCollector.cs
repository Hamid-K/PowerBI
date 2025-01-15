using System;
using System.Security;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200006E RID: 110
	[SecuritySafeCritical]
	internal class TraceLoggingDataCollector
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000D809 File Offset: 0x0000BA09
		private TraceLoggingDataCollector()
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000D811 File Offset: 0x0000BA11
		public int BeginBufferedArray()
		{
			return DataCollector.ThreadInstance.BeginBufferedArray();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000D81D File Offset: 0x0000BA1D
		public void EndBufferedArray(int bookmark, int count)
		{
			DataCollector.ThreadInstance.EndBufferedArray(bookmark, count);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000D82B File Offset: 0x0000BA2B
		public TraceLoggingDataCollector AddGroup()
		{
			return this;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000D82E File Offset: 0x0000BA2E
		public unsafe void AddScalar(bool value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000D83E File Offset: 0x0000BA3E
		public unsafe void AddScalar(sbyte value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000D84E File Offset: 0x0000BA4E
		public unsafe void AddScalar(byte value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000D85E File Offset: 0x0000BA5E
		public unsafe void AddScalar(short value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000D86E File Offset: 0x0000BA6E
		public unsafe void AddScalar(ushort value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000D87E File Offset: 0x0000BA7E
		public unsafe void AddScalar(int value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000D88E File Offset: 0x0000BA8E
		public unsafe void AddScalar(uint value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000D89E File Offset: 0x0000BA9E
		public unsafe void AddScalar(long value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000D8AE File Offset: 0x0000BAAE
		public unsafe void AddScalar(ulong value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000D8BE File Offset: 0x0000BABE
		public unsafe void AddScalar(IntPtr value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), IntPtr.Size);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000D8D2 File Offset: 0x0000BAD2
		public unsafe void AddScalar(UIntPtr value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), UIntPtr.Size);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000D8E6 File Offset: 0x0000BAE6
		public unsafe void AddScalar(float value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000D8F6 File Offset: 0x0000BAF6
		public unsafe void AddScalar(double value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000D906 File Offset: 0x0000BB06
		public unsafe void AddScalar(char value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000D916 File Offset: 0x0000BB16
		public unsafe void AddScalar(Guid value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 16);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000D927 File Offset: 0x0000BB27
		public void AddBinary(string value)
		{
			DataCollector.ThreadInstance.AddBinary(value, (value == null) ? 0 : (value.Length * 2));
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000D942 File Offset: 0x0000BB42
		public void AddBinary(byte[] value)
		{
			DataCollector.ThreadInstance.AddBinary(value, (value == null) ? 0 : value.Length);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000D958 File Offset: 0x0000BB58
		public void AddArray(bool[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000D96F File Offset: 0x0000BB6F
		public void AddArray(sbyte[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000D986 File Offset: 0x0000BB86
		public void AddArray(short[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000D99D File Offset: 0x0000BB9D
		public void AddArray(ushort[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
		public void AddArray(int[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000D9CB File Offset: 0x0000BBCB
		public void AddArray(uint[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000D9E2 File Offset: 0x0000BBE2
		public void AddArray(long[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000D9F9 File Offset: 0x0000BBF9
		public void AddArray(ulong[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000DA10 File Offset: 0x0000BC10
		public void AddArray(IntPtr[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, IntPtr.Size);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000DA2B File Offset: 0x0000BC2B
		public void AddArray(UIntPtr[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, UIntPtr.Size);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000DA46 File Offset: 0x0000BC46
		public void AddArray(float[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000DA5D File Offset: 0x0000BC5D
		public void AddArray(double[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000DA74 File Offset: 0x0000BC74
		public void AddArray(char[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000DA8B File Offset: 0x0000BC8B
		public void AddArray(Guid[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 16);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000DAA3 File Offset: 0x0000BCA3
		public void AddCustom(byte[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x04000110 RID: 272
		internal static readonly TraceLoggingDataCollector Instance = new TraceLoggingDataCollector();
	}
}
