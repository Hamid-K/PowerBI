using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002643 RID: 9795
	[ChildElementInfo(typeof(NonVisualGroupShapeDrawingProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	internal class NonVisualGroupShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005AE4 RID: 23268
		// (get) Token: 0x0601292A RID: 76074 RVA: 0x002DF395 File Offset: 0x002DD595
		public override string LocalName
		{
			get
			{
				return "nvGrpSpPr";
			}
		}

		// Token: 0x17005AE5 RID: 23269
		// (get) Token: 0x0601292B RID: 76075 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005AE6 RID: 23270
		// (get) Token: 0x0601292C RID: 76076 RVA: 0x002FCB27 File Offset: 0x002FAD27
		internal override int ElementTypeId
		{
			get
			{
				return 10613;
			}
		}

		// Token: 0x0601292D RID: 76077 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601292E RID: 76078 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGroupShapeProperties()
		{
		}

		// Token: 0x0601292F RID: 76079 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGroupShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012930 RID: 76080 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGroupShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012931 RID: 76081 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGroupShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012932 RID: 76082 RVA: 0x002FCB2E File Offset: 0x002FAD2E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (12 == namespaceId && "cNvGrpSpPr" == name)
			{
				return new NonVisualGroupShapeDrawingProperties();
			}
			return null;
		}

		// Token: 0x17005AE7 RID: 23271
		// (get) Token: 0x06012933 RID: 76083 RVA: 0x002FCB61 File Offset: 0x002FAD61
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGroupShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17005AE8 RID: 23272
		// (get) Token: 0x06012934 RID: 76084 RVA: 0x002FCB68 File Offset: 0x002FAD68
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGroupShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005AE9 RID: 23273
		// (get) Token: 0x06012935 RID: 76085 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005AEA RID: 23274
		// (get) Token: 0x06012936 RID: 76086 RVA: 0x002FBD7F File Offset: 0x002F9F7F
		// (set) Token: 0x06012937 RID: 76087 RVA: 0x002FBD88 File Offset: 0x002F9F88
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

		// Token: 0x17005AEB RID: 23275
		// (get) Token: 0x06012938 RID: 76088 RVA: 0x002FCB6F File Offset: 0x002FAD6F
		// (set) Token: 0x06012939 RID: 76089 RVA: 0x002FCB78 File Offset: 0x002FAD78
		public NonVisualGroupShapeDrawingProperties NonVisualGroupShapeDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualGroupShapeDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualGroupShapeDrawingProperties>(1, value);
			}
		}

		// Token: 0x0601293A RID: 76090 RVA: 0x002FCB82 File Offset: 0x002FAD82
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGroupShapeProperties>(deep);
		}

		// Token: 0x040080BA RID: 32954
		private const string tagName = "nvGrpSpPr";

		// Token: 0x040080BB RID: 32955
		private const byte tagNsId = 12;

		// Token: 0x040080BC RID: 32956
		internal const int ElementTypeIdConst = 10613;

		// Token: 0x040080BD RID: 32957
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvGrpSpPr" };

		// Token: 0x040080BE RID: 32958
		private static readonly byte[] eleNamespaceIds = new byte[] { 12, 12 };
	}
}
