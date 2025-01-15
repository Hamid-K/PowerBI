using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.EMMA;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003095 RID: 12437
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Emma))]
	internal class AnnotationXml : OpenXmlCompositeElement
	{
		// Token: 0x170097BE RID: 38846
		// (get) Token: 0x0601B0B3 RID: 110771 RVA: 0x0036B08B File Offset: 0x0036928B
		public override string LocalName
		{
			get
			{
				return "annotationXML";
			}
		}

		// Token: 0x170097BF RID: 38847
		// (get) Token: 0x0601B0B4 RID: 110772 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x170097C0 RID: 38848
		// (get) Token: 0x0601B0B5 RID: 110773 RVA: 0x0036B092 File Offset: 0x00369292
		internal override int ElementTypeId
		{
			get
			{
				return 12658;
			}
		}

		// Token: 0x0601B0B6 RID: 110774 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170097C1 RID: 38849
		// (get) Token: 0x0601B0B7 RID: 110775 RVA: 0x0036B099 File Offset: 0x00369299
		internal override string[] AttributeTagNames
		{
			get
			{
				return AnnotationXml.attributeTagNames;
			}
		}

		// Token: 0x170097C2 RID: 38850
		// (get) Token: 0x0601B0B8 RID: 110776 RVA: 0x0036B0A0 File Offset: 0x003692A0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AnnotationXml.attributeNamespaceIds;
			}
		}

		// Token: 0x170097C3 RID: 38851
		// (get) Token: 0x0601B0B9 RID: 110777 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B0BA RID: 110778 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public StringValue Type
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

		// Token: 0x170097C4 RID: 38852
		// (get) Token: 0x0601B0BB RID: 110779 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601B0BC RID: 110780 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "encoding")]
		public StringValue Encoding
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

		// Token: 0x170097C5 RID: 38853
		// (get) Token: 0x0601B0BD RID: 110781 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601B0BE RID: 110782 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "href")]
		public StringValue Href
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

		// Token: 0x0601B0BF RID: 110783 RVA: 0x00293ECF File Offset: 0x002920CF
		public AnnotationXml()
		{
		}

		// Token: 0x0601B0C0 RID: 110784 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AnnotationXml(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B0C1 RID: 110785 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AnnotationXml(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B0C2 RID: 110786 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AnnotationXml(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B0C3 RID: 110787 RVA: 0x0036B0A7 File Offset: 0x003692A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (44 == namespaceId && "emma" == name)
			{
				return new Emma();
			}
			return null;
		}

		// Token: 0x170097C6 RID: 38854
		// (get) Token: 0x0601B0C4 RID: 110788 RVA: 0x0036B0C2 File Offset: 0x003692C2
		internal override string[] ElementTagNames
		{
			get
			{
				return AnnotationXml.eleTagNames;
			}
		}

		// Token: 0x170097C7 RID: 38855
		// (get) Token: 0x0601B0C5 RID: 110789 RVA: 0x0036B0C9 File Offset: 0x003692C9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AnnotationXml.eleNamespaceIds;
			}
		}

		// Token: 0x170097C8 RID: 38856
		// (get) Token: 0x0601B0C6 RID: 110790 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170097C9 RID: 38857
		// (get) Token: 0x0601B0C7 RID: 110791 RVA: 0x0036B0D0 File Offset: 0x003692D0
		// (set) Token: 0x0601B0C8 RID: 110792 RVA: 0x0036B0D9 File Offset: 0x003692D9
		public Emma Emma
		{
			get
			{
				return base.GetElement<Emma>(0);
			}
			set
			{
				base.SetElement<Emma>(0, value);
			}
		}

		// Token: 0x0601B0C9 RID: 110793 RVA: 0x0036B0E4 File Offset: 0x003692E4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "encoding" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "href" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B0CA RID: 110794 RVA: 0x0036B13B File Offset: 0x0036933B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AnnotationXml>(deep);
		}

		// Token: 0x0601B0CB RID: 110795 RVA: 0x0036B144 File Offset: 0x00369344
		// Note: this type is marked as 'beforefieldinit'.
		static AnnotationXml()
		{
			byte[] array = new byte[3];
			AnnotationXml.attributeNamespaceIds = array;
			AnnotationXml.eleTagNames = new string[] { "emma" };
			AnnotationXml.eleNamespaceIds = new byte[] { 44 };
		}

		// Token: 0x0400B2B4 RID: 45748
		private const string tagName = "annotationXML";

		// Token: 0x0400B2B5 RID: 45749
		private const byte tagNsId = 43;

		// Token: 0x0400B2B6 RID: 45750
		internal const int ElementTypeIdConst = 12658;

		// Token: 0x0400B2B7 RID: 45751
		private static string[] attributeTagNames = new string[] { "type", "encoding", "href" };

		// Token: 0x0400B2B8 RID: 45752
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400B2B9 RID: 45753
		private static readonly string[] eleTagNames;

		// Token: 0x0400B2BA RID: 45754
		private static readonly byte[] eleNamespaceIds;
	}
}
