using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200004A RID: 74
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class IMDDatasetExtensions
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00008288 File Offset: 0x00006488
		public static IRowset GetAxisRowset(this IMDDataset dataset, uint axisIndex)
		{
			Guid iunknown = IID.IUnknown;
			IntPtr intPtr;
			Marshal.ThrowExceptionForHR(dataset.GetAxisRowset(IntPtr.Zero, new DBCOUNTITEM
			{
				Value = (ulong)axisIndex
			}, ref iunknown, 0U, null, out intPtr));
			object objectForIUnknown = Marshal.GetObjectForIUnknown(intPtr);
			Marshal.Release(intPtr);
			return (IRowset)objectForIUnknown;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000082D8 File Offset: 0x000064D8
		public unsafe static void GetCellData(this IMDDataset dataset, HACCESSOR accessor, uint startCell, uint endCell, byte[] buffer)
		{
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				Marshal.ThrowExceptionForHR(dataset.GetCellData(accessor, new DBORDINAL
				{
					Value = (ulong)startCell
				}, new DBORDINAL
				{
					Value = (ulong)endCell
				}, (void*)ptr));
			}
		}
	}
}
