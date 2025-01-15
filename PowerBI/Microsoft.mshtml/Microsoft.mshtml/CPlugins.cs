using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007A0 RID: 1952
	[Guid("3050F3FD-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(CPluginsClass))]
	[ComImport]
	public interface CPlugins : IHTMLPluginsCollection
	{
	}
}
