using System;
using System.IO;

namespace Microsoft.Owin.Hosting.Tracing
{
	// Token: 0x0200000E RID: 14
	public class TraceOutputFactory : ITraceOutputFactory
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000032BF File Offset: 0x000014BF
		public virtual TextWriter Create(string outputFile)
		{
			if (!string.IsNullOrWhiteSpace(outputFile))
			{
				return new StreamWriter(outputFile, true);
			}
			return new DualWriter(Console.Error);
		}
	}
}
