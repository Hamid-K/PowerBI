using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A77 RID: 10871
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SlideLayoutId : OpenXmlCompositeElement
	{
		// Token: 0x1700731C RID: 29468
		// (get) Token: 0x0601602A RID: 90154 RVA: 0x00325AB6 File Offset: 0x00323CB6
		public override string LocalName
		{
			get
			{
				return "sldLayoutId";
			}
		}

		// Token: 0x1700731D RID: 29469
		// (get) Token: 0x0601602B RID: 90155 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700731E RID: 29470
		// (get) Token: 0x0601602C RID: 90156 RVA: 0x00325ABD File Offset: 0x00323CBD
		internal override int ElementTypeId
		{
			get
			{
				return 12286;
			}
		}

		// Token: 0x0601602D RID: 90157 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700731F RID: 29471
		// (get) Token: 0x0601602E RID: 90158 RVA: 0x00325AC4 File Offset: 0x00323CC4
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlideLayoutId.attributeTagNames;
			}
		}

		// Token: 0x17007320 RID: 29472
		// (get) Token: 0x0601602F RID: 90159 RVA: 0x00325ACB File Offset: 0x00323CCB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlideLayoutId.attributeNamespaceIds;
			}
		}

		// Token: 0x17007321 RID: 29473
		// (get) Token: 0x06016030 RID: 90160 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016031 RID: 90161 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007322 RID: 29474
		// (get) Token: 0x06016032 RID: 90162 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016033 RID: 90163 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "id")]
		public StringValue RelationshipId
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

		// Token: 0x06016034 RID: 90164 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlideLayoutId()
		{
		}

		// Token: 0x06016035 RID: 90165 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlideLayoutId(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016036 RID: 90166 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlideLayoutId(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016037 RID: 90167 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlideLayoutId(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016038 RID: 90168 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007323 RID: 29475
		// (get) Token: 0x06016039 RID: 90169 RVA: 0x00325AD2 File Offset: 0x00323CD2
		internal override string[] ElementTagNames
		{
			get
			{
				return SlideLayoutId.eleTagNames;
			}
		}

		// Token: 0x17007324 RID: 29476
		// (get) Token: 0x0601603A RID: 90170 RVA: 0x00325AD9 File Offset: 0x00323CD9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SlideLayoutId.eleNamespaceIds;
			}
		}

		// Token: 0x17007325 RID: 29477
		// (get) Token: 0x0601603B RID: 90171 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007326 RID: 29478
		// (get) Token: 0x0601603C RID: 90172 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x0601603D RID: 90173 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x0601603E RID: 90174 RVA: 0x003239BB File Offset: 0x00321BBB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601603F RID: 90175 RVA: 0x00325AE0 File Offset: 0x00323CE0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideLayoutId>(deep);
		}

		// Token: 0x040095C8 RID: 38344
		private const string tagName = "sldLayoutId";

		// Token: 0x040095C9 RID: 38345
		private const byte tagNsId = 24;

		// Token: 0x040095CA RID: 38346
		internal const int ElementTypeIdConst = 12286;

		// Token: 0x040095CB RID: 38347
		private static string[] attributeTagNames = new string[] { "id", "id" };

		// Token: 0x040095CC RID: 38348
		private static byte[] attributeNamespaceIds = new byte[] { 0, 19 };

		// Token: 0x040095CD RID: 38349
		private static readonly string[] eleTagNames = new string[] { "extLst" };

		// Token: 0x040095CE RID: 38350
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
