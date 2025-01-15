using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A22 RID: 10786
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SubTimeNodeList))]
	[ChildElementInfo(typeof(EndConditionList))]
	[ChildElementInfo(typeof(StartConditionList))]
	[ChildElementInfo(typeof(EndSync))]
	[ChildElementInfo(typeof(Iterate))]
	[ChildElementInfo(typeof(ChildTimeNodeList))]
	internal class CommonTimeNode : OpenXmlCompositeElement
	{
		// Token: 0x17007070 RID: 28784
		// (get) Token: 0x06015A3D RID: 88637 RVA: 0x00321862 File Offset: 0x0031FA62
		public override string LocalName
		{
			get
			{
				return "cTn";
			}
		}

		// Token: 0x17007071 RID: 28785
		// (get) Token: 0x06015A3E RID: 88638 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007072 RID: 28786
		// (get) Token: 0x06015A3F RID: 88639 RVA: 0x00321869 File Offset: 0x0031FA69
		internal override int ElementTypeId
		{
			get
			{
				return 12212;
			}
		}

		// Token: 0x06015A40 RID: 88640 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007073 RID: 28787
		// (get) Token: 0x06015A41 RID: 88641 RVA: 0x00321870 File Offset: 0x0031FA70
		internal override string[] AttributeTagNames
		{
			get
			{
				return CommonTimeNode.attributeTagNames;
			}
		}

		// Token: 0x17007074 RID: 28788
		// (get) Token: 0x06015A42 RID: 88642 RVA: 0x00321877 File Offset: 0x0031FA77
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CommonTimeNode.attributeNamespaceIds;
			}
		}

		// Token: 0x17007075 RID: 28789
		// (get) Token: 0x06015A43 RID: 88643 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06015A44 RID: 88644 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
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

		// Token: 0x17007076 RID: 28790
		// (get) Token: 0x06015A45 RID: 88645 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06015A46 RID: 88646 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "presetID")]
		public Int32Value PresetId
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

		// Token: 0x17007077 RID: 28791
		// (get) Token: 0x06015A47 RID: 88647 RVA: 0x0032187E File Offset: 0x0031FA7E
		// (set) Token: 0x06015A48 RID: 88648 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "presetClass")]
		public EnumValue<TimeNodePresetClassValues> PresetClass
		{
			get
			{
				return (EnumValue<TimeNodePresetClassValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007078 RID: 28792
		// (get) Token: 0x06015A49 RID: 88649 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06015A4A RID: 88650 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "presetSubtype")]
		public Int32Value PresetSubtype
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007079 RID: 28793
		// (get) Token: 0x06015A4B RID: 88651 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06015A4C RID: 88652 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "dur")]
		public StringValue Duration
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700707A RID: 28794
		// (get) Token: 0x06015A4D RID: 88653 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06015A4E RID: 88654 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "repeatCount")]
		public StringValue RepeatCount
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

		// Token: 0x1700707B RID: 28795
		// (get) Token: 0x06015A4F RID: 88655 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06015A50 RID: 88656 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "repeatDur")]
		public StringValue RepeatDuration
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700707C RID: 28796
		// (get) Token: 0x06015A51 RID: 88657 RVA: 0x002D14EB File Offset: 0x002CF6EB
		// (set) Token: 0x06015A52 RID: 88658 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "spd")]
		public Int32Value Speed
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

		// Token: 0x1700707D RID: 28797
		// (get) Token: 0x06015A53 RID: 88659 RVA: 0x002ED55B File Offset: 0x002EB75B
		// (set) Token: 0x06015A54 RID: 88660 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "accel")]
		public Int32Value Acceleration
		{
			get
			{
				return (Int32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x1700707E RID: 28798
		// (get) Token: 0x06015A55 RID: 88661 RVA: 0x002D14FA File Offset: 0x002CF6FA
		// (set) Token: 0x06015A56 RID: 88662 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "decel")]
		public Int32Value Deceleration
		{
			get
			{
				return (Int32Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x1700707F RID: 28799
		// (get) Token: 0x06015A57 RID: 88663 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06015A58 RID: 88664 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "autoRev")]
		public BooleanValue AutoReverse
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17007080 RID: 28800
		// (get) Token: 0x06015A59 RID: 88665 RVA: 0x0032188D File Offset: 0x0031FA8D
		// (set) Token: 0x06015A5A RID: 88666 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "restart")]
		public EnumValue<TimeNodeRestartValues> Restart
		{
			get
			{
				return (EnumValue<TimeNodeRestartValues>)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17007081 RID: 28801
		// (get) Token: 0x06015A5B RID: 88667 RVA: 0x0032189D File Offset: 0x0031FA9D
		// (set) Token: 0x06015A5C RID: 88668 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "fill")]
		public EnumValue<TimeNodeFillValues> Fill
		{
			get
			{
				return (EnumValue<TimeNodeFillValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17007082 RID: 28802
		// (get) Token: 0x06015A5D RID: 88669 RVA: 0x003218AD File Offset: 0x0031FAAD
		// (set) Token: 0x06015A5E RID: 88670 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "syncBehavior")]
		public EnumValue<TimeNodeSyncValues> SyncBehavior
		{
			get
			{
				return (EnumValue<TimeNodeSyncValues>)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17007083 RID: 28803
		// (get) Token: 0x06015A5F RID: 88671 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x06015A60 RID: 88672 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "tmFilter")]
		public StringValue TimeFilter
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

		// Token: 0x17007084 RID: 28804
		// (get) Token: 0x06015A61 RID: 88673 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x06015A62 RID: 88674 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "evtFilter")]
		public StringValue EventFilter
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

		// Token: 0x17007085 RID: 28805
		// (get) Token: 0x06015A63 RID: 88675 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x06015A64 RID: 88676 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "display")]
		public BooleanValue Display
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

		// Token: 0x17007086 RID: 28806
		// (get) Token: 0x06015A65 RID: 88677 RVA: 0x003218BD File Offset: 0x0031FABD
		// (set) Token: 0x06015A66 RID: 88678 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "masterRel")]
		public EnumValue<TimeNodeMasterRelationValues> MasterRelation
		{
			get
			{
				return (EnumValue<TimeNodeMasterRelationValues>)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17007087 RID: 28807
		// (get) Token: 0x06015A67 RID: 88679 RVA: 0x00300821 File Offset: 0x002FEA21
		// (set) Token: 0x06015A68 RID: 88680 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "bldLvl")]
		public Int32Value BuildLevel
		{
			get
			{
				return (Int32Value)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17007088 RID: 28808
		// (get) Token: 0x06015A69 RID: 88681 RVA: 0x003218CD File Offset: 0x0031FACD
		// (set) Token: 0x06015A6A RID: 88682 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "grpId")]
		public UInt32Value GroupId
		{
			get
			{
				return (UInt32Value)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17007089 RID: 28809
		// (get) Token: 0x06015A6B RID: 88683 RVA: 0x002C8F75 File Offset: 0x002C7175
		// (set) Token: 0x06015A6C RID: 88684 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "afterEffect")]
		public BooleanValue AfterEffect
		{
			get
			{
				return (BooleanValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x1700708A RID: 28810
		// (get) Token: 0x06015A6D RID: 88685 RVA: 0x003218DD File Offset: 0x0031FADD
		// (set) Token: 0x06015A6E RID: 88686 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "nodeType")]
		public EnumValue<TimeNodeValues> NodeType
		{
			get
			{
				return (EnumValue<TimeNodeValues>)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x1700708B RID: 28811
		// (get) Token: 0x06015A6F RID: 88687 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x06015A70 RID: 88688 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "nodePh")]
		public BooleanValue NodePlaceholder
		{
			get
			{
				return (BooleanValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x1700708C RID: 28812
		// (get) Token: 0x06015A71 RID: 88689 RVA: 0x00300861 File Offset: 0x002FEA61
		// (set) Token: 0x06015A72 RID: 88690 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(49, "presetBounceEnd")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Int32Value PresetBounceEnd
		{
			get
			{
				return (Int32Value)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x06015A73 RID: 88691 RVA: 0x00293ECF File Offset: 0x002920CF
		public CommonTimeNode()
		{
		}

		// Token: 0x06015A74 RID: 88692 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CommonTimeNode(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A75 RID: 88693 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CommonTimeNode(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A76 RID: 88694 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CommonTimeNode(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015A77 RID: 88695 RVA: 0x003218F0 File Offset: 0x0031FAF0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "stCondLst" == name)
			{
				return new StartConditionList();
			}
			if (24 == namespaceId && "endCondLst" == name)
			{
				return new EndConditionList();
			}
			if (24 == namespaceId && "endSync" == name)
			{
				return new EndSync();
			}
			if (24 == namespaceId && "iterate" == name)
			{
				return new Iterate();
			}
			if (24 == namespaceId && "childTnLst" == name)
			{
				return new ChildTimeNodeList();
			}
			if (24 == namespaceId && "subTnLst" == name)
			{
				return new SubTimeNodeList();
			}
			return null;
		}

		// Token: 0x1700708D RID: 28813
		// (get) Token: 0x06015A78 RID: 88696 RVA: 0x0032198E File Offset: 0x0031FB8E
		internal override string[] ElementTagNames
		{
			get
			{
				return CommonTimeNode.eleTagNames;
			}
		}

		// Token: 0x1700708E RID: 28814
		// (get) Token: 0x06015A79 RID: 88697 RVA: 0x00321995 File Offset: 0x0031FB95
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CommonTimeNode.eleNamespaceIds;
			}
		}

		// Token: 0x1700708F RID: 28815
		// (get) Token: 0x06015A7A RID: 88698 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007090 RID: 28816
		// (get) Token: 0x06015A7B RID: 88699 RVA: 0x0032199C File Offset: 0x0031FB9C
		// (set) Token: 0x06015A7C RID: 88700 RVA: 0x003219A5 File Offset: 0x0031FBA5
		public StartConditionList StartConditionList
		{
			get
			{
				return base.GetElement<StartConditionList>(0);
			}
			set
			{
				base.SetElement<StartConditionList>(0, value);
			}
		}

		// Token: 0x17007091 RID: 28817
		// (get) Token: 0x06015A7D RID: 88701 RVA: 0x003219AF File Offset: 0x0031FBAF
		// (set) Token: 0x06015A7E RID: 88702 RVA: 0x003219B8 File Offset: 0x0031FBB8
		public EndConditionList EndConditionList
		{
			get
			{
				return base.GetElement<EndConditionList>(1);
			}
			set
			{
				base.SetElement<EndConditionList>(1, value);
			}
		}

		// Token: 0x17007092 RID: 28818
		// (get) Token: 0x06015A7F RID: 88703 RVA: 0x003219C2 File Offset: 0x0031FBC2
		// (set) Token: 0x06015A80 RID: 88704 RVA: 0x003219CB File Offset: 0x0031FBCB
		public EndSync EndSync
		{
			get
			{
				return base.GetElement<EndSync>(2);
			}
			set
			{
				base.SetElement<EndSync>(2, value);
			}
		}

		// Token: 0x17007093 RID: 28819
		// (get) Token: 0x06015A81 RID: 88705 RVA: 0x003219D5 File Offset: 0x0031FBD5
		// (set) Token: 0x06015A82 RID: 88706 RVA: 0x003219DE File Offset: 0x0031FBDE
		public Iterate Iterate
		{
			get
			{
				return base.GetElement<Iterate>(3);
			}
			set
			{
				base.SetElement<Iterate>(3, value);
			}
		}

		// Token: 0x17007094 RID: 28820
		// (get) Token: 0x06015A83 RID: 88707 RVA: 0x003219E8 File Offset: 0x0031FBE8
		// (set) Token: 0x06015A84 RID: 88708 RVA: 0x003219F1 File Offset: 0x0031FBF1
		public ChildTimeNodeList ChildTimeNodeList
		{
			get
			{
				return base.GetElement<ChildTimeNodeList>(4);
			}
			set
			{
				base.SetElement<ChildTimeNodeList>(4, value);
			}
		}

		// Token: 0x17007095 RID: 28821
		// (get) Token: 0x06015A85 RID: 88709 RVA: 0x003219FB File Offset: 0x0031FBFB
		// (set) Token: 0x06015A86 RID: 88710 RVA: 0x00321A04 File Offset: 0x0031FC04
		public SubTimeNodeList SubTimeNodeList
		{
			get
			{
				return base.GetElement<SubTimeNodeList>(5);
			}
			set
			{
				base.SetElement<SubTimeNodeList>(5, value);
			}
		}

		// Token: 0x06015A87 RID: 88711 RVA: 0x00321A10 File Offset: 0x0031FC10
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "presetID" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "presetClass" == name)
			{
				return new EnumValue<TimeNodePresetClassValues>();
			}
			if (namespaceId == 0 && "presetSubtype" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "dur" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "repeatCount" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "repeatDur" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "spd" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "accel" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "decel" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "autoRev" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "restart" == name)
			{
				return new EnumValue<TimeNodeRestartValues>();
			}
			if (namespaceId == 0 && "fill" == name)
			{
				return new EnumValue<TimeNodeFillValues>();
			}
			if (namespaceId == 0 && "syncBehavior" == name)
			{
				return new EnumValue<TimeNodeSyncValues>();
			}
			if (namespaceId == 0 && "tmFilter" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "evtFilter" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "display" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "masterRel" == name)
			{
				return new EnumValue<TimeNodeMasterRelationValues>();
			}
			if (namespaceId == 0 && "bldLvl" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "grpId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "afterEffect" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "nodeType" == name)
			{
				return new EnumValue<TimeNodeValues>();
			}
			if (namespaceId == 0 && "nodePh" == name)
			{
				return new BooleanValue();
			}
			if (49 == namespaceId && "presetBounceEnd" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015A88 RID: 88712 RVA: 0x00321C37 File Offset: 0x0031FE37
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommonTimeNode>(deep);
		}

		// Token: 0x04009437 RID: 37943
		private const string tagName = "cTn";

		// Token: 0x04009438 RID: 37944
		private const byte tagNsId = 24;

		// Token: 0x04009439 RID: 37945
		internal const int ElementTypeIdConst = 12212;

		// Token: 0x0400943A RID: 37946
		private static string[] attributeTagNames = new string[]
		{
			"id", "presetID", "presetClass", "presetSubtype", "dur", "repeatCount", "repeatDur", "spd", "accel", "decel",
			"autoRev", "restart", "fill", "syncBehavior", "tmFilter", "evtFilter", "display", "masterRel", "bldLvl", "grpId",
			"afterEffect", "nodeType", "nodePh", "presetBounceEnd"
		};

		// Token: 0x0400943B RID: 37947
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 49
		};

		// Token: 0x0400943C RID: 37948
		private static readonly string[] eleTagNames = new string[] { "stCondLst", "endCondLst", "endSync", "iterate", "childTnLst", "subTnLst" };

		// Token: 0x0400943D RID: 37949
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24, 24 };
	}
}
