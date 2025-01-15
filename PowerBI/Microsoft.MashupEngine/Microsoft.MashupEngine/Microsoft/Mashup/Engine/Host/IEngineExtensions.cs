using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Host
{
	// Token: 0x0200021C RID: 540
	public static class IEngineExtensions
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001902D File Offset: 0x0001722D
		public static IDocument Parse(this IEngine engine, string text, Action<IError> log)
		{
			return engine.Parse(engine.Tokenize(text), new TextDocumentHost(text), log);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00019043 File Offset: 0x00017243
		public static IDocument Parse(this IEngine engine, string text, IDocumentHost host, Action<IError> log)
		{
			return engine.Parse(engine.Tokenize(text), host, log);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00019054 File Offset: 0x00017254
		public static string EscapeIdentifier(this IEngine engine, string section, string name)
		{
			return engine.EscapeIdentifier(section) + "!" + engine.EscapeIdentifier(name);
		}
	}
}
