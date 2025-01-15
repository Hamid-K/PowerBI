using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027B0 RID: 10160
	[ChildElementInfo(typeof(GroupShapeLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualGroupShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006302 RID: 25346
		// (get) Token: 0x06013B68 RID: 80744 RVA: 0x002DF2E9 File Offset: 0x002DD4E9
		public override string LocalName
		{
			get
			{
				return "cNvGrpSpPr";
			}
		}

		// Token: 0x17006303 RID: 25347
		// (get) Token: 0x06013B69 RID: 80745 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006304 RID: 25348
		// (get) Token: 0x06013B6A RID: 80746 RVA: 0x0030AFB5 File Offset: 0x003091B5
		internal override int ElementTypeId
		{
			get
			{
				return 10193;
			}
		}

		// Token: 0x06013B6B RID: 80747 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013B6C RID: 80748 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGroupShapeDrawingProperties()
		{
		}

		// Token: 0x06013B6D RID: 80749 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGroupShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B6E RID: 80750 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGroupShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B6F RID: 80751 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGroupShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013B70 RID: 80752 RVA: 0x002DF2F7 File Offset: 0x002DD4F7
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

		// Token: 0x17006305 RID: 25349
		// (get) Token: 0x06013B71 RID: 80753 RVA: 0x0030AFBC File Offset: 0x003091BC
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGroupShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17006306 RID: 25350
		// (get) Token: 0x06013B72 RID: 80754 RVA: 0x0030AFC3 File Offset: 0x003091C3
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGroupShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006307 RID: 25351
		// (get) Token: 0x06013B73 RID: 80755 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006308 RID: 25352
		// (get) Token: 0x06013B74 RID: 80756 RVA: 0x002DF338 File Offset: 0x002DD538
		// (set) Token: 0x06013B75 RID: 80757 RVA: 0x002DF341 File Offset: 0x002DD541
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

		// Token: 0x17006309 RID: 25353
		// (get) Token: 0x06013B76 RID: 80758 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x06013B77 RID: 80759 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x06013B78 RID: 80760 RVA: 0x0030AFCA File Offset: 0x003091CA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGroupShapeDrawingProperties>(deep);
		}

		// Token: 0x0400876A RID: 34666
		private const string tagName = "cNvGrpSpPr";

		// Token: 0x0400876B RID: 34667
		private const byte tagNsId = 10;

		// Token: 0x0400876C RID: 34668
		internal const int ElementTypeIdConst = 10193;

		// Token: 0x0400876D RID: 34669
		private static readonly string[] eleTagNames = new string[] { "grpSpLocks", "extLst" };

		// Token: 0x0400876E RID: 34670
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
