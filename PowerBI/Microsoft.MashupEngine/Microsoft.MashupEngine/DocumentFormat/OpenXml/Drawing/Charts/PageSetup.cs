using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025CF RID: 9679
	[GeneratedCode("DomGen", "2.0")]
	internal class PageSetup : OpenXmlLeafElement
	{
		// Token: 0x170057E7 RID: 22503
		// (get) Token: 0x0601229C RID: 74396 RVA: 0x002F67DB File Offset: 0x002F49DB
		public override string LocalName
		{
			get
			{
				return "pageSetup";
			}
		}

		// Token: 0x170057E8 RID: 22504
		// (get) Token: 0x0601229D RID: 74397 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170057E9 RID: 22505
		// (get) Token: 0x0601229E RID: 74398 RVA: 0x002F67E2 File Offset: 0x002F49E2
		internal override int ElementTypeId
		{
			get
			{
				return 10519;
			}
		}

		// Token: 0x0601229F RID: 74399 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170057EA RID: 22506
		// (get) Token: 0x060122A0 RID: 74400 RVA: 0x002F67E9 File Offset: 0x002F49E9
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageSetup.attributeTagNames;
			}
		}

		// Token: 0x170057EB RID: 22507
		// (get) Token: 0x060122A1 RID: 74401 RVA: 0x002F67F0 File Offset: 0x002F49F0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageSetup.attributeNamespaceIds;
			}
		}

		// Token: 0x170057EC RID: 22508
		// (get) Token: 0x060122A2 RID: 74402 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060122A3 RID: 74403 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "paperSize")]
		public UInt32Value PaperSize
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170057ED RID: 22509
		// (get) Token: 0x060122A4 RID: 74404 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060122A5 RID: 74405 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "firstPageNumber")]
		public Int32Value FirstPageNumber
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170057EE RID: 22510
		// (get) Token: 0x060122A6 RID: 74406 RVA: 0x002F67F7 File Offset: 0x002F49F7
		// (set) Token: 0x060122A7 RID: 74407 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "orientation")]
		public EnumValue<PageSetupOrientationValues> Orientation
		{
			get
			{
				return (EnumValue<PageSetupOrientationValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170057EF RID: 22511
		// (get) Token: 0x060122A8 RID: 74408 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060122A9 RID: 74409 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "blackAndWhite")]
		public BooleanValue BlackAndWhite
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170057F0 RID: 22512
		// (get) Token: 0x060122AA RID: 74410 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060122AB RID: 74411 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "draft")]
		public BooleanValue Draft
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170057F1 RID: 22513
		// (get) Token: 0x060122AC RID: 74412 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060122AD RID: 74413 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "useFirstPageNumber")]
		public BooleanValue UseFirstPageNumber
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170057F2 RID: 22514
		// (get) Token: 0x060122AE RID: 74414 RVA: 0x002ED380 File Offset: 0x002EB580
		// (set) Token: 0x060122AF RID: 74415 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "horizontalDpi")]
		public Int32Value HorizontalDpi
		{
			get
			{
				return (Int32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170057F3 RID: 22515
		// (get) Token: 0x060122B0 RID: 74416 RVA: 0x002D14EB File Offset: 0x002CF6EB
		// (set) Token: 0x060122B1 RID: 74417 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "verticalDpi")]
		public Int32Value VerticalDpi
		{
			get
			{
				return (Int32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170057F4 RID: 22516
		// (get) Token: 0x060122B2 RID: 74418 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x060122B3 RID: 74419 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "copies")]
		public UInt32Value Copies
		{
			get
			{
				return (UInt32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x060122B5 RID: 74421 RVA: 0x002F6818 File Offset: 0x002F4A18
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "paperSize" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "firstPageNumber" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "orientation" == name)
			{
				return new EnumValue<PageSetupOrientationValues>();
			}
			if (namespaceId == 0 && "blackAndWhite" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "draft" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "useFirstPageNumber" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "horizontalDpi" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "verticalDpi" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "copies" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060122B6 RID: 74422 RVA: 0x002F68F3 File Offset: 0x002F4AF3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageSetup>(deep);
		}

		// Token: 0x060122B7 RID: 74423 RVA: 0x002F68FC File Offset: 0x002F4AFC
		// Note: this type is marked as 'beforefieldinit'.
		static PageSetup()
		{
			byte[] array = new byte[9];
			PageSetup.attributeNamespaceIds = array;
		}

		// Token: 0x04007E7F RID: 32383
		private const string tagName = "pageSetup";

		// Token: 0x04007E80 RID: 32384
		private const byte tagNsId = 11;

		// Token: 0x04007E81 RID: 32385
		internal const int ElementTypeIdConst = 10519;

		// Token: 0x04007E82 RID: 32386
		private static string[] attributeTagNames = new string[] { "paperSize", "firstPageNumber", "orientation", "blackAndWhite", "draft", "useFirstPageNumber", "horizontalDpi", "verticalDpi", "copies" };

		// Token: 0x04007E83 RID: 32387
		private static byte[] attributeNamespaceIds;
	}
}
