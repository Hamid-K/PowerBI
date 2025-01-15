using System;
using System.Text;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015D0 RID: 5584
	internal class PersistentCacheKeyBuilder
	{
		// Token: 0x06008C27 RID: 35879 RVA: 0x001D754C File Offset: 0x001D574C
		public void Add(string key)
		{
			StructuredCacheKeyExtensions.AppendCacheKeyPart(this.builder, key);
		}

		// Token: 0x06008C28 RID: 35880 RVA: 0x001D755A File Offset: 0x001D575A
		public override string ToString()
		{
			return this.builder.ToString();
		}

		// Token: 0x04004CB5 RID: 19637
		private readonly StringBuilder builder = new StringBuilder();
	}
}
