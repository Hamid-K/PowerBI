using System;
using Microsoft.AnalysisServices.Tabular;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x0200002E RID: 46
	public interface ITable
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600015A RID: 346
		bool HasErrors { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600015B RID: 347
		string ErrorMessage { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600015C RID: 348
		bool IsCalculated { get; }

		// Token: 0x0600015D RID: 349
		IColumn FindColumn(string name);

		// Token: 0x0600015E RID: 350
		IMeasure FindMeasure(string name);

		// Token: 0x0600015F RID: 351
		IHierarchy FindHierarchy(string name);

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000160 RID: 352
		bool CanRefreshIndependently { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000161 RID: 353
		bool CanEditSource { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000162 RID: 354
		bool CanRename { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000163 RID: 355
		bool CanDelete { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000164 RID: 356
		bool CanEditStorageMode { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000165 RID: 357
		ModeType? StorageMode { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000166 RID: 358
		DateTime? RefreshedTime { get; }

		// Token: 0x06000167 RID: 359
		DataViewCapabilities GetDataViewCapabilities();

		// Token: 0x06000168 RID: 360
		void GetDirectQueryResourceInfo(out string sourceType, out string sourceName);
	}
}
