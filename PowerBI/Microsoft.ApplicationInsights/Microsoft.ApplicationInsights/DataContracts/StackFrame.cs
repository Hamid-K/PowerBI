using System;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000DD RID: 221
	public sealed class StackFrame
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x0001A400 File Offset: 0x00018600
		public StackFrame(string assembly, string fileName, int level, int line, string method)
		{
			this.Data = new StackFrame
			{
				assembly = assembly,
				fileName = fileName,
				level = level,
				line = line,
				method = method
			};
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0001A438 File Offset: 0x00018638
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x0001A440 File Offset: 0x00018640
		internal StackFrame Data { get; private set; }
	}
}
