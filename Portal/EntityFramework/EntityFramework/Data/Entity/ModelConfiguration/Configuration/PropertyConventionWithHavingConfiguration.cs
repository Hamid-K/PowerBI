using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001CA RID: 458
	public class PropertyConventionWithHavingConfiguration<T> where T : class
	{
		// Token: 0x06001823 RID: 6179 RVA: 0x00041988 File Offset: 0x0003FB88
		internal PropertyConventionWithHavingConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<PropertyInfo, bool>> predicates, Func<PropertyInfo, T> capturingPredicate)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
			this._capturingPredicate = capturingPredicate;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x000419A5 File Offset: 0x0003FBA5
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x000419AD File Offset: 0x0003FBAD
		internal IEnumerable<Func<PropertyInfo, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x000419B5 File Offset: 0x0003FBB5
		internal Func<PropertyInfo, T> CapturingPredicate
		{
			get
			{
				return this._capturingPredicate;
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x000419BD File Offset: 0x0003FBBD
		public void Configure(Action<ConventionPrimitivePropertyConfiguration, T> propertyConfigurationAction)
		{
			Check.NotNull<Action<ConventionPrimitivePropertyConfiguration, T>>(propertyConfigurationAction, "propertyConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new PropertyConventionWithHaving<T>(this._predicates, this._capturingPredicate, propertyConfigurationAction)
			});
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x000419F1 File Offset: 0x0003FBF1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x000419F9 File Offset: 0x0003FBF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00041A02 File Offset: 0x0003FC02
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00041A0A File Offset: 0x0003FC0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A53 RID: 2643
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x04000A54 RID: 2644
		private readonly IEnumerable<Func<PropertyInfo, bool>> _predicates;

		// Token: 0x04000A55 RID: 2645
		private readonly Func<PropertyInfo, T> _capturingPredicate;
	}
}
