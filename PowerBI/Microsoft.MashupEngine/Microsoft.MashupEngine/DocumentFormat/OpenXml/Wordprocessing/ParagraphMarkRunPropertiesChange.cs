using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F46 RID: 12102
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PreviousParagraphMarkRunProperties))]
	internal class ParagraphMarkRunPropertiesChange : OpenXmlCompositeElement
	{
		// Token: 0x17008FDC RID: 36828
		// (get) Token: 0x06019F95 RID: 106389 RVA: 0x003494AC File Offset: 0x003476AC
		public override string LocalName
		{
			get
			{
				return "rPrChange";
			}
		}

		// Token: 0x17008FDD RID: 36829
		// (get) Token: 0x06019F96 RID: 106390 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FDE RID: 36830
		// (get) Token: 0x06019F97 RID: 106391 RVA: 0x0035A6C3 File Offset: 0x003588C3
		internal override int ElementTypeId
		{
			get
			{
				return 11749;
			}
		}

		// Token: 0x06019F98 RID: 106392 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008FDF RID: 36831
		// (get) Token: 0x06019F99 RID: 106393 RVA: 0x0035A6CA File Offset: 0x003588CA
		internal override string[] AttributeTagNames
		{
			get
			{
				return ParagraphMarkRunPropertiesChange.attributeTagNames;
			}
		}

		// Token: 0x17008FE0 RID: 36832
		// (get) Token: 0x06019F9A RID: 106394 RVA: 0x0035A6D1 File Offset: 0x003588D1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ParagraphMarkRunPropertiesChange.attributeNamespaceIds;
			}
		}

		// Token: 0x17008FE1 RID: 36833
		// (get) Token: 0x06019F9B RID: 106395 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019F9C RID: 106396 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008FE2 RID: 36834
		// (get) Token: 0x06019F9D RID: 106397 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x06019F9E RID: 106398 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17008FE3 RID: 36835
		// (get) Token: 0x06019F9F RID: 106399 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06019FA0 RID: 106400 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x06019FA1 RID: 106401 RVA: 0x00293ECF File Offset: 0x002920CF
		public ParagraphMarkRunPropertiesChange()
		{
		}

		// Token: 0x06019FA2 RID: 106402 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ParagraphMarkRunPropertiesChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019FA3 RID: 106403 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ParagraphMarkRunPropertiesChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019FA4 RID: 106404 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ParagraphMarkRunPropertiesChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019FA5 RID: 106405 RVA: 0x0035A6D8 File Offset: 0x003588D8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rPr" == name)
			{
				return new PreviousParagraphMarkRunProperties();
			}
			return null;
		}

		// Token: 0x17008FE4 RID: 36836
		// (get) Token: 0x06019FA6 RID: 106406 RVA: 0x0035A6F3 File Offset: 0x003588F3
		internal override string[] ElementTagNames
		{
			get
			{
				return ParagraphMarkRunPropertiesChange.eleTagNames;
			}
		}

		// Token: 0x17008FE5 RID: 36837
		// (get) Token: 0x06019FA7 RID: 106407 RVA: 0x0035A6FA File Offset: 0x003588FA
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ParagraphMarkRunPropertiesChange.eleNamespaceIds;
			}
		}

		// Token: 0x17008FE6 RID: 36838
		// (get) Token: 0x06019FA8 RID: 106408 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008FE7 RID: 36839
		// (get) Token: 0x06019FA9 RID: 106409 RVA: 0x0035A701 File Offset: 0x00358901
		// (set) Token: 0x06019FAA RID: 106410 RVA: 0x0035A70A File Offset: 0x0035890A
		public PreviousParagraphMarkRunProperties PreviousParagraphMarkRunProperties
		{
			get
			{
				return base.GetElement<PreviousParagraphMarkRunProperties>(0);
			}
			set
			{
				base.SetElement<PreviousParagraphMarkRunProperties>(0, value);
			}
		}

		// Token: 0x06019FAB RID: 106411 RVA: 0x0035A714 File Offset: 0x00358914
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

		// Token: 0x06019FAC RID: 106412 RVA: 0x0035A771 File Offset: 0x00358971
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphMarkRunPropertiesChange>(deep);
		}

		// Token: 0x0400AB32 RID: 43826
		private const string tagName = "rPrChange";

		// Token: 0x0400AB33 RID: 43827
		private const byte tagNsId = 23;

		// Token: 0x0400AB34 RID: 43828
		internal const int ElementTypeIdConst = 11749;

		// Token: 0x0400AB35 RID: 43829
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400AB36 RID: 43830
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400AB37 RID: 43831
		private static readonly string[] eleTagNames = new string[] { "rPr" };

		// Token: 0x0400AB38 RID: 43832
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
