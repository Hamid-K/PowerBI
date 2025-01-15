using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000A4 RID: 164
	internal sealed class CatalogSessionParameterStorage : IStoredParameterSource
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x0001FB7E File Offset: 0x0001DD7E
		public CatalogSessionParameterStorage(SessionReportItem sessionItem, ItemParameterDefinition catalogDefinition)
		{
			RSTrace.CatalogTrace.Assert(catalogDefinition != null, "catalogDefinition");
			this.m_sessionItem = sessionItem;
			this.m_catalogDefinition = catalogDefinition;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001FBA8 File Offset: 0x0001DDA8
		public ParameterInfoCollection RetrieveParameters(ReportProcessing reportProcessing)
		{
			if (this.m_sessionItem != null && this.m_sessionItem.Report.EffectiveParams != null)
			{
				return this.m_sessionItem.Report.EffectiveParams;
			}
			ParameterInfoCollection parameterInfoCollection;
			if (this.IsSnapshotExecution)
			{
				using (ISnapshotTransaction snapshotTransaction = this.CompiledParameterSource.EnterTransactionContext())
				{
					try
					{
						parameterInfoCollection = reportProcessing.GetSnapshotParameters(ReadOnlyChunkFactory.FromSnapshot(this.CompiledParameterSource));
					}
					catch (VersionMismatchException)
					{
						SnapshotConverter.ConvertFromV1(this.m_catalogDefinition.ItemContext, this.CompiledParameterSource, true);
						parameterInfoCollection = reportProcessing.GetSnapshotParameters(ReadOnlyChunkFactory.FromSnapshot(this.CompiledParameterSource));
					}
					snapshotTransaction.Commit();
					return parameterInfoCollection;
				}
			}
			parameterInfoCollection = ParameterInfoCollection.DecodeFromXml(this.m_catalogDefinition.StoredParametersXml);
			return parameterInfoCollection;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0001FC80 File Offset: 0x0001DE80
		public ReportSnapshot CompiledParameterSource
		{
			get
			{
				if (this.IsSnapshotExecution)
				{
					return this.m_catalogDefinition.SnapshotData;
				}
				return this.m_catalogDefinition.DefinitionSnapshot;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
		public bool IsSnapshotExecution
		{
			get
			{
				if (this.m_cachedIsSnapshotExecution != null)
				{
					return this.m_cachedIsSnapshotExecution.Value;
				}
				DateTime snapshotExecutionDate = this.m_catalogDefinition.SnapshotExecutionDate;
				ReportSnapshot snapshotData = this.m_catalogDefinition.SnapshotData;
				bool flag = ExecutionOptions.IsSnapshotExecution(this.m_catalogDefinition.ExecutionOptions);
				bool flag2 = snapshotExecutionDate != DateTime.MinValue || (flag && snapshotData != null);
				this.m_cachedIsSnapshotExecution = new bool?(flag2);
				return this.m_cachedIsSnapshotExecution.Value;
			}
		}

		// Token: 0x040003F5 RID: 1013
		private readonly SessionReportItem m_sessionItem;

		// Token: 0x040003F6 RID: 1014
		private readonly ItemParameterDefinition m_catalogDefinition;

		// Token: 0x040003F7 RID: 1015
		private bool? m_cachedIsSnapshotExecution;
	}
}
