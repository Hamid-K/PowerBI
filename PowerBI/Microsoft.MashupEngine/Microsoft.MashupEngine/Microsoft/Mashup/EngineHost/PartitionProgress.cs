using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001971 RID: 6513
	public class PartitionProgress
	{
		// Token: 0x0600A547 RID: 42311 RVA: 0x0022342C File Offset: 0x0022162C
		private PartitionProgress(IPartitionKey partitionKey, long? rowCount, long? errorRowCount, bool sampled, Dictionary<DataSourceKey, DataSourceProgress> dataSources)
		{
			this.partitionKey = partitionKey;
			this.rowCount = rowCount;
			this.errorRowCount = errorRowCount;
			this.sampled = sampled;
			this.dataSources = dataSources;
		}

		// Token: 0x0600A548 RID: 42312 RVA: 0x00223459 File Offset: 0x00221659
		public PartitionProgress(IPartitionKey partitionKey)
		{
			this.partitionKey = partitionKey;
			this.dataSources = new Dictionary<DataSourceKey, DataSourceProgress>();
		}

		// Token: 0x17002A36 RID: 10806
		// (get) Token: 0x0600A549 RID: 42313 RVA: 0x00223473 File Offset: 0x00221673
		public IPartitionKey PartitionKey
		{
			get
			{
				return this.partitionKey;
			}
		}

		// Token: 0x17002A37 RID: 10807
		// (get) Token: 0x0600A54A RID: 42314 RVA: 0x0022347B File Offset: 0x0022167B
		public bool Sampled
		{
			get
			{
				return this.sampled;
			}
		}

		// Token: 0x17002A38 RID: 10808
		// (get) Token: 0x0600A54B RID: 42315 RVA: 0x00223483 File Offset: 0x00221683
		// (set) Token: 0x0600A54C RID: 42316 RVA: 0x0022348B File Offset: 0x0022168B
		public long? RowCount
		{
			get
			{
				return this.rowCount;
			}
			set
			{
				this.rowCount = value;
			}
		}

		// Token: 0x17002A39 RID: 10809
		// (get) Token: 0x0600A54D RID: 42317 RVA: 0x00223494 File Offset: 0x00221694
		// (set) Token: 0x0600A54E RID: 42318 RVA: 0x0022349C File Offset: 0x0022169C
		public long? ErrorRowCount
		{
			get
			{
				return this.errorRowCount;
			}
			set
			{
				this.errorRowCount = value;
			}
		}

		// Token: 0x17002A3A RID: 10810
		// (get) Token: 0x0600A54F RID: 42319 RVA: 0x002234A8 File Offset: 0x002216A8
		public DataSourceProgress[] DataSources
		{
			get
			{
				DataSourceProgress[] array = new DataSourceProgress[this.dataSources.Count];
				this.dataSources.Values.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0600A550 RID: 42320 RVA: 0x002234D9 File Offset: 0x002216D9
		public void RecordSampling()
		{
			this.sampled = true;
		}

		// Token: 0x0600A551 RID: 42321 RVA: 0x002234E2 File Offset: 0x002216E2
		public DataSourceProgress GetMostRecentProgress()
		{
			return this.DataSources.OrderByDescending((DataSourceProgress ds) => ds.LastProgressAt).FirstOrDefault<DataSourceProgress>();
		}

		// Token: 0x0600A552 RID: 42322 RVA: 0x00223514 File Offset: 0x00221714
		public DataSourceProgress GetDataSource(string dataSourceType, string dataSource)
		{
			DataSourceKey dataSourceKey = new DataSourceKey(dataSourceType, dataSource);
			DataSourceProgress dataSourceProgress;
			if (!this.dataSources.TryGetValue(dataSourceKey, out dataSourceProgress))
			{
				dataSourceProgress = new DataSourceProgress(dataSourceType, dataSource);
				this.dataSources.Add(dataSourceKey, dataSourceProgress);
			}
			return dataSourceProgress;
		}

		// Token: 0x0600A553 RID: 42323 RVA: 0x00223550 File Offset: 0x00221750
		public static PartitionProgress FromBytes(byte[] bytes)
		{
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(bytes));
			PartitioningScheme partitioningScheme = (PartitioningScheme)binaryReader.ReadInt32();
			IPartitionKey partitionKey = binaryReader.ReadString().ToPartitionKey(partitioningScheme);
			long? num = null;
			long? num2 = null;
			if (binaryReader.ReadBoolean())
			{
				num = new long?(binaryReader.ReadInt64());
				num2 = new long?(binaryReader.ReadInt64());
			}
			bool flag = binaryReader.ReadBoolean();
			int num3 = binaryReader.ReadInt32();
			Dictionary<DataSourceKey, DataSourceProgress> dictionary = new Dictionary<DataSourceKey, DataSourceProgress>();
			for (int i = 0; i < num3; i++)
			{
				string text = binaryReader.ReadString();
				string text2 = binaryReader.ReadString();
				long num4 = binaryReader.ReadInt64();
				long num5 = binaryReader.ReadInt64();
				long num6 = binaryReader.ReadInt64();
				long num7 = binaryReader.ReadInt64();
				int num8 = binaryReader.ReadInt32();
				long num9 = binaryReader.ReadInt64();
				int num10 = binaryReader.ReadInt32();
				DataSourceProgress dataSourceProgress = new DataSourceProgress(text, text2)
				{
					BytesRead = num4,
					BytesWritten = num5,
					RowsRead = num6,
					RowsWritten = num7,
					RequestCount = num8,
					LastProgressAt = num9,
					PercentComplete = num10
				};
				dictionary.Add(new DataSourceKey(text, text2), dataSourceProgress);
			}
			return new PartitionProgress(partitionKey, num, num2, flag, dictionary);
		}

		// Token: 0x0600A554 RID: 42324 RVA: 0x00223688 File Offset: 0x00221888
		public byte[] ToBytes()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write((int)this.partitionKey.PartitioningScheme);
			binaryWriter.Write(this.partitionKey.ToSerializedString());
			binaryWriter.Write(this.rowCount != null);
			if (this.rowCount != null)
			{
				binaryWriter.Write(this.rowCount.Value);
				binaryWriter.Write(this.errorRowCount.GetValueOrDefault());
			}
			binaryWriter.Write(this.sampled);
			binaryWriter.Write(Math.Min(this.dataSources.Count, 1));
			if (this.dataSources.Count > 0)
			{
				DataSourceProgress mostRecentProgress = this.GetMostRecentProgress();
				binaryWriter.Write(mostRecentProgress.DataSourceType);
				binaryWriter.Write(mostRecentProgress.DataSource);
				binaryWriter.Write(mostRecentProgress.BytesRead);
				binaryWriter.Write(mostRecentProgress.BytesWritten);
				binaryWriter.Write(mostRecentProgress.RowsRead);
				binaryWriter.Write(mostRecentProgress.RowsWritten);
				binaryWriter.Write(mostRecentProgress.RequestCount);
				binaryWriter.Write(mostRecentProgress.LastProgressAt);
				binaryWriter.Write(mostRecentProgress.PercentComplete);
			}
			binaryWriter.Flush();
			return memoryStream.ToArray();
		}

		// Token: 0x0400560F RID: 22031
		private IPartitionKey partitionKey;

		// Token: 0x04005610 RID: 22032
		private long? rowCount;

		// Token: 0x04005611 RID: 22033
		private long? errorRowCount;

		// Token: 0x04005612 RID: 22034
		private bool sampled;

		// Token: 0x04005613 RID: 22035
		private Dictionary<DataSourceKey, DataSourceProgress> dataSources;
	}
}
