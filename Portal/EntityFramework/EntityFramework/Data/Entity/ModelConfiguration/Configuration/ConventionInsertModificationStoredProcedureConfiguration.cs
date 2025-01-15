using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001D1 RID: 465
	public class ConventionInsertModificationStoredProcedureConfiguration : ConventionModificationStoredProcedureConfiguration
	{
		// Token: 0x06001863 RID: 6243 RVA: 0x00041F02 File Offset: 0x00040102
		internal ConventionInsertModificationStoredProcedureConfiguration(Type type)
		{
			this._type = type;
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00041F11 File Offset: 0x00040111
		public ConventionInsertModificationStoredProcedureConfiguration HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00041F2C File Offset: 0x0004012C
		public ConventionInsertModificationStoredProcedureConfiguration HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00041F54 File Offset: 0x00040154
		public ConventionInsertModificationStoredProcedureConfiguration Parameter(string propertyName, string parameterName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(parameterName, "parameterName");
			return this.Parameter(this._type.GetAnyProperty(propertyName), parameterName);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00041F81 File Offset: 0x00040181
		public ConventionInsertModificationStoredProcedureConfiguration Parameter(PropertyInfo propertyInfo, string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			if (propertyInfo != null)
			{
				base.Configuration.Parameter(new PropertyPath(propertyInfo), parameterName, null, false);
			}
			return this;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00041FAD File Offset: 0x000401AD
		public ConventionInsertModificationStoredProcedureConfiguration Result(string propertyName, string columnName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(new PropertyPath(this._type.GetAnyProperty(propertyName)), columnName);
			return this;
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00041FE5 File Offset: 0x000401E5
		public ConventionInsertModificationStoredProcedureConfiguration Result(PropertyInfo propertyInfo, string columnName)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(new PropertyPath(propertyInfo), columnName);
			return this;
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00042012 File Offset: 0x00040212
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0004201A File Offset: 0x0004021A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00042023 File Offset: 0x00040223
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0004202B File Offset: 0x0004022B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A63 RID: 2659
		private readonly Type _type;
	}
}
