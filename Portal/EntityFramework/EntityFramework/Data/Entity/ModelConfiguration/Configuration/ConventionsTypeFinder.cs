using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001C8 RID: 456
	internal class ConventionsTypeFinder
	{
		// Token: 0x06001815 RID: 6165 RVA: 0x00041833 File Offset: 0x0003FA33
		public ConventionsTypeFinder()
			: this(new ConventionsTypeFilter(), new ConventionsTypeActivator())
		{
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00041845 File Offset: 0x0003FA45
		public ConventionsTypeFinder(ConventionsTypeFilter conventionsTypeFilter, ConventionsTypeActivator conventionsTypeActivator)
		{
			this._conventionsTypeFilter = conventionsTypeFilter;
			this._conventionsTypeActivator = conventionsTypeActivator;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0004185C File Offset: 0x0003FA5C
		public void AddConventions(IEnumerable<Type> types, Action<IConvention> addFunction)
		{
			foreach (Type type in types)
			{
				if (this._conventionsTypeFilter.IsConvention(type))
				{
					addFunction(this._conventionsTypeActivator.Activate(type));
				}
			}
		}

		// Token: 0x04000A4F RID: 2639
		private readonly ConventionsTypeFilter _conventionsTypeFilter;

		// Token: 0x04000A50 RID: 2640
		private readonly ConventionsTypeActivator _conventionsTypeActivator;
	}
}
