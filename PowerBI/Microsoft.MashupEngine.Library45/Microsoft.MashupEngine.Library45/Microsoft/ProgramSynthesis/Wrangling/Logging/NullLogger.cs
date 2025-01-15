using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Logging
{
	// Token: 0x02000183 RID: 387
	public class NullLogger : MultiLogger
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x00019AB5 File Offset: 0x00017CB5
		public NullLogger()
			: base(Array.Empty<ILogger>())
		{
		}

		// Token: 0x0400042E RID: 1070
		public static NullLogger Instance = new NullLogger();
	}
}
