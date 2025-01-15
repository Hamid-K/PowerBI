using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000F8 RID: 248
	public sealed class TemplateTextHandler
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x000183F4 File Offset: 0x000165F4
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x000183FC File Offset: 0x000165FC
		public string PlaceholderStartMarker { get; private set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00018405 File Offset: 0x00016605
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0001840D File Offset: 0x0001660D
		public string PlaceholderEndMarker { get; private set; }

		// Token: 0x060006EC RID: 1772 RVA: 0x00018418 File Offset: 0x00016618
		public TemplateTextHandler([NotNull] string placeholderStartMarker, [NotNull] string placeholderEndMarker)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(placeholderStartMarker, "placeholderStartMarker");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(placeholderEndMarker, "placeholderEndMarker");
			this.PlaceholderStartMarker = placeholderStartMarker;
			this.PlaceholderEndMarker = placeholderEndMarker;
			string text = Regex.Escape(placeholderStartMarker);
			string text2 = Regex.Escape(placeholderEndMarker);
			string text3 = "{0}(?<{1}>.+?){2}".FormatWithInvariantCulture(new object[] { text, "placeholder", text2 });
			this.m_regex = new Regex(text3, RegexOptions.Compiled);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001848C File Offset: 0x0001668C
		public string ReplacePlaceholders([NotNull] string templateText, [NotNull] Dictionary<string, string> placeholderToValues)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(templateText, "templateText");
			ExtendedDiagnostics.EnsureArgumentNotNull<Dictionary<string, string>>(placeholderToValues, "placeholderToValues");
			IEnumerable<string> enumerable = this.GetPlaceholders(templateText);
			enumerable = enumerable.Where((string placeholder) => placeholderToValues.ContainsKey(placeholder));
			return this.ReplacePlaceholders(enumerable, templateText, (string placeholder) => placeholderToValues[placeholder]);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000184F0 File Offset: 0x000166F0
		public string ReplacePlaceholders([NotNull] string templateText, [NotNull] Func<string, string> getPlaceholderValue)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(templateText, "templateText");
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<string, string>>(getPlaceholderValue, "getPlaceholderValue");
			IEnumerable<string> placeholders = this.GetPlaceholders(templateText);
			return this.ReplacePlaceholders(placeholders, templateText, getPlaceholderValue);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00018524 File Offset: 0x00016724
		private IEnumerable<string> GetPlaceholders(string templateText)
		{
			return (from Match match in this.m_regex.Matches(templateText)
				select match.Groups["placeholder"].Value).Distinct(StringComparer.Ordinal);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00018570 File Offset: 0x00016770
		private string ReplacePlaceholders(IEnumerable<string> placeholders, string templateText, Func<string, string> getPlaceholderValue)
		{
			StringBuilder stringBuilder = new StringBuilder(templateText);
			foreach (string text in placeholders)
			{
				string text2 = "{0}{1}{2}".FormatWithInvariantCulture(new object[] { this.PlaceholderStartMarker, text, this.PlaceholderEndMarker });
				string text3 = getPlaceholderValue(text);
				stringBuilder.Replace(text2, text3);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000253 RID: 595
		private const string c_matchGroupName = "placeholder";

		// Token: 0x04000254 RID: 596
		private readonly Regex m_regex;
	}
}
