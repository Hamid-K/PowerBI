using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025C5 RID: 9669
	[ChildElementInfo(typeof(RotateX))]
	[ChildElementInfo(typeof(RightAngleAxes))]
	[ChildElementInfo(typeof(Perspective))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HeightPercent))]
	[ChildElementInfo(typeof(RotateY))]
	[ChildElementInfo(typeof(DepthPercent))]
	internal class View3D : OpenXmlCompositeElement
	{
		// Token: 0x1700579A RID: 22426
		// (get) Token: 0x060121EF RID: 74223 RVA: 0x002F5BFE File Offset: 0x002F3DFE
		public override string LocalName
		{
			get
			{
				return "view3D";
			}
		}

		// Token: 0x1700579B RID: 22427
		// (get) Token: 0x060121F0 RID: 74224 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700579C RID: 22428
		// (get) Token: 0x060121F1 RID: 74225 RVA: 0x002F5C05 File Offset: 0x002F3E05
		internal override int ElementTypeId
		{
			get
			{
				return 10496;
			}
		}

		// Token: 0x060121F2 RID: 74226 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060121F3 RID: 74227 RVA: 0x00293ECF File Offset: 0x002920CF
		public View3D()
		{
		}

		// Token: 0x060121F4 RID: 74228 RVA: 0x00293ED7 File Offset: 0x002920D7
		public View3D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060121F5 RID: 74229 RVA: 0x00293EE0 File Offset: 0x002920E0
		public View3D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060121F6 RID: 74230 RVA: 0x00293EE9 File Offset: 0x002920E9
		public View3D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060121F7 RID: 74231 RVA: 0x002F5C0C File Offset: 0x002F3E0C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "rotX" == name)
			{
				return new RotateX();
			}
			if (11 == namespaceId && "hPercent" == name)
			{
				return new HeightPercent();
			}
			if (11 == namespaceId && "rotY" == name)
			{
				return new RotateY();
			}
			if (11 == namespaceId && "depthPercent" == name)
			{
				return new DepthPercent();
			}
			if (11 == namespaceId && "rAngAx" == name)
			{
				return new RightAngleAxes();
			}
			if (11 == namespaceId && "perspective" == name)
			{
				return new Perspective();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700579D RID: 22429
		// (get) Token: 0x060121F8 RID: 74232 RVA: 0x002F5CC2 File Offset: 0x002F3EC2
		internal override string[] ElementTagNames
		{
			get
			{
				return View3D.eleTagNames;
			}
		}

		// Token: 0x1700579E RID: 22430
		// (get) Token: 0x060121F9 RID: 74233 RVA: 0x002F5CC9 File Offset: 0x002F3EC9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return View3D.eleNamespaceIds;
			}
		}

		// Token: 0x1700579F RID: 22431
		// (get) Token: 0x060121FA RID: 74234 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170057A0 RID: 22432
		// (get) Token: 0x060121FB RID: 74235 RVA: 0x002F5CD0 File Offset: 0x002F3ED0
		// (set) Token: 0x060121FC RID: 74236 RVA: 0x002F5CD9 File Offset: 0x002F3ED9
		public RotateX RotateX
		{
			get
			{
				return base.GetElement<RotateX>(0);
			}
			set
			{
				base.SetElement<RotateX>(0, value);
			}
		}

		// Token: 0x170057A1 RID: 22433
		// (get) Token: 0x060121FD RID: 74237 RVA: 0x002F5CE3 File Offset: 0x002F3EE3
		// (set) Token: 0x060121FE RID: 74238 RVA: 0x002F5CEC File Offset: 0x002F3EEC
		public HeightPercent HeightPercent
		{
			get
			{
				return base.GetElement<HeightPercent>(1);
			}
			set
			{
				base.SetElement<HeightPercent>(1, value);
			}
		}

		// Token: 0x170057A2 RID: 22434
		// (get) Token: 0x060121FF RID: 74239 RVA: 0x002F5CF6 File Offset: 0x002F3EF6
		// (set) Token: 0x06012200 RID: 74240 RVA: 0x002F5CFF File Offset: 0x002F3EFF
		public RotateY RotateY
		{
			get
			{
				return base.GetElement<RotateY>(2);
			}
			set
			{
				base.SetElement<RotateY>(2, value);
			}
		}

		// Token: 0x170057A3 RID: 22435
		// (get) Token: 0x06012201 RID: 74241 RVA: 0x002F5D09 File Offset: 0x002F3F09
		// (set) Token: 0x06012202 RID: 74242 RVA: 0x002F5D12 File Offset: 0x002F3F12
		public DepthPercent DepthPercent
		{
			get
			{
				return base.GetElement<DepthPercent>(3);
			}
			set
			{
				base.SetElement<DepthPercent>(3, value);
			}
		}

		// Token: 0x170057A4 RID: 22436
		// (get) Token: 0x06012203 RID: 74243 RVA: 0x002F5D1C File Offset: 0x002F3F1C
		// (set) Token: 0x06012204 RID: 74244 RVA: 0x002F5D25 File Offset: 0x002F3F25
		public RightAngleAxes RightAngleAxes
		{
			get
			{
				return base.GetElement<RightAngleAxes>(4);
			}
			set
			{
				base.SetElement<RightAngleAxes>(4, value);
			}
		}

		// Token: 0x170057A5 RID: 22437
		// (get) Token: 0x06012205 RID: 74245 RVA: 0x002F5D2F File Offset: 0x002F3F2F
		// (set) Token: 0x06012206 RID: 74246 RVA: 0x002F5D38 File Offset: 0x002F3F38
		public Perspective Perspective
		{
			get
			{
				return base.GetElement<Perspective>(5);
			}
			set
			{
				base.SetElement<Perspective>(5, value);
			}
		}

		// Token: 0x170057A6 RID: 22438
		// (get) Token: 0x06012207 RID: 74247 RVA: 0x002F5D42 File Offset: 0x002F3F42
		// (set) Token: 0x06012208 RID: 74248 RVA: 0x002F5D4B File Offset: 0x002F3F4B
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(6);
			}
			set
			{
				base.SetElement<ExtensionList>(6, value);
			}
		}

		// Token: 0x06012209 RID: 74249 RVA: 0x002F5D55 File Offset: 0x002F3F55
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<View3D>(deep);
		}

		// Token: 0x04007E54 RID: 32340
		private const string tagName = "view3D";

		// Token: 0x04007E55 RID: 32341
		private const byte tagNsId = 11;

		// Token: 0x04007E56 RID: 32342
		internal const int ElementTypeIdConst = 10496;

		// Token: 0x04007E57 RID: 32343
		private static readonly string[] eleTagNames = new string[] { "rotX", "hPercent", "rotY", "depthPercent", "rAngAx", "perspective", "extLst" };

		// Token: 0x04007E58 RID: 32344
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11 };
	}
}
