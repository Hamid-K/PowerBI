using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F45 RID: 12101
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PreviousSectionProperties))]
	internal class SectionPropertiesChange : OpenXmlCompositeElement
	{
		// Token: 0x17008FD0 RID: 36816
		// (get) Token: 0x06019F7C RID: 106364 RVA: 0x0035A591 File Offset: 0x00358791
		public override string LocalName
		{
			get
			{
				return "sectPrChange";
			}
		}

		// Token: 0x17008FD1 RID: 36817
		// (get) Token: 0x06019F7D RID: 106365 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FD2 RID: 36818
		// (get) Token: 0x06019F7E RID: 106366 RVA: 0x0035A598 File Offset: 0x00358798
		internal override int ElementTypeId
		{
			get
			{
				return 11748;
			}
		}

		// Token: 0x06019F7F RID: 106367 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008FD3 RID: 36819
		// (get) Token: 0x06019F80 RID: 106368 RVA: 0x0035A59F File Offset: 0x0035879F
		internal override string[] AttributeTagNames
		{
			get
			{
				return SectionPropertiesChange.attributeTagNames;
			}
		}

		// Token: 0x17008FD4 RID: 36820
		// (get) Token: 0x06019F81 RID: 106369 RVA: 0x0035A5A6 File Offset: 0x003587A6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SectionPropertiesChange.attributeNamespaceIds;
			}
		}

		// Token: 0x17008FD5 RID: 36821
		// (get) Token: 0x06019F82 RID: 106370 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019F83 RID: 106371 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008FD6 RID: 36822
		// (get) Token: 0x06019F84 RID: 106372 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x06019F85 RID: 106373 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17008FD7 RID: 36823
		// (get) Token: 0x06019F86 RID: 106374 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06019F87 RID: 106375 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x06019F88 RID: 106376 RVA: 0x00293ECF File Offset: 0x002920CF
		public SectionPropertiesChange()
		{
		}

		// Token: 0x06019F89 RID: 106377 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SectionPropertiesChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019F8A RID: 106378 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SectionPropertiesChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019F8B RID: 106379 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SectionPropertiesChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019F8C RID: 106380 RVA: 0x0035A5AD File Offset: 0x003587AD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "sectPr" == name)
			{
				return new PreviousSectionProperties();
			}
			return null;
		}

		// Token: 0x17008FD8 RID: 36824
		// (get) Token: 0x06019F8D RID: 106381 RVA: 0x0035A5C8 File Offset: 0x003587C8
		internal override string[] ElementTagNames
		{
			get
			{
				return SectionPropertiesChange.eleTagNames;
			}
		}

		// Token: 0x17008FD9 RID: 36825
		// (get) Token: 0x06019F8E RID: 106382 RVA: 0x0035A5CF File Offset: 0x003587CF
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SectionPropertiesChange.eleNamespaceIds;
			}
		}

		// Token: 0x17008FDA RID: 36826
		// (get) Token: 0x06019F8F RID: 106383 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008FDB RID: 36827
		// (get) Token: 0x06019F90 RID: 106384 RVA: 0x0035A5D6 File Offset: 0x003587D6
		// (set) Token: 0x06019F91 RID: 106385 RVA: 0x0035A5DF File Offset: 0x003587DF
		public PreviousSectionProperties PreviousSectionProperties
		{
			get
			{
				return base.GetElement<PreviousSectionProperties>(0);
			}
			set
			{
				base.SetElement<PreviousSectionProperties>(0, value);
			}
		}

		// Token: 0x06019F92 RID: 106386 RVA: 0x0035A5EC File Offset: 0x003587EC
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

		// Token: 0x06019F93 RID: 106387 RVA: 0x0035A649 File Offset: 0x00358849
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SectionPropertiesChange>(deep);
		}

		// Token: 0x0400AB2B RID: 43819
		private const string tagName = "sectPrChange";

		// Token: 0x0400AB2C RID: 43820
		private const byte tagNsId = 23;

		// Token: 0x0400AB2D RID: 43821
		internal const int ElementTypeIdConst = 11748;

		// Token: 0x0400AB2E RID: 43822
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400AB2F RID: 43823
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400AB30 RID: 43824
		private static readonly string[] eleTagNames = new string[] { "sectPr" };

		// Token: 0x0400AB31 RID: 43825
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
