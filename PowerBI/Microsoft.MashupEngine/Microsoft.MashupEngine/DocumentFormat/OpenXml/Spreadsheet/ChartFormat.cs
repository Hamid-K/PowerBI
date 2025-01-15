using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B7B RID: 11131
	[ChildElementInfo(typeof(PivotArea))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ChartFormat : OpenXmlCompositeElement
	{
		// Token: 0x17007A0C RID: 31244
		// (get) Token: 0x06016FE5 RID: 94181 RVA: 0x0033175B File Offset: 0x0032F95B
		public override string LocalName
		{
			get
			{
				return "chartFormat";
			}
		}

		// Token: 0x17007A0D RID: 31245
		// (get) Token: 0x06016FE6 RID: 94182 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A0E RID: 31246
		// (get) Token: 0x06016FE7 RID: 94183 RVA: 0x00331762 File Offset: 0x0032F962
		internal override int ElementTypeId
		{
			get
			{
				return 11111;
			}
		}

		// Token: 0x06016FE8 RID: 94184 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A0F RID: 31247
		// (get) Token: 0x06016FE9 RID: 94185 RVA: 0x00331769 File Offset: 0x0032F969
		internal override string[] AttributeTagNames
		{
			get
			{
				return ChartFormat.attributeTagNames;
			}
		}

		// Token: 0x17007A10 RID: 31248
		// (get) Token: 0x06016FEA RID: 94186 RVA: 0x00331770 File Offset: 0x0032F970
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ChartFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A11 RID: 31249
		// (get) Token: 0x06016FEB RID: 94187 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016FEC RID: 94188 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "chart")]
		public UInt32Value Chart
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007A12 RID: 31250
		// (get) Token: 0x06016FED RID: 94189 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06016FEE RID: 94190 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "format")]
		public UInt32Value Format
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007A13 RID: 31251
		// (get) Token: 0x06016FEF RID: 94191 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016FF0 RID: 94192 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "series")]
		public BooleanValue Series
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06016FF1 RID: 94193 RVA: 0x00293ECF File Offset: 0x002920CF
		public ChartFormat()
		{
		}

		// Token: 0x06016FF2 RID: 94194 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ChartFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016FF3 RID: 94195 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ChartFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016FF4 RID: 94196 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ChartFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016FF5 RID: 94197 RVA: 0x002E9D93 File Offset: 0x002E7F93
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotArea" == name)
			{
				return new PivotArea();
			}
			return null;
		}

		// Token: 0x17007A14 RID: 31252
		// (get) Token: 0x06016FF6 RID: 94198 RVA: 0x00331777 File Offset: 0x0032F977
		internal override string[] ElementTagNames
		{
			get
			{
				return ChartFormat.eleTagNames;
			}
		}

		// Token: 0x17007A15 RID: 31253
		// (get) Token: 0x06016FF7 RID: 94199 RVA: 0x0033177E File Offset: 0x0032F97E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ChartFormat.eleNamespaceIds;
			}
		}

		// Token: 0x17007A16 RID: 31254
		// (get) Token: 0x06016FF8 RID: 94200 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007A17 RID: 31255
		// (get) Token: 0x06016FF9 RID: 94201 RVA: 0x003304CB File Offset: 0x0032E6CB
		// (set) Token: 0x06016FFA RID: 94202 RVA: 0x003304D4 File Offset: 0x0032E6D4
		public PivotArea PivotArea
		{
			get
			{
				return base.GetElement<PivotArea>(0);
			}
			set
			{
				base.SetElement<PivotArea>(0, value);
			}
		}

		// Token: 0x06016FFB RID: 94203 RVA: 0x00331788 File Offset: 0x0032F988
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "chart" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "format" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "series" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016FFC RID: 94204 RVA: 0x003317DF File Offset: 0x0032F9DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartFormat>(deep);
		}

		// Token: 0x06016FFD RID: 94205 RVA: 0x003317E8 File Offset: 0x0032F9E8
		// Note: this type is marked as 'beforefieldinit'.
		static ChartFormat()
		{
			byte[] array = new byte[3];
			ChartFormat.attributeNamespaceIds = array;
			ChartFormat.eleTagNames = new string[] { "pivotArea" };
			ChartFormat.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009AAB RID: 39595
		private const string tagName = "chartFormat";

		// Token: 0x04009AAC RID: 39596
		private const byte tagNsId = 22;

		// Token: 0x04009AAD RID: 39597
		internal const int ElementTypeIdConst = 11111;

		// Token: 0x04009AAE RID: 39598
		private static string[] attributeTagNames = new string[] { "chart", "format", "series" };

		// Token: 0x04009AAF RID: 39599
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009AB0 RID: 39600
		private static readonly string[] eleTagNames;

		// Token: 0x04009AB1 RID: 39601
		private static readonly byte[] eleNamespaceIds;
	}
}
