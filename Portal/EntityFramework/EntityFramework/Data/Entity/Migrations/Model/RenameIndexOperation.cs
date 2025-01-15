using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000C7 RID: 199
	public class RenameIndexOperation : MigrationOperation
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x000210CC File Offset: 0x0001F2CC
		public RenameIndexOperation(string table, string name, string newName, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this._table = table;
			this._name = name;
			this._newName = newName;
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0002111A File Offset: 0x0001F31A
		public virtual string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00021122 File Offset: 0x0001F322
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0002112A File Offset: 0x0001F32A
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x00021132 File Offset: 0x0001F332
		public virtual string NewName
		{
			get
			{
				return this._newName;
			}
			internal set
			{
				this._newName = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0002113B File Offset: 0x0001F33B
		public override MigrationOperation Inverse
		{
			get
			{
				return new RenameIndexOperation(this.Table, this.NewName, this.Name, null);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00021155 File Offset: 0x0001F355
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400088D RID: 2189
		private readonly string _table;

		// Token: 0x0400088E RID: 2190
		private readonly string _name;

		// Token: 0x0400088F RID: 2191
		private string _newName;
	}
}
