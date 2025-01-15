using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingShape
{
	// Token: 0x02002501 RID: 9473
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	internal class LinkedTextBox : OpenXmlCompositeElement
	{
		// Token: 0x170053EB RID: 21483
		// (get) Token: 0x060119C4 RID: 72132 RVA: 0x002F0807 File Offset: 0x002EEA07
		public override string LocalName
		{
			get
			{
				return "linkedTxbx";
			}
		}

		// Token: 0x170053EC RID: 21484
		// (get) Token: 0x060119C5 RID: 72133 RVA: 0x002EFE53 File Offset: 0x002EE053
		internal override byte NamespaceId
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x170053ED RID: 21485
		// (get) Token: 0x060119C6 RID: 72134 RVA: 0x002F080E File Offset: 0x002EEA0E
		internal override int ElementTypeId
		{
			get
			{
				return 13139;
			}
		}

		// Token: 0x060119C7 RID: 72135 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170053EE RID: 21486
		// (get) Token: 0x060119C8 RID: 72136 RVA: 0x002F0815 File Offset: 0x002EEA15
		internal override string[] AttributeTagNames
		{
			get
			{
				return LinkedTextBox.attributeTagNames;
			}
		}

		// Token: 0x170053EF RID: 21487
		// (get) Token: 0x060119C9 RID: 72137 RVA: 0x002F081C File Offset: 0x002EEA1C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LinkedTextBox.attributeNamespaceIds;
			}
		}

		// Token: 0x170053F0 RID: 21488
		// (get) Token: 0x060119CA RID: 72138 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x060119CB RID: 72139 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt16Value Id
		{
			get
			{
				return (UInt16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170053F1 RID: 21489
		// (get) Token: 0x060119CC RID: 72140 RVA: 0x002F0823 File Offset: 0x002EEA23
		// (set) Token: 0x060119CD RID: 72141 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "seq")]
		public UInt16Value Sequence
		{
			get
			{
				return (UInt16Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060119CE RID: 72142 RVA: 0x00293ECF File Offset: 0x002920CF
		public LinkedTextBox()
		{
		}

		// Token: 0x060119CF RID: 72143 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LinkedTextBox(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060119D0 RID: 72144 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LinkedTextBox(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060119D1 RID: 72145 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LinkedTextBox(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060119D2 RID: 72146 RVA: 0x002F0832 File Offset: 0x002EEA32
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (61 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x170053F2 RID: 21490
		// (get) Token: 0x060119D3 RID: 72147 RVA: 0x002F084D File Offset: 0x002EEA4D
		internal override string[] ElementTagNames
		{
			get
			{
				return LinkedTextBox.eleTagNames;
			}
		}

		// Token: 0x170053F3 RID: 21491
		// (get) Token: 0x060119D4 RID: 72148 RVA: 0x002F0854 File Offset: 0x002EEA54
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LinkedTextBox.eleNamespaceIds;
			}
		}

		// Token: 0x170053F4 RID: 21492
		// (get) Token: 0x060119D5 RID: 72149 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170053F5 RID: 21493
		// (get) Token: 0x060119D6 RID: 72150 RVA: 0x002F085B File Offset: 0x002EEA5B
		// (set) Token: 0x060119D7 RID: 72151 RVA: 0x002F0864 File Offset: 0x002EEA64
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(0);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(0, value);
			}
		}

		// Token: 0x060119D8 RID: 72152 RVA: 0x002F086E File Offset: 0x002EEA6E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt16Value();
			}
			if (namespaceId == 0 && "seq" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060119D9 RID: 72153 RVA: 0x002F08A4 File Offset: 0x002EEAA4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LinkedTextBox>(deep);
		}

		// Token: 0x060119DA RID: 72154 RVA: 0x002F08B0 File Offset: 0x002EEAB0
		// Note: this type is marked as 'beforefieldinit'.
		static LinkedTextBox()
		{
			byte[] array = new byte[2];
			LinkedTextBox.attributeNamespaceIds = array;
			LinkedTextBox.eleTagNames = new string[] { "extLst" };
			LinkedTextBox.eleNamespaceIds = new byte[] { 61 };
		}

		// Token: 0x04007B89 RID: 31625
		private const string tagName = "linkedTxbx";

		// Token: 0x04007B8A RID: 31626
		private const byte tagNsId = 61;

		// Token: 0x04007B8B RID: 31627
		internal const int ElementTypeIdConst = 13139;

		// Token: 0x04007B8C RID: 31628
		private static string[] attributeTagNames = new string[] { "id", "seq" };

		// Token: 0x04007B8D RID: 31629
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B8E RID: 31630
		private static readonly string[] eleTagNames;

		// Token: 0x04007B8F RID: 31631
		private static readonly byte[] eleNamespaceIds;
	}
}
