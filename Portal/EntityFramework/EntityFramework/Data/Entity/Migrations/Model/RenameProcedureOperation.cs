using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000C8 RID: 200
	public class RenameProcedureOperation : MigrationOperation
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x00021158 File Offset: 0x0001F358
		public RenameProcedureOperation(string name, string newName, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this._name = name;
			this._newName = newName;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00021187 File Offset: 0x0001F387
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0002118F File Offset: 0x0001F38F
		public virtual string NewName
		{
			get
			{
				return this._newName;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00021198 File Offset: 0x0001F398
		public override MigrationOperation Inverse
		{
			get
			{
				DatabaseName databaseName = DatabaseName.Parse(this._name);
				return new RenameProcedureOperation(new DatabaseName(DatabaseName.Parse(this._newName).Name, databaseName.Schema).ToString(), databaseName.Name, null);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x000211DD File Offset: 0x0001F3DD
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000890 RID: 2192
		private readonly string _name;

		// Token: 0x04000891 RID: 2193
		private readonly string _newName;
	}
}
