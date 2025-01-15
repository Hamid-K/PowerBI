using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F7C RID: 8060
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MarshalledInteropData
	{
		// Token: 0x0600C4D1 RID: 50385 RVA: 0x00274777 File Offset: 0x00272977
		public MarshalledInteropData(IntPtr managedObject, IntPtr getVtableCallback, IntPtr releaseCallback, IntPtr getErrorInfo, IntPtr setErrorInfo)
		{
			this.managedObject = managedObject;
			this.getVtableCallback = getVtableCallback;
			this.releaseCallback = releaseCallback;
			this.getErrorInfo = getErrorInfo;
			this.setErrorInfo = setErrorInfo;
		}

		// Token: 0x0600C4D2 RID: 50386 RVA: 0x0027479E File Offset: 0x0027299E
		public MarshalledInteropData(IntPtr managedObject, MarshalledInteropData interopDataCallbacks)
		{
			this = new MarshalledInteropData(managedObject, interopDataCallbacks.getVtableCallback, interopDataCallbacks.releaseCallback, interopDataCallbacks.getErrorInfo, interopDataCallbacks.setErrorInfo);
		}

		// Token: 0x040064CB RID: 25803
		public IntPtr managedObject;

		// Token: 0x040064CC RID: 25804
		public IntPtr getVtableCallback;

		// Token: 0x040064CD RID: 25805
		public IntPtr releaseCallback;

		// Token: 0x040064CE RID: 25806
		public IntPtr getErrorInfo;

		// Token: 0x040064CF RID: 25807
		public IntPtr setErrorInfo;
	}
}
