using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200008D RID: 141
	internal abstract class StandaloneExecutionBase<TCatalogItem> where TCatalogItem : BaseExecutableCatalogItem
	{
		// Token: 0x060005D9 RID: 1497
		protected abstract void CallProcessing();

		// Token: 0x060005DA RID: 1498 RVA: 0x00017C8C File Offset: 0x00015E8C
		protected StandaloneExecutionBase(TCatalogItem item)
		{
			RSTrace.CatalogTrace.Assert(item != null, "itemContext");
			RSTrace.CatalogTrace.Assert(item.Service != null);
			this.m_catalogItem = item;
			this.m_executionDateUtc = DateTimeOffset.UtcNow;
			ProcessingContext.JobContext.ExecutionInfo.ItemPath = this.ItemContext.ItemPath;
			item.LoadProperties();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00017D08 File Offset: 0x00015F08
		internal void CreateSnapshot()
		{
			int reportTimeout = this.Service.GetReportTimeout(this.Item.Properties);
			if (this.IsHistorySnapshotGeneration)
			{
				RSTrace.CatalogTrace.Assert(this.UsePermanentSnapshot, "history created in temporary catalog.");
			}
			RunningJobContext jobContext = ProcessingContext.JobContext;
			jobContext.Description = this.Item.Properties.Description;
			jobContext.SetTimeout(reportTimeout);
			this.Item.ThrowIfNotGoodForUnattended(true);
			try
			{
				bool storeRdlChunks = this.Service.StoreRdlChunks;
				try
				{
					if (!storeRdlChunks && this.IsHistorySnapshotGeneration)
					{
						this.Service.InitializeRdlChunkMapping();
					}
					this.CallProcessing();
				}
				finally
				{
					if (!storeRdlChunks)
					{
						this.Service.ResetRdlChunkMapping();
					}
				}
			}
			catch (Exception)
			{
				if (this.TargetSnapshot != null)
				{
					this.TargetSnapshot.DeleteSnapshotAndChunks();
					this.TargetSnapshot = null;
				}
				throw;
			}
			jobContext.ExecutionInfo.ByteCount = this.TargetSnapshot.TotalCreatedChunkLength;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00017E14 File Offset: 0x00016014
		internal void AddSnapshotToCache()
		{
			DateTime dateTime;
			this.Service.Storage.AddReportToExecutionCache(this.Item.ItemID, this.TargetSnapshot, this.ExecutionDateNotTruncatedLocalTime, false, out dateTime);
			new ChunkStorage
			{
				ConnectionManager = this.Service.Storage.ConnectionManager
			}.DecreaseTransientSnapshotRefcount(this.TargetSnapshot.SnapshotDataID, this.TargetSnapshot.IsPermanentSnapshot);
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00017E86 File Offset: 0x00016086
		protected RSService Service
		{
			[DebuggerStepThrough]
			get
			{
				return this.Item.Service;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00017E98 File Offset: 0x00016098
		protected CatalogItemContext ItemContext
		{
			[DebuggerStepThrough]
			get
			{
				return this.Item.ItemContext;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00017EAA File Offset: 0x000160AA
		protected string UserName
		{
			[DebuggerStepThrough]
			get
			{
				return this.Service.UserName;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00017EB7 File Offset: 0x000160B7
		protected DateTimeOffset ExecutionTime
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_executionDateUtc;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00017EC0 File Offset: 0x000160C0
		protected DateTime ExecutionDateNotTruncatedLocalTime
		{
			get
			{
				return this.ExecutionTime.LocalDateTime;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00017EDB File Offset: 0x000160DB
		protected TCatalogItem Item
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_catalogItem;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected virtual bool IsHistorySnapshotGeneration
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00017EE3 File Offset: 0x000160E3
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x00017EEB File Offset: 0x000160EB
		internal bool UsePermanentSnapshot
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_usePermanentSnapshot;
			}
			set
			{
				this.m_usePermanentSnapshot = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00017EF4 File Offset: 0x000160F4
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00017EFC File Offset: 0x000160FC
		internal ReportSnapshot TargetSnapshot
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_targetSnapshot;
			}
			set
			{
				this.m_targetSnapshot = value;
			}
		}

		// Token: 0x0400031F RID: 799
		private readonly DateTimeOffset m_executionDateUtc;

		// Token: 0x04000320 RID: 800
		private readonly TCatalogItem m_catalogItem;

		// Token: 0x04000321 RID: 801
		private bool m_usePermanentSnapshot;

		// Token: 0x04000322 RID: 802
		private ReportSnapshot m_targetSnapshot;
	}
}
