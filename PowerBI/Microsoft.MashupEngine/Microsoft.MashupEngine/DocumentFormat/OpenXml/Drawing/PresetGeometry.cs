using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002727 RID: 10023
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AdjustValueList))]
	internal class PresetGeometry : OpenXmlCompositeElement
	{
		// Token: 0x17005FCF RID: 24527
		// (get) Token: 0x06013411 RID: 78865 RVA: 0x003057B8 File Offset: 0x003039B8
		public override string LocalName
		{
			get
			{
				return "prstGeom";
			}
		}

		// Token: 0x17005FD0 RID: 24528
		// (get) Token: 0x06013412 RID: 78866 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FD1 RID: 24529
		// (get) Token: 0x06013413 RID: 78867 RVA: 0x003057BF File Offset: 0x003039BF
		internal override int ElementTypeId
		{
			get
			{
				return 10086;
			}
		}

		// Token: 0x06013414 RID: 78868 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005FD2 RID: 24530
		// (get) Token: 0x06013415 RID: 78869 RVA: 0x003057C6 File Offset: 0x003039C6
		internal override string[] AttributeTagNames
		{
			get
			{
				return PresetGeometry.attributeTagNames;
			}
		}

		// Token: 0x17005FD3 RID: 24531
		// (get) Token: 0x06013416 RID: 78870 RVA: 0x003057CD File Offset: 0x003039CD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PresetGeometry.attributeNamespaceIds;
			}
		}

		// Token: 0x17005FD4 RID: 24532
		// (get) Token: 0x06013417 RID: 78871 RVA: 0x003057D4 File Offset: 0x003039D4
		// (set) Token: 0x06013418 RID: 78872 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "prst")]
		public EnumValue<ShapeTypeValues> Preset
		{
			get
			{
				return (EnumValue<ShapeTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06013419 RID: 78873 RVA: 0x00293ECF File Offset: 0x002920CF
		public PresetGeometry()
		{
		}

		// Token: 0x0601341A RID: 78874 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PresetGeometry(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601341B RID: 78875 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PresetGeometry(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601341C RID: 78876 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PresetGeometry(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601341D RID: 78877 RVA: 0x003057E3 File Offset: 0x003039E3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "avLst" == name)
			{
				return new AdjustValueList();
			}
			return null;
		}

		// Token: 0x17005FD5 RID: 24533
		// (get) Token: 0x0601341E RID: 78878 RVA: 0x003057FE File Offset: 0x003039FE
		internal override string[] ElementTagNames
		{
			get
			{
				return PresetGeometry.eleTagNames;
			}
		}

		// Token: 0x17005FD6 RID: 24534
		// (get) Token: 0x0601341F RID: 78879 RVA: 0x00305805 File Offset: 0x00303A05
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PresetGeometry.eleNamespaceIds;
			}
		}

		// Token: 0x17005FD7 RID: 24535
		// (get) Token: 0x06013420 RID: 78880 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005FD8 RID: 24536
		// (get) Token: 0x06013421 RID: 78881 RVA: 0x003056DC File Offset: 0x003038DC
		// (set) Token: 0x06013422 RID: 78882 RVA: 0x003056E5 File Offset: 0x003038E5
		public AdjustValueList AdjustValueList
		{
			get
			{
				return base.GetElement<AdjustValueList>(0);
			}
			set
			{
				base.SetElement<AdjustValueList>(0, value);
			}
		}

		// Token: 0x06013423 RID: 78883 RVA: 0x0030580C File Offset: 0x00303A0C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "prst" == name)
			{
				return new EnumValue<ShapeTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013424 RID: 78884 RVA: 0x0030582C File Offset: 0x00303A2C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresetGeometry>(deep);
		}

		// Token: 0x06013425 RID: 78885 RVA: 0x00305838 File Offset: 0x00303A38
		// Note: this type is marked as 'beforefieldinit'.
		static PresetGeometry()
		{
			byte[] array = new byte[1];
			PresetGeometry.attributeNamespaceIds = array;
			PresetGeometry.eleTagNames = new string[] { "avLst" };
			PresetGeometry.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x0400854E RID: 34126
		private const string tagName = "prstGeom";

		// Token: 0x0400854F RID: 34127
		private const byte tagNsId = 10;

		// Token: 0x04008550 RID: 34128
		internal const int ElementTypeIdConst = 10086;

		// Token: 0x04008551 RID: 34129
		private static string[] attributeTagNames = new string[] { "prst" };

		// Token: 0x04008552 RID: 34130
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008553 RID: 34131
		private static readonly string[] eleTagNames;

		// Token: 0x04008554 RID: 34132
		private static readonly byte[] eleNamespaceIds;
	}
}
