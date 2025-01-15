using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000035 RID: 53
	[DataContract]
	public class MdxTraceInfo
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000B6BC File Offset: 0x000098BC
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000B6C4 File Offset: 0x000098C4
		[DataMember]
		public string MdxStatement { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000B6CD File Offset: 0x000098CD
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000B6D5 File Offset: 0x000098D5
		[DataMember]
		public List<MdxColumn> Columns { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000B6DE File Offset: 0x000098DE
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000B6E6 File Offset: 0x000098E6
		[DataMember]
		public object[][] ColumnInfos { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000B6EF File Offset: 0x000098EF
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000B6F7 File Offset: 0x000098F7
		[DataMember]
		public string CubeName { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000B700 File Offset: 0x00009900
		public bool HasMeasures
		{
			get
			{
				if (this.ColumnInfos != null)
				{
					return (from info in this.ColumnInfos
						where info != null && info.Length == 7
						select (MdxCubeObjectKind)((int)info[0])).Any((MdxCubeObjectKind objectKind) => objectKind == MdxCubeObjectKind.Measure);
				}
				return false;
			}
		}
	}
}
