using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024A8 RID: 9384
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LightRig), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Camera), FileFormatVersions.Office2010)]
	internal class Scene3D : OpenXmlCompositeElement
	{
		// Token: 0x1700521C RID: 21020
		// (get) Token: 0x060115B9 RID: 71097 RVA: 0x002EDA54 File Offset: 0x002EBC54
		public override string LocalName
		{
			get
			{
				return "scene3d";
			}
		}

		// Token: 0x1700521D RID: 21021
		// (get) Token: 0x060115BA RID: 71098 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700521E RID: 21022
		// (get) Token: 0x060115BB RID: 71099 RVA: 0x002EDA5B File Offset: 0x002EBC5B
		internal override int ElementTypeId
		{
			get
			{
				return 12858;
			}
		}

		// Token: 0x060115BC RID: 71100 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060115BD RID: 71101 RVA: 0x00293ECF File Offset: 0x002920CF
		public Scene3D()
		{
		}

		// Token: 0x060115BE RID: 71102 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Scene3D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060115BF RID: 71103 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Scene3D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060115C0 RID: 71104 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Scene3D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060115C1 RID: 71105 RVA: 0x002EDA62 File Offset: 0x002EBC62
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "camera" == name)
			{
				return new Camera();
			}
			if (52 == namespaceId && "lightRig" == name)
			{
				return new LightRig();
			}
			return null;
		}

		// Token: 0x1700521F RID: 21023
		// (get) Token: 0x060115C2 RID: 71106 RVA: 0x002EDA95 File Offset: 0x002EBC95
		internal override string[] ElementTagNames
		{
			get
			{
				return Scene3D.eleTagNames;
			}
		}

		// Token: 0x17005220 RID: 21024
		// (get) Token: 0x060115C3 RID: 71107 RVA: 0x002EDA9C File Offset: 0x002EBC9C
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Scene3D.eleNamespaceIds;
			}
		}

		// Token: 0x17005221 RID: 21025
		// (get) Token: 0x060115C4 RID: 71108 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005222 RID: 21026
		// (get) Token: 0x060115C5 RID: 71109 RVA: 0x002EDAA3 File Offset: 0x002EBCA3
		// (set) Token: 0x060115C6 RID: 71110 RVA: 0x002EDAAC File Offset: 0x002EBCAC
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

		// Token: 0x17005223 RID: 21027
		// (get) Token: 0x060115C7 RID: 71111 RVA: 0x002EDAB6 File Offset: 0x002EBCB6
		// (set) Token: 0x060115C8 RID: 71112 RVA: 0x002EDABF File Offset: 0x002EBCBF
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

		// Token: 0x060115C9 RID: 71113 RVA: 0x002EDAC9 File Offset: 0x002EBCC9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Scene3D>(deep);
		}

		// Token: 0x0400796E RID: 31086
		private const string tagName = "scene3d";

		// Token: 0x0400796F RID: 31087
		private const byte tagNsId = 52;

		// Token: 0x04007970 RID: 31088
		internal const int ElementTypeIdConst = 12858;

		// Token: 0x04007971 RID: 31089
		private static readonly string[] eleTagNames = new string[] { "camera", "lightRig" };

		// Token: 0x04007972 RID: 31090
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52 };
	}
}
