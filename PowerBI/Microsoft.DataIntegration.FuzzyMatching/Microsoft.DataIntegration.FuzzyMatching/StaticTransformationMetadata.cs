using System;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000106 RID: 262
	[Serializable]
	public struct StaticTransformationMetadata
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x0003079F File Offset: 0x0002E99F
		public StaticTransformationMetadata(ArraySegment<byte> metadata, ISegmentAllocator<int> allocator)
		{
			this.Context = default(TokenSequence);
			if (metadata.Count > 0)
			{
				this.Read(new BinaryReader(new MemoryStream(metadata.Array, metadata.Offset, metadata.Count)), allocator);
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000307DD File Offset: 0x0002E9DD
		public void Read(BinaryReader r, ISegmentAllocator<int> allocator)
		{
			this.Context.Read(r, allocator);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000307EC File Offset: 0x0002E9EC
		public void Write(BinaryWriter w)
		{
			this.Context.Write(w);
		}

		// Token: 0x04000416 RID: 1046
		public TokenSequence Context;
	}
}
