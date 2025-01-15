using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001D4 RID: 468
	public class ConventionUpdateModificationStoredProcedureConfiguration : ConventionModificationStoredProcedureConfiguration
	{
		// Token: 0x06001879 RID: 6265 RVA: 0x0004214F File Offset: 0x0004034F
		internal ConventionUpdateModificationStoredProcedureConfiguration(Type type)
		{
			this._type = type;
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0004215E File Offset: 0x0004035E
		public ConventionUpdateModificationStoredProcedureConfiguration HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00042179 File Offset: 0x00040379
		public ConventionUpdateModificationStoredProcedureConfiguration HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000421A1 File Offset: 0x000403A1
		public ConventionUpdateModificationStoredProcedureConfiguration Parameter(string propertyName, string parameterName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(parameterName, "parameterName");
			return this.Parameter(this._type.GetAnyProperty(propertyName), parameterName);
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x000421CE File Offset: 0x000403CE
		public ConventionUpdateModificationStoredProcedureConfiguration Parameter(PropertyInfo propertyInfo, string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			if (propertyInfo != null)
			{
				base.Configuration.Parameter(new PropertyPath(propertyInfo), parameterName, null, false);
			}
			return this;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x000421FA File Offset: 0x000403FA
		public ConventionUpdateModificationStoredProcedureConfiguration Parameter(string propertyName, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			return this.Parameter(this._type.GetAnyProperty(propertyName), currentValueParameterName, originalValueParameterName);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00042234 File Offset: 0x00040434
		public ConventionUpdateModificationStoredProcedureConfiguration Parameter(PropertyInfo propertyInfo, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			if (propertyInfo != null)
			{
				base.Configuration.Parameter(new PropertyPath(propertyInfo), currentValueParameterName, originalValueParameterName, false);
			}
			return this;
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0004226C File Offset: 0x0004046C
		public ConventionUpdateModificationStoredProcedureConfiguration Result(string propertyName, string columnName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(new PropertyPath(this._type.GetAnyProperty(propertyName)), columnName);
			return this;
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x000422A4 File Offset: 0x000404A4
		public ConventionUpdateModificationStoredProcedureConfiguration Result(PropertyInfo propertyInfo, string columnName)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(new PropertyPath(propertyInfo), columnName);
			return this;
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x000422D1 File Offset: 0x000404D1
		public ConventionUpdateModificationStoredProcedureConfiguration RowsAffectedParameter(string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.RowsAffectedParameter(parameterName);
			return this;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x000422EC File Offset: 0x000404EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x000422F4 File Offset: 0x000404F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000422FD File Offset: 0x000404FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00042305 File Offset: 0x00040505
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A67 RID: 2663
		private readonly Type _type;
	}
}
