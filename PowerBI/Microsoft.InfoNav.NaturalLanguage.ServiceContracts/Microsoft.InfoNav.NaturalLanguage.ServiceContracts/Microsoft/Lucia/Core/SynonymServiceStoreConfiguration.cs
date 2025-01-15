using System;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000F1 RID: 241
	[ImmutableObject(true)]
	public sealed class SynonymServiceStoreConfiguration
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x000089A4 File Offset: 0x00006BA4
		public SynonymServiceStoreConfiguration(LanguageIdentifier language, StringStoreSpecification stringStoreSpecification, StringSequenceStoreSpecification stringSequenceStoreSpecification, SynonymEntriesStoreSpecification synonymEntriesStoreSpecification)
		{
			Contract.CheckValue<StringStoreSpecification>(stringStoreSpecification, "stringStoreSpecification");
			Contract.CheckValue<StringSequenceStoreSpecification>(stringSequenceStoreSpecification, "stringSequenceStoreSpecification");
			Contract.CheckValue<SynonymEntriesStoreSpecification>(synonymEntriesStoreSpecification, "synonymEntriesStoreSpecification");
			this._language = language;
			this._stringStoreSpecification = stringStoreSpecification;
			this._stringSequenceStoreSpecification = stringSequenceStoreSpecification;
			this._synonymEntriesStoreSpecification = synonymEntriesStoreSpecification;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x000089F6 File Offset: 0x00006BF6
		public LanguageIdentifier Language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000089FE File Offset: 0x00006BFE
		public StringStoreSpecification StringStoreSpecification
		{
			get
			{
				return this._stringStoreSpecification;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00008A06 File Offset: 0x00006C06
		public StringSequenceStoreSpecification StringSequenceStoreSpecification
		{
			get
			{
				return this._stringSequenceStoreSpecification;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00008A0E File Offset: 0x00006C0E
		public SynonymEntriesStoreSpecification SynonymEntriesStoreSpecification
		{
			get
			{
				return this._synonymEntriesStoreSpecification;
			}
		}

		// Token: 0x0400052D RID: 1325
		private readonly LanguageIdentifier _language;

		// Token: 0x0400052E RID: 1326
		private readonly StringStoreSpecification _stringStoreSpecification;

		// Token: 0x0400052F RID: 1327
		private readonly StringSequenceStoreSpecification _stringSequenceStoreSpecification;

		// Token: 0x04000530 RID: 1328
		private readonly SynonymEntriesStoreSpecification _synonymEntriesStoreSpecification;
	}
}
