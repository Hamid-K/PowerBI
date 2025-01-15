using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027AD RID: 10157
	[ChildElementInfo(typeof(NonVisualPictureDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualPictureProperties : OpenXmlCompositeElement
	{
		// Token: 0x170062EA RID: 25322
		// (get) Token: 0x06013B32 RID: 80690 RVA: 0x002FC4B3 File Offset: 0x002FA6B3
		public override string LocalName
		{
			get
			{
				return "nvPicPr";
			}
		}

		// Token: 0x170062EB RID: 25323
		// (get) Token: 0x06013B33 RID: 80691 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170062EC RID: 25324
		// (get) Token: 0x06013B34 RID: 80692 RVA: 0x0030AE07 File Offset: 0x00309007
		internal override int ElementTypeId
		{
			get
			{
				return 10190;
			}
		}

		// Token: 0x06013B35 RID: 80693 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013B36 RID: 80694 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPictureProperties()
		{
		}

		// Token: 0x06013B37 RID: 80695 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPictureProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B38 RID: 80696 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPictureProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B39 RID: 80697 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPictureProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013B3A RID: 80698 RVA: 0x0030AE0E File Offset: 0x0030900E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (10 == namespaceId && "cNvPicPr" == name)
			{
				return new NonVisualPictureDrawingProperties();
			}
			return null;
		}

		// Token: 0x170062ED RID: 25325
		// (get) Token: 0x06013B3B RID: 80699 RVA: 0x0030AE41 File Offset: 0x00309041
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualPictureProperties.eleTagNames;
			}
		}

		// Token: 0x170062EE RID: 25326
		// (get) Token: 0x06013B3C RID: 80700 RVA: 0x0030AE48 File Offset: 0x00309048
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualPictureProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170062EF RID: 25327
		// (get) Token: 0x06013B3D RID: 80701 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170062F0 RID: 25328
		// (get) Token: 0x06013B3E RID: 80702 RVA: 0x0030A72F File Offset: 0x0030892F
		// (set) Token: 0x06013B3F RID: 80703 RVA: 0x0030A738 File Offset: 0x00308938
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

		// Token: 0x170062F1 RID: 25329
		// (get) Token: 0x06013B40 RID: 80704 RVA: 0x0030AE4F File Offset: 0x0030904F
		// (set) Token: 0x06013B41 RID: 80705 RVA: 0x0030AE58 File Offset: 0x00309058
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

		// Token: 0x06013B42 RID: 80706 RVA: 0x0030AE62 File Offset: 0x00309062
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPictureProperties>(deep);
		}

		// Token: 0x0400875B RID: 34651
		private const string tagName = "nvPicPr";

		// Token: 0x0400875C RID: 34652
		private const byte tagNsId = 10;

		// Token: 0x0400875D RID: 34653
		internal const int ElementTypeIdConst = 10190;

		// Token: 0x0400875E RID: 34654
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvPicPr" };

		// Token: 0x0400875F RID: 34655
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
