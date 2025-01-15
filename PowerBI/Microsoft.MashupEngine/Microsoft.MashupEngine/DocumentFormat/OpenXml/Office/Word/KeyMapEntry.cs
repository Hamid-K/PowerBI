using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002467 RID: 9319
	[ChildElementInfo(typeof(MacroKeyboardCustomization))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FixedCommandKeyboardCustomization))]
	[ChildElementInfo(typeof(AllocatedCommandKeyboardCustomization))]
	[ChildElementInfo(typeof(WllMacroKeyboardCustomization))]
	[ChildElementInfo(typeof(CharacterInsertion))]
	internal class KeyMapEntry : OpenXmlCompositeElement
	{
		// Token: 0x170050CC RID: 20684
		// (get) Token: 0x060112BF RID: 70335 RVA: 0x002EB418 File Offset: 0x002E9618
		public override string LocalName
		{
			get
			{
				return "keymap";
			}
		}

		// Token: 0x170050CD RID: 20685
		// (get) Token: 0x060112C0 RID: 70336 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050CE RID: 20686
		// (get) Token: 0x060112C1 RID: 70337 RVA: 0x002EB41F File Offset: 0x002E961F
		internal override int ElementTypeId
		{
			get
			{
				return 12546;
			}
		}

		// Token: 0x060112C2 RID: 70338 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170050CF RID: 20687
		// (get) Token: 0x060112C3 RID: 70339 RVA: 0x002EB426 File Offset: 0x002E9626
		internal override string[] AttributeTagNames
		{
			get
			{
				return KeyMapEntry.attributeTagNames;
			}
		}

		// Token: 0x170050D0 RID: 20688
		// (get) Token: 0x060112C4 RID: 70340 RVA: 0x002EB42D File Offset: 0x002E962D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return KeyMapEntry.attributeNamespaceIds;
			}
		}

		// Token: 0x170050D1 RID: 20689
		// (get) Token: 0x060112C5 RID: 70341 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x060112C6 RID: 70342 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(33, "chmPrimary")]
		public HexBinaryValue CharacterMapPrimary
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170050D2 RID: 20690
		// (get) Token: 0x060112C7 RID: 70343 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x060112C8 RID: 70344 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(33, "chmSecondary")]
		public HexBinaryValue CharacterMapSecondary
		{
			get
			{
				return (HexBinaryValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170050D3 RID: 20691
		// (get) Token: 0x060112C9 RID: 70345 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x060112CA RID: 70346 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(33, "kcmPrimary")]
		public HexBinaryValue KeyCodePrimary
		{
			get
			{
				return (HexBinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170050D4 RID: 20692
		// (get) Token: 0x060112CB RID: 70347 RVA: 0x002EB434 File Offset: 0x002E9634
		// (set) Token: 0x060112CC RID: 70348 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(33, "kcmSecondary")]
		public HexBinaryValue KeyCodeSecondary
		{
			get
			{
				return (HexBinaryValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170050D5 RID: 20693
		// (get) Token: 0x060112CD RID: 70349 RVA: 0x002EB443 File Offset: 0x002E9643
		// (set) Token: 0x060112CE RID: 70350 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(33, "mask")]
		public OnOffValue Mask
		{
			get
			{
				return (OnOffValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x060112CF RID: 70351 RVA: 0x00293ECF File Offset: 0x002920CF
		public KeyMapEntry()
		{
		}

		// Token: 0x060112D0 RID: 70352 RVA: 0x00293ED7 File Offset: 0x002920D7
		public KeyMapEntry(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060112D1 RID: 70353 RVA: 0x00293EE0 File Offset: 0x002920E0
		public KeyMapEntry(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060112D2 RID: 70354 RVA: 0x00293EE9 File Offset: 0x002920E9
		public KeyMapEntry(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060112D3 RID: 70355 RVA: 0x002EB454 File Offset: 0x002E9654
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "fci" == name)
			{
				return new FixedCommandKeyboardCustomization();
			}
			if (33 == namespaceId && "macro" == name)
			{
				return new MacroKeyboardCustomization();
			}
			if (33 == namespaceId && "acd" == name)
			{
				return new AllocatedCommandKeyboardCustomization();
			}
			if (33 == namespaceId && "wll" == name)
			{
				return new WllMacroKeyboardCustomization();
			}
			if (33 == namespaceId && "wch" == name)
			{
				return new CharacterInsertion();
			}
			return null;
		}

		// Token: 0x170050D6 RID: 20694
		// (get) Token: 0x060112D4 RID: 70356 RVA: 0x002EB4DA File Offset: 0x002E96DA
		internal override string[] ElementTagNames
		{
			get
			{
				return KeyMapEntry.eleTagNames;
			}
		}

		// Token: 0x170050D7 RID: 20695
		// (get) Token: 0x060112D5 RID: 70357 RVA: 0x002EB4E1 File Offset: 0x002E96E1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return KeyMapEntry.eleNamespaceIds;
			}
		}

		// Token: 0x170050D8 RID: 20696
		// (get) Token: 0x060112D6 RID: 70358 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170050D9 RID: 20697
		// (get) Token: 0x060112D7 RID: 70359 RVA: 0x002EB4E8 File Offset: 0x002E96E8
		// (set) Token: 0x060112D8 RID: 70360 RVA: 0x002EB4F1 File Offset: 0x002E96F1
		public FixedCommandKeyboardCustomization FixedCommandKeyboardCustomization
		{
			get
			{
				return base.GetElement<FixedCommandKeyboardCustomization>(0);
			}
			set
			{
				base.SetElement<FixedCommandKeyboardCustomization>(0, value);
			}
		}

		// Token: 0x170050DA RID: 20698
		// (get) Token: 0x060112D9 RID: 70361 RVA: 0x002EB4FB File Offset: 0x002E96FB
		// (set) Token: 0x060112DA RID: 70362 RVA: 0x002EB504 File Offset: 0x002E9704
		public MacroKeyboardCustomization MacroKeyboardCustomization
		{
			get
			{
				return base.GetElement<MacroKeyboardCustomization>(1);
			}
			set
			{
				base.SetElement<MacroKeyboardCustomization>(1, value);
			}
		}

		// Token: 0x170050DB RID: 20699
		// (get) Token: 0x060112DB RID: 70363 RVA: 0x002EB50E File Offset: 0x002E970E
		// (set) Token: 0x060112DC RID: 70364 RVA: 0x002EB517 File Offset: 0x002E9717
		public AllocatedCommandKeyboardCustomization AllocatedCommandKeyboardCustomization
		{
			get
			{
				return base.GetElement<AllocatedCommandKeyboardCustomization>(2);
			}
			set
			{
				base.SetElement<AllocatedCommandKeyboardCustomization>(2, value);
			}
		}

		// Token: 0x170050DC RID: 20700
		// (get) Token: 0x060112DD RID: 70365 RVA: 0x002EB521 File Offset: 0x002E9721
		// (set) Token: 0x060112DE RID: 70366 RVA: 0x002EB52A File Offset: 0x002E972A
		public WllMacroKeyboardCustomization WllMacroKeyboardCustomization
		{
			get
			{
				return base.GetElement<WllMacroKeyboardCustomization>(3);
			}
			set
			{
				base.SetElement<WllMacroKeyboardCustomization>(3, value);
			}
		}

		// Token: 0x170050DD RID: 20701
		// (get) Token: 0x060112DF RID: 70367 RVA: 0x002EB534 File Offset: 0x002E9734
		// (set) Token: 0x060112E0 RID: 70368 RVA: 0x002EB53D File Offset: 0x002E973D
		public CharacterInsertion CharacterInsertion
		{
			get
			{
				return base.GetElement<CharacterInsertion>(4);
			}
			set
			{
				base.SetElement<CharacterInsertion>(4, value);
			}
		}

		// Token: 0x060112E1 RID: 70369 RVA: 0x002EB548 File Offset: 0x002E9748
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "chmPrimary" == name)
			{
				return new HexBinaryValue();
			}
			if (33 == namespaceId && "chmSecondary" == name)
			{
				return new HexBinaryValue();
			}
			if (33 == namespaceId && "kcmPrimary" == name)
			{
				return new HexBinaryValue();
			}
			if (33 == namespaceId && "kcmSecondary" == name)
			{
				return new HexBinaryValue();
			}
			if (33 == namespaceId && "mask" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060112E2 RID: 70370 RVA: 0x002EB5D5 File Offset: 0x002E97D5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<KeyMapEntry>(deep);
		}

		// Token: 0x04007881 RID: 30849
		private const string tagName = "keymap";

		// Token: 0x04007882 RID: 30850
		private const byte tagNsId = 33;

		// Token: 0x04007883 RID: 30851
		internal const int ElementTypeIdConst = 12546;

		// Token: 0x04007884 RID: 30852
		private static string[] attributeTagNames = new string[] { "chmPrimary", "chmSecondary", "kcmPrimary", "kcmSecondary", "mask" };

		// Token: 0x04007885 RID: 30853
		private static byte[] attributeNamespaceIds = new byte[] { 33, 33, 33, 33, 33 };

		// Token: 0x04007886 RID: 30854
		private static readonly string[] eleTagNames = new string[] { "fci", "macro", "acd", "wll", "wch" };

		// Token: 0x04007887 RID: 30855
		private static readonly byte[] eleNamespaceIds = new byte[] { 33, 33, 33, 33, 33 };
	}
}
