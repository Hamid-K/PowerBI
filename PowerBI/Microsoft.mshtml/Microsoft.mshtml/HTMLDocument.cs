using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000835 RID: 2101
	[Guid("3050F55F-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLDocumentClass))]
	[ComImport]
	public interface HTMLDocument : DispHTMLDocument, HTMLDocumentEvents_Event
	{
	}
}
