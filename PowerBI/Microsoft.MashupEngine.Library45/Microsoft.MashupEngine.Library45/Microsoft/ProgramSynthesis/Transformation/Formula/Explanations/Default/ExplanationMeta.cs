using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Explanations.Default
{
	// Token: 0x020019C4 RID: 6596
	public class ExplanationMeta : IExplanationMeta
	{
		// Token: 0x0600D759 RID: 55129 RVA: 0x002DC2DA File Offset: 0x002DA4DA
		public ExplanationMeta(IExplanationTemplateProvider templateProvider, IEnumerable<string> keyPath, IDictionary<string, object> replacements)
		{
			this._templateProvider = templateProvider;
			this.Key = keyPath.ToJoinString("/");
			this._rawReplacements = replacements;
		}

		// Token: 0x170023C9 RID: 9161
		// (get) Token: 0x0600D75A RID: 55130 RVA: 0x002DC301 File Offset: 0x002DA501
		public string Key { get; }

		// Token: 0x170023CA RID: 9162
		// (get) Token: 0x0600D75B RID: 55131 RVA: 0x002DC30C File Offset: 0x002DA50C
		public IReadOnlyList<string> OrderedReplacements
		{
			get
			{
				IReadOnlyList<string> readOnlyList;
				if ((readOnlyList = this._orderedReplacements) == null)
				{
					readOnlyList = (this._orderedReplacements = this.LoadOrderedReplacements());
				}
				return readOnlyList;
			}
		}

		// Token: 0x170023CB RID: 9163
		// (get) Token: 0x0600D75C RID: 55132 RVA: 0x002DC334 File Offset: 0x002DA534
		public IDictionary<string, string> Replacements
		{
			get
			{
				IDictionary<string, string> dictionary;
				if ((dictionary = this._replacements) == null)
				{
					dictionary = (this._replacements = this.LoadReplacements());
				}
				return dictionary;
			}
		}

		// Token: 0x170023CC RID: 9164
		// (get) Token: 0x0600D75D RID: 55133 RVA: 0x002DC35C File Offset: 0x002DA55C
		public string TemplateText
		{
			get
			{
				string text;
				if ((text = this._templateText) == null)
				{
					text = (this._templateText = this.LoadTemplateText());
				}
				return text;
			}
		}

		// Token: 0x170023CD RID: 9165
		// (get) Token: 0x0600D75E RID: 55134 RVA: 0x002DC384 File Offset: 0x002DA584
		public string Text
		{
			get
			{
				string text;
				if ((text = this._text) == null)
				{
					text = (this._text = this.LoadText());
				}
				return text;
			}
		}

		// Token: 0x0600D75F RID: 55135 RVA: 0x002DC3AA File Offset: 0x002DA5AA
		public override string ToString()
		{
			return this.Text;
		}

		// Token: 0x0600D760 RID: 55136 RVA: 0x002DC3B4 File Offset: 0x002DA5B4
		private IReadOnlyList<string> LoadOrderedReplacements()
		{
			List<string> list = new List<string>();
			foreach (Match match in ExplanationMeta._replacementRegex.NonCachingMatches(this.TemplateText))
			{
				string text = match.Value.Replace("{", string.Empty).Replace("}", string.Empty);
				string text2;
				if (this.Replacements.TryGetValue(text, out text2))
				{
					list.Add(text2);
				}
			}
			return list;
		}

		// Token: 0x0600D761 RID: 55137 RVA: 0x002DC448 File Offset: 0x002DA648
		private IDictionary<string, string> LoadReplacements()
		{
			return this._rawReplacements.ToDictionary((KeyValuePair<string, object> pair) => pair.Key, (KeyValuePair<string, object> pair) => this._templateProvider.FormatReplacement(pair.Key, pair.Value));
		}

		// Token: 0x0600D762 RID: 55138 RVA: 0x002DC480 File Offset: 0x002DA680
		private string LoadTemplateText()
		{
			string text = this._templateProvider.Template(this.Key) ?? this._templateProvider.Template(this.Key.Before("/"));
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x0600D763 RID: 55139 RVA: 0x002DC4CD File Offset: 0x002DA6CD
		private string LoadText()
		{
			return this.Replacements.Aggregate(this.TemplateText, (string current, KeyValuePair<string, string> pair) => current.Replace("{" + pair.Key + "}", pair.Value));
		}

		// Token: 0x040052AF RID: 21167
		private IReadOnlyList<string> _orderedReplacements;

		// Token: 0x040052B0 RID: 21168
		private readonly IDictionary<string, object> _rawReplacements;

		// Token: 0x040052B1 RID: 21169
		private static readonly Regex _replacementRegex = "\\{\\w[^\\}]+\\}".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x040052B2 RID: 21170
		private IDictionary<string, string> _replacements;

		// Token: 0x040052B3 RID: 21171
		private readonly IExplanationTemplateProvider _templateProvider;

		// Token: 0x040052B4 RID: 21172
		private string _templateText;

		// Token: 0x040052B5 RID: 21173
		private string _text;
	}
}
