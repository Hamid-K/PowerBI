using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CEB RID: 3307
	[InterfaceType(1)]
	[Guid("3050F844-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IEnumPrivacyRecords
	{
		// Token: 0x06016368 RID: 90984
		[MethodImpl(4096, MethodCodeType = 3)]
		void reset();

		// Token: 0x06016369 RID: 90985
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetSize(out uint pSize);

		// Token: 0x0601636A RID: 90986
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPrivacyImpacted(out int pState);

		// Token: 0x0601636B RID: 90987
		[MethodImpl(4096, MethodCodeType = 3)]
		void Next([MarshalAs(19)] out string pbstrUrl, [MarshalAs(19)] out string pbstrPolicyRef, out int pdwReserved, out uint pdwPrivacyFlags);
	}
}
