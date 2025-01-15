using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200234B RID: 9035
	[ChildElementInfo(typeof(ExtensionList))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Camera))]
	[ChildElementInfo(typeof(LightRig))]
	[ChildElementInfo(typeof(Backdrop))]
	[GeneratedCode("DomGen", "2.0")]
	internal class HiddenScene3D : OpenXmlCompositeElement
	{
		// Token: 0x170049AD RID: 18861
		// (get) Token: 0x060102EE RID: 66286 RVA: 0x002E0B65 File Offset: 0x002DED65
		public override string LocalName
		{
			get
			{
				return "hiddenScene3d";
			}
		}

		// Token: 0x170049AE RID: 18862
		// (get) Token: 0x060102EF RID: 66287 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x170049AF RID: 18863
		// (get) Token: 0x060102F0 RID: 66288 RVA: 0x002E0B6C File Offset: 0x002DED6C
		internal override int ElementTypeId
		{
			get
			{
				return 12720;
			}
		}

		// Token: 0x060102F1 RID: 66289 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060102F2 RID: 66290 RVA: 0x00293ECF File Offset: 0x002920CF
		public HiddenScene3D()
		{
		}

		// Token: 0x060102F3 RID: 66291 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HiddenScene3D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060102F4 RID: 66292 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HiddenScene3D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060102F5 RID: 66293 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HiddenScene3D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060102F6 RID: 66294 RVA: 0x002E0B74 File Offset: 0x002DED74
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "camera" == name)
			{
				return new Camera();
			}
			if (10 == namespaceId && "lightRig" == name)
			{
				return new LightRig();
			}
			if (10 == namespaceId && "backdrop" == name)
			{
				return new Backdrop();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170049B0 RID: 18864
		// (get) Token: 0x060102F7 RID: 66295 RVA: 0x002E0BE2 File Offset: 0x002DEDE2
		internal override string[] ElementTagNames
		{
			get
			{
				return HiddenScene3D.eleTagNames;
			}
		}

		// Token: 0x170049B1 RID: 18865
		// (get) Token: 0x060102F8 RID: 66296 RVA: 0x002E0BE9 File Offset: 0x002DEDE9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HiddenScene3D.eleNamespaceIds;
			}
		}

		// Token: 0x170049B2 RID: 18866
		// (get) Token: 0x060102F9 RID: 66297 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170049B3 RID: 18867
		// (get) Token: 0x060102FA RID: 66298 RVA: 0x002E0BF0 File Offset: 0x002DEDF0
		// (set) Token: 0x060102FB RID: 66299 RVA: 0x002E0BF9 File Offset: 0x002DEDF9
		public Camera Camera
		{
			get
			{
				return base.GetElement<Camera>(0);
			}
			set
			{
				base.SetElement<Camera>(0, value);
			}
		}

		// Token: 0x170049B4 RID: 18868
		// (get) Token: 0x060102FC RID: 66300 RVA: 0x002E0C03 File Offset: 0x002DEE03
		// (set) Token: 0x060102FD RID: 66301 RVA: 0x002E0C0C File Offset: 0x002DEE0C
		public LightRig LightRig
		{
			get
			{
				return base.GetElement<LightRig>(1);
			}
			set
			{
				base.SetElement<LightRig>(1, value);
			}
		}

		// Token: 0x170049B5 RID: 18869
		// (get) Token: 0x060102FE RID: 66302 RVA: 0x002E0C16 File Offset: 0x002DEE16
		// (set) Token: 0x060102FF RID: 66303 RVA: 0x002E0C1F File Offset: 0x002DEE1F
		public Backdrop Backdrop
		{
			get
			{
				return base.GetElement<Backdrop>(2);
			}
			set
			{
				base.SetElement<Backdrop>(2, value);
			}
		}

		// Token: 0x170049B6 RID: 18870
		// (get) Token: 0x06010300 RID: 66304 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x06010301 RID: 66305 RVA: 0x002E0C32 File Offset: 0x002DEE32
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(3);
			}
			set
			{
				base.SetElement<ExtensionList>(3, value);
			}
		}

		// Token: 0x06010302 RID: 66306 RVA: 0x002E0C3C File Offset: 0x002DEE3C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HiddenScene3D>(deep);
		}

		// Token: 0x04007372 RID: 29554
		private const string tagName = "hiddenScene3d";

		// Token: 0x04007373 RID: 29555
		private const byte tagNsId = 48;

		// Token: 0x04007374 RID: 29556
		internal const int ElementTypeIdConst = 12720;

		// Token: 0x04007375 RID: 29557
		private static readonly string[] eleTagNames = new string[] { "camera", "lightRig", "backdrop", "extLst" };

		// Token: 0x04007376 RID: 29558
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
