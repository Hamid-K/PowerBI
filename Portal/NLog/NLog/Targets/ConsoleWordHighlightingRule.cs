using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using NLog.Config;

namespace NLog.Targets
{
	// Token: 0x02000030 RID: 48
	[NLogConfigurationItem]
	public class ConsoleWordHighlightingRule
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x0000B364 File Offset: 0x00009564
		public ConsoleWordHighlightingRule()
		{
			this.BackgroundColor = ConsoleOutputColor.NoChange;
			this.ForegroundColor = ConsoleOutputColor.NoChange;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000B37C File Offset: 0x0000957C
		public ConsoleWordHighlightingRule(string text, ConsoleOutputColor foregroundColor, ConsoleOutputColor backgroundColor)
		{
			this.Text = text;
			this.ForegroundColor = foregroundColor;
			this.BackgroundColor = backgroundColor;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000B399 File Offset: 0x00009599
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0000B3A1 File Offset: 0x000095A1
		public string Regex { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000B3AA File Offset: 0x000095AA
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000B3B2 File Offset: 0x000095B2
		[DefaultValue(false)]
		public bool CompileRegex { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000B3BB File Offset: 0x000095BB
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0000B3C3 File Offset: 0x000095C3
		public string Text { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000B3CC File Offset: 0x000095CC
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0000B3D4 File Offset: 0x000095D4
		[DefaultValue(false)]
		public bool WholeWords { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000B3DD File Offset: 0x000095DD
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000B3E5 File Offset: 0x000095E5
		[DefaultValue(false)]
		public bool IgnoreCase { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000B3EE File Offset: 0x000095EE
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000B3F6 File Offset: 0x000095F6
		[DefaultValue("NoChange")]
		public ConsoleOutputColor ForegroundColor { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000B3FF File Offset: 0x000095FF
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000B407 File Offset: 0x00009607
		[DefaultValue("NoChange")]
		public ConsoleOutputColor BackgroundColor { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000B410 File Offset: 0x00009610
		public Regex CompiledRegex
		{
			get
			{
				if (this._compiledRegex == null)
				{
					string regexExpression = this.GetRegexExpression();
					if (regexExpression == null)
					{
						return null;
					}
					RegexOptions regexOptions = this.GetRegexOptions(RegexOptions.Compiled);
					this._compiledRegex = new Regex(regexExpression, regexOptions);
				}
				return this._compiledRegex;
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000B44C File Offset: 0x0000964C
		private RegexOptions GetRegexOptions(RegexOptions regexOptions)
		{
			if (this.IgnoreCase)
			{
				regexOptions |= RegexOptions.IgnoreCase;
			}
			return regexOptions;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000B45C File Offset: 0x0000965C
		private string GetRegexExpression()
		{
			string text = this.Regex;
			if (text == null && this.Text != null)
			{
				text = global::System.Text.RegularExpressions.Regex.Escape(this.Text);
				if (this.WholeWords)
				{
					text = "\\b" + text + "\\b";
				}
			}
			return text;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000B4A4 File Offset: 0x000096A4
		internal MatchCollection Matches(string message)
		{
			if (this.CompileRegex)
			{
				Regex compiledRegex = this.CompiledRegex;
				if (compiledRegex == null)
				{
					return null;
				}
				return compiledRegex.Matches(message);
			}
			else
			{
				string regexExpression = this.GetRegexExpression();
				if (regexExpression != null)
				{
					RegexOptions regexOptions = this.GetRegexOptions(RegexOptions.None);
					return global::System.Text.RegularExpressions.Regex.Matches(message, regexExpression, regexOptions);
				}
				return null;
			}
		}

		// Token: 0x0400008D RID: 141
		private Regex _compiledRegex;
	}
}
