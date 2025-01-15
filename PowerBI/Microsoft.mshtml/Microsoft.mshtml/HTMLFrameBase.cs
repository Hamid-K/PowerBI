using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BCC RID: 3020
	[Guid("3050F541-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLFrameBaseClass))]
	[ComImport]
	public interface HTMLFrameBase : DispHTMLFrameBase, HTMLControlElementEvents_Event
	{
	}
}
