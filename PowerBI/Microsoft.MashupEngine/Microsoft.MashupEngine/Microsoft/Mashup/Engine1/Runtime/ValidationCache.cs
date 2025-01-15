using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200169A RID: 5786
	internal class ValidationCache<TKey> where TKey : class
	{
		// Token: 0x06009266 RID: 37478 RVA: 0x001E5718 File Offset: 0x001E3918
		public void Validate(TKey key, Action validator)
		{
			byte b = (byte)key.GetHashCode();
			if (this.validatedEntires[(int)b] == null || !key.Equals(this.validatedEntires[(int)b]))
			{
				validator();
				this.validatedEntires[(int)b] = key;
			}
		}

		// Token: 0x04004E84 RID: 20100
		private TKey[] validatedEntires = new TKey[256];
	}
}
