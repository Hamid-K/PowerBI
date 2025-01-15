using System;
using System.IO;
using YamlDotNet.Core;

namespace Microsoft.Lucia.Yaml
{
	// Token: 0x0200001C RID: 28
	public class TextWriterEmitter : Emitter, ITextWriterEmitter, IEmitter
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002B38 File Offset: 0x00000D38
		public TextWriterEmitter(TextWriter output)
			: base(output)
		{
			this.Output = output;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002B48 File Offset: 0x00000D48
		public TextWriter Output { get; }
	}
}
