using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Microsoft.ProgramSynthesis.Detection.Encoding
{
	// Token: 0x02000AD7 RID: 2775
	public static class EncodingTypeUtils
	{
		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x000D9750 File Offset: 0x000D7950
		private static Dictionary<EncodingType, string> EncodingTypeToDotNetName
		{
			get
			{
				return EncodingTypeUtils.EncodingTypeToDotNetNameLazy.Value;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x0600458C RID: 17804 RVA: 0x000D975C File Offset: 0x000D795C
		private static Dictionary<string, EncodingType> DotNetNameToEncodingType
		{
			get
			{
				return EncodingTypeUtils.DotNetNameToEncodingTypeLazy.Value;
			}
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x000D9768 File Offset: 0x000D7968
		public static EncodingType? GetEncodingTypeForDotNetName(string name)
		{
			EncodingType encodingType;
			if (EncodingTypeUtils.DotNetNameToEncodingType.TryGetValue(name, out encodingType))
			{
				return new EncodingType?(encodingType);
			}
			return null;
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x000D9794 File Offset: 0x000D7994
		public static string GetDotNetName(this EncodingType type)
		{
			string text;
			if (EncodingTypeUtils.EncodingTypeToDotNetName.TryGetValue(type, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x000D97B4 File Offset: 0x000D79B4
		public static Encoding GetEncoding(this EncodingType type)
		{
			if (type == EncodingType.Unknown)
			{
				return null;
			}
			string dotNetName = type.GetDotNetName();
			Encoding encoding;
			try
			{
				encoding = Encoding.GetEncoding(dotNetName);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(string.Format("The encoding type with numeric value '{0}' does not ", (int)type) + "have a supported encoding on this platform.", ex);
			}
			return encoding;
		}

		// Token: 0x04001FC6 RID: 8134
		private static readonly Lazy<Dictionary<EncodingType, string>> EncodingTypeToDotNetNameLazy = new Lazy<Dictionary<EncodingType, string>>(delegate
		{
			Dictionary<EncodingType, string> dictionary = new Dictionary<EncodingType, string>();
			dictionary[EncodingType.Ascii] = "us-ascii";
			dictionary[EncodingType.Iso88591] = "iso-8859-1";
			dictionary[EncodingType.Utf16Le] = "utf-16";
			dictionary[EncodingType.Utf16Be] = "utf-16BE";
			dictionary[EncodingType.Utf32Le] = "utf-32";
			dictionary[EncodingType.Utf32Be] = "utf-32BE";
			dictionary[EncodingType.Utf8] = "utf-8";
			dictionary[EncodingType.Windows1252] = "windows-1252";
			return dictionary;
		}, LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04001FC7 RID: 8135
		private static readonly Lazy<Dictionary<string, EncodingType>> DotNetNameToEncodingTypeLazy = new Lazy<Dictionary<string, EncodingType>>(() => EncodingTypeUtils.EncodingTypeToDotNetName.ToDictionary((KeyValuePair<EncodingType, string> t) => t.Value, (KeyValuePair<EncodingType, string> t) => t.Key));
	}
}
