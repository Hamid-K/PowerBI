using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200026A RID: 618
	public static class PrivateInformation
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x00038036 File Offset: 0x00036236
		public static bool ContainsPrivateInformation(Type type)
		{
			return PrivateInformation.c_iContainsPrivateInformationType.IsAssignableFrom(type);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00038043 File Offset: 0x00036243
		public static string MarkAsUserContent(this string userContent)
		{
			return userContent.MarkAsPrivate();
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00038043 File Offset: 0x00036243
		public static string MarkAsModelInfo(this string modelInfo)
		{
			return modelInfo.MarkAsPrivate();
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00038043 File Offset: 0x00036243
		public static string MarkAsUtterance(this string utterance)
		{
			return utterance.MarkAsPrivate();
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0003804B File Offset: 0x0003624B
		public static string MarkAsPrivate(this string plainString)
		{
			if (plainString == null)
			{
				return string.Empty;
			}
			if (Tracing.RemovePersonallyIdentifiableInformationFromTraces)
			{
				return PrivateInformation.MarkString(plainString, "pi");
			}
			return plainString;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0003806A File Offset: 0x0003626A
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

		// Token: 0x0600103F RID: 4159 RVA: 0x0003808C File Offset: 0x0003628C
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

		// Token: 0x06001040 RID: 4160 RVA: 0x000380B9 File Offset: 0x000362B9
		public static string MarkAsTenantId(this string tenantId)
		{
			return tenantId.MarkAsInternal();
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000380B9 File Offset: 0x000362B9
		public static string MarkAsOrgId(this string orgId)
		{
			return orgId.MarkAsInternal();
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000380C1 File Offset: 0x000362C1
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

		// Token: 0x06001043 RID: 4163 RVA: 0x000380E0 File Offset: 0x000362E0
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

		// Token: 0x06001044 RID: 4164 RVA: 0x00038100 File Offset: 0x00036300
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

		// Token: 0x06001045 RID: 4165 RVA: 0x0003812D File Offset: 0x0003632D
		private static string MarkAs(this string s, PrivateInformationMarkupKind markupKind)
		{
			if (markupKind == PrivateInformationMarkupKind.Internal)
			{
				return s.MarkAsInternal();
			}
			if (markupKind == PrivateInformationMarkupKind.Private)
			{
				return s.MarkAsPrivate();
			}
			return s;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00038146 File Offset: 0x00036346
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

		// Token: 0x06001047 RID: 4167 RVA: 0x00038178 File Offset: 0x00036378
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

		// Token: 0x06001048 RID: 4168 RVA: 0x000381D3 File Offset: 0x000363D3
		public static string ScrubPII(this string plainString)
		{
			if (plainString == null)
			{
				return string.Empty;
			}
			if (Tracing.RemovePersonallyIdentifiableInformationFromTraces)
			{
				return Obfuscation.Obfuscate(plainString, false);
			}
			return plainString;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0003806A File Offset: 0x0003626A
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

		// Token: 0x0600104A RID: 4170 RVA: 0x000381F0 File Offset: 0x000363F0
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

		// Token: 0x0600104B RID: 4171 RVA: 0x0003821D File Offset: 0x0003641D
		public static string TagAsPII(this string plainString)
		{
			return PrivateInformation.MarkString(plainString, "pi");
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000380E0 File Offset: 0x000362E0
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

		// Token: 0x0600104D RID: 4173 RVA: 0x0003822C File Offset: 0x0003642C
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

		// Token: 0x0600104E RID: 4174 RVA: 0x00038259 File Offset: 0x00036459
		public static string RemovePrivateMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_privateTagNamesArray, new Func<string, string>[0]);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0003826C File Offset: 0x0003646C
		public static string RemovePrivateMarkup(this string markedUpString, Func<string, string> stringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_privateTagNamesArray, new Func<string, string>[] { stringTransform });
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00038283 File Offset: 0x00036483
		public static string RemoveInternalMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_internalTagNamesArray, new Func<string, string>[0]);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00038296 File Offset: 0x00036496
		public static string RemoveInternalMarkup(this string markedUpString, Func<string, string> stringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_internalTagNamesArray, new Func<string, string>[] { stringTransform });
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000382AD File Offset: 0x000364AD
		public static string RemovePrivateAndInternalMarkup(this string markedUpString)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, null, null);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x000382B7 File Offset: 0x000364B7
		public static string RemovePrivateAndInternalMarkup(this string markedUpString, Func<string, string> privateStringTransform, Func<string, string> internalStringTransform)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, privateStringTransform, internalStringTransform);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000382C4 File Offset: 0x000364C4
		public static string RemovePrivateInternalAndCustomMarkup(this string markedUpString, string[] customTagNames, Func<string, string> privateStringTransform = null, Func<string, string> internalStringTransform = null, Func<string, string> customStringTransform = null)
		{
			string[] array = new string[PrivateInformation.c_allTagNamesArray.Length + customTagNames.Length];
			Array.Copy(PrivateInformation.c_allTagNamesArray, array, PrivateInformation.c_allTagNamesArray.Length);
			Array.Copy(customTagNames, 0, array, PrivateInformation.c_allTagNamesArray.Length, customTagNames.Length);
			return PrivateInformation.RemoveMarkup(markedUpString, array, new Func<string, string>[] { privateStringTransform, internalStringTransform, customStringTransform });
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00038320 File Offset: 0x00036520
		public static string RemoveCustomMarkup(this string markedUpString, string[] customTagNames)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, customTagNames, new Func<string, string>[0]);
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0003832F File Offset: 0x0003652F
		private static string RemoveMarkup(string markedUpString, Func<string, string> privateStringTransform = null, Func<string, string> internalStringTransform = null)
		{
			return PrivateInformation.RemoveMarkup(markedUpString, PrivateInformation.c_allTagNamesArray, new Func<string, string>[] { privateStringTransform, internalStringTransform });
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0003834C File Offset: 0x0003654C
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

		// Token: 0x06001058 RID: 4184 RVA: 0x00038421 File Offset: 0x00036621
		public static string UntagPII(this string taggedString)
		{
			return taggedString.RemoveInternalMarkup();
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00038429 File Offset: 0x00036629
		public static ScrubbedString ToScrubbedString(this string value)
		{
			return new ScrubbedString(value);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00038434 File Offset: 0x00036634
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

		// Token: 0x0600105B RID: 4187 RVA: 0x00038494 File Offset: 0x00036694
		public static string FormatInvariant(string format, object[] args, bool traceString = false)
		{
			return PrivateInformation.Format(CultureInfo.InvariantCulture, format, args, traceString);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000384A3 File Offset: 0x000366A3
		private static void AppendStartTag(this StringBuilder builder, string markupTagName)
		{
			builder.Append('<');
			builder.Append(markupTagName);
			builder.Append('>');
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x000384BF File Offset: 0x000366BF
		private static void AppendEndTag(this StringBuilder builder, string markupTagName)
		{
			builder.Append('<');
			builder.Append('/');
			builder.Append(markupTagName);
			builder.Append('>');
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x000384E4 File Offset: 0x000366E4
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

		// Token: 0x0600105F RID: 4191 RVA: 0x00038545 File Offset: 0x00036745
		private static int GetStartTagLength(this string tagName)
		{
			return tagName.Length + 2;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0003854F File Offset: 0x0003674F
		private static int GetEndTagLength(this string tagName)
		{
			return tagName.Length + 3;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003855C File Offset: 0x0003675C
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

		// Token: 0x06001062 RID: 4194 RVA: 0x00038614 File Offset: 0x00036814
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

		// Token: 0x04000616 RID: 1558
		private const string c_privateInformationTagName = "pi";

		// Token: 0x04000617 RID: 1559
		private const string c_internalInformationTagName = "ii";

		// Token: 0x04000618 RID: 1560
		private static readonly string[] c_privateTagNamesArray = new string[] { "pi" };

		// Token: 0x04000619 RID: 1561
		private static readonly string[] c_internalTagNamesArray = new string[] { "ii" };

		// Token: 0x0400061A RID: 1562
		private static readonly string[] c_allTagNamesArray = new string[] { "pi", "ii" };

		// Token: 0x0400061B RID: 1563
		private static readonly Type c_iContainsPrivateInformationType = typeof(IContainsPrivateInformation);
	}
}
