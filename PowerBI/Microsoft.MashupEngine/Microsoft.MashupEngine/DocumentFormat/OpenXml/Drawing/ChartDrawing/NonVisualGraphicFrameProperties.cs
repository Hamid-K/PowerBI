using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200263A RID: 9786
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualGraphicFrameDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	internal class NonVisualGraphicFrameProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005AAF RID: 23215
		// (get) Token: 0x060128AE RID: 75950 RVA: 0x002FC745 File Offset: 0x002FA945
		public override string LocalName
		{
			get
			{
				return "nvGraphicFramePr";
			}
		}

		// Token: 0x17005AB0 RID: 23216
		// (get) Token: 0x060128AF RID: 75951 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005AB1 RID: 23217
		// (get) Token: 0x060128B0 RID: 75952 RVA: 0x002FC74C File Offset: 0x002FA94C
		internal override int ElementTypeId
		{
			get
			{
				return 10605;
			}
		}

		// Token: 0x060128B1 RID: 75953 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060128B2 RID: 75954 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGraphicFrameProperties()
		{
		}

		// Token: 0x060128B3 RID: 75955 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGraphicFrameProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060128B4 RID: 75956 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGraphicFrameProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060128B5 RID: 75957 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGraphicFrameProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060128B6 RID: 75958 RVA: 0x002FC753 File Offset: 0x002FA953
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (12 == namespaceId && "cNvGraphicFramePr" == name)
			{
				return new NonVisualGraphicFrameDrawingProperties();
			}
			return null;
		}

		// Token: 0x17005AB2 RID: 23218
		// (get) Token: 0x060128B7 RID: 75959 RVA: 0x002FC786 File Offset: 0x002FA986
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGraphicFrameProperties.eleTagNames;
			}
		}

		// Token: 0x17005AB3 RID: 23219
		// (get) Token: 0x060128B8 RID: 75960 RVA: 0x002FC78D File Offset: 0x002FA98D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGraphicFrameProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005AB4 RID: 23220
		// (get) Token: 0x060128B9 RID: 75961 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005AB5 RID: 23221
		// (get) Token: 0x060128BA RID: 75962 RVA: 0x002FBD7F File Offset: 0x002F9F7F
		// (set) Token: 0x060128BB RID: 75963 RVA: 0x002FBD88 File Offset: 0x002F9F88
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

		// Token: 0x17005AB6 RID: 23222
		// (get) Token: 0x060128BC RID: 75964 RVA: 0x002FC794 File Offset: 0x002FA994
		// (set) Token: 0x060128BD RID: 75965 RVA: 0x002FC79D File Offset: 0x002FA99D
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

		// Token: 0x060128BE RID: 75966 RVA: 0x002FC7A7 File Offset: 0x002FA9A7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGraphicFrameProperties>(deep);
		}

		// Token: 0x04008096 RID: 32918
		private const string tagName = "nvGraphicFramePr";

		// Token: 0x04008097 RID: 32919
		private const byte tagNsId = 12;

		// Token: 0x04008098 RID: 32920
		internal const int ElementTypeIdConst = 10605;

		// Token: 0x04008099 RID: 32921
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvGraphicFramePr" };

		// Token: 0x0400809A RID: 32922
		private static readonly byte[] eleNamespaceIds = new byte[] { 12, 12 };
	}
}
