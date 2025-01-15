using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F61 RID: 12129
	[ChildElementInfo(typeof(PreviousTableProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TablePropertiesChange : OpenXmlCompositeElement
	{
		// Token: 0x17009084 RID: 36996
		// (get) Token: 0x0601A11B RID: 106779 RVA: 0x0035D196 File Offset: 0x0035B396
		public override string LocalName
		{
			get
			{
				return "tblPrChange";
			}
		}

		// Token: 0x17009085 RID: 36997
		// (get) Token: 0x0601A11C RID: 106780 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009086 RID: 36998
		// (get) Token: 0x0601A11D RID: 106781 RVA: 0x0035D19D File Offset: 0x0035B39D
		internal override int ElementTypeId
		{
			get
			{
				return 11787;
			}
		}

		// Token: 0x0601A11E RID: 106782 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009087 RID: 36999
		// (get) Token: 0x0601A11F RID: 106783 RVA: 0x0035D1A4 File Offset: 0x0035B3A4
		internal override string[] AttributeTagNames
		{
			get
			{
				return TablePropertiesChange.attributeTagNames;
			}
		}

		// Token: 0x17009088 RID: 37000
		// (get) Token: 0x0601A120 RID: 106784 RVA: 0x0035D1AB File Offset: 0x0035B3AB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TablePropertiesChange.attributeNamespaceIds;
			}
		}

		// Token: 0x17009089 RID: 37001
		// (get) Token: 0x0601A121 RID: 106785 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A122 RID: 106786 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700908A RID: 37002
		// (get) Token: 0x0601A123 RID: 106787 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x0601A124 RID: 106788 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700908B RID: 37003
		// (get) Token: 0x0601A125 RID: 106789 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601A126 RID: 106790 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x0601A127 RID: 106791 RVA: 0x00293ECF File Offset: 0x002920CF
		public TablePropertiesChange()
		{
		}

		// Token: 0x0601A128 RID: 106792 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TablePropertiesChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A129 RID: 106793 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TablePropertiesChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A12A RID: 106794 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TablePropertiesChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A12B RID: 106795 RVA: 0x0035D1B2 File Offset: 0x0035B3B2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "tblPr" == name)
			{
				return new PreviousTableProperties();
			}
			return null;
		}

		// Token: 0x1700908C RID: 37004
		// (get) Token: 0x0601A12C RID: 106796 RVA: 0x0035D1CD File Offset: 0x0035B3CD
		internal override string[] ElementTagNames
		{
			get
			{
				return TablePropertiesChange.eleTagNames;
			}
		}

		// Token: 0x1700908D RID: 37005
		// (get) Token: 0x0601A12D RID: 106797 RVA: 0x0035D1D4 File Offset: 0x0035B3D4
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TablePropertiesChange.eleNamespaceIds;
			}
		}

		// Token: 0x1700908E RID: 37006
		// (get) Token: 0x0601A12E RID: 106798 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700908F RID: 37007
		// (get) Token: 0x0601A12F RID: 106799 RVA: 0x0035D1DB File Offset: 0x0035B3DB
		// (set) Token: 0x0601A130 RID: 106800 RVA: 0x0035D1E4 File Offset: 0x0035B3E4
		public PreviousTableProperties PreviousTableProperties
		{
			get
			{
				return base.GetElement<PreviousTableProperties>(0);
			}
			set
			{
				base.SetElement<PreviousTableProperties>(0, value);
			}
		}

		// Token: 0x0601A131 RID: 106801 RVA: 0x0035D1F0 File Offset: 0x0035B3F0
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

		// Token: 0x0601A132 RID: 106802 RVA: 0x0035D24D File Offset: 0x0035B44D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TablePropertiesChange>(deep);
		}

		// Token: 0x0400ABA6 RID: 43942
		private const string tagName = "tblPrChange";

		// Token: 0x0400ABA7 RID: 43943
		private const byte tagNsId = 23;

		// Token: 0x0400ABA8 RID: 43944
		internal const int ElementTypeIdConst = 11787;

		// Token: 0x0400ABA9 RID: 43945
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400ABAA RID: 43946
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400ABAB RID: 43947
		private static readonly string[] eleTagNames = new string[] { "tblPr" };

		// Token: 0x0400ABAC RID: 43948
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
