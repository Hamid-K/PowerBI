using System;
using System.Security.Principal;
using Microsoft.ReportingServices.CatalogAccess;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x0200002C RID: 44
	internal class PbiExecutionLogScope : IDisposable
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004591 File Offset: 0x00002791
		public PbiExecutionLogScope(IPrincipal userPrincipal, Guid catalogItemId, ICatalogDataAccessor catalogDataAccessor)
		{
			this._userPrincipal = userPrincipal;
			this._startTime = DateTime.Now;
			this._catalogItemId = catalogItemId;
			this._catalogDataAccessor = catalogDataAccessor;
			this.SourceType = ExecutionLogInfoEntity.ExecutionLogExecType.AdHoc;
			this.ErrorCode = ExecutionLogInfoEntity.ErrorCode.rsSuccess;
			this.DontLogOnDispose = false;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000045CE File Offset: 0x000027CE
		// (set) Token: 0x060000CA RID: 202 RVA: 0x000045D6 File Offset: 0x000027D6
		public Guid ItemID { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000045DF File Offset: 0x000027DF
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000045E7 File Offset: 0x000027E7
		public ExecutionLogInfoEntity.ReportEventType Operation { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000045F0 File Offset: 0x000027F0
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000045F8 File Offset: 0x000027F8
		public string SessionID { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004601 File Offset: 0x00002801
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004609 File Offset: 0x00002809
		public ExecutionLogInfoEntity.ErrorCode ErrorCode { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004612 File Offset: 0x00002812
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x0000461A File Offset: 0x0000281A
		public ExecutionLogInfoEntity.ExecutionLogExecType SourceType { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004623 File Offset: 0x00002823
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000462B File Offset: 0x0000282B
		public long Size { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004634 File Offset: 0x00002834
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000463C File Offset: 0x0000283C
		public bool IsDataRetrieval { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004645 File Offset: 0x00002845
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000464D File Offset: 0x0000284D
		public bool DontLogOnDispose { get; set; }

		// Token: 0x060000D9 RID: 217 RVA: 0x00004658 File Offset: 0x00002858
		public void Dispose()
		{
			if (this.DontLogOnDispose)
			{
				return;
			}
			this._endTime = DateTime.Now;
			ExecutionLogInfoEntity executionLogInfoEntity = new ExecutionLogInfoEntity();
			executionLogInfoEntity.UserName = this._userPrincipal.Identity.Name;
			executionLogInfoEntity.ItemId = this._catalogItemId;
			executionLogInfoEntity.Format = "PBIX";
			executionLogInfoEntity.StartTime = this._startTime;
			executionLogInfoEntity.EndTime = this._endTime;
			executionLogInfoEntity.Source = this.SourceType;
			executionLogInfoEntity.Status = this.ErrorCode;
			executionLogInfoEntity.ByteCount = this.Size;
			executionLogInfoEntity.ExecutionId = this.SessionID;
			executionLogInfoEntity.EventType = this.Operation;
			executionLogInfoEntity.RequestType = ExecutionLogInfoEntity.RequestTypeOf.Interactive;
			if (this.IsDataRetrieval)
			{
				executionLogInfoEntity.DataRetrievalTime = this._endTime.Subtract(this._startTime).Milliseconds;
			}
			this._catalogDataAccessor.AddExecutionLogInfoByReportIdAsync(executionLogInfoEntity);
		}

		// Token: 0x04000080 RID: 128
		private readonly ICatalogDataAccessor _catalogDataAccessor;

		// Token: 0x04000081 RID: 129
		private readonly IPrincipal _userPrincipal;

		// Token: 0x04000082 RID: 130
		private readonly DateTime _startTime;

		// Token: 0x04000083 RID: 131
		private readonly Guid _catalogItemId;

		// Token: 0x04000084 RID: 132
		private DateTime _endTime;
	}
}
