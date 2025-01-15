using System;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Storage;
using Microsoft.Data.SqlClient;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x0200002C RID: 44
	internal sealed class ProductInfoDataAccessor : IProductInfoDataAccessor, IDisposable
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00006E99 File Offset: 0x00005099
		public ProductInfoDataAccessor()
		{
			this._sql = CatalogAccessFactory.NewConnection();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006EAC File Offset: 0x000050AC
		public ProductInfoDataAccessor(ISqlAccess existingSqlAccess)
		{
			this._sql = ReferenceSqlAccess.UseButDoNotDispose(existingSqlAccess);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006EC0 File Offset: 0x000050C0
		public async Task<ProductInfoEntity> GetCurrentProductInfoAsync()
		{
			ProductInfoEntity productInfoEntity;
			try
			{
				productInfoEntity = await this._sql.QueryFirstOrDefaultAsync<ProductInfoEntity>("GetCurrentProductInfo");
			}
			catch (SqlException ex)
			{
				if (ex.HResult != -2146232060)
				{
					throw;
				}
				Logger.Info("Could not find ProductInfo table. Will do first-time upgrade.", Array.Empty<object>());
				productInfoEntity = null;
			}
			return productInfoEntity;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006F08 File Offset: 0x00005108
		public async Task<int> AddProductInfoAsync(ProductInfoEntity productInfoEntity)
		{
			return await this._sql.ExecuteAsync("AddProductInfo", productInfoEntity);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006F55 File Offset: 0x00005155
		public void Dispose()
		{
			this._sql.Dispose();
		}

		// Token: 0x040000A7 RID: 167
		private readonly ISqlAccess _sql;
	}
}
