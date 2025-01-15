using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002B3 RID: 691
	[Serializable]
	internal class StopListFullTextIndexOption : FullTextIndexOption
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x0016608A File Offset: 0x0016428A
		// (set) Token: 0x0600284C RID: 10316 RVA: 0x00166092 File Offset: 0x00164292
		public bool IsOff
		{
			get
			{
				return this._isOff;
			}
			set
			{
				this._isOff = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x0016609B File Offset: 0x0016429B
		// (set) Token: 0x0600284E RID: 10318 RVA: 0x001660A3 File Offset: 0x001642A3
		public Identifier StopListName
		{
			get
			{
				return this._stopListName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._stopListName = value;
			}
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x001660B3 File Offset: 0x001642B3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x001660BF File Offset: 0x001642BF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.StopListName != null)
			{
				this.StopListName.Accept(visitor);
			}
		}

		// Token: 0x04001BDA RID: 7130
		private bool _isOff;

		// Token: 0x04001BDB RID: 7131
		private Identifier _stopListName;
	}
}
