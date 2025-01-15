using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200263C RID: 9788
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(GroupShapeLocks))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualGroupShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005AC4 RID: 23236
		// (get) Token: 0x060128DB RID: 75995 RVA: 0x002DF2E9 File Offset: 0x002DD4E9
		public override string LocalName
		{
			get
			{
				return "cNvGrpSpPr";
			}
		}

		// Token: 0x17005AC5 RID: 23237
		// (get) Token: 0x060128DC RID: 75996 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005AC6 RID: 23238
		// (get) Token: 0x060128DD RID: 75997 RVA: 0x002FC8E7 File Offset: 0x002FAAE7
		internal override int ElementTypeId
		{
			get
			{
				return 10607;
			}
		}

		// Token: 0x060128DE RID: 75998 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060128DF RID: 75999 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGroupShapeDrawingProperties()
		{
		}

		// Token: 0x060128E0 RID: 76000 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGroupShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060128E1 RID: 76001 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGroupShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060128E2 RID: 76002 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGroupShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060128E3 RID: 76003 RVA: 0x002DF2F7 File Offset: 0x002DD4F7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "grpSpLocks" == name)
			{
				return new GroupShapeLocks();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005AC7 RID: 23239
		// (get) Token: 0x060128E4 RID: 76004 RVA: 0x002FC8EE File Offset: 0x002FAAEE
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGroupShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17005AC8 RID: 23240
		// (get) Token: 0x060128E5 RID: 76005 RVA: 0x002FC8F5 File Offset: 0x002FAAF5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGroupShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005AC9 RID: 23241
		// (get) Token: 0x060128E6 RID: 76006 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005ACA RID: 23242
		// (get) Token: 0x060128E7 RID: 76007 RVA: 0x002DF338 File Offset: 0x002DD538
		// (set) Token: 0x060128E8 RID: 76008 RVA: 0x002DF341 File Offset: 0x002DD541
		public GroupShapeLocks GroupShapeLocks
		{
			get
			{
				return base.GetElement<GroupShapeLocks>(0);
			}
			set
			{
				base.SetElement<GroupShapeLocks>(0, value);
			}
		}

		// Token: 0x17005ACB RID: 23243
		// (get) Token: 0x060128E9 RID: 76009 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x060128EA RID: 76010 RVA: 0x002DEB73 File Offset: 0x002DCD73
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x060128EB RID: 76011 RVA: 0x002FC8FC File Offset: 0x002FAAFC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGroupShapeDrawingProperties>(deep);
		}

		// Token: 0x040080A2 RID: 32930
		private const string tagName = "cNvGrpSpPr";

		// Token: 0x040080A3 RID: 32931
		private const byte tagNsId = 12;

		// Token: 0x040080A4 RID: 32932
		internal const int ElementTypeIdConst = 10607;

		// Token: 0x040080A5 RID: 32933
		private static readonly string[] eleTagNames = new string[] { "grpSpLocks", "extLst" };

		// Token: 0x040080A6 RID: 32934
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
