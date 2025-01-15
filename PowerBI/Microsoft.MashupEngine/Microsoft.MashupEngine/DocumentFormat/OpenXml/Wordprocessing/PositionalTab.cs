using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E7F RID: 11903
	[GeneratedCode("DomGen", "2.0")]
	internal class PositionalTab : OpenXmlLeafElement
	{
		// Token: 0x17008AE1 RID: 35553
		// (get) Token: 0x060194B1 RID: 103601 RVA: 0x003484F1 File Offset: 0x003466F1
		public override string LocalName
		{
			get
			{
				return "ptab";
			}
		}

		// Token: 0x17008AE2 RID: 35554
		// (get) Token: 0x060194B2 RID: 103602 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AE3 RID: 35555
		// (get) Token: 0x060194B3 RID: 103603 RVA: 0x003484F8 File Offset: 0x003466F8
		internal override int ElementTypeId
		{
			get
			{
				return 11573;
			}
		}

		// Token: 0x060194B4 RID: 103604 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008AE4 RID: 35556
		// (get) Token: 0x060194B5 RID: 103605 RVA: 0x003484FF File Offset: 0x003466FF
		internal override string[] AttributeTagNames
		{
			get
			{
				return PositionalTab.attributeTagNames;
			}
		}

		// Token: 0x17008AE5 RID: 35557
		// (get) Token: 0x060194B6 RID: 103606 RVA: 0x00348506 File Offset: 0x00346706
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PositionalTab.attributeNamespaceIds;
			}
		}

		// Token: 0x17008AE6 RID: 35558
		// (get) Token: 0x060194B7 RID: 103607 RVA: 0x0034850D File Offset: 0x0034670D
		// (set) Token: 0x060194B8 RID: 103608 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "alignment")]
		public EnumValue<AbsolutePositionTabAlignmentValues> Alignment
		{
			get
			{
				return (EnumValue<AbsolutePositionTabAlignmentValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008AE7 RID: 35559
		// (get) Token: 0x060194B9 RID: 103609 RVA: 0x0034851C File Offset: 0x0034671C
		// (set) Token: 0x060194BA RID: 103610 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "relativeTo")]
		public EnumValue<AbsolutePositionTabPositioningBaseValues> RelativeTo
		{
			get
			{
				return (EnumValue<AbsolutePositionTabPositioningBaseValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008AE8 RID: 35560
		// (get) Token: 0x060194BB RID: 103611 RVA: 0x0034852B File Offset: 0x0034672B
		// (set) Token: 0x060194BC RID: 103612 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "leader")]
		public EnumValue<AbsolutePositionTabLeaderCharValues> Leader
		{
			get
			{
				return (EnumValue<AbsolutePositionTabLeaderCharValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060194BE RID: 103614 RVA: 0x0034853C File Offset: 0x0034673C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "alignment" == name)
			{
				return new EnumValue<AbsolutePositionTabAlignmentValues>();
			}
			if (23 == namespaceId && "relativeTo" == name)
			{
				return new EnumValue<AbsolutePositionTabPositioningBaseValues>();
			}
			if (23 == namespaceId && "leader" == name)
			{
				return new EnumValue<AbsolutePositionTabLeaderCharValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060194BF RID: 103615 RVA: 0x00348599 File Offset: 0x00346799
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PositionalTab>(deep);
		}

		// Token: 0x0400A822 RID: 43042
		private const string tagName = "ptab";

		// Token: 0x0400A823 RID: 43043
		private const byte tagNsId = 23;

		// Token: 0x0400A824 RID: 43044
		internal const int ElementTypeIdConst = 11573;

		// Token: 0x0400A825 RID: 43045
		private static string[] attributeTagNames = new string[] { "alignment", "relativeTo", "leader" };

		// Token: 0x0400A826 RID: 43046
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
