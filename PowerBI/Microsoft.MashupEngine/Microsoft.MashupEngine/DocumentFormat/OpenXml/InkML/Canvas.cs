using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003097 RID: 12439
	[ChildElementInfo(typeof(TraceFormat))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Canvas : OpenXmlCompositeElement
	{
		// Token: 0x170097D2 RID: 38866
		// (get) Token: 0x0601B0E0 RID: 110816 RVA: 0x0036B29B File Offset: 0x0036949B
		public override string LocalName
		{
			get
			{
				return "canvas";
			}
		}

		// Token: 0x170097D3 RID: 38867
		// (get) Token: 0x0601B0E1 RID: 110817 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x170097D4 RID: 38868
		// (get) Token: 0x0601B0E2 RID: 110818 RVA: 0x0036B2A2 File Offset: 0x003694A2
		internal override int ElementTypeId
		{
			get
			{
				return 12660;
			}
		}

		// Token: 0x0601B0E3 RID: 110819 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170097D5 RID: 38869
		// (get) Token: 0x0601B0E4 RID: 110820 RVA: 0x0036B2A9 File Offset: 0x003694A9
		internal override string[] AttributeTagNames
		{
			get
			{
				return Canvas.attributeTagNames;
			}
		}

		// Token: 0x170097D6 RID: 38870
		// (get) Token: 0x0601B0E5 RID: 110821 RVA: 0x0036B2B0 File Offset: 0x003694B0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Canvas.attributeNamespaceIds;
			}
		}

		// Token: 0x170097D7 RID: 38871
		// (get) Token: 0x0601B0E6 RID: 110822 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B0E7 RID: 110823 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170097D8 RID: 38872
		// (get) Token: 0x0601B0E8 RID: 110824 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601B0E9 RID: 110825 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "traceFormatRef")]
		public StringValue TraceFormatRef
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

		// Token: 0x0601B0EA RID: 110826 RVA: 0x00293ECF File Offset: 0x002920CF
		public Canvas()
		{
		}

		// Token: 0x0601B0EB RID: 110827 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Canvas(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B0EC RID: 110828 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Canvas(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B0ED RID: 110829 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Canvas(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B0EE RID: 110830 RVA: 0x0036B2B7 File Offset: 0x003694B7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "traceFormat" == name)
			{
				return new TraceFormat();
			}
			return null;
		}

		// Token: 0x170097D9 RID: 38873
		// (get) Token: 0x0601B0EF RID: 110831 RVA: 0x0036B2D2 File Offset: 0x003694D2
		internal override string[] ElementTagNames
		{
			get
			{
				return Canvas.eleTagNames;
			}
		}

		// Token: 0x170097DA RID: 38874
		// (get) Token: 0x0601B0F0 RID: 110832 RVA: 0x0036B2D9 File Offset: 0x003694D9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Canvas.eleNamespaceIds;
			}
		}

		// Token: 0x170097DB RID: 38875
		// (get) Token: 0x0601B0F1 RID: 110833 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170097DC RID: 38876
		// (get) Token: 0x0601B0F2 RID: 110834 RVA: 0x0036B2E0 File Offset: 0x003694E0
		// (set) Token: 0x0601B0F3 RID: 110835 RVA: 0x0036B2E9 File Offset: 0x003694E9
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

		// Token: 0x0601B0F4 RID: 110836 RVA: 0x0036B2F3 File Offset: 0x003694F3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "traceFormatRef" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B0F5 RID: 110837 RVA: 0x0036B32A File Offset: 0x0036952A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Canvas>(deep);
		}

		// Token: 0x0601B0F6 RID: 110838 RVA: 0x0036B334 File Offset: 0x00369534
		// Note: this type is marked as 'beforefieldinit'.
		static Canvas()
		{
			byte[] array = new byte[2];
			array[0] = 1;
			Canvas.attributeNamespaceIds = array;
			Canvas.eleTagNames = new string[] { "traceFormat" };
			Canvas.eleNamespaceIds = new byte[] { 43 };
		}

		// Token: 0x0400B2C0 RID: 45760
		private const string tagName = "canvas";

		// Token: 0x0400B2C1 RID: 45761
		private const byte tagNsId = 43;

		// Token: 0x0400B2C2 RID: 45762
		internal const int ElementTypeIdConst = 12660;

		// Token: 0x0400B2C3 RID: 45763
		private static string[] attributeTagNames = new string[] { "id", "traceFormatRef" };

		// Token: 0x0400B2C4 RID: 45764
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400B2C5 RID: 45765
		private static readonly string[] eleTagNames;

		// Token: 0x0400B2C6 RID: 45766
		private static readonly byte[] eleNamespaceIds;
	}
}
