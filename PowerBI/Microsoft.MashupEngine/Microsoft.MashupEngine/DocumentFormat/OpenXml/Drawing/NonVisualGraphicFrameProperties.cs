using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027AF RID: 10159
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualGraphicFrameDrawingProperties))]
	internal class NonVisualGraphicFrameProperties : OpenXmlCompositeElement
	{
		// Token: 0x170062FA RID: 25338
		// (get) Token: 0x06013B56 RID: 80726 RVA: 0x002FC745 File Offset: 0x002FA945
		public override string LocalName
		{
			get
			{
				return "nvGraphicFramePr";
			}
		}

		// Token: 0x170062FB RID: 25339
		// (get) Token: 0x06013B57 RID: 80727 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170062FC RID: 25340
		// (get) Token: 0x06013B58 RID: 80728 RVA: 0x0030AF0D File Offset: 0x0030910D
		internal override int ElementTypeId
		{
			get
			{
				return 10192;
			}
		}

		// Token: 0x06013B59 RID: 80729 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013B5A RID: 80730 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGraphicFrameProperties()
		{
		}

		// Token: 0x06013B5B RID: 80731 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGraphicFrameProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B5C RID: 80732 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGraphicFrameProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B5D RID: 80733 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGraphicFrameProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013B5E RID: 80734 RVA: 0x0030AF14 File Offset: 0x00309114
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (10 == namespaceId && "cNvGraphicFramePr" == name)
			{
				return new NonVisualGraphicFrameDrawingProperties();
			}
			return null;
		}

		// Token: 0x170062FD RID: 25341
		// (get) Token: 0x06013B5F RID: 80735 RVA: 0x0030AF47 File Offset: 0x00309147
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGraphicFrameProperties.eleTagNames;
			}
		}

		// Token: 0x170062FE RID: 25342
		// (get) Token: 0x06013B60 RID: 80736 RVA: 0x0030AF4E File Offset: 0x0030914E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGraphicFrameProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170062FF RID: 25343
		// (get) Token: 0x06013B61 RID: 80737 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006300 RID: 25344
		// (get) Token: 0x06013B62 RID: 80738 RVA: 0x0030A72F File Offset: 0x0030892F
		// (set) Token: 0x06013B63 RID: 80739 RVA: 0x0030A738 File Offset: 0x00308938
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

		// Token: 0x17006301 RID: 25345
		// (get) Token: 0x06013B64 RID: 80740 RVA: 0x0030AF55 File Offset: 0x00309155
		// (set) Token: 0x06013B65 RID: 80741 RVA: 0x0030AF5E File Offset: 0x0030915E
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

		// Token: 0x06013B66 RID: 80742 RVA: 0x0030AF68 File Offset: 0x00309168
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGraphicFrameProperties>(deep);
		}

		// Token: 0x04008765 RID: 34661
		private const string tagName = "nvGraphicFramePr";

		// Token: 0x04008766 RID: 34662
		private const byte tagNsId = 10;

		// Token: 0x04008767 RID: 34663
		internal const int ElementTypeIdConst = 10192;

		// Token: 0x04008768 RID: 34664
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvGraphicFramePr" };

		// Token: 0x04008769 RID: 34665
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
