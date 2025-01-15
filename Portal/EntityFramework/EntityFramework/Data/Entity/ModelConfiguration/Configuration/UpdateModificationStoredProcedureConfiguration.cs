using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001DD RID: 477
	public class UpdateModificationStoredProcedureConfiguration<TEntityType> : ModificationStoredProcedureConfigurationBase where TEntityType : class
	{
		// Token: 0x060018F3 RID: 6387 RVA: 0x0004369D File Offset: 0x0004189D
		internal UpdateModificationStoredProcedureConfiguration()
		{
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x000436A5 File Offset: 0x000418A5
		public UpdateModificationStoredProcedureConfiguration<TEntityType> HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000436C0 File Offset: 0x000418C0
		public UpdateModificationStoredProcedureConfiguration<TEntityType> HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000436E8 File Offset: 0x000418E8
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00043717 File Offset: 0x00041917
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00043746 File Offset: 0x00041946
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, string>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00043775 File Offset: 0x00041975
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, byte[]>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000437A4 File Offset: 0x000419A4
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeography>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeography>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x000437D3 File Offset: 0x000419D3
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeometry>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeometry>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x00043802 File Offset: 0x00041A02
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string currentValueParameterName, string originalValueParameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0004383D File Offset: 0x00041A3D
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string currentValueParameterName, string originalValueParameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00043878 File Offset: 0x00041A78
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, string>> propertyExpression, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000438B3 File Offset: 0x00041AB3
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, byte[]>> propertyExpression, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000438EE File Offset: 0x00041AEE
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeography>> propertyExpression, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeography>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00043929 File Offset: 0x00041B29
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeometry>> propertyExpression, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeometry>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), currentValueParameterName, originalValueParameterName, false);
			return this;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00043964 File Offset: 0x00041B64
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string columnName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00043991 File Offset: 0x00041B91
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string columnName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x000439BE File Offset: 0x00041BBE
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, string>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x000439EB File Offset: 0x00041BEB
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, byte[]>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00043A18 File Offset: 0x00041C18
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, DbGeography>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeography>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00043A45 File Offset: 0x00041C45
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, DbGeometry>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeometry>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00043A72 File Offset: 0x00041C72
		public UpdateModificationStoredProcedureConfiguration<TEntityType> RowsAffectedParameter(string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.RowsAffectedParameter(parameterName);
			return this;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00043A90 File Offset: 0x00041C90
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Navigation<TPrincipalEntityType>(Expression<Func<TPrincipalEntityType, TEntityType>> navigationPropertyExpression, Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>> associationModificationStoredProcedureConfigurationAction) where TPrincipalEntityType : class
		{
			Check.NotNull<Expression<Func<TPrincipalEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			Check.NotNull<Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>>>(associationModificationStoredProcedureConfigurationAction, "associationModificationStoredProcedureConfigurationAction");
			AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType> associationModificationStoredProcedureConfiguration = new AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>(), base.Configuration);
			associationModificationStoredProcedureConfigurationAction(associationModificationStoredProcedureConfiguration);
			return this;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00043AD4 File Offset: 0x00041CD4
		public UpdateModificationStoredProcedureConfiguration<TEntityType> Navigation<TPrincipalEntityType>(Expression<Func<TPrincipalEntityType, ICollection<TEntityType>>> navigationPropertyExpression, Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>> associationModificationStoredProcedureConfigurationAction) where TPrincipalEntityType : class
		{
			Check.NotNull<Expression<Func<TPrincipalEntityType, ICollection<TEntityType>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			Check.NotNull<Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>>>(associationModificationStoredProcedureConfigurationAction, "associationModificationStoredProcedureConfigurationAction");
			AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType> associationModificationStoredProcedureConfiguration = new AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>(), base.Configuration);
			associationModificationStoredProcedureConfigurationAction(associationModificationStoredProcedureConfiguration);
			return this;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00043B18 File Offset: 0x00041D18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00043B20 File Offset: 0x00041D20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00043B29 File Offset: 0x00041D29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00043B31 File Offset: 0x00041D31
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
