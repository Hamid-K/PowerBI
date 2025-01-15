using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A6A RID: 10858
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualGraphicFrameDrawingProperties))]
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingProperties))]
	internal class NonVisualGraphicFrameProperties : OpenXmlCompositeElement
	{
		// Token: 0x170072B4 RID: 29364
		// (get) Token: 0x06015F3B RID: 89915 RVA: 0x002FC745 File Offset: 0x002FA945
		public override string LocalName
		{
			get
			{
				return "nvGraphicFramePr";
			}
		}

		// Token: 0x170072B5 RID: 29365
		// (get) Token: 0x06015F3C RID: 89916 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170072B6 RID: 29366
		// (get) Token: 0x06015F3D RID: 89917 RVA: 0x00324DA9 File Offset: 0x00322FA9
		internal override int ElementTypeId
		{
			get
			{
				return 12276;
			}
		}

		// Token: 0x06015F3E RID: 89918 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015F3F RID: 89919 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGraphicFrameProperties()
		{
		}

		// Token: 0x06015F40 RID: 89920 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGraphicFrameProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F41 RID: 89921 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGraphicFrameProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F42 RID: 89922 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGraphicFrameProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015F43 RID: 89923 RVA: 0x00324DB0 File Offset: 0x00322FB0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (24 == namespaceId && "cNvGraphicFramePr" == name)
			{
				return new NonVisualGraphicFrameDrawingProperties();
			}
			if (24 == namespaceId && "nvPr" == name)
			{
				return new ApplicationNonVisualDrawingProperties();
			}
			return null;
		}

		// Token: 0x170072B7 RID: 29367
		// (get) Token: 0x06015F44 RID: 89924 RVA: 0x00324E06 File Offset: 0x00323006
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGraphicFrameProperties.eleTagNames;
			}
		}

		// Token: 0x170072B8 RID: 29368
		// (get) Token: 0x06015F45 RID: 89925 RVA: 0x00324E0D File Offset: 0x0032300D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGraphicFrameProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170072B9 RID: 29369
		// (get) Token: 0x06015F46 RID: 89926 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170072BA RID: 29370
		// (get) Token: 0x06015F47 RID: 89927 RVA: 0x00324478 File Offset: 0x00322678
		// (set) Token: 0x06015F48 RID: 89928 RVA: 0x00324481 File Offset: 0x00322681
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

		// Token: 0x170072BB RID: 29371
		// (get) Token: 0x06015F49 RID: 89929 RVA: 0x00324E14 File Offset: 0x00323014
		// (set) Token: 0x06015F4A RID: 89930 RVA: 0x00324E1D File Offset: 0x0032301D
		public NonVisualGraphicFrameDrawingProperties NonVisualGraphicFrameDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualGraphicFrameDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualGraphicFrameDrawingProperties>(1, value);
			}
		}

		// Token: 0x170072BC RID: 29372
		// (get) Token: 0x06015F4B RID: 89931 RVA: 0x0032449E File Offset: 0x0032269E
		// (set) Token: 0x06015F4C RID: 89932 RVA: 0x003244A7 File Offset: 0x003226A7
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

		// Token: 0x06015F4D RID: 89933 RVA: 0x00324E27 File Offset: 0x00323027
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGraphicFrameProperties>(deep);
		}

		// Token: 0x0400958E RID: 38286
		private const string tagName = "nvGraphicFramePr";

		// Token: 0x0400958F RID: 38287
		private const byte tagNsId = 24;

		// Token: 0x04009590 RID: 38288
		internal const int ElementTypeIdConst = 12276;

		// Token: 0x04009591 RID: 38289
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvGraphicFramePr", "nvPr" };

		// Token: 0x04009592 RID: 38290
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24 };
	}
}
