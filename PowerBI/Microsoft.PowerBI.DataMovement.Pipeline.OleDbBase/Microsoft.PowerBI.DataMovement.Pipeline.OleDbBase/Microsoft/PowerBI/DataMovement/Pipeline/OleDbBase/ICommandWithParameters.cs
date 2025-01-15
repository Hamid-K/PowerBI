using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000082 RID: 130
	[Guid("0c733a64-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ICommandWithParameters
	{
		// Token: 0x060002D8 RID: 728
		[PreserveSig]
		unsafe int GetParameterInfo(out DB_UPARAMS paramCount, out DBPARAMINFO* paramInfo, char** namesBuffer);

		// Token: 0x060002D9 RID: 729
		[PreserveSig]
		unsafe int MapParameterNames(DB_UPARAMS paramNameCount, char** paramNames, DB_LPARAMS* paramOrdinals);

		// Token: 0x060002DA RID: 730
		[PreserveSig]
		unsafe int SetParameterInfo(DB_UPARAMS paramCount, DB_UPARAMS* paramOrdinals, DBPARAMBINDINFO* paramBindInfo);
	}
}
