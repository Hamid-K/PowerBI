using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x02002332 RID: 9010
	[ChildElementInfo(typeof(GroupShape))]
	[ChildElementInfo(typeof(OfficeArtExtensionList))]
	[ChildElementInfo(typeof(GroupShapeNonVisualProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(GroupShapeProperties))]
	internal abstract class GroupShapeType : OpenXmlCompositeElement
	{
		// Token: 0x0601014D RID: 65869 RVA: 0x002DF838 File Offset: 0x002DDA38
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (56 == namespaceId && "nvGrpSpPr" == name)
			{
				return new GroupShapeNonVisualProperties();
			}
			if (56 == namespaceId && "grpSpPr" == name)
			{
				return new GroupShapeProperties();
			}
			if (56 == namespaceId && "sp" == name)
			{
				return new Shape();
			}
			if (56 == namespaceId && "grpSp" == name)
			{
				return new GroupShape();
			}
			if (56 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x170048F2 RID: 18674
		// (get) Token: 0x0601014E RID: 65870 RVA: 0x002DF8BE File Offset: 0x002DDABE
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShapeType.eleTagNames;
			}
		}

		// Token: 0x170048F3 RID: 18675
		// (get) Token: 0x0601014F RID: 65871 RVA: 0x002DF8C5 File Offset: 0x002DDAC5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShapeType.eleNamespaceIds;
			}
		}

		// Token: 0x170048F4 RID: 18676
		// (get) Token: 0x06010150 RID: 65872 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170048F5 RID: 18677
		// (get) Token: 0x06010151 RID: 65873 RVA: 0x002DF8CC File Offset: 0x002DDACC
		// (set) Token: 0x06010152 RID: 65874 RVA: 0x002DF8D5 File Offset: 0x002DDAD5
		public GroupShapeNonVisualProperties GroupShapeNonVisualProperties
		{
			get
			{
				return base.GetElement<GroupShapeNonVisualProperties>(0);
			}
			set
			{
				base.SetElement<GroupShapeNonVisualProperties>(0, value);
			}
		}

		// Token: 0x170048F6 RID: 18678
		// (get) Token: 0x06010153 RID: 65875 RVA: 0x002DF8DF File Offset: 0x002DDADF
		// (set) Token: 0x06010154 RID: 65876 RVA: 0x002DF8E8 File Offset: 0x002DDAE8
		public GroupShapeProperties GroupShapeProperties
		{
			get
			{
				return base.GetElement<GroupShapeProperties>(1);
			}
			set
			{
				base.SetElement<GroupShapeProperties>(1, value);
			}
		}

		// Token: 0x06010155 RID: 65877 RVA: 0x00293ECF File Offset: 0x002920CF
		protected GroupShapeType()
		{
		}

		// Token: 0x06010156 RID: 65878 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected GroupShapeType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010157 RID: 65879 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected GroupShapeType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010158 RID: 65880 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected GroupShapeType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007300 RID: 29440
		private static readonly string[] eleTagNames = new string[] { "nvGrpSpPr", "grpSpPr", "sp", "grpSp", "extLst" };

		// Token: 0x04007301 RID: 29441
		private static readonly byte[] eleNamespaceIds = new byte[] { 56, 56, 56, 56, 56 };
	}
}
