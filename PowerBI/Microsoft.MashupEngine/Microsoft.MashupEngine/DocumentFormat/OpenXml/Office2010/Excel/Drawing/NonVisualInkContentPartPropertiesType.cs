using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Excel.Drawing
{
	// Token: 0x02002388 RID: 9096
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ContentPartLocks), FileFormatVersions.Office2010)]
	internal abstract class NonVisualInkContentPartPropertiesType : OpenXmlCompositeElement
	{
		// Token: 0x17004B87 RID: 19335
		// (get) Token: 0x060106EC RID: 67308 RVA: 0x002E38E6 File Offset: 0x002E1AE6
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.attributeTagNames;
			}
		}

		// Token: 0x17004B88 RID: 19336
		// (get) Token: 0x060106ED RID: 67309 RVA: 0x002E38ED File Offset: 0x002E1AED
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.attributeNamespaceIds;
			}
		}

		// Token: 0x17004B89 RID: 19337
		// (get) Token: 0x060106EE RID: 67310 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060106EF RID: 67311 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060106F0 RID: 67312 RVA: 0x002DFD24 File Offset: 0x002DDF24
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

		// Token: 0x17004B8A RID: 19338
		// (get) Token: 0x060106F1 RID: 67313 RVA: 0x002E38F4 File Offset: 0x002E1AF4
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.eleTagNames;
			}
		}

		// Token: 0x17004B8B RID: 19339
		// (get) Token: 0x060106F2 RID: 67314 RVA: 0x002E38FB File Offset: 0x002E1AFB
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.eleNamespaceIds;
			}
		}

		// Token: 0x17004B8C RID: 19340
		// (get) Token: 0x060106F3 RID: 67315 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004B8D RID: 19341
		// (get) Token: 0x060106F4 RID: 67316 RVA: 0x002DFD65 File Offset: 0x002DDF65
		// (set) Token: 0x060106F5 RID: 67317 RVA: 0x002DFD6E File Offset: 0x002DDF6E
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

		// Token: 0x17004B8E RID: 19342
		// (get) Token: 0x060106F6 RID: 67318 RVA: 0x002DFD78 File Offset: 0x002DDF78
		// (set) Token: 0x060106F7 RID: 67319 RVA: 0x002DFD81 File Offset: 0x002DDF81
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

		// Token: 0x060106F8 RID: 67320 RVA: 0x002DFD8B File Offset: 0x002DDF8B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "isComment" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060106F9 RID: 67321 RVA: 0x00293ECF File Offset: 0x002920CF
		protected NonVisualInkContentPartPropertiesType()
		{
		}

		// Token: 0x060106FA RID: 67322 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected NonVisualInkContentPartPropertiesType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060106FB RID: 67323 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected NonVisualInkContentPartPropertiesType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060106FC RID: 67324 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected NonVisualInkContentPartPropertiesType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060106FD RID: 67325 RVA: 0x002E3904 File Offset: 0x002E1B04
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualInkContentPartPropertiesType()
		{
			byte[] array = new byte[1];
			NonVisualInkContentPartPropertiesType.attributeNamespaceIds = array;
			NonVisualInkContentPartPropertiesType.eleTagNames = new string[] { "cpLocks", "extLst" };
			NonVisualInkContentPartPropertiesType.eleNamespaceIds = new byte[] { 48, 48 };
		}

		// Token: 0x04007494 RID: 29844
		private static string[] attributeTagNames = new string[] { "isComment" };

		// Token: 0x04007495 RID: 29845
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007496 RID: 29846
		private static readonly string[] eleTagNames;

		// Token: 0x04007497 RID: 29847
		private static readonly byte[] eleNamespaceIds;
	}
}
