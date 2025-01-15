using System;
using System.Collections.Generic;
using System.Globalization;

namespace AngleSharp.Services
{
	// Token: 0x02000038 RID: 56
	public interface ISpellCheckService
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600013D RID: 317
		CultureInfo Culture { get; }

		// Token: 0x0600013E RID: 318
		void Ignore(string word, bool persistent);

		// Token: 0x0600013F RID: 319
		bool IsCorrect(string word);

		// Token: 0x06000140 RID: 320
		IEnumerable<string> SuggestFor(string word);
	}
}
