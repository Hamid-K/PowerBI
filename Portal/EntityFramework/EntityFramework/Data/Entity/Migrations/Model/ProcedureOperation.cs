using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000C4 RID: 196
	public abstract class ProcedureOperation : MigrationOperation
	{
		// Token: 0x06000FA7 RID: 4007 RVA: 0x00020C6B File Offset: 0x0001EE6B
		protected ProcedureOperation(string name, string bodySql, object anonymousArguments = null)
			: base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._bodySql = bodySql;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x00020C99 File Offset: 0x0001EE99
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00020CA1 File Offset: 0x0001EEA1
		public string BodySql
		{
			get
			{
				return this._bodySql;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00020CA9 File Offset: 0x0001EEA9
		public virtual IList<ParameterModel> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00020CB1 File Offset: 0x0001EEB1
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400087C RID: 2172
		private readonly string _name;

		// Token: 0x0400087D RID: 2173
		private readonly string _bodySql;

		// Token: 0x0400087E RID: 2174
		private readonly List<ParameterModel> _parameters = new List<ParameterModel>();
	}
}
