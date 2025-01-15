using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027D1 RID: 10193
	[ChildElementInfo(typeof(CubicBezierCurveTo))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LineTo))]
	[ChildElementInfo(typeof(MoveTo))]
	[ChildElementInfo(typeof(QuadraticBezierCurveTo))]
	[ChildElementInfo(typeof(CloseShapePath))]
	[ChildElementInfo(typeof(ArcTo))]
	internal class Path : OpenXmlCompositeElement
	{
		// Token: 0x170063D1 RID: 25553
		// (get) Token: 0x06013D24 RID: 81188 RVA: 0x002BFFB6 File Offset: 0x002BE1B6
		public override string LocalName
		{
			get
			{
				return "path";
			}
		}

		// Token: 0x170063D2 RID: 25554
		// (get) Token: 0x06013D25 RID: 81189 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063D3 RID: 25555
		// (get) Token: 0x06013D26 RID: 81190 RVA: 0x0030BFE5 File Offset: 0x0030A1E5
		internal override int ElementTypeId
		{
			get
			{
				return 10227;
			}
		}

		// Token: 0x06013D27 RID: 81191 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170063D4 RID: 25556
		// (get) Token: 0x06013D28 RID: 81192 RVA: 0x0030BFEC File Offset: 0x0030A1EC
		internal override string[] AttributeTagNames
		{
			get
			{
				return Path.attributeTagNames;
			}
		}

		// Token: 0x170063D5 RID: 25557
		// (get) Token: 0x06013D29 RID: 81193 RVA: 0x0030BFF3 File Offset: 0x0030A1F3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Path.attributeNamespaceIds;
			}
		}

		// Token: 0x170063D6 RID: 25558
		// (get) Token: 0x06013D2A RID: 81194 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06013D2B RID: 81195 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "w")]
		public Int64Value Width
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170063D7 RID: 25559
		// (get) Token: 0x06013D2C RID: 81196 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x06013D2D RID: 81197 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "h")]
		public Int64Value Height
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170063D8 RID: 25560
		// (get) Token: 0x06013D2E RID: 81198 RVA: 0x0030BFFA File Offset: 0x0030A1FA
		// (set) Token: 0x06013D2F RID: 81199 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "fill")]
		public EnumValue<PathFillModeValues> Fill
		{
			get
			{
				return (EnumValue<PathFillModeValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170063D9 RID: 25561
		// (get) Token: 0x06013D30 RID: 81200 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06013D31 RID: 81201 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "stroke")]
		public BooleanValue Stroke
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170063DA RID: 25562
		// (get) Token: 0x06013D32 RID: 81202 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06013D33 RID: 81203 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "extrusionOk")]
		public BooleanValue ExtrusionOk
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06013D34 RID: 81204 RVA: 0x00293ECF File Offset: 0x002920CF
		public Path()
		{
		}

		// Token: 0x06013D35 RID: 81205 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Path(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D36 RID: 81206 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Path(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D37 RID: 81207 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Path(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013D38 RID: 81208 RVA: 0x0030C00C File Offset: 0x0030A20C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "close" == name)
			{
				return new CloseShapePath();
			}
			if (10 == namespaceId && "moveTo" == name)
			{
				return new MoveTo();
			}
			if (10 == namespaceId && "lnTo" == name)
			{
				return new LineTo();
			}
			if (10 == namespaceId && "arcTo" == name)
			{
				return new ArcTo();
			}
			if (10 == namespaceId && "quadBezTo" == name)
			{
				return new QuadraticBezierCurveTo();
			}
			if (10 == namespaceId && "cubicBezTo" == name)
			{
				return new CubicBezierCurveTo();
			}
			return null;
		}

		// Token: 0x06013D39 RID: 81209 RVA: 0x0030C0AC File Offset: 0x0030A2AC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "w" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "h" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "fill" == name)
			{
				return new EnumValue<PathFillModeValues>();
			}
			if (namespaceId == 0 && "stroke" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "extrusionOk" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013D3A RID: 81210 RVA: 0x0030C12F File Offset: 0x0030A32F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Path>(deep);
		}

		// Token: 0x06013D3B RID: 81211 RVA: 0x0030C138 File Offset: 0x0030A338
		// Note: this type is marked as 'beforefieldinit'.
		static Path()
		{
			byte[] array = new byte[5];
			Path.attributeNamespaceIds = array;
		}

		// Token: 0x040087F1 RID: 34801
		private const string tagName = "path";

		// Token: 0x040087F2 RID: 34802
		private const byte tagNsId = 10;

		// Token: 0x040087F3 RID: 34803
		internal const int ElementTypeIdConst = 10227;

		// Token: 0x040087F4 RID: 34804
		private static string[] attributeTagNames = new string[] { "w", "h", "fill", "stroke", "extrusionOk" };

		// Token: 0x040087F5 RID: 34805
		private static byte[] attributeNamespaceIds;
	}
}
