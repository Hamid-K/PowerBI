using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1
{
	// Token: 0x02000228 RID: 552
	internal static class EngineExtensions
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x0001AFDC File Offset: 0x000191DC
		public static IDocument Parse(this Engine engine, string text, Action<IError> log)
		{
			IDocumentHost documentHost = new TextDocumentHost(text);
			return engine.Parse(engine.Tokenize(text), documentHost, log);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0001B000 File Offset: 0x00019200
		public static Module Compile(this Engine engine, string text, RecordValue library, CompileOptions options, Action<IError> log)
		{
			IDocumentHost documentHost = new TextDocumentHost(text);
			return engine.Compile(engine.Parse(engine.Tokenize(text), documentHost, log), library, options, log);
		}
	}
}
