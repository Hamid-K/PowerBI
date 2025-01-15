using System;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200000F RID: 15
	public interface IDumperAdapter
	{
		// Token: 0x0600005F RID: 95
		void Prepare();

		// Token: 0x06000060 RID: 96
		string Dump(DumpInstructions instructions);
	}
}
