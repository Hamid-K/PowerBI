using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel.Drawing;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200287A RID: 10362
	[ChildElementInfo(typeof(GraphicFrame))]
	[ChildElementInfo(typeof(NonVisualGroupShapeProperties))]
	[ChildElementInfo(typeof(GroupShapeProperties))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(GroupShape))]
	[ChildElementInfo(typeof(ConnectionShape))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class GroupShape : OpenXmlCompositeElement
	{
		// Token: 0x170066EF RID: 26351
		// (get) Token: 0x0601449B RID: 83099 RVA: 0x002DF94C File Offset: 0x002DDB4C
		public override string LocalName
		{
			get
			{
				return "grpSp";
			}
		}

		// Token: 0x170066F0 RID: 26352
		// (get) Token: 0x0601449C RID: 83100 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170066F1 RID: 26353
		// (get) Token: 0x0601449D RID: 83101 RVA: 0x003119AE File Offset: 0x0030FBAE
		internal override int ElementTypeId
		{
			get
			{
				return 10724;
			}
		}

		// Token: 0x0601449E RID: 83102 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601449F RID: 83103 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupShape()
		{
		}

		// Token: 0x060144A0 RID: 83104 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060144A1 RID: 83105 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060144A2 RID: 83106 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060144A3 RID: 83107 RVA: 0x003119B8 File Offset: 0x0030FBB8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "nvGrpSpPr" == name)
			{
				return new NonVisualGroupShapeProperties();
			}
			if (18 == namespaceId && "grpSpPr" == name)
			{
				return new GroupShapeProperties();
			}
			if (18 == namespaceId && "sp" == name)
			{
				return new Shape();
			}
			if (18 == namespaceId && "grpSp" == name)
			{
				return new GroupShape();
			}
			if (18 == namespaceId && "graphicFrame" == name)
			{
				return new GraphicFrame();
			}
			if (18 == namespaceId && "cxnSp" == name)
			{
				return new ConnectionShape();
			}
			if (18 == namespaceId && "pic" == name)
			{
				return new Picture();
			}
			if (54 == namespaceId && "contentPart" == name)
			{
				return new ContentPart();
			}
			return null;
		}

		// Token: 0x170066F2 RID: 26354
		// (get) Token: 0x060144A4 RID: 83108 RVA: 0x00311A86 File Offset: 0x0030FC86
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShape.eleTagNames;
			}
		}

		// Token: 0x170066F3 RID: 26355
		// (get) Token: 0x060144A5 RID: 83109 RVA: 0x00311A8D File Offset: 0x0030FC8D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShape.eleNamespaceIds;
			}
		}

		// Token: 0x170066F4 RID: 26356
		// (get) Token: 0x060144A6 RID: 83110 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170066F5 RID: 26357
		// (get) Token: 0x060144A7 RID: 83111 RVA: 0x00311A94 File Offset: 0x0030FC94
		// (set) Token: 0x060144A8 RID: 83112 RVA: 0x00311A9D File Offset: 0x0030FC9D
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

		// Token: 0x170066F6 RID: 26358
		// (get) Token: 0x060144A9 RID: 83113 RVA: 0x00311AA7 File Offset: 0x0030FCA7
		// (set) Token: 0x060144AA RID: 83114 RVA: 0x00311AB0 File Offset: 0x0030FCB0
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

		// Token: 0x060144AB RID: 83115 RVA: 0x00311ABA File Offset: 0x0030FCBA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShape>(deep);
		}

		// Token: 0x04008D6F RID: 36207
		private const string tagName = "grpSp";

		// Token: 0x04008D70 RID: 36208
		private const byte tagNsId = 18;

		// Token: 0x04008D71 RID: 36209
		internal const int ElementTypeIdConst = 10724;

		// Token: 0x04008D72 RID: 36210
		private static readonly string[] eleTagNames = new string[] { "nvGrpSpPr", "grpSpPr", "sp", "grpSp", "graphicFrame", "cxnSp", "pic", "contentPart" };

		// Token: 0x04008D73 RID: 36211
		private static readonly byte[] eleNamespaceIds = new byte[] { 18, 18, 18, 18, 18, 18, 18, 54 };
	}
}
