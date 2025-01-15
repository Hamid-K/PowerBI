using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002637 RID: 9783
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualPictureDrawingProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualPictureProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005A93 RID: 23187
		// (get) Token: 0x06012871 RID: 75889 RVA: 0x002FC4B3 File Offset: 0x002FA6B3
		public override string LocalName
		{
			get
			{
				return "nvPicPr";
			}
		}

		// Token: 0x17005A94 RID: 23188
		// (get) Token: 0x06012872 RID: 75890 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A95 RID: 23189
		// (get) Token: 0x06012873 RID: 75891 RVA: 0x002FC4BA File Offset: 0x002FA6BA
		internal override int ElementTypeId
		{
			get
			{
				return 10602;
			}
		}

		// Token: 0x06012874 RID: 75892 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012875 RID: 75893 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPictureProperties()
		{
		}

		// Token: 0x06012876 RID: 75894 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPictureProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012877 RID: 75895 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPictureProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012878 RID: 75896 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPictureProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012879 RID: 75897 RVA: 0x002FC4C1 File Offset: 0x002FA6C1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (12 == namespaceId && "cNvPicPr" == name)
			{
				return new NonVisualPictureDrawingProperties();
			}
			return null;
		}

		// Token: 0x17005A96 RID: 23190
		// (get) Token: 0x0601287A RID: 75898 RVA: 0x002FC4F4 File Offset: 0x002FA6F4
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualPictureProperties.eleTagNames;
			}
		}

		// Token: 0x17005A97 RID: 23191
		// (get) Token: 0x0601287B RID: 75899 RVA: 0x002FC4FB File Offset: 0x002FA6FB
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualPictureProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005A98 RID: 23192
		// (get) Token: 0x0601287C RID: 75900 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A99 RID: 23193
		// (get) Token: 0x0601287D RID: 75901 RVA: 0x002FBD7F File Offset: 0x002F9F7F
		// (set) Token: 0x0601287E RID: 75902 RVA: 0x002FBD88 File Offset: 0x002F9F88
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

		// Token: 0x17005A9A RID: 23194
		// (get) Token: 0x0601287F RID: 75903 RVA: 0x002FC502 File Offset: 0x002FA702
		// (set) Token: 0x06012880 RID: 75904 RVA: 0x002FC50B File Offset: 0x002FA70B
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

		// Token: 0x06012881 RID: 75905 RVA: 0x002FC515 File Offset: 0x002FA715
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPictureProperties>(deep);
		}

		// Token: 0x04008085 RID: 32901
		private const string tagName = "nvPicPr";

		// Token: 0x04008086 RID: 32902
		private const byte tagNsId = 12;

		// Token: 0x04008087 RID: 32903
		internal const int ElementTypeIdConst = 10602;

		// Token: 0x04008088 RID: 32904
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvPicPr" };

		// Token: 0x04008089 RID: 32905
		private static readonly byte[] eleNamespaceIds = new byte[] { 12, 12 };
	}
}
