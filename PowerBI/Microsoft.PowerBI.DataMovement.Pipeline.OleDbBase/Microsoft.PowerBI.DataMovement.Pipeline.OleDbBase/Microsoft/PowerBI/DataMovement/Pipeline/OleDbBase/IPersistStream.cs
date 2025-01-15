using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200006C RID: 108
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[Guid("00000109-0000-0000-c000-000000000046")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPersistStream : IPersist
	{
		// Token: 0x0600029A RID: 666
		void GetClassID(out Guid classID);

		// Token: 0x0600029B RID: 667
		void GetSizeMax(out ULARGE_INTEGER size);

		// Token: 0x0600029C RID: 668
		void InitNew();

		// Token: 0x0600029D RID: 669
		[PreserveSig]
		int IsDirty();

		// Token: 0x0600029E RID: 670
		void Load([MarshalAs(UnmanagedType.Interface)] [In] IStream stream);

		// Token: 0x0600029F RID: 671
		void Save([MarshalAs(UnmanagedType.Interface)] [In] IStream stream, [In] int clearDirty);
	}
}
