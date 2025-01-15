using System;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000B1 RID: 177
	[ImmutableObject(true)]
	public sealed class OfficeThesaurusSpecification : PoolObjectSpecification
	{
		// Token: 0x06000392 RID: 914 RVA: 0x00006CD8 File Offset: 0x00004ED8
		public OfficeThesaurusSpecification(LanguageIdentifier language, int minPoolSize, int maxPoolSize, string lexiconFile, string dllFile)
			: base(minPoolSize, maxPoolSize)
		{
			Contract.CheckNonEmpty(lexiconFile, "lexiconFile");
			Contract.CheckNonEmpty(dllFile, "dllFile");
			this.LexiconFile = lexiconFile;
			this.DllPath = dllFile;
			this.Language = language;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00006D11 File Offset: 0x00004F11
		public string LexiconFile { get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00006D19 File Offset: 0x00004F19
		public string DllPath { get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00006D21 File Offset: 0x00004F21
		public LanguageIdentifier Language { get; }
	}
}
