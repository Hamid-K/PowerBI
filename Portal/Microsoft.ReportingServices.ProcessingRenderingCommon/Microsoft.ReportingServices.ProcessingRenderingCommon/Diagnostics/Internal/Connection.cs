using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000BE RID: 190
	public sealed class Connection
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x0001262C File Offset: 0x0001082C
		internal Connection()
		{
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00012634 File Offset: 0x00010834
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0001263C File Offset: 0x0001083C
		public long? ConnectionOpenTime { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00012648 File Offset: 0x00010848
		[XmlIgnore]
		public bool ConnectionOpenTimeSpecified
		{
			get
			{
				return this.ConnectionOpenTime != null;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00012663 File Offset: 0x00010863
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0001266B File Offset: 0x0001086B
		public bool? ConnectionFromPool { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00012674 File Offset: 0x00010874
		[XmlIgnore]
		public bool ConnectionFromPoolSpecified
		{
			get
			{
				return this.ConnectionFromPool != null;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001268F File Offset: 0x0001088F
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x00012697 File Offset: 0x00010897
		public ModelMetadata ModelMetadata { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x000126A0 File Offset: 0x000108A0
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x000126A8 File Offset: 0x000108A8
		public DataSource DataSource { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x000126B1 File Offset: 0x000108B1
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x000126B9 File Offset: 0x000108B9
		public List<DataSet> DataSets { get; set; }
	}
}
