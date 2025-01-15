using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CBA RID: 3258
	[InterfaceType(1)]
	[Guid("3050F3EC-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface ICSSFilter
	{
		// Token: 0x060162B3 RID: 90803
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetSite([MarshalAs(28)] [In] ICSSFilterSite pSink);

		// Token: 0x060162B4 RID: 90804
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnAmbientPropertyChange([In] int dispid);
	}
}
