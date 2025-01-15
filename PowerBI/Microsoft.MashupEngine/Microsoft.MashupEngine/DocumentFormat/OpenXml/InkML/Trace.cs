using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200309C RID: 12444
	[GeneratedCode("DomGen", "2.0")]
	internal class Trace : OpenXmlLeafTextElement
	{
		// Token: 0x17009807 RID: 38919
		// (get) Token: 0x0601B154 RID: 110932 RVA: 0x0036B877 File Offset: 0x00369A77
		public override string LocalName
		{
			get
			{
				return "trace";
			}
		}

		// Token: 0x17009808 RID: 38920
		// (get) Token: 0x0601B155 RID: 110933 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009809 RID: 38921
		// (get) Token: 0x0601B156 RID: 110934 RVA: 0x0036B87E File Offset: 0x00369A7E
		internal override int ElementTypeId
		{
			get
			{
				return 12665;
			}
		}

		// Token: 0x0601B157 RID: 110935 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700980A RID: 38922
		// (get) Token: 0x0601B158 RID: 110936 RVA: 0x0036B885 File Offset: 0x00369A85
		internal override string[] AttributeTagNames
		{
			get
			{
				return Trace.attributeTagNames;
			}
		}

		// Token: 0x1700980B RID: 38923
		// (get) Token: 0x0601B159 RID: 110937 RVA: 0x0036B88C File Offset: 0x00369A8C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Trace.attributeNamespaceIds;
			}
		}

		// Token: 0x1700980C RID: 38924
		// (get) Token: 0x0601B15A RID: 110938 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B15B RID: 110939 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700980D RID: 38925
		// (get) Token: 0x0601B15C RID: 110940 RVA: 0x0036B893 File Offset: 0x00369A93
		// (set) Token: 0x0601B15D RID: 110941 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public EnumValue<TraceTypeValues> Type
		{
			get
			{
				return (EnumValue<TraceTypeValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700980E RID: 38926
		// (get) Token: 0x0601B15E RID: 110942 RVA: 0x0036B8A2 File Offset: 0x00369AA2
		// (set) Token: 0x0601B15F RID: 110943 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "continuation")]
		public EnumValue<TraceContinuationValues> Continuation
		{
			get
			{
				return (EnumValue<TraceContinuationValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700980F RID: 38927
		// (get) Token: 0x0601B160 RID: 110944 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601B161 RID: 110945 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "priorRef")]
		public StringValue PriorRef
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

		// Token: 0x17009810 RID: 38928
		// (get) Token: 0x0601B162 RID: 110946 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601B163 RID: 110947 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "contextRef")]
		public StringValue ContextRef
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

		// Token: 0x17009811 RID: 38929
		// (get) Token: 0x0601B164 RID: 110948 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0601B165 RID: 110949 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "brushRef")]
		public StringValue BrushRef
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

		// Token: 0x17009812 RID: 38930
		// (get) Token: 0x0601B166 RID: 110950 RVA: 0x0036A194 File Offset: 0x00368394
		// (set) Token: 0x0601B167 RID: 110951 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "duration")]
		public DecimalValue Duration
		{
			get
			{
				return (DecimalValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17009813 RID: 38931
		// (get) Token: 0x0601B168 RID: 110952 RVA: 0x002BEEE1 File Offset: 0x002BD0E1
		// (set) Token: 0x0601B169 RID: 110953 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "timeOffset")]
		public DecimalValue TimeOffset
		{
			get
			{
				return (DecimalValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x0601B16A RID: 110954 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Trace()
		{
		}

		// Token: 0x0601B16B RID: 110955 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Trace(string text)
			: base(text)
		{
		}

		// Token: 0x0601B16C RID: 110956 RVA: 0x0036B8B4 File Offset: 0x00369AB4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601B16D RID: 110957 RVA: 0x0036B8D0 File Offset: 0x00369AD0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<TraceTypeValues>();
			}
			if (namespaceId == 0 && "continuation" == name)
			{
				return new EnumValue<TraceContinuationValues>();
			}
			if (namespaceId == 0 && "priorRef" == name)
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
			if (namespaceId == 0 && "duration" == name)
			{
				return new DecimalValue();
			}
			if (namespaceId == 0 && "timeOffset" == name)
			{
				return new DecimalValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B16E RID: 110958 RVA: 0x0036B996 File Offset: 0x00369B96
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Trace>(deep);
		}

		// Token: 0x0601B16F RID: 110959 RVA: 0x0036B9A0 File Offset: 0x00369BA0
		// Note: this type is marked as 'beforefieldinit'.
		static Trace()
		{
			byte[] array = new byte[8];
			array[0] = 1;
			Trace.attributeNamespaceIds = array;
		}

		// Token: 0x0400B2DD RID: 45789
		private const string tagName = "trace";

		// Token: 0x0400B2DE RID: 45790
		private const byte tagNsId = 43;

		// Token: 0x0400B2DF RID: 45791
		internal const int ElementTypeIdConst = 12665;

		// Token: 0x0400B2E0 RID: 45792
		private static string[] attributeTagNames = new string[] { "id", "type", "continuation", "priorRef", "contextRef", "brushRef", "duration", "timeOffset" };

		// Token: 0x0400B2E1 RID: 45793
		private static byte[] attributeNamespaceIds;
	}
}
