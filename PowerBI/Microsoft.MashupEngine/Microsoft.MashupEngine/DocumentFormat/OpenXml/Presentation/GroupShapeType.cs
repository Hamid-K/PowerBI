using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A9D RID: 10909
	[ChildElementInfo(typeof(NonVisualGroupShapeProperties))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(GroupShape))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GroupShapeProperties))]
	[ChildElementInfo(typeof(GraphicFrame))]
	[ChildElementInfo(typeof(ConnectionShape))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	internal abstract class GroupShapeType : OpenXmlCompositeElement
	{
		// Token: 0x06016291 RID: 90769 RVA: 0x00327160 File Offset: 0x00325360
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "nvGrpSpPr" == name)
			{
				return new NonVisualGroupShapeProperties();
			}
			if (24 == namespaceId && "grpSpPr" == name)
			{
				return new GroupShapeProperties();
			}
			if (24 == namespaceId && "sp" == name)
			{
				return new Shape();
			}
			if (24 == namespaceId && "grpSp" == name)
			{
				return new GroupShape();
			}
			if (24 == namespaceId && "graphicFrame" == name)
			{
				return new GraphicFrame();
			}
			if (24 == namespaceId && "cxnSp" == name)
			{
				return new ConnectionShape();
			}
			if (24 == namespaceId && "pic" == name)
			{
				return new Picture();
			}
			if (24 == namespaceId && "contentPart" == name)
			{
				return new ContentPart();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x1700742B RID: 29739
		// (get) Token: 0x06016292 RID: 90770 RVA: 0x00327246 File Offset: 0x00325446
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShapeType.eleTagNames;
			}
		}

		// Token: 0x1700742C RID: 29740
		// (get) Token: 0x06016293 RID: 90771 RVA: 0x0032724D File Offset: 0x0032544D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShapeType.eleNamespaceIds;
			}
		}

		// Token: 0x1700742D RID: 29741
		// (get) Token: 0x06016294 RID: 90772 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700742E RID: 29742
		// (get) Token: 0x06016295 RID: 90773 RVA: 0x00327254 File Offset: 0x00325454
		// (set) Token: 0x06016296 RID: 90774 RVA: 0x0032725D File Offset: 0x0032545D
		public NonVisualGroupShapeProperties NonVisualGroupShapeProperties
		{
			get
			{
				return base.GetElement<NonVisualGroupShapeProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualGroupShapeProperties>(0, value);
			}
		}

		// Token: 0x1700742F RID: 29743
		// (get) Token: 0x06016297 RID: 90775 RVA: 0x00327267 File Offset: 0x00325467
		// (set) Token: 0x06016298 RID: 90776 RVA: 0x00327270 File Offset: 0x00325470
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

		// Token: 0x06016299 RID: 90777 RVA: 0x00293ECF File Offset: 0x002920CF
		protected GroupShapeType()
		{
		}

		// Token: 0x0601629A RID: 90778 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected GroupShapeType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601629B RID: 90779 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected GroupShapeType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601629C RID: 90780 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected GroupShapeType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0400967E RID: 38526
		private static readonly string[] eleTagNames = new string[] { "nvGrpSpPr", "grpSpPr", "sp", "grpSp", "graphicFrame", "cxnSp", "pic", "contentPart", "extLst" };

		// Token: 0x0400967F RID: 38527
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24, 24, 24, 24, 24 };
	}
}
