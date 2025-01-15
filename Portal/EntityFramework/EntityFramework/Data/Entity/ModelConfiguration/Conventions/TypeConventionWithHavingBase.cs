using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000185 RID: 389
	internal abstract class TypeConventionWithHavingBase<T> : TypeConventionBase where T : class
	{
		// Token: 0x0600170B RID: 5899 RVA: 0x0003D66D File Offset: 0x0003B86D
		public TypeConventionWithHavingBase(IEnumerable<Func<Type, bool>> predicates, Func<Type, T> capturingPredicate)
			: base(predicates)
		{
			this._capturingPredicate = capturingPredicate;
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x0003D67D File Offset: 0x0003B87D
		internal Func<Type, T> CapturingPredicate
		{
			get
			{
				return this._capturingPredicate;
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0003D688 File Offset: 0x0003B888
		protected override void ApplyCore(Type memberInfo, ModelConfiguration modelConfiguration)
		{
			T t = this._capturingPredicate(memberInfo);
			if (t != null)
			{
				this.InvokeAction(memberInfo, modelConfiguration, t);
			}
		}

		// Token: 0x0600170E RID: 5902
		protected abstract void InvokeAction(Type memberInfo, ModelConfiguration configuration, T value);

		// Token: 0x0600170F RID: 5903 RVA: 0x0003D6B4 File Offset: 0x0003B8B4
		protected sealed override void ApplyCore(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			T t = this._capturingPredicate(memberInfo);
			if (t != null)
			{
				this.InvokeAction(memberInfo, configuration, modelConfiguration, t);
			}
		}

		// Token: 0x06001710 RID: 5904
		protected abstract void InvokeAction(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration, T value);

		// Token: 0x06001711 RID: 5905 RVA: 0x0003D6E0 File Offset: 0x0003B8E0
		protected override void ApplyCore(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			T t = this._capturingPredicate(memberInfo);
			if (t != null)
			{
				this.InvokeAction(memberInfo, configuration, modelConfiguration, t);
			}
		}

		// Token: 0x06001712 RID: 5906
		protected abstract void InvokeAction(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration, T value);

		// Token: 0x04000A27 RID: 2599
		private readonly Func<Type, T> _capturingPredicate;
	}
}
