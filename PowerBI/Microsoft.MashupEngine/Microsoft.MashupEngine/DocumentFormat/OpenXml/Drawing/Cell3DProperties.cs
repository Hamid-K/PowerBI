using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027EC RID: 10220
	[ChildElementInfo(typeof(Bevel))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LightRig))]
	internal class Cell3DProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700648C RID: 25740
		// (get) Token: 0x06013EDC RID: 81628 RVA: 0x0030D420 File Offset: 0x0030B620
		public override string LocalName
		{
			get
			{
				return "cell3D";
			}
		}

		// Token: 0x1700648D RID: 25741
		// (get) Token: 0x06013EDD RID: 81629 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700648E RID: 25742
		// (get) Token: 0x06013EDE RID: 81630 RVA: 0x0030D427 File Offset: 0x0030B627
		internal override int ElementTypeId
		{
			get
			{
				return 10258;
			}
		}

		// Token: 0x06013EDF RID: 81631 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700648F RID: 25743
		// (get) Token: 0x06013EE0 RID: 81632 RVA: 0x0030D42E File Offset: 0x0030B62E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Cell3DProperties.attributeTagNames;
			}
		}

		// Token: 0x17006490 RID: 25744
		// (get) Token: 0x06013EE1 RID: 81633 RVA: 0x0030D435 File Offset: 0x0030B635
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Cell3DProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17006491 RID: 25745
		// (get) Token: 0x06013EE2 RID: 81634 RVA: 0x0030D43C File Offset: 0x0030B63C
		// (set) Token: 0x06013EE3 RID: 81635 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "prstMaterial")]
		public EnumValue<PresetMaterialTypeValues> PresetMaterial
		{
			get
			{
				return (EnumValue<PresetMaterialTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06013EE4 RID: 81636 RVA: 0x00293ECF File Offset: 0x002920CF
		public Cell3DProperties()
		{
		}

		// Token: 0x06013EE5 RID: 81637 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Cell3DProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013EE6 RID: 81638 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Cell3DProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013EE7 RID: 81639 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Cell3DProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013EE8 RID: 81640 RVA: 0x0030D44C File Offset: 0x0030B64C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "bevel" == name)
			{
				return new Bevel();
			}
			if (10 == namespaceId && "lightRig" == name)
			{
				return new LightRig();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006492 RID: 25746
		// (get) Token: 0x06013EE9 RID: 81641 RVA: 0x0030D4A2 File Offset: 0x0030B6A2
		internal override string[] ElementTagNames
		{
			get
			{
				return Cell3DProperties.eleTagNames;
			}
		}

		// Token: 0x17006493 RID: 25747
		// (get) Token: 0x06013EEA RID: 81642 RVA: 0x0030D4A9 File Offset: 0x0030B6A9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Cell3DProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006494 RID: 25748
		// (get) Token: 0x06013EEB RID: 81643 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006495 RID: 25749
		// (get) Token: 0x06013EEC RID: 81644 RVA: 0x0030D4B0 File Offset: 0x0030B6B0
		// (set) Token: 0x06013EED RID: 81645 RVA: 0x0030D4B9 File Offset: 0x0030B6B9
		public Bevel Bevel
		{
			get
			{
				return base.GetElement<Bevel>(0);
			}
			set
			{
				base.SetElement<Bevel>(0, value);
			}
		}

		// Token: 0x17006496 RID: 25750
		// (get) Token: 0x06013EEE RID: 81646 RVA: 0x002E0C03 File Offset: 0x002DEE03
		// (set) Token: 0x06013EEF RID: 81647 RVA: 0x002E0C0C File Offset: 0x002DEE0C
		public LightRig LightRig
		{
			get
			{
				return base.GetElement<LightRig>(1);
			}
			set
			{
				base.SetElement<LightRig>(1, value);
			}
		}

		// Token: 0x17006497 RID: 25751
		// (get) Token: 0x06013EF0 RID: 81648 RVA: 0x003012C6 File Offset: 0x002FF4C6
		// (set) Token: 0x06013EF1 RID: 81649 RVA: 0x003012CF File Offset: 0x002FF4CF
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

		// Token: 0x06013EF2 RID: 81650 RVA: 0x0030D4C3 File Offset: 0x0030B6C3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "prstMaterial" == name)
			{
				return new EnumValue<PresetMaterialTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013EF3 RID: 81651 RVA: 0x0030D4E3 File Offset: 0x0030B6E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Cell3DProperties>(deep);
		}

		// Token: 0x06013EF4 RID: 81652 RVA: 0x0030D4EC File Offset: 0x0030B6EC
		// Note: this type is marked as 'beforefieldinit'.
		static Cell3DProperties()
		{
			byte[] array = new byte[1];
			Cell3DProperties.attributeNamespaceIds = array;
			Cell3DProperties.eleTagNames = new string[] { "bevel", "lightRig", "extLst" };
			Cell3DProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04008854 RID: 34900
		private const string tagName = "cell3D";

		// Token: 0x04008855 RID: 34901
		private const byte tagNsId = 10;

		// Token: 0x04008856 RID: 34902
		internal const int ElementTypeIdConst = 10258;

		// Token: 0x04008857 RID: 34903
		private static string[] attributeTagNames = new string[] { "prstMaterial" };

		// Token: 0x04008858 RID: 34904
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008859 RID: 34905
		private static readonly string[] eleTagNames;

		// Token: 0x0400885A RID: 34906
		private static readonly byte[] eleNamespaceIds;
	}
}
