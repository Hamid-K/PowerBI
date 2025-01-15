using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A66 RID: 10854
	[ChildElementInfo(typeof(NonVisualPicturePropertiesExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PictureLocks))]
	internal class NonVisualPictureDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700728C RID: 29324
		// (get) Token: 0x06015EE5 RID: 89829 RVA: 0x002FC3A1 File Offset: 0x002FA5A1
		public override string LocalName
		{
			get
			{
				return "cNvPicPr";
			}
		}

		// Token: 0x1700728D RID: 29325
		// (get) Token: 0x06015EE6 RID: 89830 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700728E RID: 29326
		// (get) Token: 0x06015EE7 RID: 89831 RVA: 0x00324AD0 File Offset: 0x00322CD0
		internal override int ElementTypeId
		{
			get
			{
				return 12272;
			}
		}

		// Token: 0x06015EE8 RID: 89832 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700728F RID: 29327
		// (get) Token: 0x06015EE9 RID: 89833 RVA: 0x00324AD7 File Offset: 0x00322CD7
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualPictureDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17007290 RID: 29328
		// (get) Token: 0x06015EEA RID: 89834 RVA: 0x00324ADE File Offset: 0x00322CDE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualPictureDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007291 RID: 29329
		// (get) Token: 0x06015EEB RID: 89835 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015EEC RID: 89836 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06015EED RID: 89837 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPictureDrawingProperties()
		{
		}

		// Token: 0x06015EEE RID: 89838 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPictureDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015EEF RID: 89839 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPictureDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015EF0 RID: 89840 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPictureDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015EF1 RID: 89841 RVA: 0x002FC3BD File Offset: 0x002FA5BD
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

		// Token: 0x17007292 RID: 29330
		// (get) Token: 0x06015EF2 RID: 89842 RVA: 0x00324AE5 File Offset: 0x00322CE5
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualPictureDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17007293 RID: 29331
		// (get) Token: 0x06015EF3 RID: 89843 RVA: 0x00324AEC File Offset: 0x00322CEC
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualPictureDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007294 RID: 29332
		// (get) Token: 0x06015EF4 RID: 89844 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007295 RID: 29333
		// (get) Token: 0x06015EF5 RID: 89845 RVA: 0x002FC3FE File Offset: 0x002FA5FE
		// (set) Token: 0x06015EF6 RID: 89846 RVA: 0x002FC407 File Offset: 0x002FA607
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

		// Token: 0x17007296 RID: 29334
		// (get) Token: 0x06015EF7 RID: 89847 RVA: 0x002FC411 File Offset: 0x002FA611
		// (set) Token: 0x06015EF8 RID: 89848 RVA: 0x002FC41A File Offset: 0x002FA61A
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

		// Token: 0x06015EF9 RID: 89849 RVA: 0x002FC424 File Offset: 0x002FA624
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "preferRelativeResize" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015EFA RID: 89850 RVA: 0x00324AF3 File Offset: 0x00322CF3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPictureDrawingProperties>(deep);
		}

		// Token: 0x06015EFB RID: 89851 RVA: 0x00324AFC File Offset: 0x00322CFC
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualPictureDrawingProperties()
		{
			byte[] array = new byte[1];
			NonVisualPictureDrawingProperties.attributeNamespaceIds = array;
			NonVisualPictureDrawingProperties.eleTagNames = new string[] { "picLocks", "extLst" };
			NonVisualPictureDrawingProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04009576 RID: 38262
		private const string tagName = "cNvPicPr";

		// Token: 0x04009577 RID: 38263
		private const byte tagNsId = 24;

		// Token: 0x04009578 RID: 38264
		internal const int ElementTypeIdConst = 12272;

		// Token: 0x04009579 RID: 38265
		private static string[] attributeTagNames = new string[] { "preferRelativeResize" };

		// Token: 0x0400957A RID: 38266
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400957B RID: 38267
		private static readonly string[] eleTagNames;

		// Token: 0x0400957C RID: 38268
		private static readonly byte[] eleNamespaceIds;
	}
}
