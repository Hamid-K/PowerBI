using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000076 RID: 118
	public interface IRowsetDistributor
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004C7 RID: 1223
		IRowsetManager RowsetManager { get; }

		// Token: 0x060004C8 RID: 1224
		void RequestRowset(string rowsetName, IRecordUpdate recordUpdate);

		// Token: 0x060004C9 RID: 1225
		void RequestRowset(string rowsetName, IRecordUpdate recordUpdate, int pass);
	}
}
