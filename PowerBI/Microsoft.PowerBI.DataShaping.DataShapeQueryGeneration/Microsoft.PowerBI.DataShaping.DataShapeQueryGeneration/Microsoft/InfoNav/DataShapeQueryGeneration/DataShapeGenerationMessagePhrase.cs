using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200004C RID: 76
	internal sealed class DataShapeGenerationMessagePhrase
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000AC84 File Offset: 0x00008E84
		internal DataShapeGenerationMessagePhrase(string formattedString)
		{
			this._formattedString = formattedString;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000AC93 File Offset: 0x00008E93
		internal string FormattedString
		{
			get
			{
				return this._formattedString;
			}
		}

		// Token: 0x040001FE RID: 510
		private readonly string _formattedString;
	}
}
