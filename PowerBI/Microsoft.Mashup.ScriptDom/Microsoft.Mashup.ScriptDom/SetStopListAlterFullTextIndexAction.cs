using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003EC RID: 1004
	[Serializable]
	internal class SetStopListAlterFullTextIndexAction : AlterFullTextIndexAction
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x0016D9BC File Offset: 0x0016BBBC
		// (set) Token: 0x06002FC1 RID: 12225 RVA: 0x0016D9C4 File Offset: 0x0016BBC4
		public StopListFullTextIndexOption StopListOption
		{
			get
			{
				return this._stopListOption;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._stopListOption = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x0016D9D4 File Offset: 0x0016BBD4
		// (set) Token: 0x06002FC3 RID: 12227 RVA: 0x0016D9DC File Offset: 0x0016BBDC
		public bool WithNoPopulation
		{
			get
			{
				return this._withNoPopulation;
			}
			set
			{
				this._withNoPopulation = value;
			}
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x0016D9E5 File Offset: 0x0016BBE5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x0016D9F1 File Offset: 0x0016BBF1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.StopListOption != null)
			{
				this.StopListOption.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DF8 RID: 7672
		private StopListFullTextIndexOption _stopListOption;

		// Token: 0x04001DF9 RID: 7673
		private bool _withNoPopulation;
	}
}
