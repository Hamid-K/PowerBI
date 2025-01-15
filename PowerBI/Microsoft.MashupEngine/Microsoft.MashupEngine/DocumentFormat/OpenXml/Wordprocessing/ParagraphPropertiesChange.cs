using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E3C RID: 11836
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ParagraphPropertiesExtended))]
	internal class ParagraphPropertiesChange : OpenXmlCompositeElement
	{
		// Token: 0x170089A0 RID: 35232
		// (get) Token: 0x06019209 RID: 102921 RVA: 0x003468FD File Offset: 0x00344AFD
		public override string LocalName
		{
			get
			{
				return "pPrChange";
			}
		}

		// Token: 0x170089A1 RID: 35233
		// (get) Token: 0x0601920A RID: 102922 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170089A2 RID: 35234
		// (get) Token: 0x0601920B RID: 102923 RVA: 0x00346904 File Offset: 0x00344B04
		internal override int ElementTypeId
		{
			get
			{
				return 11523;
			}
		}

		// Token: 0x0601920C RID: 102924 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170089A3 RID: 35235
		// (get) Token: 0x0601920D RID: 102925 RVA: 0x0034690B File Offset: 0x00344B0B
		internal override string[] AttributeTagNames
		{
			get
			{
				return ParagraphPropertiesChange.attributeTagNames;
			}
		}

		// Token: 0x170089A4 RID: 35236
		// (get) Token: 0x0601920E RID: 102926 RVA: 0x00346912 File Offset: 0x00344B12
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ParagraphPropertiesChange.attributeNamespaceIds;
			}
		}

		// Token: 0x170089A5 RID: 35237
		// (get) Token: 0x0601920F RID: 102927 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019210 RID: 102928 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170089A6 RID: 35238
		// (get) Token: 0x06019211 RID: 102929 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x06019212 RID: 102930 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170089A7 RID: 35239
		// (get) Token: 0x06019213 RID: 102931 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06019214 RID: 102932 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x06019215 RID: 102933 RVA: 0x00293ECF File Offset: 0x002920CF
		public ParagraphPropertiesChange()
		{
		}

		// Token: 0x06019216 RID: 102934 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ParagraphPropertiesChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019217 RID: 102935 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ParagraphPropertiesChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019218 RID: 102936 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ParagraphPropertiesChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019219 RID: 102937 RVA: 0x00346919 File Offset: 0x00344B19
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "pPr" == name)
			{
				return new ParagraphPropertiesExtended();
			}
			return null;
		}

		// Token: 0x170089A8 RID: 35240
		// (get) Token: 0x0601921A RID: 102938 RVA: 0x00346934 File Offset: 0x00344B34
		internal override string[] ElementTagNames
		{
			get
			{
				return ParagraphPropertiesChange.eleTagNames;
			}
		}

		// Token: 0x170089A9 RID: 35241
		// (get) Token: 0x0601921B RID: 102939 RVA: 0x0034693B File Offset: 0x00344B3B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ParagraphPropertiesChange.eleNamespaceIds;
			}
		}

		// Token: 0x170089AA RID: 35242
		// (get) Token: 0x0601921C RID: 102940 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170089AB RID: 35243
		// (get) Token: 0x0601921D RID: 102941 RVA: 0x00346942 File Offset: 0x00344B42
		// (set) Token: 0x0601921E RID: 102942 RVA: 0x0034694B File Offset: 0x00344B4B
		public ParagraphPropertiesExtended ParagraphPropertiesExtended
		{
			get
			{
				return base.GetElement<ParagraphPropertiesExtended>(0);
			}
			set
			{
				base.SetElement<ParagraphPropertiesExtended>(0, value);
			}
		}

		// Token: 0x0601921F RID: 102943 RVA: 0x00346958 File Offset: 0x00344B58
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

		// Token: 0x06019220 RID: 102944 RVA: 0x003469B5 File Offset: 0x00344BB5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphPropertiesChange>(deep);
		}

		// Token: 0x0400A732 RID: 42802
		private const string tagName = "pPrChange";

		// Token: 0x0400A733 RID: 42803
		private const byte tagNsId = 23;

		// Token: 0x0400A734 RID: 42804
		internal const int ElementTypeIdConst = 11523;

		// Token: 0x0400A735 RID: 42805
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400A736 RID: 42806
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400A737 RID: 42807
		private static readonly string[] eleTagNames = new string[] { "pPr" };

		// Token: 0x0400A738 RID: 42808
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
