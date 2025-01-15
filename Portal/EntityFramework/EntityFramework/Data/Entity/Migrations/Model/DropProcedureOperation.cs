using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000B8 RID: 184
	public class DropProcedureOperation : MigrationOperation
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x00020705 File Offset: 0x0001E905
		public DropProcedureOperation(string name, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00020721 File Offset: 0x0001E921
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x00020729 File Offset: 0x0001E929
		public override MigrationOperation Inverse
		{
			get
			{
				return NotSupportedOperation.Instance;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00020730 File Offset: 0x0001E930
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000861 RID: 2145
		private readonly string _name;
	}
}
