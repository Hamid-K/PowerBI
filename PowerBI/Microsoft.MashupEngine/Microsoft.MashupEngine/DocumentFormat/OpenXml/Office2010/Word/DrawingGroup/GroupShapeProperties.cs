using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingGroup
{
	// Token: 0x020024F7 RID: 9463
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(TransformGroup))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class GroupShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005385 RID: 21381
		// (get) Token: 0x060118E8 RID: 71912 RVA: 0x002DF441 File Offset: 0x002DD641
		public override string LocalName
		{
			get
			{
				return "grpSpPr";
			}
		}

		// Token: 0x17005386 RID: 21382
		// (get) Token: 0x060118E9 RID: 71913 RVA: 0x002EF715 File Offset: 0x002ED915
		internal override byte NamespaceId
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x17005387 RID: 21383
		// (get) Token: 0x060118EA RID: 71914 RVA: 0x002EFB05 File Offset: 0x002EDD05
		internal override int ElementTypeId
		{
			get
			{
				return 13128;
			}
		}

		// Token: 0x060118EB RID: 71915 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005388 RID: 21384
		// (get) Token: 0x060118EC RID: 71916 RVA: 0x002EFB0C File Offset: 0x002EDD0C
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x17005389 RID: 21385
		// (get) Token: 0x060118ED RID: 71917 RVA: 0x002EFB13 File Offset: 0x002EDD13
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700538A RID: 21386
		// (get) Token: 0x060118EE RID: 71918 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x060118EF RID: 71919 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060118F0 RID: 71920 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupShapeProperties()
		{
		}

		// Token: 0x060118F1 RID: 71921 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060118F2 RID: 71922 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060118F3 RID: 71923 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060118F4 RID: 71924 RVA: 0x002EFB1C File Offset: 0x002EDD1C
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

		// Token: 0x1700538B RID: 21387
		// (get) Token: 0x060118F5 RID: 71925 RVA: 0x002EFC32 File Offset: 0x002EDE32
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShapeProperties.eleTagNames;
			}
		}

		// Token: 0x1700538C RID: 21388
		// (get) Token: 0x060118F6 RID: 71926 RVA: 0x002EFC39 File Offset: 0x002EDE39
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700538D RID: 21389
		// (get) Token: 0x060118F7 RID: 71927 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700538E RID: 21390
		// (get) Token: 0x060118F8 RID: 71928 RVA: 0x002DF584 File Offset: 0x002DD784
		// (set) Token: 0x060118F9 RID: 71929 RVA: 0x002DF58D File Offset: 0x002DD78D
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

		// Token: 0x060118FA RID: 71930 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060118FB RID: 71931 RVA: 0x002EFC40 File Offset: 0x002EDE40
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShapeProperties>(deep);
		}

		// Token: 0x060118FC RID: 71932 RVA: 0x002EFC4C File Offset: 0x002EDE4C
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

		// Token: 0x04007B4D RID: 31565
		private const string tagName = "grpSpPr";

		// Token: 0x04007B4E RID: 31566
		private const byte tagNsId = 60;

		// Token: 0x04007B4F RID: 31567
		internal const int ElementTypeIdConst = 13128;

		// Token: 0x04007B50 RID: 31568
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x04007B51 RID: 31569
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B52 RID: 31570
		private static readonly string[] eleTagNames;

		// Token: 0x04007B53 RID: 31571
		private static readonly byte[] eleNamespaceIds;
	}
}
