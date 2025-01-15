using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000311 RID: 785
	[Serializable]
	internal class CreateDatabaseStatement : TSqlStatement, ICollationSetter
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06002A3F RID: 10815 RVA: 0x00167E3E File Offset: 0x0016603E
		// (set) Token: 0x06002A40 RID: 10816 RVA: 0x00167E46 File Offset: 0x00166046
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

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x00167E56 File Offset: 0x00166056
		// (set) Token: 0x06002A42 RID: 10818 RVA: 0x00167E5E File Offset: 0x0016605E
		public ContainmentDatabaseOption Containment
		{
			get
			{
				return this._containment;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._containment = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x00167E6E File Offset: 0x0016606E
		public IList<FileGroupDefinition> FileGroups
		{
			get
			{
				return this._fileGroups;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x00167E76 File Offset: 0x00166076
		public IList<FileDeclaration> LogOn
		{
			get
			{
				return this._logOn;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x00167E7E File Offset: 0x0016607E
		public IList<DatabaseOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x00167E86 File Offset: 0x00166086
		// (set) Token: 0x06002A47 RID: 10823 RVA: 0x00167E8E File Offset: 0x0016608E
		public AttachMode AttachMode
		{
			get
			{
				return this._attachMode;
			}
			set
			{
				this._attachMode = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x00167E97 File Offset: 0x00166097
		// (set) Token: 0x06002A49 RID: 10825 RVA: 0x00167E9F File Offset: 0x0016609F
		public Identifier DatabaseSnapshot
		{
			get
			{
				return this._databaseSnapshot;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._databaseSnapshot = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06002A4A RID: 10826 RVA: 0x00167EAF File Offset: 0x001660AF
		// (set) Token: 0x06002A4B RID: 10827 RVA: 0x00167EB7 File Offset: 0x001660B7
		public MultiPartIdentifier CopyOf
		{
			get
			{
				return this._copyOf;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._copyOf = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x00167EC7 File Offset: 0x001660C7
		// (set) Token: 0x06002A4D RID: 10829 RVA: 0x00167ECF File Offset: 0x001660CF
		public Identifier Collation
		{
			get
			{
				return this._collation;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._collation = value;
			}
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x00167EDF File Offset: 0x001660DF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x00167EEC File Offset: 0x001660EC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.DatabaseName != null)
			{
				this.DatabaseName.Accept(visitor);
			}
			if (this.Containment != null)
			{
				this.Containment.Accept(visitor);
			}
			int i = 0;
			int count = this.FileGroups.Count;
			while (i < count)
			{
				this.FileGroups[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.LogOn.Count;
			while (j < count2)
			{
				this.LogOn[j].Accept(visitor);
				j++;
			}
			int k = 0;
			int count3 = this.Options.Count;
			while (k < count3)
			{
				this.Options[k].Accept(visitor);
				k++;
			}
			if (this.DatabaseSnapshot != null)
			{
				this.DatabaseSnapshot.Accept(visitor);
			}
			if (this.CopyOf != null)
			{
				this.CopyOf.Accept(visitor);
			}
			if (this.Collation != null)
			{
				this.Collation.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C55 RID: 7253
		private Identifier _databaseName;

		// Token: 0x04001C56 RID: 7254
		private ContainmentDatabaseOption _containment;

		// Token: 0x04001C57 RID: 7255
		private List<FileGroupDefinition> _fileGroups = new List<FileGroupDefinition>();

		// Token: 0x04001C58 RID: 7256
		private List<FileDeclaration> _logOn = new List<FileDeclaration>();

		// Token: 0x04001C59 RID: 7257
		private List<DatabaseOption> _options = new List<DatabaseOption>();

		// Token: 0x04001C5A RID: 7258
		private AttachMode _attachMode;

		// Token: 0x04001C5B RID: 7259
		private Identifier _databaseSnapshot;

		// Token: 0x04001C5C RID: 7260
		private MultiPartIdentifier _copyOf;

		// Token: 0x04001C5D RID: 7261
		private Identifier _collation;
	}
}
