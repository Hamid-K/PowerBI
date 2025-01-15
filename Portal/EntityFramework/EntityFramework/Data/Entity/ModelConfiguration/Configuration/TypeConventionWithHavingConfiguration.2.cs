using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001CE RID: 462
	public class TypeConventionWithHavingConfiguration<T, TValue> where T : class where TValue : class
	{
		// Token: 0x0600184B RID: 6219 RVA: 0x00041C2C File Offset: 0x0003FE2C
		internal TypeConventionWithHavingConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<Type, bool>> predicates, Func<Type, TValue> capturingPredicate)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
			this._capturingPredicate = capturingPredicate;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x00041C49 File Offset: 0x0003FE49
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x00041C51 File Offset: 0x0003FE51
		internal IEnumerable<Func<Type, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00041C59 File Offset: 0x0003FE59
		internal Func<Type, TValue> CapturingPredicate
		{
			get
			{
				return this._capturingPredicate;
			}
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00041C61 File Offset: 0x0003FE61
		public void Configure(Action<ConventionTypeConfiguration<T>, TValue> entityConfigurationAction)
		{
			Check.NotNull<Action<ConventionTypeConfiguration<T>, TValue>>(entityConfigurationAction, "entityConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new TypeConventionWithHaving<T, TValue>(this._predicates, this._capturingPredicate, entityConfigurationAction)
			});
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00041C95 File Offset: 0x0003FE95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00041C9D File Offset: 0x0003FE9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00041CA6 File Offset: 0x0003FEA6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00041CAE File Offset: 0x0003FEAE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A5D RID: 2653
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x04000A5E RID: 2654
		private readonly IEnumerable<Func<Type, bool>> _predicates;

		// Token: 0x04000A5F RID: 2655
		private readonly Func<Type, TValue> _capturingPredicate;
	}
}
