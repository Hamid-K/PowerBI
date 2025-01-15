using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000008 RID: 8
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class Aggregator
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000025AC File Offset: 0x000007AC
		public static int AggregateDataSource(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = Aggregator.AggregateDataSource(punkOuter, iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000025FC File Offset: 0x000007FC
		public static int AggregateSession(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = Aggregator.AggregateSession(punkOuter, iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000264C File Offset: 0x0000084C
		public static int AggregateCommand(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = Aggregator.AggregateCommand(punkOuter, iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000269C File Offset: 0x0000089C
		public static int AggregateRowset(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = Aggregator.AggregateRowset(punkOuter, iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026EC File Offset: 0x000008EC
		public static int AggregateMdDataset(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = Aggregator.AggregateMdDataset(punkOuter, iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000273C File Offset: 0x0000093C
		public static int AggregateMultipleResults(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = Aggregator.AggregateMultipleResults(punkOuter, iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x06000017 RID: 23
		[DllImport("Microsoft.PowerBI.DataMovement.Pipeline.OleDbTransferProviderFactory.dll")]
		public static extern int AggregateDataSource(IntPtr punkOuter, IntPtr punk, ref Guid iid, out IntPtr ppv);

		// Token: 0x06000018 RID: 24
		[DllImport("Microsoft.PowerBI.DataMovement.Pipeline.OleDbTransferProviderFactory.dll")]
		public static extern int AggregateSession(IntPtr punkOuter, IntPtr punk, ref Guid iid, out IntPtr ppv);

		// Token: 0x06000019 RID: 25
		[DllImport("Microsoft.PowerBI.DataMovement.Pipeline.OleDbTransferProviderFactory.dll")]
		public static extern int AggregateCommand(IntPtr punkOuter, IntPtr punk, ref Guid iid, out IntPtr ppv);

		// Token: 0x0600001A RID: 26
		[DllImport("Microsoft.PowerBI.DataMovement.Pipeline.OleDbTransferProviderFactory.dll")]
		public static extern int AggregateRowset(IntPtr punkOuter, IntPtr punk, ref Guid iid, out IntPtr ppv);

		// Token: 0x0600001B RID: 27
		[DllImport("Microsoft.PowerBI.DataMovement.Pipeline.OleDbTransferProviderFactory.dll")]
		public static extern int AggregateMdDataset(IntPtr punkOuter, IntPtr punk, ref Guid iid, out IntPtr ppv);

		// Token: 0x0600001C RID: 28
		[DllImport("Microsoft.PowerBI.DataMovement.Pipeline.OleDbTransferProviderFactory.dll")]
		public static extern int AggregateMultipleResults(IntPtr punkOuter, IntPtr punk, ref Guid iid, out IntPtr ppv);
	}
}
