using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.InfoNav.Utils
{
	// Token: 0x02000036 RID: 54
	public static class PrivateInformation
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000256 RID: 598 RVA: 0x000072C6 File Offset: 0x000054C6
		internal static IReadOnlyList<string> PrivateTagNamesArray
		{
			get
			{
				return PrivateInformation.c_privateTagNamesArray;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000257 RID: 599 RVA: 0x000072CD File Offset: 0x000054CD
		internal static IReadOnlyList<string> InternalTagNamesArray
		{
			get
			{
				return PrivateInformation.c_internalTagNamesArray;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000072D4 File Offset: 0x000054D4
		internal static IReadOnlyList<string> AllTagNamesArray
		{
			get
			{
				return PrivateInformation.c_allTagNamesArray;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000072DC File Offset: 0x000054DC
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

		// Token: 0x0600025A RID: 602 RVA: 0x0000733C File Offset: 0x0000553C
		public static bool ContainsPrivateInformation(Type type)
		{
			return PrivateInformation.c_iContainsPrivateInformationType.IsAssignableFrom(type);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007349 File Offset: 0x00005549
		public static string MarkAsUserContent(this string userContent)
		{
			return userContent.MarkAsCustomerContent();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007351 File Offset: 0x00005551
		public static string MarkAsModelInfo(this string modelInfo)
		{
			return modelInfo.MarkAsCustomerContent();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00007359 File Offset: 0x00005559
		public static string MarkAsUtterance(this string utterance)
		{
			return utterance.MarkAsCustomerContent();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00007361 File Offset: 0x00005561
		public static string MarkAsPrivate(this Guid plainGuid)
		{
			return plainGuid.ToString().MarkAsPrivate();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007378 File Offset: 0x00005578
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
			if (Tracing.RemovePersonallyIdentifiableInformationFromTraces)
			{
				return PrivateInformation.MarkString(stringBuilder.ToString(), "pi");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000744F File Offset: 0x0000564F
		public static string MarkAsPrivate(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			if (Tracing.RemovePersonallyIdentifiableInformationFromTraces)
			{
				return pi.ToPrivateString();
			}
			return pi.ToOriginalString();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007470 File Offset: 0x00005670
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

		// Token: 0x06000262 RID: 610 RVA: 0x0000749D File Offset: 0x0000569D
		public static string MarkAsTenantId(this string tenantId)
		{
			return tenantId;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000074A0 File Offset: 0x000056A0
		public static string MarkAsOrgId(this string orgId)
		{
			return orgId;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000074A3 File Offset: 0x000056A3
		public static string MarkAsInternal(this string plainString)
		{
			if (plainString == null)
			{
				return string.Empty;
			}
			if (Tracing.RemovePersonallyIdentifiableInformationFromTraces)
			{
				return PrivateInformation.MarkString(plainString, "ii");
			}
			return plainString;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000074C2 File Offset: 0x000056C2
		public static string MarkAsInternal(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			if (Tracing.RemovePersonallyIdentifiableInformationFromTraces)
			{
				return pi.ToInternalString();
			}
			return pi.ToOriginalString();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000074E4 File Offset: 0x000056E4
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

		// Token: 0x06000267 RID: 615 RVA: 0x00007511 File Offset: 0x00005711
		public static string MarkAsEUII(this string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return string.Empty;
			}
			return plainString.MarkWithMarkupTag("euii");
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000752C File Offset: 0x0000572C
		public static string MarkAsEUPI(this string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return string.Empty;
			}
			return plainString.MarkWithMarkupTag("eupi");
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00007548 File Offset: 0x00005748
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

		// Token: 0x0600026A RID: 618 RVA: 0x00007613 File Offset: 0x00005813
		public static string MarkAsIPAddress(this IPEndPoint ipEndpoint)
		{
			return ipEndpoint.Address.MarkAsIPAddress();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00007620 File Offset: 0x00005820
		public static string MarkAsIPAddress(this IPAddress ipAddress)
		{
			string text = ipAddress.ToString();
			if (text.Contains("::"))
			{
				return text.MarkAsCustomerContent();
			}
			return "<ip>" + text + "</ip>";
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00007658 File Offset: 0x00005858
		public static string MarkAsEndpoint(this IPEndPoint ipEndpoint)
		{
			return string.Format("{0}:{1}", ipEndpoint.Address.MarkAsIPAddress(), ipEndpoint.Port);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000767A File Offset: 0x0000587A
		public static string MarkAsCustomerContent(this string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return string.Empty;
			}
			return plainString.MarkWithMarkupTag("ccon");
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00007695 File Offset: 0x00005895
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

		// Token: 0x0600026F RID: 623 RVA: 0x000076A9 File Offset: 0x000058A9
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

		// Token: 0x06000270 RID: 624 RVA: 0x000076D8 File Offset: 0x000058D8
		public static string ScrubPII(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			if (Tracing.RemovePersonallyIdentifiableInformationFromTraces)
			{
				return pi.ToPrivateString();
			}
			return pi.ToOriginalString();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000076F8 File Offset: 0x000058F8
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

		// Token: 0x06000272 RID: 626 RVA: 0x00007725 File Offset: 0x00005925
		public static string TagAsPII(this string plainString)
		{
			return PrivateInformation.MarkString(plainString, "pi");
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00007732 File Offset: 0x00005932
		public static string TagAsPII(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			if (Tracing.RemovePersonallyIdentifiableInformationFromTraces)
			{
				return pi.ToInternalString();
			}
			return pi.ToOriginalString();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00007754 File Offset: 0x00005954
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

		// Token: 0x06000275 RID: 629 RVA: 0x00007781 File Offset: 0x00005981
		public static string RemovePrivateMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_privateTagNamesArray, null, null, null);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00007791 File Offset: 0x00005991
		public static string RemovePrivateMarkup(this string markedUpString, Func<string, string> stringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_privateTagNamesArray, stringTransform, null, null);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000077A1 File Offset: 0x000059A1
		public static string RemoveInternalMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_internalTagNamesArray, null, null, null);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000077B1 File Offset: 0x000059B1
		public static string RemoveInternalMarkup(this string markedUpString, Func<string, string> stringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_internalTagNamesArray, stringTransform, null, null);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000077C1 File Offset: 0x000059C1
		public static string RemovePrivateAndInternalMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, null, null);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000077CB File Offset: 0x000059CB
		public static string RemovePrivateAndInternalMarkup(this string markedUpString, Func<string, string> privateStringTransform, Func<string, string> internalStringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, privateStringTransform, internalStringTransform);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000077D8 File Offset: 0x000059D8
		public static string RemovePrivateInternalAndCustomMarkup(this string markedUpString, string[] customTagNames, Func<string, string> privateStringTransform = null, Func<string, string> internalStringTransform = null, Func<string, string> customStringTransform = null)
		{
			string[] array = PrivateInformation.CombineArrays(PrivateInformation.c_allTagNamesArray, customTagNames);
			return PrivateInformation.RemoveMarkup(markedUpString, array, privateStringTransform, internalStringTransform, customStringTransform);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000077FC File Offset: 0x000059FC
		public static string RemoveCustomMarkup(this string markedUpString, string[] customTagNames)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, customTagNames, null, null, null);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007808 File Offset: 0x00005A08
		private static string RemoveMarkup(string markedUpString, Func<string, string> privateStringTransform = null, Func<string, string> internalStringTransform = null)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_allTagNamesArray, privateStringTransform, internalStringTransform, null);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00007818 File Offset: 0x00005A18
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

		// Token: 0x0600027F RID: 639 RVA: 0x00007918 File Offset: 0x00005B18
		public static string UntagPII(this string taggedString)
		{
			return taggedString.RemoveInternalMarkup();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00007920 File Offset: 0x00005B20
		public static ScrubbedString ToScrubbedString(this string value)
		{
			return new ScrubbedString(value);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007928 File Offset: 0x00005B28
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

		// Token: 0x06000282 RID: 642 RVA: 0x00007988 File Offset: 0x00005B88
		public static string FormatInvariant(string format, object[] args, bool traceString = false)
		{
			return PrivateInformation.Format(CultureInfo.InvariantCulture, format, args, traceString);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007997 File Offset: 0x00005B97
		private static void AppendStartTag(this StringBuilder builder, string markupTagName)
		{
			builder.Append('<');
			builder.Append(markupTagName);
			builder.Append('>');
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000079B3 File Offset: 0x00005BB3
		private static void AppendEndTag(this StringBuilder builder, string markupTagName)
		{
			builder.Append('<');
			builder.Append('/');
			builder.Append(markupTagName);
			builder.Append('>');
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000079D8 File Offset: 0x00005BD8
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

		// Token: 0x06000286 RID: 646 RVA: 0x00007A39 File Offset: 0x00005C39
		public static int GetStartTagLength(this string tagName)
		{
			return tagName.Length + 2;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007A43 File Offset: 0x00005C43
		public static int GetEndTagLength(this string tagName)
		{
			return tagName.Length + 3;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00007A50 File Offset: 0x00005C50
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

		// Token: 0x06000289 RID: 649 RVA: 0x00007B10 File Offset: 0x00005D10
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

		// Token: 0x0600028A RID: 650 RVA: 0x00007BAC File Offset: 0x00005DAC
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

		// Token: 0x0600028B RID: 651 RVA: 0x00007C1C File Offset: 0x00005E1C
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

		// Token: 0x0600028C RID: 652 RVA: 0x00007DD4 File Offset: 0x00005FD4
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

		// Token: 0x0600028D RID: 653 RVA: 0x00007E41 File Offset: 0x00006041
		private static bool IsInMarkupTagsList(this string tag)
		{
			return PrivateInformation.MarkupTags.Contains(tag);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007E50 File Offset: 0x00006050
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

		// Token: 0x0600028F RID: 655 RVA: 0x00007F24 File Offset: 0x00006124
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

		// Token: 0x06000290 RID: 656 RVA: 0x00007F8C File Offset: 0x0000618C
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

		// Token: 0x06000291 RID: 657 RVA: 0x00008078 File Offset: 0x00006278
		private static string[] CombineArrays(string[] x, string[] y)
		{
			string[] array = new string[x.Length + y.Length];
			Array.Copy(x, array, x.Length);
			Array.Copy(y, 0, array, x.Length, y.Length);
			return array;
		}

		// Token: 0x04000076 RID: 118
		public const string PI_PRIVATE_INFORMATION_TAG_NAME = "pi";

		// Token: 0x04000077 RID: 119
		public const string PII_PRIVATE_INFORMATION_TAG_NAME = "pii";

		// Token: 0x04000078 RID: 120
		public const string INTERNAL_INFORMATION_TAG_NAME = "ii";

		// Token: 0x04000079 RID: 121
		private static readonly HashSet<string> c_privateStartAndEndTagNames = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase) { "<pi>", "</pi>", "<pii>", "</pii>" };

		// Token: 0x0400007A RID: 122
		public const string IP_ADDRESS_TAG_NAME = "ip";

		// Token: 0x0400007B RID: 123
		public const int MAX_STRING_LENGTH_BETWEEN_IP_TAGS = 60;

		// Token: 0x0400007C RID: 124
		public const string CUSTOMER_CONTENT_TAG_NAME = "ccon";

		// Token: 0x0400007D RID: 125
		public const string EUII_TAG_NAME = "euii";

		// Token: 0x0400007E RID: 126
		public const string EUPI_TAG_NAME = "eupi";

		// Token: 0x0400007F RID: 127
		public const string WARNING_MESSAGE_FOR_NON_VALID_IP_ADDRESS = "INPUT STRING IS NOT VALID IP ADDRESS";

		// Token: 0x04000080 RID: 128
		public static readonly string[] NonIpMarkupTags = new string[] { "pi", "pii", "euii", "ccon", "eupi" };

		// Token: 0x04000081 RID: 129
		private static readonly List<string> MarkupTags = new List<string> { "pi", "pii", "ip", "ccon", "euii", "eupi" };

		// Token: 0x04000082 RID: 130
		private static readonly int MaxMarkupTagLength = PrivateInformation.GetMaxMarkupTagLength();

		// Token: 0x04000083 RID: 131
		private static readonly Regex TagRegex = new Regex("<(?<IsEnding>/?)(?<TagName>[a-zA-Z]+)>", RegexOptions.Compiled);

		// Token: 0x04000084 RID: 132
		private static readonly string[] c_privateTagNamesArray = new string[] { "pi", "euii", "ccon", "ip" };

		// Token: 0x04000085 RID: 133
		private static readonly string[] c_internalTagNamesArray = new string[] { "ii" };

		// Token: 0x04000086 RID: 134
		private static readonly string[] c_allTagNamesArray = PrivateInformation.CombineArrays(PrivateInformation.c_privateTagNamesArray, PrivateInformation.c_internalTagNamesArray);

		// Token: 0x04000087 RID: 135
		private static readonly Type c_iContainsPrivateInformationType = typeof(IContainsPrivateInformation);
	}
}
