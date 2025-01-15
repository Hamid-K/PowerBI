using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001D0 RID: 464
	public class ConventionDeleteModificationStoredProcedureConfiguration : ConventionModificationStoredProcedureConfiguration
	{
		// Token: 0x06001859 RID: 6233 RVA: 0x00041E1B File Offset: 0x0004001B
		internal ConventionDeleteModificationStoredProcedureConfiguration(Type type)
		{
			this._type = type;
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00041E2A File Offset: 0x0004002A
		public ConventionDeleteModificationStoredProcedureConfiguration HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00041E45 File Offset: 0x00040045
		public ConventionDeleteModificationStoredProcedureConfiguration HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00041E6D File Offset: 0x0004006D
		public ConventionDeleteModificationStoredProcedureConfiguration Parameter(string propertyName, string parameterName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(parameterName, "parameterName");
			return this.Parameter(this._type.GetAnyProperty(propertyName), parameterName);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00041E9A File Offset: 0x0004009A
		public ConventionDeleteModificationStoredProcedureConfiguration Parameter(PropertyInfo propertyInfo, string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			if (propertyInfo != null)
			{
				base.Configuration.Parameter(new PropertyPath(propertyInfo), parameterName, null, false);
			}
			return this;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00041EC6 File Offset: 0x000400C6
		public ConventionDeleteModificationStoredProcedureConfiguration RowsAffectedParameter(string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.RowsAffectedParameter(parameterName);
			return this;
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00041EE1 File Offset: 0x000400E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00041EE9 File Offset: 0x000400E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00041EF2 File Offset: 0x000400F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00041EFA File Offset: 0x000400FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A62 RID: 2658
		private readonly Type _type;
	}
}
