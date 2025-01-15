using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000404 RID: 1028
	[Serializable]
	internal class AlterLoginEnableDisableStatement : AlterLoginStatement
	{
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x0016E2F1 File Offset: 0x0016C4F1
		// (set) Token: 0x06003055 RID: 12373 RVA: 0x0016E2F9 File Offset: 0x0016C4F9
		public bool IsEnable
		{
			get
			{
				return this._isEnable;
			}
			set
			{
				this._isEnable = value;
			}
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x0016E302 File Offset: 0x0016C502
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x0016E30E File Offset: 0x0016C50E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E22 RID: 7714
		private bool _isEnable;
	}
}
