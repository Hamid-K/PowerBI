using System;
using System.IO;

namespace Microsoft.Owin.Hosting.Tracing
{
	// Token: 0x0200000D RID: 13
	public interface ITraceOutputFactory
	{
		// Token: 0x06000055 RID: 85
		TextWriter Create(string outputFile);
	}
}
