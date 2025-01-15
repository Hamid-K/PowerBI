using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C8E RID: 11406
	[ChildElementInfo(typeof(TabColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OutlineProperties))]
	[ChildElementInfo(typeof(PageSetupProperties))]
	internal class SheetProperties : OpenXmlCompositeElement
	{
		// Token: 0x170083AC RID: 33708
		// (get) Token: 0x06018517 RID: 99607 RVA: 0x0033FD79 File Offset: 0x0033DF79
		public override string LocalName
		{
			get
			{
				return "sheetPr";
			}
		}

		// Token: 0x170083AD RID: 33709
		// (get) Token: 0x06018518 RID: 99608 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170083AE RID: 33710
		// (get) Token: 0x06018519 RID: 99609 RVA: 0x003406E3 File Offset: 0x0033E8E3
		internal override int ElementTypeId
		{
			get
			{
				return 11386;
			}
		}

		// Token: 0x0601851A RID: 99610 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170083AF RID: 33711
		// (get) Token: 0x0601851B RID: 99611 RVA: 0x003406EA File Offset: 0x0033E8EA
		internal override string[] AttributeTagNames
		{
			get
			{
				return SheetProperties.attributeTagNames;
			}
		}

		// Token: 0x170083B0 RID: 33712
		// (get) Token: 0x0601851C RID: 99612 RVA: 0x003406F1 File Offset: 0x0033E8F1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SheetProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170083B1 RID: 33713
		// (get) Token: 0x0601851D RID: 99613 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601851E RID: 99614 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "syncHorizontal")]
		public BooleanValue SyncHorizontal
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

		// Token: 0x170083B2 RID: 33714
		// (get) Token: 0x0601851F RID: 99615 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06018520 RID: 99616 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "syncVertical")]
		public BooleanValue SyncVertical
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

		// Token: 0x170083B3 RID: 33715
		// (get) Token: 0x06018521 RID: 99617 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06018522 RID: 99618 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "syncRef")]
		public StringValue SyncReference
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170083B4 RID: 33716
		// (get) Token: 0x06018523 RID: 99619 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06018524 RID: 99620 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "transitionEvaluation")]
		public BooleanValue TransitionEvaluation
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

		// Token: 0x170083B5 RID: 33717
		// (get) Token: 0x06018525 RID: 99621 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06018526 RID: 99622 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "transitionEntry")]
		public BooleanValue TransitionEntry
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

		// Token: 0x170083B6 RID: 33718
		// (get) Token: 0x06018527 RID: 99623 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06018528 RID: 99624 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "published")]
		public BooleanValue Published
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

		// Token: 0x170083B7 RID: 33719
		// (get) Token: 0x06018529 RID: 99625 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0601852A RID: 99626 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "codeName")]
		public StringValue CodeName
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

		// Token: 0x170083B8 RID: 33720
		// (get) Token: 0x0601852B RID: 99627 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0601852C RID: 99628 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "filterMode")]
		public BooleanValue FilterMode
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

		// Token: 0x170083B9 RID: 33721
		// (get) Token: 0x0601852D RID: 99629 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0601852E RID: 99630 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "enableFormatConditionsCalculation")]
		public BooleanValue EnableFormatConditionsCalculation
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

		// Token: 0x0601852F RID: 99631 RVA: 0x00293ECF File Offset: 0x002920CF
		public SheetProperties()
		{
		}

		// Token: 0x06018530 RID: 99632 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SheetProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018531 RID: 99633 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SheetProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018532 RID: 99634 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SheetProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018533 RID: 99635 RVA: 0x003406F8 File Offset: 0x0033E8F8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tabColor" == name)
			{
				return new TabColor();
			}
			if (22 == namespaceId && "outlinePr" == name)
			{
				return new OutlineProperties();
			}
			if (22 == namespaceId && "pageSetUpPr" == name)
			{
				return new PageSetupProperties();
			}
			return null;
		}

		// Token: 0x170083BA RID: 33722
		// (get) Token: 0x06018534 RID: 99636 RVA: 0x0034074E File Offset: 0x0033E94E
		internal override string[] ElementTagNames
		{
			get
			{
				return SheetProperties.eleTagNames;
			}
		}

		// Token: 0x170083BB RID: 33723
		// (get) Token: 0x06018535 RID: 99637 RVA: 0x00340755 File Offset: 0x0033E955
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SheetProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170083BC RID: 33724
		// (get) Token: 0x06018536 RID: 99638 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170083BD RID: 33725
		// (get) Token: 0x06018537 RID: 99639 RVA: 0x0033FDBE File Offset: 0x0033DFBE
		// (set) Token: 0x06018538 RID: 99640 RVA: 0x0033FDC7 File Offset: 0x0033DFC7
		public TabColor TabColor
		{
			get
			{
				return base.GetElement<TabColor>(0);
			}
			set
			{
				base.SetElement<TabColor>(0, value);
			}
		}

		// Token: 0x170083BE RID: 33726
		// (get) Token: 0x06018539 RID: 99641 RVA: 0x0034075C File Offset: 0x0033E95C
		// (set) Token: 0x0601853A RID: 99642 RVA: 0x00340765 File Offset: 0x0033E965
		public OutlineProperties OutlineProperties
		{
			get
			{
				return base.GetElement<OutlineProperties>(1);
			}
			set
			{
				base.SetElement<OutlineProperties>(1, value);
			}
		}

		// Token: 0x170083BF RID: 33727
		// (get) Token: 0x0601853B RID: 99643 RVA: 0x0034076F File Offset: 0x0033E96F
		// (set) Token: 0x0601853C RID: 99644 RVA: 0x00340778 File Offset: 0x0033E978
		public PageSetupProperties PageSetupProperties
		{
			get
			{
				return base.GetElement<PageSetupProperties>(2);
			}
			set
			{
				base.SetElement<PageSetupProperties>(2, value);
			}
		}

		// Token: 0x0601853D RID: 99645 RVA: 0x00340784 File Offset: 0x0033E984
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "syncHorizontal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "syncVertical" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "syncRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "transitionEvaluation" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "transitionEntry" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "published" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "codeName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "filterMode" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "enableFormatConditionsCalculation" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601853E RID: 99646 RVA: 0x0034085F File Offset: 0x0033EA5F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetProperties>(deep);
		}

		// Token: 0x0601853F RID: 99647 RVA: 0x00340868 File Offset: 0x0033EA68
		// Note: this type is marked as 'beforefieldinit'.
		static SheetProperties()
		{
			byte[] array = new byte[9];
			SheetProperties.attributeNamespaceIds = array;
			SheetProperties.eleTagNames = new string[] { "tabColor", "outlinePr", "pageSetUpPr" };
			SheetProperties.eleNamespaceIds = new byte[] { 22, 22, 22 };
		}

		// Token: 0x04009FC7 RID: 40903
		private const string tagName = "sheetPr";

		// Token: 0x04009FC8 RID: 40904
		private const byte tagNsId = 22;

		// Token: 0x04009FC9 RID: 40905
		internal const int ElementTypeIdConst = 11386;

		// Token: 0x04009FCA RID: 40906
		private static string[] attributeTagNames = new string[] { "syncHorizontal", "syncVertical", "syncRef", "transitionEvaluation", "transitionEntry", "published", "codeName", "filterMode", "enableFormatConditionsCalculation" };

		// Token: 0x04009FCB RID: 40907
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009FCC RID: 40908
		private static readonly string[] eleTagNames;

		// Token: 0x04009FCD RID: 40909
		private static readonly byte[] eleNamespaceIds;
	}
}
