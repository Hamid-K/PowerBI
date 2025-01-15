using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200287C RID: 10364
	[ChildElementInfo(typeof(NonVisualConnectionShapeProperties))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(ShapeStyle))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConnectionShape : OpenXmlCompositeElement
	{
		// Token: 0x17006704 RID: 26372
		// (get) Token: 0x060144C8 RID: 83144 RVA: 0x002FB89A File Offset: 0x002F9A9A
		public override string LocalName
		{
			get
			{
				return "cxnSp";
			}
		}

		// Token: 0x17006705 RID: 26373
		// (get) Token: 0x060144C9 RID: 83145 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006706 RID: 26374
		// (get) Token: 0x060144CA RID: 83146 RVA: 0x00311C52 File Offset: 0x0030FE52
		internal override int ElementTypeId
		{
			get
			{
				return 10726;
			}
		}

		// Token: 0x060144CB RID: 83147 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006707 RID: 26375
		// (get) Token: 0x060144CC RID: 83148 RVA: 0x00311C59 File Offset: 0x0030FE59
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConnectionShape.attributeTagNames;
			}
		}

		// Token: 0x17006708 RID: 26376
		// (get) Token: 0x060144CD RID: 83149 RVA: 0x00311C60 File Offset: 0x0030FE60
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConnectionShape.attributeNamespaceIds;
			}
		}

		// Token: 0x17006709 RID: 26377
		// (get) Token: 0x060144CE RID: 83150 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060144CF RID: 83151 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700670A RID: 26378
		// (get) Token: 0x060144D0 RID: 83152 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060144D1 RID: 83153 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x060144D2 RID: 83154 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConnectionShape()
		{
		}

		// Token: 0x060144D3 RID: 83155 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConnectionShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060144D4 RID: 83156 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConnectionShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060144D5 RID: 83157 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConnectionShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060144D6 RID: 83158 RVA: 0x00311C68 File Offset: 0x0030FE68
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "nvCxnSpPr" == name)
			{
				return new NonVisualConnectionShapeProperties();
			}
			if (18 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (18 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			return null;
		}

		// Token: 0x1700670B RID: 26379
		// (get) Token: 0x060144D7 RID: 83159 RVA: 0x00311CBE File Offset: 0x0030FEBE
		internal override string[] ElementTagNames
		{
			get
			{
				return ConnectionShape.eleTagNames;
			}
		}

		// Token: 0x1700670C RID: 26380
		// (get) Token: 0x060144D8 RID: 83160 RVA: 0x00311CC5 File Offset: 0x0030FEC5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ConnectionShape.eleNamespaceIds;
			}
		}

		// Token: 0x1700670D RID: 26381
		// (get) Token: 0x060144D9 RID: 83161 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700670E RID: 26382
		// (get) Token: 0x060144DA RID: 83162 RVA: 0x00311CCC File Offset: 0x0030FECC
		// (set) Token: 0x060144DB RID: 83163 RVA: 0x00311CD5 File Offset: 0x0030FED5
		public NonVisualConnectionShapeProperties NonVisualConnectionShapeProperties
		{
			get
			{
				return base.GetElement<NonVisualConnectionShapeProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualConnectionShapeProperties>(0, value);
			}
		}

		// Token: 0x1700670F RID: 26383
		// (get) Token: 0x060144DC RID: 83164 RVA: 0x00311873 File Offset: 0x0030FA73
		// (set) Token: 0x060144DD RID: 83165 RVA: 0x0031187C File Offset: 0x0030FA7C
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(1);
			}
			set
			{
				base.SetElement<ShapeProperties>(1, value);
			}
		}

		// Token: 0x17006710 RID: 26384
		// (get) Token: 0x060144DE RID: 83166 RVA: 0x00311886 File Offset: 0x0030FA86
		// (set) Token: 0x060144DF RID: 83167 RVA: 0x0031188F File Offset: 0x0030FA8F
		public ShapeStyle ShapeStyle
		{
			get
			{
				return base.GetElement<ShapeStyle>(2);
			}
			set
			{
				base.SetElement<ShapeStyle>(2, value);
			}
		}

		// Token: 0x060144E0 RID: 83168 RVA: 0x002DFFB5 File Offset: 0x002DE1B5
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

		// Token: 0x060144E1 RID: 83169 RVA: 0x00311CDF File Offset: 0x0030FEDF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConnectionShape>(deep);
		}

		// Token: 0x060144E2 RID: 83170 RVA: 0x00311CE8 File Offset: 0x0030FEE8
		// Note: this type is marked as 'beforefieldinit'.
		static ConnectionShape()
		{
			byte[] array = new byte[2];
			ConnectionShape.attributeNamespaceIds = array;
			ConnectionShape.eleTagNames = new string[] { "nvCxnSpPr", "spPr", "style" };
			ConnectionShape.eleNamespaceIds = new byte[] { 18, 18, 18 };
		}

		// Token: 0x04008D7B RID: 36219
		private const string tagName = "cxnSp";

		// Token: 0x04008D7C RID: 36220
		private const byte tagNsId = 18;

		// Token: 0x04008D7D RID: 36221
		internal const int ElementTypeIdConst = 10726;

		// Token: 0x04008D7E RID: 36222
		private static string[] attributeTagNames = new string[] { "macro", "fPublished" };

		// Token: 0x04008D7F RID: 36223
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008D80 RID: 36224
		private static readonly string[] eleTagNames;

		// Token: 0x04008D81 RID: 36225
		private static readonly byte[] eleNamespaceIds;
	}
}
