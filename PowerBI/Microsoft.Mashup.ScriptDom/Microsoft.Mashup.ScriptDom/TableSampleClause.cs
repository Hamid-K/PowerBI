using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003AD RID: 941
	[Serializable]
	internal class TableSampleClause : TSqlFragment
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x0016C35C File Offset: 0x0016A55C
		// (set) Token: 0x06002E5A RID: 11866 RVA: 0x0016C364 File Offset: 0x0016A564
		public bool System
		{
			get
			{
				return this._system;
			}
			set
			{
				this._system = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x0016C36D File Offset: 0x0016A56D
		// (set) Token: 0x06002E5C RID: 11868 RVA: 0x0016C375 File Offset: 0x0016A575
		public ScalarExpression SampleNumber
		{
			get
			{
				return this._sampleNumber;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._sampleNumber = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x0016C385 File Offset: 0x0016A585
		// (set) Token: 0x06002E5E RID: 11870 RVA: 0x0016C38D File Offset: 0x0016A58D
		public TableSampleClauseOption TableSampleClauseOption
		{
			get
			{
				return this._tableSampleClauseOption;
			}
			set
			{
				this._tableSampleClauseOption = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x0016C396 File Offset: 0x0016A596
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x0016C39E File Offset: 0x0016A59E
		public ScalarExpression RepeatSeed
		{
			get
			{
				return this._repeatSeed;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._repeatSeed = value;
			}
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x0016C3AE File Offset: 0x0016A5AE
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x0016C3BA File Offset: 0x0016A5BA
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SampleNumber != null)
			{
				this.SampleNumber.Accept(visitor);
			}
			if (this.RepeatSeed != null)
			{
				this.RepeatSeed.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D9A RID: 7578
		private bool _system;

		// Token: 0x04001D9B RID: 7579
		private ScalarExpression _sampleNumber;

		// Token: 0x04001D9C RID: 7580
		private TableSampleClauseOption _tableSampleClauseOption;

		// Token: 0x04001D9D RID: 7581
		private ScalarExpression _repeatSeed;
	}
}
