using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200287B RID: 10363
	[ChildElementInfo(typeof(NonVisualGraphicFrameProperties))]
	[ChildElementInfo(typeof(Transform))]
	[ChildElementInfo(typeof(Graphic))]
	[GeneratedCode("DomGen", "2.0")]
	internal class GraphicFrame : OpenXmlCompositeElement
	{
		// Token: 0x170066F7 RID: 26359
		// (get) Token: 0x060144AD RID: 83117 RVA: 0x002EFCFA File Offset: 0x002EDEFA
		public override string LocalName
		{
			get
			{
				return "graphicFrame";
			}
		}

		// Token: 0x170066F8 RID: 26360
		// (get) Token: 0x060144AE RID: 83118 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170066F9 RID: 26361
		// (get) Token: 0x060144AF RID: 83119 RVA: 0x00311B34 File Offset: 0x0030FD34
		internal override int ElementTypeId
		{
			get
			{
				return 10725;
			}
		}

		// Token: 0x060144B0 RID: 83120 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170066FA RID: 26362
		// (get) Token: 0x060144B1 RID: 83121 RVA: 0x00311B3B File Offset: 0x0030FD3B
		internal override string[] AttributeTagNames
		{
			get
			{
				return GraphicFrame.attributeTagNames;
			}
		}

		// Token: 0x170066FB RID: 26363
		// (get) Token: 0x060144B2 RID: 83122 RVA: 0x00311B42 File Offset: 0x0030FD42
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GraphicFrame.attributeNamespaceIds;
			}
		}

		// Token: 0x170066FC RID: 26364
		// (get) Token: 0x060144B3 RID: 83123 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060144B4 RID: 83124 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "macro")]
		public StringValue Macro
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170066FD RID: 26365
		// (get) Token: 0x060144B5 RID: 83125 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060144B6 RID: 83126 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fPublished")]
		public BooleanValue Published
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060144B7 RID: 83127 RVA: 0x00293ECF File Offset: 0x002920CF
		public GraphicFrame()
		{
		}

		// Token: 0x060144B8 RID: 83128 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GraphicFrame(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060144B9 RID: 83129 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GraphicFrame(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060144BA RID: 83130 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GraphicFrame(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060144BB RID: 83131 RVA: 0x00311B4C File Offset: 0x0030FD4C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "nvGraphicFramePr" == name)
			{
				return new NonVisualGraphicFrameProperties();
			}
			if (18 == namespaceId && "xfrm" == name)
			{
				return new Transform();
			}
			if (10 == namespaceId && "graphic" == name)
			{
				return new Graphic();
			}
			return null;
		}

		// Token: 0x170066FE RID: 26366
		// (get) Token: 0x060144BC RID: 83132 RVA: 0x00311BA2 File Offset: 0x0030FDA2
		internal override string[] ElementTagNames
		{
			get
			{
				return GraphicFrame.eleTagNames;
			}
		}

		// Token: 0x170066FF RID: 26367
		// (get) Token: 0x060144BD RID: 83133 RVA: 0x00311BA9 File Offset: 0x0030FDA9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GraphicFrame.eleNamespaceIds;
			}
		}

		// Token: 0x17006700 RID: 26368
		// (get) Token: 0x060144BE RID: 83134 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006701 RID: 26369
		// (get) Token: 0x060144BF RID: 83135 RVA: 0x00311BB0 File Offset: 0x0030FDB0
		// (set) Token: 0x060144C0 RID: 83136 RVA: 0x00311BB9 File Offset: 0x0030FDB9
		public NonVisualGraphicFrameProperties NonVisualGraphicFrameProperties
		{
			get
			{
				return base.GetElement<NonVisualGraphicFrameProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualGraphicFrameProperties>(0, value);
			}
		}

		// Token: 0x17006702 RID: 26370
		// (get) Token: 0x060144C1 RID: 83137 RVA: 0x00311BC3 File Offset: 0x0030FDC3
		// (set) Token: 0x060144C2 RID: 83138 RVA: 0x00311BCC File Offset: 0x0030FDCC
		public Transform Transform
		{
			get
			{
				return base.GetElement<Transform>(1);
			}
			set
			{
				base.SetElement<Transform>(1, value);
			}
		}

		// Token: 0x17006703 RID: 26371
		// (get) Token: 0x060144C3 RID: 83139 RVA: 0x002FB80A File Offset: 0x002F9A0A
		// (set) Token: 0x060144C4 RID: 83140 RVA: 0x002FB813 File Offset: 0x002F9A13
		public Graphic Graphic
		{
			get
			{
				return base.GetElement<Graphic>(2);
			}
			set
			{
				base.SetElement<Graphic>(2, value);
			}
		}

		// Token: 0x060144C5 RID: 83141 RVA: 0x002DFFB5 File Offset: 0x002DE1B5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "macro" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fPublished" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060144C6 RID: 83142 RVA: 0x00311BD6 File Offset: 0x0030FDD6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GraphicFrame>(deep);
		}

		// Token: 0x060144C7 RID: 83143 RVA: 0x00311BE0 File Offset: 0x0030FDE0
		// Note: this type is marked as 'beforefieldinit'.
		static GraphicFrame()
		{
			byte[] array = new byte[2];
			GraphicFrame.attributeNamespaceIds = array;
			GraphicFrame.eleTagNames = new string[] { "nvGraphicFramePr", "xfrm", "graphic" };
			GraphicFrame.eleNamespaceIds = new byte[] { 18, 18, 10 };
		}

		// Token: 0x04008D74 RID: 36212
		private const string tagName = "graphicFrame";

		// Token: 0x04008D75 RID: 36213
		private const byte tagNsId = 18;

		// Token: 0x04008D76 RID: 36214
		internal const int ElementTypeIdConst = 10725;

		// Token: 0x04008D77 RID: 36215
		private static string[] attributeTagNames = new string[] { "macro", "fPublished" };

		// Token: 0x04008D78 RID: 36216
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008D79 RID: 36217
		private static readonly string[] eleTagNames;

		// Token: 0x04008D7A RID: 36218
		private static readonly byte[] eleNamespaceIds;
	}
}
