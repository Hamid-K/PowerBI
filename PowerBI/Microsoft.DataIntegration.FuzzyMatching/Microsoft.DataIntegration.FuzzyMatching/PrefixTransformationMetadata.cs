using System;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000101 RID: 257
	[Serializable]
	public struct PrefixTransformationMetadata : IBinarySerialize
	{
		// Token: 0x06000A82 RID: 2690 RVA: 0x0002F0EC File Offset: 0x0002D2EC
		public PrefixTransformationMetadata(ArraySegment<byte> metadata)
		{
			this.PrefixMatchLength = (this.MaxLength = 0);
			this.Read(metadata);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002F110 File Offset: 0x0002D310
		public void Write(ArraySegment<byte> metadata)
		{
			int offset = metadata.Offset;
			metadata.Array[offset++] = BitOperations.GetUpperByte(this.PrefixMatchLength);
			metadata.Array[offset++] = BitOperations.GetLowerByte(this.PrefixMatchLength);
			metadata.Array[offset++] = BitOperations.GetUpperByte(this.MaxLength);
			metadata.Array[offset++] = BitOperations.GetLowerByte(this.MaxLength);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002F188 File Offset: 0x0002D388
		public void Read(ArraySegment<byte> metadata)
		{
			int offset = metadata.Offset;
			this.PrefixMatchLength = (short)(((int)metadata.Array[offset++] << 8) | (int)metadata.Array[offset++]);
			this.MaxLength = (short)(((int)metadata.Array[offset++] << 8) | (int)metadata.Array[offset++]);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002F1E5 File Offset: 0x0002D3E5
		public void Write(BinaryWriter w)
		{
			w.Write(this.PrefixMatchLength);
			w.Write(this.MaxLength);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002F1FF File Offset: 0x0002D3FF
		public void Read(BinaryReader r)
		{
			this.PrefixMatchLength = r.ReadInt16();
			this.MaxLength = r.ReadInt16();
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002F219 File Offset: 0x0002D419
		public override string ToString()
		{
			return string.Format("<PrefixTransformation prefixMatchLength=\"{0}\" maxLength=\"{1}\"/>", this.PrefixMatchLength, this.MaxLength);
		}

		// Token: 0x040003F8 RID: 1016
		internal const int Length = 4;

		// Token: 0x040003F9 RID: 1017
		public short PrefixMatchLength;

		// Token: 0x040003FA RID: 1018
		public short MaxLength;
	}
}
