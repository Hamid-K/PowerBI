using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000390 RID: 912
	[CoClass(typeof(HTMLBodyClass))]
	[Guid("3050F507-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLBody : DispHTMLBody, HTMLTextContainerEvents_Event
	{
	}
}
