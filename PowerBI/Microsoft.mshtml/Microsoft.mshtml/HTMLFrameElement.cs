using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BD1 RID: 3025
	[Guid("3050F513-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLFrameElementClass))]
	[ComImport]
	public interface HTMLFrameElement : DispHTMLFrameElement, HTMLControlElementEvents_Event
	{
	}
}
