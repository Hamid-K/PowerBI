using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x02002394 RID: 9108
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MediaFade), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MediaBookmarkList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MediaTrim), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Media : OpenXmlCompositeElement
	{
		// Token: 0x17004BD4 RID: 19412
		// (get) Token: 0x060107A3 RID: 67491 RVA: 0x00295DCB File Offset: 0x00293FCB
		public override string LocalName
		{
			get
			{
				return "media";
			}
		}

		// Token: 0x17004BD5 RID: 19413
		// (get) Token: 0x060107A4 RID: 67492 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BD6 RID: 19414
		// (get) Token: 0x060107A5 RID: 67493 RVA: 0x002E3EB3 File Offset: 0x002E20B3
		internal override int ElementTypeId
		{
			get
			{
				return 12767;
			}
		}

		// Token: 0x060107A6 RID: 67494 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004BD7 RID: 19415
		// (get) Token: 0x060107A7 RID: 67495 RVA: 0x002E3EBA File Offset: 0x002E20BA
		internal override string[] AttributeTagNames
		{
			get
			{
				return Media.attributeTagNames;
			}
		}

		// Token: 0x17004BD8 RID: 19416
		// (get) Token: 0x060107A8 RID: 67496 RVA: 0x002E3EC1 File Offset: 0x002E20C1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Media.attributeNamespaceIds;
			}
		}

		// Token: 0x17004BD9 RID: 19417
		// (get) Token: 0x060107A9 RID: 67497 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060107AA RID: 67498 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "embed")]
		public StringValue Embed
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

		// Token: 0x17004BDA RID: 19418
		// (get) Token: 0x060107AB RID: 67499 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060107AC RID: 67500 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "link")]
		public StringValue Link
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

		// Token: 0x060107AD RID: 67501 RVA: 0x00293ECF File Offset: 0x002920CF
		public Media()
		{
		}

		// Token: 0x060107AE RID: 67502 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Media(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060107AF RID: 67503 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Media(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060107B0 RID: 67504 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Media(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060107B1 RID: 67505 RVA: 0x002E3EC8 File Offset: 0x002E20C8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "trim" == name)
			{
				return new MediaTrim();
			}
			if (49 == namespaceId && "fade" == name)
			{
				return new MediaFade();
			}
			if (49 == namespaceId && "bmkLst" == name)
			{
				return new MediaBookmarkList();
			}
			if (49 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004BDB RID: 19419
		// (get) Token: 0x060107B2 RID: 67506 RVA: 0x002E3F36 File Offset: 0x002E2136
		internal override string[] ElementTagNames
		{
			get
			{
				return Media.eleTagNames;
			}
		}

		// Token: 0x17004BDC RID: 19420
		// (get) Token: 0x060107B3 RID: 67507 RVA: 0x002E3F3D File Offset: 0x002E213D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Media.eleNamespaceIds;
			}
		}

		// Token: 0x17004BDD RID: 19421
		// (get) Token: 0x060107B4 RID: 67508 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004BDE RID: 19422
		// (get) Token: 0x060107B5 RID: 67509 RVA: 0x002E3F44 File Offset: 0x002E2144
		// (set) Token: 0x060107B6 RID: 67510 RVA: 0x002E3F4D File Offset: 0x002E214D
		public MediaTrim MediaTrim
		{
			get
			{
				return base.GetElement<MediaTrim>(0);
			}
			set
			{
				base.SetElement<MediaTrim>(0, value);
			}
		}

		// Token: 0x17004BDF RID: 19423
		// (get) Token: 0x060107B7 RID: 67511 RVA: 0x002E3F57 File Offset: 0x002E2157
		// (set) Token: 0x060107B8 RID: 67512 RVA: 0x002E3F60 File Offset: 0x002E2160
		public MediaFade MediaFade
		{
			get
			{
				return base.GetElement<MediaFade>(1);
			}
			set
			{
				base.SetElement<MediaFade>(1, value);
			}
		}

		// Token: 0x17004BE0 RID: 19424
		// (get) Token: 0x060107B9 RID: 67513 RVA: 0x002E3F6A File Offset: 0x002E216A
		// (set) Token: 0x060107BA RID: 67514 RVA: 0x002E3F73 File Offset: 0x002E2173
		public MediaBookmarkList MediaBookmarkList
		{
			get
			{
				return base.GetElement<MediaBookmarkList>(2);
			}
			set
			{
				base.SetElement<MediaBookmarkList>(2, value);
			}
		}

		// Token: 0x17004BE1 RID: 19425
		// (get) Token: 0x060107BB RID: 67515 RVA: 0x002E3F7D File Offset: 0x002E217D
		// (set) Token: 0x060107BC RID: 67516 RVA: 0x002E3F86 File Offset: 0x002E2186
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(3);
			}
			set
			{
				base.SetElement<ExtensionList>(3, value);
			}
		}

		// Token: 0x060107BD RID: 67517 RVA: 0x002E3F90 File Offset: 0x002E2190
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "embed" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "link" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060107BE RID: 67518 RVA: 0x002E3FCA File Offset: 0x002E21CA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Media>(deep);
		}

		// Token: 0x040074C6 RID: 29894
		private const string tagName = "media";

		// Token: 0x040074C7 RID: 29895
		private const byte tagNsId = 49;

		// Token: 0x040074C8 RID: 29896
		internal const int ElementTypeIdConst = 12767;

		// Token: 0x040074C9 RID: 29897
		private static string[] attributeTagNames = new string[] { "embed", "link" };

		// Token: 0x040074CA RID: 29898
		private static byte[] attributeNamespaceIds = new byte[] { 19, 19 };

		// Token: 0x040074CB RID: 29899
		private static readonly string[] eleTagNames = new string[] { "trim", "fade", "bmkLst", "extLst" };

		// Token: 0x040074CC RID: 29900
		private static readonly byte[] eleNamespaceIds = new byte[] { 49, 49, 49, 49 };
	}
}
