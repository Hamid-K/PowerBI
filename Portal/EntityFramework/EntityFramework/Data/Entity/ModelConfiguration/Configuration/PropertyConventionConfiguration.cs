using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001C9 RID: 457
	public class PropertyConventionConfiguration
	{
		// Token: 0x06001818 RID: 6168 RVA: 0x000418C0 File Offset: 0x0003FAC0
		internal PropertyConventionConfiguration(ConventionsConfiguration conventionsConfiguration)
			: this(conventionsConfiguration, Enumerable.Empty<Func<PropertyInfo, bool>>())
		{
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000418CE File Offset: 0x0003FACE
		private PropertyConventionConfiguration(ConventionsConfiguration conventionsConfiguration, IEnumerable<Func<PropertyInfo, bool>> predicates)
		{
			this._conventionsConfiguration = conventionsConfiguration;
			this._predicates = predicates;
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x000418E4 File Offset: 0x0003FAE4
		internal ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x000418EC File Offset: 0x0003FAEC
		internal IEnumerable<Func<PropertyInfo, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x000418F4 File Offset: 0x0003FAF4
		public PropertyConventionConfiguration Where(Func<PropertyInfo, bool> predicate)
		{
			Check.NotNull<Func<PropertyInfo, bool>>(predicate, "predicate");
			return new PropertyConventionConfiguration(this._conventionsConfiguration, this._predicates.Append(predicate));
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00041919 File Offset: 0x0003FB19
		public PropertyConventionWithHavingConfiguration<T> Having<T>(Func<PropertyInfo, T> capturingPredicate) where T : class
		{
			Check.NotNull<Func<PropertyInfo, T>>(capturingPredicate, "capturingPredicate");
			return new PropertyConventionWithHavingConfiguration<T>(this._conventionsConfiguration, this._predicates, capturingPredicate);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00041939 File Offset: 0x0003FB39
		public void Configure(Action<ConventionPrimitivePropertyConfiguration> propertyConfigurationAction)
		{
			Check.NotNull<Action<ConventionPrimitivePropertyConfiguration>>(propertyConfigurationAction, "propertyConfigurationAction");
			this._conventionsConfiguration.Add(new IConvention[]
			{
				new PropertyConvention(this._predicates, propertyConfigurationAction)
			});
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00041967 File Offset: 0x0003FB67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0004196F File Offset: 0x0003FB6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00041978 File Offset: 0x0003FB78
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00041980 File Offset: 0x0003FB80
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A51 RID: 2641
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x04000A52 RID: 2642
		private readonly IEnumerable<Func<PropertyInfo, bool>> _predicates;
	}
}
