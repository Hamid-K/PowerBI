using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200268A RID: 9866
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Camera))]
	[ChildElementInfo(typeof(LightRig))]
	[ChildElementInfo(typeof(Backdrop))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Scene3D : OpenXmlCompositeElement
	{
		// Token: 0x17005CDE RID: 23774
		// (get) Token: 0x06012DC7 RID: 77255 RVA: 0x002EDA54 File Offset: 0x002EBC54
		public override string LocalName
		{
			get
			{
				return "scene3d";
			}
		}

		// Token: 0x17005CDF RID: 23775
		// (get) Token: 0x06012DC8 RID: 77256 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CE0 RID: 23776
		// (get) Token: 0x06012DC9 RID: 77257 RVA: 0x0030020F File Offset: 0x002FE40F
		internal override int ElementTypeId
		{
			get
			{
				return 10681;
			}
		}

		// Token: 0x06012DCA RID: 77258 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012DCB RID: 77259 RVA: 0x00293ECF File Offset: 0x002920CF
		public Scene3D()
		{
		}

		// Token: 0x06012DCC RID: 77260 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Scene3D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012DCD RID: 77261 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Scene3D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012DCE RID: 77262 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Scene3D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012DCF RID: 77263 RVA: 0x00300218 File Offset: 0x002FE418
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

		// Token: 0x17005CE1 RID: 23777
		// (get) Token: 0x06012DD0 RID: 77264 RVA: 0x00300286 File Offset: 0x002FE486
		internal override string[] ElementTagNames
		{
			get
			{
				return Scene3D.eleTagNames;
			}
		}

		// Token: 0x17005CE2 RID: 23778
		// (get) Token: 0x06012DD1 RID: 77265 RVA: 0x0030028D File Offset: 0x002FE48D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Scene3D.eleNamespaceIds;
			}
		}

		// Token: 0x17005CE3 RID: 23779
		// (get) Token: 0x06012DD2 RID: 77266 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005CE4 RID: 23780
		// (get) Token: 0x06012DD3 RID: 77267 RVA: 0x002E0BF0 File Offset: 0x002DEDF0
		// (set) Token: 0x06012DD4 RID: 77268 RVA: 0x002E0BF9 File Offset: 0x002DEDF9
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

		// Token: 0x17005CE5 RID: 23781
		// (get) Token: 0x06012DD5 RID: 77269 RVA: 0x002E0C03 File Offset: 0x002DEE03
		// (set) Token: 0x06012DD6 RID: 77270 RVA: 0x002E0C0C File Offset: 0x002DEE0C
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

		// Token: 0x17005CE6 RID: 23782
		// (get) Token: 0x06012DD7 RID: 77271 RVA: 0x002E0C16 File Offset: 0x002DEE16
		// (set) Token: 0x06012DD8 RID: 77272 RVA: 0x002E0C1F File Offset: 0x002DEE1F
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

		// Token: 0x17005CE7 RID: 23783
		// (get) Token: 0x06012DD9 RID: 77273 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x06012DDA RID: 77274 RVA: 0x002E0C32 File Offset: 0x002DEE32
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

		// Token: 0x06012DDB RID: 77275 RVA: 0x00300294 File Offset: 0x002FE494
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Scene3D>(deep);
		}

		// Token: 0x040081F8 RID: 33272
		private const string tagName = "scene3d";

		// Token: 0x040081F9 RID: 33273
		private const byte tagNsId = 14;

		// Token: 0x040081FA RID: 33274
		internal const int ElementTypeIdConst = 10681;

		// Token: 0x040081FB RID: 33275
		private static readonly string[] eleTagNames = new string[] { "camera", "lightRig", "backdrop", "extLst" };

		// Token: 0x040081FC RID: 33276
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
