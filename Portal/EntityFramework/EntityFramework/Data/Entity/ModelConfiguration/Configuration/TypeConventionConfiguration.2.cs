using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001CC RID: 460
	public class TypeConventionConfiguration<T> where T : class
	{
		// Token: 0x06001837 RID: 6199 RVA: 0x00041ADA File Offset: 0x0003FCDA
		internal TypeConventionConfiguration(ConventionsConfiguration conventionsConfiguration)
			: this(conventionsConfiguration, Enumerable.Empty<Func<Type, bool>>())
		{
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00041AE8 File Offset: 0x0003FCE8
		private TypeConventionConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<Type, bool>> predicates)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x00041AFE File Offset: 0x0003FCFE
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x00041B06 File Offset: 0x0003FD06
		internal IEnumerable<Func<Type, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00041B0E File Offset: 0x0003FD0E
		public TypeConventionConfiguration<T> Where(Func<Type, bool> predicate)
		{
			Check.NotNull<Func<Type, bool>>(predicate, "predicate");
			return new TypeConventionConfiguration<T>(this._conventionsConfiguration, this._predicates.Append(predicate));
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00041B33 File Offset: 0x0003FD33
		public TypeConventionWithHavingConfiguration<T, TValue> Having<TValue>(Func<Type, TValue> capturingPredicate) where TValue : class
		{
			Check.NotNull<Func<Type, TValue>>(capturingPredicate, "capturingPredicate");
			return new TypeConventionWithHavingConfiguration<T, TValue>(this._conventionsConfiguration, this._predicates, capturingPredicate);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00041B53 File Offset: 0x0003FD53
		public void Configure(Action<ConventionTypeConfiguration<T>> entityConfigurationAction)
		{
			Check.NotNull<Action<ConventionTypeConfiguration<T>>>(entityConfigurationAction, "entityConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new TypeConvention<T>(this._predicates, entityConfigurationAction)
			});
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00041B81 File Offset: 0x0003FD81
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00041B89 File Offset: 0x0003FD89
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00041B92 File Offset: 0x0003FD92
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00041B9A File Offset: 0x0003FD9A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A58 RID: 2648
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x04000A59 RID: 2649
		private readonly IEnumerable<Func<Type, bool>> _predicates;
	}
}
