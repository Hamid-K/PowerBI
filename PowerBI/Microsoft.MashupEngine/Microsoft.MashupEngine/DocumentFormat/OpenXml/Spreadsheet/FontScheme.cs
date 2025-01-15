using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BAB RID: 11179
	[GeneratedCode("DomGen", "2.0")]
	internal class FontScheme : OpenXmlLeafElement
	{
		// Token: 0x17007B6D RID: 31597
		// (get) Token: 0x060172DA RID: 94938 RVA: 0x00333791 File Offset: 0x00331991
		public override string LocalName
		{
			get
			{
				return "scheme";
			}
		}

		// Token: 0x17007B6E RID: 31598
		// (get) Token: 0x060172DB RID: 94939 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B6F RID: 31599
		// (get) Token: 0x060172DC RID: 94940 RVA: 0x00333798 File Offset: 0x00331998
		internal override int ElementTypeId
		{
			get
			{
				return 11149;
			}
		}

		// Token: 0x060172DD RID: 94941 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007B70 RID: 31600
		// (get) Token: 0x060172DE RID: 94942 RVA: 0x0033379F File Offset: 0x0033199F
		internal override string[] AttributeTagNames
		{
			get
			{
				return FontScheme.attributeTagNames;
			}
		}

		// Token: 0x17007B71 RID: 31601
		// (get) Token: 0x060172DF RID: 94943 RVA: 0x003337A6 File Offset: 0x003319A6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FontScheme.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B72 RID: 31602
		// (get) Token: 0x060172E0 RID: 94944 RVA: 0x003337AD File Offset: 0x003319AD
		// (set) Token: 0x060172E1 RID: 94945 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<FontSchemeValues> Val
		{
			get
			{
				return (EnumValue<FontSchemeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060172E3 RID: 94947 RVA: 0x003337BC File Offset: 0x003319BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<FontSchemeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060172E4 RID: 94948 RVA: 0x003337DC File Offset: 0x003319DC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontScheme>(deep);
		}

		// Token: 0x060172E5 RID: 94949 RVA: 0x003337E8 File Offset: 0x003319E8
		// Note: this type is marked as 'beforefieldinit'.
		static FontScheme()
		{
			byte[] array = new byte[1];
			FontScheme.attributeNamespaceIds = array;
		}

		// Token: 0x04009B76 RID: 39798
		private const string tagName = "scheme";

		// Token: 0x04009B77 RID: 39799
		private const byte tagNsId = 22;

		// Token: 0x04009B78 RID: 39800
		internal const int ElementTypeIdConst = 11149;

		// Token: 0x04009B79 RID: 39801
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009B7A RID: 39802
		private static byte[] attributeNamespaceIds;
	}
}
