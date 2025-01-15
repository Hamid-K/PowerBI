using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001F8 RID: 504
	internal class WriteBackLookupKey
	{
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00036F94 File Offset: 0x00035194
		public AOMCacheItem OmItem
		{
			get
			{
				return this._omItem;
			}
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00036F9C File Offset: 0x0003519C
		public WriteBackLookupKey(AOMCacheItem item)
		{
			this._omItem = item;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00003CAB File Offset: 0x00001EAB
		public override bool Equals(object obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00036FAB File Offset: 0x000351AB
		public override int GetHashCode()
		{
			return WriteBackItem.ComputeHashCode(this._omItem);
		}

		// Token: 0x04000AC8 RID: 2760
		private readonly AOMCacheItem _omItem;
	}
}
