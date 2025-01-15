using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200008C RID: 140
	public interface IRecordWithIdLookup
	{
		// Token: 0x060005B7 RID: 1463
		bool TryGetRecord(ISession session, int rid, ref object[] values);
	}
}
