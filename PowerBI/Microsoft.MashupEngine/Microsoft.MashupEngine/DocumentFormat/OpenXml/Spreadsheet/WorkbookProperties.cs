using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C52 RID: 11346
	[GeneratedCode("DomGen", "2.0")]
	internal class WorkbookProperties : OpenXmlLeafElement
	{
		// Token: 0x1700820D RID: 33293
		// (get) Token: 0x0601813E RID: 98622 RVA: 0x002E5D66 File Offset: 0x002E3F66
		public override string LocalName
		{
			get
			{
				return "workbookPr";
			}
		}

		// Token: 0x1700820E RID: 33294
		// (get) Token: 0x0601813F RID: 98623 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700820F RID: 33295
		// (get) Token: 0x06018140 RID: 98624 RVA: 0x0033E33F File Offset: 0x0033C53F
		internal override int ElementTypeId
		{
			get
			{
				return 11327;
			}
		}

		// Token: 0x06018141 RID: 98625 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008210 RID: 33296
		// (get) Token: 0x06018142 RID: 98626 RVA: 0x0033E346 File Offset: 0x0033C546
		internal override string[] AttributeTagNames
		{
			get
			{
				return WorkbookProperties.attributeTagNames;
			}
		}

		// Token: 0x17008211 RID: 33297
		// (get) Token: 0x06018143 RID: 98627 RVA: 0x0033E34D File Offset: 0x0033C54D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WorkbookProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17008212 RID: 33298
		// (get) Token: 0x06018144 RID: 98628 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018145 RID: 98629 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "date1904")]
		public BooleanValue Date1904
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008213 RID: 33299
		// (get) Token: 0x06018146 RID: 98630 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06018147 RID: 98631 RVA: 0x002BD47A File Offset: 0x002BB67A
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(0, "dateCompatibility")]
		public BooleanValue DateCompatibility
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008214 RID: 33300
		// (get) Token: 0x06018148 RID: 98632 RVA: 0x0033E354 File Offset: 0x0033C554
		// (set) Token: 0x06018149 RID: 98633 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showObjects")]
		public EnumValue<ObjectDisplayValues> ShowObjects
		{
			get
			{
				return (EnumValue<ObjectDisplayValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008215 RID: 33301
		// (get) Token: 0x0601814A RID: 98634 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601814B RID: 98635 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showBorderUnselectedTables")]
		public BooleanValue ShowBorderUnselectedTables
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

		// Token: 0x17008216 RID: 33302
		// (get) Token: 0x0601814C RID: 98636 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601814D RID: 98637 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "filterPrivacy")]
		public BooleanValue FilterPrivacy
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

		// Token: 0x17008217 RID: 33303
		// (get) Token: 0x0601814E RID: 98638 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0601814F RID: 98639 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "promptedSolutions")]
		public BooleanValue PromptedSolutions
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

		// Token: 0x17008218 RID: 33304
		// (get) Token: 0x06018150 RID: 98640 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06018151 RID: 98641 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "showInkAnnotation")]
		public BooleanValue ShowInkAnnotation
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008219 RID: 33305
		// (get) Token: 0x06018152 RID: 98642 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06018153 RID: 98643 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "backupFile")]
		public BooleanValue BackupFile
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700821A RID: 33306
		// (get) Token: 0x06018154 RID: 98644 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06018155 RID: 98645 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "saveExternalLinkValues")]
		public BooleanValue SaveExternalLinkValues
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x1700821B RID: 33307
		// (get) Token: 0x06018156 RID: 98646 RVA: 0x0033E363 File Offset: 0x0033C563
		// (set) Token: 0x06018157 RID: 98647 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "updateLinks")]
		public EnumValue<UpdateLinksBehaviorValues> UpdateLinks
		{
			get
			{
				return (EnumValue<UpdateLinksBehaviorValues>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x1700821C RID: 33308
		// (get) Token: 0x06018158 RID: 98648 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x06018159 RID: 98649 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "codeName")]
		public StringValue CodeName
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x1700821D RID: 33309
		// (get) Token: 0x0601815A RID: 98650 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0601815B RID: 98651 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "hidePivotFieldList")]
		public BooleanValue HidePivotFieldList
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x1700821E RID: 33310
		// (get) Token: 0x0601815C RID: 98652 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x0601815D RID: 98653 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "showPivotChartFilter")]
		public BooleanValue ShowPivotChartFilter
		{
			get
			{
				return (BooleanValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x1700821F RID: 33311
		// (get) Token: 0x0601815E RID: 98654 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x0601815F RID: 98655 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "allowRefreshQuery")]
		public BooleanValue AllowRefreshQuery
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17008220 RID: 33312
		// (get) Token: 0x06018160 RID: 98656 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x06018161 RID: 98657 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "publishItems")]
		public BooleanValue PublishItems
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17008221 RID: 33313
		// (get) Token: 0x06018162 RID: 98658 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x06018163 RID: 98659 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "checkCompatibility")]
		public BooleanValue CheckCompatibility
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17008222 RID: 33314
		// (get) Token: 0x06018164 RID: 98660 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x06018165 RID: 98661 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "autoCompressPictures")]
		public BooleanValue AutoCompressPictures
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17008223 RID: 33315
		// (get) Token: 0x06018166 RID: 98662 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x06018167 RID: 98663 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "refreshAllConnections")]
		public BooleanValue RefreshAllConnections
		{
			get
			{
				return (BooleanValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17008224 RID: 33316
		// (get) Token: 0x06018168 RID: 98664 RVA: 0x003389D0 File Offset: 0x00336BD0
		// (set) Token: 0x06018169 RID: 98665 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "defaultThemeVersion")]
		public UInt32Value DefaultThemeVersion
		{
			get
			{
				return (UInt32Value)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x0601816B RID: 98667 RVA: 0x0033E374 File Offset: 0x0033C574
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "date1904" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dateCompatibility" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showObjects" == name)
			{
				return new EnumValue<ObjectDisplayValues>();
			}
			if (namespaceId == 0 && "showBorderUnselectedTables" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "filterPrivacy" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "promptedSolutions" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showInkAnnotation" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "backupFile" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "saveExternalLinkValues" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "updateLinks" == name)
			{
				return new EnumValue<UpdateLinksBehaviorValues>();
			}
			if (namespaceId == 0 && "codeName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hidePivotFieldList" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showPivotChartFilter" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "allowRefreshQuery" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "publishItems" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "checkCompatibility" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoCompressPictures" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "refreshAllConnections" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "defaultThemeVersion" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601816C RID: 98668 RVA: 0x0033E52B File Offset: 0x0033C72B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WorkbookProperties>(deep);
		}

		// Token: 0x0601816D RID: 98669 RVA: 0x0033E534 File Offset: 0x0033C734
		// Note: this type is marked as 'beforefieldinit'.
		static WorkbookProperties()
		{
			byte[] array = new byte[19];
			WorkbookProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04009ECB RID: 40651
		private const string tagName = "workbookPr";

		// Token: 0x04009ECC RID: 40652
		private const byte tagNsId = 22;

		// Token: 0x04009ECD RID: 40653
		internal const int ElementTypeIdConst = 11327;

		// Token: 0x04009ECE RID: 40654
		private static string[] attributeTagNames = new string[]
		{
			"date1904", "dateCompatibility", "showObjects", "showBorderUnselectedTables", "filterPrivacy", "promptedSolutions", "showInkAnnotation", "backupFile", "saveExternalLinkValues", "updateLinks",
			"codeName", "hidePivotFieldList", "showPivotChartFilter", "allowRefreshQuery", "publishItems", "checkCompatibility", "autoCompressPictures", "refreshAllConnections", "defaultThemeVersion"
		};

		// Token: 0x04009ECF RID: 40655
		private static byte[] attributeNamespaceIds;
	}
}
