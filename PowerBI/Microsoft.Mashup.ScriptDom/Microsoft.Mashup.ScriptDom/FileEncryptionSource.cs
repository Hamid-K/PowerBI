using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200036D RID: 877
	[Serializable]
	internal class FileEncryptionSource : EncryptionSource
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x0016A72E File Offset: 0x0016892E
		// (set) Token: 0x06002CB8 RID: 11448 RVA: 0x0016A736 File Offset: 0x00168936
		public bool IsExecutable
		{
			get
			{
				return this._isExecutable;
			}
			set
			{
				this._isExecutable = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x0016A73F File Offset: 0x0016893F
		// (set) Token: 0x06002CBA RID: 11450 RVA: 0x0016A747 File Offset: 0x00168947
		public Literal File
		{
			get
			{
				return this._file;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._file = value;
			}
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x0016A757 File Offset: 0x00168957
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x0016A763 File Offset: 0x00168963
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.File != null)
			{
				this.File.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D1B RID: 7451
		private bool _isExecutable;

		// Token: 0x04001D1C RID: 7452
		private Literal _file;
	}
}
