using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200259C RID: 9628
	[GeneratedCode("DomGen", "2.0")]
	internal class ErrorBarType : OpenXmlLeafElement
	{
		// Token: 0x170056D0 RID: 22224
		// (get) Token: 0x06012027 RID: 73767 RVA: 0x002F4BFF File Offset: 0x002F2DFF
		public override string LocalName
		{
			get
			{
				return "errBarType";
			}
		}

		// Token: 0x170056D1 RID: 22225
		// (get) Token: 0x06012028 RID: 73768 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056D2 RID: 22226
		// (get) Token: 0x06012029 RID: 73769 RVA: 0x002F4C06 File Offset: 0x002F2E06
		internal override int ElementTypeId
		{
			get
			{
				return 10447;
			}
		}

		// Token: 0x0601202A RID: 73770 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170056D3 RID: 22227
		// (get) Token: 0x0601202B RID: 73771 RVA: 0x002F4C0D File Offset: 0x002F2E0D
		internal override string[] AttributeTagNames
		{
			get
			{
				return ErrorBarType.attributeTagNames;
			}
		}

		// Token: 0x170056D4 RID: 22228
		// (get) Token: 0x0601202C RID: 73772 RVA: 0x002F4C14 File Offset: 0x002F2E14
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ErrorBarType.attributeNamespaceIds;
			}
		}

		// Token: 0x170056D5 RID: 22229
		// (get) Token: 0x0601202D RID: 73773 RVA: 0x002F4C1B File Offset: 0x002F2E1B
		// (set) Token: 0x0601202E RID: 73774 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<ErrorBarValues> Val
		{
			get
			{
				return (EnumValue<ErrorBarValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012030 RID: 73776 RVA: 0x002F4C2A File Offset: 0x002F2E2A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<ErrorBarValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012031 RID: 73777 RVA: 0x002F4C4A File Offset: 0x002F2E4A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ErrorBarType>(deep);
		}

		// Token: 0x06012032 RID: 73778 RVA: 0x002F4C54 File Offset: 0x002F2E54
		// Note: this type is marked as 'beforefieldinit'.
		static ErrorBarType()
		{
			byte[] array = new byte[1];
			ErrorBarType.attributeNamespaceIds = array;
		}

		// Token: 0x04007DB6 RID: 32182
		private const string tagName = "errBarType";

		// Token: 0x04007DB7 RID: 32183
		private const byte tagNsId = 11;

		// Token: 0x04007DB8 RID: 32184
		internal const int ElementTypeIdConst = 10447;

		// Token: 0x04007DB9 RID: 32185
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007DBA RID: 32186
		private static byte[] attributeNamespaceIds;
	}
}
