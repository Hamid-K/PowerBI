using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportProcessing.Utilities;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200058A RID: 1418
	[DataContract]
	internal sealed class DataRow : IEnumerable<DataIntersection>, IEnumerable
	{
		// Token: 0x06005188 RID: 20872 RVA: 0x00159BA9 File Offset: 0x00157DA9
		internal DataRow()
			: this(null)
		{
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x00159BB2 File Offset: 0x00157DB2
		internal DataRow(IEnumerable<DataIntersection> dataIntersections)
		{
			this.m_dataIntersections = dataIntersections.ToReadOnlyCollection<DataIntersection>();
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x00159BC6 File Offset: 0x00157DC6
		public IEnumerator<DataIntersection> GetEnumerator()
		{
			return this.m_dataIntersections.GetEnumerator();
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x00159BD3 File Offset: 0x00157DD3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dataIntersections.GetEnumerator();
		}

		// Token: 0x04002923 RID: 10531
		[DataMember(Name = "Intersections", Order = 1)]
		private readonly IEnumerable<DataIntersection> m_dataIntersections;
	}
}
