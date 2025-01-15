using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A6D RID: 10861
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeTree))]
	[ChildElementInfo(typeof(Background))]
	[ChildElementInfo(typeof(CustomerDataList))]
	[ChildElementInfo(typeof(ControlList))]
	[ChildElementInfo(typeof(CommonSlideDataExtensionList))]
	internal class CommonSlideData : OpenXmlCompositeElement
	{
		// Token: 0x170072D2 RID: 29394
		// (get) Token: 0x06015F7C RID: 89980 RVA: 0x00324FD1 File Offset: 0x003231D1
		public override string LocalName
		{
			get
			{
				return "cSld";
			}
		}

		// Token: 0x170072D3 RID: 29395
		// (get) Token: 0x06015F7D RID: 89981 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170072D4 RID: 29396
		// (get) Token: 0x06015F7E RID: 89982 RVA: 0x00324FD8 File Offset: 0x003231D8
		internal override int ElementTypeId
		{
			get
			{
				return 12279;
			}
		}

		// Token: 0x06015F7F RID: 89983 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170072D5 RID: 29397
		// (get) Token: 0x06015F80 RID: 89984 RVA: 0x00324FDF File Offset: 0x003231DF
		internal override string[] AttributeTagNames
		{
			get
			{
				return CommonSlideData.attributeTagNames;
			}
		}

		// Token: 0x170072D6 RID: 29398
		// (get) Token: 0x06015F81 RID: 89985 RVA: 0x00324FE6 File Offset: 0x003231E6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CommonSlideData.attributeNamespaceIds;
			}
		}

		// Token: 0x170072D7 RID: 29399
		// (get) Token: 0x06015F82 RID: 89986 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015F83 RID: 89987 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x06015F84 RID: 89988 RVA: 0x00293ECF File Offset: 0x002920CF
		public CommonSlideData()
		{
		}

		// Token: 0x06015F85 RID: 89989 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CommonSlideData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F86 RID: 89990 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CommonSlideData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F87 RID: 89991 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CommonSlideData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015F88 RID: 89992 RVA: 0x00324FF0 File Offset: 0x003231F0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "bg" == name)
			{
				return new Background();
			}
			if (24 == namespaceId && "spTree" == name)
			{
				return new ShapeTree();
			}
			if (24 == namespaceId && "custDataLst" == name)
			{
				return new CustomerDataList();
			}
			if (24 == namespaceId && "controls" == name)
			{
				return new ControlList();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new CommonSlideDataExtensionList();
			}
			return null;
		}

		// Token: 0x170072D8 RID: 29400
		// (get) Token: 0x06015F89 RID: 89993 RVA: 0x00325076 File Offset: 0x00323276
		internal override string[] ElementTagNames
		{
			get
			{
				return CommonSlideData.eleTagNames;
			}
		}

		// Token: 0x170072D9 RID: 29401
		// (get) Token: 0x06015F8A RID: 89994 RVA: 0x0032507D File Offset: 0x0032327D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CommonSlideData.eleNamespaceIds;
			}
		}

		// Token: 0x170072DA RID: 29402
		// (get) Token: 0x06015F8B RID: 89995 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170072DB RID: 29403
		// (get) Token: 0x06015F8C RID: 89996 RVA: 0x00325084 File Offset: 0x00323284
		// (set) Token: 0x06015F8D RID: 89997 RVA: 0x0032508D File Offset: 0x0032328D
		public Background Background
		{
			get
			{
				return base.GetElement<Background>(0);
			}
			set
			{
				base.SetElement<Background>(0, value);
			}
		}

		// Token: 0x170072DC RID: 29404
		// (get) Token: 0x06015F8E RID: 89998 RVA: 0x00325097 File Offset: 0x00323297
		// (set) Token: 0x06015F8F RID: 89999 RVA: 0x003250A0 File Offset: 0x003232A0
		public ShapeTree ShapeTree
		{
			get
			{
				return base.GetElement<ShapeTree>(1);
			}
			set
			{
				base.SetElement<ShapeTree>(1, value);
			}
		}

		// Token: 0x170072DD RID: 29405
		// (get) Token: 0x06015F90 RID: 90000 RVA: 0x003250AA File Offset: 0x003232AA
		// (set) Token: 0x06015F91 RID: 90001 RVA: 0x003250B3 File Offset: 0x003232B3
		public CustomerDataList CustomerDataList
		{
			get
			{
				return base.GetElement<CustomerDataList>(2);
			}
			set
			{
				base.SetElement<CustomerDataList>(2, value);
			}
		}

		// Token: 0x170072DE RID: 29406
		// (get) Token: 0x06015F92 RID: 90002 RVA: 0x003250BD File Offset: 0x003232BD
		// (set) Token: 0x06015F93 RID: 90003 RVA: 0x003250C6 File Offset: 0x003232C6
		public ControlList ControlList
		{
			get
			{
				return base.GetElement<ControlList>(3);
			}
			set
			{
				base.SetElement<ControlList>(3, value);
			}
		}

		// Token: 0x170072DF RID: 29407
		// (get) Token: 0x06015F94 RID: 90004 RVA: 0x003250D0 File Offset: 0x003232D0
		// (set) Token: 0x06015F95 RID: 90005 RVA: 0x003250D9 File Offset: 0x003232D9
		public CommonSlideDataExtensionList CommonSlideDataExtensionList
		{
			get
			{
				return base.GetElement<CommonSlideDataExtensionList>(4);
			}
			set
			{
				base.SetElement<CommonSlideDataExtensionList>(4, value);
			}
		}

		// Token: 0x06015F96 RID: 90006 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015F97 RID: 90007 RVA: 0x003250E3 File Offset: 0x003232E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommonSlideData>(deep);
		}

		// Token: 0x06015F98 RID: 90008 RVA: 0x003250EC File Offset: 0x003232EC
		// Note: this type is marked as 'beforefieldinit'.
		static CommonSlideData()
		{
			byte[] array = new byte[1];
			CommonSlideData.attributeNamespaceIds = array;
			CommonSlideData.eleTagNames = new string[] { "bg", "spTree", "custDataLst", "controls", "extLst" };
			CommonSlideData.eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24 };
		}

		// Token: 0x0400959F RID: 38303
		private const string tagName = "cSld";

		// Token: 0x040095A0 RID: 38304
		private const byte tagNsId = 24;

		// Token: 0x040095A1 RID: 38305
		internal const int ElementTypeIdConst = 12279;

		// Token: 0x040095A2 RID: 38306
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x040095A3 RID: 38307
		private static byte[] attributeNamespaceIds;

		// Token: 0x040095A4 RID: 38308
		private static readonly string[] eleTagNames;

		// Token: 0x040095A5 RID: 38309
		private static readonly byte[] eleNamespaceIds;
	}
}
