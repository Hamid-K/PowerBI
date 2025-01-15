using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003D0 RID: 976
	[Serializable]
	internal class TopRowFilter : TSqlFragment
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x0016D2BC File Offset: 0x0016B4BC
		// (set) Token: 0x06002F37 RID: 12087 RVA: 0x0016D2C4 File Offset: 0x0016B4C4
		public ScalarExpression Expression
		{
			get
			{
				return this._expression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._expression = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x0016D2D4 File Offset: 0x0016B4D4
		// (set) Token: 0x06002F39 RID: 12089 RVA: 0x0016D2DC File Offset: 0x0016B4DC
		public bool Percent
		{
			get
			{
				return this._percent;
			}
			set
			{
				this._percent = value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x0016D2E5 File Offset: 0x0016B4E5
		// (set) Token: 0x06002F3B RID: 12091 RVA: 0x0016D2ED File Offset: 0x0016B4ED
		public bool WithTies
		{
			get
			{
				return this._withTies;
			}
			set
			{
				this._withTies = value;
			}
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x0016D2F6 File Offset: 0x0016B4F6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x0016D302 File Offset: 0x0016B502
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DDC RID: 7644
		private ScalarExpression _expression;

		// Token: 0x04001DDD RID: 7645
		private bool _percent;

		// Token: 0x04001DDE RID: 7646
		private bool _withTies;
	}
}
