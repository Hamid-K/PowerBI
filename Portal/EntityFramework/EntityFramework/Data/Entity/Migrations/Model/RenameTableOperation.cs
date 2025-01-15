using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000C9 RID: 201
	public class RenameTableOperation : MigrationOperation
	{
		// Token: 0x06000FD6 RID: 4054 RVA: 0x000211E0 File Offset: 0x0001F3E0
		public RenameTableOperation(string name, string newName, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this._name = name;
			this._newName = newName;
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0002120F File Offset: 0x0001F40F
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00021217 File Offset: 0x0001F417
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x0002121F File Offset: 0x0001F41F
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

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00021228 File Offset: 0x0001F428
		public override MigrationOperation Inverse
		{
			get
			{
				DatabaseName databaseName = DatabaseName.Parse(this._name);
				return new RenameTableOperation(new DatabaseName(DatabaseName.Parse(this._newName).Name, databaseName.Schema).ToString(), databaseName.Name, null);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0002126D File Offset: 0x0001F46D
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000892 RID: 2194
		private readonly string _name;

		// Token: 0x04000893 RID: 2195
		private string _newName;
	}
}
