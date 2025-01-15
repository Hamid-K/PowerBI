using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A70 RID: 10864
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	internal class HeaderFooter : OpenXmlCompositeElement
	{
		// Token: 0x170072F2 RID: 29426
		// (get) Token: 0x06015FC3 RID: 90051 RVA: 0x00325730 File Offset: 0x00323930
		public override string LocalName
		{
			get
			{
				return "hf";
			}
		}

		// Token: 0x170072F3 RID: 29427
		// (get) Token: 0x06015FC4 RID: 90052 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170072F4 RID: 29428
		// (get) Token: 0x06015FC5 RID: 90053 RVA: 0x00325737 File Offset: 0x00323937
		internal override int ElementTypeId
		{
			get
			{
				return 12282;
			}
		}

		// Token: 0x06015FC6 RID: 90054 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170072F5 RID: 29429
		// (get) Token: 0x06015FC7 RID: 90055 RVA: 0x0032573E File Offset: 0x0032393E
		internal override string[] AttributeTagNames
		{
			get
			{
				return HeaderFooter.attributeTagNames;
			}
		}

		// Token: 0x170072F6 RID: 29430
		// (get) Token: 0x06015FC8 RID: 90056 RVA: 0x00325745 File Offset: 0x00323945
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HeaderFooter.attributeNamespaceIds;
			}
		}

		// Token: 0x170072F7 RID: 29431
		// (get) Token: 0x06015FC9 RID: 90057 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015FCA RID: 90058 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "sldNum")]
		public BooleanValue SlideNumber
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

		// Token: 0x170072F8 RID: 29432
		// (get) Token: 0x06015FCB RID: 90059 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06015FCC RID: 90060 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "hdr")]
		public BooleanValue Header
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

		// Token: 0x170072F9 RID: 29433
		// (get) Token: 0x06015FCD RID: 90061 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06015FCE RID: 90062 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ftr")]
		public BooleanValue Footer
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170072FA RID: 29434
		// (get) Token: 0x06015FCF RID: 90063 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06015FD0 RID: 90064 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "dt")]
		public BooleanValue DateTime
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

		// Token: 0x06015FD1 RID: 90065 RVA: 0x00293ECF File Offset: 0x002920CF
		public HeaderFooter()
		{
		}

		// Token: 0x06015FD2 RID: 90066 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HeaderFooter(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015FD3 RID: 90067 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HeaderFooter(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015FD4 RID: 90068 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HeaderFooter(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015FD5 RID: 90069 RVA: 0x0032574C File Offset: 0x0032394C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x170072FB RID: 29435
		// (get) Token: 0x06015FD6 RID: 90070 RVA: 0x00325767 File Offset: 0x00323967
		internal override string[] ElementTagNames
		{
			get
			{
				return HeaderFooter.eleTagNames;
			}
		}

		// Token: 0x170072FC RID: 29436
		// (get) Token: 0x06015FD7 RID: 90071 RVA: 0x0032576E File Offset: 0x0032396E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HeaderFooter.eleNamespaceIds;
			}
		}

		// Token: 0x170072FD RID: 29437
		// (get) Token: 0x06015FD8 RID: 90072 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170072FE RID: 29438
		// (get) Token: 0x06015FD9 RID: 90073 RVA: 0x00325775 File Offset: 0x00323975
		// (set) Token: 0x06015FDA RID: 90074 RVA: 0x0032577E File Offset: 0x0032397E
		public ExtensionListWithModification ExtensionListWithModification
		{
			get
			{
				return base.GetElement<ExtensionListWithModification>(0);
			}
			set
			{
				base.SetElement<ExtensionListWithModification>(0, value);
			}
		}

		// Token: 0x06015FDB RID: 90075 RVA: 0x00325788 File Offset: 0x00323988
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "sldNum" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hdr" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ftr" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dt" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015FDC RID: 90076 RVA: 0x003257F5 File Offset: 0x003239F5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HeaderFooter>(deep);
		}

		// Token: 0x06015FDD RID: 90077 RVA: 0x00325800 File Offset: 0x00323A00
		// Note: this type is marked as 'beforefieldinit'.
		static HeaderFooter()
		{
			byte[] array = new byte[4];
			HeaderFooter.attributeNamespaceIds = array;
			HeaderFooter.eleTagNames = new string[] { "extLst" };
			HeaderFooter.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x040095B0 RID: 38320
		private const string tagName = "hf";

		// Token: 0x040095B1 RID: 38321
		private const byte tagNsId = 24;

		// Token: 0x040095B2 RID: 38322
		internal const int ElementTypeIdConst = 12282;

		// Token: 0x040095B3 RID: 38323
		private static string[] attributeTagNames = new string[] { "sldNum", "hdr", "ftr", "dt" };

		// Token: 0x040095B4 RID: 38324
		private static byte[] attributeNamespaceIds;

		// Token: 0x040095B5 RID: 38325
		private static readonly string[] eleTagNames;

		// Token: 0x040095B6 RID: 38326
		private static readonly byte[] eleNamespaceIds;
	}
}
