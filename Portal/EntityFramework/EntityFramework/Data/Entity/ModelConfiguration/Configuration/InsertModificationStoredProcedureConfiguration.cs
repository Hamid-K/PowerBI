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
	// Token: 0x020001D6 RID: 470
	public class InsertModificationStoredProcedureConfiguration<TEntityType> : ModificationStoredProcedureConfigurationBase where TEntityType : class
	{
		// Token: 0x06001897 RID: 6295 RVA: 0x00042539 File Offset: 0x00040739
		internal InsertModificationStoredProcedureConfiguration()
		{
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00042541 File Offset: 0x00040741
		public InsertModificationStoredProcedureConfiguration<TEntityType> HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0004255C File Offset: 0x0004075C
		public InsertModificationStoredProcedureConfiguration<TEntityType> HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00042584 File Offset: 0x00040784
		public InsertModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x000425B3 File Offset: 0x000407B3
		public InsertModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x000425E2 File Offset: 0x000407E2
		public InsertModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, string>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00042611 File Offset: 0x00040811
		public InsertModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, byte[]>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00042640 File Offset: 0x00040840
		public InsertModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeography>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeography>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0004266F File Offset: 0x0004086F
		public InsertModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeometry>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeometry>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0004269E File Offset: 0x0004089E
		public InsertModificationStoredProcedureConfiguration<TEntityType> Result<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string columnName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000426CB File Offset: 0x000408CB
		public InsertModificationStoredProcedureConfiguration<TEntityType> Result<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string columnName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x000426F8 File Offset: 0x000408F8
		public InsertModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, string>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00042725 File Offset: 0x00040925
		public InsertModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, byte[]>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00042752 File Offset: 0x00040952
		public InsertModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, DbGeography>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeography>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0004277F File Offset: 0x0004097F
		public InsertModificationStoredProcedureConfiguration<TEntityType> Result(Expression<Func<TEntityType, DbGeometry>> propertyExpression, string columnName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeometry>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(propertyExpression.GetSimplePropertyAccess(), columnName);
			return this;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000427AC File Offset: 0x000409AC
		public InsertModificationStoredProcedureConfiguration<TEntityType> Navigation<TPrincipalEntityType>(Expression<Func<TPrincipalEntityType, TEntityType>> navigationPropertyExpression, Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>> associationModificationStoredProcedureConfigurationAction) where TPrincipalEntityType : class
		{
			Check.NotNull<Expression<Func<TPrincipalEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			Check.NotNull<Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>>>(associationModificationStoredProcedureConfigurationAction, "associationModificationStoredProcedureConfigurationAction");
			AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType> associationModificationStoredProcedureConfiguration = new AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>(), base.Configuration);
			associationModificationStoredProcedureConfigurationAction(associationModificationStoredProcedureConfiguration);
			return this;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000427F0 File Offset: 0x000409F0
		public InsertModificationStoredProcedureConfiguration<TEntityType> Navigation<TPrincipalEntityType>(Expression<Func<TPrincipalEntityType, ICollection<TEntityType>>> navigationPropertyExpression, Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>> associationModificationStoredProcedureConfigurationAction) where TPrincipalEntityType : class
		{
			Check.NotNull<Expression<Func<TPrincipalEntityType, ICollection<TEntityType>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			Check.NotNull<Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>>>(associationModificationStoredProcedureConfigurationAction, "associationModificationStoredProcedureConfigurationAction");
			AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType> associationModificationStoredProcedureConfiguration = new AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>(), base.Configuration);
			associationModificationStoredProcedureConfigurationAction(associationModificationStoredProcedureConfiguration);
			return this;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00042834 File Offset: 0x00040A34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0004283C File Offset: 0x00040A3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00042845 File Offset: 0x00040A45
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0004284D File Offset: 0x00040A4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
