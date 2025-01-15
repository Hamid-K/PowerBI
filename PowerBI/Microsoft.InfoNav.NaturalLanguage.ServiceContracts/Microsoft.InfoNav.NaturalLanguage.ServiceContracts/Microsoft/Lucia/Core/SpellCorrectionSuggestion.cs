using System;
using System.ComponentModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000CB RID: 203
	[ImmutableObject(true)]
	public sealed class SpellCorrectionSuggestion
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x00007D41 File Offset: 0x00005F41
		public SpellCorrectionSuggestion(string suggestion, double suggestionConfidence)
		{
			this._suggestion = suggestion;
			this._suggestionConfidence = suggestionConfidence;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00007D57 File Offset: 0x00005F57
		public string Suggestion
		{
			get
			{
				return this._suggestion;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00007D5F File Offset: 0x00005F5F
		public double SuggestionConfidence
		{
			get
			{
				return this._suggestionConfidence;
			}
		}

		// Token: 0x040004CE RID: 1230
		private readonly string _suggestion;

		// Token: 0x040004CF RID: 1231
		private readonly double _suggestionConfidence;
	}
}
