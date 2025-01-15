using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200022D RID: 557
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class CatalogItem
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x000223C2 File Offset: 0x000205C2
		// (set) Token: 0x0600152C RID: 5420 RVA: 0x000223CA File Offset: 0x000205CA
		public string ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x000223D3 File Offset: 0x000205D3
		// (set) Token: 0x0600152E RID: 5422 RVA: 0x000223DB File Offset: 0x000205DB
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x000223E4 File Offset: 0x000205E4
		// (set) Token: 0x06001530 RID: 5424 RVA: 0x000223EC File Offset: 0x000205EC
		public string Path
		{
			get
			{
				return this.pathField;
			}
			set
			{
				this.pathField = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x000223F5 File Offset: 0x000205F5
		// (set) Token: 0x06001532 RID: 5426 RVA: 0x000223FD File Offset: 0x000205FD
		public string VirtualPath
		{
			get
			{
				return this.virtualPathField;
			}
			set
			{
				this.virtualPathField = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00022406 File Offset: 0x00020606
		// (set) Token: 0x06001534 RID: 5428 RVA: 0x0002240E File Offset: 0x0002060E
		public ItemTypeEnum Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x00022417 File Offset: 0x00020617
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x0002241F File Offset: 0x0002061F
		public int Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00022428 File Offset: 0x00020628
		// (set) Token: 0x06001538 RID: 5432 RVA: 0x00022430 File Offset: 0x00020630
		[XmlIgnore]
		public bool SizeSpecified
		{
			get
			{
				return this.sizeFieldSpecified;
			}
			set
			{
				this.sizeFieldSpecified = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x00022439 File Offset: 0x00020639
		// (set) Token: 0x0600153A RID: 5434 RVA: 0x00022441 File Offset: 0x00020641
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0002244A File Offset: 0x0002064A
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x00022452 File Offset: 0x00020652
		public bool Hidden
		{
			get
			{
				return this.hiddenField;
			}
			set
			{
				this.hiddenField = value;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x0002245B File Offset: 0x0002065B
		// (set) Token: 0x0600153E RID: 5438 RVA: 0x00022463 File Offset: 0x00020663
		[XmlIgnore]
		public bool HiddenSpecified
		{
			get
			{
				return this.hiddenFieldSpecified;
			}
			set
			{
				this.hiddenFieldSpecified = value;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x0002246C File Offset: 0x0002066C
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x00022474 File Offset: 0x00020674
		public DateTime CreationDate
		{
			get
			{
				return this.creationDateField;
			}
			set
			{
				this.creationDateField = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x0002247D File Offset: 0x0002067D
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x00022485 File Offset: 0x00020685
		[XmlIgnore]
		public bool CreationDateSpecified
		{
			get
			{
				return this.creationDateFieldSpecified;
			}
			set
			{
				this.creationDateFieldSpecified = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x0002248E File Offset: 0x0002068E
		// (set) Token: 0x06001544 RID: 5444 RVA: 0x00022496 File Offset: 0x00020696
		public DateTime ModifiedDate
		{
			get
			{
				return this.modifiedDateField;
			}
			set
			{
				this.modifiedDateField = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x0002249F File Offset: 0x0002069F
		// (set) Token: 0x06001546 RID: 5446 RVA: 0x000224A7 File Offset: 0x000206A7
		[XmlIgnore]
		public bool ModifiedDateSpecified
		{
			get
			{
				return this.modifiedDateFieldSpecified;
			}
			set
			{
				this.modifiedDateFieldSpecified = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x000224B0 File Offset: 0x000206B0
		// (set) Token: 0x06001548 RID: 5448 RVA: 0x000224B8 File Offset: 0x000206B8
		public string CreatedBy
		{
			get
			{
				return this.createdByField;
			}
			set
			{
				this.createdByField = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x000224C1 File Offset: 0x000206C1
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x000224C9 File Offset: 0x000206C9
		public string ModifiedBy
		{
			get
			{
				return this.modifiedByField;
			}
			set
			{
				this.modifiedByField = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x000224D2 File Offset: 0x000206D2
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x000224DA File Offset: 0x000206DA
		public string MimeType
		{
			get
			{
				return this.mimeTypeField;
			}
			set
			{
				this.mimeTypeField = value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x000224E3 File Offset: 0x000206E3
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x000224EB File Offset: 0x000206EB
		public DateTime ExecutionDate
		{
			get
			{
				return this.executionDateField;
			}
			set
			{
				this.executionDateField = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000224F4 File Offset: 0x000206F4
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x000224FC File Offset: 0x000206FC
		[XmlIgnore]
		public bool ExecutionDateSpecified
		{
			get
			{
				return this.executionDateFieldSpecified;
			}
			set
			{
				this.executionDateFieldSpecified = value;
			}
		}

		// Token: 0x0400067F RID: 1663
		private string idField;

		// Token: 0x04000680 RID: 1664
		private string nameField;

		// Token: 0x04000681 RID: 1665
		private string pathField;

		// Token: 0x04000682 RID: 1666
		private string virtualPathField;

		// Token: 0x04000683 RID: 1667
		private ItemTypeEnum typeField;

		// Token: 0x04000684 RID: 1668
		private int sizeField;

		// Token: 0x04000685 RID: 1669
		private bool sizeFieldSpecified;

		// Token: 0x04000686 RID: 1670
		private string descriptionField;

		// Token: 0x04000687 RID: 1671
		private bool hiddenField;

		// Token: 0x04000688 RID: 1672
		private bool hiddenFieldSpecified;

		// Token: 0x04000689 RID: 1673
		private DateTime creationDateField;

		// Token: 0x0400068A RID: 1674
		private bool creationDateFieldSpecified;

		// Token: 0x0400068B RID: 1675
		private DateTime modifiedDateField;

		// Token: 0x0400068C RID: 1676
		private bool modifiedDateFieldSpecified;

		// Token: 0x0400068D RID: 1677
		private string createdByField;

		// Token: 0x0400068E RID: 1678
		private string modifiedByField;

		// Token: 0x0400068F RID: 1679
		private string mimeTypeField;

		// Token: 0x04000690 RID: 1680
		private DateTime executionDateField;

		// Token: 0x04000691 RID: 1681
		private bool executionDateFieldSpecified;
	}
}
