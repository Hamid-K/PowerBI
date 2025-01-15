using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020008C9 RID: 2249
	[CoClass(typeof(HTMLEmbedClass))]
	[Guid("3050F52E-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLEmbed : DispHTMLEmbed, HTMLControlElementEvents_Event
	{
	}
}
