using System;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Spatial
{
	// Token: 0x02000095 RID: 149
	public abstract class DbSpatialDataReader
	{
		// Token: 0x06000567 RID: 1383
		public abstract DbGeography GetGeography(int ordinal);

		// Token: 0x06000568 RID: 1384 RVA: 0x00012500 File Offset: 0x00010700
		public virtual Task<DbGeography> GetGeographyAsync(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelper.FromCancellation<DbGeography>();
			}
			Task<DbGeography> task;
			try
			{
				task = Task.FromResult<DbGeography>(this.GetGeography(ordinal));
			}
			catch (Exception ex)
			{
				task = TaskHelper.FromException<DbGeography>(ex);
			}
			return task;
		}

		// Token: 0x06000569 RID: 1385
		public abstract DbGeometry GetGeometry(int ordinal);

		// Token: 0x0600056A RID: 1386 RVA: 0x00012548 File Offset: 0x00010748
		public virtual Task<DbGeometry> GetGeometryAsync(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelper.FromCancellation<DbGeometry>();
			}
			Task<DbGeometry> task;
			try
			{
				task = Task.FromResult<DbGeometry>(this.GetGeometry(ordinal));
			}
			catch (Exception ex)
			{
				task = TaskHelper.FromException<DbGeometry>(ex);
			}
			return task;
		}

		// Token: 0x0600056B RID: 1387
		public abstract bool IsGeographyColumn(int ordinal);

		// Token: 0x0600056C RID: 1388
		public abstract bool IsGeometryColumn(int ordinal);
	}
}
