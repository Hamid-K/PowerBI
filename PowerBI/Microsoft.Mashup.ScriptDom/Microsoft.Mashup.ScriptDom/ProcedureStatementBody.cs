using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001C1 RID: 449
	[Serializable]
	internal abstract class ProcedureStatementBody : ProcedureStatementBodyBase
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x0015F871 File Offset: 0x0015DA71
		// (set) Token: 0x06002294 RID: 8852 RVA: 0x0015F879 File Offset: 0x0015DA79
		public ProcedureReference ProcedureReference
		{
			get
			{
				return this._procedureReference;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._procedureReference = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x0015F889 File Offset: 0x0015DA89
		// (set) Token: 0x06002296 RID: 8854 RVA: 0x0015F891 File Offset: 0x0015DA91
		public bool IsForReplication
		{
			get
			{
				return this._isForReplication;
			}
			set
			{
				this._isForReplication = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x0015F89A File Offset: 0x0015DA9A
		public IList<ProcedureOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0015F8A4 File Offset: 0x0015DAA4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.ProcedureReference != null)
			{
				this.ProcedureReference.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A33 RID: 6707
		private ProcedureReference _procedureReference;

		// Token: 0x04001A34 RID: 6708
		private bool _isForReplication;

		// Token: 0x04001A35 RID: 6709
		private List<ProcedureOption> _options = new List<ProcedureOption>();
	}
}
