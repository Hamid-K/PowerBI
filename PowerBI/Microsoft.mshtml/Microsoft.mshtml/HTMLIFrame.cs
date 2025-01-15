using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BD6 RID: 3030
	[CoClass(typeof(HTMLIFrameClass))]
	[Guid("3050F51B-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLIFrame : DispHTMLIFrame, HTMLControlElementEvents_Event
	{
	}
}
