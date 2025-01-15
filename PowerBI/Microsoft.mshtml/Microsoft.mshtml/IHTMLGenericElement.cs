using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000B7 RID: 183
	[TypeLibType(4160)]
	[Guid("3050F4B7-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLGenericElement
	{
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06000DAB RID: 3499
		[DispId(1001)]
		object recordset
		{
			[TypeLibFunc(64)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06000DAC RID: 3500
		[DispId(1002)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object namedRecordset([MarshalAs(19)] [In] string dataMember, [MarshalAs(27)] [In] [Optional] ref object hierarchy);
	}
}
