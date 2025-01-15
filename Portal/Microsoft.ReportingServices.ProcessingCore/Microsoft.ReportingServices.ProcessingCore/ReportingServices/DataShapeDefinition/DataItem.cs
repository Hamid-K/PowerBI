using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportProcessing.Utilities;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000588 RID: 1416
	[DataContract]
	internal abstract class DataItem
	{
		// Token: 0x0600517F RID: 20863 RVA: 0x00159B28 File Offset: 0x00157D28
		internal DataItem(string id)
		{
			this.m_id = id;
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x00159B37 File Offset: 0x00157D37
		internal DataItem(string id, IEnumerable<Calculation> calculations, IEnumerable<DataShape> dataShapes)
			: this(id)
		{
			this.m_calculations = calculations.ToReadOnlyCollection<Calculation>();
			this.m_dataShapes = dataShapes.ToReadOnlyCollection<DataShape>();
		}

		// Token: 0x17001E3C RID: 7740
		// (get) Token: 0x06005181 RID: 20865 RVA: 0x00159B58 File Offset: 0x00157D58
		internal string ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001E3D RID: 7741
		// (get) Token: 0x06005182 RID: 20866 RVA: 0x00159B60 File Offset: 0x00157D60
		internal IEnumerable<Calculation> Calculations
		{
			get
			{
				return this.m_calculations;
			}
		}

		// Token: 0x17001E3E RID: 7742
		// (get) Token: 0x06005183 RID: 20867 RVA: 0x00159B68 File Offset: 0x00157D68
		internal IEnumerable<DataShape> DataShapes
		{
			get
			{
				return this.m_dataShapes;
			}
		}

		// Token: 0x0400291E RID: 10526
		[DataMember(Name = "ID", Order = 1)]
		private readonly string m_id;

		// Token: 0x0400291F RID: 10527
		[DataMember(Name = "Calculations", Order = 2)]
		private readonly IEnumerable<Calculation> m_calculations;

		// Token: 0x04002920 RID: 10528
		[DataMember(Name = "DataShapes", Order = 3)]
		private readonly IEnumerable<DataShape> m_dataShapes;
	}
}
