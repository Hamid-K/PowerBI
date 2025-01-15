using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D0B RID: 3339
	[Guid("3050F55F-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(OldHTMLDocumentClass))]
	[ComImport]
	public interface OldHTMLDocument : DispHTMLDocument, HTMLDocumentEvents_Event
	{
	}
}
