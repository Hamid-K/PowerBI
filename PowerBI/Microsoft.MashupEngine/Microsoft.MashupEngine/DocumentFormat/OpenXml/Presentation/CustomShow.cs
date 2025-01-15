using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A5C RID: 10844
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SlideList))]
	internal class CustomShow : OpenXmlCompositeElement
	{
		// Token: 0x17007222 RID: 29218
		// (get) Token: 0x06015E02 RID: 89602 RVA: 0x0031E33C File Offset: 0x0031C53C
		public override string LocalName
		{
			get
			{
				return "custShow";
			}
		}

		// Token: 0x17007223 RID: 29219
		// (get) Token: 0x06015E03 RID: 89603 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007224 RID: 29220
		// (get) Token: 0x06015E04 RID: 89604 RVA: 0x00323F2A File Offset: 0x0032212A
		internal override int ElementTypeId
		{
			get
			{
				return 12262;
			}
		}

		// Token: 0x06015E05 RID: 89605 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007225 RID: 29221
		// (get) Token: 0x06015E06 RID: 89606 RVA: 0x00323F31 File Offset: 0x00322131
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomShow.attributeTagNames;
			}
		}

		// Token: 0x17007226 RID: 29222
		// (get) Token: 0x06015E07 RID: 89607 RVA: 0x00323F38 File Offset: 0x00322138
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomShow.attributeNamespaceIds;
			}
		}

		// Token: 0x17007227 RID: 29223
		// (get) Token: 0x06015E08 RID: 89608 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015E09 RID: 89609 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007228 RID: 29224
		// (get) Token: 0x06015E0A RID: 89610 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06015E0B RID: 89611 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "id")]
		public UInt32Value Id
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06015E0C RID: 89612 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomShow()
		{
		}

		// Token: 0x06015E0D RID: 89613 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomShow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E0E RID: 89614 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomShow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E0F RID: 89615 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomShow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015E10 RID: 89616 RVA: 0x00323F3F File Offset: 0x0032213F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "sldLst" == name)
			{
				return new SlideList();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007229 RID: 29225
		// (get) Token: 0x06015E11 RID: 89617 RVA: 0x00323F72 File Offset: 0x00322172
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomShow.eleTagNames;
			}
		}

		// Token: 0x1700722A RID: 29226
		// (get) Token: 0x06015E12 RID: 89618 RVA: 0x00323F79 File Offset: 0x00322179
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomShow.eleNamespaceIds;
			}
		}

		// Token: 0x1700722B RID: 29227
		// (get) Token: 0x06015E13 RID: 89619 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700722C RID: 29228
		// (get) Token: 0x06015E14 RID: 89620 RVA: 0x00323F80 File Offset: 0x00322180
		// (set) Token: 0x06015E15 RID: 89621 RVA: 0x00323F89 File Offset: 0x00322189
		public SlideList SlideList
		{
			get
			{
				return base.GetElement<SlideList>(0);
			}
			set
			{
				base.SetElement<SlideList>(0, value);
			}
		}

		// Token: 0x1700722D RID: 29229
		// (get) Token: 0x06015E16 RID: 89622 RVA: 0x00323F93 File Offset: 0x00322193
		// (set) Token: 0x06015E17 RID: 89623 RVA: 0x00323F9C File Offset: 0x0032219C
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06015E18 RID: 89624 RVA: 0x00323FA6 File Offset: 0x003221A6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015E19 RID: 89625 RVA: 0x00323FDC File Offset: 0x003221DC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomShow>(deep);
		}

		// Token: 0x06015E1A RID: 89626 RVA: 0x00323FE8 File Offset: 0x003221E8
		// Note: this type is marked as 'beforefieldinit'.
		static CustomShow()
		{
			byte[] array = new byte[2];
			CustomShow.attributeNamespaceIds = array;
			CustomShow.eleTagNames = new string[] { "sldLst", "extLst" };
			CustomShow.eleNamespaceIds = new byte[] { 24, 24 };
		}

		// Token: 0x0400953A RID: 38202
		private const string tagName = "custShow";

		// Token: 0x0400953B RID: 38203
		private const byte tagNsId = 24;

		// Token: 0x0400953C RID: 38204
		internal const int ElementTypeIdConst = 12262;

		// Token: 0x0400953D RID: 38205
		private static string[] attributeTagNames = new string[] { "name", "id" };

		// Token: 0x0400953E RID: 38206
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400953F RID: 38207
		private static readonly string[] eleTagNames;

		// Token: 0x04009540 RID: 38208
		private static readonly byte[] eleNamespaceIds;
	}
}
