using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000275 RID: 629
	[Serializable]
	internal class AlterAssemblyStatement : AssemblyStatement
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x001646C5 File Offset: 0x001628C5
		public IList<Literal> DropFiles
		{
			get
			{
				return this._dropFiles;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x001646CD File Offset: 0x001628CD
		// (set) Token: 0x060026CF RID: 9935 RVA: 0x001646D5 File Offset: 0x001628D5
		public bool IsDropAll
		{
			get
			{
				return this._isDropAll;
			}
			set
			{
				this._isDropAll = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x001646DE File Offset: 0x001628DE
		public IList<AddFileSpec> AddFiles
		{
			get
			{
				return this._addFiles;
			}
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x001646E6 File Offset: 0x001628E6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x001646F4 File Offset: 0x001628F4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.DropFiles.Count;
			while (i < count)
			{
				this.DropFiles[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.AddFiles.Count;
			while (j < count2)
			{
				this.AddFiles[j].Accept(visitor);
				j++;
			}
		}

		// Token: 0x04001B6B RID: 7019
		private List<Literal> _dropFiles = new List<Literal>();

		// Token: 0x04001B6C RID: 7020
		private bool _isDropAll;

		// Token: 0x04001B6D RID: 7021
		private List<AddFileSpec> _addFiles = new List<AddFileSpec>();
	}
}
