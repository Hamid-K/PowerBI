using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000069 RID: 105
	public sealed class CompactStringSetLongId : CompactStringSetBase<long>
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0001B34D File Offset: 0x0001954D
		public CompactStringSetLongId()
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001B36B File Offset: 0x0001956B
		public CompactStringSetLongId(Func<string, string> normalizeString)
			: base(normalizeString)
		{
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001B38A File Offset: 0x0001958A
		public CompactStringSetLongId(Func<string, string> normalizeString, Func<string, string> normalizeRetrievedString)
			: base(normalizeString, normalizeRetrievedString)
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001B3AA File Offset: 0x000195AA
		protected override bool Hash2Id_TryGetValue(int hashCode, out long id)
		{
			return this.m_hash2Id.TryGetValue(hashCode, ref id);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001B3B9 File Offset: 0x000195B9
		protected override void Hash2Id_SetValue(int hashCode, long id)
		{
			this.m_hash2Id[hashCode] = id;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001B3C8 File Offset: 0x000195C8
		protected override bool Hash2Id_TryAdd(int hashCode, long id)
		{
			if (this.m_hash2Id.ContainsKey(hashCode))
			{
				return false;
			}
			this.m_hash2Id[hashCode] = id;
			return true;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001B3E8 File Offset: 0x000195E8
		protected override bool Id2Position_TryGetValue(long id, out int position)
		{
			return this.m_id2position.TryGetValue(id, ref position);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001B3F7 File Offset: 0x000195F7
		protected override int Id2Position_GetValue(long id)
		{
			return this.m_id2position[id];
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001B405 File Offset: 0x00019605
		protected override bool Id2Position_TryAdd(long id, int position)
		{
			if (this.m_id2position.ContainsKey(id))
			{
				return false;
			}
			this.m_id2position[id] = position;
			return true;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001B425 File Offset: 0x00019625
		protected override void Id2Position_Remove(long id)
		{
			this.m_id2position.Remove(id);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001B434 File Offset: 0x00019634
		protected override bool Id2Position_ContainsKey(long id)
		{
			return this.m_id2position.ContainsKey(id);
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001B442 File Offset: 0x00019642
		protected override long ZeroId
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0001B446 File Offset: 0x00019646
		protected override long NegativeOneId
		{
			get
			{
				return -1L;
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001B44A File Offset: 0x0001964A
		protected override long Negate(long id)
		{
			return -id;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001B44E File Offset: 0x0001964E
		protected override long ConvertFromInt(int id)
		{
			return (long)id;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001B452 File Offset: 0x00019652
		protected override bool IsNegative(long id)
		{
			return id < 0L;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001B459 File Offset: 0x00019659
		protected override bool Equals(long id1, long id2)
		{
			return id1 == id2;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001B45F File Offset: 0x0001965F
		protected override long NextId()
		{
			if (this.m_nextId + 1L == 9223372036854775807L)
			{
				throw new Exception(string.Format("Exceeded distinct string limit of {0}.", long.MaxValue));
			}
			return Interlocked.Increment(ref this.m_nextId);
		}

		// Token: 0x040000B3 RID: 179
		private long m_nextId;

		// Token: 0x040000B4 RID: 180
		private Dictionary<int, long> m_hash2Id = new Dictionary<int, long>();

		// Token: 0x040000B5 RID: 181
		private Dictionary<long, int> m_id2position = new Dictionary<long, int>();
	}
}
