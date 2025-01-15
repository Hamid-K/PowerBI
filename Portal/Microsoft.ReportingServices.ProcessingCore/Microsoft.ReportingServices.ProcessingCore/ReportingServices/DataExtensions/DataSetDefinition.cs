using System;
using System.IO;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005AC RID: 1452
	[Serializable]
	public sealed class DataSetDefinition
	{
		// Token: 0x0600523A RID: 21050 RVA: 0x0015AB78 File Offset: 0x00158D78
		public DataSetDefinition(DataSetCore dataSetCore, string description, DataSourceInfo dataSourceInfo, ParameterInfoCollection dataSetParameters)
		{
			this.m_dataSetCore = dataSetCore;
			this.m_description = description;
			this.m_dataSetParameters = dataSetParameters;
			if (dataSourceInfo != null && dataSourceInfo.IsReference)
			{
				this.m_sharedDataSourceReferenceId = dataSourceInfo.ID;
			}
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x0015ABB8 File Offset: 0x00158DB8
		public DataSetDefinition(IChunkFactory getCompiledDefinition)
		{
			Global.Tracer.Assert(getCompiledDefinition != null, "Shared dataset definition chunk factory does not exist");
			string text;
			Stream chunk = getCompiledDefinition.GetChunk("CompiledDefinition", ReportProcessing.ReportChunkTypes.CompiledDefinition, ChunkMode.Open, out text);
			Global.Tracer.Assert(chunk != null, "Shared dataset definition stream does not exist");
			try
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader intermediateFormatReader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader(chunk, new ProcessingRIFObjectCreator(null, null), null);
				this.m_dataSetCore = (DataSetCore)intermediateFormatReader.ReadRIFObject();
			}
			finally
			{
				if (chunk != null)
				{
					chunk.Close();
				}
			}
		}

		// Token: 0x17001E9E RID: 7838
		// (get) Token: 0x0600523C RID: 21052 RVA: 0x0015AC4C File Offset: 0x00158E4C
		// (set) Token: 0x0600523D RID: 21053 RVA: 0x0015AC54 File Offset: 0x00158E54
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x17001E9F RID: 7839
		// (get) Token: 0x0600523E RID: 21054 RVA: 0x0015AC5D File Offset: 0x00158E5D
		// (set) Token: 0x0600523F RID: 21055 RVA: 0x0015AC65 File Offset: 0x00158E65
		public Guid SharedDataSourceReferenceId
		{
			get
			{
				return this.m_sharedDataSourceReferenceId;
			}
			set
			{
				this.m_sharedDataSourceReferenceId = value;
			}
		}

		// Token: 0x17001EA0 RID: 7840
		// (get) Token: 0x06005240 RID: 21056 RVA: 0x0015AC6E File Offset: 0x00158E6E
		public ParameterInfoCollection DataSetParameters
		{
			get
			{
				return this.m_dataSetParameters;
			}
		}

		// Token: 0x17001EA1 RID: 7841
		// (get) Token: 0x06005241 RID: 21057 RVA: 0x0015AC76 File Offset: 0x00158E76
		public DataSetCore DataSetCore
		{
			get
			{
				return this.m_dataSetCore;
			}
		}

		// Token: 0x0400298B RID: 10635
		private string m_description;

		// Token: 0x0400298C RID: 10636
		private Guid m_sharedDataSourceReferenceId = Guid.Empty;

		// Token: 0x0400298D RID: 10637
		private ParameterInfoCollection m_dataSetParameters;

		// Token: 0x0400298E RID: 10638
		private DataSetCore m_dataSetCore;
	}
}
