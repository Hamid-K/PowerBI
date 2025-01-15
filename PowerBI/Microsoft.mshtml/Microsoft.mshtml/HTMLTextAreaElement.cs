using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000697 RID: 1687
	[Guid("3050F521-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLTextAreaElementClass))]
	[ComImport]
	public interface HTMLTextAreaElement : DispHTMLTextAreaElement, HTMLInputTextElementEvents_Event
	{
	}
}
