using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportProcessing.Utilities;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000586 RID: 1414
	[DataContract]
	internal sealed class DataBinding
	{
		// Token: 0x06005178 RID: 20856 RVA: 0x00159AD0 File Offset: 0x00157CD0
		internal DataBinding(string dataSetId)
		{
			this.m_dataSetId = dataSetId;
		}

		// Token: 0x06005179 RID: 20857 RVA: 0x00159ADF File Offset: 0x00157CDF
		internal DataBinding(string dataSetName, IEnumerable<Relationship> relationships)
			: this(dataSetName)
		{
			this.m_relationships = relationships.ToReadOnlyCollection<Relationship>();
		}

		// Token: 0x17001E39 RID: 7737
		// (get) Token: 0x0600517A RID: 20858 RVA: 0x00159AF4 File Offset: 0x00157CF4
		internal string DataSetId
		{
			get
			{
				return this.m_dataSetId;
			}
		}

		// Token: 0x17001E3A RID: 7738
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x00159AFC File Offset: 0x00157CFC
		internal IEnumerable<Relationship> Relationships
		{
			get
			{
				return this.m_relationships;
			}
		}

		// Token: 0x0400291B RID: 10523
		[DataMember(Name = "DataSetId", Order = 1)]
		private readonly string m_dataSetId;

		// Token: 0x0400291C RID: 10524
		[DataMember(Name = "Relationships", Order = 2)]
		private readonly IEnumerable<Relationship> m_relationships;
	}
}
