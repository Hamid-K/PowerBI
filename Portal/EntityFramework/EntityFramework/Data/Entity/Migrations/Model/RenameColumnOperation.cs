using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000C6 RID: 198
	public class RenameColumnOperation : MigrationOperation
	{
		// Token: 0x06000FC3 RID: 4035 RVA: 0x00021040 File Offset: 0x0001F240
		public RenameColumnOperation(string table, string name, string newName, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this._table = table;
			this._name = name;
			this._newName = newName;
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x0002108E File Offset: 0x0001F28E
		public virtual string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00021096 File Offset: 0x0001F296
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x0002109E File Offset: 0x0001F29E
		// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x000210A6 File Offset: 0x0001F2A6
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

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x000210AF File Offset: 0x0001F2AF
		public override MigrationOperation Inverse
		{
			get
			{
				return new RenameColumnOperation(this.Table, this.NewName, this.Name, null);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x000210C9 File Offset: 0x0001F2C9
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400088A RID: 2186
		private readonly string _table;

		// Token: 0x0400088B RID: 2187
		private readonly string _name;

		// Token: 0x0400088C RID: 2188
		private string _newName;
	}
}
