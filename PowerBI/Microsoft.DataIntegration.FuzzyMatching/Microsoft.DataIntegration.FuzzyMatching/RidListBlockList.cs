using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200006E RID: 110
	[Serializable]
	internal sealed class RidListBlockList : IMemoryUsage, IRawSerializable, ISerializable
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000158AD File Offset: 0x00013AAD
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x000158B5 File Offset: 0x00013AB5
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000158BE File Offset: 0x00013ABE
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x000158C6 File Offset: 0x00013AC6
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x060004A5 RID: 1189 RVA: 0x000158CF File Offset: 0x00013ACF
		public RidListBlockList(int ridListLength)
		{
			this.RidListLength = ridListLength;
			this.m_blocks = new List<int[]>();
			this.m_freeReferences = new Stack<int>();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000158F4 File Offset: 0x00013AF4
		private RidListBlockList(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.RidListLength = (int)info.GetValue("RidListLength", typeof(int));
			this.m_ridListCount = (int)info.GetValue("m_ridListCount", typeof(int));
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				this.m_blocks = (List<int[]>)info.GetValue("m_blocks", typeof(List<int[]>));
				this.m_freeReferences = (Stack<int>)info.GetValue("m_freeReferences", typeof(Stack<int>));
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000159D0 File Offset: 0x00013BD0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("RidListLength", this.RidListLength);
			info.AddValue("m_ridListCount", this.m_ridListCount);
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				info.AddValue("m_blocks", this.m_blocks);
				info.AddValue("m_freeReferences", this.m_freeReferences);
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00015A4C File Offset: 0x00013C4C
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.m_blocks.Count);
			for (int i = 0; i < this.m_blocks.Count; i++)
			{
				int[] array = this.m_blocks[i];
				StreamUtilities.WriteInt32(s, array.Length);
				for (int j = 0; j < array.Length; j++)
				{
					StreamUtilities.WriteInt32(s, array[j]);
				}
			}
			StreamUtilities.WriteInt32(s, this.m_freeReferences.Count);
			foreach (int num in this.m_freeReferences)
			{
				StreamUtilities.WriteInt32(s, num);
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00015B20 File Offset: 0x00013D20
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			int num = StreamUtilities.ReadInt32(s);
			this.m_blocks = new List<int[]>(num);
			for (int i = 0; i < num; i++)
			{
				int[] array = new int[StreamUtilities.ReadInt32(s)];
				this.m_blocks.Add(array);
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = StreamUtilities.ReadInt32(s);
				}
			}
			num = StreamUtilities.ReadInt32(s);
			this.m_freeReferences = new Stack<int>(num);
			for (int k = 0; k < num; k++)
			{
				this.m_freeReferences.Push(StreamUtilities.ReadInt32(s));
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00015BE1 File Offset: 0x00013DE1
		public long MemoryUsage
		{
			get
			{
				return (long)(this.RidListLength * 65536 * this.m_blocks.Count);
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00015BFC File Offset: 0x00013DFC
		public void GetRidList(int ridListReference, out int[] recordIdArray, out int startIndex)
		{
			recordIdArray = this.m_blocks[ridListReference / 65536];
			startIndex = this.RidListLength * (ridListReference % 65536);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00015C24 File Offset: 0x00013E24
		public void AllocRidList(out int ridListReference, out int[] recordIdArray, out int startIndex)
		{
			if (this.m_freeReferences.Count > 0)
			{
				ridListReference = this.m_freeReferences.Pop();
				recordIdArray = this.m_blocks[ridListReference / 65536];
				startIndex = this.RidListLength * (ridListReference % 65536);
				for (int i = startIndex; i < startIndex + this.RidListLength; i++)
				{
					if (recordIdArray[i] != 0)
					{
						throw new Exception("Trying to overwrite a ridlist that has not been freed properly.  This likely indicates a bug in FuzzyLookup.");
					}
				}
			}
			else
			{
				if (this.m_ridListCount == this.m_blocks.Count * 65536)
				{
					this.m_blocks.Add(new int[this.RidListLength * 65536]);
				}
				ridListReference = (this.m_blocks.Count - 1) * 65536 + this.m_ridListCount % 65536;
				recordIdArray = this.m_blocks[ridListReference / 65536];
				startIndex = this.RidListLength * (ridListReference % 65536);
			}
			this.m_ridListCount++;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00015D28 File Offset: 0x00013F28
		public void FreeRidList(int ridListReference)
		{
			int[] array = this.m_blocks[ridListReference / 65536];
			int num = this.RidListLength * (ridListReference % 65536);
			for (int i = num; i < num + this.RidListLength; i++)
			{
				array[i] = 0;
			}
			this.m_freeReferences.Push(ridListReference);
			this.m_ridListCount--;
		}

		// Token: 0x04000190 RID: 400
		private const int RidListsPerBlock = 65536;

		// Token: 0x04000191 RID: 401
		internal readonly int RidListLength;

		// Token: 0x04000192 RID: 402
		private List<int[]> m_blocks;

		// Token: 0x04000193 RID: 403
		private Stack<int> m_freeReferences;

		// Token: 0x04000194 RID: 404
		private int m_ridListCount;
	}
}
