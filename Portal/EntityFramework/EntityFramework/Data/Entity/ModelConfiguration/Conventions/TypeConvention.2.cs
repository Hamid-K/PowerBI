using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000187 RID: 391
	internal class TypeConvention<T> : TypeConventionBase where T : class
	{
		// Token: 0x06001718 RID: 5912 RVA: 0x0003D772 File Offset: 0x0003B972
		public TypeConvention(IEnumerable<Func<Type, bool>> predicates, Action<ConventionTypeConfiguration<T>> entityConfigurationAction)
			: base(predicates.Prepend(TypeConvention<T>._ofTypePredicate))
		{
			this._entityConfigurationAction = entityConfigurationAction;
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0003D78C File Offset: 0x0003B98C
		internal Action<ConventionTypeConfiguration<T>> EntityConfigurationAction
		{
			get
			{
				return this._entityConfigurationAction;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0003D794 File Offset: 0x0003B994
		internal static Func<Type, bool> OfTypePredicate
		{
			get
			{
				return TypeConvention<T>._ofTypePredicate;
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0003D79B File Offset: 0x0003B99B
		protected override void ApplyCore(Type memberInfo, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, modelConfiguration));
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0003D7AF File Offset: 0x0003B9AF
		protected override void ApplyCore(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, configuration, modelConfiguration));
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0003D7C4 File Offset: 0x0003B9C4
		protected override void ApplyCore(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, configuration, modelConfiguration));
		}

		// Token: 0x04000A29 RID: 2601
		private static readonly Func<Type, bool> _ofTypePredicate = (Type t) => typeof(T).IsAssignableFrom(t);

		// Token: 0x04000A2A RID: 2602
		private readonly Action<ConventionTypeConfiguration<T>> _entityConfigurationAction;
	}
}
