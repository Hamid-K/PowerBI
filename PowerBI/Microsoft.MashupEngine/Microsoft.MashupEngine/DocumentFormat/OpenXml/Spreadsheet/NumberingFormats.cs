using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C71 RID: 11377
	[ChildElementInfo(typeof(NumberingFormat))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingFormats : OpenXmlCompositeElement
	{
		// Token: 0x170082CE RID: 33486
		// (get) Token: 0x0601830D RID: 99085 RVA: 0x0033F39E File Offset: 0x0033D59E
		public override string LocalName
		{
			get
			{
				return "numFmts";
			}
		}

		// Token: 0x170082CF RID: 33487
		// (get) Token: 0x0601830E RID: 99086 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082D0 RID: 33488
		// (get) Token: 0x0601830F RID: 99087 RVA: 0x0033F3A5 File Offset: 0x0033D5A5
		internal override int ElementTypeId
		{
			get
			{
				return 11357;
			}
		}

		// Token: 0x06018310 RID: 99088 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170082D1 RID: 33489
		// (get) Token: 0x06018311 RID: 99089 RVA: 0x0033F3AC File Offset: 0x0033D5AC
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberingFormats.attributeTagNames;
			}
		}

		// Token: 0x170082D2 RID: 33490
		// (get) Token: 0x06018312 RID: 99090 RVA: 0x0033F3B3 File Offset: 0x0033D5B3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberingFormats.attributeNamespaceIds;
			}
		}

		// Token: 0x170082D3 RID: 33491
		// (get) Token: 0x06018313 RID: 99091 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018314 RID: 99092 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018315 RID: 99093 RVA: 0x00293ECF File Offset: 0x002920CF
		public NumberingFormats()
		{
		}

		// Token: 0x06018316 RID: 99094 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NumberingFormats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018317 RID: 99095 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NumberingFormats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018318 RID: 99096 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NumberingFormats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018319 RID: 99097 RVA: 0x0033F3BA File Offset: 0x0033D5BA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "numFmt" == name)
			{
				return new NumberingFormat();
			}
			return null;
		}

		// Token: 0x0601831A RID: 99098 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601831B RID: 99099 RVA: 0x0033F3D5 File Offset: 0x0033D5D5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingFormats>(deep);
		}

		// Token: 0x0601831C RID: 99100 RVA: 0x0033F3E0 File Offset: 0x0033D5E0
		// Note: this type is marked as 'beforefieldinit'.
		static NumberingFormats()
		{
			byte[] array = new byte[1];
			NumberingFormats.attributeNamespaceIds = array;
		}

		// Token: 0x04009F41 RID: 40769
		private const string tagName = "numFmts";

		// Token: 0x04009F42 RID: 40770
		private const byte tagNsId = 22;

		// Token: 0x04009F43 RID: 40771
		internal const int ElementTypeIdConst = 11357;

		// Token: 0x04009F44 RID: 40772
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009F45 RID: 40773
		private static byte[] attributeNamespaceIds;
	}
}
