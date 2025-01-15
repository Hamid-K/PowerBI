using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200021E RID: 542
	internal class DMMultiLevelHashTableSchema : IIndexStoreSchema
	{
		// Token: 0x06001204 RID: 4612 RVA: 0x00002061 File Offset: 0x00000261
		internal DMMultiLevelHashTableSchema()
		{
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000397C7 File Offset: 0x000379C7
		internal DMMultiLevelHashTableSchema(int level)
		{
			this._level = level;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x000397D6 File Offset: 0x000379D6
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x000397DE File Offset: 0x000379DE
		public int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		// Token: 0x04000B0B RID: 2827
		private int _level;
	}
}
