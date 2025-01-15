using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A21 RID: 10785
	[ChildElementInfo(typeof(CommonMediaNode))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Video : OpenXmlCompositeElement
	{
		// Token: 0x17007066 RID: 28774
		// (get) Token: 0x06015A28 RID: 88616 RVA: 0x003217B6 File Offset: 0x0031F9B6
		public override string LocalName
		{
			get
			{
				return "video";
			}
		}

		// Token: 0x17007067 RID: 28775
		// (get) Token: 0x06015A29 RID: 88617 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007068 RID: 28776
		// (get) Token: 0x06015A2A RID: 88618 RVA: 0x003217BD File Offset: 0x0031F9BD
		internal override int ElementTypeId
		{
			get
			{
				return 12211;
			}
		}

		// Token: 0x06015A2B RID: 88619 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007069 RID: 28777
		// (get) Token: 0x06015A2C RID: 88620 RVA: 0x003217C4 File Offset: 0x0031F9C4
		internal override string[] AttributeTagNames
		{
			get
			{
				return Video.attributeTagNames;
			}
		}

		// Token: 0x1700706A RID: 28778
		// (get) Token: 0x06015A2D RID: 88621 RVA: 0x003217CB File Offset: 0x0031F9CB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Video.attributeNamespaceIds;
			}
		}

		// Token: 0x1700706B RID: 28779
		// (get) Token: 0x06015A2E RID: 88622 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015A2F RID: 88623 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "fullScrn")]
		public BooleanValue FullScreen
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015A30 RID: 88624 RVA: 0x00293ECF File Offset: 0x002920CF
		public Video()
		{
		}

		// Token: 0x06015A31 RID: 88625 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Video(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A32 RID: 88626 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Video(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A33 RID: 88627 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Video(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015A34 RID: 88628 RVA: 0x003216F9 File Offset: 0x0031F8F9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cMediaNode" == name)
			{
				return new CommonMediaNode();
			}
			return null;
		}

		// Token: 0x1700706C RID: 28780
		// (get) Token: 0x06015A35 RID: 88629 RVA: 0x003217D2 File Offset: 0x0031F9D2
		internal override string[] ElementTagNames
		{
			get
			{
				return Video.eleTagNames;
			}
		}

		// Token: 0x1700706D RID: 28781
		// (get) Token: 0x06015A36 RID: 88630 RVA: 0x003217D9 File Offset: 0x0031F9D9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Video.eleNamespaceIds;
			}
		}

		// Token: 0x1700706E RID: 28782
		// (get) Token: 0x06015A37 RID: 88631 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700706F RID: 28783
		// (get) Token: 0x06015A38 RID: 88632 RVA: 0x00321722 File Offset: 0x0031F922
		// (set) Token: 0x06015A39 RID: 88633 RVA: 0x0032172B File Offset: 0x0031F92B
		public CommonMediaNode CommonMediaNode
		{
			get
			{
				return base.GetElement<CommonMediaNode>(0);
			}
			set
			{
				base.SetElement<CommonMediaNode>(0, value);
			}
		}

		// Token: 0x06015A3A RID: 88634 RVA: 0x003217E0 File Offset: 0x0031F9E0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "fullScrn" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015A3B RID: 88635 RVA: 0x00321800 File Offset: 0x0031FA00
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Video>(deep);
		}

		// Token: 0x06015A3C RID: 88636 RVA: 0x0032180C File Offset: 0x0031FA0C
		// Note: this type is marked as 'beforefieldinit'.
		static Video()
		{
			byte[] array = new byte[1];
			Video.attributeNamespaceIds = array;
			Video.eleTagNames = new string[] { "cMediaNode" };
			Video.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x04009430 RID: 37936
		private const string tagName = "video";

		// Token: 0x04009431 RID: 37937
		private const byte tagNsId = 24;

		// Token: 0x04009432 RID: 37938
		internal const int ElementTypeIdConst = 12211;

		// Token: 0x04009433 RID: 37939
		private static string[] attributeTagNames = new string[] { "fullScrn" };

		// Token: 0x04009434 RID: 37940
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009435 RID: 37941
		private static readonly string[] eleTagNames;

		// Token: 0x04009436 RID: 37942
		private static readonly byte[] eleNamespaceIds;
	}
}
