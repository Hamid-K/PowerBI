using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004D2 RID: 1234
	[Guid("3050F52C-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLParaElementClass))]
	[ComImport]
	public interface HTMLParaElement : DispHTMLParaElement, HTMLElementEvents_Event
	{
	}
}
