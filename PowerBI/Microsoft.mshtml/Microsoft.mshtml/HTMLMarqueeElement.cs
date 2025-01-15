using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200076E RID: 1902
	[CoClass(typeof(HTMLMarqueeElementClass))]
	[Guid("3050F527-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLMarqueeElement : DispHTMLMarqueeElement, HTMLMarqueeElementEvents_Event
	{
	}
}
