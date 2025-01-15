using System;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000118 RID: 280
	[ImmutableObject(true)]
	public sealed class TextAnalysisLexiconSpecification
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x0000A8E1 File Offset: 0x00008AE1
		public TextAnalysisLexiconSpecification(string lexiconFilePath)
		{
			Contract.CheckNonEmpty(lexiconFilePath, "lexiconFilePath");
			this._lexiconFilePath = lexiconFilePath;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000A8FB File Offset: 0x00008AFB
		public string LexiconFilePath
		{
			get
			{
				return this._lexiconFilePath;
			}
		}

		// Token: 0x040005CF RID: 1487
		private readonly string _lexiconFilePath;
	}
}
