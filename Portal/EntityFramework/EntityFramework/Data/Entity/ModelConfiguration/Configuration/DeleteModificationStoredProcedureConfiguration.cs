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
	// Token: 0x020001D5 RID: 469
	public class DeleteModificationStoredProcedureConfiguration<TEntityType> : ModificationStoredProcedureConfigurationBase where TEntityType : class
	{
		// Token: 0x06001887 RID: 6279 RVA: 0x0004230D File Offset: 0x0004050D
		internal DeleteModificationStoredProcedureConfiguration()
		{
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x00042315 File Offset: 0x00040515
		public DeleteModificationStoredProcedureConfiguration<TEntityType> HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00042330 File Offset: 0x00040530
		public DeleteModificationStoredProcedureConfiguration<TEntityType> HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00042358 File Offset: 0x00040558
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00042387 File Offset: 0x00040587
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x000423B6 File Offset: 0x000405B6
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, string>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x000423E5 File Offset: 0x000405E5
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, byte[]>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00042414 File Offset: 0x00040614
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeography>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeography>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00042443 File Offset: 0x00040643
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, DbGeometry>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, DbGeometry>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetComplexPropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00042472 File Offset: 0x00040672
		public DeleteModificationStoredProcedureConfiguration<TEntityType> RowsAffectedParameter(string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.RowsAffectedParameter(parameterName);
			return this;
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00042490 File Offset: 0x00040690
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Navigation<TPrincipalEntityType>(Expression<Func<TPrincipalEntityType, TEntityType>> navigationPropertyExpression, Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>> associationModificationStoredProcedureConfigurationAction) where TPrincipalEntityType : class
		{
			Check.NotNull<Expression<Func<TPrincipalEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			Check.NotNull<Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>>>(associationModificationStoredProcedureConfigurationAction, "associationModificationStoredProcedureConfigurationAction");
			AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType> associationModificationStoredProcedureConfiguration = new AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>(), base.Configuration);
			associationModificationStoredProcedureConfigurationAction(associationModificationStoredProcedureConfiguration);
			return this;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x000424D4 File Offset: 0x000406D4
		public DeleteModificationStoredProcedureConfiguration<TEntityType> Navigation<TPrincipalEntityType>(Expression<Func<TPrincipalEntityType, ICollection<TEntityType>>> navigationPropertyExpression, Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>> associationModificationStoredProcedureConfigurationAction) where TPrincipalEntityType : class
		{
			Check.NotNull<Expression<Func<TPrincipalEntityType, ICollection<TEntityType>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			Check.NotNull<Action<AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>>>(associationModificationStoredProcedureConfigurationAction, "associationModificationStoredProcedureConfigurationAction");
			AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType> associationModificationStoredProcedureConfiguration = new AssociationModificationStoredProcedureConfiguration<TPrincipalEntityType>(navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>(), base.Configuration);
			associationModificationStoredProcedureConfigurationAction(associationModificationStoredProcedureConfiguration);
			return this;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00042518 File Offset: 0x00040718
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00042520 File Offset: 0x00040720
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00042529 File Offset: 0x00040729
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00042531 File Offset: 0x00040731
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
