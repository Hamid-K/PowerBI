using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001CD RID: 461
	public class TypeConventionWithHavingConfiguration<T> where T : class
	{
		// Token: 0x06001842 RID: 6210 RVA: 0x00041BA2 File Offset: 0x0003FDA2
		internal TypeConventionWithHavingConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<Type, bool>> predicates, Func<Type, T> capturingPredicate)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
			this._capturingPredicate = capturingPredicate;
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00041BBF File Offset: 0x0003FDBF
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00041BC7 File Offset: 0x0003FDC7
		internal IEnumerable<Func<Type, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x00041BCF File Offset: 0x0003FDCF
		internal Func<Type, T> CapturingPredicate
		{
			get
			{
				return this._capturingPredicate;
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00041BD7 File Offset: 0x0003FDD7
		public void Configure(Action<ConventionTypeConfiguration, T> entityConfigurationAction)
		{
			Check.NotNull<Action<ConventionTypeConfiguration, T>>(entityConfigurationAction, "entityConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new TypeConventionWithHaving<T>(this._predicates, this._capturingPredicate, entityConfigurationAction)
			});
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00041C0B File Offset: 0x0003FE0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00041C13 File Offset: 0x0003FE13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00041C1C File Offset: 0x0003FE1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00041C24 File Offset: 0x0003FE24
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A5A RID: 2650
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x04000A5B RID: 2651
		private readonly IEnumerable<Func<Type, bool>> _predicates;

		// Token: 0x04000A5C RID: 2652
		private readonly Func<Type, T> _capturingPredicate;
	}
}
