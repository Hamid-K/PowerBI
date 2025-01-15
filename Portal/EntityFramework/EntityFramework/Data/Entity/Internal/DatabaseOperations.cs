using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000F3 RID: 243
	internal class DatabaseOperations
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x0002EF3E File Offset: 0x0002D13E
		public virtual bool Create(ObjectContext objectContext)
		{
			objectContext.CreateDatabase();
			return true;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0002EF48 File Offset: 0x0002D148
		public virtual bool Exists(DbConnection connection, int? commandTimeout, Lazy<StoreItemCollection> storeItemCollection)
		{
			if (connection.State == ConnectionState.Open)
			{
				return true;
			}
			bool flag;
			try
			{
				flag = DbProviderServices.GetProviderServices(connection).DatabaseExists(connection, commandTimeout, storeItemCollection);
			}
			catch
			{
				try
				{
					connection.Open();
					flag = true;
				}
				catch (Exception)
				{
					flag = false;
				}
				finally
				{
					connection.Close();
				}
			}
			return flag;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0002EFB4 File Offset: 0x0002D1B4
		public virtual void Delete(ObjectContext objectContext)
		{
			objectContext.DeleteDatabase();
		}
	}
}
