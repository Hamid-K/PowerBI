using System;
using System.ComponentModel;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001D7 RID: 471
	public class ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> : ModificationStoredProcedureConfigurationBase where TEntityType : class where TTargetEntityType : class
	{
		// Token: 0x060018AC RID: 6316 RVA: 0x00042855 File Offset: 0x00040A55
		internal ManyToManyModificationStoredProcedureConfiguration()
		{
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0004285D File Offset: 0x00040A5D
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00042878 File Offset: 0x00040A78
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x000428A0 File Offset: 0x00040AA0
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> LeftKeyParameter<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x000428CF File Offset: 0x00040ACF
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> LeftKeyParameter<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x000428FE File Offset: 0x00040AFE
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> LeftKeyParameter(Expression<Func<TEntityType, string>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0004292D File Offset: 0x00040B2D
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> LeftKeyParameter(Expression<Func<TEntityType, byte[]>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, false);
			return this;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0004295C File Offset: 0x00040B5C
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> RightKeyParameter<TProperty>(Expression<Func<TTargetEntityType, TProperty>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, true);
			return this;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0004298B File Offset: 0x00040B8B
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> RightKeyParameter<TProperty>(Expression<Func<TTargetEntityType, TProperty?>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, true);
			return this;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x000429BA File Offset: 0x00040BBA
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> RightKeyParameter(Expression<Func<TTargetEntityType, string>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, true);
			return this;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000429E9 File Offset: 0x00040BE9
		public ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> RightKeyParameter(Expression<Func<TTargetEntityType, byte[]>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.Parameter(propertyExpression.GetSimplePropertyAccess(), parameterName, null, true);
			return this;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00042A18 File Offset: 0x00040C18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00042A20 File Offset: 0x00040C20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00042A29 File Offset: 0x00040C29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00042A31 File Offset: 0x00040C31
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
