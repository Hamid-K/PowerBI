using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002636 RID: 9782
	[ChildElementInfo(typeof(PictureLocks))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualPicturePropertiesExtensionList))]
	internal class NonVisualPictureDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005A88 RID: 23176
		// (get) Token: 0x0601285A RID: 75866 RVA: 0x002FC3A1 File Offset: 0x002FA5A1
		public override string LocalName
		{
			get
			{
				return "cNvPicPr";
			}
		}

		// Token: 0x17005A89 RID: 23177
		// (get) Token: 0x0601285B RID: 75867 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A8A RID: 23178
		// (get) Token: 0x0601285C RID: 75868 RVA: 0x002FC3A8 File Offset: 0x002FA5A8
		internal override int ElementTypeId
		{
			get
			{
				return 10601;
			}
		}

		// Token: 0x0601285D RID: 75869 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005A8B RID: 23179
		// (get) Token: 0x0601285E RID: 75870 RVA: 0x002FC3AF File Offset: 0x002FA5AF
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualPictureDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17005A8C RID: 23180
		// (get) Token: 0x0601285F RID: 75871 RVA: 0x002FC3B6 File Offset: 0x002FA5B6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualPictureDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17005A8D RID: 23181
		// (get) Token: 0x06012860 RID: 75872 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06012861 RID: 75873 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "preferRelativeResize")]
		public BooleanValue PreferRelativeResize
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012862 RID: 75874 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPictureDrawingProperties()
		{
		}

		// Token: 0x06012863 RID: 75875 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPictureDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012864 RID: 75876 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPictureDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012865 RID: 75877 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPictureDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012866 RID: 75878 RVA: 0x002FC3BD File Offset: 0x002FA5BD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "picLocks" == name)
			{
				return new PictureLocks();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new NonVisualPicturePropertiesExtensionList();
			}
			return null;
		}

		// Token: 0x17005A8E RID: 23182
		// (get) Token: 0x06012867 RID: 75879 RVA: 0x002FC3F0 File Offset: 0x002FA5F0
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualPictureDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17005A8F RID: 23183
		// (get) Token: 0x06012868 RID: 75880 RVA: 0x002FC3F7 File Offset: 0x002FA5F7
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualPictureDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005A90 RID: 23184
		// (get) Token: 0x06012869 RID: 75881 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A91 RID: 23185
		// (get) Token: 0x0601286A RID: 75882 RVA: 0x002FC3FE File Offset: 0x002FA5FE
		// (set) Token: 0x0601286B RID: 75883 RVA: 0x002FC407 File Offset: 0x002FA607
		public PictureLocks PictureLocks
		{
			get
			{
				return base.GetElement<PictureLocks>(0);
			}
			set
			{
				base.SetElement<PictureLocks>(0, value);
			}
		}

		// Token: 0x17005A92 RID: 23186
		// (get) Token: 0x0601286C RID: 75884 RVA: 0x002FC411 File Offset: 0x002FA611
		// (set) Token: 0x0601286D RID: 75885 RVA: 0x002FC41A File Offset: 0x002FA61A
		public NonVisualPicturePropertiesExtensionList NonVisualPicturePropertiesExtensionList
		{
			get
			{
				return base.GetElement<NonVisualPicturePropertiesExtensionList>(1);
			}
			set
			{
				base.SetElement<NonVisualPicturePropertiesExtensionList>(1, value);
			}
		}

		// Token: 0x0601286E RID: 75886 RVA: 0x002FC424 File Offset: 0x002FA624
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "preferRelativeResize" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601286F RID: 75887 RVA: 0x002FC444 File Offset: 0x002FA644
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPictureDrawingProperties>(deep);
		}

		// Token: 0x06012870 RID: 75888 RVA: 0x002FC450 File Offset: 0x002FA650
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualPictureDrawingProperties()
		{
			byte[] array = new byte[1];
			NonVisualPictureDrawingProperties.attributeNamespaceIds = array;
			NonVisualPictureDrawingProperties.eleTagNames = new string[] { "picLocks", "extLst" };
			NonVisualPictureDrawingProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x0400807E RID: 32894
		private const string tagName = "cNvPicPr";

		// Token: 0x0400807F RID: 32895
		private const byte tagNsId = 12;

		// Token: 0x04008080 RID: 32896
		internal const int ElementTypeIdConst = 10601;

		// Token: 0x04008081 RID: 32897
		private static string[] attributeTagNames = new string[] { "preferRelativeResize" };

		// Token: 0x04008082 RID: 32898
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008083 RID: 32899
		private static readonly string[] eleTagNames;

		// Token: 0x04008084 RID: 32900
		private static readonly byte[] eleNamespaceIds;
	}
}
