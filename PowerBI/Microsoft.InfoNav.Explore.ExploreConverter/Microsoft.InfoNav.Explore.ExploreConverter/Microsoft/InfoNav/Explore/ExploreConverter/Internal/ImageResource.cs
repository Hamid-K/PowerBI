using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200005B RID: 91
	internal sealed class ImageResource
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0000B299 File Offset: 0x00009499
		internal ImageResource(byte[] bytes)
		{
			this._bytes = bytes;
			this._encodedBytes = Convert.ToBase64String(bytes);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000B2B4 File Offset: 0x000094B4
		internal byte[] Bytes
		{
			get
			{
				return this._bytes;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000B2BC File Offset: 0x000094BC
		internal string EncodedBytes
		{
			get
			{
				return this._encodedBytes;
			}
		}

		// Token: 0x04000151 RID: 337
		private readonly byte[] _bytes;

		// Token: 0x04000152 RID: 338
		private readonly string _encodedBytes;
	}
}
