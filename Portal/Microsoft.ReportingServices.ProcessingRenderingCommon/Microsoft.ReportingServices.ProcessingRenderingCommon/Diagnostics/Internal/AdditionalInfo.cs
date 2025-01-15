using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000BD RID: 189
	[XmlRoot(IsNullable = false)]
	public sealed class AdditionalInfo
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x000123DF File Offset: 0x000105DF
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x000123E7 File Offset: 0x000105E7
		public string RdcePreparationTime { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x000123F0 File Offset: 0x000105F0
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x000123F8 File Offset: 0x000105F8
		public string RdceInvocationTime { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00012401 File Offset: 0x00010601
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00012409 File Offset: 0x00010609
		public string RdceSnapshotGenerationTime { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00012412 File Offset: 0x00010612
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001241A File Offset: 0x0001061A
		public string SharedDataSet { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00012423 File Offset: 0x00010623
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0001242B File Offset: 0x0001062B
		public string ProcessingEngine { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00012434 File Offset: 0x00010634
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0001243C File Offset: 0x0001063C
		public long? TimeQueryTranslation { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00012448 File Offset: 0x00010648
		[XmlIgnore]
		public bool TimeQueryTranslationSpecified
		{
			get
			{
				return this.TimeQueryTranslation != null;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00012463 File Offset: 0x00010663
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0001246B File Offset: 0x0001066B
		[XmlIgnore]
		public bool? HasCloudToOnPremiseConnection { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00012474 File Offset: 0x00010674
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0001247C File Offset: 0x0001067C
		public ScaleTimeCategory ScalabilityTime { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00012485 File Offset: 0x00010685
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0001248D File Offset: 0x0001068D
		public EstimatedMemoryUsageKBCategory EstimatedMemoryUsageKB { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00012496 File Offset: 0x00010696
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x0001249E File Offset: 0x0001069E
		public ExternalImageCategory ExternalImages { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x000124A7 File Offset: 0x000106A7
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x000124AF File Offset: 0x000106AF
		public SerializableDictionary DataExtension { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x000124B8 File Offset: 0x000106B8
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x000124C0 File Offset: 0x000106C0
		public List<DataShape> DataShapes { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x000124C9 File Offset: 0x000106C9
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x000124D1 File Offset: 0x000106D1
		public List<Connection> Connections { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x000124DA File Offset: 0x000106DA
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x000124E2 File Offset: 0x000106E2
		public string SourceReportUri { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x000124EB File Offset: 0x000106EB
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x000124F3 File Offset: 0x000106F3
		public string SortItem { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x000124FC File Offset: 0x000106FC
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x00012504 File Offset: 0x00010704
		public string Direction { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0001250D File Offset: 0x0001070D
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x00012515 File Offset: 0x00010715
		public string ClearExistingSort { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001251E File Offset: 0x0001071E
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x00012526 File Offset: 0x00010726
		public string DrillthroughId { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001252F File Offset: 0x0001072F
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x00012537 File Offset: 0x00010737
		public string ToggleId { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00012540 File Offset: 0x00010740
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00012548 File Offset: 0x00010748
		public string DocumentMapId { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00012551 File Offset: 0x00010751
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x00012559 File Offset: 0x00010759
		public string BookmarkId { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00012562 File Offset: 0x00010762
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x0001256A File Offset: 0x0001076A
		public string StartPage { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00012573 File Offset: 0x00010773
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x0001257B File Offset: 0x0001077B
		public string EndPage { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00012584 File Offset: 0x00010784
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x0001258C File Offset: 0x0001078C
		public string FindValue { get; set; }

		// Token: 0x0600065F RID: 1631 RVA: 0x00012598 File Offset: 0x00010798
		internal void IncrementDataExtensionOperationCounter(string operation)
		{
			if (string.IsNullOrEmpty(operation))
			{
				return;
			}
			object dataExtensionSync = this.m_dataExtensionSync;
			lock (dataExtensionSync)
			{
				if (this.DataExtension == null)
				{
					this.DataExtension = new SerializableDictionary(StringComparer.OrdinalIgnoreCase);
				}
				if (this.DataExtension.ContainsKey(operation))
				{
					SerializableDictionary dataExtension = this.DataExtension;
					int num = dataExtension[operation];
					dataExtension[operation] = num + 1;
				}
				else
				{
					this.DataExtension.Add(operation, 1);
				}
			}
		}

		// Token: 0x04000361 RID: 865
		private readonly object m_dataExtensionSync = new object();
	}
}
