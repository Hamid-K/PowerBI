using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000165 RID: 357
	[Guid("0000000b-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ComIStorage
	{
		// Token: 0x06000D61 RID: 3425
		void CreateStream(string pwcsName, STGM grfMode, uint reserved1, uint reserved2, out IStream ppstm);

		// Token: 0x06000D62 RID: 3426
		void OpenStream(string pwcsName, IntPtr reserved1, STGM grfMode, uint reserved2, out IStream ppstm);

		// Token: 0x06000D63 RID: 3427
		void CreateStorage(string pwcsName, uint grfMode, uint reserved1, uint reserved2, out ComIStorage ppstg);

		// Token: 0x06000D64 RID: 3428
		void OpenStorage(string pwcsName, ComIStorage pstgPriority, uint grfMode, IntPtr snbExclude, uint reserved, out ComIStorage ppstg);

		// Token: 0x06000D65 RID: 3429
		void CopyTo(uint ciidExclude, IntPtr rgiidExclude, IntPtr snbExclude, ComIStorage pstgDest);

		// Token: 0x06000D66 RID: 3430
		void MoveElementTo(string pwcsName, ComIStorage pstgDest, string pwcsNewName, uint grfFlags);

		// Token: 0x06000D67 RID: 3431
		void Commit(uint grfCommitFlags);

		// Token: 0x06000D68 RID: 3432
		void Revert();

		// Token: 0x06000D69 RID: 3433
		void EnumElements(uint reserved1, IntPtr reserved2, uint reserved3, out IEnumSTATSTG ppenum);

		// Token: 0x06000D6A RID: 3434
		void DestroyElement(string pwcsName);

		// Token: 0x06000D6B RID: 3435
		void RenameElement(string pwcsOldName, string pwcsNewName);

		// Token: 0x06000D6C RID: 3436
		void SetElementTimes(string pwcsName, global::System.Runtime.InteropServices.ComTypes.FILETIME pctime, global::System.Runtime.InteropServices.ComTypes.FILETIME patime, global::System.Runtime.InteropServices.ComTypes.FILETIME pmtime);

		// Token: 0x06000D6D RID: 3437
		void SetClass(Guid clsid);

		// Token: 0x06000D6E RID: 3438
		void SetStateBits(uint grfStateBits, uint grfMask);

		// Token: 0x06000D6F RID: 3439
		void Stat(out global::System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, uint grfStatFlag);
	}
}
