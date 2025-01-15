using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CB6 RID: 11446
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConditionalFormat))]
	internal class ConditionalFormats : OpenXmlCompositeElement
	{
		// Token: 0x170084AC RID: 33964
		// (get) Token: 0x0601879B RID: 100251 RVA: 0x002E9357 File Offset: 0x002E7557
		public override string LocalName
		{
			get
			{
				return "conditionalFormats";
			}
		}

		// Token: 0x170084AD RID: 33965
		// (get) Token: 0x0601879C RID: 100252 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084AE RID: 33966
		// (get) Token: 0x0601879D RID: 100253 RVA: 0x00341C6B File Offset: 0x0033FE6B
		internal override int ElementTypeId
		{
			get
			{
				return 11426;
			}
		}

		// Token: 0x0601879E RID: 100254 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084AF RID: 33967
		// (get) Token: 0x0601879F RID: 100255 RVA: 0x00341C72 File Offset: 0x0033FE72
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormats.attributeTagNames;
			}
		}

		// Token: 0x170084B0 RID: 33968
		// (get) Token: 0x060187A0 RID: 100256 RVA: 0x00341C79 File Offset: 0x0033FE79
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormats.attributeNamespaceIds;
			}
		}

		// Token: 0x170084B1 RID: 33969
		// (get) Token: 0x060187A1 RID: 100257 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060187A2 RID: 100258 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060187A3 RID: 100259 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormats()
		{
		}

		// Token: 0x060187A4 RID: 100260 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060187A5 RID: 100261 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060187A6 RID: 100262 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060187A7 RID: 100263 RVA: 0x00341C80 File Offset: 0x0033FE80
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "conditionalFormat" == name)
			{
				return new ConditionalFormat();
			}
			return null;
		}

		// Token: 0x060187A8 RID: 100264 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060187A9 RID: 100265 RVA: 0x00341C9B File Offset: 0x0033FE9B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormats>(deep);
		}

		// Token: 0x060187AA RID: 100266 RVA: 0x00341CA4 File Offset: 0x0033FEA4
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormats()
		{
			byte[] array = new byte[1];
			ConditionalFormats.attributeNamespaceIds = array;
		}

		// Token: 0x0400A073 RID: 41075
		private const string tagName = "conditionalFormats";

		// Token: 0x0400A074 RID: 41076
		private const byte tagNsId = 22;

		// Token: 0x0400A075 RID: 41077
		internal const int ElementTypeIdConst = 11426;

		// Token: 0x0400A076 RID: 41078
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A077 RID: 41079
		private static byte[] attributeNamespaceIds;
	}
}
