using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A3E RID: 6718
	public sealed class PartitionProgressService : IPartitionProgressService, IGetDataSourceProgress, IDisposable
	{
		// Token: 0x0600A9E2 RID: 43490 RVA: 0x0023174C File Offset: 0x0022F94C
		public PartitionProgressService(IRecordProgress progressRecorder)
		{
			this.syncRoot = new object();
			this.progressRecorder = progressRecorder;
			this.partitionProgress = new Dictionary<IPartitionKey, PartitionProgressService.ProgressService>(PartitionKeyEqualityComparer.Instance);
			this.timer = new Timer(SafeThread2.CreateTimerCallback(new TimerCallback(this.OnUpdate)), null, TimeSpan.Zero, PartitionProgressService.UpdatePeriod);
		}

		// Token: 0x0600A9E3 RID: 43491 RVA: 0x002317A8 File Offset: 0x0022F9A8
		public IProgressService2 GetPartitionProgressService(IPartitionKey partitionKey)
		{
			object obj = this.syncRoot;
			IProgressService2 progressService2;
			lock (obj)
			{
				PartitionProgressService.ProgressService progressService;
				if (!this.partitionProgress.TryGetValue(partitionKey, out progressService))
				{
					progressService = new PartitionProgressService.ProgressService(this, partitionKey, new PartitionProgress(partitionKey));
					this.partitionProgress.Add(partitionKey, progressService);
				}
				progressService2 = progressService;
			}
			return progressService2;
		}

		// Token: 0x0600A9E4 RID: 43492 RVA: 0x00231810 File Offset: 0x0022FA10
		public IEnumerable<DataSourceProgress2> GetDataSourceProgress()
		{
			object obj = this.syncRoot;
			IEnumerable<DataSourceProgress2> enumerable;
			lock (obj)
			{
				Dictionary<DataSourceKey, DataSourceProgress2> dictionary = new Dictionary<DataSourceKey, DataSourceProgress2>();
				foreach (PartitionProgressService.ProgressService progressService in this.partitionProgress.Values)
				{
					foreach (DataSourceProgress dataSourceProgress in progressService.PartitionProgress.DataSources)
					{
						DataSourceKey dataSourceKey = new DataSourceKey(dataSourceProgress.DataSourceType, dataSourceProgress.DataSource);
						DataSourceProgress2 dataSourceProgress2;
						if (!dictionary.TryGetValue(dataSourceKey, out dataSourceProgress2))
						{
							dataSourceProgress2 = new DataSourceProgress2(dataSourceProgress.DataSourceType, dataSourceProgress.DataSource);
							dictionary.Add(dataSourceKey, dataSourceProgress2);
						}
						dataSourceProgress2.LastProgressAt = Math.Max(dataSourceProgress2.LastProgressAt, dataSourceProgress.LastProgressAt);
						dataSourceProgress2.RequestCount += dataSourceProgress.RequestCount;
						dataSourceProgress2.RowsRead += dataSourceProgress.RowsRead;
						dataSourceProgress2.RowsWritten += dataSourceProgress.RowsWritten;
						dataSourceProgress2.BytesRead += dataSourceProgress.BytesRead;
						dataSourceProgress2.BytesWritten += dataSourceProgress.BytesWritten;
						dataSourceProgress2.PercentComplete = Math.Max(dataSourceProgress2.PercentComplete, dataSourceProgress.PercentComplete);
					}
				}
				enumerable = dictionary.Values.ToArray<DataSourceProgress2>();
			}
			return enumerable;
		}

		// Token: 0x0600A9E5 RID: 43493 RVA: 0x002319CC File Offset: 0x0022FBCC
		public void Dispose()
		{
			using (DisposalScope disposalScope = new DisposalScope())
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.OnUpdate(null);
					disposalScope.ClearAndDispose<Timer>(ref this.timer);
				}
			}
		}

		// Token: 0x0600A9E6 RID: 43494 RVA: 0x00231A38 File Offset: 0x0022FC38
		private long GetTimestamp(PartitionProgressService.ProgressService progressService)
		{
			object obj = this.syncRoot;
			long num;
			lock (obj)
			{
				this.lastProgressService = progressService;
				num = this.lastTimestamp;
				this.lastTimestamp = num + 1L;
				num = num;
			}
			return num;
		}

		// Token: 0x0600A9E7 RID: 43495 RVA: 0x00231A90 File Offset: 0x0022FC90
		private void OnUpdate(object state)
		{
			object obj = this.syncRoot;
			PartitionProgressService.ProgressService progressService;
			lock (obj)
			{
				progressService = this.lastProgressService;
				this.lastProgressService = null;
			}
			if (progressService != null)
			{
				progressService.RecordProgress(this.progressRecorder);
			}
		}

		// Token: 0x0400584C RID: 22604
		public static readonly TimeSpan UpdatePeriod = TimeSpan.FromSeconds(0.5);

		// Token: 0x0400584D RID: 22605
		private readonly object syncRoot;

		// Token: 0x0400584E RID: 22606
		private readonly IRecordProgress progressRecorder;

		// Token: 0x0400584F RID: 22607
		private readonly Dictionary<IPartitionKey, PartitionProgressService.ProgressService> partitionProgress;

		// Token: 0x04005850 RID: 22608
		private Timer timer;

		// Token: 0x04005851 RID: 22609
		private long lastTimestamp;

		// Token: 0x04005852 RID: 22610
		private PartitionProgressService.ProgressService lastProgressService;

		// Token: 0x02001A3F RID: 6719
		private sealed class ProgressService : IProgressService2, IProgressService, IProgressService2Config
		{
			// Token: 0x0600A9E9 RID: 43497 RVA: 0x00231AFD File Offset: 0x0022FCFD
			public ProgressService(PartitionProgressService partitionProgressService, IPartitionKey partitionKey, PartitionProgress partitionProgress)
			{
				this.partitionProgressService = partitionProgressService;
				this.partitionKey = partitionKey;
				this.partitionProgress = partitionProgress;
				this.lastProgressAt = -1L;
			}

			// Token: 0x17002B27 RID: 11047
			// (get) Token: 0x0600A9EA RID: 43498 RVA: 0x00231B22 File Offset: 0x0022FD22
			public IPartitionKey PartitionKey
			{
				get
				{
					return this.partitionKey;
				}
			}

			// Token: 0x17002B28 RID: 11048
			// (get) Token: 0x0600A9EB RID: 43499 RVA: 0x00231B2A File Offset: 0x0022FD2A
			public PartitionProgress PartitionProgress
			{
				get
				{
					return this.partitionProgress;
				}
			}

			// Token: 0x0600A9EC RID: 43500 RVA: 0x00231B34 File Offset: 0x0022FD34
			public IHostProgress GetHostProgress(string dataSourceType, string dataSource)
			{
				object syncRoot = this.partitionProgressService.syncRoot;
				IHostProgress hostProgress;
				lock (syncRoot)
				{
					DataSourceProgress dataSource2 = this.partitionProgress.GetDataSource(dataSourceType, dataSource);
					hostProgress = new PartitionProgressService.ProgressService.HostProgress(this, dataSource2);
				}
				return hostProgress;
			}

			// Token: 0x0600A9ED RID: 43501 RVA: 0x00231B8C File Offset: 0x0022FD8C
			public void RecordRowCount(long rowCount, long errorRowCount)
			{
				object syncRoot = this.partitionProgressService.syncRoot;
				lock (syncRoot)
				{
					this.partitionProgress.RowCount = new long?(rowCount);
					this.partitionProgress.ErrorRowCount = new long?(errorRowCount);
					this.GetTimestampAndNoteProgress();
				}
			}

			// Token: 0x0600A9EE RID: 43502 RVA: 0x00231BF4 File Offset: 0x0022FDF4
			public void RecordProgress(IRecordProgress progressRecorder)
			{
				object syncRoot = this.partitionProgressService.syncRoot;
				lock (syncRoot)
				{
					progressRecorder.RecordProgress(this.partitionProgress.ToBytes());
				}
			}

			// Token: 0x0600A9EF RID: 43503 RVA: 0x00231C44 File Offset: 0x0022FE44
			private long GetTimestampAndNoteProgress()
			{
				this.lastProgressAt = this.partitionProgressService.GetTimestamp(this);
				return this.lastProgressAt;
			}

			// Token: 0x04005853 RID: 22611
			private readonly PartitionProgressService partitionProgressService;

			// Token: 0x04005854 RID: 22612
			private readonly IPartitionKey partitionKey;

			// Token: 0x04005855 RID: 22613
			private readonly PartitionProgress partitionProgress;

			// Token: 0x04005856 RID: 22614
			private long lastProgressAt;

			// Token: 0x02001A40 RID: 6720
			private class HostProgress : IHostProgress
			{
				// Token: 0x0600A9F0 RID: 43504 RVA: 0x00231C5E File Offset: 0x0022FE5E
				public HostProgress(PartitionProgressService.ProgressService progressService, DataSourceProgress dataSourceProgress)
				{
					this.progressService = progressService;
					this.dataSourceProgress = dataSourceProgress;
				}

				// Token: 0x0600A9F1 RID: 43505 RVA: 0x00231C74 File Offset: 0x0022FE74
				public void StartRequest()
				{
					object syncRoot = this.progressService.partitionProgressService.syncRoot;
					lock (syncRoot)
					{
						DataSourceProgress dataSourceProgress = this.dataSourceProgress;
						int requestCount = dataSourceProgress.RequestCount;
						dataSourceProgress.RequestCount = requestCount + 1;
						this.RecordAccess();
					}
				}

				// Token: 0x0600A9F2 RID: 43506 RVA: 0x00231CD4 File Offset: 0x0022FED4
				public void StopRequest()
				{
					object syncRoot = this.progressService.partitionProgressService.syncRoot;
					lock (syncRoot)
					{
						DataSourceProgress dataSourceProgress = this.dataSourceProgress;
						int requestCount = dataSourceProgress.RequestCount;
						dataSourceProgress.RequestCount = requestCount - 1;
						this.RecordAccess();
					}
				}

				// Token: 0x0600A9F3 RID: 43507 RVA: 0x00231D34 File Offset: 0x0022FF34
				public void RecordBytesRead(long bytesRead)
				{
					object syncRoot = this.progressService.partitionProgressService.syncRoot;
					lock (syncRoot)
					{
						this.dataSourceProgress.BytesRead += bytesRead;
						this.RecordAccess();
					}
				}

				// Token: 0x0600A9F4 RID: 43508 RVA: 0x00231D94 File Offset: 0x0022FF94
				public void RecordBytesWritten(long bytesWritten)
				{
					object syncRoot = this.progressService.partitionProgressService.syncRoot;
					lock (syncRoot)
					{
						this.dataSourceProgress.BytesWritten += bytesWritten;
						this.RecordAccess();
					}
				}

				// Token: 0x0600A9F5 RID: 43509 RVA: 0x00231DF4 File Offset: 0x0022FFF4
				public void RecordRowRead()
				{
					this.RecordRowsRead(1L);
				}

				// Token: 0x0600A9F6 RID: 43510 RVA: 0x00231E00 File Offset: 0x00230000
				public void RecordRowsRead(long rowsRead)
				{
					object syncRoot = this.progressService.partitionProgressService.syncRoot;
					lock (syncRoot)
					{
						this.dataSourceProgress.RowsRead += rowsRead;
						this.RecordAccess();
					}
				}

				// Token: 0x0600A9F7 RID: 43511 RVA: 0x00231E60 File Offset: 0x00230060
				public void RecordRowWritten()
				{
					this.RecordRowsWritten(1L);
				}

				// Token: 0x0600A9F8 RID: 43512 RVA: 0x00231E6C File Offset: 0x0023006C
				public void RecordRowsWritten(long rowsWritten)
				{
					object syncRoot = this.progressService.partitionProgressService.syncRoot;
					lock (syncRoot)
					{
						this.dataSourceProgress.RowsWritten += rowsWritten;
						this.RecordAccess();
					}
				}

				// Token: 0x0600A9F9 RID: 43513 RVA: 0x00231ECC File Offset: 0x002300CC
				public void RecordPercentComplete(int percentComplete)
				{
					object syncRoot = this.progressService.partitionProgressService.syncRoot;
					lock (syncRoot)
					{
						this.dataSourceProgress.PercentComplete = percentComplete;
						this.RecordAccess();
					}
				}

				// Token: 0x0600A9FA RID: 43514 RVA: 0x00231F24 File Offset: 0x00230124
				private void RecordAccess()
				{
					this.dataSourceProgress.LastProgressAt = this.progressService.GetTimestampAndNoteProgress();
				}

				// Token: 0x04005857 RID: 22615
				private readonly PartitionProgressService.ProgressService progressService;

				// Token: 0x04005858 RID: 22616
				private readonly DataSourceProgress dataSourceProgress;
			}
		}
	}
}
