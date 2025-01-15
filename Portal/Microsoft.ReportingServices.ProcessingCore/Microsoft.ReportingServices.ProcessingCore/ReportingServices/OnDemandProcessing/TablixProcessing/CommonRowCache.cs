using System;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008AE RID: 2222
	internal sealed class CommonRowCache : IDisposable
	{
		// Token: 0x06007954 RID: 31060 RVA: 0x001F37EB File Offset: 0x001F19EB
		internal CommonRowCache(IScalabilityCache scaleCache)
		{
			this.m_rows = new ScalableList<DataFieldRow>(0, scaleCache, 1000, 100);
		}

		// Token: 0x06007955 RID: 31061 RVA: 0x001F3807 File Offset: 0x001F1A07
		internal int AddRow(DataFieldRow row)
		{
			int count = this.m_rows.Count;
			this.m_rows.Add(row);
			return count;
		}

		// Token: 0x06007956 RID: 31062 RVA: 0x001F3820 File Offset: 0x001F1A20
		internal DataFieldRow GetRow(int index)
		{
			return this.m_rows[index];
		}

		// Token: 0x06007957 RID: 31063 RVA: 0x001F382E File Offset: 0x001F1A2E
		internal void SetupRow(int index, OnDemandProcessingContext odpContext)
		{
			this.GetRow(index).SetFields(odpContext.ReportObjectModel.FieldsImpl);
		}

		// Token: 0x06007958 RID: 31064 RVA: 0x001F3847 File Offset: 0x001F1A47
		public void Dispose()
		{
			this.m_rows.Dispose();
			this.m_rows = null;
		}

		// Token: 0x17002829 RID: 10281
		// (get) Token: 0x06007959 RID: 31065 RVA: 0x001F385B File Offset: 0x001F1A5B
		internal int Count
		{
			get
			{
				return this.m_rows.Count;
			}
		}

		// Token: 0x1700282A RID: 10282
		// (get) Token: 0x0600795A RID: 31066 RVA: 0x001F3868 File Offset: 0x001F1A68
		internal int LastRowIndex
		{
			get
			{
				return this.Count - 1;
			}
		}

		// Token: 0x04003CE2 RID: 15586
		private ScalableList<DataFieldRow> m_rows;

		// Token: 0x04003CE3 RID: 15587
		internal const int UnInitializedRowIndex = -1;
	}
}
