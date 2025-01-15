using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000312 RID: 786
	[Serializable]
	internal class FileDeclaration : TSqlFragment
	{
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x00168012 File Offset: 0x00166212
		public IList<FileDeclarationOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x0016801A File Offset: 0x0016621A
		// (set) Token: 0x06002A53 RID: 10835 RVA: 0x00168022 File Offset: 0x00166222
		public bool IsPrimary
		{
			get
			{
				return this._isPrimary;
			}
			set
			{
				this._isPrimary = value;
			}
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x0016802B File Offset: 0x0016622B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x00168038 File Offset: 0x00166238
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C5E RID: 7262
		private List<FileDeclarationOption> _options = new List<FileDeclarationOption>();

		// Token: 0x04001C5F RID: 7263
		private bool _isPrimary;
	}
}
