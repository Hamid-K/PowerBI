using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002772 RID: 10098
	[ChildElementInfo(typeof(Camera))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LightRig))]
	[ChildElementInfo(typeof(Backdrop))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Scene3DType : OpenXmlCompositeElement
	{
		// Token: 0x1700615A RID: 24922
		// (get) Token: 0x060137AA RID: 79786 RVA: 0x002EDA54 File Offset: 0x002EBC54
		public override string LocalName
		{
			get
			{
				return "scene3d";
			}
		}

		// Token: 0x1700615B RID: 24923
		// (get) Token: 0x060137AB RID: 79787 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700615C RID: 24924
		// (get) Token: 0x060137AC RID: 79788 RVA: 0x00307817 File Offset: 0x00305A17
		internal override int ElementTypeId
		{
			get
			{
				return 10135;
			}
		}

		// Token: 0x060137AD RID: 79789 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060137AE RID: 79790 RVA: 0x00293ECF File Offset: 0x002920CF
		public Scene3DType()
		{
		}

		// Token: 0x060137AF RID: 79791 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Scene3DType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137B0 RID: 79792 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Scene3DType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137B1 RID: 79793 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Scene3DType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060137B2 RID: 79794 RVA: 0x00307820 File Offset: 0x00305A20
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

		// Token: 0x1700615D RID: 24925
		// (get) Token: 0x060137B3 RID: 79795 RVA: 0x0030788E File Offset: 0x00305A8E
		internal override string[] ElementTagNames
		{
			get
			{
				return Scene3DType.eleTagNames;
			}
		}

		// Token: 0x1700615E RID: 24926
		// (get) Token: 0x060137B4 RID: 79796 RVA: 0x00307895 File Offset: 0x00305A95
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Scene3DType.eleNamespaceIds;
			}
		}

		// Token: 0x1700615F RID: 24927
		// (get) Token: 0x060137B5 RID: 79797 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006160 RID: 24928
		// (get) Token: 0x060137B6 RID: 79798 RVA: 0x002E0BF0 File Offset: 0x002DEDF0
		// (set) Token: 0x060137B7 RID: 79799 RVA: 0x002E0BF9 File Offset: 0x002DEDF9
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

		// Token: 0x17006161 RID: 24929
		// (get) Token: 0x060137B8 RID: 79800 RVA: 0x002E0C03 File Offset: 0x002DEE03
		// (set) Token: 0x060137B9 RID: 79801 RVA: 0x002E0C0C File Offset: 0x002DEE0C
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

		// Token: 0x17006162 RID: 24930
		// (get) Token: 0x060137BA RID: 79802 RVA: 0x002E0C16 File Offset: 0x002DEE16
		// (set) Token: 0x060137BB RID: 79803 RVA: 0x002E0C1F File Offset: 0x002DEE1F
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

		// Token: 0x17006163 RID: 24931
		// (get) Token: 0x060137BC RID: 79804 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x060137BD RID: 79805 RVA: 0x002E0C32 File Offset: 0x002DEE32
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

		// Token: 0x060137BE RID: 79806 RVA: 0x0030789C File Offset: 0x00305A9C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Scene3DType>(deep);
		}

		// Token: 0x04008666 RID: 34406
		private const string tagName = "scene3d";

		// Token: 0x04008667 RID: 34407
		private const byte tagNsId = 10;

		// Token: 0x04008668 RID: 34408
		internal const int ElementTypeIdConst = 10135;

		// Token: 0x04008669 RID: 34409
		private static readonly string[] eleTagNames = new string[] { "camera", "lightRig", "backdrop", "extLst" };

		// Token: 0x0400866A RID: 34410
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
