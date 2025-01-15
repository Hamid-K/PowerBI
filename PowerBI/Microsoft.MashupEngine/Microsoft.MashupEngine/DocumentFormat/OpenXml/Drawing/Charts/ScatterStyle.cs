using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025D8 RID: 9688
	[GeneratedCode("DomGen", "2.0")]
	internal class ScatterStyle : OpenXmlLeafElement
	{
		// Token: 0x17005842 RID: 22594
		// (get) Token: 0x06012362 RID: 74594 RVA: 0x002F7492 File Offset: 0x002F5692
		public override string LocalName
		{
			get
			{
				return "scatterStyle";
			}
		}

		// Token: 0x17005843 RID: 22595
		// (get) Token: 0x06012363 RID: 74595 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005844 RID: 22596
		// (get) Token: 0x06012364 RID: 74596 RVA: 0x002F7499 File Offset: 0x002F5699
		internal override int ElementTypeId
		{
			get
			{
				return 10530;
			}
		}

		// Token: 0x06012365 RID: 74597 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005845 RID: 22597
		// (get) Token: 0x06012366 RID: 74598 RVA: 0x002F74A0 File Offset: 0x002F56A0
		internal override string[] AttributeTagNames
		{
			get
			{
				return ScatterStyle.attributeTagNames;
			}
		}

		// Token: 0x17005846 RID: 22598
		// (get) Token: 0x06012367 RID: 74599 RVA: 0x002F74A7 File Offset: 0x002F56A7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ScatterStyle.attributeNamespaceIds;
			}
		}

		// Token: 0x17005847 RID: 22599
		// (get) Token: 0x06012368 RID: 74600 RVA: 0x002F74AE File Offset: 0x002F56AE
		// (set) Token: 0x06012369 RID: 74601 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<ScatterStyleValues> Val
		{
			get
			{
				return (EnumValue<ScatterStyleValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601236B RID: 74603 RVA: 0x002F74BD File Offset: 0x002F56BD
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<ScatterStyleValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601236C RID: 74604 RVA: 0x002F74DD File Offset: 0x002F56DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScatterStyle>(deep);
		}

		// Token: 0x0601236D RID: 74605 RVA: 0x002F74E8 File Offset: 0x002F56E8
		// Note: this type is marked as 'beforefieldinit'.
		static ScatterStyle()
		{
			byte[] array = new byte[1];
			ScatterStyle.attributeNamespaceIds = array;
		}

		// Token: 0x04007EA7 RID: 32423
		private const string tagName = "scatterStyle";

		// Token: 0x04007EA8 RID: 32424
		private const byte tagNsId = 11;

		// Token: 0x04007EA9 RID: 32425
		internal const int ElementTypeIdConst = 10530;

		// Token: 0x04007EAA RID: 32426
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007EAB RID: 32427
		private static byte[] attributeNamespaceIds;
	}
}
