using System;
using System.IO;
using YamlDotNet.Core;

namespace Microsoft.Lucia.Yaml
{
	// Token: 0x0200001B RID: 27
	public interface ITextWriterEmitter : IEmitter
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000065 RID: 101
		TextWriter Output { get; }
	}
}
