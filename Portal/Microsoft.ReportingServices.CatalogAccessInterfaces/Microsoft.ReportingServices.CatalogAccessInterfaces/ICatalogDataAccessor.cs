using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x0200000B RID: 11
	public interface ICatalogDataAccessor
	{
		// Token: 0x060000B2 RID: 178
		Task<IList<DataSourceEntity>> GetDataSourcesAsync(Guid itemId, int authType);

		// Token: 0x060000B3 RID: 179
		Task<IList<DataSetEntity>> GetDataSetsAsync(Guid itemId, int authType);

		// Token: 0x060000B4 RID: 180
		Task<IList<CommentEntity>> GetCommentsByItemAsync(Guid catalogItemId);

		// Token: 0x060000B5 RID: 181
		Task<int> GetCommentsCountByItemAsync(Guid catalogItemId);

		// Token: 0x060000B6 RID: 182
		Task<CommentEntity> GetCommentAsync(long commentId);

		// Token: 0x060000B7 RID: 183
		Task<int> DeleteCommentAsync(long commentId);

		// Token: 0x060000B8 RID: 184
		Task<int> AddCommentEvent(long commentId);

		// Token: 0x060000B9 RID: 185
		Task<Guid> GetUserIDWithNoCreate(byte[] sidBytes, string userName, int authType);

		// Token: 0x060000BA RID: 186
		Task<Guid> GetUserID(byte[] sidBytes, string userName, int authType);

		// Token: 0x060000BB RID: 187
		Task<string> GetDefaultEmailAsync(Guid userId);

		// Token: 0x060000BC RID: 188
		Task<int> SetDefaultEmailAsync(Guid userId, string email);

		// Token: 0x060000BD RID: 189
		Task<long> GetAlertSubscriptionId(Guid userId, Guid itemId, string alertType);

		// Token: 0x060000BE RID: 190
		Task<int> AddAlertSubscription(Guid userId, Guid itemId, string alertType);

		// Token: 0x060000BF RID: 191
		Task<int> DeleteAlertSubscription(long id);

		// Token: 0x060000C0 RID: 192
		Guid GetItemIdFromHistoryId(Guid historyId);

		// Token: 0x060000C1 RID: 193
		Task<int> AddExecutionLogInfoAsync(ExecutionLogInfoEntity payLoad);

		// Token: 0x060000C2 RID: 194
		Task<int> AddExecutionLogInfoByReportIdAsync(ExecutionLogInfoEntity payload);
	}
}
