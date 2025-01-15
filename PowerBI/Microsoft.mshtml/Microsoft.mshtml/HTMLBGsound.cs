using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BF1 RID: 3057
	[Guid("3050F53C-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLBGsoundClass))]
	[ComImport]
	public interface HTMLBGsound : DispHTMLBGsound, HTMLElementEvents_Event
	{
	}
}
