using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200000F RID: 15
	public static class PrivateInformation
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002361 File Offset: 0x00000561
		internal static IReadOnlyList<string> PrivateTagNamesArray
		{
			get
			{
				return PrivateInformation.c_privateTagNamesArray;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002368 File Offset: 0x00000568
		internal static IReadOnlyList<string> InternalTagNamesArray
		{
			get
			{
				return PrivateInformation.c_internalTagNamesArray;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000236F File Offset: 0x0000056F
		internal static IReadOnlyList<string> AllTagNamesArray
		{
			get
			{
				return PrivateInformation.c_allTagNamesArray;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002378 File Offset: 0x00000578
		private static int GetMaxMarkupTagLength()
		{
			int num = 1;
			foreach (string text in PrivateInformation.MarkupTags)
			{
				if (num < text.Length)
				{
					num = text.Length;
				}
			}
			return num + 3;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023D8 File Offset: 0x000005D8
		public static bool ContainsPrivateInformation(Type type)
		{
			return PrivateInformation.c_iContainsPrivateInformationType.IsAssignableFrom(type);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023E5 File Offset: 0x000005E5
		public static string MarkAsUserContent(this string userContent)
		{
			return userContent.MarkAsCustomerContent();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000023ED File Offset: 0x000005ED
		public static string MarkAsModelInfo(this string modelInfo)
		{
			return modelInfo.MarkAsCustomerContent();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000023F5 File Offset: 0x000005F5
		public static string MarkAsUtterance(this string utterance)
		{
			return utterance.MarkAsCustomerContent();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000023FD File Offset: 0x000005FD
		public static string MarkAsPrivate(this Guid plainGuid)
		{
			return plainGuid.ToString().MarkAsPrivate();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002414 File Offset: 0x00000614
		public static string MarkAsPrivate(this string plainString)
		{
			if (plainString == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(plainString.Length);
			for (int i = 0; i < plainString.Length; i++)
			{
				if (plainString[i] == '<')
				{
					int num = plainString.IndexOf('>', i, Math.Min(i + "</pii>".Length, plainString.Length) - i);
					if (num != -1 && PrivateInformation.c_privateStartAndEndTagNames.Contains(plainString.Substring(i, num - i + 1)))
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

		// Token: 0x06000032 RID: 50 RVA: 0x000024DD File Offset: 0x000006DD
		public static string MarkAsPrivate(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			return pi.ToPrivateString();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000024F0 File Offset: 0x000006F0
		public static string MarkIfPrivate(this object o)
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
			return containsPrivateInformation.ToPrivateString();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000251D File Offset: 0x0000071D
		public static string MarkAsTenantId(this string tenantId)
		{
			return tenantId;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002520 File Offset: 0x00000720
		public static string MarkAsOrgId(this string orgId)
		{
			return orgId;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002523 File Offset: 0x00000723
		public static string MarkAsInternal(this string plainString)
		{
			if (plainString == null)
			{
				return string.Empty;
			}
			return PrivateInformation.MarkString(plainString, "ii");
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002539 File Offset: 0x00000739
		public static string MarkAsInternal(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			return pi.ToInternalString();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000254C File Offset: 0x0000074C
		public static string MarkIfInternal(this object o)
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
			return containsPrivateInformation.ToInternalString();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002579 File Offset: 0x00000779
		public static string MarkAsEUII(this string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return string.Empty;
			}
			return plainString.MarkWithMarkupTag("euii");
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002594 File Offset: 0x00000794
		public static string MarkAsEUPI(this string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return string.Empty;
			}
			return plainString.MarkWithMarkupTag("eupi");
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000025B0 File Offset: 0x000007B0
		public static string MarkAsIPAddress(this string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return "INPUT STRING IS NOT VALID IP ADDRESS";
			}
			if (plainString.Length >= 60)
			{
				return "INPUT STRING IS NOT VALID IP ADDRESS";
			}
			if (plainString.Contains("::"))
			{
				return plainString.MarkAsCustomerContent();
			}
			IPAddress ipaddress;
			if (!IPAddress.TryParse(plainString, out ipaddress))
			{
				return "INPUT STRING IS NOT VALID IP ADDRESS";
			}
			if (plainString.StartsWith("[") && plainString.Contains("]"))
			{
				int num = plainString.IndexOf("]");
				string text = "[<ip>" + plainString.Substring(1, num - 1) + "</ip>]";
				string text2 = ((plainString.Length > num + 1) ? plainString.Substring(num + 1, plainString.Length - num - 1) : string.Empty);
				return text + text2;
			}
			return "<ip>" + plainString + "</ip>";
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000267B File Offset: 0x0000087B
		public static string MarkAsIPAddress(this IPEndPoint ipEndpoint)
		{
			return ipEndpoint.Address.MarkAsIPAddress();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002688 File Offset: 0x00000888
		public static string MarkAsIPAddress(this IPAddress ipAddress)
		{
			string text = ipAddress.ToString();
			if (text.Contains("::"))
			{
				return text.MarkAsCustomerContent();
			}
			return "<ip>" + text + "</ip>";
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026C0 File Offset: 0x000008C0
		public static string MarkAsEndpoint(this IPEndPoint ipEndpoint)
		{
			return string.Format("{0}:{1}", ipEndpoint.Address.MarkAsIPAddress(), ipEndpoint.Port);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026E2 File Offset: 0x000008E2
		public static string MarkAsCustomerContent(this string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return string.Empty;
			}
			return plainString.MarkWithMarkupTag("ccon");
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026FD File Offset: 0x000008FD
		private static string MarkAs(this string s, PrivateInformationMarkupKind markupKind)
		{
			if (markupKind == PrivateInformationMarkupKind.Internal)
			{
				return s;
			}
			if (markupKind == PrivateInformationMarkupKind.Private)
			{
				return s.MarkAsPrivate();
			}
			return s;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002711 File Offset: 0x00000911
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

		// Token: 0x06000042 RID: 66 RVA: 0x00002740 File Offset: 0x00000940
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

		// Token: 0x06000043 RID: 67 RVA: 0x0000279B File Offset: 0x0000099B
		public static string ScrubPII(this string plainString)
		{
			if (plainString == null)
			{
				return string.Empty;
			}
			return Obfuscation.Obfuscate(plainString, false);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027AD File Offset: 0x000009AD
		public static string ScrubPII(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			return pi.ToPrivateString();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027C0 File Offset: 0x000009C0
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

		// Token: 0x06000046 RID: 70 RVA: 0x000027ED File Offset: 0x000009ED
		public static string TagAsPII(this string plainString)
		{
			return PrivateInformation.MarkString(plainString, "pi");
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000027FA File Offset: 0x000009FA
		public static string TagAsPII(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			return pi.ToInternalString();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000280C File Offset: 0x00000A0C
		public static string TagIfPII(this object o)
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
			return containsPrivateInformation.ToInternalString();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002839 File Offset: 0x00000A39
		public static string RemovePrivateMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_privateTagNamesArray, null, null, null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002849 File Offset: 0x00000A49
		public static string RemovePrivateMarkup(this string markedUpString, Func<string, string> stringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_privateTagNamesArray, stringTransform, null, null);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002859 File Offset: 0x00000A59
		public static string RemoveInternalMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_internalTagNamesArray, null, null, null);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002869 File Offset: 0x00000A69
		public static string RemoveInternalMarkup(this string markedUpString, Func<string, string> stringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_internalTagNamesArray, stringTransform, null, null);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002879 File Offset: 0x00000A79
		public static string RemovePrivateAndInternalMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, null, null);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002883 File Offset: 0x00000A83
		public static string RemovePrivateAndInternalMarkup(this string markedUpString, Func<string, string> privateStringTransform, Func<string, string> internalStringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, privateStringTransform, internalStringTransform);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002890 File Offset: 0x00000A90
		public static string RemovePrivateInternalAndCustomMarkup(this string markedUpString, string[] customTagNames, Func<string, string> privateStringTransform = null, Func<string, string> internalStringTransform = null, Func<string, string> customStringTransform = null)
		{
			string[] array = PrivateInformation.CombineArrays(PrivateInformation.c_allTagNamesArray, customTagNames);
			return PrivateInformation.RemoveMarkup(markedUpString, array, privateStringTransform, internalStringTransform, customStringTransform);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000028B4 File Offset: 0x00000AB4
		public static string RemoveCustomMarkup(this string markedUpString, string[] customTagNames)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, customTagNames, null, null, null);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000028C0 File Offset: 0x00000AC0
		private static string RemoveMarkup(string markedUpString, Func<string, string> privateStringTransform = null, Func<string, string> internalStringTransform = null)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_allTagNamesArray, privateStringTransform, internalStringTransform, null);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000028D0 File Offset: 0x00000AD0
		private static string RemoveMarkup(string markedUpString, string[] tagNames, Func<string, string> privateStringTransform = null, Func<string, string> internalStringTransform = null, Func<string, string> customStringTransform = null)
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
			bool flag = privateStringTransform != null || internalStringTransform != null || customStringTransform != null;
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
				if (flag)
				{
					Func<string, string> func;
					if (num2 < PrivateInformation.c_privateTagNamesArray.Length)
					{
						func = privateStringTransform;
					}
					else if (num2 < PrivateInformation.c_allTagNamesArray.Length)
					{
						func = internalStringTransform;
					}
					else
					{
						func = customStringTransform;
					}
					if (func != null)
					{
						text3 = func(text3);
					}
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

		// Token: 0x06000053 RID: 83 RVA: 0x000029D0 File Offset: 0x00000BD0
		public static string UntagPII(this string taggedString)
		{
			return taggedString.RemoveInternalMarkup();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000029D8 File Offset: 0x00000BD8
		public static ScrubbedString ToScrubbedString(this string value)
		{
			return new ScrubbedString(value);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000029E0 File Offset: 0x00000BE0
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

		// Token: 0x06000056 RID: 86 RVA: 0x00002A40 File Offset: 0x00000C40
		public static string FormatInvariant(string format, object[] args, bool traceString = false)
		{
			return PrivateInformation.Format(CultureInfo.InvariantCulture, format, args, traceString);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A4F File Offset: 0x00000C4F
		private static void AppendStartTag(this StringBuilder builder, string markupTagName)
		{
			builder.Append('<');
			builder.Append(markupTagName);
			builder.Append('>');
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002A6B File Offset: 0x00000C6B
		private static void AppendEndTag(this StringBuilder builder, string markupTagName)
		{
			builder.Append('<');
			builder.Append('/');
			builder.Append(markupTagName);
			builder.Append('>');
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002A90 File Offset: 0x00000C90
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

		// Token: 0x0600005A RID: 90 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public static int GetStartTagLength(this string tagName)
		{
			return tagName.Length + 2;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002AFB File Offset: 0x00000CFB
		public static int GetEndTagLength(this string tagName)
		{
			return tagName.Length + 3;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B08 File Offset: 0x00000D08
		public static int IndexOfStartTag(this string value, IReadOnlyList<string> tagNames, int startIx, out int tagNameIx)
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
				for (int j = 0; j < tagNames.Count; j++)
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

		// Token: 0x0600005D RID: 93 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public static int IndexOfEndTag(this string value, string tagName, int startIx)
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

		// Token: 0x0600005E RID: 94 RVA: 0x00002C64 File Offset: 0x00000E64
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

		// Token: 0x0600005F RID: 95 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public static string ValidateAndAddPrefixAndSuffixMarkupTags(this string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return string.Empty;
			}
			string text = string.Empty;
			string text2 = string.Empty;
			int num = 0;
			bool flag = true;
			HashSet<string> hashSet = new HashSet<string>();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			SortedList<int, string> sortedList = new SortedList<int, string>();
			while (flag)
			{
				int num2;
				int num3;
				string text3;
				bool flag2;
				flag = PrivateInformation.TryGetNextMarkupTag(str, num, str.Length - 1, out num2, out num3, out text3, out flag2);
				if (flag)
				{
					if (flag2)
					{
						if (!hashSet.Contains(text3))
						{
							if (dictionary.Count != 0)
							{
								return str.MarkAsCustomerContent();
							}
							text = "<" + text3 + ">" + text;
							hashSet.Add(text3);
						}
						else
						{
							if (!dictionary.ContainsKey(text3) || !(text3 == sortedList.Last<KeyValuePair<int, string>>().Value))
							{
								return str.MarkAsCustomerContent();
							}
							sortedList.Remove(dictionary[text3]);
							dictionary.Remove(text3);
						}
					}
					else
					{
						if (dictionary.ContainsKey(text3))
						{
							return str.MarkAsCustomerContent();
						}
						dictionary.Add(text3, num3);
						sortedList.Add(num3, text3);
						hashSet.Add(text3);
					}
					num = num3 + 1;
					if (num >= str.Length)
					{
						break;
					}
				}
			}
			if (dictionary.Count > 0)
			{
				foreach (KeyValuePair<int, string> keyValuePair in sortedList)
				{
					text2 = "</" + keyValuePair.Value + ">" + text2;
				}
			}
			if (text.Contains("ip") || text2.Contains("ip"))
			{
				return str.MarkAsCustomerContent();
			}
			return text + str + text2;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E8C File Offset: 0x0000108C
		private static bool TryGetNextMarkupTag(string plainString, int fromIndex, int tillIndex, out int indexOpen, out int indexClose, out string tagName, out bool isCloseTag)
		{
			indexOpen = -1;
			indexClose = -1;
			tagName = string.Empty;
			isCloseTag = false;
			while (fromIndex <= Math.Min(tillIndex, plainString.Length - 1))
			{
				int num;
				int num2;
				string text;
				bool flag;
				if (!PrivateInformation.TryGetNextShortCandidateXmlStyleTag(plainString, fromIndex, tillIndex, PrivateInformation.MaxMarkupTagLength, out num, out num2, out text, out flag))
				{
					return false;
				}
				if (text.IsInMarkupTagsList())
				{
					tagName = text;
					isCloseTag = flag;
					indexOpen = num;
					indexClose = num2;
					return true;
				}
				fromIndex = num2 + 1;
			}
			return false;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002EF9 File Offset: 0x000010F9
		private static bool IsInMarkupTagsList(this string tag)
		{
			return PrivateInformation.MarkupTags.Contains(tag);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F08 File Offset: 0x00001108
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

		// Token: 0x06000063 RID: 99 RVA: 0x00002FDC File Offset: 0x000011DC
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

		// Token: 0x06000064 RID: 100 RVA: 0x00003044 File Offset: 0x00001244
		private static string MarkWithMarkupTag(this string plainString, string tagName)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return plainString;
			}
			string text = "<" + tagName + ">";
			string text2 = "</" + tagName + ">";
			StringBuilder stringBuilder = new StringBuilder(text, text.Length + plainString.Length + text2.Length);
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

		// Token: 0x06000065 RID: 101 RVA: 0x00003130 File Offset: 0x00001330
		private static string[] CombineArrays(string[] x, string[] y)
		{
			string[] array = new string[x.Length + y.Length];
			Array.Copy(x, array, x.Length);
			Array.Copy(y, 0, array, x.Length, y.Length);
			return array;
		}

		// Token: 0x0400003B RID: 59
		public const string PI_PRIVATE_INFORMATION_TAG_NAME = "pi";

		// Token: 0x0400003C RID: 60
		public const string PII_PRIVATE_INFORMATION_TAG_NAME = "pii";

		// Token: 0x0400003D RID: 61
		public const string INTERNAL_INFORMATION_TAG_NAME = "ii";

		// Token: 0x0400003E RID: 62
		private static readonly HashSet<string> c_privateStartAndEndTagNames = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase) { "<pi>", "</pi>", "<pii>", "</pii>" };

		// Token: 0x0400003F RID: 63
		public const string IP_ADDRESS_TAG_NAME = "ip";

		// Token: 0x04000040 RID: 64
		public const int MAX_STRING_LENGTH_BETWEEN_IP_TAGS = 60;

		// Token: 0x04000041 RID: 65
		public const string CUSTOMER_CONTENT_TAG_NAME = "ccon";

		// Token: 0x04000042 RID: 66
		public const string EUII_TAG_NAME = "euii";

		// Token: 0x04000043 RID: 67
		public const string EUPI_TAG_NAME = "eupi";

		// Token: 0x04000044 RID: 68
		public const string WARNING_MESSAGE_FOR_NON_VALID_IP_ADDRESS = "INPUT STRING IS NOT VALID IP ADDRESS";

		// Token: 0x04000045 RID: 69
		public static readonly string[] NonIpMarkupTags = new string[] { "pi", "pii", "euii", "ccon", "eupi" };

		// Token: 0x04000046 RID: 70
		private static readonly List<string> MarkupTags = new List<string> { "pi", "pii", "ip", "ccon", "euii", "eupi" };

		// Token: 0x04000047 RID: 71
		private static readonly int MaxMarkupTagLength = PrivateInformation.GetMaxMarkupTagLength();

		// Token: 0x04000048 RID: 72
		private static readonly Regex TagRegex = new Regex("<(?<IsEnding>/?)(?<TagName>[a-zA-Z]+)>", RegexOptions.Compiled);

		// Token: 0x04000049 RID: 73
		private static readonly string[] c_privateTagNamesArray = new string[] { "pi", "euii", "ccon", "ip" };

		// Token: 0x0400004A RID: 74
		private static readonly string[] c_internalTagNamesArray = new string[] { "ii" };

		// Token: 0x0400004B RID: 75
		private static readonly string[] c_allTagNamesArray = PrivateInformation.CombineArrays(PrivateInformation.c_privateTagNamesArray, PrivateInformation.c_internalTagNamesArray);

		// Token: 0x0400004C RID: 76
		private static readonly Type c_iContainsPrivateInformationType = typeof(IContainsPrivateInformation);
	}
}
