using System;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001CF RID: 463
	public class AssociationModificationStoredProcedureConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x06001854 RID: 6228 RVA: 0x00041CB6 File Offset: 0x0003FEB6
		internal AssociationModificationStoredProcedureConfiguration(PropertyInfo navigationPropertyInfo, ModificationStoredProcedureConfiguration configuration)
		{
			this._navigationPropertyInfo = navigationPropertyInfo;
			this._configuration = configuration;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00041CCC File Offset: 0x0003FECC
		public AssociationModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			this._configuration.Parameter(new PropertyPath(new PropertyInfo[] { this._navigationPropertyInfo }.Concat(propertyExpression.GetSimplePropertyAccess())), parameterName, null, false);
			return this;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00041D20 File Offset: 0x0003FF20
		public AssociationModificationStoredProcedureConfiguration<TEntityType> Parameter<TProperty>(Expression<Func<TEntityType, TProperty?>> propertyExpression, string parameterName) where TProperty : struct
		{
			Check.NotNull<Expression<Func<TEntityType, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			this._configuration.Parameter(new PropertyPath(new PropertyInfo[] { this._navigationPropertyInfo }.Concat(propertyExpression.GetSimplePropertyAccess())), parameterName, null, false);
			return this;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00041D74 File Offset: 0x0003FF74
		public AssociationModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, string>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, string>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			this._configuration.Parameter(new PropertyPath(new PropertyInfo[] { this._navigationPropertyInfo }.Concat(propertyExpression.GetSimplePropertyAccess())), parameterName, null, false);
			return this;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00041DC8 File Offset: 0x0003FFC8
		public AssociationModificationStoredProcedureConfiguration<TEntityType> Parameter(Expression<Func<TEntityType, byte[]>> propertyExpression, string parameterName)
		{
			Check.NotNull<Expression<Func<TEntityType, byte[]>>>(propertyExpression, "propertyExpression");
			Check.NotEmpty(parameterName, "parameterName");
			this._configuration.Parameter(new PropertyPath(new PropertyInfo[] { this._navigationPropertyInfo }.Concat(propertyExpression.GetSimplePropertyAccess())), parameterName, null, false);
			return this;
		}

		// Token: 0x04000A60 RID: 2656
		private readonly PropertyInfo _navigationPropertyInfo;

		// Token: 0x04000A61 RID: 2657
		private readonly ModificationStoredProcedureConfiguration _configuration;
	}
}
