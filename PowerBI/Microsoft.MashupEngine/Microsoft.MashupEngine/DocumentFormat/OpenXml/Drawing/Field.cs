using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002762 RID: 10082
	[ChildElementInfo(typeof(ParagraphProperties))]
	[ChildElementInfo(typeof(Text))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RunProperties))]
	internal class Field : OpenXmlCompositeElement
	{
		// Token: 0x170060ED RID: 24813
		// (get) Token: 0x060136B0 RID: 79536 RVA: 0x00306C9C File Offset: 0x00304E9C
		public override string LocalName
		{
			get
			{
				return "fld";
			}
		}

		// Token: 0x170060EE RID: 24814
		// (get) Token: 0x060136B1 RID: 79537 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060EF RID: 24815
		// (get) Token: 0x060136B2 RID: 79538 RVA: 0x00306CA3 File Offset: 0x00304EA3
		internal override int ElementTypeId
		{
			get
			{
				return 10119;
			}
		}

		// Token: 0x060136B3 RID: 79539 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170060F0 RID: 24816
		// (get) Token: 0x060136B4 RID: 79540 RVA: 0x00306CAA File Offset: 0x00304EAA
		internal override string[] AttributeTagNames
		{
			get
			{
				return Field.attributeTagNames;
			}
		}

		// Token: 0x170060F1 RID: 24817
		// (get) Token: 0x060136B5 RID: 79541 RVA: 0x00306CB1 File Offset: 0x00304EB1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Field.attributeNamespaceIds;
			}
		}

		// Token: 0x170060F2 RID: 24818
		// (get) Token: 0x060136B6 RID: 79542 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060136B7 RID: 79543 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170060F3 RID: 24819
		// (get) Token: 0x060136B8 RID: 79544 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060136B9 RID: 79545 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public StringValue Type
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

		// Token: 0x060136BA RID: 79546 RVA: 0x00293ECF File Offset: 0x002920CF
		public Field()
		{
		}

		// Token: 0x060136BB RID: 79547 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Field(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060136BC RID: 79548 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Field(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060136BD RID: 79549 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Field(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060136BE RID: 79550 RVA: 0x00306CB8 File Offset: 0x00304EB8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
			if (10 == namespaceId && "pPr" == name)
			{
				return new ParagraphProperties();
			}
			if (10 == namespaceId && "t" == name)
			{
				return new Text();
			}
			return null;
		}

		// Token: 0x170060F4 RID: 24820
		// (get) Token: 0x060136BF RID: 79551 RVA: 0x00306D0E File Offset: 0x00304F0E
		internal override string[] ElementTagNames
		{
			get
			{
				return Field.eleTagNames;
			}
		}

		// Token: 0x170060F5 RID: 24821
		// (get) Token: 0x060136C0 RID: 79552 RVA: 0x00306D15 File Offset: 0x00304F15
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Field.eleNamespaceIds;
			}
		}

		// Token: 0x170060F6 RID: 24822
		// (get) Token: 0x060136C1 RID: 79553 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170060F7 RID: 24823
		// (get) Token: 0x060136C2 RID: 79554 RVA: 0x00306BB4 File Offset: 0x00304DB4
		// (set) Token: 0x060136C3 RID: 79555 RVA: 0x00306BBD File Offset: 0x00304DBD
		public RunProperties RunProperties
		{
			get
			{
				return base.GetElement<RunProperties>(0);
			}
			set
			{
				base.SetElement<RunProperties>(0, value);
			}
		}

		// Token: 0x170060F8 RID: 24824
		// (get) Token: 0x060136C4 RID: 79556 RVA: 0x00306D1C File Offset: 0x00304F1C
		// (set) Token: 0x060136C5 RID: 79557 RVA: 0x00306D25 File Offset: 0x00304F25
		public ParagraphProperties ParagraphProperties
		{
			get
			{
				return base.GetElement<ParagraphProperties>(1);
			}
			set
			{
				base.SetElement<ParagraphProperties>(1, value);
			}
		}

		// Token: 0x170060F9 RID: 24825
		// (get) Token: 0x060136C6 RID: 79558 RVA: 0x00306D2F File Offset: 0x00304F2F
		// (set) Token: 0x060136C7 RID: 79559 RVA: 0x00306D38 File Offset: 0x00304F38
		public Text Text
		{
			get
			{
				return base.GetElement<Text>(2);
			}
			set
			{
				base.SetElement<Text>(2, value);
			}
		}

		// Token: 0x060136C8 RID: 79560 RVA: 0x00306D42 File Offset: 0x00304F42
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060136C9 RID: 79561 RVA: 0x00306D78 File Offset: 0x00304F78
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Field>(deep);
		}

		// Token: 0x060136CA RID: 79562 RVA: 0x00306D84 File Offset: 0x00304F84
		// Note: this type is marked as 'beforefieldinit'.
		static Field()
		{
			byte[] array = new byte[2];
			Field.attributeNamespaceIds = array;
			Field.eleTagNames = new string[] { "rPr", "pPr", "t" };
			Field.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04008622 RID: 34338
		private const string tagName = "fld";

		// Token: 0x04008623 RID: 34339
		private const byte tagNsId = 10;

		// Token: 0x04008624 RID: 34340
		internal const int ElementTypeIdConst = 10119;

		// Token: 0x04008625 RID: 34341
		private static string[] attributeTagNames = new string[] { "id", "type" };

		// Token: 0x04008626 RID: 34342
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008627 RID: 34343
		private static readonly string[] eleTagNames;

		// Token: 0x04008628 RID: 34344
		private static readonly byte[] eleNamespaceIds;
	}
}
