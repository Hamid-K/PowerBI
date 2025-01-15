using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC8 RID: 3272
	[InterfaceType(1)]
	[Guid("3050F6CA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IIMEServices
	{
		// Token: 0x06016307 RID: 90887
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetActiveIMM([MarshalAs(28)] out IActiveIMMApp ppActiveIMM);
	}
}
