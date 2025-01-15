using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003099 RID: 12441
	[ChildElementInfo(typeof(ActiveArea))]
	[ChildElementInfo(typeof(TraceFormat))]
	[ChildElementInfo(typeof(SampleRate))]
	[ChildElementInfo(typeof(Latency))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SourceProperty))]
	[ChildElementInfo(typeof(ChannelProperties))]
	internal class InkSource : OpenXmlCompositeElement
	{
		// Token: 0x170097E4 RID: 38884
		// (get) Token: 0x0601B109 RID: 110857 RVA: 0x0036B42F File Offset: 0x0036962F
		public override string LocalName
		{
			get
			{
				return "inkSource";
			}
		}

		// Token: 0x170097E5 RID: 38885
		// (get) Token: 0x0601B10A RID: 110858 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x170097E6 RID: 38886
		// (get) Token: 0x0601B10B RID: 110859 RVA: 0x0036B436 File Offset: 0x00369636
		internal override int ElementTypeId
		{
			get
			{
				return 12662;
			}
		}

		// Token: 0x0601B10C RID: 110860 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170097E7 RID: 38887
		// (get) Token: 0x0601B10D RID: 110861 RVA: 0x0036B43D File Offset: 0x0036963D
		internal override string[] AttributeTagNames
		{
			get
			{
				return InkSource.attributeTagNames;
			}
		}

		// Token: 0x170097E8 RID: 38888
		// (get) Token: 0x0601B10E RID: 110862 RVA: 0x0036B444 File Offset: 0x00369644
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return InkSource.attributeNamespaceIds;
			}
		}

		// Token: 0x170097E9 RID: 38889
		// (get) Token: 0x0601B10F RID: 110863 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B110 RID: 110864 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170097EA RID: 38890
		// (get) Token: 0x0601B111 RID: 110865 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601B112 RID: 110866 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "manufacturer")]
		public StringValue Manufacturer
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

		// Token: 0x170097EB RID: 38891
		// (get) Token: 0x0601B113 RID: 110867 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601B114 RID: 110868 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "model")]
		public StringValue Model
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

		// Token: 0x170097EC RID: 38892
		// (get) Token: 0x0601B115 RID: 110869 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601B116 RID: 110870 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "serialNo")]
		public StringValue SerialNo
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

		// Token: 0x170097ED RID: 38893
		// (get) Token: 0x0601B117 RID: 110871 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601B118 RID: 110872 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "specificationRef")]
		public StringValue SpecificationRef
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

		// Token: 0x170097EE RID: 38894
		// (get) Token: 0x0601B119 RID: 110873 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0601B11A RID: 110874 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x0601B11B RID: 110875 RVA: 0x00293ECF File Offset: 0x002920CF
		public InkSource()
		{
		}

		// Token: 0x0601B11C RID: 110876 RVA: 0x00293ED7 File Offset: 0x002920D7
		public InkSource(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B11D RID: 110877 RVA: 0x00293EE0 File Offset: 0x002920E0
		public InkSource(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B11E RID: 110878 RVA: 0x00293EE9 File Offset: 0x002920E9
		public InkSource(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B11F RID: 110879 RVA: 0x0036B44C File Offset: 0x0036964C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "traceFormat" == name)
			{
				return new TraceFormat();
			}
			if (43 == namespaceId && "sampleRate" == name)
			{
				return new SampleRate();
			}
			if (43 == namespaceId && "latency" == name)
			{
				return new Latency();
			}
			if (43 == namespaceId && "activeArea" == name)
			{
				return new ActiveArea();
			}
			if (43 == namespaceId && "srcProperty" == name)
			{
				return new SourceProperty();
			}
			if (43 == namespaceId && "channelProperties" == name)
			{
				return new ChannelProperties();
			}
			return null;
		}

		// Token: 0x170097EF RID: 38895
		// (get) Token: 0x0601B120 RID: 110880 RVA: 0x0036B4EA File Offset: 0x003696EA
		internal override string[] ElementTagNames
		{
			get
			{
				return InkSource.eleTagNames;
			}
		}

		// Token: 0x170097F0 RID: 38896
		// (get) Token: 0x0601B121 RID: 110881 RVA: 0x0036B4F1 File Offset: 0x003696F1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return InkSource.eleNamespaceIds;
			}
		}

		// Token: 0x170097F1 RID: 38897
		// (get) Token: 0x0601B122 RID: 110882 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170097F2 RID: 38898
		// (get) Token: 0x0601B123 RID: 110883 RVA: 0x0036B2E0 File Offset: 0x003694E0
		// (set) Token: 0x0601B124 RID: 110884 RVA: 0x0036B2E9 File Offset: 0x003694E9
		public TraceFormat TraceFormat
		{
			get
			{
				return base.GetElement<TraceFormat>(0);
			}
			set
			{
				base.SetElement<TraceFormat>(0, value);
			}
		}

		// Token: 0x170097F3 RID: 38899
		// (get) Token: 0x0601B125 RID: 110885 RVA: 0x0036B4F8 File Offset: 0x003696F8
		// (set) Token: 0x0601B126 RID: 110886 RVA: 0x0036B501 File Offset: 0x00369701
		public SampleRate SampleRate
		{
			get
			{
				return base.GetElement<SampleRate>(1);
			}
			set
			{
				base.SetElement<SampleRate>(1, value);
			}
		}

		// Token: 0x170097F4 RID: 38900
		// (get) Token: 0x0601B127 RID: 110887 RVA: 0x0036B50B File Offset: 0x0036970B
		// (set) Token: 0x0601B128 RID: 110888 RVA: 0x0036B514 File Offset: 0x00369714
		public Latency Latency
		{
			get
			{
				return base.GetElement<Latency>(2);
			}
			set
			{
				base.SetElement<Latency>(2, value);
			}
		}

		// Token: 0x170097F5 RID: 38901
		// (get) Token: 0x0601B129 RID: 110889 RVA: 0x0036B51E File Offset: 0x0036971E
		// (set) Token: 0x0601B12A RID: 110890 RVA: 0x0036B527 File Offset: 0x00369727
		public ActiveArea ActiveArea
		{
			get
			{
				return base.GetElement<ActiveArea>(3);
			}
			set
			{
				base.SetElement<ActiveArea>(3, value);
			}
		}

		// Token: 0x0601B12B RID: 110891 RVA: 0x0036B534 File Offset: 0x00369734
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "manufacturer" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "model" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "serialNo" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "specificationRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B12C RID: 110892 RVA: 0x0036B5CE File Offset: 0x003697CE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InkSource>(deep);
		}

		// Token: 0x0601B12D RID: 110893 RVA: 0x0036B5D8 File Offset: 0x003697D8
		// Note: this type is marked as 'beforefieldinit'.
		static InkSource()
		{
			byte[] array = new byte[6];
			array[0] = 1;
			InkSource.attributeNamespaceIds = array;
			InkSource.eleTagNames = new string[] { "traceFormat", "sampleRate", "latency", "activeArea", "srcProperty", "channelProperties" };
			InkSource.eleNamespaceIds = new byte[] { 43, 43, 43, 43, 43, 43 };
		}

		// Token: 0x0400B2CC RID: 45772
		private const string tagName = "inkSource";

		// Token: 0x0400B2CD RID: 45773
		private const byte tagNsId = 43;

		// Token: 0x0400B2CE RID: 45774
		internal const int ElementTypeIdConst = 12662;

		// Token: 0x0400B2CF RID: 45775
		private static string[] attributeTagNames = new string[] { "id", "manufacturer", "model", "serialNo", "specificationRef", "description" };

		// Token: 0x0400B2D0 RID: 45776
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400B2D1 RID: 45777
		private static readonly string[] eleTagNames;

		// Token: 0x0400B2D2 RID: 45778
		private static readonly byte[] eleNamespaceIds;
	}
}
