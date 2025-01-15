using System;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200004E RID: 78
	internal class DatabaseSpecificationProxy : IDatabaseSpecificationProxy
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00006502 File Offset: 0x00004702
		public DatabaseSpecificationProxy(string key, IDatabaseSpecificationProxyQuery proxyQuery)
		{
			this.Key = key;
			this.m_proxyQuery = proxyQuery;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006518 File Offset: 0x00004718
		public IDatabaseSpecification GetSpecification()
		{
			return this.m_proxyQuery.GetSpecification(this.Key);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000652C File Offset: 0x0000472C
		public IDatabaseSpecification GetEnabledSpecification()
		{
			IDatabaseSpecification specification = this.m_proxyQuery.GetSpecification(this.Key);
			if (specification.OperationMode == StorageOperationMode.None)
			{
				throw new DatabaseStorageModeViolationException(specification.OperationMode, StorageOperationMode.Read | StorageOperationMode.Write);
			}
			return specification;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006561 File Offset: 0x00004761
		public IDatabaseSpecification GetSpecificationPreferSecondary()
		{
			return this.m_proxyQuery.GetSpecificationPreferSecondary(this.Key);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006574 File Offset: 0x00004774
		public IDatabaseSpecification GetEnabledSpecificationPreferSecondary()
		{
			IDatabaseSpecification specificationPreferSecondary = this.m_proxyQuery.GetSpecificationPreferSecondary(this.Key);
			if (specificationPreferSecondary.OperationMode == StorageOperationMode.None)
			{
				throw new DatabaseStorageModeViolationException(specificationPreferSecondary.OperationMode, StorageOperationMode.Read);
			}
			return specificationPreferSecondary;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000065A9 File Offset: 0x000047A9
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000065B1 File Offset: 0x000047B1
		public string Key { get; private set; }

		// Token: 0x040000D2 RID: 210
		private readonly IDatabaseSpecificationProxyQuery m_proxyQuery;
	}
}
