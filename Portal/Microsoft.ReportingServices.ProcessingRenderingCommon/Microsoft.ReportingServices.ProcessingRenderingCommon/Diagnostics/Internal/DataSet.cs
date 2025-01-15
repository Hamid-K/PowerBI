using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.ProcessingRenderingCommon;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000BF RID: 191
	public sealed class DataSet
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x000126C2 File Offset: 0x000108C2
		internal DataSet()
		{
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x000126CA File Offset: 0x000108CA
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x000126D2 File Offset: 0x000108D2
		[PotentialPiiMaskWhenLogging]
		public string Name { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x000126DB File Offset: 0x000108DB
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x000126E3 File Offset: 0x000108E3
		public string CommandText { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x000126EC File Offset: 0x000108EC
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x000126F4 File Offset: 0x000108F4
		[PotentialPiiMaskWhenLogging]
		public List<QueryParameter> QueryParameters { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x000126FD File Offset: 0x000108FD
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x00012705 File Offset: 0x00010905
		public long? RowsRead { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00012710 File Offset: 0x00010910
		[XmlIgnore]
		public bool RowsReadSpecified
		{
			get
			{
				return this.RowsRead != null;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001272B File Offset: 0x0001092B
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x00012733 File Offset: 0x00010933
		public long? TotalTimeDataRetrieval { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001273C File Offset: 0x0001093C
		[XmlIgnore]
		public bool TotalTimeDataRetrievalSpecified
		{
			get
			{
				return this.TotalTimeDataRetrieval != null;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00012757 File Offset: 0x00010957
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0001275F File Offset: 0x0001095F
		public long? QueryPrepareAndExecutionTime { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00012768 File Offset: 0x00010968
		[XmlIgnore]
		public bool QueryPrepareAndExecutionTimeSpecified
		{
			get
			{
				return this.QueryPrepareAndExecutionTime != null;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00012783 File Offset: 0x00010983
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x0001278B File Offset: 0x0001098B
		public long? ExecuteReaderTime { get; set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00012794 File Offset: 0x00010994
		[XmlIgnore]
		public bool ExecuteReaderTimeSpecified
		{
			get
			{
				return this.ExecuteReaderTime != null;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000127AF File Offset: 0x000109AF
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x000127B7 File Offset: 0x000109B7
		public long? DataReaderMappingTime { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000127C0 File Offset: 0x000109C0
		[XmlIgnore]
		public bool DataReaderMappingTimeSpecified
		{
			get
			{
				return this.DataReaderMappingTime != null;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000127DB File Offset: 0x000109DB
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x000127E3 File Offset: 0x000109E3
		public long? DisposeDataReaderTime { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x000127EC File Offset: 0x000109EC
		[XmlIgnore]
		public bool DisposeDataReaderTimeSpecified
		{
			get
			{
				return this.DisposeDataReaderTime != null;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00012807 File Offset: 0x00010A07
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0001280F File Offset: 0x00010A0F
		public string CancelCommandTime { get; set; }
	}
}
