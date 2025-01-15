using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy
{
	// Token: 0x020000D5 RID: 213
	[NullableContext(1)]
	[Nullable(0)]
	public static class PrivateInformation
	{
		// Token: 0x0600108C RID: 4236 RVA: 0x00045827 File Offset: 0x00043A27
		public static bool ContainsPrivateInformation(Type type)
		{
			return PrivateInformation.c_iContainsPrivateInformationType.IsAssignableFrom(type);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00045834 File Offset: 0x00043A34
		public static string MarkAsUserContent(this string userContent)
		{
			return userContent.MarkAsCustomerContent();
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0004583C File Offset: 0x00043A3C
		public static string MarkAsModelInfo(this string modelInfo)
		{
			return modelInfo.MarkAsCustomerContent();
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00045844 File Offset: 0x00043A44
		public static string MarkAsUtterance(this string utterance)
		{
			return utterance.MarkAsCustomerContent();
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0004584C File Offset: 0x00043A4C
		public static string MarkAsPrivate(this string plainString)
		{
			if (plainString == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < plainString.Length; i++)
			{
				if (plainString[i] == '<')
				{
					int num = plainString.IndexOf('>', i, Math.Min(i + "</pii>".Length, plainString.Length) - i);
					if (num != -1 && PrivateInformation.c_PrivateTagNamesMappedToItsLength.ContainsKey(plainString.Substring(i, num - i + 1)))
					{
						i = num;
					}
					else
					{
						stringBuilder.Append(plainString[i]);
					}
				}
				else if (plainString[i] == '\0')
				{
					stringBuilder.Append(' ');
				}
				else
				{
					stringBuilder.Append(plainString[i]);
				}
			}
			return PrivateInformation.MarkString(stringBuilder.ToString(), "pi");
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0004590F File Offset: 0x00043B0F
		public static string MarkAsPrivate(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			return pi.ToPrivateString();
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00045920 File Offset: 0x00043B20
		public static string MarkAsEUII(this string plainString)
		{
			return plainString.MarkWithMarkupTag("euii");
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0004592D File Offset: 0x00043B2D
		public static string MarkAsEUPI(this string plainString)
		{
			return plainString.MarkWithMarkupTag("eupi");
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0004593C File Offset: 0x00043B3C
		public static string MarkAsIPAddress(this string plainString)
		{
			if (plainString.Length >= 60)
			{
				return "INPUT STRING IS NOT VALID IP ADDRESS";
			}
			IPAddress ipaddress;
			if (IPAddress.TryParse(plainString, out ipaddress))
			{
				return "<ip>" + plainString + "</ip>";
			}
			return "INPUT STRING IS NOT VALID IP ADDRESS";
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00045979 File Offset: 0x00043B79
		public static string MarkAsCustomerContent(this string plainString)
		{
			return plainString.MarkWithMarkupTag("ccon");
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00045986 File Offset: 0x00043B86
		private static string MarkString(string plainString, string markupTagName)
		{
			if (plainString == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(plainString.Length + 2);
			stringBuilder.AppendStartTag(markupTagName);
			stringBuilder.Append(plainString);
			stringBuilder.AppendEndTag(markupTagName);
			return stringBuilder.ToString();
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000459B8 File Offset: 0x00043BB8
		public static string ObfuscatePrivateValue(this string value, bool substringMarkup = false)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			if (substringMarkup)
			{
				return value.RemovePrivateMarkup((string s) => Obfuscation.Obfuscate(s, false));
			}
			if (!value.StartsWithTag("pi"))
			{
				return value;
			}
			return Obfuscation.Obfuscate(value.RemovePrivateMarkup(), false);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00045A13 File Offset: 0x00043C13
		public static string ScrubPII(this string plainString)
		{
			if (plainString == null)
			{
				return string.Empty;
			}
			return Obfuscation.Obfuscate(plainString, false);
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00045A25 File Offset: 0x00043C25
		public static string ScrubPII(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			return pi.ToPrivateString();
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00045A38 File Offset: 0x00043C38
		public static string ScrubIfPII(this object o)
		{
			if (o == null)
			{
				return string.Empty;
			}
			IContainsPrivateInformation containsPrivateInformation = o as IContainsPrivateInformation;
			if (containsPrivateInformation == null)
			{
				return o.ToString();
			}
			return containsPrivateInformation.ScrubPII();
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00045A65 File Offset: 0x00043C65
		public static string TagAsPII(this string plainString)
		{
			return PrivateInformation.MarkString(plainString, "pi");
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00045A72 File Offset: 0x00043C72
		public static string RemovePrivateMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_privateTagNamesArray, Array.Empty<Func<string, string>>());
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00045A84 File Offset: 0x00043C84
		public static string RemovePrivateMarkup(this string markedUpString, Func<string, string> stringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_privateTagNamesArray, new Func<string, string>[] { stringTransform });
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00045A9B File Offset: 0x00043C9B
		public static string RemoveInternalMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_internalTagNamesArray, Array.Empty<Func<string, string>>());
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00045AAD File Offset: 0x00043CAD
		public static string RemoveInternalMarkup(this string markedUpString, Func<string, string> stringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_internalTagNamesArray, new Func<string, string>[] { stringTransform });
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00045AC4 File Offset: 0x00043CC4
		public static string RemovePrivateAndInternalMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, null, null);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00045ACE File Offset: 0x00043CCE
		public static string RemovePrivateAndInternalMarkup(this string markedUpString, Func<string, string> privateStringTransform, Func<string, string> internalStringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, privateStringTransform, internalStringTransform);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00045AD8 File Offset: 0x00043CD8
		private static string RemoveMarkup(string markedUpString, Func<string, string> privateStringTransform = null, Func<string, string> internalStringTransform = null)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_allTagNamesArray, new Func<string, string>[] { privateStringTransform, internalStringTransform });
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00045AF4 File Offset: 0x00043CF4
		private static string RemoveMarkup(string markedUpString, string[] tagNames, params Func<string, string>[] stringTransforms)
		{
			if (markedUpString == null)
			{
				return null;
			}
			int num2;
			int num = markedUpString.IndexOfStartTag(tagNames, 0, out num2);
			if (num < 0)
			{
				return markedUpString;
			}
			int num3 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			do
			{
				string text = markedUpString.Substring(num3, num - num3);
				stringBuilder.Append(text);
				string text2 = tagNames[num2];
				int num4 = num + text2.GetStartTagLength();
				int num5 = markedUpString.IndexOfEndTag(text2, num4);
				if (num5 < 0)
				{
					num5 = markedUpString.Length;
				}
				string text3 = markedUpString.Substring(num4, num5 - num4);
				if (stringTransforms != null && stringTransforms.Length > num2 && stringTransforms[num2] != null)
				{
					text3 = stringTransforms[num2](text3);
				}
				stringBuilder.Append(text3);
				num3 = num5 + text2.GetEndTagLength();
				num = markedUpString.IndexOfStartTag(tagNames, num3, out num2);
			}
			while (num >= 0);
			if (num3 < markedUpString.Length)
			{
				stringBuilder.Append(markedUpString.Substring(num3));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00045BC9 File Offset: 0x00043DC9
		public static string UntagPII(this string taggedString)
		{
			return taggedString.RemoveInternalMarkup();
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00045BD1 File Offset: 0x00043DD1
		public static ScrubbedString ToScrubbedString(this string value)
		{
			return new ScrubbedString(value);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00045BDC File Offset: 0x00043DDC
		public static string Format(IFormatProvider formatProvider, string format, object[] args, bool traceString = false)
		{
			if (args == null || args.Length == 0)
			{
				return format;
			}
			int num = args.Length;
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				IContainsPrivateInformation containsPrivateInformation = args[i] as IContainsPrivateInformation;
				if (containsPrivateInformation == null)
				{
					array[i] = args[i];
				}
				else if (traceString)
				{
					array[i] = containsPrivateInformation.ToPrivateString();
				}
				else
				{
					array[i] = containsPrivateInformation.ToOriginalString();
				}
			}
			return string.Format(formatProvider, format, array);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00045C3C File Offset: 0x00043E3C
		public static string FormatInvariant(string format, object[] args, bool traceString = false)
		{
			return PrivateInformation.Format(CultureInfo.InvariantCulture, format, args, traceString);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00045C4B File Offset: 0x00043E4B
		private static void AppendStartTag(this StringBuilder builder, string markupTagName)
		{
			builder.Append('<');
			builder.Append(markupTagName);
			builder.Append('>');
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00045C67 File Offset: 0x00043E67
		private static void AppendEndTag(this StringBuilder builder, string markupTagName)
		{
			builder.Append('<');
			builder.Append('/');
			builder.Append(markupTagName);
			builder.Append('>');
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00045C8C File Offset: 0x00043E8C
		internal static bool StartsWithTag(this string value, string markupTagName)
		{
			int length = markupTagName.Length;
			if (value == null || value.Length < length + 2)
			{
				return false;
			}
			if (value[0] != '<')
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				if (value[i + 1] != markupTagName[i])
				{
					return false;
				}
			}
			return value[length + 1] == '>';
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00045CED File Offset: 0x00043EED
		internal static int GetStartTagLength(this string tagName)
		{
			return tagName.Length + 2;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00045CF7 File Offset: 0x00043EF7
		internal static int GetEndTagLength(this string tagName)
		{
			return tagName.Length + 3;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00045D04 File Offset: 0x00043F04
		internal static int IndexOfStartTag(this string value, string[] tagNames, int startIx, out int tagNameIx)
		{
			tagNameIx = -1;
			if (string.IsNullOrEmpty(value))
			{
				return -1;
			}
			if (startIx >= value.Length)
			{
				return -1;
			}
			for (int i = value.IndexOf('<', startIx); i >= 0; i = value.IndexOf('<', i + 1))
			{
				for (int j = 0; j < tagNames.Length; j++)
				{
					string text = tagNames[j];
					int startTagLength = text.GetStartTagLength();
					int num = i + 1;
					if (value.Length >= i + startTagLength)
					{
						int num2 = 0;
						while (num2 < text.Length && value[num + num2] == text[num2])
						{
							num2++;
						}
						if (num2 == text.Length && value[i + startTagLength - 1] == '>')
						{
							tagNameIx = j;
							return i;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00045DBC File Offset: 0x00043FBC
		internal static int IndexOfEndTag(this string value, string tagName, int startIx)
		{
			if (string.IsNullOrEmpty(value))
			{
				return -1;
			}
			if (startIx >= value.Length)
			{
				return -1;
			}
			int endTagLength = tagName.GetEndTagLength();
			for (int i = value.IndexOf("</", startIx, StringComparison.Ordinal); i >= 0; i = value.IndexOf("</", i + 2, StringComparison.Ordinal))
			{
				if (value.Length < i + endTagLength)
				{
					return -1;
				}
				int num = i + 2;
				int num2 = 0;
				while (num2 < tagName.Length && value[num + num2] == tagName[num2])
				{
					num2++;
				}
				if (num2 == tagName.Length && value[i + endTagLength - 1] == '>')
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00045E58 File Offset: 0x00044058
		public static bool TryGetTagInfo(string tagString, out string tagName, out bool isEndingTag)
		{
			tagName = string.Empty;
			isEndingTag = false;
			Match match = PrivateInformation.TagRegex.Match(tagString);
			if (match.Success)
			{
				tagName = match.Groups["TagName"].Value;
				isEndingTag = match.Groups["IsEnding"].Value == "/";
			}
			return !string.IsNullOrEmpty(tagName);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00045EC8 File Offset: 0x000440C8
		private static bool TryGetNextShortCandidateXmlStyleTag(string plainString, int fromIndex, int tillIndex, int maxLength, out int indexOpen, out int indexClose, out string tagName, out bool isCloseTag)
		{
			indexOpen = -1;
			indexClose = -1;
			tagName = string.Empty;
			isCloseTag = false;
			while (fromIndex <= Math.Min(tillIndex, plainString.Length - 1))
			{
				int num = plainString.IndexOf('<', fromIndex);
				if (num < 0 || num > tillIndex)
				{
					return false;
				}
				int num2 = plainString.IndexOf('>', num);
				if (num2 < 0)
				{
					return false;
				}
				int num3;
				if (num2 <= maxLength)
				{
					num3 = num + plainString.Substring(num, num2 - num).LastIndexOf('<');
				}
				else
				{
					int num4 = plainString.Substring(num2 - maxLength, maxLength).LastIndexOf('<');
					if (num4 < 0)
					{
						fromIndex = num2 + 1;
						continue;
					}
					num3 = num2 - maxLength + num4;
				}
				string text;
				bool flag;
				if (PrivateInformation.TryGetTagInfo(plainString.Substring(num3, num2 - num3 + 1), out text, out flag))
				{
					tagName = text;
					isCloseTag = flag;
					indexOpen = num3;
					indexClose = num2;
					return true;
				}
				fromIndex = num2 + 1;
			}
			return false;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00045F9C File Offset: 0x0004419C
		private static bool TryGetNextXmlStyleTagName(string plainString, string tagName, int fromIndex, out int indexOpenTag, out int indexCloseTag, out bool isCloseTag)
		{
			indexOpenTag = -1;
			indexCloseTag = -1;
			isCloseTag = false;
			while (fromIndex < plainString.Length - 1)
			{
				int num;
				int num2;
				string text;
				bool flag;
				if (!PrivateInformation.TryGetNextShortCandidateXmlStyleTag(plainString, fromIndex, plainString.Length - 1, tagName.Length + 3, out num, out num2, out text, out flag))
				{
					return false;
				}
				if (text.Equals(tagName))
				{
					isCloseTag = flag;
					indexOpenTag = num;
					indexCloseTag = num2;
					return true;
				}
				fromIndex = num2 + 1;
			}
			return false;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00046004 File Offset: 0x00044204
		private static string MarkWithMarkupTag(this string plainString, string tagName)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return plainString;
			}
			string text = "<" + tagName + ">";
			string text2 = "</" + tagName + ">";
			StringBuilder stringBuilder = new StringBuilder(text);
			int num = 0;
			int num2;
			int num3;
			bool flag;
			while (num < plainString.Length - 1 && PrivateInformation.TryGetNextXmlStyleTagName(plainString, tagName, num, out num2, out num3, out flag))
			{
				string text3 = (flag ? "/" : string.Empty);
				stringBuilder.Append(string.Concat(new string[]
				{
					plainString.Substring(num, num2 - num),
					"[",
					text3,
					tagName,
					"]"
				}));
				num = num3 + 1;
			}
			stringBuilder.Append(plainString.Substring(num, plainString.Length - num) + text2);
			return stringBuilder.Replace('\0', ' ').ToString();
		}

		// Token: 0x0400034D RID: 845
		public const string PI_PRIVATE_INFORMATION_TAG_NAME = "pi";

		// Token: 0x0400034E RID: 846
		public const string PII_PRIVATE_INFORMATION_TAG_NAME = "pii";

		// Token: 0x0400034F RID: 847
		public const string INTERNAL_INFORMATION_TAG_NAME = "ii";

		// Token: 0x04000350 RID: 848
		private static readonly Dictionary<string, int> c_PrivateTagNamesMappedToItsLength = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase)
		{
			{ "<pi>", 3 },
			{ "</pi>", 4 },
			{ "<pii>", 4 },
			{ "</pii>", 5 }
		};

		// Token: 0x04000351 RID: 849
		public const string IP_ADDRESS_TAG_NAME = "ip";

		// Token: 0x04000352 RID: 850
		public const int MAX_STRING_LENGTH_BETWEEN_IP_TAGS = 60;

		// Token: 0x04000353 RID: 851
		public const string CUSTOMER_CONTENT_TAG_NAME = "ccon";

		// Token: 0x04000354 RID: 852
		public const string EUII_TAG_NAME = "euii";

		// Token: 0x04000355 RID: 853
		public const string EUPI_TAG_NAME = "eupi";

		// Token: 0x04000356 RID: 854
		public const string WARNING_MESSAGE_FOR_NON_VALID_IP_ADDRESS = "INPUT STRING IS NOT VALID IP ADDRESS";

		// Token: 0x04000357 RID: 855
		private static readonly Regex TagRegex = new Regex("<(?<IsEnding>/?)(?<TagName>[a-zA-Z]+)>", RegexOptions.Compiled);

		// Token: 0x04000358 RID: 856
		private static readonly string[] c_privateTagNamesArray = new string[] { "pi" };

		// Token: 0x04000359 RID: 857
		private static readonly string[] c_internalTagNamesArray = new string[] { "ii" };

		// Token: 0x0400035A RID: 858
		private static readonly string[] c_allTagNamesArray = new string[] { "pi", "ii" };

		// Token: 0x0400035B RID: 859
		private static readonly Type c_iContainsPrivateInformationType = typeof(IContainsPrivateInformation);
	}
}
