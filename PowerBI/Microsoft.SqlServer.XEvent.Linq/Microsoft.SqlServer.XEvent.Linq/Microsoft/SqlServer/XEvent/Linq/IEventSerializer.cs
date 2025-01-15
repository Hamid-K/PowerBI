using System;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000D9 RID: 217
	public interface IEventSerializer
	{
		// Token: 0x060002E3 RID: 739
		void SerializeObjectData(IMetadataGeneration generation, IntPtr rawEvent, uint rawEventLength);
	}
}
