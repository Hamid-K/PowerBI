using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001FB RID: 507
	public class ConventionTypeConfiguration<T> where T : class
	{
		// Token: 0x06001AA8 RID: 6824 RVA: 0x000484E8 File Offset: 0x000466E8
		internal ConventionTypeConfiguration(Type type, Func<EntityTypeConfiguration> entityTypeConfiguration, ModelConfiguration modelConfiguration)
		{
			this._configuration = new ConventionTypeConfiguration(type, entityTypeConfiguration, modelConfiguration);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x000484FE File Offset: 0x000466FE
		internal ConventionTypeConfiguration(Type type, Func<ComplexTypeConfiguration> complexTypeConfiguration, ModelConfiguration modelConfiguration)
		{
			this._configuration = new ConventionTypeConfiguration(type, complexTypeConfiguration, modelConfiguration);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00048514 File Offset: 0x00046714
		internal ConventionTypeConfiguration(Type type, ModelConfiguration modelConfiguration)
		{
			this._configuration = new ConventionTypeConfiguration(type, modelConfiguration);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00048529 File Offset: 0x00046729
		[Conditional("DEBUG")]
		private static void VerifyType(Type type)
		{
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x0004852B File Offset: 0x0004672B
		public Type ClrType
		{
			get
			{
				return this._configuration.ClrType;
			}
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00048538 File Offset: 0x00046738
		public ConventionTypeConfiguration<T> HasEntitySetName(string entitySetName)
		{
			this._configuration.HasEntitySetName(entitySetName);
			return this;
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00048548 File Offset: 0x00046748
		public ConventionTypeConfiguration<T> Ignore()
		{
			this._configuration.Ignore();
			return this;
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00048557 File Offset: 0x00046757
		public ConventionTypeConfiguration<T> IsComplexType()
		{
			this._configuration.IsComplexType();
			return this;
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00048566 File Offset: 0x00046766
		public ConventionTypeConfiguration<T> Ignore<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
		{
			Check.NotNull<Expression<Func<T, TProperty>>>(propertyExpression, "propertyExpression");
			this._configuration.Ignore(propertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>());
			return this;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0004858C File Offset: 0x0004678C
		public ConventionPrimitivePropertyConfiguration Property<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
		{
			Check.NotNull<Expression<Func<T, TProperty>>>(propertyExpression, "propertyExpression");
			return this._configuration.Property(propertyExpression.GetComplexPropertyAccess());
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000485AB File Offset: 0x000467AB
		internal ConventionNavigationPropertyConfiguration NavigationProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
		{
			Check.NotNull<Expression<Func<T, TProperty>>>(propertyExpression, "propertyExpression");
			return this._configuration.NavigationProperty(propertyExpression.GetComplexPropertyAccess());
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000485CC File Offset: 0x000467CC
		public ConventionTypeConfiguration<T> HasKey<TProperty>(Expression<Func<T, TProperty>> keyExpression)
		{
			Check.NotNull<Expression<Func<T, TProperty>>>(keyExpression, "keyExpression");
			this._configuration.HasKey(from p in keyExpression.GetSimplePropertyAccessList()
				select p.Single<PropertyInfo>());
			return this;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0004861C File Offset: 0x0004681C
		public ConventionTypeConfiguration<T> ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._configuration.ToTable(tableName);
			return this;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00048638 File Offset: 0x00046838
		public ConventionTypeConfiguration<T> ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this._configuration.ToTable(tableName, schemaName);
			return this;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00048655 File Offset: 0x00046855
		public ConventionTypeConfiguration<T> HasTableAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this._configuration.HasTableAnnotation(name, value);
			return this;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x00048672 File Offset: 0x00046872
		public ConventionTypeConfiguration<T> MapToStoredProcedures()
		{
			this._configuration.MapToStoredProcedures();
			return this;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00048684 File Offset: 0x00046884
		public ConventionTypeConfiguration<T> MapToStoredProcedures(Action<ModificationStoredProceduresConfiguration<T>> modificationStoredProceduresConfigurationAction)
		{
			Check.NotNull<Action<ModificationStoredProceduresConfiguration<T>>>(modificationStoredProceduresConfigurationAction, "modificationStoredProceduresConfigurationAction");
			ModificationStoredProceduresConfiguration<T> modificationStoredProceduresConfiguration = new ModificationStoredProceduresConfiguration<T>();
			modificationStoredProceduresConfigurationAction(modificationStoredProceduresConfiguration);
			this._configuration.MapToStoredProcedures(modificationStoredProceduresConfiguration.Configuration);
			return this;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000486BC File Offset: 0x000468BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000486C4 File Offset: 0x000468C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x000486CD File Offset: 0x000468CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000486D5 File Offset: 0x000468D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000AA2 RID: 2722
		private readonly ConventionTypeConfiguration _configuration;
	}
}
