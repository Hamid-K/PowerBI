using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.Marshallers
{
	// Token: 0x02001FD4 RID: 8148
	public class StructureMarshaller<T> : IMarshaller<T>, IMarshaller
	{
		// Token: 0x0600C70E RID: 50958 RVA: 0x0027A650 File Offset: 0x00278850
		public StructureMarshaller(VARTYPE type = VARTYPE.RECORD)
		{
			this.type = type;
		}

		// Token: 0x17003039 RID: 12345
		// (get) Token: 0x0600C70F RID: 50959 RVA: 0x0027A65F File Offset: 0x0027885F
		public int NativeSizeInBytes
		{
			get
			{
				return Marshal.SizeOf(typeof(T));
			}
		}

		// Token: 0x1700303A RID: 12346
		// (get) Token: 0x0600C710 RID: 50960 RVA: 0x0027A670 File Offset: 0x00278870
		public VARTYPE Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600C711 RID: 50961 RVA: 0x0027A678 File Offset: 0x00278878
		public void Cleanup(IntPtr native)
		{
			Marshal.DestroyStructure(native, typeof(T));
		}

		// Token: 0x0600C712 RID: 50962 RVA: 0x0027A68A File Offset: 0x0027888A
		public T GetManaged(IntPtr native)
		{
			return (T)((object)Marshal.PtrToStructure(native, typeof(T)));
		}

		// Token: 0x0600C713 RID: 50963 RVA: 0x0027A6A1 File Offset: 0x002788A1
		public void GetNative(T managed, IntPtr native)
		{
			Marshal.StructureToPtr<T>(managed, native, false);
		}

		// Token: 0x0400658B RID: 25995
		private readonly VARTYPE type;
	}
}
