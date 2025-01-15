using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC9 RID: 3273
	[Guid("3050F699-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface ISelectionServicesListener
	{
		// Token: 0x06016308 RID: 90888
		[MethodImpl(4096, MethodCodeType = 3)]
		void BeginSelectionUndo();

		// Token: 0x06016309 RID: 90889
		[MethodImpl(4096, MethodCodeType = 3)]
		void EndSelectionUndo();

		// Token: 0x0601630A RID: 90890
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnSelectedElementExit([MarshalAs(28)] [In] IMarkupPointer pIElementStart, [MarshalAs(28)] [In] IMarkupPointer pIElementEnd, [MarshalAs(28)] [In] IMarkupPointer pIElementContentStart, [MarshalAs(28)] [In] IMarkupPointer pIElementContentEnd);

		// Token: 0x0601630B RID: 90891
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnChangeType([In] _SELECTION_TYPE eType, [MarshalAs(28)] [In] ISelectionServicesListener pIListener);

		// Token: 0x0601630C RID: 90892
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetTypeDetail([MarshalAs(19)] out string pTypeDetail);
	}
}
