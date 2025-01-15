using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200005D RID: 93
	internal sealed class TranslationMessagePhrase
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0000D93A File Offset: 0x0000BB3A
		internal TranslationMessagePhrase(string formattedString)
		{
			this.m_formattedString = formattedString;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000D949 File Offset: 0x0000BB49
		internal string FormattedString
		{
			get
			{
				return this.m_formattedString;
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000D951 File Offset: 0x0000BB51
		public override string ToString()
		{
			return this.m_formattedString;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000D959 File Offset: 0x0000BB59
		public static implicit operator string(TranslationMessagePhrase phrase)
		{
			if (phrase == null)
			{
				return null;
			}
			return phrase.m_formattedString;
		}

		// Token: 0x0400025C RID: 604
		private readonly string m_formattedString;
	}
}
