using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000183 RID: 387
	internal abstract class TypeConventionBase : IConfigurationConvention<Type, EntityTypeConfiguration>, IConvention, IConfigurationConvention<Type, ComplexTypeConfiguration>, IConfigurationConvention<Type>
	{
		// Token: 0x060016FE RID: 5886 RVA: 0x0003D533 File Offset: 0x0003B733
		protected TypeConventionBase(IEnumerable<Func<Type, bool>> predicates)
		{
			this._predicates = predicates;
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0003D542 File Offset: 0x0003B742
		internal IEnumerable<Func<Type, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0003D54C File Offset: 0x0003B74C
		public void Apply(Type memberInfo, ModelConfiguration modelConfiguration)
		{
			if (this._predicates.All((Func<Type, bool> p) => p(memberInfo)))
			{
				this.ApplyCore(memberInfo, modelConfiguration);
			}
		}

		// Token: 0x06001701 RID: 5889
		protected abstract void ApplyCore(Type memberInfo, ModelConfiguration modelConfiguration);

		// Token: 0x06001702 RID: 5890 RVA: 0x0003D58C File Offset: 0x0003B78C
		public void Apply(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			if (this._predicates.All((Func<Type, bool> p) => p(memberInfo)))
			{
				this.ApplyCore(memberInfo, configuration, modelConfiguration);
			}
		}

		// Token: 0x06001703 RID: 5891
		protected abstract void ApplyCore(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration);

		// Token: 0x06001704 RID: 5892 RVA: 0x0003D5D0 File Offset: 0x0003B7D0
		public void Apply(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			if (this._predicates.All((Func<Type, bool> p) => p(memberInfo)))
			{
				this.ApplyCore(memberInfo, configuration, modelConfiguration);
			}
		}

		// Token: 0x06001705 RID: 5893
		protected abstract void ApplyCore(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration);

		// Token: 0x04000A25 RID: 2597
		private readonly IEnumerable<Func<Type, bool>> _predicates;
	}
}
