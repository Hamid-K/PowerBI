using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.PowerBI.Telemetry.PIIUtils
{
	// Token: 0x0200003B RID: 59
	public static class PrivateInformation
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00004A79 File Offset: 0x00002C79
		internal static IReadOnlyList<string> PrivateTagNamesArray
		{
			get
			{
				return PrivateInformation.c_privateTagNamesArray;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004A80 File Offset: 0x00002C80
		internal static IReadOnlyList<string> InternalTagNamesArray
		{
			get
			{
				return PrivateInformation.c_internalTagNamesArray;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00004A87 File Offset: 0x00002C87
		internal static IReadOnlyList<string> AllTagNamesArray
		{
			get
			{
				return PrivateInformation.c_allTagNamesArray;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004A90 File Offset: 0x00002C90
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

		// Token: 0x06000151 RID: 337 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public static bool ContainsPrivateInformation(Type type)
		{
			return PrivateInformation.c_iContainsPrivateInformationType.IsAssignableFrom(type);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004AFD File Offset: 0x00002CFD
		public static string MarkAsUserContent(this string userContent)
		{
			return userContent.MarkAsCustomerContent();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004B05 File Offset: 0x00002D05
		public static string MarkAsModelInfo(this string modelInfo)
		{
			return modelInfo.MarkAsCustomerContent();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004B0D File Offset: 0x00002D0D
		public static string MarkAsUtterance(this string utterance)
		{
			return utterance.MarkAsCustomerContent();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004B15 File Offset: 0x00002D15
		public static string MarkAsPrivate(this Guid plainGuid)
		{
			return plainGuid.ToString().MarkAsPrivate();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004B2C File Offset: 0x00002D2C
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

		// Token: 0x06000157 RID: 343 RVA: 0x00004BF5 File Offset: 0x00002DF5
		public static string MarkAsPrivate(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			return pi.ToPrivateString();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004C08 File Offset: 0x00002E08
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

		// Token: 0x06000159 RID: 345 RVA: 0x00004C35 File Offset: 0x00002E35
		public static string MarkAsTenantId(this string tenantId)
		{
			return tenantId;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004C38 File Offset: 0x00002E38
		public static string MarkAsOrgId(this string orgId)
		{
			return orgId;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004C3B File Offset: 0x00002E3B
		public static string MarkAsInternal(this string plainString)
		{
			if (plainString == null)
			{
				return string.Empty;
			}
			return PrivateInformation.MarkString(plainString, "ii");
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004C51 File Offset: 0x00002E51
		public static string MarkAsInternal(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			return pi.ToInternalString();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004C64 File Offset: 0x00002E64
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

		// Token: 0x0600015E RID: 350 RVA: 0x00004C91 File Offset: 0x00002E91
		public static string MarkAsEUII(this string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return string.Empty;
			}
			return plainString.MarkWithMarkupTag("euii");
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004CAC File Offset: 0x00002EAC
		public static string MarkAsEUPI(this string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return string.Empty;
			}
			return plainString.MarkWithMarkupTag("eupi");
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004CC8 File Offset: 0x00002EC8
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

		// Token: 0x06000161 RID: 353 RVA: 0x00004D93 File Offset: 0x00002F93
		public static string MarkAsIPAddress(this IPEndPoint ipEndpoint)
		{
			return ipEndpoint.Address.MarkAsIPAddress();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public static string MarkAsIPAddress(this IPAddress ipAddress)
		{
			string text = ipAddress.ToString();
			if (text.Contains("::"))
			{
				return text.MarkAsCustomerContent();
			}
			return "<ip>" + text + "</ip>";
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004DD8 File Offset: 0x00002FD8
		public static string MarkAsEndpoint(this IPEndPoint ipEndpoint)
		{
			return string.Format("{0}:{1}", ipEndpoint.Address.MarkAsIPAddress(), ipEndpoint.Port);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00004DFA File Offset: 0x00002FFA
		public static string MarkAsCustomerContent(this string plainString)
		{
			if (string.IsNullOrEmpty(plainString))
			{
				return string.Empty;
			}
			return plainString.MarkWithMarkupTag("ccon");
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00004E15 File Offset: 0x00003015
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

		// Token: 0x06000166 RID: 358 RVA: 0x00004E29 File Offset: 0x00003029
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

		// Token: 0x06000167 RID: 359 RVA: 0x00004E58 File Offset: 0x00003058
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

		// Token: 0x06000168 RID: 360 RVA: 0x00004EB3 File Offset: 0x000030B3
		public static string ScrubPII(this string plainString)
		{
			if (plainString == null)
			{
				return string.Empty;
			}
			return Obfuscation.Obfuscate(plainString, false);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004EC5 File Offset: 0x000030C5
		public static string ScrubPII(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			return pi.ToPrivateString();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004ED8 File Offset: 0x000030D8
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

		// Token: 0x0600016B RID: 363 RVA: 0x00004F05 File Offset: 0x00003105
		public static string TagAsPII(this string plainString)
		{
			return PrivateInformation.MarkString(plainString, "pi");
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004F12 File Offset: 0x00003112
		public static string TagAsPII(this IContainsPrivateInformation pi)
		{
			if (pi == null)
			{
				return string.Empty;
			}
			return pi.ToInternalString();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004F24 File Offset: 0x00003124
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

		// Token: 0x0600016E RID: 366 RVA: 0x00004F51 File Offset: 0x00003151
		public static string RemovePrivateMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_privateTagNamesArray, null, null, null);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004F61 File Offset: 0x00003161
		public static string RemovePrivateMarkup(this string markedUpString, Func<string, string> stringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_privateTagNamesArray, stringTransform, null, null);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004F71 File Offset: 0x00003171
		public static string RemoveInternalMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_internalTagNamesArray, null, null, null);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004F81 File Offset: 0x00003181
		public static string RemoveInternalMarkup(this string markedUpString, Func<string, string> stringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_internalTagNamesArray, stringTransform, null, null);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004F91 File Offset: 0x00003191
		public static string RemovePrivateAndInternalMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, null, null);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004F9B File Offset: 0x0000319B
		public static string RemovePrivateAndInternalMarkup(this string markedUpString, Func<string, string> privateStringTransform, Func<string, string> internalStringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, privateStringTransform, internalStringTransform);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004FA8 File Offset: 0x000031A8
		public static string RemovePrivateInternalAndCustomMarkup(this string markedUpString, string[] customTagNames, Func<string, string> privateStringTransform = null, Func<string, string> internalStringTransform = null, Func<string, string> customStringTransform = null)
		{
			string[] array = PrivateInformation.CombineArrays(PrivateInformation.c_allTagNamesArray, customTagNames);
			return PrivateInformation.RemoveMarkup(markedUpString, array, privateStringTransform, internalStringTransform, customStringTransform);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004FCC File Offset: 0x000031CC
		public static string RemoveCustomMarkup(this string markedUpString, string[] customTagNames)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, customTagNames, null, null, null);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004FD8 File Offset: 0x000031D8
		private static string RemoveMarkup(string markedUpString, Func<string, string> privateStringTransform = null, Func<string, string> internalStringTransform = null)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_allTagNamesArray, privateStringTransform, internalStringTransform, null);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004FE8 File Offset: 0x000031E8
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

		// Token: 0x06000178 RID: 376 RVA: 0x000050E8 File Offset: 0x000032E8
		public static string UntagPII(this string taggedString)
		{
			return taggedString.RemoveInternalMarkup();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000050F0 File Offset: 0x000032F0
		public static ScrubbedString ToScrubbedString(this string value)
		{
			return new ScrubbedString(value);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000050F8 File Offset: 0x000032F8
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

		// Token: 0x0600017B RID: 379 RVA: 0x00005158 File Offset: 0x00003358
		public static string FormatInvariant(string format, object[] args, bool traceString = false)
		{
			return PrivateInformation.Format(CultureInfo.InvariantCulture, format, args, traceString);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005167 File Offset: 0x00003367
		private static void AppendStartTag(this StringBuilder builder, string markupTagName)
		{
			builder.Append('<');
			builder.Append(markupTagName);
			builder.Append('>');
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005183 File Offset: 0x00003383
		private static void AppendEndTag(this StringBuilder builder, string markupTagName)
		{
			builder.Append('<');
			builder.Append('/');
			builder.Append(markupTagName);
			builder.Append('>');
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000051A8 File Offset: 0x000033A8
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

		// Token: 0x0600017F RID: 383 RVA: 0x00005209 File Offset: 0x00003409
		public static int GetStartTagLength(this string tagName)
		{
			return tagName.Length + 2;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005213 File Offset: 0x00003413
		public static int GetEndTagLength(this string tagName)
		{
			return tagName.Length + 3;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005220 File Offset: 0x00003420
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

		// Token: 0x06000182 RID: 386 RVA: 0x000052E0 File Offset: 0x000034E0
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

		// Token: 0x06000183 RID: 387 RVA: 0x0000537C File Offset: 0x0000357C
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

		// Token: 0x06000184 RID: 388 RVA: 0x000053EC File Offset: 0x000035EC
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

		// Token: 0x06000185 RID: 389 RVA: 0x000055A4 File Offset: 0x000037A4
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

		// Token: 0x06000186 RID: 390 RVA: 0x00005611 File Offset: 0x00003811
		private static bool IsInMarkupTagsList(this string tag)
		{
			return PrivateInformation.MarkupTags.Contains(tag);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005620 File Offset: 0x00003820
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

		// Token: 0x06000188 RID: 392 RVA: 0x000056F4 File Offset: 0x000038F4
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

		// Token: 0x06000189 RID: 393 RVA: 0x0000575C File Offset: 0x0000395C
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

		// Token: 0x0600018A RID: 394 RVA: 0x00005848 File Offset: 0x00003A48
		private static string[] CombineArrays(string[] x, string[] y)
		{
			string[] array = new string[x.Length + y.Length];
			Array.Copy(x, array, x.Length);
			Array.Copy(y, 0, array, x.Length, y.Length);
			return array;
		}

		// Token: 0x040000D9 RID: 217
		public const string PI_PRIVATE_INFORMATION_TAG_NAME = "pi";

		// Token: 0x040000DA RID: 218
		public const string PII_PRIVATE_INFORMATION_TAG_NAME = "pii";

		// Token: 0x040000DB RID: 219
		public const string INTERNAL_INFORMATION_TAG_NAME = "ii";

		// Token: 0x040000DC RID: 220
		private static readonly HashSet<string> c_privateStartAndEndTagNames = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase) { "<pi>", "</pi>", "<pii>", "</pii>" };

		// Token: 0x040000DD RID: 221
		public const string IP_ADDRESS_TAG_NAME = "ip";

		// Token: 0x040000DE RID: 222
		public const int MAX_STRING_LENGTH_BETWEEN_IP_TAGS = 60;

		// Token: 0x040000DF RID: 223
		public const string CUSTOMER_CONTENT_TAG_NAME = "ccon";

		// Token: 0x040000E0 RID: 224
		public const string EUII_TAG_NAME = "euii";

		// Token: 0x040000E1 RID: 225
		public const string EUPI_TAG_NAME = "eupi";

		// Token: 0x040000E2 RID: 226
		public const string WARNING_MESSAGE_FOR_NON_VALID_IP_ADDRESS = "INPUT STRING IS NOT VALID IP ADDRESS";

		// Token: 0x040000E3 RID: 227
		public static readonly string[] NonIpMarkupTags = new string[] { "pi", "pii", "euii", "ccon", "eupi" };

		// Token: 0x040000E4 RID: 228
		private static readonly List<string> MarkupTags = new List<string> { "pi", "pii", "ip", "ccon", "euii", "eupi" };

		// Token: 0x040000E5 RID: 229
		private static readonly int MaxMarkupTagLength = PrivateInformation.GetMaxMarkupTagLength();

		// Token: 0x040000E6 RID: 230
		private static readonly Regex TagRegex = new Regex("<(?<IsEnding>/?)(?<TagName>[a-zA-Z]+)>", RegexOptions.Compiled);

		// Token: 0x040000E7 RID: 231
		private static readonly string[] c_privateTagNamesArray = new string[] { "pi", "euii", "ccon", "ip" };

		// Token: 0x040000E8 RID: 232
		private static readonly string[] c_internalTagNamesArray = new string[] { "ii" };

		// Token: 0x040000E9 RID: 233
		private static readonly string[] c_allTagNamesArray = PrivateInformation.CombineArrays(PrivateInformation.c_privateTagNamesArray, PrivateInformation.c_internalTagNamesArray);

		// Token: 0x040000EA RID: 234
		private static readonly Type c_iContainsPrivateInformationType = typeof(IContainsPrivateInformation);
	}
}
