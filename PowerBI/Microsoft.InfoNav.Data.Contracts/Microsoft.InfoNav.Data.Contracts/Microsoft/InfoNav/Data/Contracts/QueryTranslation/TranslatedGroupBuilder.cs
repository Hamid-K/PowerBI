using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000BA RID: 186
	public sealed class TranslatedGroupBuilder<TParent> : BaseBuilder<TranslatedGroup, TParent>
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x0000BB74 File Offset: 0x00009D74
		public TranslatedGroupBuilder(TranslatedGroup group, TParent parent)
			: base(parent)
		{
			this._group = group;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000BB84 File Offset: 0x00009D84
		public TranslatedGroupBuilder<TParent> WithSubtotalIndicatorColumnName(string name)
		{
			this._group.SubtotalIndicatorColumnName = name;
			return this;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000BB93 File Offset: 0x00009D93
		public override TranslatedGroup Build()
		{
			return this._group;
		}

		// Token: 0x04000214 RID: 532
		private readonly TranslatedGroup _group;
	}
}
