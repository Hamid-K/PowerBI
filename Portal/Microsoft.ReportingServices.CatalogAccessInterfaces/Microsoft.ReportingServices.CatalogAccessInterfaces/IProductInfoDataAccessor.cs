using System;
using System.Threading.Tasks;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000013 RID: 19
	public interface IProductInfoDataAccessor : IDisposable
	{
		// Token: 0x060000D9 RID: 217
		Task<ProductInfoEntity> GetCurrentProductInfoAsync();

		// Token: 0x060000DA RID: 218
		Task<int> AddProductInfoAsync(ProductInfoEntity productInfoEntity);
	}
}
