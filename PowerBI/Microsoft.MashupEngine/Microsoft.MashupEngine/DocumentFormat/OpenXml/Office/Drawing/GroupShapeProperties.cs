using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x02002330 RID: 9008
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(TransformGroup))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	internal class GroupShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x170048D9 RID: 18649
		// (get) Token: 0x06010119 RID: 65817 RVA: 0x002DF441 File Offset: 0x002DD641
		public override string LocalName
		{
			get
			{
				return "grpSpPr";
			}
		}

		// Token: 0x170048DA RID: 18650
		// (get) Token: 0x0601011A RID: 65818 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x170048DB RID: 18651
		// (get) Token: 0x0601011B RID: 65819 RVA: 0x002DF448 File Offset: 0x002DD648
		internal override int ElementTypeId
		{
			get
			{
				return 13031;
			}
		}

		// Token: 0x0601011C RID: 65820 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170048DC RID: 18652
		// (get) Token: 0x0601011D RID: 65821 RVA: 0x002DF44F File Offset: 0x002DD64F
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x170048DD RID: 18653
		// (get) Token: 0x0601011E RID: 65822 RVA: 0x002DF456 File Offset: 0x002DD656
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170048DE RID: 18654
		// (get) Token: 0x0601011F RID: 65823 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06010120 RID: 65824 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "bwMode")]
		public EnumValue<BlackWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackWhiteModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010121 RID: 65825 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupShapeProperties()
		{
		}

		// Token: 0x06010122 RID: 65826 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010123 RID: 65827 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010124 RID: 65828 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010125 RID: 65829 RVA: 0x002DF460 File Offset: 0x002DD660
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "xfrm" == name)
			{
				return new TransformGroup();
			}
			if (10 == namespaceId && "noFill" == name)
			{
				return new NoFill();
			}
			if (10 == namespaceId && "solidFill" == name)
			{
				return new SolidFill();
			}
			if (10 == namespaceId && "gradFill" == name)
			{
				return new GradientFill();
			}
			if (10 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (10 == namespaceId && "pattFill" == name)
			{
				return new PatternFill();
			}
			if (10 == namespaceId && "grpFill" == name)
			{
				return new GroupFill();
			}
			if (10 == namespaceId && "effectLst" == name)
			{
				return new EffectList();
			}
			if (10 == namespaceId && "effectDag" == name)
			{
				return new EffectDag();
			}
			if (10 == namespaceId && "scene3d" == name)
			{
				return new Scene3DType();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170048DF RID: 18655
		// (get) Token: 0x06010126 RID: 65830 RVA: 0x002DF576 File Offset: 0x002DD776
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShapeProperties.eleTagNames;
			}
		}

		// Token: 0x170048E0 RID: 18656
		// (get) Token: 0x06010127 RID: 65831 RVA: 0x002DF57D File Offset: 0x002DD77D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170048E1 RID: 18657
		// (get) Token: 0x06010128 RID: 65832 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170048E2 RID: 18658
		// (get) Token: 0x06010129 RID: 65833 RVA: 0x002DF584 File Offset: 0x002DD784
		// (set) Token: 0x0601012A RID: 65834 RVA: 0x002DF58D File Offset: 0x002DD78D
		public TransformGroup TransformGroup
		{
			get
			{
				return base.GetElement<TransformGroup>(0);
			}
			set
			{
				base.SetElement<TransformGroup>(0, value);
			}
		}

		// Token: 0x0601012B RID: 65835 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601012C RID: 65836 RVA: 0x002DF597 File Offset: 0x002DD797
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShapeProperties>(deep);
		}

		// Token: 0x0601012D RID: 65837 RVA: 0x002DF5A0 File Offset: 0x002DD7A0
		// Note: this type is marked as 'beforefieldinit'.
		static GroupShapeProperties()
		{
			byte[] array = new byte[1];
			GroupShapeProperties.attributeNamespaceIds = array;
			GroupShapeProperties.eleTagNames = new string[]
			{
				"xfrm", "noFill", "solidFill", "gradFill", "blipFill", "pattFill", "grpFill", "effectLst", "effectDag", "scene3d",
				"extLst"
			};
			GroupShapeProperties.eleNamespaceIds = new byte[]
			{
				10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
				10
			};
		}

		// Token: 0x040072F2 RID: 29426
		private const string tagName = "grpSpPr";

		// Token: 0x040072F3 RID: 29427
		private const byte tagNsId = 56;

		// Token: 0x040072F4 RID: 29428
		internal const int ElementTypeIdConst = 13031;

		// Token: 0x040072F5 RID: 29429
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x040072F6 RID: 29430
		private static byte[] attributeNamespaceIds;

		// Token: 0x040072F7 RID: 29431
		private static readonly string[] eleTagNames;

		// Token: 0x040072F8 RID: 29432
		private static readonly byte[] eleNamespaceIds;
	}
}
