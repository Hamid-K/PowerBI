using System;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x02000005 RID: 5
	public class DedupRecord
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002142 File Offset: 0x00000342
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000214A File Offset: 0x0000034A
		internal string[] Row { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002153 File Offset: 0x00000353
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000215B File Offset: 0x0000035B
		internal int Freq { get; private set; }

		// Token: 0x0600000E RID: 14 RVA: 0x00002164 File Offset: 0x00000364
		private DedupRecord()
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000216C File Offset: 0x0000036C
		public DedupRecord(DataRow row, int start, int end)
		{
			int num = end - start;
			this.Row = new string[num];
			for (int i = 0; i < num; i++)
			{
				this.Row[i] = row[start + i].ToString();
			}
			this.Freq = 0;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021B8 File Offset: 0x000003B8
		public DedupRecord(IDataReader reader, int start, int end)
		{
			int num = end - start;
			this.Row = new string[num];
			for (int i = 0; i < num; i++)
			{
				this.Row[i] = reader.GetString(start + i);
			}
			this.Freq = 0;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002200 File Offset: 0x00000400
		public override int GetHashCode()
		{
			int num = 0;
			foreach (string obj in this.Row)
			{
				num ^= obj.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002234 File Offset: 0x00000434
		public override bool Equals(object other)
		{
			if (other == null || !(other is DedupRecord))
			{
				return false;
			}
			object[] row = ((DedupRecord)other).Row;
			object[] array = row;
			if (this.Row == array)
			{
				return true;
			}
			int num = this.Row.Length;
			if (array == null || array.Length != num)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (!this.Row[i].Equals(array[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000229C File Offset: 0x0000049C
		public override string ToString()
		{
			if (this.str == null)
			{
				this.str = string.Join(", ", this.Row);
			}
			return this.str;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022C4 File Offset: 0x000004C4
		internal DedupRecord ToLower()
		{
			DedupRecord dedupRecord = new DedupRecord();
			int num = this.Row.Length;
			dedupRecord.Row = new string[num];
			for (int i = 0; i < num; i++)
			{
				dedupRecord.Row[i] = this.Row[i].ToLower();
			}
			return dedupRecord;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002310 File Offset: 0x00000510
		internal void IncFreq()
		{
			int num = this.Freq + 1;
			this.Freq = num;
		}

		// Token: 0x04000005 RID: 5
		internal int dedupRid;

		// Token: 0x04000006 RID: 6
		private string str;
	}
}
