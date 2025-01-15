using System;
using System.Collections;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FDC RID: 4060
	internal class ActiveDirectoryDataWriter
	{
		// Token: 0x06006A76 RID: 27254 RVA: 0x0016E4D3 File Offset: 0x0016C6D3
		public ActiveDirectoryDataWriter(IPersistentCache cache, string key, long maxEntryLength)
		{
			this.stream = new PersistentCacheExtensions.WriteOnlyCachingStream(cache, key, maxEntryLength, null);
			this.writer = new BinaryWriter(this.stream);
		}

		// Token: 0x06006A77 RID: 27255 RVA: 0x0016E500 File Offset: 0x0016C700
		public void Write(ActiveDirectoryServiceSearchResult result)
		{
			this.writer.Write(true);
			this.writer.Write(result.AttributeNames.Count);
			foreach (string text in result.AttributeNames)
			{
				this.writer.Write(text);
				ICollection attribute = result.GetAttribute(text);
				this.writer.Write(attribute.Count);
				foreach (object obj in attribute)
				{
					this.writer.WriteObject(obj);
				}
			}
		}

		// Token: 0x06006A78 RID: 27256 RVA: 0x0016E5D8 File Offset: 0x0016C7D8
		public void Write(ActiveDirectoryRootServiceEntry activeDirectoryRootServiceEntry)
		{
			this.writer.Write(activeDirectoryRootServiceEntry.ConfigurationNamingContext);
			this.writer.Write(activeDirectoryRootServiceEntry.DefaultNamingContext);
			this.writer.Write(activeDirectoryRootServiceEntry.RootDomainNamingContext);
			this.writer.Write(activeDirectoryRootServiceEntry.SchemaNamingContext);
		}

		// Token: 0x06006A79 RID: 27257 RVA: 0x0016E629 File Offset: 0x0016C829
		public void WriteResultEnd()
		{
			this.writer.Write(false);
			this.writer.Flush();
			this.stream.Close();
		}

		// Token: 0x04003B21 RID: 15137
		private readonly BinaryWriter writer;

		// Token: 0x04003B22 RID: 15138
		private PersistentCacheExtensions.WriteOnlyCachingStream stream;
	}
}
