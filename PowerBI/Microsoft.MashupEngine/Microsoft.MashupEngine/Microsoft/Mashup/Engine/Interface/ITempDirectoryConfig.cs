using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200004D RID: 77
	public interface ITempDirectoryConfig
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014A RID: 330
		string TempDirectoryPath { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014B RID: 331
		long TempDirectoryMaxSize { get; }
	}
}
