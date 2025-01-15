using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002836 RID: 10294
	[ChildElementInfo(typeof(NonVisualGroupShapeDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualGroupShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700661F RID: 26143
		// (get) Token: 0x060142D3 RID: 82643 RVA: 0x002DF395 File Offset: 0x002DD595
		public override string LocalName
		{
			get
			{
				return "nvGrpSpPr";
			}
		}

		// Token: 0x17006620 RID: 26144
		// (get) Token: 0x060142D4 RID: 82644 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006621 RID: 26145
		// (get) Token: 0x060142D5 RID: 82645 RVA: 0x0030FF91 File Offset: 0x0030E191
		internal override int ElementTypeId
		{
			get
			{
				return 10330;
			}
		}

		// Token: 0x060142D6 RID: 82646 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060142D7 RID: 82647 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGroupShapeProperties()
		{
		}

		// Token: 0x060142D8 RID: 82648 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGroupShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060142D9 RID: 82649 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGroupShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060142DA RID: 82650 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGroupShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060142DB RID: 82651 RVA: 0x0030FF98 File Offset: 0x0030E198
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (10 == namespaceId && "cNvGrpSpPr" == name)
			{
				return new NonVisualGroupShapeDrawingProperties();
			}
			return null;
		}

		// Token: 0x17006622 RID: 26146
		// (get) Token: 0x060142DC RID: 82652 RVA: 0x0030FFCB File Offset: 0x0030E1CB
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGroupShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17006623 RID: 26147
		// (get) Token: 0x060142DD RID: 82653 RVA: 0x0030FFD2 File Offset: 0x0030E1D2
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGroupShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006624 RID: 26148
		// (get) Token: 0x060142DE RID: 82654 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006625 RID: 26149
		// (get) Token: 0x060142DF RID: 82655 RVA: 0x0030A72F File Offset: 0x0030892F
		// (set) Token: 0x060142E0 RID: 82656 RVA: 0x0030A738 File Offset: 0x00308938
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

		// Token: 0x17006626 RID: 26150
		// (get) Token: 0x060142E1 RID: 82657 RVA: 0x0030FFD9 File Offset: 0x0030E1D9
		// (set) Token: 0x060142E2 RID: 82658 RVA: 0x0030FFE2 File Offset: 0x0030E1E2
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

		// Token: 0x060142E3 RID: 82659 RVA: 0x0030FFEC File Offset: 0x0030E1EC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGroupShapeProperties>(deep);
		}

		// Token: 0x04008964 RID: 35172
		private const string tagName = "nvGrpSpPr";

		// Token: 0x04008965 RID: 35173
		private const byte tagNsId = 10;

		// Token: 0x04008966 RID: 35174
		internal const int ElementTypeIdConst = 10330;

		// Token: 0x04008967 RID: 35175
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvGrpSpPr" };

		// Token: 0x04008968 RID: 35176
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
