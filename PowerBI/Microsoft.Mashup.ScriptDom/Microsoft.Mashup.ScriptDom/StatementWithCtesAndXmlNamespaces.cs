using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001EE RID: 494
	[Serializable]
	internal abstract class StatementWithCtesAndXmlNamespaces : TSqlStatement
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x00160B06 File Offset: 0x0015ED06
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x00160B0E File Offset: 0x0015ED0E
		public WithCtesAndXmlNamespaces WithCtesAndXmlNamespaces
		{
			get
			{
				return this._withCtesAndXmlNamespaces;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._withCtesAndXmlNamespaces = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x00160B1E File Offset: 0x0015ED1E
		public IList<OptimizerHint> OptimizerHints
		{
			get
			{
				return this._optimizerHints;
			}
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x00160B28 File Offset: 0x0015ED28
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.WithCtesAndXmlNamespaces != null)
			{
				this.WithCtesAndXmlNamespaces.Accept(visitor);
			}
			int i = 0;
			int count = this.OptimizerHints.Count;
			while (i < count)
			{
				this.OptimizerHints[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A7B RID: 6779
		private WithCtesAndXmlNamespaces _withCtesAndXmlNamespaces;

		// Token: 0x04001A7C RID: 6780
		private List<OptimizerHint> _optimizerHints = new List<OptimizerHint>();
	}
}
