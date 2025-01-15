using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000068 RID: 104
	public sealed class CompactStringSet : CompactStringSetBase<int>, IEnumerable<KeyValuePair<int, string>>, IEnumerable
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0001B207 File Offset: 0x00019407
		public CompactStringSet()
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001B225 File Offset: 0x00019425
		public CompactStringSet(Func<string, string> normalizeString)
			: base(normalizeString)
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001B244 File Offset: 0x00019444
		public CompactStringSet(Func<string, string> normalizeString, Func<string, string> normalizeRetrievedString)
			: base(normalizeString, normalizeRetrievedString)
		{
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001B264 File Offset: 0x00019464
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001B26C File Offset: 0x0001946C
		public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
		{
			foreach (KeyValuePair<int, int> keyValuePair in this.m_id2position)
			{
				yield return new KeyValuePair<int, string>(keyValuePair.Key, base.GetString(keyValuePair.Key));
			}
			IEnumerator<KeyValuePair<int, int>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001B27B File Offset: 0x0001947B
		protected override bool Hash2Id_TryGetValue(int hashCode, out int id)
		{
			return this.m_hash2Id.TryGetValue(hashCode, out id);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001B28A File Offset: 0x0001948A
		protected override void Hash2Id_SetValue(int hashCode, int id)
		{
			this.m_hash2Id[hashCode] = id;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001B299 File Offset: 0x00019499
		protected override bool Hash2Id_TryAdd(int hashCode, int id)
		{
			return this.m_hash2Id.TryAdd(hashCode, id);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001B2A8 File Offset: 0x000194A8
		protected override bool Id2Position_TryGetValue(int id, out int position)
		{
			return this.m_id2position.TryGetValue(id, out position);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001B2B7 File Offset: 0x000194B7
		protected override int Id2Position_GetValue(int id)
		{
			return this.m_id2position[id];
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001B2C5 File Offset: 0x000194C5
		protected override bool Id2Position_TryAdd(int id, int position)
		{
			return this.m_id2position.TryAdd(id, position);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001B2D4 File Offset: 0x000194D4
		protected override void Id2Position_Remove(int id)
		{
			int num;
			this.m_id2position.TryRemove(id, out num);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001B2F0 File Offset: 0x000194F0
		protected override bool Id2Position_ContainsKey(int id)
		{
			return this.m_id2position.ContainsKey(id);
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0001B2FE File Offset: 0x000194FE
		protected override int ZeroId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001B301 File Offset: 0x00019501
		protected override int NegativeOneId
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001B304 File Offset: 0x00019504
		protected override int Negate(int id)
		{
			return -id;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001B308 File Offset: 0x00019508
		protected override int ConvertFromInt(int id)
		{
			return id;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001B30B File Offset: 0x0001950B
		protected override bool IsNegative(int id)
		{
			return id < 0;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001B311 File Offset: 0x00019511
		protected override bool Equals(int id1, int id2)
		{
			return id1 == id2;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001B317 File Offset: 0x00019517
		protected override int NextId()
		{
			if (this.m_nextId + 1 == 2147483647)
			{
				throw new Exception(string.Format("Exceeded distinct string limit of {0}.", int.MaxValue));
			}
			return Interlocked.Increment(ref this.m_nextId);
		}

		// Token: 0x040000B0 RID: 176
		private int m_nextId;

		// Token: 0x040000B1 RID: 177
		private ConcurrentDictionary<int, int> m_hash2Id = new ConcurrentDictionary<int, int>();

		// Token: 0x040000B2 RID: 178
		private ConcurrentDictionary<int, int> m_id2position = new ConcurrentDictionary<int, int>();
	}
}
