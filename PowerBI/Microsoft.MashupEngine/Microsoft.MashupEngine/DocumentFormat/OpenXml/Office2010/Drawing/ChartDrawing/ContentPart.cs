using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing
{
	// Token: 0x02002335 RID: 9013
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualContentProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualContentPartProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Transform2D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	internal class ContentPart : OpenXmlCompositeElement
	{
		// Token: 0x170048FD RID: 18685
		// (get) Token: 0x0601016C RID: 65900 RVA: 0x002DF99D File Offset: 0x002DDB9D
		public override string LocalName
		{
			get
			{
				return "contentPart";
			}
		}

		// Token: 0x170048FE RID: 18686
		// (get) Token: 0x0601016D RID: 65901 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
		internal override byte NamespaceId
		{
			get
			{
				return 47;
			}
		}

		// Token: 0x170048FF RID: 18687
		// (get) Token: 0x0601016E RID: 65902 RVA: 0x002DF9A8 File Offset: 0x002DDBA8
		internal override int ElementTypeId
		{
			get
			{
				return 12703;
			}
		}

		// Token: 0x0601016F RID: 65903 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004900 RID: 18688
		// (get) Token: 0x06010170 RID: 65904 RVA: 0x002DF9AF File Offset: 0x002DDBAF
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContentPart.attributeTagNames;
			}
		}

		// Token: 0x17004901 RID: 18689
		// (get) Token: 0x06010171 RID: 65905 RVA: 0x002DF9B6 File Offset: 0x002DDBB6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContentPart.attributeNamespaceIds;
			}
		}

		// Token: 0x17004902 RID: 18690
		// (get) Token: 0x06010172 RID: 65906 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010173 RID: 65907 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue RelationshipId
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

		// Token: 0x17004903 RID: 18691
		// (get) Token: 0x06010174 RID: 65908 RVA: 0x002DF9BD File Offset: 0x002DDBBD
		// (set) Token: 0x06010175 RID: 65909 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "bwMode")]
		public EnumValue<BlackWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackWhiteModeValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010176 RID: 65910 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContentPart()
		{
		}

		// Token: 0x06010177 RID: 65911 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContentPart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010178 RID: 65912 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContentPart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010179 RID: 65913 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContentPart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601017A RID: 65914 RVA: 0x002DF9CC File Offset: 0x002DDBCC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (47 == namespaceId && "nvContentPr" == name)
			{
				return new NonVisualContentProperties();
			}
			if (47 == namespaceId && "nvContentPartPr" == name)
			{
				return new NonVisualContentPartProperties();
			}
			if (47 == namespaceId && "nvPr" == name)
			{
				return new ApplicationNonVisualDrawingProperties();
			}
			if (47 == namespaceId && "xfrm" == name)
			{
				return new Transform2D();
			}
			if (47 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x17004904 RID: 18692
		// (get) Token: 0x0601017B RID: 65915 RVA: 0x002DFA52 File Offset: 0x002DDC52
		internal override string[] ElementTagNames
		{
			get
			{
				return ContentPart.eleTagNames;
			}
		}

		// Token: 0x17004905 RID: 18693
		// (get) Token: 0x0601017C RID: 65916 RVA: 0x002DFA59 File Offset: 0x002DDC59
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ContentPart.eleNamespaceIds;
			}
		}

		// Token: 0x17004906 RID: 18694
		// (get) Token: 0x0601017D RID: 65917 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004907 RID: 18695
		// (get) Token: 0x0601017E RID: 65918 RVA: 0x002DFA60 File Offset: 0x002DDC60
		// (set) Token: 0x0601017F RID: 65919 RVA: 0x002DFA69 File Offset: 0x002DDC69
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

		// Token: 0x17004908 RID: 18696
		// (get) Token: 0x06010180 RID: 65920 RVA: 0x002DFA73 File Offset: 0x002DDC73
		// (set) Token: 0x06010181 RID: 65921 RVA: 0x002DFA7C File Offset: 0x002DDC7C
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

		// Token: 0x17004909 RID: 18697
		// (get) Token: 0x06010182 RID: 65922 RVA: 0x002DFA86 File Offset: 0x002DDC86
		// (set) Token: 0x06010183 RID: 65923 RVA: 0x002DFA8F File Offset: 0x002DDC8F
		public ApplicationNonVisualDrawingProperties ApplicationNonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<ApplicationNonVisualDrawingProperties>(2);
			}
			set
			{
				base.SetElement<ApplicationNonVisualDrawingProperties>(2, value);
			}
		}

		// Token: 0x1700490A RID: 18698
		// (get) Token: 0x06010184 RID: 65924 RVA: 0x002DFA99 File Offset: 0x002DDC99
		// (set) Token: 0x06010185 RID: 65925 RVA: 0x002DFAA2 File Offset: 0x002DDCA2
		public Transform2D Transform2D
		{
			get
			{
				return base.GetElement<Transform2D>(3);
			}
			set
			{
				base.SetElement<Transform2D>(3, value);
			}
		}

		// Token: 0x1700490B RID: 18699
		// (get) Token: 0x06010186 RID: 65926 RVA: 0x002DFAAC File Offset: 0x002DDCAC
		// (set) Token: 0x06010187 RID: 65927 RVA: 0x002DFAB5 File Offset: 0x002DDCB5
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(4);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(4, value);
			}
		}

		// Token: 0x06010188 RID: 65928 RVA: 0x002DFABF File Offset: 0x002DDCBF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010189 RID: 65929 RVA: 0x002DFAF7 File Offset: 0x002DDCF7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContentPart>(deep);
		}

		// Token: 0x0601018A RID: 65930 RVA: 0x002DFB00 File Offset: 0x002DDD00
		// Note: this type is marked as 'beforefieldinit'.
		static ContentPart()
		{
			byte[] array = new byte[2];
			array[0] = 19;
			ContentPart.attributeNamespaceIds = array;
			ContentPart.eleTagNames = new string[] { "nvContentPr", "nvContentPartPr", "nvPr", "xfrm", "extLst" };
			ContentPart.eleNamespaceIds = new byte[] { 47, 47, 47, 47, 47 };
		}

		// Token: 0x04007308 RID: 29448
		private const string tagName = "contentPart";

		// Token: 0x04007309 RID: 29449
		private const byte tagNsId = 47;

		// Token: 0x0400730A RID: 29450
		internal const int ElementTypeIdConst = 12703;

		// Token: 0x0400730B RID: 29451
		private static string[] attributeTagNames = new string[] { "id", "bwMode" };

		// Token: 0x0400730C RID: 29452
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400730D RID: 29453
		private static readonly string[] eleTagNames;

		// Token: 0x0400730E RID: 29454
		private static readonly byte[] eleNamespaceIds;
	}
}
