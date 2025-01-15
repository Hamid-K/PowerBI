using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.Libraries
{
	// Token: 0x020020D6 RID: 8406
	public sealed class MemoryLibrary : ILibrary
	{
		// Token: 0x0600CDF5 RID: 52725 RVA: 0x0028F51D File Offset: 0x0028D71D
		public MemoryLibrary(ILibraryProvider provider, string identifier, byte[] contents, bool allowWrite = true)
		{
			this.provider = provider;
			this.identifier = identifier;
			this.version = Utilities.CreateHash(contents);
			this.contents = contents;
			this.metadata = new Dictionary<string, byte[]>();
			this.allowWrite = allowWrite;
		}

		// Token: 0x0600CDF6 RID: 52726 RVA: 0x0028F55C File Offset: 0x0028D75C
		private MemoryLibrary(ILibrary library, string newIdentifier = null)
		{
			this.identifier = library.Identifier ?? newIdentifier;
			this.contents = library.Contents;
			this.metadata = new Dictionary<string, byte[]>();
			foreach (KeyValuePair<string, byte[]> keyValuePair in library.Metadata)
			{
				this.metadata[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x0600CDF7 RID: 52727 RVA: 0x0028F5EC File Offset: 0x0028D7EC
		public static MemoryLibrary New(ILibrary library, string newIdentifier = null)
		{
			MemoryLibrary memoryLibrary = library as MemoryLibrary;
			if (memoryLibrary == null || library.Identifier != newIdentifier)
			{
				memoryLibrary = new MemoryLibrary(library, newIdentifier);
			}
			return memoryLibrary;
		}

		// Token: 0x0600CDF8 RID: 52728 RVA: 0x0028F61C File Offset: 0x0028D81C
		public static MemoryLibrary Deserialize(BinaryReader reader)
		{
			string text = reader.ReadString();
			int num = reader.ReadInt32();
			MemoryLibrary memoryLibrary = new MemoryLibrary(null, text, reader.ReadBytes(num), true);
			int num2 = reader.ReadInt32();
			for (int i = 0; i < num2; i++)
			{
				string text2 = reader.ReadString();
				num = reader.ReadInt32();
				memoryLibrary.metadata[text2] = reader.ReadBytes(num);
			}
			return memoryLibrary;
		}

		// Token: 0x1700317C RID: 12668
		// (get) Token: 0x0600CDF9 RID: 52729 RVA: 0x0028F682 File Offset: 0x0028D882
		public ILibraryProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x1700317D RID: 12669
		// (get) Token: 0x0600CDFA RID: 52730 RVA: 0x0028F68A File Offset: 0x0028D88A
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700317E RID: 12670
		// (get) Token: 0x0600CDFB RID: 52731 RVA: 0x0028F692 File Offset: 0x0028D892
		public string Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700317F RID: 12671
		// (get) Token: 0x0600CDFC RID: 52732 RVA: 0x0028F69A File Offset: 0x0028D89A
		public byte[] Contents
		{
			get
			{
				return this.contents;
			}
		}

		// Token: 0x17003180 RID: 12672
		// (get) Token: 0x0600CDFD RID: 52733 RVA: 0x0028F6A2 File Offset: 0x0028D8A2
		public IEnumerable<KeyValuePair<string, byte[]>> Metadata
		{
			get
			{
				return this.metadata;
			}
		}

		// Token: 0x0600CDFE RID: 52734 RVA: 0x0028F6AC File Offset: 0x0028D8AC
		public bool TryGetMetadata(string metadataName, out byte[] metadata)
		{
			Dictionary<string, byte[]> dictionary = this.metadata;
			bool flag2;
			lock (dictionary)
			{
				flag2 = this.metadata.TryGetValue(metadataName, out metadata);
			}
			return flag2;
		}

		// Token: 0x0600CDFF RID: 52735 RVA: 0x0028F6F8 File Offset: 0x0028D8F8
		public bool TrySetMetadata(string metadataName, byte[] metadata)
		{
			if (!this.allowWrite)
			{
				return false;
			}
			Dictionary<string, byte[]> dictionary = this.metadata;
			bool flag2;
			lock (dictionary)
			{
				this.metadata[metadataName] = metadata;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600CE00 RID: 52736 RVA: 0x0028F74C File Offset: 0x0028D94C
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this.identifier);
			writer.Write(this.contents.Length);
			writer.Write(this.contents);
			writer.Write(this.metadata.Count);
			foreach (KeyValuePair<string, byte[]> keyValuePair in this.metadata)
			{
				writer.Write(keyValuePair.Key);
				writer.Write(keyValuePair.Value.Length);
				writer.Write(keyValuePair.Value);
			}
		}

		// Token: 0x04006821 RID: 26657
		private readonly ILibraryProvider provider;

		// Token: 0x04006822 RID: 26658
		private readonly string identifier;

		// Token: 0x04006823 RID: 26659
		private readonly string version;

		// Token: 0x04006824 RID: 26660
		private readonly byte[] contents;

		// Token: 0x04006825 RID: 26661
		private readonly Dictionary<string, byte[]> metadata;

		// Token: 0x04006826 RID: 26662
		private readonly bool allowWrite;
	}
}
