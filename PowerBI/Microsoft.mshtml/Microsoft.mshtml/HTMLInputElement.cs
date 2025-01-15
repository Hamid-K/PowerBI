using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000693 RID: 1683
	[CoClass(typeof(HTMLInputElementClass))]
	[Guid("3050F57D-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLInputElement : DispHTMLInputElement, HTMLInputTextElementEvents_Event
	{
	}
}
