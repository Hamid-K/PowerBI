using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003009 RID: 12297
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DocPartGallery))]
	[ChildElementInfo(typeof(DocPartCategory))]
	[ChildElementInfo(typeof(DocPartUnique))]
	internal abstract class SdtDocPartType : OpenXmlCompositeElement
	{
		// Token: 0x0601AD6F RID: 109935 RVA: 0x00368548 File Offset: 0x00366748
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "docPartGallery" == name)
			{
				return new DocPartGallery();
			}
			if (23 == namespaceId && "docPartCategory" == name)
			{
				return new DocPartCategory();
			}
			if (23 == namespaceId && "docPartUnique" == name)
			{
				return new DocPartUnique();
			}
			return null;
		}

		// Token: 0x17009652 RID: 38482
		// (get) Token: 0x0601AD70 RID: 109936 RVA: 0x0036859E File Offset: 0x0036679E
		internal override string[] ElementTagNames
		{
			get
			{
				return SdtDocPartType.eleTagNames;
			}
		}

		// Token: 0x17009653 RID: 38483
		// (get) Token: 0x0601AD71 RID: 109937 RVA: 0x003685A5 File Offset: 0x003667A5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SdtDocPartType.eleNamespaceIds;
			}
		}

		// Token: 0x17009654 RID: 38484
		// (get) Token: 0x0601AD72 RID: 109938 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009655 RID: 38485
		// (get) Token: 0x0601AD73 RID: 109939 RVA: 0x003685AC File Offset: 0x003667AC
		// (set) Token: 0x0601AD74 RID: 109940 RVA: 0x003685B5 File Offset: 0x003667B5
		public DocPartGallery DocPartGallery
		{
			get
			{
				return base.GetElement<DocPartGallery>(0);
			}
			set
			{
				base.SetElement<DocPartGallery>(0, value);
			}
		}

		// Token: 0x17009656 RID: 38486
		// (get) Token: 0x0601AD75 RID: 109941 RVA: 0x003685BF File Offset: 0x003667BF
		// (set) Token: 0x0601AD76 RID: 109942 RVA: 0x003685C8 File Offset: 0x003667C8
		public DocPartCategory DocPartCategory
		{
			get
			{
				return base.GetElement<DocPartCategory>(1);
			}
			set
			{
				base.SetElement<DocPartCategory>(1, value);
			}
		}

		// Token: 0x17009657 RID: 38487
		// (get) Token: 0x0601AD77 RID: 109943 RVA: 0x003685D2 File Offset: 0x003667D2
		// (set) Token: 0x0601AD78 RID: 109944 RVA: 0x003685DB File Offset: 0x003667DB
		public DocPartUnique DocPartUnique
		{
			get
			{
				return base.GetElement<DocPartUnique>(2);
			}
			set
			{
				base.SetElement<DocPartUnique>(2, value);
			}
		}

		// Token: 0x0601AD79 RID: 109945 RVA: 0x00293ECF File Offset: 0x002920CF
		protected SdtDocPartType()
		{
		}

		// Token: 0x0601AD7A RID: 109946 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected SdtDocPartType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD7B RID: 109947 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected SdtDocPartType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD7C RID: 109948 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected SdtDocPartType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0400AE7C RID: 44668
		private static readonly string[] eleTagNames = new string[] { "docPartGallery", "docPartCategory", "docPartUnique" };

		// Token: 0x0400AE7D RID: 44669
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
