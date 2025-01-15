using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000323 RID: 803
	[Serializable]
	internal class AlterDatabaseModifyFileGroupStatement : AlterDatabaseStatement
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x0016862F File Offset: 0x0016682F
		// (set) Token: 0x06002ABD RID: 10941 RVA: 0x00168637 File Offset: 0x00166837
		public Identifier FileGroup
		{
			get
			{
				return this._fileGroup;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fileGroup = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x00168647 File Offset: 0x00166847
		// (set) Token: 0x06002ABF RID: 10943 RVA: 0x0016864F File Offset: 0x0016684F
		public Identifier NewFileGroupName
		{
			get
			{
				return this._newFileGroupName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._newFileGroupName = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x0016865F File Offset: 0x0016685F
		// (set) Token: 0x06002AC1 RID: 10945 RVA: 0x00168667 File Offset: 0x00166867
		public bool MakeDefault
		{
			get
			{
				return this._makeDefault;
			}
			set
			{
				this._makeDefault = value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x00168670 File Offset: 0x00166870
		// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x00168678 File Offset: 0x00166878
		public ModifyFileGroupOption UpdatabilityOption
		{
			get
			{
				return this._updatabilityOption;
			}
			set
			{
				this._updatabilityOption = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x00168681 File Offset: 0x00166881
		// (set) Token: 0x06002AC5 RID: 10949 RVA: 0x00168689 File Offset: 0x00166889
		public AlterDatabaseTermination Termination
		{
			get
			{
				return this._termination;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._termination = value;
			}
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x00168699 File Offset: 0x00166899
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x001686A8 File Offset: 0x001668A8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.FileGroup != null)
			{
				this.FileGroup.Accept(visitor);
			}
			if (this.NewFileGroupName != null)
			{
				this.NewFileGroupName.Accept(visitor);
			}
			if (this.Termination != null)
			{
				this.Termination.Accept(visitor);
			}
		}

		// Token: 0x04001C7C RID: 7292
		private Identifier _fileGroup;

		// Token: 0x04001C7D RID: 7293
		private Identifier _newFileGroupName;

		// Token: 0x04001C7E RID: 7294
		private bool _makeDefault;

		// Token: 0x04001C7F RID: 7295
		private ModifyFileGroupOption _updatabilityOption;

		// Token: 0x04001C80 RID: 7296
		private AlterDatabaseTermination _termination;
	}
}
