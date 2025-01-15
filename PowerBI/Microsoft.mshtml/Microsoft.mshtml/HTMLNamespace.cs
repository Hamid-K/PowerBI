using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CDA RID: 3290
	[Guid("3050F6BB-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLNamespaceClass))]
	[ComImport]
	public interface HTMLNamespace : IHTMLNamespace, HTMLNamespaceEvents_Event
	{
	}
}
