using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportProcessing.Utilities;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000589 RID: 1417
	[DataContract]
	internal sealed class DataMember : DataItem
	{
		// Token: 0x06005184 RID: 20868 RVA: 0x00159B70 File Offset: 0x00157D70
		internal DataMember(string id)
			: base(id)
		{
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x00159B79 File Offset: 0x00157D79
		internal DataMember(string id, IEnumerable<Calculation> calculations, IEnumerable<DataShape> dataShapes, Group group, IEnumerable<DataMember> dataMembers)
			: base(id, calculations, dataShapes)
		{
			this.m_group = group;
			this.m_dataMembers = dataMembers.ToReadOnlyCollection<DataMember>();
		}

		// Token: 0x17001E3F RID: 7743
		// (get) Token: 0x06005186 RID: 20870 RVA: 0x00159B99 File Offset: 0x00157D99
		internal Group Group
		{
			get
			{
				return this.m_group;
			}
		}

		// Token: 0x17001E40 RID: 7744
		// (get) Token: 0x06005187 RID: 20871 RVA: 0x00159BA1 File Offset: 0x00157DA1
		internal IEnumerable<DataMember> DataMembers
		{
			get
			{
				return this.m_dataMembers;
			}
		}

		// Token: 0x04002921 RID: 10529
		[DataMember(Name = "Group", Order = 4)]
		private readonly Group m_group;

		// Token: 0x04002922 RID: 10530
		[DataMember(Name = "DataMembers", Order = 5)]
		private readonly IEnumerable<DataMember> m_dataMembers;
	}
}
