using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002593 RID: 9619
	[GeneratedCode("DomGen", "2.0")]
	internal class Symbol : OpenXmlLeafElement
	{
		// Token: 0x1700568B RID: 22155
		// (get) Token: 0x06011F97 RID: 73623 RVA: 0x002F4589 File Offset: 0x002F2789
		public override string LocalName
		{
			get
			{
				return "symbol";
			}
		}

		// Token: 0x1700568C RID: 22156
		// (get) Token: 0x06011F98 RID: 73624 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700568D RID: 22157
		// (get) Token: 0x06011F99 RID: 73625 RVA: 0x002F4590 File Offset: 0x002F2790
		internal override int ElementTypeId
		{
			get
			{
				return 10429;
			}
		}

		// Token: 0x06011F9A RID: 73626 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700568E RID: 22158
		// (get) Token: 0x06011F9B RID: 73627 RVA: 0x002F4597 File Offset: 0x002F2797
		internal override string[] AttributeTagNames
		{
			get
			{
				return Symbol.attributeTagNames;
			}
		}

		// Token: 0x1700568F RID: 22159
		// (get) Token: 0x06011F9C RID: 73628 RVA: 0x002F459E File Offset: 0x002F279E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Symbol.attributeNamespaceIds;
			}
		}

		// Token: 0x17005690 RID: 22160
		// (get) Token: 0x06011F9D RID: 73629 RVA: 0x002F45A5 File Offset: 0x002F27A5
		// (set) Token: 0x06011F9E RID: 73630 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<MarkerStyleValues> Val
		{
			get
			{
				return (EnumValue<MarkerStyleValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011FA0 RID: 73632 RVA: 0x002F45B4 File Offset: 0x002F27B4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<MarkerStyleValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011FA1 RID: 73633 RVA: 0x002F45D4 File Offset: 0x002F27D4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Symbol>(deep);
		}

		// Token: 0x06011FA2 RID: 73634 RVA: 0x002F45E0 File Offset: 0x002F27E0
		// Note: this type is marked as 'beforefieldinit'.
		static Symbol()
		{
			byte[] array = new byte[1];
			Symbol.attributeNamespaceIds = array;
		}

		// Token: 0x04007D89 RID: 32137
		private const string tagName = "symbol";

		// Token: 0x04007D8A RID: 32138
		private const byte tagNsId = 11;

		// Token: 0x04007D8B RID: 32139
		internal const int ElementTypeIdConst = 10429;

		// Token: 0x04007D8C RID: 32140
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007D8D RID: 32141
		private static byte[] attributeNamespaceIds;
	}
}
