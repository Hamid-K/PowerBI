using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.ProgramSynthesis.Detection.Encoding
{
	// Token: 0x02000AD2 RID: 2770
	public static class BomUtils
	{
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004576 RID: 17782 RVA: 0x000D9346 File Offset: 0x000D7546
		public static IReadOnlyDictionary<EncodingType, byte[]> BomPatterns { get; }

		// Token: 0x06004577 RID: 17783 RVA: 0x000D9350 File Offset: 0x000D7550
		public static byte[] GetBomPattern(this Encoding encoding)
		{
			EncodingType? encodingTypeForDotNetName = EncodingTypeUtils.GetEncodingTypeForDotNetName(encoding.WebName);
			byte[] array;
			if (encodingTypeForDotNetName != null && BomUtils.BomPatterns.TryGetValue(encodingTypeForDotNetName.Value, out array))
			{
				return array;
			}
			return null;
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x000D938C File Offset: 0x000D758C
		public static string GetStringWithoutBom(this Encoding encoding, byte[] encodedBytes, out bool bomDetected)
		{
			byte[] bomPattern = encoding.GetBomPattern();
			bomDetected = bomPattern != null && encodedBytes.Take(bomPattern.Length).SequenceEqual(bomPattern);
			int num = (bomDetected ? bomPattern.Length : 0);
			return encoding.GetString(encodedBytes, num, encodedBytes.Length - num);
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x000D93D0 File Offset: 0x000D75D0
		public static byte[] GetBytes(this Encoding encoding, string s, bool includeBom)
		{
			if (!includeBom)
			{
				return encoding.GetBytes(s);
			}
			byte[] bomPattern = encoding.GetBomPattern();
			byte[] array = new byte[encoding.GetByteCount(s) + ((bomPattern != null) ? bomPattern.Length : 0)];
			encoding.GetBytes(s, 0, s.Length, array, (bomPattern != null) ? bomPattern.Length : 0);
			if (bomPattern != null)
			{
				Array.Copy(bomPattern, array, bomPattern.Length);
			}
			return array;
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x000D9430 File Offset: 0x000D7630
		// Note: this type is marked as 'beforefieldinit'.
		static BomUtils()
		{
			Dictionary<EncodingType, byte[]> dictionary = new Dictionary<EncodingType, byte[]>();
			dictionary[EncodingType.Utf8] = new byte[] { 239, 187, 191 };
			dictionary[EncodingType.Utf16Le] = new byte[] { byte.MaxValue, 254 };
			dictionary[EncodingType.Utf16Be] = new byte[] { 254, byte.MaxValue };
			Dictionary<EncodingType, byte[]> dictionary2 = dictionary;
			EncodingType encodingType = EncodingType.Utf32Le;
			byte[] array = new byte[4];
			array[0] = byte.MaxValue;
			array[1] = 254;
			dictionary2[encodingType] = array;
			dictionary[EncodingType.Utf32Be] = new byte[] { 0, 0, 254, byte.MaxValue };
			BomUtils.BomPatterns = dictionary;
		}
	}
}
