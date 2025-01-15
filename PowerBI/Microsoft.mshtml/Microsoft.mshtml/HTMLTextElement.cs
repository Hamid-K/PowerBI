using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000273 RID: 627
	[Guid("3050F537-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLTextElementClass))]
	[ComImport]
	public interface HTMLTextElement : DispHTMLTextElement, HTMLElementEvents_Event
	{
	}
}
