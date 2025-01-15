using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200309A RID: 12442
	[ChildElementInfo(typeof(Annotation))]
	[ChildElementInfo(typeof(BrushProperty))]
	[ChildElementInfo(typeof(AnnotationXml))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Brush : OpenXmlCompositeElement
	{
		// Token: 0x170097F6 RID: 38902
		// (get) Token: 0x0601B12E RID: 110894 RVA: 0x0036B686 File Offset: 0x00369886
		public override string LocalName
		{
			get
			{
				return "brush";
			}
		}

		// Token: 0x170097F7 RID: 38903
		// (get) Token: 0x0601B12F RID: 110895 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x170097F8 RID: 38904
		// (get) Token: 0x0601B130 RID: 110896 RVA: 0x0036B68D File Offset: 0x0036988D
		internal override int ElementTypeId
		{
			get
			{
				return 12663;
			}
		}

		// Token: 0x0601B131 RID: 110897 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170097F9 RID: 38905
		// (get) Token: 0x0601B132 RID: 110898 RVA: 0x0036B694 File Offset: 0x00369894
		internal override string[] AttributeTagNames
		{
			get
			{
				return Brush.attributeTagNames;
			}
		}

		// Token: 0x170097FA RID: 38906
		// (get) Token: 0x0601B133 RID: 110899 RVA: 0x0036B69B File Offset: 0x0036989B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Brush.attributeNamespaceIds;
			}
		}

		// Token: 0x170097FB RID: 38907
		// (get) Token: 0x0601B134 RID: 110900 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B135 RID: 110901 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(1, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170097FC RID: 38908
		// (get) Token: 0x0601B136 RID: 110902 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601B137 RID: 110903 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "brushRef")]
		public StringValue BrushRef
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601B138 RID: 110904 RVA: 0x00293ECF File Offset: 0x002920CF
		public Brush()
		{
		}

		// Token: 0x0601B139 RID: 110905 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Brush(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B13A RID: 110906 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Brush(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B13B RID: 110907 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Brush(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B13C RID: 110908 RVA: 0x0036B6A4 File Offset: 0x003698A4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "annotation" == name)
			{
				return new Annotation();
			}
			if (43 == namespaceId && "annotationXML" == name)
			{
				return new AnnotationXml();
			}
			if (43 == namespaceId && "brushProperty" == name)
			{
				return new BrushProperty();
			}
			return null;
		}

		// Token: 0x0601B13D RID: 110909 RVA: 0x0036B6FA File Offset: 0x003698FA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "brushRef" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B13E RID: 110910 RVA: 0x0036B731 File Offset: 0x00369931
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Brush>(deep);
		}

		// Token: 0x0601B13F RID: 110911 RVA: 0x0036B73C File Offset: 0x0036993C
		// Note: this type is marked as 'beforefieldinit'.
		static Brush()
		{
			byte[] array = new byte[2];
			array[0] = 1;
			Brush.attributeNamespaceIds = array;
		}

		// Token: 0x0400B2D3 RID: 45779
		private const string tagName = "brush";

		// Token: 0x0400B2D4 RID: 45780
		private const byte tagNsId = 43;

		// Token: 0x0400B2D5 RID: 45781
		internal const int ElementTypeIdConst = 12663;

		// Token: 0x0400B2D6 RID: 45782
		private static string[] attributeTagNames = new string[] { "id", "brushRef" };

		// Token: 0x0400B2D7 RID: 45783
		private static byte[] attributeNamespaceIds;
	}
}
