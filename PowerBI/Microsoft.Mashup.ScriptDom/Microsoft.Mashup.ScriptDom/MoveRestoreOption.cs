using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000350 RID: 848
	[Serializable]
	internal class MoveRestoreOption : RestoreOption
	{
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x00169AA5 File Offset: 0x00167CA5
		// (set) Token: 0x06002BFF RID: 11263 RVA: 0x00169AAD File Offset: 0x00167CAD
		public ValueExpression LogicalFileName
		{
			get
			{
				return this._logicalFileName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._logicalFileName = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x00169ABD File Offset: 0x00167CBD
		// (set) Token: 0x06002C01 RID: 11265 RVA: 0x00169AC5 File Offset: 0x00167CC5
		public ValueExpression OSFileName
		{
			get
			{
				return this._oSFileName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._oSFileName = value;
			}
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x00169AD5 File Offset: 0x00167CD5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x00169AE1 File Offset: 0x00167CE1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.LogicalFileName != null)
			{
				this.LogicalFileName.Accept(visitor);
			}
			if (this.OSFileName != null)
			{
				this.OSFileName.Accept(visitor);
			}
		}

		// Token: 0x04001CE3 RID: 7395
		private ValueExpression _logicalFileName;

		// Token: 0x04001CE4 RID: 7396
		private ValueExpression _oSFileName;
	}
}
