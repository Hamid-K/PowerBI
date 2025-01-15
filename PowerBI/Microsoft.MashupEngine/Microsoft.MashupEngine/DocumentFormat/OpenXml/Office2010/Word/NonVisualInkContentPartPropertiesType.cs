using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024BB RID: 9403
	[ChildElementInfo(typeof(ContentPartLocks), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class NonVisualInkContentPartPropertiesType : OpenXmlCompositeElement
	{
		// Token: 0x17005293 RID: 21139
		// (get) Token: 0x060116B4 RID: 71348 RVA: 0x002EE52E File Offset: 0x002EC72E
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.attributeTagNames;
			}
		}

		// Token: 0x17005294 RID: 21140
		// (get) Token: 0x060116B5 RID: 71349 RVA: 0x002EE535 File Offset: 0x002EC735
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005295 RID: 21141
		// (get) Token: 0x060116B6 RID: 71350 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060116B7 RID: 71351 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060116B8 RID: 71352 RVA: 0x002DFD24 File Offset: 0x002DDF24
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

		// Token: 0x17005296 RID: 21142
		// (get) Token: 0x060116B9 RID: 71353 RVA: 0x002EE53C File Offset: 0x002EC73C
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.eleTagNames;
			}
		}

		// Token: 0x17005297 RID: 21143
		// (get) Token: 0x060116BA RID: 71354 RVA: 0x002EE543 File Offset: 0x002EC743
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.eleNamespaceIds;
			}
		}

		// Token: 0x17005298 RID: 21144
		// (get) Token: 0x060116BB RID: 71355 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005299 RID: 21145
		// (get) Token: 0x060116BC RID: 71356 RVA: 0x002DFD65 File Offset: 0x002DDF65
		// (set) Token: 0x060116BD RID: 71357 RVA: 0x002DFD6E File Offset: 0x002DDF6E
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

		// Token: 0x1700529A RID: 21146
		// (get) Token: 0x060116BE RID: 71358 RVA: 0x002DFD78 File Offset: 0x002DDF78
		// (set) Token: 0x060116BF RID: 71359 RVA: 0x002DFD81 File Offset: 0x002DDF81
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

		// Token: 0x060116C0 RID: 71360 RVA: 0x002DFD8B File Offset: 0x002DDF8B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "isComment" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060116C1 RID: 71361 RVA: 0x00293ECF File Offset: 0x002920CF
		protected NonVisualInkContentPartPropertiesType()
		{
		}

		// Token: 0x060116C2 RID: 71362 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected NonVisualInkContentPartPropertiesType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116C3 RID: 71363 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected NonVisualInkContentPartPropertiesType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116C4 RID: 71364 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected NonVisualInkContentPartPropertiesType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060116C5 RID: 71365 RVA: 0x002EE54C File Offset: 0x002EC74C
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualInkContentPartPropertiesType()
		{
			byte[] array = new byte[1];
			NonVisualInkContentPartPropertiesType.attributeNamespaceIds = array;
			NonVisualInkContentPartPropertiesType.eleTagNames = new string[] { "cpLocks", "extLst" };
			NonVisualInkContentPartPropertiesType.eleNamespaceIds = new byte[] { 48, 48 };
		}

		// Token: 0x040079BF RID: 31167
		private static string[] attributeTagNames = new string[] { "isComment" };

		// Token: 0x040079C0 RID: 31168
		private static byte[] attributeNamespaceIds;

		// Token: 0x040079C1 RID: 31169
		private static readonly string[] eleTagNames;

		// Token: 0x040079C2 RID: 31170
		private static readonly byte[] eleNamespaceIds;
	}
}
