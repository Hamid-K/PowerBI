using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000BF RID: 191
	public class MoveProcedureOperation : MigrationOperation
	{
		// Token: 0x06000F84 RID: 3972 RVA: 0x00020A46 File Offset: 0x0001EC46
		public MoveProcedureOperation(string name, string newSchema, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._newSchema = newSchema;
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00020A69 File Offset: 0x0001EC69
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00020A71 File Offset: 0x0001EC71
		public virtual string NewSchema
		{
			get
			{
				return this._newSchema;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00020A7C File Offset: 0x0001EC7C
		public override MigrationOperation Inverse
		{
			get
			{
				DatabaseName databaseName = DatabaseName.Parse(this._name);
				return new MoveProcedureOperation(new DatabaseName(databaseName.Name, this.NewSchema).ToString(), databaseName.Schema, null);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00020AB7 File Offset: 0x0001ECB7
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400086F RID: 2159
		private readonly string _name;

		// Token: 0x04000870 RID: 2160
		private readonly string _newSchema;
	}
}
