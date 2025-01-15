using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001E86 RID: 7814
	public class ComInteropServices : IInteropServices
	{
		// Token: 0x0600C112 RID: 49426 RVA: 0x0026D5C0 File Offset: 0x0026B7C0
		public int AggregateDataSource(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = ComInteropServices.Imports.ComAggregateDataSource(punkOuter, iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x0600C113 RID: 49427 RVA: 0x0026D614 File Offset: 0x0026B814
		public int AggregateSession(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = ComInteropServices.Imports.ComAggregateSession(punkOuter, iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x0600C114 RID: 49428 RVA: 0x0026D668 File Offset: 0x0026B868
		public int AggregateCommand(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = ComInteropServices.Imports.ComAggregateCommand(punkOuter, iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x0600C115 RID: 49429 RVA: 0x0026D6BC File Offset: 0x0026B8BC
		public int AggregateRowset(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = ComInteropServices.Imports.ComAggregateRowset(punkOuter, iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x0600C116 RID: 49430 RVA: 0x0026D710 File Offset: 0x0026B910
		public int AggregateMultipleResults(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = ComInteropServices.Imports.ComAggregateMultipleResults(punkOuter, iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x0600C117 RID: 49431 RVA: 0x0026D764 File Offset: 0x0026B964
		public int QueryInterface(object obj, ref Guid iid, out IntPtr ppv)
		{
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			int num = Marshal.QueryInterface(iunknownForObject, ref iid, out ppv);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x0600C118 RID: 49432 RVA: 0x0026D787 File Offset: 0x0026B987
		public int QueryInterface(IntPtr pUnknown, ref Guid iid, out IntPtr ppv)
		{
			return Marshal.QueryInterface(pUnknown, ref iid, out ppv);
		}

		// Token: 0x0600C119 RID: 49433 RVA: 0x0026D791 File Offset: 0x0026B991
		public int AddRef(IntPtr pUnknown)
		{
			return Marshal.AddRef(pUnknown);
		}

		// Token: 0x0600C11A RID: 49434 RVA: 0x0026D799 File Offset: 0x0026B999
		public int Release(IntPtr pUnknown)
		{
			return Marshal.Release(pUnknown);
		}

		// Token: 0x0600C11B RID: 49435 RVA: 0x0026D7A1 File Offset: 0x0026B9A1
		public int ReleaseComObject(object obj)
		{
			return Marshal.ReleaseComObject(obj);
		}

		// Token: 0x0600C11C RID: 49436 RVA: 0x0026D7AC File Offset: 0x0026B9AC
		public int GetErrorInfo(uint dwReserved, out IntPtr errorInfoUnmanaged)
		{
			errorInfoUnmanaged = IntPtr.Zero;
			IErrorInfo errorInfo;
			int num = this.GetErrorInfo(dwReserved, out errorInfo);
			if (num != 0)
			{
				return num;
			}
			try
			{
				Guid ierrorInfo = IID.IErrorInfo;
				num = this.QueryInterface(errorInfo, ref ierrorInfo, out errorInfoUnmanaged);
			}
			finally
			{
				this.ReleaseComObject(errorInfo);
			}
			return num;
		}

		// Token: 0x0600C11D RID: 49437 RVA: 0x0026D800 File Offset: 0x0026BA00
		public int GetErrorInfo(uint dwReserved, out IErrorInfo errorInfoManaged)
		{
			return ComInteropServices.Imports.GetErrorInfo(dwReserved, out errorInfoManaged);
		}

		// Token: 0x0600C11E RID: 49438 RVA: 0x0026D80C File Offset: 0x0026BA0C
		public static IErrorInfo GetErrorInfo()
		{
			IErrorInfo errorInfo;
			if (ComInteropServices.Imports.GetErrorInfo(0U, out errorInfo) < 0)
			{
				return null;
			}
			return errorInfo;
		}

		// Token: 0x02001E87 RID: 7815
		private static class Imports
		{
			// Token: 0x0600C120 RID: 49440
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int ComAggregateDataSource(IntPtr punkOuter, IntPtr punk, ref Guid iid, out IntPtr ppv);

			// Token: 0x0600C121 RID: 49441
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int ComAggregateSession(IntPtr punkOuter, IntPtr punk, ref Guid iid, out IntPtr ppv);

			// Token: 0x0600C122 RID: 49442
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int ComAggregateCommand(IntPtr punkOuter, IntPtr punk, ref Guid iid, out IntPtr ppv);

			// Token: 0x0600C123 RID: 49443
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int ComAggregateRowset(IntPtr punkOuter, IntPtr punk, ref Guid iid, out IntPtr ppv);

			// Token: 0x0600C124 RID: 49444
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int ComAggregateMultipleResults(IntPtr punkOuter, IntPtr punk, ref Guid iid, out IntPtr ppv);

			// Token: 0x0600C125 RID: 49445
			[DllImport("ole32.dll")]
			public static extern int GetErrorInfo([In] uint dwReserved, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo pperrinfo);
		}
	}
}
