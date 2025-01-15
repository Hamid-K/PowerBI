using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000B9 RID: 185
	public sealed class TranslatedGroupsBuilder<TParent> : BaseBuilder<TranslatedGroups, TParent>
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0000BACB File Offset: 0x00009CCB
		public TranslatedGroupsBuilder(TranslatedGroups groups, TParent parent)
			: base(parent)
		{
			this._groups = groups;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000BADC File Offset: 0x00009CDC
		public TranslatedGroupBuilder<TranslatedGroupsBuilder<TParent>> WithPrimaryGroup()
		{
			if (this._groups.Primary == null)
			{
				this._groups.Primary = new List<TranslatedGroup>();
			}
			TranslatedGroup translatedGroup = new TranslatedGroup();
			this._groups.Primary.Add(translatedGroup);
			return new TranslatedGroupBuilder<TranslatedGroupsBuilder<TParent>>(translatedGroup, this);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000BB24 File Offset: 0x00009D24
		public TranslatedGroupBuilder<TranslatedGroupsBuilder<TParent>> WithSecondary()
		{
			if (this._groups.Secondary == null)
			{
				this._groups.Secondary = new List<TranslatedGroup>();
			}
			TranslatedGroup translatedGroup = new TranslatedGroup();
			this._groups.Secondary.Add(translatedGroup);
			return new TranslatedGroupBuilder<TranslatedGroupsBuilder<TParent>>(translatedGroup, this);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000BB6C File Offset: 0x00009D6C
		public override TranslatedGroups Build()
		{
			return this._groups;
		}

		// Token: 0x04000213 RID: 531
		private readonly TranslatedGroups _groups;
	}
}
