using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200309F RID: 12447
	[ChildElementInfo(typeof(CanvasTransform))]
	[ChildElementInfo(typeof(Timestamp))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Brush))]
	[ChildElementInfo(typeof(Canvas))]
	[ChildElementInfo(typeof(TraceFormat))]
	[ChildElementInfo(typeof(InkSource))]
	internal class Context : OpenXmlCompositeElement
	{
		// Token: 0x17009826 RID: 38950
		// (get) Token: 0x0601B19C RID: 111004 RVA: 0x002C8263 File Offset: 0x002C6463
		public override string LocalName
		{
			get
			{
				return "context";
			}
		}

		// Token: 0x17009827 RID: 38951
		// (get) Token: 0x0601B19D RID: 111005 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009828 RID: 38952
		// (get) Token: 0x0601B19E RID: 111006 RVA: 0x0036BC97 File Offset: 0x00369E97
		internal override int ElementTypeId
		{
			get
			{
				return 12668;
			}
		}

		// Token: 0x0601B19F RID: 111007 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009829 RID: 38953
		// (get) Token: 0x0601B1A0 RID: 111008 RVA: 0x0036BC9E File Offset: 0x00369E9E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Context.attributeTagNames;
			}
		}

		// Token: 0x1700982A RID: 38954
		// (get) Token: 0x0601B1A1 RID: 111009 RVA: 0x0036BCA5 File Offset: 0x00369EA5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Context.attributeNamespaceIds;
			}
		}

		// Token: 0x1700982B RID: 38955
		// (get) Token: 0x0601B1A2 RID: 111010 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B1A3 RID: 111011 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700982C RID: 38956
		// (get) Token: 0x0601B1A4 RID: 111012 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601B1A5 RID: 111013 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700982D RID: 38957
		// (get) Token: 0x0601B1A6 RID: 111014 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601B1A7 RID: 111015 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "canvasRef")]
		public StringValue CanvasRef
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

		// Token: 0x1700982E RID: 38958
		// (get) Token: 0x0601B1A8 RID: 111016 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601B1A9 RID: 111017 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "canvasTransformRef")]
		public StringValue CanvasTransformRef
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

		// Token: 0x1700982F RID: 38959
		// (get) Token: 0x0601B1AA RID: 111018 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601B1AB RID: 111019 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "traceFormatRef")]
		public StringValue TraceFromatRef
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

		// Token: 0x17009830 RID: 38960
		// (get) Token: 0x0601B1AC RID: 111020 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0601B1AD RID: 111021 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "inkSourceRef")]
		public StringValue InkSourceRef
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17009831 RID: 38961
		// (get) Token: 0x0601B1AE RID: 111022 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0601B1AF RID: 111023 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "brushRef")]
		public StringValue BrushRef
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17009832 RID: 38962
		// (get) Token: 0x0601B1B0 RID: 111024 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0601B1B1 RID: 111025 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "timestampRef")]
		public StringValue TimestampRef
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x0601B1B2 RID: 111026 RVA: 0x00293ECF File Offset: 0x002920CF
		public Context()
		{
		}

		// Token: 0x0601B1B3 RID: 111027 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Context(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B1B4 RID: 111028 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Context(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B1B5 RID: 111029 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Context(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B1B6 RID: 111030 RVA: 0x0036BCAC File Offset: 0x00369EAC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "canvas" == name)
			{
				return new Canvas();
			}
			if (43 == namespaceId && "canvasTransform" == name)
			{
				return new CanvasTransform();
			}
			if (43 == namespaceId && "traceFormat" == name)
			{
				return new TraceFormat();
			}
			if (43 == namespaceId && "inkSource" == name)
			{
				return new InkSource();
			}
			if (43 == namespaceId && "brush" == name)
			{
				return new Brush();
			}
			if (43 == namespaceId && "timestamp" == name)
			{
				return new Timestamp();
			}
			return null;
		}

		// Token: 0x17009833 RID: 38963
		// (get) Token: 0x0601B1B7 RID: 111031 RVA: 0x0036BD4A File Offset: 0x00369F4A
		internal override string[] ElementTagNames
		{
			get
			{
				return Context.eleTagNames;
			}
		}

		// Token: 0x17009834 RID: 38964
		// (get) Token: 0x0601B1B8 RID: 111032 RVA: 0x0036BD51 File Offset: 0x00369F51
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Context.eleNamespaceIds;
			}
		}

		// Token: 0x17009835 RID: 38965
		// (get) Token: 0x0601B1B9 RID: 111033 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009836 RID: 38966
		// (get) Token: 0x0601B1BA RID: 111034 RVA: 0x0036BD58 File Offset: 0x00369F58
		// (set) Token: 0x0601B1BB RID: 111035 RVA: 0x0036BD61 File Offset: 0x00369F61
		public Canvas Canvas
		{
			get
			{
				return base.GetElement<Canvas>(0);
			}
			set
			{
				base.SetElement<Canvas>(0, value);
			}
		}

		// Token: 0x17009837 RID: 38967
		// (get) Token: 0x0601B1BC RID: 111036 RVA: 0x0036BD6B File Offset: 0x00369F6B
		// (set) Token: 0x0601B1BD RID: 111037 RVA: 0x0036BD74 File Offset: 0x00369F74
		public CanvasTransform CanvasTransform
		{
			get
			{
				return base.GetElement<CanvasTransform>(1);
			}
			set
			{
				base.SetElement<CanvasTransform>(1, value);
			}
		}

		// Token: 0x17009838 RID: 38968
		// (get) Token: 0x0601B1BE RID: 111038 RVA: 0x0036BD7E File Offset: 0x00369F7E
		// (set) Token: 0x0601B1BF RID: 111039 RVA: 0x0036BD87 File Offset: 0x00369F87
		public TraceFormat TraceFormat
		{
			get
			{
				return base.GetElement<TraceFormat>(2);
			}
			set
			{
				base.SetElement<TraceFormat>(2, value);
			}
		}

		// Token: 0x17009839 RID: 38969
		// (get) Token: 0x0601B1C0 RID: 111040 RVA: 0x0036BD91 File Offset: 0x00369F91
		// (set) Token: 0x0601B1C1 RID: 111041 RVA: 0x0036BD9A File Offset: 0x00369F9A
		public InkSource InkSource
		{
			get
			{
				return base.GetElement<InkSource>(3);
			}
			set
			{
				base.SetElement<InkSource>(3, value);
			}
		}

		// Token: 0x1700983A RID: 38970
		// (get) Token: 0x0601B1C2 RID: 111042 RVA: 0x0036BDA4 File Offset: 0x00369FA4
		// (set) Token: 0x0601B1C3 RID: 111043 RVA: 0x0036BDAD File Offset: 0x00369FAD
		public Brush Brush
		{
			get
			{
				return base.GetElement<Brush>(4);
			}
			set
			{
				base.SetElement<Brush>(4, value);
			}
		}

		// Token: 0x1700983B RID: 38971
		// (get) Token: 0x0601B1C4 RID: 111044 RVA: 0x0036BDB7 File Offset: 0x00369FB7
		// (set) Token: 0x0601B1C5 RID: 111045 RVA: 0x0036BDC0 File Offset: 0x00369FC0
		public Timestamp Timestamp
		{
			get
			{
				return base.GetElement<Timestamp>(5);
			}
			set
			{
				base.SetElement<Timestamp>(5, value);
			}
		}

		// Token: 0x0601B1C6 RID: 111046 RVA: 0x0036BDCC File Offset: 0x00369FCC
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
			if (namespaceId == 0 && "canvasRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "canvasTransformRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "traceFormatRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "inkSourceRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "brushRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "timestampRef" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B1C7 RID: 111047 RVA: 0x0036BE92 File Offset: 0x0036A092
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Context>(deep);
		}

		// Token: 0x0601B1C8 RID: 111048 RVA: 0x0036BE9C File Offset: 0x0036A09C
		// Note: this type is marked as 'beforefieldinit'.
		static Context()
		{
			byte[] array = new byte[8];
			array[0] = 1;
			Context.attributeNamespaceIds = array;
			Context.eleTagNames = new string[] { "canvas", "canvasTransform", "traceFormat", "inkSource", "brush", "timestamp" };
			Context.eleNamespaceIds = new byte[] { 43, 43, 43, 43, 43, 43 };
		}

		// Token: 0x0400B2EC RID: 45804
		private const string tagName = "context";

		// Token: 0x0400B2ED RID: 45805
		private const byte tagNsId = 43;

		// Token: 0x0400B2EE RID: 45806
		internal const int ElementTypeIdConst = 12668;

		// Token: 0x0400B2EF RID: 45807
		private static string[] attributeTagNames = new string[] { "id", "contextRef", "canvasRef", "canvasTransformRef", "traceFormatRef", "inkSourceRef", "brushRef", "timestampRef" };

		// Token: 0x0400B2F0 RID: 45808
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400B2F1 RID: 45809
		private static readonly string[] eleTagNames;

		// Token: 0x0400B2F2 RID: 45810
		private static readonly byte[] eleNamespaceIds;
	}
}
