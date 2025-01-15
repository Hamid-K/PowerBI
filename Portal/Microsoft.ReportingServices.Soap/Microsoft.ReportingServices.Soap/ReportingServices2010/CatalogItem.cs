using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000040 RID: 64
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class CatalogItem
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0000D5E1 File Offset: 0x0000B7E1
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x0000D5E9 File Offset: 0x0000B7E9
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0000D5F2 File Offset: 0x0000B7F2
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x0000D5FA File Offset: 0x0000B7FA
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000D603 File Offset: 0x0000B803
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x0000D60B File Offset: 0x0000B80B
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000D614 File Offset: 0x0000B814
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x0000D61C File Offset: 0x0000B81C
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000D625 File Offset: 0x0000B825
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x0000D62D File Offset: 0x0000B82D
		public string TypeName
		{
			get
			{
				return this.typeNameField;
			}
			set
			{
				this.typeNameField = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000D636 File Offset: 0x0000B836
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0000D63E File Offset: 0x0000B83E
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

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000D647 File Offset: 0x0000B847
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0000D64F File Offset: 0x0000B84F
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

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000D658 File Offset: 0x0000B858
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0000D660 File Offset: 0x0000B860
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

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000D669 File Offset: 0x0000B869
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0000D671 File Offset: 0x0000B871
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000D67A File Offset: 0x0000B87A
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0000D682 File Offset: 0x0000B882
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000D68B File Offset: 0x0000B88B
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0000D693 File Offset: 0x0000B893
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

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000D69C File Offset: 0x0000B89C
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
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

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000D6AD File Offset: 0x0000B8AD
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0000D6B5 File Offset: 0x0000B8B5
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

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000D6BE File Offset: 0x0000B8BE
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x0000D6C6 File Offset: 0x0000B8C6
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

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000D6CF File Offset: 0x0000B8CF
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0000D6D7 File Offset: 0x0000B8D7
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

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0000D6E8 File Offset: 0x0000B8E8
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

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0000D6F1 File Offset: 0x0000B8F1
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x0000D6F9 File Offset: 0x0000B8F9
		public Property[] ItemMetadata
		{
			get
			{
				return this.itemMetadataField;
			}
			set
			{
				this.itemMetadataField = value;
			}
		}

		// Token: 0x04000204 RID: 516
		private string idField;

		// Token: 0x04000205 RID: 517
		private string nameField;

		// Token: 0x04000206 RID: 518
		private string pathField;

		// Token: 0x04000207 RID: 519
		private string virtualPathField;

		// Token: 0x04000208 RID: 520
		private string typeNameField;

		// Token: 0x04000209 RID: 521
		private int sizeField;

		// Token: 0x0400020A RID: 522
		private bool sizeFieldSpecified;

		// Token: 0x0400020B RID: 523
		private string descriptionField;

		// Token: 0x0400020C RID: 524
		private bool hiddenField;

		// Token: 0x0400020D RID: 525
		private bool hiddenFieldSpecified;

		// Token: 0x0400020E RID: 526
		private DateTime creationDateField;

		// Token: 0x0400020F RID: 527
		private bool creationDateFieldSpecified;

		// Token: 0x04000210 RID: 528
		private DateTime modifiedDateField;

		// Token: 0x04000211 RID: 529
		private bool modifiedDateFieldSpecified;

		// Token: 0x04000212 RID: 530
		private string createdByField;

		// Token: 0x04000213 RID: 531
		private string modifiedByField;

		// Token: 0x04000214 RID: 532
		private Property[] itemMetadataField;
	}
}
