using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingGroup
{
	// Token: 0x020024F6 RID: 9462
	[ChildElementInfo(typeof(GroupShapeLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualGroupDrawingShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700537D RID: 21373
		// (get) Token: 0x060118D6 RID: 71894 RVA: 0x002DF2E9 File Offset: 0x002DD4E9
		public override string LocalName
		{
			get
			{
				return "cNvGrpSpPr";
			}
		}

		// Token: 0x1700537E RID: 21374
		// (get) Token: 0x060118D7 RID: 71895 RVA: 0x002EF715 File Offset: 0x002ED915
		internal override byte NamespaceId
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x1700537F RID: 21375
		// (get) Token: 0x060118D8 RID: 71896 RVA: 0x002EFAA3 File Offset: 0x002EDCA3
		internal override int ElementTypeId
		{
			get
			{
				return 13127;
			}
		}

		// Token: 0x060118D9 RID: 71897 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060118DA RID: 71898 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGroupDrawingShapeProperties()
		{
		}

		// Token: 0x060118DB RID: 71899 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGroupDrawingShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060118DC RID: 71900 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGroupDrawingShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060118DD RID: 71901 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGroupDrawingShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060118DE RID: 71902 RVA: 0x002DF2F7 File Offset: 0x002DD4F7
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

		// Token: 0x17005380 RID: 21376
		// (get) Token: 0x060118DF RID: 71903 RVA: 0x002EFAAA File Offset: 0x002EDCAA
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGroupDrawingShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17005381 RID: 21377
		// (get) Token: 0x060118E0 RID: 71904 RVA: 0x002EFAB1 File Offset: 0x002EDCB1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGroupDrawingShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005382 RID: 21378
		// (get) Token: 0x060118E1 RID: 71905 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005383 RID: 21379
		// (get) Token: 0x060118E2 RID: 71906 RVA: 0x002DF338 File Offset: 0x002DD538
		// (set) Token: 0x060118E3 RID: 71907 RVA: 0x002DF341 File Offset: 0x002DD541
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

		// Token: 0x17005384 RID: 21380
		// (get) Token: 0x060118E4 RID: 71908 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x060118E5 RID: 71909 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x060118E6 RID: 71910 RVA: 0x002EFAB8 File Offset: 0x002EDCB8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGroupDrawingShapeProperties>(deep);
		}

		// Token: 0x04007B48 RID: 31560
		private const string tagName = "cNvGrpSpPr";

		// Token: 0x04007B49 RID: 31561
		private const byte tagNsId = 60;

		// Token: 0x04007B4A RID: 31562
		internal const int ElementTypeIdConst = 13127;

		// Token: 0x04007B4B RID: 31563
		private static readonly string[] eleTagNames = new string[] { "grpSpLocks", "extLst" };

		// Token: 0x04007B4C RID: 31564
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
