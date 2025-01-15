using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200309E RID: 12446
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Annotation))]
	[ChildElementInfo(typeof(AnnotationXml))]
	[ChildElementInfo(typeof(TraceView))]
	internal class TraceView : OpenXmlCompositeElement
	{
		// Token: 0x1700981C RID: 38940
		// (get) Token: 0x0601B184 RID: 110980 RVA: 0x0036BB3F File Offset: 0x00369D3F
		public override string LocalName
		{
			get
			{
				return "traceView";
			}
		}

		// Token: 0x1700981D RID: 38941
		// (get) Token: 0x0601B185 RID: 110981 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x1700981E RID: 38942
		// (get) Token: 0x0601B186 RID: 110982 RVA: 0x0036BB46 File Offset: 0x00369D46
		internal override int ElementTypeId
		{
			get
			{
				return 12667;
			}
		}

		// Token: 0x0601B187 RID: 110983 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700981F RID: 38943
		// (get) Token: 0x0601B188 RID: 110984 RVA: 0x0036BB4D File Offset: 0x00369D4D
		internal override string[] AttributeTagNames
		{
			get
			{
				return TraceView.attributeTagNames;
			}
		}

		// Token: 0x17009820 RID: 38944
		// (get) Token: 0x0601B189 RID: 110985 RVA: 0x0036BB54 File Offset: 0x00369D54
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TraceView.attributeNamespaceIds;
			}
		}

		// Token: 0x17009821 RID: 38945
		// (get) Token: 0x0601B18A RID: 110986 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B18B RID: 110987 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17009822 RID: 38946
		// (get) Token: 0x0601B18C RID: 110988 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601B18D RID: 110989 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17009823 RID: 38947
		// (get) Token: 0x0601B18E RID: 110990 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601B18F RID: 110991 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "traceDataRef")]
		public StringValue TraceDataRef
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

		// Token: 0x17009824 RID: 38948
		// (get) Token: 0x0601B190 RID: 110992 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601B191 RID: 110993 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "from")]
		public StringValue From
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17009825 RID: 38949
		// (get) Token: 0x0601B192 RID: 110994 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601B193 RID: 110995 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "to")]
		public StringValue To
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x0601B194 RID: 110996 RVA: 0x00293ECF File Offset: 0x002920CF
		public TraceView()
		{
		}

		// Token: 0x0601B195 RID: 110997 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TraceView(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B196 RID: 110998 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TraceView(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B197 RID: 110999 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TraceView(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B198 RID: 111000 RVA: 0x0036BB5C File Offset: 0x00369D5C
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
			if (43 == namespaceId && "traceView" == name)
			{
				return new TraceView();
			}
			return null;
		}

		// Token: 0x0601B199 RID: 111001 RVA: 0x0036BBB4 File Offset: 0x00369DB4
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
			if (namespaceId == 0 && "traceDataRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "from" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "to" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B19A RID: 111002 RVA: 0x0036BC38 File Offset: 0x00369E38
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TraceView>(deep);
		}

		// Token: 0x0601B19B RID: 111003 RVA: 0x0036BC44 File Offset: 0x00369E44
		// Note: this type is marked as 'beforefieldinit'.
		static TraceView()
		{
			byte[] array = new byte[5];
			array[0] = 1;
			TraceView.attributeNamespaceIds = array;
		}

		// Token: 0x0400B2E7 RID: 45799
		private const string tagName = "traceView";

		// Token: 0x0400B2E8 RID: 45800
		private const byte tagNsId = 43;

		// Token: 0x0400B2E9 RID: 45801
		internal const int ElementTypeIdConst = 12667;

		// Token: 0x0400B2EA RID: 45802
		private static string[] attributeTagNames = new string[] { "id", "contextRef", "traceDataRef", "from", "to" };

		// Token: 0x0400B2EB RID: 45803
		private static byte[] attributeNamespaceIds;
	}
}
