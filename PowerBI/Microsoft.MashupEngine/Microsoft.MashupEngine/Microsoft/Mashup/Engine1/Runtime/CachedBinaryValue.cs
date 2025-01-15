using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001294 RID: 4756
	internal class CachedBinaryValue : StreamedBinaryValue
	{
		// Token: 0x06007CF4 RID: 31988 RVA: 0x001ACB68 File Offset: 0x001AAD68
		public CachedBinaryValue(IPersistentCache persistentCache, string key, BinaryValue value)
		{
			this.persistentCache = persistentCache;
			this.key = key;
			this.value = value;
		}

		// Token: 0x06007CF5 RID: 31989 RVA: 0x001ACB88 File Offset: 0x001AAD88
		public override Stream Open()
		{
			Stream stream;
			if (!this.persistentCache.TryGetValue(this.key, out stream))
			{
				stream = this.persistentCache.Add(this.key, this.value.Open());
			}
			return stream;
		}

		// Token: 0x06007CF6 RID: 31990 RVA: 0x001ACBD2 File Offset: 0x001AADD2
		public override bool TryGetLength(out long length)
		{
			return this.value.TryGetLength(out length);
		}

		// Token: 0x040044DF RID: 17631
		private IPersistentCache persistentCache;

		// Token: 0x040044E0 RID: 17632
		private string key;

		// Token: 0x040044E1 RID: 17633
		private BinaryValue value;
	}
}
