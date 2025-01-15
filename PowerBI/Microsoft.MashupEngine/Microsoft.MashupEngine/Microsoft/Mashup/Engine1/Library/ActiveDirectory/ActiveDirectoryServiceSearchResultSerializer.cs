using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FD9 RID: 4057
	internal class ActiveDirectoryServiceSearchResultSerializer
	{
		// Token: 0x06006A65 RID: 27237 RVA: 0x0016E28F File Offset: 0x0016C48F
		public static void Serialize(IPersistentCache cache, string key, ActiveDirectoryServiceSearchResult result)
		{
			ActiveDirectoryDataWriter activeDirectoryDataWriter = new ActiveDirectoryDataWriter(cache, key, cache.MaxEntryLength);
			activeDirectoryDataWriter.Write(result);
			activeDirectoryDataWriter.WriteResultEnd();
		}

		// Token: 0x06006A66 RID: 27238 RVA: 0x0016E2AC File Offset: 0x0016C4AC
		public static IEnumerable<ActiveDirectoryServiceSearchResult> Deserialize(Stream stream)
		{
			ActiveDirectoryServiceSearchResultSerializer.ActiveDirectoryDataReader activeDirectoryDataReader = new ActiveDirectoryServiceSearchResultSerializer.ActiveDirectoryDataReader(stream);
			return activeDirectoryDataReader.ReadResults();
		}

		// Token: 0x06006A67 RID: 27239 RVA: 0x0016E2C8 File Offset: 0x0016C4C8
		public static void SerializeActiveDirectoryRootServiceEntry(IPersistentCache cache, string key, ActiveDirectoryRootServiceEntry activeDirectoryRootServiceEntry)
		{
			ActiveDirectoryDataWriter activeDirectoryDataWriter = new ActiveDirectoryDataWriter(cache, key, cache.MaxEntryLength);
			activeDirectoryDataWriter.Write(activeDirectoryRootServiceEntry);
			activeDirectoryDataWriter.WriteResultEnd();
		}

		// Token: 0x06006A68 RID: 27240 RVA: 0x0016E2E4 File Offset: 0x0016C4E4
		public static ActiveDirectoryRootServiceEntry DeserializeActiveDirectoryRootServiceEntry(Stream stream)
		{
			ActiveDirectoryServiceSearchResultSerializer.ActiveDirectoryDataReader activeDirectoryDataReader = new ActiveDirectoryServiceSearchResultSerializer.ActiveDirectoryDataReader(stream);
			return activeDirectoryDataReader.ReadActiveDirectoryRootServiceEntry();
		}

		// Token: 0x02000FDA RID: 4058
		private struct ActiveDirectoryDataReader
		{
			// Token: 0x06006A6A RID: 27242 RVA: 0x0016E300 File Offset: 0x0016C500
			public ActiveDirectoryDataReader(Stream stream)
			{
				this.reader = new BinaryReader(stream);
			}

			// Token: 0x06006A6B RID: 27243 RVA: 0x0016E30E File Offset: 0x0016C50E
			public IEnumerable<ActiveDirectoryServiceSearchResult> ReadResults()
			{
				while (this.reader.ReadBoolean())
				{
					yield return this.ReadResult();
				}
				yield break;
			}

			// Token: 0x06006A6C RID: 27244 RVA: 0x0016E324 File Offset: 0x0016C524
			private ActiveDirectoryServiceSearchResult ReadResult()
			{
				int num = this.reader.ReadInt32();
				Dictionary<string, object[]> dictionary = new Dictionary<string, object[]>(num, StringComparer.OrdinalIgnoreCase);
				for (int i = 0; i < num; i++)
				{
					string text = this.reader.ReadString();
					int num2 = this.reader.ReadInt32();
					object[] array = new object[num2];
					for (int j = 0; j < num2; j++)
					{
						array[j] = this.reader.ReadObject(this.reader.ReadObjectTag());
					}
					dictionary.Add(text, array);
				}
				return new ActiveDirectoryCachingServiceSearchResult(dictionary);
			}

			// Token: 0x06006A6D RID: 27245 RVA: 0x0016E3B4 File Offset: 0x0016C5B4
			public ActiveDirectoryRootServiceEntry ReadActiveDirectoryRootServiceEntry()
			{
				return new ActiveDirectoryRootServiceEntry
				{
					ConfigurationNamingContext = this.reader.ReadString(),
					DefaultNamingContext = this.reader.ReadString(),
					RootDomainNamingContext = this.reader.ReadString(),
					SchemaNamingContext = this.reader.ReadString()
				};
			}

			// Token: 0x04003B1B RID: 15131
			private readonly BinaryReader reader;
		}
	}
}
