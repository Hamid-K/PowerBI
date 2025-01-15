using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001CB RID: 459
	public class TypeConventionConfiguration
	{
		// Token: 0x0600182C RID: 6188 RVA: 0x00041A12 File Offset: 0x0003FC12
		internal TypeConventionConfiguration(ConventionsConfiguration conventionsConfiguration)
			: this(conventionsConfiguration, Enumerable.Empty<Func<Type, bool>>())
		{
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00041A20 File Offset: 0x0003FC20
		private TypeConventionConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<Type, bool>> predicates)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x00041A36 File Offset: 0x0003FC36
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x00041A3E File Offset: 0x0003FC3E
		internal IEnumerable<Func<Type, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00041A46 File Offset: 0x0003FC46
		public TypeConventionConfiguration Where(Func<Type, bool> predicate)
		{
			Check.NotNull<Func<Type, bool>>(predicate, "predicate");
			return new TypeConventionConfiguration(this._conventionsConfiguration, this._predicates.Append(predicate));
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00041A6B File Offset: 0x0003FC6B
		public TypeConventionWithHavingConfiguration<T> Having<T>(Func<Type, T> capturingPredicate) where T : class
		{
			Check.NotNull<Func<Type, T>>(capturingPredicate, "capturingPredicate");
			return new TypeConventionWithHavingConfiguration<T>(this._conventionsConfiguration, this._predicates, capturingPredicate);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00041A8B File Offset: 0x0003FC8B
		public void Configure(Action<ConventionTypeConfiguration> entityConfigurationAction)
		{
			Check.NotNull<Action<ConventionTypeConfiguration>>(entityConfigurationAction, "entityConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new TypeConvention(this._predicates, entityConfigurationAction)
			});
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00041AB9 File Offset: 0x0003FCB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00041AC1 File Offset: 0x0003FCC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00041ACA File Offset: 0x0003FCCA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00041AD2 File Offset: 0x0003FCD2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A56 RID: 2646
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x04000A57 RID: 2647
		private readonly IEnumerable<Func<Type, bool>> _predicates;
	}
}
