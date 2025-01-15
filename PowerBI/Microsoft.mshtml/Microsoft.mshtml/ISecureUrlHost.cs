using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CBB RID: 3259
	[Guid("C81984C4-74C8-11D2-BAA9-00C04FC2040E")]
	[InterfaceType(1)]
	[ComImport]
	public interface ISecureUrlHost
	{
		// Token: 0x060162B5 RID: 90805
		[MethodImpl(4096, MethodCodeType = 3)]
		void ValidateSecureUrl(out int pfAllow, [In] ref ushort pchUrlInQuestion, [In] uint dwFlags);
	}
}
