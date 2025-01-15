using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CB7 RID: 11447
	[ChildElementInfo(typeof(ChartFormat))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ChartFormats : OpenXmlCompositeElement
	{
		// Token: 0x170084B2 RID: 33970
		// (get) Token: 0x060187AB RID: 100267 RVA: 0x00341CD3 File Offset: 0x0033FED3
		public override string LocalName
		{
			get
			{
				return "chartFormats";
			}
		}

		// Token: 0x170084B3 RID: 33971
		// (get) Token: 0x060187AC RID: 100268 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084B4 RID: 33972
		// (get) Token: 0x060187AD RID: 100269 RVA: 0x00341CDA File Offset: 0x0033FEDA
		internal override int ElementTypeId
		{
			get
			{
				return 11427;
			}
		}

		// Token: 0x060187AE RID: 100270 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084B5 RID: 33973
		// (get) Token: 0x060187AF RID: 100271 RVA: 0x00341CE1 File Offset: 0x0033FEE1
		internal override string[] AttributeTagNames
		{
			get
			{
				return ChartFormats.attributeTagNames;
			}
		}

		// Token: 0x170084B6 RID: 33974
		// (get) Token: 0x060187B0 RID: 100272 RVA: 0x00341CE8 File Offset: 0x0033FEE8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ChartFormats.attributeNamespaceIds;
			}
		}

		// Token: 0x170084B7 RID: 33975
		// (get) Token: 0x060187B1 RID: 100273 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060187B2 RID: 100274 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060187B3 RID: 100275 RVA: 0x00293ECF File Offset: 0x002920CF
		public ChartFormats()
		{
		}

		// Token: 0x060187B4 RID: 100276 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ChartFormats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060187B5 RID: 100277 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ChartFormats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060187B6 RID: 100278 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ChartFormats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060187B7 RID: 100279 RVA: 0x00341CEF File Offset: 0x0033FEEF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "chartFormat" == name)
			{
				return new ChartFormat();
			}
			return null;
		}

		// Token: 0x060187B8 RID: 100280 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060187B9 RID: 100281 RVA: 0x00341D0A File Offset: 0x0033FF0A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartFormats>(deep);
		}

		// Token: 0x060187BA RID: 100282 RVA: 0x00341D14 File Offset: 0x0033FF14
		// Note: this type is marked as 'beforefieldinit'.
		static ChartFormats()
		{
			byte[] array = new byte[1];
			ChartFormats.attributeNamespaceIds = array;
		}

		// Token: 0x0400A078 RID: 41080
		private const string tagName = "chartFormats";

		// Token: 0x0400A079 RID: 41081
		private const byte tagNsId = 22;

		// Token: 0x0400A07A RID: 41082
		internal const int ElementTypeIdConst = 11427;

		// Token: 0x0400A07B RID: 41083
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A07C RID: 41084
		private static byte[] attributeNamespaceIds;
	}
}
