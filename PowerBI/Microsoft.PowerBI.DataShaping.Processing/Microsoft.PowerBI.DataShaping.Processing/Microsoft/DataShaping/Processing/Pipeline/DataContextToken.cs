using System;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x0200008D RID: 141
	internal struct DataContextToken
	{
		// Token: 0x06000399 RID: 921 RVA: 0x0000BF26 File Offset: 0x0000A126
		internal DataContextToken(int matchConditionCount, int restorationContextIndex)
		{
			this._matchConditionCount = matchConditionCount;
			this._restorationContextIndex = restorationContextIndex;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000BF36 File Offset: 0x0000A136
		public int MatchConditionCount
		{
			get
			{
				return this._matchConditionCount;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000BF3E File Offset: 0x0000A13E
		public int RestorationContextIndex
		{
			get
			{
				return this._restorationContextIndex;
			}
		}

		// Token: 0x04000203 RID: 515
		private readonly int _matchConditionCount;

		// Token: 0x04000204 RID: 516
		private readonly int _restorationContextIndex;
	}
}
