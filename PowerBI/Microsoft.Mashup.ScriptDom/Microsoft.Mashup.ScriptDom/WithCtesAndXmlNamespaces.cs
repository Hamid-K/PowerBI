using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001D0 RID: 464
	[Serializable]
	internal class WithCtesAndXmlNamespaces : TSqlFragment
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x0015FF25 File Offset: 0x0015E125
		// (set) Token: 0x060022E6 RID: 8934 RVA: 0x0015FF2D File Offset: 0x0015E12D
		public XmlNamespaces XmlNamespaces
		{
			get
			{
				return this._xmlNamespaces;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._xmlNamespaces = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x0015FF3D File Offset: 0x0015E13D
		public IList<CommonTableExpression> CommonTableExpressions
		{
			get
			{
				return this._commonTableExpressions;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x0015FF45 File Offset: 0x0015E145
		// (set) Token: 0x060022E9 RID: 8937 RVA: 0x0015FF4D File Offset: 0x0015E14D
		public ValueExpression ChangeTrackingContext
		{
			get
			{
				return this._changeTrackingContext;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._changeTrackingContext = value;
			}
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0015FF5D File Offset: 0x0015E15D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0015FF6C File Offset: 0x0015E16C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.XmlNamespaces != null)
			{
				this.XmlNamespaces.Accept(visitor);
			}
			int i = 0;
			int count = this.CommonTableExpressions.Count;
			while (i < count)
			{
				this.CommonTableExpressions[i].Accept(visitor);
				i++;
			}
			if (this.ChangeTrackingContext != null)
			{
				this.ChangeTrackingContext.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A49 RID: 6729
		private XmlNamespaces _xmlNamespaces;

		// Token: 0x04001A4A RID: 6730
		private List<CommonTableExpression> _commonTableExpressions = new List<CommonTableExpression>();

		// Token: 0x04001A4B RID: 6731
		private ValueExpression _changeTrackingContext;
	}
}
