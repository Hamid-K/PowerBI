using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002346 RID: 9030
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualContentProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualContentPartProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Transform2D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	internal class GvmlContentPart : OpenXmlCompositeElement
	{
		// Token: 0x1700497C RID: 18812
		// (get) Token: 0x06010283 RID: 66179 RVA: 0x002DF99D File Offset: 0x002DDB9D
		public override string LocalName
		{
			get
			{
				return "contentPart";
			}
		}

		// Token: 0x1700497D RID: 18813
		// (get) Token: 0x06010284 RID: 66180 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x1700497E RID: 18814
		// (get) Token: 0x06010285 RID: 66181 RVA: 0x002E04DB File Offset: 0x002DE6DB
		internal override int ElementTypeId
		{
			get
			{
				return 12715;
			}
		}

		// Token: 0x06010286 RID: 66182 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700497F RID: 18815
		// (get) Token: 0x06010287 RID: 66183 RVA: 0x002E04E2 File Offset: 0x002DE6E2
		internal override string[] AttributeTagNames
		{
			get
			{
				return GvmlContentPart.attributeTagNames;
			}
		}

		// Token: 0x17004980 RID: 18816
		// (get) Token: 0x06010288 RID: 66184 RVA: 0x002E04E9 File Offset: 0x002DE6E9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GvmlContentPart.attributeNamespaceIds;
			}
		}

		// Token: 0x17004981 RID: 18817
		// (get) Token: 0x06010289 RID: 66185 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x0601028A RID: 66186 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "bwMode")]
		public EnumValue<BlackWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackWhiteModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004982 RID: 18818
		// (get) Token: 0x0601028B RID: 66187 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601028C RID: 66188 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x0601028D RID: 66189 RVA: 0x00293ECF File Offset: 0x002920CF
		public GvmlContentPart()
		{
		}

		// Token: 0x0601028E RID: 66190 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GvmlContentPart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601028F RID: 66191 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GvmlContentPart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010290 RID: 66192 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GvmlContentPart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010291 RID: 66193 RVA: 0x002E04F0 File Offset: 0x002DE6F0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "nvContentPr" == name)
			{
				return new NonVisualContentProperties();
			}
			if (48 == namespaceId && "nvContentPartPr" == name)
			{
				return new NonVisualContentPartProperties();
			}
			if (48 == namespaceId && "xfrm" == name)
			{
				return new Transform2D();
			}
			if (48 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x17004983 RID: 18819
		// (get) Token: 0x06010292 RID: 66194 RVA: 0x002E055E File Offset: 0x002DE75E
		internal override string[] ElementTagNames
		{
			get
			{
				return GvmlContentPart.eleTagNames;
			}
		}

		// Token: 0x17004984 RID: 18820
		// (get) Token: 0x06010293 RID: 66195 RVA: 0x002E0565 File Offset: 0x002DE765
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GvmlContentPart.eleNamespaceIds;
			}
		}

		// Token: 0x17004985 RID: 18821
		// (get) Token: 0x06010294 RID: 66196 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004986 RID: 18822
		// (get) Token: 0x06010295 RID: 66197 RVA: 0x002E056C File Offset: 0x002DE76C
		// (set) Token: 0x06010296 RID: 66198 RVA: 0x002E0575 File Offset: 0x002DE775
		public NonVisualContentProperties NonVisualContentProperties
		{
			get
			{
				return base.GetElement<NonVisualContentProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualContentProperties>(0, value);
			}
		}

		// Token: 0x17004987 RID: 18823
		// (get) Token: 0x06010297 RID: 66199 RVA: 0x002E057F File Offset: 0x002DE77F
		// (set) Token: 0x06010298 RID: 66200 RVA: 0x002E0588 File Offset: 0x002DE788
		public NonVisualContentPartProperties NonVisualContentPartProperties
		{
			get
			{
				return base.GetElement<NonVisualContentPartProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualContentPartProperties>(1, value);
			}
		}

		// Token: 0x17004988 RID: 18824
		// (get) Token: 0x06010299 RID: 66201 RVA: 0x002E0592 File Offset: 0x002DE792
		// (set) Token: 0x0601029A RID: 66202 RVA: 0x002E059B File Offset: 0x002DE79B
		public Transform2D Transform2D
		{
			get
			{
				return base.GetElement<Transform2D>(2);
			}
			set
			{
				base.SetElement<Transform2D>(2, value);
			}
		}

		// Token: 0x17004989 RID: 18825
		// (get) Token: 0x0601029B RID: 66203 RVA: 0x002E05A5 File Offset: 0x002DE7A5
		// (set) Token: 0x0601029C RID: 66204 RVA: 0x002E05AE File Offset: 0x002DE7AE
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(3);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(3, value);
			}
		}

		// Token: 0x0601029D RID: 66205 RVA: 0x002E05B8 File Offset: 0x002DE7B8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601029E RID: 66206 RVA: 0x002E05F0 File Offset: 0x002DE7F0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GvmlContentPart>(deep);
		}

		// Token: 0x04007357 RID: 29527
		private const string tagName = "contentPart";

		// Token: 0x04007358 RID: 29528
		private const byte tagNsId = 48;

		// Token: 0x04007359 RID: 29529
		internal const int ElementTypeIdConst = 12715;

		// Token: 0x0400735A RID: 29530
		private static string[] attributeTagNames = new string[] { "bwMode", "id" };

		// Token: 0x0400735B RID: 29531
		private static byte[] attributeNamespaceIds = new byte[] { 0, 19 };

		// Token: 0x0400735C RID: 29532
		private static readonly string[] eleTagNames = new string[] { "nvContentPr", "nvContentPartPr", "xfrm", "extLst" };

		// Token: 0x0400735D RID: 29533
		private static readonly byte[] eleNamespaceIds = new byte[] { 48, 48, 48, 48 };
	}
}
