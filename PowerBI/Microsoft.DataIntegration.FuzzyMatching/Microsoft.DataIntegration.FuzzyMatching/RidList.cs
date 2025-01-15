using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200006D RID: 109
	[DebuggerDisplay("Length={Length} Rids={ToString()}")]
	[Serializable]
	public struct RidList
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x00015772 File Offset: 0x00013972
		public RidList(ArraySegment<int> segment)
		{
			this.Array = segment.Array;
			this.Offset = segment.Offset;
			this.Count = segment.Count;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001579B File Offset: 0x0001399B
		public RidList(int[] recordIdArray, int startIndex, int length)
		{
			this.Array = recordIdArray;
			this.Offset = startIndex;
			this.Count = length;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000157B4 File Offset: 0x000139B4
		internal void Write(Stream s)
		{
			if (this.Array == null)
			{
				StreamUtilities.WriteInt32(s, -1);
				return;
			}
			StreamUtilities.WriteInt32(s, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				StreamUtilities.WriteInt32(s, this.Array[i]);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000157FC File Offset: 0x000139FC
		internal static RidList Read(Stream s)
		{
			int num = StreamUtilities.ReadInt32(s);
			if (-1 == num)
			{
				return RidList.Empty;
			}
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = StreamUtilities.ReadInt32(s);
			}
			return new RidList(array, 0, num);
		}

		// Token: 0x17000105 RID: 261
		public int this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				return this.Array[this.Offset + index];
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00015850 File Offset: 0x00013A50
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = Math.Min(100, this.Count);
			for (int i = 0; i < num; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(this[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400018C RID: 396
		public static readonly RidList Empty = new RidList(null, 0, 0);

		// Token: 0x0400018D RID: 397
		public int[] Array;

		// Token: 0x0400018E RID: 398
		public int Offset;

		// Token: 0x0400018F RID: 399
		public int Count;
	}
}
