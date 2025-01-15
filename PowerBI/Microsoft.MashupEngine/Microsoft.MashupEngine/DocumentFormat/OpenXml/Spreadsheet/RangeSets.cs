using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B50 RID: 11088
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RangeSet))]
	internal class RangeSets : OpenXmlCompositeElement
	{
		// Token: 0x17007844 RID: 30788
		// (get) Token: 0x06016BFA RID: 93178 RVA: 0x0032EA63 File Offset: 0x0032CC63
		public override string LocalName
		{
			get
			{
				return "rangeSets";
			}
		}

		// Token: 0x17007845 RID: 30789
		// (get) Token: 0x06016BFB RID: 93179 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007846 RID: 30790
		// (get) Token: 0x06016BFC RID: 93180 RVA: 0x0032EA6A File Offset: 0x0032CC6A
		internal override int ElementTypeId
		{
			get
			{
				return 11071;
			}
		}

		// Token: 0x06016BFD RID: 93181 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007847 RID: 30791
		// (get) Token: 0x06016BFE RID: 93182 RVA: 0x0032EA71 File Offset: 0x0032CC71
		internal override string[] AttributeTagNames
		{
			get
			{
				return RangeSets.attributeTagNames;
			}
		}

		// Token: 0x17007848 RID: 30792
		// (get) Token: 0x06016BFF RID: 93183 RVA: 0x0032EA78 File Offset: 0x0032CC78
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RangeSets.attributeNamespaceIds;
			}
		}

		// Token: 0x17007849 RID: 30793
		// (get) Token: 0x06016C00 RID: 93184 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016C01 RID: 93185 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06016C02 RID: 93186 RVA: 0x00293ECF File Offset: 0x002920CF
		public RangeSets()
		{
		}

		// Token: 0x06016C03 RID: 93187 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RangeSets(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016C04 RID: 93188 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RangeSets(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016C05 RID: 93189 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RangeSets(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016C06 RID: 93190 RVA: 0x0032EA7F File Offset: 0x0032CC7F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "rangeSet" == name)
			{
				return new RangeSet();
			}
			return null;
		}

		// Token: 0x06016C07 RID: 93191 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016C08 RID: 93192 RVA: 0x0032EA9A File Offset: 0x0032CC9A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RangeSets>(deep);
		}

		// Token: 0x06016C09 RID: 93193 RVA: 0x0032EAA4 File Offset: 0x0032CCA4
		// Note: this type is marked as 'beforefieldinit'.
		static RangeSets()
		{
			byte[] array = new byte[1];
			RangeSets.attributeNamespaceIds = array;
		}

		// Token: 0x040099CE RID: 39374
		private const string tagName = "rangeSets";

		// Token: 0x040099CF RID: 39375
		private const byte tagNsId = 22;

		// Token: 0x040099D0 RID: 39376
		internal const int ElementTypeIdConst = 11071;

		// Token: 0x040099D1 RID: 39377
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x040099D2 RID: 39378
		private static byte[] attributeNamespaceIds;
	}
}
