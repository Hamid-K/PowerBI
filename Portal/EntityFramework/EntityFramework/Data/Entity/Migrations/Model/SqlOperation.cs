using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000CA RID: 202
	public class SqlOperation : MigrationOperation
	{
		// Token: 0x06000FDC RID: 4060 RVA: 0x00021270 File Offset: 0x0001F470
		public SqlOperation(string sql, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(sql, "sql");
			this._sql = sql;
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0002128C File Offset: 0x0001F48C
		public virtual string Sql
		{
			get
			{
				return this._sql;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00021294 File Offset: 0x0001F494
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x0002129C File Offset: 0x0001F49C
		public virtual bool SuppressTransaction { get; set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x000212A5 File Offset: 0x0001F4A5
		public override bool IsDestructiveChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000894 RID: 2196
		private readonly string _sql;
	}
}
