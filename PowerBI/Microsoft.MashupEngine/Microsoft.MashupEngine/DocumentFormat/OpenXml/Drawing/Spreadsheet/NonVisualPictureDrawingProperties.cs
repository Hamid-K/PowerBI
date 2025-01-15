using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002896 RID: 10390
	[ChildElementInfo(typeof(NonVisualPicturePropertiesExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PictureLocks))]
	internal class NonVisualPictureDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170067D7 RID: 26583
		// (get) Token: 0x0601469C RID: 83612 RVA: 0x002FC3A1 File Offset: 0x002FA5A1
		public override string LocalName
		{
			get
			{
				return "cNvPicPr";
			}
		}

		// Token: 0x170067D8 RID: 26584
		// (get) Token: 0x0601469D RID: 83613 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170067D9 RID: 26585
		// (get) Token: 0x0601469E RID: 83614 RVA: 0x00312F84 File Offset: 0x00311184
		internal override int ElementTypeId
		{
			get
			{
				return 10751;
			}
		}

		// Token: 0x0601469F RID: 83615 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170067DA RID: 26586
		// (get) Token: 0x060146A0 RID: 83616 RVA: 0x00312F8B File Offset: 0x0031118B
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualPictureDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x170067DB RID: 26587
		// (get) Token: 0x060146A1 RID: 83617 RVA: 0x00312F92 File Offset: 0x00311192
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualPictureDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170067DC RID: 26588
		// (get) Token: 0x060146A2 RID: 83618 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060146A3 RID: 83619 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060146A4 RID: 83620 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPictureDrawingProperties()
		{
		}

		// Token: 0x060146A5 RID: 83621 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPictureDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060146A6 RID: 83622 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPictureDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060146A7 RID: 83623 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPictureDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060146A8 RID: 83624 RVA: 0x002FC3BD File Offset: 0x002FA5BD
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

		// Token: 0x170067DD RID: 26589
		// (get) Token: 0x060146A9 RID: 83625 RVA: 0x00312F99 File Offset: 0x00311199
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualPictureDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170067DE RID: 26590
		// (get) Token: 0x060146AA RID: 83626 RVA: 0x00312FA0 File Offset: 0x003111A0
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualPictureDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170067DF RID: 26591
		// (get) Token: 0x060146AB RID: 83627 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170067E0 RID: 26592
		// (get) Token: 0x060146AC RID: 83628 RVA: 0x002FC3FE File Offset: 0x002FA5FE
		// (set) Token: 0x060146AD RID: 83629 RVA: 0x002FC407 File Offset: 0x002FA607
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

		// Token: 0x170067E1 RID: 26593
		// (get) Token: 0x060146AE RID: 83630 RVA: 0x002FC411 File Offset: 0x002FA611
		// (set) Token: 0x060146AF RID: 83631 RVA: 0x002FC41A File Offset: 0x002FA61A
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

		// Token: 0x060146B0 RID: 83632 RVA: 0x002FC424 File Offset: 0x002FA624
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "preferRelativeResize" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060146B1 RID: 83633 RVA: 0x00312FA7 File Offset: 0x003111A7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPictureDrawingProperties>(deep);
		}

		// Token: 0x060146B2 RID: 83634 RVA: 0x00312FB0 File Offset: 0x003111B0
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualPictureDrawingProperties()
		{
			byte[] array = new byte[1];
			NonVisualPictureDrawingProperties.attributeNamespaceIds = array;
			NonVisualPictureDrawingProperties.eleTagNames = new string[] { "picLocks", "extLst" };
			NonVisualPictureDrawingProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04008DFC RID: 36348
		private const string tagName = "cNvPicPr";

		// Token: 0x04008DFD RID: 36349
		private const byte tagNsId = 18;

		// Token: 0x04008DFE RID: 36350
		internal const int ElementTypeIdConst = 10751;

		// Token: 0x04008DFF RID: 36351
		private static string[] attributeTagNames = new string[] { "preferRelativeResize" };

		// Token: 0x04008E00 RID: 36352
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008E01 RID: 36353
		private static readonly string[] eleTagNames;

		// Token: 0x04008E02 RID: 36354
		private static readonly byte[] eleNamespaceIds;
	}
}
