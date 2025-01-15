using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C20 RID: 11296
	[ChildElementInfo(typeof(Xstring))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExternalCell : OpenXmlCompositeElement
	{
		// Token: 0x17008062 RID: 32866
		// (get) Token: 0x06017D78 RID: 97656 RVA: 0x0033BBD3 File Offset: 0x00339DD3
		public override string LocalName
		{
			get
			{
				return "cell";
			}
		}

		// Token: 0x17008063 RID: 32867
		// (get) Token: 0x06017D79 RID: 97657 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008064 RID: 32868
		// (get) Token: 0x06017D7A RID: 97658 RVA: 0x0033BBDA File Offset: 0x00339DDA
		internal override int ElementTypeId
		{
			get
			{
				return 11277;
			}
		}

		// Token: 0x06017D7B RID: 97659 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008065 RID: 32869
		// (get) Token: 0x06017D7C RID: 97660 RVA: 0x0033BBE1 File Offset: 0x00339DE1
		internal override string[] AttributeTagNames
		{
			get
			{
				return ExternalCell.attributeTagNames;
			}
		}

		// Token: 0x17008066 RID: 32870
		// (get) Token: 0x06017D7D RID: 97661 RVA: 0x0033BBE8 File Offset: 0x00339DE8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ExternalCell.attributeNamespaceIds;
			}
		}

		// Token: 0x17008067 RID: 32871
		// (get) Token: 0x06017D7E RID: 97662 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017D7F RID: 97663 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "r")]
		public StringValue CellReference
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

		// Token: 0x17008068 RID: 32872
		// (get) Token: 0x06017D80 RID: 97664 RVA: 0x0033BBEF File Offset: 0x00339DEF
		// (set) Token: 0x06017D81 RID: 97665 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "t")]
		public EnumValue<CellValues> DataType
		{
			get
			{
				return (EnumValue<CellValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008069 RID: 32873
		// (get) Token: 0x06017D82 RID: 97666 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06017D83 RID: 97667 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "vm")]
		public UInt32Value ValueMetaIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06017D84 RID: 97668 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExternalCell()
		{
		}

		// Token: 0x06017D85 RID: 97669 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExternalCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D86 RID: 97670 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExternalCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D87 RID: 97671 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExternalCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017D88 RID: 97672 RVA: 0x0033BBFE File Offset: 0x00339DFE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "v" == name)
			{
				return new Xstring();
			}
			return null;
		}

		// Token: 0x1700806A RID: 32874
		// (get) Token: 0x06017D89 RID: 97673 RVA: 0x0033BC19 File Offset: 0x00339E19
		internal override string[] ElementTagNames
		{
			get
			{
				return ExternalCell.eleTagNames;
			}
		}

		// Token: 0x1700806B RID: 32875
		// (get) Token: 0x06017D8A RID: 97674 RVA: 0x0033BC20 File Offset: 0x00339E20
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ExternalCell.eleNamespaceIds;
			}
		}

		// Token: 0x1700806C RID: 32876
		// (get) Token: 0x06017D8B RID: 97675 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700806D RID: 32877
		// (get) Token: 0x06017D8C RID: 97676 RVA: 0x0033BC27 File Offset: 0x00339E27
		// (set) Token: 0x06017D8D RID: 97677 RVA: 0x0033BC30 File Offset: 0x00339E30
		public Xstring Xstring
		{
			get
			{
				return base.GetElement<Xstring>(0);
			}
			set
			{
				base.SetElement<Xstring>(0, value);
			}
		}

		// Token: 0x06017D8E RID: 97678 RVA: 0x0033BC3C File Offset: 0x00339E3C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "t" == name)
			{
				return new EnumValue<CellValues>();
			}
			if (namespaceId == 0 && "vm" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017D8F RID: 97679 RVA: 0x0033BC93 File Offset: 0x00339E93
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExternalCell>(deep);
		}

		// Token: 0x06017D90 RID: 97680 RVA: 0x0033BC9C File Offset: 0x00339E9C
		// Note: this type is marked as 'beforefieldinit'.
		static ExternalCell()
		{
			byte[] array = new byte[3];
			ExternalCell.attributeNamespaceIds = array;
			ExternalCell.eleTagNames = new string[] { "v" };
			ExternalCell.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009DCD RID: 40397
		private const string tagName = "cell";

		// Token: 0x04009DCE RID: 40398
		private const byte tagNsId = 22;

		// Token: 0x04009DCF RID: 40399
		internal const int ElementTypeIdConst = 11277;

		// Token: 0x04009DD0 RID: 40400
		private static string[] attributeTagNames = new string[] { "r", "t", "vm" };

		// Token: 0x04009DD1 RID: 40401
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009DD2 RID: 40402
		private static readonly string[] eleTagNames;

		// Token: 0x04009DD3 RID: 40403
		private static readonly byte[] eleNamespaceIds;
	}
}
