using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027B2 RID: 10162
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Rotation))]
	internal class Camera : OpenXmlCompositeElement
	{
		// Token: 0x17006312 RID: 25362
		// (get) Token: 0x06013B8A RID: 80778 RVA: 0x002EEAD0 File Offset: 0x002ECCD0
		public override string LocalName
		{
			get
			{
				return "camera";
			}
		}

		// Token: 0x17006313 RID: 25363
		// (get) Token: 0x06013B8B RID: 80779 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006314 RID: 25364
		// (get) Token: 0x06013B8C RID: 80780 RVA: 0x0030B0CB File Offset: 0x003092CB
		internal override int ElementTypeId
		{
			get
			{
				return 10195;
			}
		}

		// Token: 0x06013B8D RID: 80781 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006315 RID: 25365
		// (get) Token: 0x06013B8E RID: 80782 RVA: 0x0030B0D2 File Offset: 0x003092D2
		internal override string[] AttributeTagNames
		{
			get
			{
				return Camera.attributeTagNames;
			}
		}

		// Token: 0x17006316 RID: 25366
		// (get) Token: 0x06013B8F RID: 80783 RVA: 0x0030B0D9 File Offset: 0x003092D9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Camera.attributeNamespaceIds;
			}
		}

		// Token: 0x17006317 RID: 25367
		// (get) Token: 0x06013B90 RID: 80784 RVA: 0x0030B0E0 File Offset: 0x003092E0
		// (set) Token: 0x06013B91 RID: 80785 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "prst")]
		public EnumValue<PresetCameraValues> Preset
		{
			get
			{
				return (EnumValue<PresetCameraValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006318 RID: 25368
		// (get) Token: 0x06013B92 RID: 80786 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06013B93 RID: 80787 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fov")]
		public Int32Value FieldOfView
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006319 RID: 25369
		// (get) Token: 0x06013B94 RID: 80788 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06013B95 RID: 80789 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "zoom")]
		public Int32Value Zoom
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06013B96 RID: 80790 RVA: 0x00293ECF File Offset: 0x002920CF
		public Camera()
		{
		}

		// Token: 0x06013B97 RID: 80791 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Camera(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B98 RID: 80792 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Camera(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B99 RID: 80793 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Camera(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013B9A RID: 80794 RVA: 0x0030B0EF File Offset: 0x003092EF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "rot" == name)
			{
				return new Rotation();
			}
			return null;
		}

		// Token: 0x1700631A RID: 25370
		// (get) Token: 0x06013B9B RID: 80795 RVA: 0x0030B10A File Offset: 0x0030930A
		internal override string[] ElementTagNames
		{
			get
			{
				return Camera.eleTagNames;
			}
		}

		// Token: 0x1700631B RID: 25371
		// (get) Token: 0x06013B9C RID: 80796 RVA: 0x0030B111 File Offset: 0x00309311
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Camera.eleNamespaceIds;
			}
		}

		// Token: 0x1700631C RID: 25372
		// (get) Token: 0x06013B9D RID: 80797 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700631D RID: 25373
		// (get) Token: 0x06013B9E RID: 80798 RVA: 0x0030B118 File Offset: 0x00309318
		// (set) Token: 0x06013B9F RID: 80799 RVA: 0x0030B121 File Offset: 0x00309321
		public Rotation Rotation
		{
			get
			{
				return base.GetElement<Rotation>(0);
			}
			set
			{
				base.SetElement<Rotation>(0, value);
			}
		}

		// Token: 0x06013BA0 RID: 80800 RVA: 0x0030B12C File Offset: 0x0030932C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "prst" == name)
			{
				return new EnumValue<PresetCameraValues>();
			}
			if (namespaceId == 0 && "fov" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "zoom" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013BA1 RID: 80801 RVA: 0x0030B183 File Offset: 0x00309383
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Camera>(deep);
		}

		// Token: 0x06013BA2 RID: 80802 RVA: 0x0030B18C File Offset: 0x0030938C
		// Note: this type is marked as 'beforefieldinit'.
		static Camera()
		{
			byte[] array = new byte[3];
			Camera.attributeNamespaceIds = array;
			Camera.eleTagNames = new string[] { "rot" };
			Camera.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x04008774 RID: 34676
		private const string tagName = "camera";

		// Token: 0x04008775 RID: 34677
		private const byte tagNsId = 10;

		// Token: 0x04008776 RID: 34678
		internal const int ElementTypeIdConst = 10195;

		// Token: 0x04008777 RID: 34679
		private static string[] attributeTagNames = new string[] { "prst", "fov", "zoom" };

		// Token: 0x04008778 RID: 34680
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008779 RID: 34681
		private static readonly string[] eleTagNames;

		// Token: 0x0400877A RID: 34682
		private static readonly byte[] eleNamespaceIds;
	}
}
