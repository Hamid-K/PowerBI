using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003096 RID: 12438
	[ChildElementInfo(typeof(Annotation))]
	[ChildElementInfo(typeof(AnnotationXml))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BrushProperty : OpenXmlCompositeElement
	{
		// Token: 0x170097CA RID: 38858
		// (get) Token: 0x0601B0CC RID: 110796 RVA: 0x0036B1AA File Offset: 0x003693AA
		public override string LocalName
		{
			get
			{
				return "brushProperty";
			}
		}

		// Token: 0x170097CB RID: 38859
		// (get) Token: 0x0601B0CD RID: 110797 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x170097CC RID: 38860
		// (get) Token: 0x0601B0CE RID: 110798 RVA: 0x0036B1B1 File Offset: 0x003693B1
		internal override int ElementTypeId
		{
			get
			{
				return 12659;
			}
		}

		// Token: 0x0601B0CF RID: 110799 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170097CD RID: 38861
		// (get) Token: 0x0601B0D0 RID: 110800 RVA: 0x0036B1B8 File Offset: 0x003693B8
		internal override string[] AttributeTagNames
		{
			get
			{
				return BrushProperty.attributeTagNames;
			}
		}

		// Token: 0x170097CE RID: 38862
		// (get) Token: 0x0601B0D1 RID: 110801 RVA: 0x0036B1BF File Offset: 0x003693BF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BrushProperty.attributeNamespaceIds;
			}
		}

		// Token: 0x170097CF RID: 38863
		// (get) Token: 0x0601B0D2 RID: 110802 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B0D3 RID: 110803 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x170097D0 RID: 38864
		// (get) Token: 0x0601B0D4 RID: 110804 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601B0D5 RID: 110805 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "value")]
		public StringValue Value
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

		// Token: 0x170097D1 RID: 38865
		// (get) Token: 0x0601B0D6 RID: 110806 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601B0D7 RID: 110807 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "units")]
		public StringValue Units
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601B0D8 RID: 110808 RVA: 0x00293ECF File Offset: 0x002920CF
		public BrushProperty()
		{
		}

		// Token: 0x0601B0D9 RID: 110809 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BrushProperty(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B0DA RID: 110810 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BrushProperty(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B0DB RID: 110811 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BrushProperty(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B0DC RID: 110812 RVA: 0x0036B1C6 File Offset: 0x003693C6
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
			return null;
		}

		// Token: 0x0601B0DD RID: 110813 RVA: 0x0036B1FC File Offset: 0x003693FC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "value" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "units" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B0DE RID: 110814 RVA: 0x0036B253 File Offset: 0x00369453
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BrushProperty>(deep);
		}

		// Token: 0x0601B0DF RID: 110815 RVA: 0x0036B25C File Offset: 0x0036945C
		// Note: this type is marked as 'beforefieldinit'.
		static BrushProperty()
		{
			byte[] array = new byte[3];
			BrushProperty.attributeNamespaceIds = array;
		}

		// Token: 0x0400B2BB RID: 45755
		private const string tagName = "brushProperty";

		// Token: 0x0400B2BC RID: 45756
		private const byte tagNsId = 43;

		// Token: 0x0400B2BD RID: 45757
		internal const int ElementTypeIdConst = 12659;

		// Token: 0x0400B2BE RID: 45758
		private static string[] attributeTagNames = new string[] { "name", "value", "units" };

		// Token: 0x0400B2BF RID: 45759
		private static byte[] attributeNamespaceIds;
	}
}
