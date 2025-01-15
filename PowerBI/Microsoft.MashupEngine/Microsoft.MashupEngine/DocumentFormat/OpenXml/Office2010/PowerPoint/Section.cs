using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023C4 RID: 9156
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SectionSlideIdList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	internal class Section : OpenXmlCompositeElement
	{
		// Token: 0x17004CE2 RID: 19682
		// (get) Token: 0x060109E5 RID: 68069 RVA: 0x002E5401 File Offset: 0x002E3601
		public override string LocalName
		{
			get
			{
				return "section";
			}
		}

		// Token: 0x17004CE3 RID: 19683
		// (get) Token: 0x060109E6 RID: 68070 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CE4 RID: 19684
		// (get) Token: 0x060109E7 RID: 68071 RVA: 0x002E55D5 File Offset: 0x002E37D5
		internal override int ElementTypeId
		{
			get
			{
				return 12810;
			}
		}

		// Token: 0x060109E8 RID: 68072 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004CE5 RID: 19685
		// (get) Token: 0x060109E9 RID: 68073 RVA: 0x002E55DC File Offset: 0x002E37DC
		internal override string[] AttributeTagNames
		{
			get
			{
				return Section.attributeTagNames;
			}
		}

		// Token: 0x17004CE6 RID: 19686
		// (get) Token: 0x060109EA RID: 68074 RVA: 0x002E55E3 File Offset: 0x002E37E3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Section.attributeNamespaceIds;
			}
		}

		// Token: 0x17004CE7 RID: 19687
		// (get) Token: 0x060109EB RID: 68075 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060109EC RID: 68076 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004CE8 RID: 19688
		// (get) Token: 0x060109ED RID: 68077 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060109EE RID: 68078 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060109EF RID: 68079 RVA: 0x00293ECF File Offset: 0x002920CF
		public Section()
		{
		}

		// Token: 0x060109F0 RID: 68080 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Section(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060109F1 RID: 68081 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Section(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060109F2 RID: 68082 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Section(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060109F3 RID: 68083 RVA: 0x002E55EA File Offset: 0x002E37EA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "sldIdLst" == name)
			{
				return new SectionSlideIdList();
			}
			if (49 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004CE9 RID: 19689
		// (get) Token: 0x060109F4 RID: 68084 RVA: 0x002E561D File Offset: 0x002E381D
		internal override string[] ElementTagNames
		{
			get
			{
				return Section.eleTagNames;
			}
		}

		// Token: 0x17004CEA RID: 19690
		// (get) Token: 0x060109F5 RID: 68085 RVA: 0x002E5624 File Offset: 0x002E3824
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Section.eleNamespaceIds;
			}
		}

		// Token: 0x17004CEB RID: 19691
		// (get) Token: 0x060109F6 RID: 68086 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004CEC RID: 19692
		// (get) Token: 0x060109F7 RID: 68087 RVA: 0x002E562B File Offset: 0x002E382B
		// (set) Token: 0x060109F8 RID: 68088 RVA: 0x002E5634 File Offset: 0x002E3834
		public SectionSlideIdList SectionSlideIdList
		{
			get
			{
				return base.GetElement<SectionSlideIdList>(0);
			}
			set
			{
				base.SetElement<SectionSlideIdList>(0, value);
			}
		}

		// Token: 0x17004CED RID: 19693
		// (get) Token: 0x060109F9 RID: 68089 RVA: 0x002E563E File Offset: 0x002E383E
		// (set) Token: 0x060109FA RID: 68090 RVA: 0x002E5647 File Offset: 0x002E3847
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x060109FB RID: 68091 RVA: 0x002E5651 File Offset: 0x002E3851
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060109FC RID: 68092 RVA: 0x002E5687 File Offset: 0x002E3887
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Section>(deep);
		}

		// Token: 0x060109FD RID: 68093 RVA: 0x002E5690 File Offset: 0x002E3890
		// Note: this type is marked as 'beforefieldinit'.
		static Section()
		{
			byte[] array = new byte[2];
			Section.attributeNamespaceIds = array;
			Section.eleTagNames = new string[] { "sldIdLst", "extLst" };
			Section.eleNamespaceIds = new byte[] { 49, 49 };
		}

		// Token: 0x04007589 RID: 30089
		private const string tagName = "section";

		// Token: 0x0400758A RID: 30090
		private const byte tagNsId = 49;

		// Token: 0x0400758B RID: 30091
		internal const int ElementTypeIdConst = 12810;

		// Token: 0x0400758C RID: 30092
		private static string[] attributeTagNames = new string[] { "name", "id" };

		// Token: 0x0400758D RID: 30093
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400758E RID: 30094
		private static readonly string[] eleTagNames;

		// Token: 0x0400758F RID: 30095
		private static readonly byte[] eleNamespaceIds;
	}
}
