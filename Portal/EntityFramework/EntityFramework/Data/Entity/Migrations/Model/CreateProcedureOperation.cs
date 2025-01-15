using System;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000B2 RID: 178
	public class CreateProcedureOperation : ProcedureOperation
	{
		// Token: 0x06000F38 RID: 3896 RVA: 0x00020391 File Offset: 0x0001E591
		public CreateProcedureOperation(string name, string bodySql, object anonymousArguments = null)
			: base(name, bodySql, anonymousArguments)
		{
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0002039C File Offset: 0x0001E59C
		public override MigrationOperation Inverse
		{
			get
			{
				return new DropProcedureOperation(this.Name, null);
			}
		}
	}
}
