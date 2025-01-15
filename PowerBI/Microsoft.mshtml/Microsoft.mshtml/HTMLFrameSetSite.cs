using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BED RID: 3053
	[Guid("3050F514-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLFrameSetSiteClass))]
	[ComImport]
	public interface HTMLFrameSetSite : DispHTMLFrameSetSite, HTMLControlElementEvents_Event
	{
	}
}
