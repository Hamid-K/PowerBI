using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200014A RID: 330
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class CatalogItem
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000174E9 File Offset: 0x000156E9
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x000174F1 File Offset: 0x000156F1
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

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x000174FA File Offset: 0x000156FA
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x00017502 File Offset: 0x00015702
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

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0001750B File Offset: 0x0001570B
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x00017513 File Offset: 0x00015713
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

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0001751C File Offset: 0x0001571C
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x00017524 File Offset: 0x00015724
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

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0001752D File Offset: 0x0001572D
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x00017535 File Offset: 0x00015735
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

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0001753E File Offset: 0x0001573E
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x00017546 File Offset: 0x00015746
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

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0001754F File Offset: 0x0001574F
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x00017557 File Offset: 0x00015757
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

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00017560 File Offset: 0x00015760
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x00017568 File Offset: 0x00015768
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

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00017571 File Offset: 0x00015771
		// (set) Token: 0x06000DBD RID: 3517 RVA: 0x00017579 File Offset: 0x00015779
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

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x00017582 File Offset: 0x00015782
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x0001758A File Offset: 0x0001578A
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

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00017593 File Offset: 0x00015793
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0001759B File Offset: 0x0001579B
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

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x000175A4 File Offset: 0x000157A4
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x000175AC File Offset: 0x000157AC
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

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x000175B5 File Offset: 0x000157B5
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x000175BD File Offset: 0x000157BD
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

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x000175C6 File Offset: 0x000157C6
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x000175CE File Offset: 0x000157CE
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

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x000175D7 File Offset: 0x000157D7
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x000175DF File Offset: 0x000157DF
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

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x000175E8 File Offset: 0x000157E8
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x000175F0 File Offset: 0x000157F0
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

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x000175F9 File Offset: 0x000157F9
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x00017601 File Offset: 0x00015801
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

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0001760A File Offset: 0x0001580A
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x00017612 File Offset: 0x00015812
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

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0001761B File Offset: 0x0001581B
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x00017623 File Offset: 0x00015823
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

		// Token: 0x04000435 RID: 1077
		private string idField;

		// Token: 0x04000436 RID: 1078
		private string nameField;

		// Token: 0x04000437 RID: 1079
		private string pathField;

		// Token: 0x04000438 RID: 1080
		private string virtualPathField;

		// Token: 0x04000439 RID: 1081
		private ItemTypeEnum typeField;

		// Token: 0x0400043A RID: 1082
		private int sizeField;

		// Token: 0x0400043B RID: 1083
		private bool sizeFieldSpecified;

		// Token: 0x0400043C RID: 1084
		private string descriptionField;

		// Token: 0x0400043D RID: 1085
		private bool hiddenField;

		// Token: 0x0400043E RID: 1086
		private bool hiddenFieldSpecified;

		// Token: 0x0400043F RID: 1087
		private DateTime creationDateField;

		// Token: 0x04000440 RID: 1088
		private bool creationDateFieldSpecified;

		// Token: 0x04000441 RID: 1089
		private DateTime modifiedDateField;

		// Token: 0x04000442 RID: 1090
		private bool modifiedDateFieldSpecified;

		// Token: 0x04000443 RID: 1091
		private string createdByField;

		// Token: 0x04000444 RID: 1092
		private string modifiedByField;

		// Token: 0x04000445 RID: 1093
		private string mimeTypeField;

		// Token: 0x04000446 RID: 1094
		private DateTime executionDateField;

		// Token: 0x04000447 RID: 1095
		private bool executionDateFieldSpecified;
	}
}
