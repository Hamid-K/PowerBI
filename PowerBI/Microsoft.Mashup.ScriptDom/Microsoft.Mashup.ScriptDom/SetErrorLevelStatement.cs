using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000310 RID: 784
	[Serializable]
	internal class SetErrorLevelStatement : TSqlStatement
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06002A3A RID: 10810 RVA: 0x00167DF5 File Offset: 0x00165FF5
		// (set) Token: 0x06002A3B RID: 10811 RVA: 0x00167DFD File Offset: 0x00165FFD
		public ScalarExpression Level
		{
			get
			{
				return this._level;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._level = value;
			}
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x00167E0D File Offset: 0x0016600D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x00167E19 File Offset: 0x00166019
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Level != null)
			{
				this.Level.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C54 RID: 7252
		private ScalarExpression _level;
	}
}
