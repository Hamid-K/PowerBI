using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.Marshallers
{
	// Token: 0x02001FCC RID: 8140
	public class BStrMarshaller : IMarshaller<string>, IMarshaller
	{
		// Token: 0x1700302F RID: 12335
		// (get) Token: 0x0600C6E8 RID: 50920 RVA: 0x0027A0D7 File Offset: 0x002782D7
		public int NativeSizeInBytes
		{
			get
			{
				return IntPtr.Size;
			}
		}

		// Token: 0x17003030 RID: 12336
		// (get) Token: 0x0600C6E9 RID: 50921 RVA: 0x000024ED File Offset: 0x000006ED
		public VARTYPE Type
		{
			get
			{
				return VARTYPE.BSTR;
			}
		}

		// Token: 0x0600C6EA RID: 50922 RVA: 0x0027A0E0 File Offset: 0x002782E0
		public unsafe string GetManaged(IntPtr native)
		{
			void** ptr = (void**)(void*)native;
			return Marshal.PtrToStringBSTR((IntPtr)(*(IntPtr*)ptr));
		}

		// Token: 0x0600C6EB RID: 50923 RVA: 0x0027A100 File Offset: 0x00278300
		public unsafe void GetNative(string managed, IntPtr native)
		{
			void** ptr = (void**)(void*)native;
			*(IntPtr*)ptr = (void*)Marshal.StringToBSTR(managed);
		}

		// Token: 0x0600C6EC RID: 50924 RVA: 0x0027A124 File Offset: 0x00278324
		public unsafe void Cleanup(IntPtr native)
		{
			void** ptr = (void**)(void*)native;
			Marshal.FreeBSTR((IntPtr)(*(IntPtr*)ptr));
		}
	}
}
