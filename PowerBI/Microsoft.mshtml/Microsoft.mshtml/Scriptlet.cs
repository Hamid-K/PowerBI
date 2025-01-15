using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000845 RID: 2117
	[Guid("AE24FDAD-03C6-11D1-8B76-0080C744F389")]
	[CoClass(typeof(ScriptletClass))]
	[ComImport]
	public interface Scriptlet : IWebBridge, DWebBridgeEvents_Event
	{
	}
}
