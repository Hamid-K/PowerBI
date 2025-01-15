using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing
{
	// Token: 0x02002337 RID: 9015
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ContentPartLocks), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	internal abstract class NonVisualInkContentPartPropertiesType : OpenXmlCompositeElement
	{
		// Token: 0x1700491C RID: 18716
		// (get) Token: 0x060101AC RID: 65964 RVA: 0x002DFD16 File Offset: 0x002DDF16
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.attributeTagNames;
			}
		}

		// Token: 0x1700491D RID: 18717
		// (get) Token: 0x060101AD RID: 65965 RVA: 0x002DFD1D File Offset: 0x002DDF1D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700491E RID: 18718
		// (get) Token: 0x060101AE RID: 65966 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060101AF RID: 65967 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "isComment")]
		public BooleanValue IsComment
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

		// Token: 0x060101B0 RID: 65968 RVA: 0x002DFD24 File Offset: 0x002DDF24
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "cpLocks" == name)
			{
				return new ContentPartLocks();
			}
			if (48 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x1700491F RID: 18719
		// (get) Token: 0x060101B1 RID: 65969 RVA: 0x002DFD57 File Offset: 0x002DDF57
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.eleTagNames;
			}
		}

		// Token: 0x17004920 RID: 18720
		// (get) Token: 0x060101B2 RID: 65970 RVA: 0x002DFD5E File Offset: 0x002DDF5E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.eleNamespaceIds;
			}
		}

		// Token: 0x17004921 RID: 18721
		// (get) Token: 0x060101B3 RID: 65971 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004922 RID: 18722
		// (get) Token: 0x060101B4 RID: 65972 RVA: 0x002DFD65 File Offset: 0x002DDF65
		// (set) Token: 0x060101B5 RID: 65973 RVA: 0x002DFD6E File Offset: 0x002DDF6E
		public ContentPartLocks ContentPartLocks
		{
			get
			{
				return base.GetElement<ContentPartLocks>(0);
			}
			set
			{
				base.SetElement<ContentPartLocks>(0, value);
			}
		}

		// Token: 0x17004923 RID: 18723
		// (get) Token: 0x060101B6 RID: 65974 RVA: 0x002DFD78 File Offset: 0x002DDF78
		// (set) Token: 0x060101B7 RID: 65975 RVA: 0x002DFD81 File Offset: 0x002DDF81
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(1);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(1, value);
			}
		}

		// Token: 0x060101B8 RID: 65976 RVA: 0x002DFD8B File Offset: 0x002DDF8B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "isComment" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060101B9 RID: 65977 RVA: 0x00293ECF File Offset: 0x002920CF
		protected NonVisualInkContentPartPropertiesType()
		{
		}

		// Token: 0x060101BA RID: 65978 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected NonVisualInkContentPartPropertiesType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101BB RID: 65979 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected NonVisualInkContentPartPropertiesType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101BC RID: 65980 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected NonVisualInkContentPartPropertiesType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060101BD RID: 65981 RVA: 0x002DFDAC File Offset: 0x002DDFAC
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualInkContentPartPropertiesType()
		{
			byte[] array = new byte[1];
			NonVisualInkContentPartPropertiesType.attributeNamespaceIds = array;
			NonVisualInkContentPartPropertiesType.eleTagNames = new string[] { "cpLocks", "extLst" };
			NonVisualInkContentPartPropertiesType.eleNamespaceIds = new byte[] { 48, 48 };
		}

		// Token: 0x04007316 RID: 29462
		private static string[] attributeTagNames = new string[] { "isComment" };

		// Token: 0x04007317 RID: 29463
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007318 RID: 29464
		private static readonly string[] eleTagNames;

		// Token: 0x04007319 RID: 29465
		private static readonly byte[] eleNamespaceIds;
	}
}
