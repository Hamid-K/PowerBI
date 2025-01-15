using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F0A RID: 7946
	[Guid("0c733a95-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBAsynchStatus
	{
		// Token: 0x0600C2BA RID: 49850
		void Abort(HCHAPTER hChapter, DBASYNCHOP eOperation);

		// Token: 0x0600C2BB RID: 49851
		unsafe void GetStatus(HCHAPTER hChapter, DBASYNCHOP eOperation, DBCOUNTITEM* pulProgress, DBCOUNTITEM* pulProgressMax, out DBASYNCHPHASE peAsynchPhase, char** ppwszStatusText);
	}
}
