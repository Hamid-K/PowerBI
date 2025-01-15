using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x0200029C RID: 668
	internal static class AdalCacheOperations
	{
		// Token: 0x06001945 RID: 6469 RVA: 0x00052DF8 File Offset: 0x00050FF8
		public static byte[] Serialize(ILoggerAdapter logger, IDictionary<AdalTokenCacheKey, AdalResultWrapper> tokenCacheDictionary)
		{
			byte[] array;
			using (Stream stream = new MemoryStream())
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				binaryWriter.Write(3);
				logger.Info(() => string.Format("[AdalCacheOperations] Serializing token cache with {0} items. ", tokenCacheDictionary.Count));
				binaryWriter.Write(tokenCacheDictionary.Count);
				foreach (KeyValuePair<AdalTokenCacheKey, AdalResultWrapper> keyValuePair in tokenCacheDictionary)
				{
					AdalTokenCacheKey key = keyValuePair.Key;
					binaryWriter.Write(string.Format("{0}{1}{2}{3}{4}{5}{6}", new object[]
					{
						key.Authority,
						":::",
						key.Resource,
						":::",
						key.ClientId,
						":::",
						(int)key.TokenSubjectType
					}));
					binaryWriter.Write(keyValuePair.Value.Serialize());
				}
				int num = (int)stream.Position;
				stream.Position = 0L;
				array = new BinaryReader(stream).ReadBytes(num);
			}
			return array;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00052F3C File Offset: 0x0005113C
		public static IDictionary<AdalTokenCacheKey, AdalResultWrapper> Deserialize(ILoggerAdapter logger, byte[] state)
		{
			IDictionary<AdalTokenCacheKey, AdalResultWrapper> dictionary = new Dictionary<AdalTokenCacheKey, AdalResultWrapper>();
			if (state == null || state.Length == 0)
			{
				return dictionary;
			}
			using (Stream stream = new MemoryStream())
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				binaryWriter.Write(state);
				binaryWriter.Flush();
				stream.Position = 0L;
				BinaryReader binaryReader = new BinaryReader(stream);
				if (binaryReader.ReadInt32() != 3)
				{
					logger.Warning("[AdalCacheOperations] The version of the persistent state of the cache does not match the current schema, so skipping deserialization. ");
					return dictionary;
				}
				int num = binaryReader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					string[] array = binaryReader.ReadString().Split(new string[] { ":::" }, StringSplitOptions.None);
					AdalResultWrapper adalResultWrapper = AdalResultWrapper.Deserialize(binaryReader.ReadString());
					AdalTokenCacheKey adalTokenCacheKey = new AdalTokenCacheKey(array[0], array[1], array[2], (TokenSubjectType)int.Parse(array[3], CultureInfo.CurrentCulture), adalResultWrapper.Result.UserInfo);
					dictionary[adalTokenCacheKey] = adalResultWrapper;
				}
				logger.Info(() => string.Format("[AdalCacheOperations] Deserialized {0} items to ADAL token cache. ", dictionary.Count));
			}
			return dictionary;
		}

		// Token: 0x04000B5A RID: 2906
		private const int SchemaVersion = 3;

		// Token: 0x04000B5B RID: 2907
		private const string Delimiter = ":::";
	}
}
