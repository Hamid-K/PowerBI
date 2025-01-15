using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003002 RID: 12290
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PreviousTableRowProperties))]
	internal class TableRowPropertiesChange : OpenXmlCompositeElement
	{
		// Token: 0x170095F4 RID: 38388
		// (get) Token: 0x0601ACA9 RID: 109737 RVA: 0x00367AFB File Offset: 0x00365CFB
		public override string LocalName
		{
			get
			{
				return "trPrChange";
			}
		}

		// Token: 0x170095F5 RID: 38389
		// (get) Token: 0x0601ACAA RID: 109738 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095F6 RID: 38390
		// (get) Token: 0x0601ACAB RID: 109739 RVA: 0x00367B02 File Offset: 0x00365D02
		internal override int ElementTypeId
		{
			get
			{
				return 12133;
			}
		}

		// Token: 0x0601ACAC RID: 109740 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170095F7 RID: 38391
		// (get) Token: 0x0601ACAD RID: 109741 RVA: 0x00367B09 File Offset: 0x00365D09
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableRowPropertiesChange.attributeTagNames;
			}
		}

		// Token: 0x170095F8 RID: 38392
		// (get) Token: 0x0601ACAE RID: 109742 RVA: 0x00367B10 File Offset: 0x00365D10
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableRowPropertiesChange.attributeNamespaceIds;
			}
		}

		// Token: 0x170095F9 RID: 38393
		// (get) Token: 0x0601ACAF RID: 109743 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601ACB0 RID: 109744 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "author")]
		public StringValue Author
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

		// Token: 0x170095FA RID: 38394
		// (get) Token: 0x0601ACB1 RID: 109745 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x0601ACB2 RID: 109746 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "date")]
		public DateTimeValue Date
		{
			get
			{
				return (DateTimeValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170095FB RID: 38395
		// (get) Token: 0x0601ACB3 RID: 109747 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601ACB4 RID: 109748 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "id")]
		public StringValue Id
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

		// Token: 0x0601ACB5 RID: 109749 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableRowPropertiesChange()
		{
		}

		// Token: 0x0601ACB6 RID: 109750 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableRowPropertiesChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ACB7 RID: 109751 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableRowPropertiesChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ACB8 RID: 109752 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableRowPropertiesChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601ACB9 RID: 109753 RVA: 0x00367B17 File Offset: 0x00365D17
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "trPr" == name)
			{
				return new PreviousTableRowProperties();
			}
			return null;
		}

		// Token: 0x170095FC RID: 38396
		// (get) Token: 0x0601ACBA RID: 109754 RVA: 0x00367B32 File Offset: 0x00365D32
		internal override string[] ElementTagNames
		{
			get
			{
				return TableRowPropertiesChange.eleTagNames;
			}
		}

		// Token: 0x170095FD RID: 38397
		// (get) Token: 0x0601ACBB RID: 109755 RVA: 0x00367B39 File Offset: 0x00365D39
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableRowPropertiesChange.eleNamespaceIds;
			}
		}

		// Token: 0x170095FE RID: 38398
		// (get) Token: 0x0601ACBC RID: 109756 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170095FF RID: 38399
		// (get) Token: 0x0601ACBD RID: 109757 RVA: 0x00367B40 File Offset: 0x00365D40
		// (set) Token: 0x0601ACBE RID: 109758 RVA: 0x00367B49 File Offset: 0x00365D49
		public PreviousTableRowProperties PreviousTableRowProperties
		{
			get
			{
				return base.GetElement<PreviousTableRowProperties>(0);
			}
			set
			{
				base.SetElement<PreviousTableRowProperties>(0, value);
			}
		}

		// Token: 0x0601ACBF RID: 109759 RVA: 0x00367B54 File Offset: 0x00365D54
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "author" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "date" == name)
			{
				return new DateTimeValue();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601ACC0 RID: 109760 RVA: 0x00367BB1 File Offset: 0x00365DB1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableRowPropertiesChange>(deep);
		}

		// Token: 0x0400AE55 RID: 44629
		private const string tagName = "trPrChange";

		// Token: 0x0400AE56 RID: 44630
		private const byte tagNsId = 23;

		// Token: 0x0400AE57 RID: 44631
		internal const int ElementTypeIdConst = 12133;

		// Token: 0x0400AE58 RID: 44632
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400AE59 RID: 44633
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400AE5A RID: 44634
		private static readonly string[] eleTagNames = new string[] { "trPr" };

		// Token: 0x0400AE5B RID: 44635
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
