using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200012C RID: 300
	public sealed class TextAnalysisResult : IUtteranceText
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x0000AA01 File Offset: 0x00008C01
		public TextAnalysisResult(string utterance, IEnumerable<IStemmerToken> tokens, LanguageIdentifier language)
		{
			this._tokens = tokens.AsReadOnlyCollection<IStemmerToken>();
			this._utteranceTokens = this._tokens.Cast<IUtteranceToken>().AsReadOnlyCollection<IUtteranceToken>();
			this._utterance = utterance;
			this._language = language;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000AA39 File Offset: 0x00008C39
		public string Utterance
		{
			get
			{
				return this._utterance;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000AA41 File Offset: 0x00008C41
		public LanguageIdentifier Language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000AA49 File Offset: 0x00008C49
		ReadOnlyCollection<IUtteranceToken> IUtteranceText.Tokens
		{
			get
			{
				return this._utteranceTokens;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000AA51 File Offset: 0x00008C51
		public ReadOnlyCollection<IStemmerToken> Tokens
		{
			get
			{
				return this._tokens;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000AA59 File Offset: 0x00008C59
		public CultureInfo Culture
		{
			get
			{
				if (this._culture == null)
				{
					this._culture = this._language.ToCultureInfo();
				}
				return this._culture;
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000AA7C File Offset: 0x00008C7C
		public override string ToString()
		{
			string text = string.Join("|", this._tokens.Select((IStemmerToken t) => t.Value));
			return StringUtil.FormatInvariant("{0}: {1} [{2}]", this._language, this._utterance, text);
		}

		// Token: 0x040005E0 RID: 1504
		private readonly string _utterance;

		// Token: 0x040005E1 RID: 1505
		private readonly ReadOnlyCollection<IStemmerToken> _tokens;

		// Token: 0x040005E2 RID: 1506
		private readonly ReadOnlyCollection<IUtteranceToken> _utteranceTokens;

		// Token: 0x040005E3 RID: 1507
		private readonly LanguageIdentifier _language;

		// Token: 0x040005E4 RID: 1508
		private CultureInfo _culture;
	}
}
