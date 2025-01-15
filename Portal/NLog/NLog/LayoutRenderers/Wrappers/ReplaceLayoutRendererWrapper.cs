using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000FB RID: 251
	[LayoutRenderer("replace")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class ReplaceLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00022E59 File Offset: 0x00021059
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x00022E61 File Offset: 0x00021061
		public string SearchFor { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00022E6A File Offset: 0x0002106A
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x00022E72 File Offset: 0x00021072
		public bool Regex { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00022E7B File Offset: 0x0002107B
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x00022E83 File Offset: 0x00021083
		public string ReplaceWith { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00022E8C File Offset: 0x0002108C
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x00022E94 File Offset: 0x00021094
		public string ReplaceGroupName { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00022E9D File Offset: 0x0002109D
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x00022EA5 File Offset: 0x000210A5
		public bool IgnoreCase { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00022EAE File Offset: 0x000210AE
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x00022EB6 File Offset: 0x000210B6
		public bool WholeWords { get; set; }

		// Token: 0x06000DFC RID: 3580 RVA: 0x00022EC0 File Offset: 0x000210C0
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			string text = this.SearchFor;
			if (!this.Regex)
			{
				text = global::System.Text.RegularExpressions.Regex.Escape(text);
			}
			RegexOptions regexOptions = RegexOptions.Compiled;
			if (this.IgnoreCase)
			{
				regexOptions |= RegexOptions.IgnoreCase;
			}
			if (this.WholeWords)
			{
				text = "\\b" + text + "\\b";
			}
			this._regex = new Regex(text, regexOptions);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00022F20 File Offset: 0x00021120
		protected override string Transform(string text)
		{
			if (string.IsNullOrEmpty(this.ReplaceGroupName))
			{
				return this._regex.Replace(text, this.ReplaceWith);
			}
			ReplaceLayoutRendererWrapper.Replacer replacer = new ReplaceLayoutRendererWrapper.Replacer(text, this.ReplaceGroupName, this.ReplaceWith);
			return this._regex.Replace(text, new MatchEvaluator(replacer.EvaluateMatch));
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00022F78 File Offset: 0x00021178
		public static string ReplaceNamedGroup(string input, string groupName, string replacement, Match match)
		{
			StringBuilder stringBuilder = new StringBuilder(input);
			int index = match.Index;
			int num = match.Length;
			foreach (Capture capture in from c in match.Groups[groupName].Captures.OfType<Capture>()
				orderby c.Index descending
				select c)
			{
				if (capture != null)
				{
					num += replacement.Length - capture.Length;
					stringBuilder.Remove(capture.Index, capture.Length);
					stringBuilder.Insert(capture.Index, replacement);
				}
			}
			int num2 = index + num;
			stringBuilder.Remove(num2, stringBuilder.Length - num2);
			stringBuilder.Remove(0, index);
			return stringBuilder.ToString();
		}

		// Token: 0x040003C3 RID: 963
		private Regex _regex;

		// Token: 0x02000261 RID: 609
		[ThreadAgnostic]
		public class Replacer
		{
			// Token: 0x06001600 RID: 5632 RVA: 0x00039DD8 File Offset: 0x00037FD8
			internal Replacer(string text, string replaceGroupName, string replaceWith)
			{
				this._text = text;
				this._replaceGroupName = replaceGroupName;
				this._replaceWith = replaceWith;
			}

			// Token: 0x06001601 RID: 5633 RVA: 0x00039DF5 File Offset: 0x00037FF5
			internal string EvaluateMatch(Match match)
			{
				return ReplaceLayoutRendererWrapper.ReplaceNamedGroup(this._text, this._replaceGroupName, this._replaceWith, match);
			}

			// Token: 0x04000695 RID: 1685
			private readonly string _text;

			// Token: 0x04000696 RID: 1686
			private readonly string _replaceGroupName;

			// Token: 0x04000697 RID: 1687
			private readonly string _replaceWith;
		}
	}
}
