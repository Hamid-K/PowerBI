using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dapper;
using Microsoft.BIServer.Configuration;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.CSharp.RuntimeBinder;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000025 RID: 37
	public sealed class CatalogDataAccessor : ICatalogDataAccessor
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x000060EC File Offset: 0x000042EC
		public async Task<IList<DataSourceEntity>> GetDataSourcesAsync(Guid itemId, int authType)
		{
			var <>f__AnonymousType = new
			{
				ItemId = itemId,
				AuthType = authType
			};
			return await CatalogAccessFactory.QueryAsync<DataSourceEntity>("GetDataSources", <>f__AnonymousType);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000613C File Offset: 0x0000433C
		public async Task<IList<DataSetEntity>> GetDataSetsAsync(Guid itemId, int authType)
		{
			var <>f__AnonymousType = new
			{
				ItemId = itemId,
				AuthType = authType
			};
			return await CatalogAccessFactory.QueryAsync<DataSetEntity>("GetDataSets", <>f__AnonymousType);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000618C File Offset: 0x0000438C
		public async Task<IList<CommentEntity>> GetCommentsByItemAsync(Guid catalogItemId)
		{
			var <>f__AnonymousType = new
			{
				ItemID = catalogItemId
			};
			return await CatalogAccessFactory.QueryAsync<CommentEntity>("GetCommentsByItemID", <>f__AnonymousType);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000061D4 File Offset: 0x000043D4
		public async Task<int> GetCommentsCountByItemAsync(Guid catalogItemId)
		{
			var <>f__AnonymousType = new
			{
				ItemID = catalogItemId
			};
			return await CatalogAccessFactory.QueryFirstOrDefaultAsync<int>("GetCommentsCountByItemID", <>f__AnonymousType);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000621C File Offset: 0x0000441C
		public async Task<CommentEntity> GetCommentAsync(Guid catalogItemId)
		{
			var <>f__AnonymousType = new
			{
				ItemID = catalogItemId
			};
			return await CatalogAccessFactory.QueryFirstOrDefaultAsync<CommentEntity>("GetCommentByCommentID", <>f__AnonymousType);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006264 File Offset: 0x00004464
		public async Task<CommentEntity> GetCommentAsync(long commentId)
		{
			var <>f__AnonymousType = new
			{
				CommentID = commentId
			};
			return await CatalogAccessFactory.QueryFirstOrDefaultAsync<CommentEntity>("GetCommentByCommentID", <>f__AnonymousType);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000062AC File Offset: 0x000044AC
		public async Task<int> DeleteCommentAsync(long commentId)
		{
			var <>f__AnonymousType = new
			{
				CommentID = commentId
			};
			return await CatalogAccessFactory.ExecuteAsync("DeleteComment", <>f__AnonymousType);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000062F4 File Offset: 0x000044F4
		public async Task<int> AddCommentEvent(long commentId)
		{
			var <>f__AnonymousType = new
			{
				EventType = "CommentAddedAlert",
				EventData = commentId.ToString()
			};
			return await CatalogAccessFactory.ExecuteAsync("AddEvent", <>f__AnonymousType);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000633C File Offset: 0x0000453C
		public async Task<Guid> GetUserIDWithNoCreate(byte[] sidBytes, string userName, int authType)
		{
			Guid userId = Guid.Empty;
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("UserSid", sidBytes, new DbType?(DbType.Binary), null, null, null, null);
			parameters.Add("UserName", userName, null, null, null, null, null);
			parameters.Add("AuthType", authType, null, null, null, null, null);
			parameters.Add("UserID", null, new DbType?(DbType.Guid), new ParameterDirection?(ParameterDirection.Output), null, null, null);
			await CatalogAccessFactory.QueryFirstOrDefaultAsync<Guid>("GetUserIDWithNoCreate", parameters);
			if (CatalogDataAccessor.<>o__8.<>p__1 == null)
			{
				CatalogDataAccessor.<>o__8.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(CatalogDataAccessor), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			Func<CallSite, object, bool> target = CatalogDataAccessor.<>o__8.<>p__1.Target;
			CallSite <>p__ = CatalogDataAccessor.<>o__8.<>p__1;
			if (CatalogDataAccessor.<>o__8.<>p__0 == null)
			{
				CatalogDataAccessor.<>o__8.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(CatalogDataAccessor), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			if (target(<>p__, CatalogDataAccessor.<>o__8.<>p__0.Target(CatalogDataAccessor.<>o__8.<>p__0, parameters.Get<object>("UserID"), null)))
			{
				userId = parameters.Get<Guid>("UserID");
			}
			return await Task.FromResult<Guid>(userId);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006394 File Offset: 0x00004594
		public async Task<Guid> GetUserID(byte[] sidBytes, string userName, int authType)
		{
			Guid userId = Guid.Empty;
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("UserSid", sidBytes, new DbType?(DbType.Binary), null, null, null, null);
			parameters.Add("UserName", userName, null, null, null, null, null);
			parameters.Add("AuthType", authType, null, null, null, null, null);
			parameters.Add("UserID", null, new DbType?(DbType.Guid), new ParameterDirection?(ParameterDirection.Output), null, null, null);
			await CatalogAccessFactory.QueryFirstOrDefaultAsync<Guid>("GetUserID", parameters);
			if (CatalogDataAccessor.<>o__9.<>p__1 == null)
			{
				CatalogDataAccessor.<>o__9.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(CatalogDataAccessor), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			Func<CallSite, object, bool> target = CatalogDataAccessor.<>o__9.<>p__1.Target;
			CallSite <>p__ = CatalogDataAccessor.<>o__9.<>p__1;
			if (CatalogDataAccessor.<>o__9.<>p__0 == null)
			{
				CatalogDataAccessor.<>o__9.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(CatalogDataAccessor), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			if (target(<>p__, CatalogDataAccessor.<>o__9.<>p__0.Target(CatalogDataAccessor.<>o__9.<>p__0, parameters.Get<object>("UserID"), null)))
			{
				userId = parameters.Get<Guid>("UserID");
			}
			return await Task.FromResult<Guid>(userId);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000063EC File Offset: 0x000045EC
		public async Task<long> GetAlertSubscriptionId(Guid userId, Guid itemId, string alertType)
		{
			var <>f__AnonymousType = new
			{
				UserID = userId,
				ItemId = itemId,
				AlertType = alertType
			};
			return await CatalogAccessFactory.QueryFirstOrDefaultAsync<long>("GetAlertSubscriptionID", <>f__AnonymousType);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006444 File Offset: 0x00004644
		public async Task<int> AddAlertSubscription(Guid userId, Guid itemId, string alertType)
		{
			var <>f__AnonymousType = new
			{
				UserID = userId,
				ItemId = itemId,
				AlertType = alertType
			};
			return await CatalogAccessFactory.ExecuteAsync("AddAlertSubscription", <>f__AnonymousType);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000649C File Offset: 0x0000469C
		public async Task<int> DeleteAlertSubscription(long id)
		{
			var <>f__AnonymousType = new
			{
				AlertSubscriptionID = id
			};
			return await CatalogAccessFactory.ExecuteAsync("DeleteAlertSubscription", <>f__AnonymousType);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000064E4 File Offset: 0x000046E4
		public Guid GetItemIdFromHistoryId(Guid historyId)
		{
			return CatalogAccessFactory.ExecuteScalar<Guid>("SELECT [ReportId] FROM [History] WHERE [HistoryId] = @pHistoryId", new Dictionary<string, object> { { "pHistoryId", historyId } });
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006514 File Offset: 0x00004714
		public async Task<int> AddExecutionLogInfoAsync(ExecutionLogInfoEntity payload)
		{
			var <>f__AnonymousType = new
			{
				InstanceName = Environment.MachineName + "\\" + ConfigReader.Current.InstanceId,
				Report = payload.ItemPath,
				UserName = payload.UserName,
				AuthType = 0,
				RequestType = 0,
				Format = payload.Format,
				Parameters = payload.Parameters,
				TimeStart = payload.StartTime,
				TimeEnd = payload.EndTime,
				TimeDataRetrieval = payload.DataRetrievalTime,
				TimeProcessing = payload.ProcessingTime,
				TimeRendering = payload.RenderingTime,
				Source = (int)payload.Source,
				Status = payload.Status.ToString(),
				ByteCount = payload.ByteCount,
				RowCount = payload.RowCount,
				ExecutionId = payload.ExecutionId,
				ReportAction = (int)payload.EventType,
				AdditionalInfo = payload.AdditionalInfo
			};
			return await CatalogAccessFactory.ExecuteAsync("AddExecutionLogEntry", <>f__AnonymousType);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000655C File Offset: 0x0000475C
		public async Task<int> AddExecutionLogInfoByReportIdAsync(ExecutionLogInfoEntity payload)
		{
			var <>f__AnonymousType = new
			{
				InstanceName = Environment.MachineName + "\\" + ConfigReader.Current.InstanceId,
				ReportID = payload.ItemId,
				UserName = payload.UserName,
				AuthType = 0,
				RequestType = payload.RequestType,
				Format = payload.Format,
				Parameters = payload.Parameters,
				TimeStart = payload.StartTime,
				TimeEnd = payload.EndTime,
				TimeDataRetrieval = payload.DataRetrievalTime,
				TimeProcessing = payload.ProcessingTime,
				TimeRendering = payload.RenderingTime,
				Source = (int)payload.Source,
				Status = payload.Status.ToString(),
				ByteCount = payload.ByteCount,
				RowCount = payload.RowCount,
				ExecutionId = payload.ExecutionId,
				ReportAction = (int)payload.EventType,
				AdditionalInfo = payload.AdditionalInfo
			};
			return await CatalogAccessFactory.ExecuteAsync("AddExecutionLogEntryByReportId", <>f__AnonymousType);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000065A4 File Offset: 0x000047A4
		public async Task<string> GetDefaultEmailAsync(Guid userId)
		{
			var <>f__AnonymousType = new
			{
				UserID = userId
			};
			return await CatalogAccessFactory.QueryFirstOrDefaultAsync<string>("GetDefaultEmail", <>f__AnonymousType);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000065EC File Offset: 0x000047EC
		public async Task<int> SetDefaultEmailAsync(Guid userId, string email)
		{
			var <>f__AnonymousType = new
			{
				UserId = userId,
				DefaultEmailAddress = email
			};
			return await CatalogAccessFactory.ExecuteAsync("SetDefaultEmail", <>f__AnonymousType);
		}
	}
}
