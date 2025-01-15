using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025B8 RID: 9656
	[GeneratedCode("DomGen", "2.0")]
	internal class LabelOffset : OpenXmlLeafElement
	{
		// Token: 0x1700575E RID: 22366
		// (get) Token: 0x06012169 RID: 74089 RVA: 0x002F56B3 File Offset: 0x002F38B3
		public override string LocalName
		{
			get
			{
				return "lblOffset";
			}
		}

		// Token: 0x1700575F RID: 22367
		// (get) Token: 0x0601216A RID: 74090 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005760 RID: 22368
		// (get) Token: 0x0601216B RID: 74091 RVA: 0x002F56BA File Offset: 0x002F38BA
		internal override int ElementTypeId
		{
			get
			{
				return 10483;
			}
		}

		// Token: 0x0601216C RID: 74092 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005761 RID: 22369
		// (get) Token: 0x0601216D RID: 74093 RVA: 0x002F56C1 File Offset: 0x002F38C1
		internal override string[] AttributeTagNames
		{
			get
			{
				return LabelOffset.attributeTagNames;
			}
		}

		// Token: 0x17005762 RID: 22370
		// (get) Token: 0x0601216E RID: 74094 RVA: 0x002F56C8 File Offset: 0x002F38C8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LabelOffset.attributeNamespaceIds;
			}
		}

		// Token: 0x17005763 RID: 22371
		// (get) Token: 0x0601216F RID: 74095 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x06012170 RID: 74096 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public UInt16Value Val
		{
			get
			{
				return (UInt16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012172 RID: 74098 RVA: 0x002F41C3 File Offset: 0x002F23C3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012173 RID: 74099 RVA: 0x002F56CF File Offset: 0x002F38CF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LabelOffset>(deep);
		}

		// Token: 0x06012174 RID: 74100 RVA: 0x002F56D8 File Offset: 0x002F38D8
		// Note: this type is marked as 'beforefieldinit'.
		static LabelOffset()
		{
			byte[] array = new byte[1];
			LabelOffset.attributeNamespaceIds = array;
		}

		// Token: 0x04007E25 RID: 32293
		private const string tagName = "lblOffset";

		// Token: 0x04007E26 RID: 32294
		private const byte tagNsId = 11;

		// Token: 0x04007E27 RID: 32295
		internal const int ElementTypeIdConst = 10483;

		// Token: 0x04007E28 RID: 32296
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E29 RID: 32297
		private static byte[] attributeNamespaceIds;
	}
}
