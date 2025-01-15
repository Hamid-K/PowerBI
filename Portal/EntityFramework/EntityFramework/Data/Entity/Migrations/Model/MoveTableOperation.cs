using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000C0 RID: 192
	public class MoveTableOperation : MigrationOperation
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x00020ABA File Offset: 0x0001ECBA
		public MoveTableOperation(string name, string newSchema, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._newSchema = newSchema;
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00020ADD File Offset: 0x0001ECDD
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00020AE5 File Offset: 0x0001ECE5
		public virtual string NewSchema
		{
			get
			{
				return this._newSchema;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00020AF0 File Offset: 0x0001ECF0
		public override MigrationOperation Inverse
		{
			get
			{
				DatabaseName databaseName = DatabaseName.Parse(this._name);
				return new MoveTableOperation(new DatabaseName(databaseName.Name, this.NewSchema).ToString(), databaseName.Schema, null)
				{
					IsSystem = this.IsSystem
				};
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00020B37 File Offset: 0x0001ED37
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00020B3A File Offset: 0x0001ED3A
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x00020B42 File Offset: 0x0001ED42
		public string ContextKey { get; internal set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00020B4B File Offset: 0x0001ED4B
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x00020B53 File Offset: 0x0001ED53
		public bool IsSystem { get; internal set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00020B5C File Offset: 0x0001ED5C
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x00020B64 File Offset: 0x0001ED64
		public CreateTableOperation CreateTableOperation { get; internal set; }

		// Token: 0x04000871 RID: 2161
		private readonly string _name;

		// Token: 0x04000872 RID: 2162
		private readonly string _newSchema;
	}
}
