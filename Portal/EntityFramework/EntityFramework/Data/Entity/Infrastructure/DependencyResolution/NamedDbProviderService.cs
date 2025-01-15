using System;
using System.Data.Entity.Core.Common;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002B9 RID: 697
	internal class NamedDbProviderService
	{
		// Token: 0x060021F6 RID: 8694 RVA: 0x0005F6D1 File Offset: 0x0005D8D1
		public NamedDbProviderService(string invariantName, DbProviderServices providerServices)
		{
			this._invariantName = invariantName;
			this._providerServices = providerServices;
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x0005F6E7 File Offset: 0x0005D8E7
		public string InvariantName
		{
			get
			{
				return this._invariantName;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x0005F6EF File Offset: 0x0005D8EF
		public DbProviderServices ProviderServices
		{
			get
			{
				return this._providerServices;
			}
		}

		// Token: 0x04000BCE RID: 3022
		private readonly string _invariantName;

		// Token: 0x04000BCF RID: 3023
		private readonly DbProviderServices _providerServices;
	}
}
