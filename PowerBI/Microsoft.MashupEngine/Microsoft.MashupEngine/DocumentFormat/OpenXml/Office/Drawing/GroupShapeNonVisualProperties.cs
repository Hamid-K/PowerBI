using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x0200232F RID: 9007
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualGroupDrawingShapeProperties))]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	internal class GroupShapeNonVisualProperties : OpenXmlCompositeElement
	{
		// Token: 0x170048D1 RID: 18641
		// (get) Token: 0x06010107 RID: 65799 RVA: 0x002DF395 File Offset: 0x002DD595
		public override string LocalName
		{
			get
			{
				return "nvGrpSpPr";
			}
		}

		// Token: 0x170048D2 RID: 18642
		// (get) Token: 0x06010108 RID: 65800 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x170048D3 RID: 18643
		// (get) Token: 0x06010109 RID: 65801 RVA: 0x002DF39C File Offset: 0x002DD59C
		internal override int ElementTypeId
		{
			get
			{
				return 13030;
			}
		}

		// Token: 0x0601010A RID: 65802 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601010B RID: 65803 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupShapeNonVisualProperties()
		{
		}

		// Token: 0x0601010C RID: 65804 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupShapeNonVisualProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601010D RID: 65805 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupShapeNonVisualProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601010E RID: 65806 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupShapeNonVisualProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601010F RID: 65807 RVA: 0x002DF3A3 File Offset: 0x002DD5A3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (56 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (56 == namespaceId && "cNvGrpSpPr" == name)
			{
				return new NonVisualGroupDrawingShapeProperties();
			}
			return null;
		}

		// Token: 0x170048D4 RID: 18644
		// (get) Token: 0x06010110 RID: 65808 RVA: 0x002DF3D6 File Offset: 0x002DD5D6
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShapeNonVisualProperties.eleTagNames;
			}
		}

		// Token: 0x170048D5 RID: 18645
		// (get) Token: 0x06010111 RID: 65809 RVA: 0x002DF3DD File Offset: 0x002DD5DD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShapeNonVisualProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170048D6 RID: 18646
		// (get) Token: 0x06010112 RID: 65810 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170048D7 RID: 18647
		// (get) Token: 0x06010113 RID: 65811 RVA: 0x002DEC5A File Offset: 0x002DCE5A
		// (set) Token: 0x06010114 RID: 65812 RVA: 0x002DEC63 File Offset: 0x002DCE63
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

		// Token: 0x170048D8 RID: 18648
		// (get) Token: 0x06010115 RID: 65813 RVA: 0x002DF3E4 File Offset: 0x002DD5E4
		// (set) Token: 0x06010116 RID: 65814 RVA: 0x002DF3ED File Offset: 0x002DD5ED
		public NonVisualGroupDrawingShapeProperties NonVisualGroupDrawingShapeProperties
		{
			get
			{
				return base.GetElement<NonVisualGroupDrawingShapeProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualGroupDrawingShapeProperties>(1, value);
			}
		}

		// Token: 0x06010117 RID: 65815 RVA: 0x002DF3F7 File Offset: 0x002DD5F7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShapeNonVisualProperties>(deep);
		}

		// Token: 0x040072ED RID: 29421
		private const string tagName = "nvGrpSpPr";

		// Token: 0x040072EE RID: 29422
		private const byte tagNsId = 56;

		// Token: 0x040072EF RID: 29423
		internal const int ElementTypeIdConst = 13030;

		// Token: 0x040072F0 RID: 29424
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvGrpSpPr" };

		// Token: 0x040072F1 RID: 29425
		private static readonly byte[] eleNamespaceIds = new byte[] { 56, 56 };
	}
}
