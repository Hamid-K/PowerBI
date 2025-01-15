using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000587 RID: 1415
	[DataContract]
	internal sealed class DataIntersection : DataItem
	{
		// Token: 0x0600517C RID: 20860 RVA: 0x00159B04 File Offset: 0x00157D04
		internal DataIntersection(string id)
			: base(id)
		{
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x00159B0D File Offset: 0x00157D0D
		internal DataIntersection(string id, IEnumerable<Calculation> calculations, IEnumerable<DataShape> dataShapes, DataBinding dataBinding)
			: base(id, calculations, dataShapes)
		{
			this.m_dataBinding = dataBinding;
		}

		// Token: 0x17001E3B RID: 7739
		// (get) Token: 0x0600517E RID: 20862 RVA: 0x00159B20 File Offset: 0x00157D20
		internal DataBinding DataBinding
		{
			get
			{
				return this.m_dataBinding;
			}
		}

		// Token: 0x0400291D RID: 10525
		[DataMember(Name = "DataBinding", Order = 4)]
		private readonly DataBinding m_dataBinding;
	}
}
