using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.ReportingServices.Portal.Interfaces.Models;

namespace Model
{
	// Token: 0x02000065 RID: 101
	public sealed class MobileReportManifest : IMobileReportManifest
	{
		// Token: 0x060002BD RID: 701 RVA: 0x000037CD File Offset: 0x000019CD
		public MobileReportManifest()
		{
			this.Resources = Enumerable.Empty<MobileReportManifest.ResourceGroup>();
			this.DataSets = Enumerable.Empty<MobileReportManifest.DataSetItem>();
			this.Thumbnails = Enumerable.Empty<MobileReportManifest.ThumbnailItem>();
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000037F6 File Offset: 0x000019F6
		// (set) Token: 0x060002BF RID: 703 RVA: 0x000037FE File Offset: 0x000019FE
		public MobileReportManifest.DefinitionItem Definition { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00003807 File Offset: 0x00001A07
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000380F File Offset: 0x00001A0F
		public IEnumerable<MobileReportManifest.ResourceGroup> Resources { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00003818 File Offset: 0x00001A18
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00003820 File Offset: 0x00001A20
		public IEnumerable<MobileReportManifest.DataSetItem> DataSets { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00003829 File Offset: 0x00001A29
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00003831 File Offset: 0x00001A31
		public IEnumerable<MobileReportManifest.ThumbnailItem> Thumbnails { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000383A File Offset: 0x00001A3A
		IMobileReportManifestItem IMobileReportManifest.Definition
		{
			get
			{
				return this.Definition;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00003842 File Offset: 0x00001A42
		IEnumerable<IMobileReportManifestResourceGroup> IMobileReportManifest.Resources
		{
			get
			{
				return this.Resources;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000384A File Offset: 0x00001A4A
		IEnumerable<IMobileReportManifestDataSetItem> IMobileReportManifest.DataSets
		{
			get
			{
				return this.DataSets;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00003852 File Offset: 0x00001A52
		IEnumerable<IMobileReportManifestThumbnailItem> IMobileReportManifest.Thumbnails
		{
			get
			{
				return this.Thumbnails;
			}
		}

		// Token: 0x020000CA RID: 202
		public abstract class ManifestItem : IMobileReportManifestItem
		{
			// Token: 0x1700022D RID: 557
			// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00004B8E File Offset: 0x00002D8E
			// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00004B96 File Offset: 0x00002D96
			public Guid Id { get; set; }

			// Token: 0x1700022E RID: 558
			// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00004B9F File Offset: 0x00002D9F
			// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00004BA7 File Offset: 0x00002DA7
			public string Path { get; set; }

			// Token: 0x1700022F RID: 559
			// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00004BB0 File Offset: 0x00002DB0
			// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00004BB8 File Offset: 0x00002DB8
			public string Name { get; set; }

			// Token: 0x17000230 RID: 560
			// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00004BC1 File Offset: 0x00002DC1
			// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00004BC9 File Offset: 0x00002DC9
			public string Hash { get; set; }
		}

		// Token: 0x020000CB RID: 203
		[ComplexType]
		public sealed class DefinitionItem : MobileReportManifest.ManifestItem
		{
		}

		// Token: 0x020000CC RID: 204
		[ComplexType]
		public sealed class DataSetItem : MobileReportManifest.ManifestItem, IMobileReportManifestDataSetItem, IMobileReportManifestItem
		{
			// Token: 0x17000231 RID: 561
			// (get) Token: 0x060005BC RID: 1468 RVA: 0x00004BDA File Offset: 0x00002DDA
			// (set) Token: 0x060005BD RID: 1469 RVA: 0x00004BE2 File Offset: 0x00002DE2
			public MobileReportDataSetType Type { get; set; }

			// Token: 0x17000232 RID: 562
			// (get) Token: 0x060005BE RID: 1470 RVA: 0x00004BEB File Offset: 0x00002DEB
			// (set) Token: 0x060005BF RID: 1471 RVA: 0x00004BF3 File Offset: 0x00002DF3
			public string TimeUnit { get; set; }

			// Token: 0x17000233 RID: 563
			// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00004BFC File Offset: 0x00002DFC
			// (set) Token: 0x060005C1 RID: 1473 RVA: 0x00004C04 File Offset: 0x00002E04
			public string DateTimeColumn { get; set; }

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00004C0D File Offset: 0x00002E0D
			// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00004C15 File Offset: 0x00002E15
			public bool IsParameterized { get; set; }
		}

		// Token: 0x020000CD RID: 205
		public sealed class ResourceGroup : IMobileReportManifestResourceGroup
		{
			// Token: 0x060005C5 RID: 1477 RVA: 0x00004C1E File Offset: 0x00002E1E
			public ResourceGroup()
			{
				this.Items = Enumerable.Empty<MobileReportManifest.ResourceItem>();
			}

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00004C31 File Offset: 0x00002E31
			// (set) Token: 0x060005C7 RID: 1479 RVA: 0x00004C39 File Offset: 0x00002E39
			public string Name { get; set; }

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00004C42 File Offset: 0x00002E42
			// (set) Token: 0x060005C9 RID: 1481 RVA: 0x00004C4A File Offset: 0x00002E4A
			public MobileReportResourceGroupType Type { get; set; }

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x060005CA RID: 1482 RVA: 0x00004C53 File Offset: 0x00002E53
			// (set) Token: 0x060005CB RID: 1483 RVA: 0x00004C5B File Offset: 0x00002E5B
			public IEnumerable<MobileReportManifest.ResourceItem> Items { get; set; }

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x060005CC RID: 1484 RVA: 0x00004C64 File Offset: 0x00002E64
			IEnumerable<IMobileReportManifestResourceItem> IMobileReportManifestResourceGroup.Items
			{
				get
				{
					return this.Items;
				}
			}
		}

		// Token: 0x020000CE RID: 206
		[ComplexType]
		public sealed class ResourceItem : MobileReportManifest.ManifestItem, IMobileReportManifestResourceItem, IMobileReportManifestItem
		{
			// Token: 0x17000239 RID: 569
			// (get) Token: 0x060005CD RID: 1485 RVA: 0x00004C6C File Offset: 0x00002E6C
			// (set) Token: 0x060005CE RID: 1486 RVA: 0x00004C74 File Offset: 0x00002E74
			public string Key { get; set; }
		}

		// Token: 0x020000CF RID: 207
		[ComplexType]
		public sealed class ThumbnailItem : MobileReportManifest.ManifestItem, IMobileReportManifestThumbnailItem, IMobileReportManifestItem
		{
			// Token: 0x1700023A RID: 570
			// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00004C7D File Offset: 0x00002E7D
			// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00004C85 File Offset: 0x00002E85
			public MobileReportThumbnailType Type { get; set; }
		}
	}
}
