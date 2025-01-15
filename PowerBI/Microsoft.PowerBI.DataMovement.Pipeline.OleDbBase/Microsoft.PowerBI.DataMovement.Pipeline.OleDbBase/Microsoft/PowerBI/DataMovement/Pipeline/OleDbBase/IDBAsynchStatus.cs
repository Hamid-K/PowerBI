using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000085 RID: 133
	[Guid("0c733a95-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBAsynchStatus
	{
		// Token: 0x060002DE RID: 734
		void Abort(HCHAPTER chapter, DBASYNCHOP operation);

		// Token: 0x060002DF RID: 735
		unsafe void GetStatus(HCHAPTER chapter, DBASYNCHOP operation, DBCOUNTITEM* progress, DBCOUNTITEM* progressMax, out DBASYNCHPHASE asynchPhase, char** statusText);
	}
}
