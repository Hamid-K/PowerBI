using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200309D RID: 12445
	[ChildElementInfo(typeof(AnnotationXml))]
	[ChildElementInfo(typeof(TraceGroup))]
	[ChildElementInfo(typeof(Annotation))]
	[ChildElementInfo(typeof(Trace))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TraceGroup : OpenXmlCompositeElement
	{
		// Token: 0x17009814 RID: 38932
		// (get) Token: 0x0601B170 RID: 110960 RVA: 0x0036BA0B File Offset: 0x00369C0B
		public override string LocalName
		{
			get
			{
				return "traceGroup";
			}
		}

		// Token: 0x17009815 RID: 38933
		// (get) Token: 0x0601B171 RID: 110961 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009816 RID: 38934
		// (get) Token: 0x0601B172 RID: 110962 RVA: 0x0036BA12 File Offset: 0x00369C12
		internal override int ElementTypeId
		{
			get
			{
				return 12666;
			}
		}

		// Token: 0x0601B173 RID: 110963 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009817 RID: 38935
		// (get) Token: 0x0601B174 RID: 110964 RVA: 0x0036BA19 File Offset: 0x00369C19
		internal override string[] AttributeTagNames
		{
			get
			{
				return TraceGroup.attributeTagNames;
			}
		}

		// Token: 0x17009818 RID: 38936
		// (get) Token: 0x0601B175 RID: 110965 RVA: 0x0036BA20 File Offset: 0x00369C20
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TraceGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x17009819 RID: 38937
		// (get) Token: 0x0601B176 RID: 110966 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B177 RID: 110967 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700981A RID: 38938
		// (get) Token: 0x0601B178 RID: 110968 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601B179 RID: 110969 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "contextRef")]
		public StringValue ContextRef
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

		// Token: 0x1700981B RID: 38939
		// (get) Token: 0x0601B17A RID: 110970 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601B17B RID: 110971 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "brushRef")]
		public StringValue BrushRef
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

		// Token: 0x0601B17C RID: 110972 RVA: 0x00293ECF File Offset: 0x002920CF
		public TraceGroup()
		{
		}

		// Token: 0x0601B17D RID: 110973 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TraceGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B17E RID: 110974 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TraceGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B17F RID: 110975 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TraceGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B180 RID: 110976 RVA: 0x0036BA28 File Offset: 0x00369C28
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
			if (43 == namespaceId && "trace" == name)
			{
				return new Trace();
			}
			if (43 == namespaceId && "traceGroup" == name)
			{
				return new TraceGroup();
			}
			return null;
		}

		// Token: 0x0601B181 RID: 110977 RVA: 0x0036BA98 File Offset: 0x00369C98
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "contextRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "brushRef" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B182 RID: 110978 RVA: 0x0036BAF0 File Offset: 0x00369CF0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TraceGroup>(deep);
		}

		// Token: 0x0601B183 RID: 110979 RVA: 0x0036BAFC File Offset: 0x00369CFC
		// Note: this type is marked as 'beforefieldinit'.
		static TraceGroup()
		{
			byte[] array = new byte[3];
			array[0] = 1;
			TraceGroup.attributeNamespaceIds = array;
		}

		// Token: 0x0400B2E2 RID: 45794
		private const string tagName = "traceGroup";

		// Token: 0x0400B2E3 RID: 45795
		private const byte tagNsId = 43;

		// Token: 0x0400B2E4 RID: 45796
		internal const int ElementTypeIdConst = 12666;

		// Token: 0x0400B2E5 RID: 45797
		private static string[] attributeTagNames = new string[] { "id", "contextRef", "brushRef" };

		// Token: 0x0400B2E6 RID: 45798
		private static byte[] attributeNamespaceIds;
	}
}
