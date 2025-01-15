using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000023 RID: 35
	internal struct ManifestEnvelope
	{
		// Token: 0x040000A7 RID: 167
		public const int MaxChunkSize = 65280;

		// Token: 0x040000A8 RID: 168
		public ManifestEnvelope.ManifestFormats Format;

		// Token: 0x040000A9 RID: 169
		public byte MajorVersion;

		// Token: 0x040000AA RID: 170
		public byte MinorVersion;

		// Token: 0x040000AB RID: 171
		public byte Magic;

		// Token: 0x040000AC RID: 172
		public ushort TotalChunks;

		// Token: 0x040000AD RID: 173
		public ushort ChunkNumber;

		// Token: 0x0200008A RID: 138
		public enum ManifestFormats : byte
		{
			// Token: 0x040001B7 RID: 439
			SimpleXmlFormat = 1
		}
	}
}
