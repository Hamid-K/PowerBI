using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BB9 RID: 11193
	[ChildElementInfo(typeof(Formula))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OldFormula))]
	internal class RevisionDefinedName : OpenXmlCompositeElement
	{
		// Token: 0x17007C16 RID: 31766
		// (get) Token: 0x0601743F RID: 95295 RVA: 0x00334AD5 File Offset: 0x00332CD5
		public override string LocalName
		{
			get
			{
				return "rdn";
			}
		}

		// Token: 0x17007C17 RID: 31767
		// (get) Token: 0x06017440 RID: 95296 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C18 RID: 31768
		// (get) Token: 0x06017441 RID: 95297 RVA: 0x00334ADC File Offset: 0x00332CDC
		internal override int ElementTypeId
		{
			get
			{
				return 11164;
			}
		}

		// Token: 0x06017442 RID: 95298 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007C19 RID: 31769
		// (get) Token: 0x06017443 RID: 95299 RVA: 0x00334AE3 File Offset: 0x00332CE3
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionDefinedName.attributeTagNames;
			}
		}

		// Token: 0x17007C1A RID: 31770
		// (get) Token: 0x06017444 RID: 95300 RVA: 0x00334AEA File Offset: 0x00332CEA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionDefinedName.attributeNamespaceIds;
			}
		}

		// Token: 0x17007C1B RID: 31771
		// (get) Token: 0x06017445 RID: 95301 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017446 RID: 95302 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rId")]
		public UInt32Value RevisionId
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

		// Token: 0x17007C1C RID: 31772
		// (get) Token: 0x06017447 RID: 95303 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017448 RID: 95304 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ua")]
		public BooleanValue Ua
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

		// Token: 0x17007C1D RID: 31773
		// (get) Token: 0x06017449 RID: 95305 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601744A RID: 95306 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ra")]
		public BooleanValue Ra
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007C1E RID: 31774
		// (get) Token: 0x0601744B RID: 95307 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601744C RID: 95308 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "localSheetId")]
		public UInt32Value LocalSheetId
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007C1F RID: 31775
		// (get) Token: 0x0601744D RID: 95309 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601744E RID: 95310 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "customView")]
		public BooleanValue CustomView
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

		// Token: 0x17007C20 RID: 31776
		// (get) Token: 0x0601744F RID: 95311 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06017450 RID: 95312 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007C21 RID: 31777
		// (get) Token: 0x06017451 RID: 95313 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017452 RID: 95314 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "function")]
		public BooleanValue Function
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

		// Token: 0x17007C22 RID: 31778
		// (get) Token: 0x06017453 RID: 95315 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017454 RID: 95316 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "oldFunction")]
		public BooleanValue OldFunction
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

		// Token: 0x17007C23 RID: 31779
		// (get) Token: 0x06017455 RID: 95317 RVA: 0x00334AF1 File Offset: 0x00332CF1
		// (set) Token: 0x06017456 RID: 95318 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "functionGroupId")]
		public ByteValue FunctionGroupId
		{
			get
			{
				return (ByteValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007C24 RID: 31780
		// (get) Token: 0x06017457 RID: 95319 RVA: 0x0032DE80 File Offset: 0x0032C080
		// (set) Token: 0x06017458 RID: 95320 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "oldFunctionGroupId")]
		public ByteValue OldFunctionGroupId
		{
			get
			{
				return (ByteValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17007C25 RID: 31781
		// (get) Token: 0x06017459 RID: 95321 RVA: 0x0032DE90 File Offset: 0x0032C090
		// (set) Token: 0x0601745A RID: 95322 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "shortcutKey")]
		public ByteValue ShortcutKey
		{
			get
			{
				return (ByteValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17007C26 RID: 31782
		// (get) Token: 0x0601745B RID: 95323 RVA: 0x003299AA File Offset: 0x00327BAA
		// (set) Token: 0x0601745C RID: 95324 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "oldShortcutKey")]
		public ByteValue OldShortcutKey
		{
			get
			{
				return (ByteValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17007C27 RID: 31783
		// (get) Token: 0x0601745D RID: 95325 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x0601745E RID: 95326 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
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

		// Token: 0x17007C28 RID: 31784
		// (get) Token: 0x0601745F RID: 95327 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06017460 RID: 95328 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "oldHidden")]
		public BooleanValue OldHidden
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

		// Token: 0x17007C29 RID: 31785
		// (get) Token: 0x06017461 RID: 95329 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x06017462 RID: 95330 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "customMenu")]
		public StringValue CustomMenu
		{
			get
			{
				return (StringValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17007C2A RID: 31786
		// (get) Token: 0x06017463 RID: 95331 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x06017464 RID: 95332 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "oldCustomMenu")]
		public StringValue OldCustomMenu
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17007C2B RID: 31787
		// (get) Token: 0x06017465 RID: 95333 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x06017466 RID: 95334 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "description")]
		public StringValue Description
		{
			get
			{
				return (StringValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17007C2C RID: 31788
		// (get) Token: 0x06017467 RID: 95335 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x06017468 RID: 95336 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "oldDescription")]
		public StringValue OldDescription
		{
			get
			{
				return (StringValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17007C2D RID: 31789
		// (get) Token: 0x06017469 RID: 95337 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0601746A RID: 95338 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "help")]
		public StringValue Help
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17007C2E RID: 31790
		// (get) Token: 0x0601746B RID: 95339 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0601746C RID: 95340 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "oldHelp")]
		public StringValue OldHelp
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17007C2F RID: 31791
		// (get) Token: 0x0601746D RID: 95341 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0601746E RID: 95342 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "statusBar")]
		public StringValue StatusBar
		{
			get
			{
				return (StringValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17007C30 RID: 31792
		// (get) Token: 0x0601746F RID: 95343 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x06017470 RID: 95344 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "oldStatusBar")]
		public StringValue OldStatusBar
		{
			get
			{
				return (StringValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17007C31 RID: 31793
		// (get) Token: 0x06017471 RID: 95345 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x06017472 RID: 95346 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "comment")]
		public StringValue Comment
		{
			get
			{
				return (StringValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17007C32 RID: 31794
		// (get) Token: 0x06017473 RID: 95347 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x06017474 RID: 95348 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "oldComment")]
		public StringValue OldComment
		{
			get
			{
				return (StringValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x06017475 RID: 95349 RVA: 0x00293ECF File Offset: 0x002920CF
		public RevisionDefinedName()
		{
		}

		// Token: 0x06017476 RID: 95350 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RevisionDefinedName(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017477 RID: 95351 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RevisionDefinedName(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017478 RID: 95352 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RevisionDefinedName(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017479 RID: 95353 RVA: 0x00334B00 File Offset: 0x00332D00
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "formula" == name)
			{
				return new Formula();
			}
			if (22 == namespaceId && "oldFormula" == name)
			{
				return new OldFormula();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007C33 RID: 31795
		// (get) Token: 0x0601747A RID: 95354 RVA: 0x00334B56 File Offset: 0x00332D56
		internal override string[] ElementTagNames
		{
			get
			{
				return RevisionDefinedName.eleTagNames;
			}
		}

		// Token: 0x17007C34 RID: 31796
		// (get) Token: 0x0601747B RID: 95355 RVA: 0x00334B5D File Offset: 0x00332D5D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RevisionDefinedName.eleNamespaceIds;
			}
		}

		// Token: 0x17007C35 RID: 31797
		// (get) Token: 0x0601747C RID: 95356 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007C36 RID: 31798
		// (get) Token: 0x0601747D RID: 95357 RVA: 0x00334B64 File Offset: 0x00332D64
		// (set) Token: 0x0601747E RID: 95358 RVA: 0x00334B6D File Offset: 0x00332D6D
		public Formula Formula
		{
			get
			{
				return base.GetElement<Formula>(0);
			}
			set
			{
				base.SetElement<Formula>(0, value);
			}
		}

		// Token: 0x17007C37 RID: 31799
		// (get) Token: 0x0601747F RID: 95359 RVA: 0x00334B77 File Offset: 0x00332D77
		// (set) Token: 0x06017480 RID: 95360 RVA: 0x00334B80 File Offset: 0x00332D80
		public OldFormula OldFormula
		{
			get
			{
				return base.GetElement<OldFormula>(1);
			}
			set
			{
				base.SetElement<OldFormula>(1, value);
			}
		}

		// Token: 0x17007C38 RID: 31800
		// (get) Token: 0x06017481 RID: 95361 RVA: 0x00329822 File Offset: 0x00327A22
		// (set) Token: 0x06017482 RID: 95362 RVA: 0x0032982B File Offset: 0x00327A2B
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x06017483 RID: 95363 RVA: 0x00334B8C File Offset: 0x00332D8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "ua" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ra" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "localSheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "customView" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "function" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "oldFunction" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "functionGroupId" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "oldFunctionGroupId" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "shortcutKey" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "oldShortcutKey" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "oldHidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "customMenu" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "oldCustomMenu" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "oldDescription" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "help" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "oldHelp" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "statusBar" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "oldStatusBar" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "comment" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "oldComment" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017484 RID: 95364 RVA: 0x00334DB1 File Offset: 0x00332FB1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionDefinedName>(deep);
		}

		// Token: 0x06017485 RID: 95365 RVA: 0x00334DBC File Offset: 0x00332FBC
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionDefinedName()
		{
			byte[] array = new byte[24];
			RevisionDefinedName.attributeNamespaceIds = array;
			RevisionDefinedName.eleTagNames = new string[] { "formula", "oldFormula", "extLst" };
			RevisionDefinedName.eleNamespaceIds = new byte[] { 22, 22, 22 };
		}

		// Token: 0x04009BC4 RID: 39876
		private const string tagName = "rdn";

		// Token: 0x04009BC5 RID: 39877
		private const byte tagNsId = 22;

		// Token: 0x04009BC6 RID: 39878
		internal const int ElementTypeIdConst = 11164;

		// Token: 0x04009BC7 RID: 39879
		private static string[] attributeTagNames = new string[]
		{
			"rId", "ua", "ra", "localSheetId", "customView", "name", "function", "oldFunction", "functionGroupId", "oldFunctionGroupId",
			"shortcutKey", "oldShortcutKey", "hidden", "oldHidden", "customMenu", "oldCustomMenu", "description", "oldDescription", "help", "oldHelp",
			"statusBar", "oldStatusBar", "comment", "oldComment"
		};

		// Token: 0x04009BC8 RID: 39880
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009BC9 RID: 39881
		private static readonly string[] eleTagNames;

		// Token: 0x04009BCA RID: 39882
		private static readonly byte[] eleNamespaceIds;
	}
}
