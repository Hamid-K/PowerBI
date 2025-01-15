using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.Marshallers
{
	// Token: 0x02001FCF RID: 8143
	public class I4Marshaller : IMarshaller<int>, IMarshaller
	{
		// Token: 0x17003035 RID: 12341
		// (get) Token: 0x0600C6FD RID: 50941 RVA: 0x0000244F File Offset: 0x0000064F
		public int NativeSizeInBytes
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17003036 RID: 12342
		// (get) Token: 0x0600C6FE RID: 50942 RVA: 0x0000240C File Offset: 0x0000060C
		public VARTYPE Type
		{
			get
			{
				return VARTYPE.I4;
			}
		}

		// Token: 0x0600C6FF RID: 50943 RVA: 0x0027A4E8 File Offset: 0x002786E8
		public int GetManaged(IntPtr native)
		{
			return Marshal.ReadInt32(native);
		}

		// Token: 0x0600C700 RID: 50944 RVA: 0x0027A4F0 File Offset: 0x002786F0
		public void GetNative(int managed, IntPtr native)
		{
			Marshal.WriteInt32(native, managed);
		}

		// Token: 0x0600C701 RID: 50945 RVA: 0x0000336E File Offset: 0x0000156E
		public void Cleanup(IntPtr native)
		{
		}
	}
}
