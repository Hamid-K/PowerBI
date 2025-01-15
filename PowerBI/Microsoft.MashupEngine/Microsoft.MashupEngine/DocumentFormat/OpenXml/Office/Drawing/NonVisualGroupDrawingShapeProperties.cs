using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x0200232E RID: 9006
	[ChildElementInfo(typeof(GroupShapeLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualGroupDrawingShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x170048C9 RID: 18633
		// (get) Token: 0x060100F5 RID: 65781 RVA: 0x002DF2E9 File Offset: 0x002DD4E9
		public override string LocalName
		{
			get
			{
				return "cNvGrpSpPr";
			}
		}

		// Token: 0x170048CA RID: 18634
		// (get) Token: 0x060100F6 RID: 65782 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x170048CB RID: 18635
		// (get) Token: 0x060100F7 RID: 65783 RVA: 0x002DF2F0 File Offset: 0x002DD4F0
		internal override int ElementTypeId
		{
			get
			{
				return 13029;
			}
		}

		// Token: 0x060100F8 RID: 65784 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060100F9 RID: 65785 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGroupDrawingShapeProperties()
		{
		}

		// Token: 0x060100FA RID: 65786 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGroupDrawingShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060100FB RID: 65787 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGroupDrawingShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060100FC RID: 65788 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGroupDrawingShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060100FD RID: 65789 RVA: 0x002DF2F7 File Offset: 0x002DD4F7
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

		// Token: 0x170048CC RID: 18636
		// (get) Token: 0x060100FE RID: 65790 RVA: 0x002DF32A File Offset: 0x002DD52A
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGroupDrawingShapeProperties.eleTagNames;
			}
		}

		// Token: 0x170048CD RID: 18637
		// (get) Token: 0x060100FF RID: 65791 RVA: 0x002DF331 File Offset: 0x002DD531
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGroupDrawingShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170048CE RID: 18638
		// (get) Token: 0x06010100 RID: 65792 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170048CF RID: 18639
		// (get) Token: 0x06010101 RID: 65793 RVA: 0x002DF338 File Offset: 0x002DD538
		// (set) Token: 0x06010102 RID: 65794 RVA: 0x002DF341 File Offset: 0x002DD541
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

		// Token: 0x170048D0 RID: 18640
		// (get) Token: 0x06010103 RID: 65795 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x06010104 RID: 65796 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x06010105 RID: 65797 RVA: 0x002DF34B File Offset: 0x002DD54B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGroupDrawingShapeProperties>(deep);
		}

		// Token: 0x040072E8 RID: 29416
		private const string tagName = "cNvGrpSpPr";

		// Token: 0x040072E9 RID: 29417
		private const byte tagNsId = 56;

		// Token: 0x040072EA RID: 29418
		internal const int ElementTypeIdConst = 13029;

		// Token: 0x040072EB RID: 29419
		private static readonly string[] eleTagNames = new string[] { "grpSpLocks", "extLst" };

		// Token: 0x040072EC RID: 29420
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
