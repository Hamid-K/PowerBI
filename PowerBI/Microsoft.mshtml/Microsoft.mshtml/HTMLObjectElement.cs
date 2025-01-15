using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B40 RID: 2880
	[Guid("3050F529-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLObjectElementClass))]
	[ComImport]
	public interface HTMLObjectElement : DispHTMLObjectElement, HTMLObjectElementEvents_Event
	{
	}
}
