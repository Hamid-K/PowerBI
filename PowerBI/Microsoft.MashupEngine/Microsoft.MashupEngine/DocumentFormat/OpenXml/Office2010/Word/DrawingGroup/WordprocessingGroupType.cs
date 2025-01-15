using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml.Office2010.Word.DrawingShape;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingGroup
{
	// Token: 0x020024EF RID: 9455
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.Word.DrawingGroup.NonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualGroupDrawingShapeProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GroupShapeProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WordprocessingShape), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GroupShape), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GraphicFrame), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class WordprocessingGroupType : OpenXmlCompositeElement
	{
		// Token: 0x0601185D RID: 71773 RVA: 0x002EF564 File Offset: 0x002ED764
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (60 == namespaceId && "cNvPr" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.Word.DrawingGroup.NonVisualDrawingProperties();
			}
			if (60 == namespaceId && "cNvGrpSpPr" == name)
			{
				return new NonVisualGroupDrawingShapeProperties();
			}
			if (60 == namespaceId && "grpSpPr" == name)
			{
				return new GroupShapeProperties();
			}
			if (61 == namespaceId && "wsp" == name)
			{
				return new WordprocessingShape();
			}
			if (60 == namespaceId && "grpSp" == name)
			{
				return new GroupShape();
			}
			if (60 == namespaceId && "graphicFrame" == name)
			{
				return new GraphicFrame();
			}
			if (17 == namespaceId && "pic" == name)
			{
				return new Picture();
			}
			if (52 == namespaceId && "contentPart" == name)
			{
				return new ContentPart();
			}
			if (60 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x17005349 RID: 21321
		// (get) Token: 0x0601185E RID: 71774 RVA: 0x002EF64A File Offset: 0x002ED84A
		internal override string[] ElementTagNames
		{
			get
			{
				return WordprocessingGroupType.eleTagNames;
			}
		}

		// Token: 0x1700534A RID: 21322
		// (get) Token: 0x0601185F RID: 71775 RVA: 0x002EF651 File Offset: 0x002ED851
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WordprocessingGroupType.eleNamespaceIds;
			}
		}

		// Token: 0x1700534B RID: 21323
		// (get) Token: 0x06011860 RID: 71776 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700534C RID: 21324
		// (get) Token: 0x06011861 RID: 71777 RVA: 0x002EF658 File Offset: 0x002ED858
		// (set) Token: 0x06011862 RID: 71778 RVA: 0x002EF661 File Offset: 0x002ED861
		public DocumentFormat.OpenXml.Office2010.Word.DrawingGroup.NonVisualDrawingProperties NonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<DocumentFormat.OpenXml.Office2010.Word.DrawingGroup.NonVisualDrawingProperties>(0);
			}
			set
			{
				base.SetElement<DocumentFormat.OpenXml.Office2010.Word.DrawingGroup.NonVisualDrawingProperties>(0, value);
			}
		}

		// Token: 0x1700534D RID: 21325
		// (get) Token: 0x06011863 RID: 71779 RVA: 0x002EF66B File Offset: 0x002ED86B
		// (set) Token: 0x06011864 RID: 71780 RVA: 0x002EF674 File Offset: 0x002ED874
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

		// Token: 0x1700534E RID: 21326
		// (get) Token: 0x06011865 RID: 71781 RVA: 0x002EF67E File Offset: 0x002ED87E
		// (set) Token: 0x06011866 RID: 71782 RVA: 0x002EF687 File Offset: 0x002ED887
		public GroupShapeProperties GroupShapeProperties
		{
			get
			{
				return base.GetElement<GroupShapeProperties>(2);
			}
			set
			{
				base.SetElement<GroupShapeProperties>(2, value);
			}
		}

		// Token: 0x06011867 RID: 71783 RVA: 0x00293ECF File Offset: 0x002920CF
		protected WordprocessingGroupType()
		{
		}

		// Token: 0x06011868 RID: 71784 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected WordprocessingGroupType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011869 RID: 71785 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected WordprocessingGroupType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601186A RID: 71786 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected WordprocessingGroupType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007B2A RID: 31530
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvGrpSpPr", "grpSpPr", "wsp", "grpSp", "graphicFrame", "pic", "contentPart", "extLst" };

		// Token: 0x04007B2B RID: 31531
		private static readonly byte[] eleNamespaceIds = new byte[] { 60, 60, 60, 61, 60, 60, 17, 52, 60 };
	}
}
