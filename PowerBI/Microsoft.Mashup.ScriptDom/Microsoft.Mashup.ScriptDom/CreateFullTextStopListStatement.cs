using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200046A RID: 1130
	[Serializable]
	internal class CreateFullTextStopListStatement : TSqlStatement, IAuthorization
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x001702A7 File Offset: 0x0016E4A7
		// (set) Token: 0x06003278 RID: 12920 RVA: 0x001702AF File Offset: 0x0016E4AF
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x001702BF File Offset: 0x0016E4BF
		// (set) Token: 0x0600327A RID: 12922 RVA: 0x001702C7 File Offset: 0x0016E4C7
		public bool IsSystemStopList
		{
			get
			{
				return this._isSystemStopList;
			}
			set
			{
				this._isSystemStopList = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x001702D0 File Offset: 0x0016E4D0
		// (set) Token: 0x0600327C RID: 12924 RVA: 0x001702D8 File Offset: 0x0016E4D8
		public Identifier DatabaseName
		{
			get
			{
				return this._databaseName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._databaseName = value;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x001702E8 File Offset: 0x0016E4E8
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x001702F0 File Offset: 0x0016E4F0
		public Identifier SourceStopListName
		{
			get
			{
				return this._sourceStopListName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._sourceStopListName = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x00170300 File Offset: 0x0016E500
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x00170308 File Offset: 0x0016E508
		public Identifier Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._owner = value;
			}
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x00170318 File Offset: 0x0016E518
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x00170324 File Offset: 0x0016E524
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.DatabaseName != null)
			{
				this.DatabaseName.Accept(visitor);
			}
			if (this.SourceStopListName != null)
			{
				this.SourceStopListName.Accept(visitor);
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EAB RID: 7851
		private Identifier _name;

		// Token: 0x04001EAC RID: 7852
		private bool _isSystemStopList;

		// Token: 0x04001EAD RID: 7853
		private Identifier _databaseName;

		// Token: 0x04001EAE RID: 7854
		private Identifier _sourceStopListName;

		// Token: 0x04001EAF RID: 7855
		private Identifier _owner;
	}
}
