using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A67 RID: 10855
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualPictureDrawingProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	internal class NonVisualPictureProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007297 RID: 29335
		// (get) Token: 0x06015EFC RID: 89852 RVA: 0x002FC4B3 File Offset: 0x002FA6B3
		public override string LocalName
		{
			get
			{
				return "nvPicPr";
			}
		}

		// Token: 0x17007298 RID: 29336
		// (get) Token: 0x06015EFD RID: 89853 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007299 RID: 29337
		// (get) Token: 0x06015EFE RID: 89854 RVA: 0x00324B5F File Offset: 0x00322D5F
		internal override int ElementTypeId
		{
			get
			{
				return 12273;
			}
		}

		// Token: 0x06015EFF RID: 89855 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015F00 RID: 89856 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPictureProperties()
		{
		}

		// Token: 0x06015F01 RID: 89857 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPictureProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F02 RID: 89858 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPictureProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F03 RID: 89859 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPictureProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015F04 RID: 89860 RVA: 0x00324B68 File Offset: 0x00322D68
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (24 == namespaceId && "cNvPicPr" == name)
			{
				return new NonVisualPictureDrawingProperties();
			}
			if (24 == namespaceId && "nvPr" == name)
			{
				return new ApplicationNonVisualDrawingProperties();
			}
			return null;
		}

		// Token: 0x1700729A RID: 29338
		// (get) Token: 0x06015F05 RID: 89861 RVA: 0x00324BBE File Offset: 0x00322DBE
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualPictureProperties.eleTagNames;
			}
		}

		// Token: 0x1700729B RID: 29339
		// (get) Token: 0x06015F06 RID: 89862 RVA: 0x00324BC5 File Offset: 0x00322DC5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualPictureProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700729C RID: 29340
		// (get) Token: 0x06015F07 RID: 89863 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700729D RID: 29341
		// (get) Token: 0x06015F08 RID: 89864 RVA: 0x00324478 File Offset: 0x00322678
		// (set) Token: 0x06015F09 RID: 89865 RVA: 0x00324481 File Offset: 0x00322681
		public NonVisualDrawingProperties NonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualDrawingProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualDrawingProperties>(0, value);
			}
		}

		// Token: 0x1700729E RID: 29342
		// (get) Token: 0x06015F0A RID: 89866 RVA: 0x00324BCC File Offset: 0x00322DCC
		// (set) Token: 0x06015F0B RID: 89867 RVA: 0x00324BD5 File Offset: 0x00322DD5
		public NonVisualPictureDrawingProperties NonVisualPictureDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualPictureDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualPictureDrawingProperties>(1, value);
			}
		}

		// Token: 0x1700729F RID: 29343
		// (get) Token: 0x06015F0C RID: 89868 RVA: 0x0032449E File Offset: 0x0032269E
		// (set) Token: 0x06015F0D RID: 89869 RVA: 0x003244A7 File Offset: 0x003226A7
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

		// Token: 0x06015F0E RID: 89870 RVA: 0x00324BDF File Offset: 0x00322DDF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPictureProperties>(deep);
		}

		// Token: 0x0400957D RID: 38269
		private const string tagName = "nvPicPr";

		// Token: 0x0400957E RID: 38270
		private const byte tagNsId = 24;

		// Token: 0x0400957F RID: 38271
		internal const int ElementTypeIdConst = 12273;

		// Token: 0x04009580 RID: 38272
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvPicPr", "nvPr" };

		// Token: 0x04009581 RID: 38273
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24 };
	}
}
