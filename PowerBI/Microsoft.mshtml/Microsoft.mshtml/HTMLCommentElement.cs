using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009E4 RID: 2532
	[Guid("3050F50A-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLCommentElementClass))]
	[ComImport]
	public interface HTMLCommentElement : DispHTMLCommentElement, HTMLElementEvents_Event
	{
	}
}
