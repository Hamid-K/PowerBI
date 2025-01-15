using System;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000AE RID: 174
	public class AlterProcedureOperation : ProcedureOperation
	{
		// Token: 0x06000F16 RID: 3862 RVA: 0x0001FD7A File Offset: 0x0001DF7A
		public AlterProcedureOperation(string name, string bodySql, object anonymousArguments = null)
			: base(name, bodySql, anonymousArguments)
		{
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0001FD85 File Offset: 0x0001DF85
		public override MigrationOperation Inverse
		{
			get
			{
				return NotSupportedOperation.Instance;
			}
		}
	}
}
