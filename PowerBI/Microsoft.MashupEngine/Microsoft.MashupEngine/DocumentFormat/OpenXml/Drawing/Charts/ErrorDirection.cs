using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200259B RID: 9627
	[GeneratedCode("DomGen", "2.0")]
	internal class ErrorDirection : OpenXmlLeafElement
	{
		// Token: 0x170056CA RID: 22218
		// (get) Token: 0x0601201B RID: 73755 RVA: 0x002F4B7C File Offset: 0x002F2D7C
		public override string LocalName
		{
			get
			{
				return "errDir";
			}
		}

		// Token: 0x170056CB RID: 22219
		// (get) Token: 0x0601201C RID: 73756 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056CC RID: 22220
		// (get) Token: 0x0601201D RID: 73757 RVA: 0x002F4B83 File Offset: 0x002F2D83
		internal override int ElementTypeId
		{
			get
			{
				return 10446;
			}
		}

		// Token: 0x0601201E RID: 73758 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170056CD RID: 22221
		// (get) Token: 0x0601201F RID: 73759 RVA: 0x002F4B8A File Offset: 0x002F2D8A
		internal override string[] AttributeTagNames
		{
			get
			{
				return ErrorDirection.attributeTagNames;
			}
		}

		// Token: 0x170056CE RID: 22222
		// (get) Token: 0x06012020 RID: 73760 RVA: 0x002F4B91 File Offset: 0x002F2D91
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ErrorDirection.attributeNamespaceIds;
			}
		}

		// Token: 0x170056CF RID: 22223
		// (get) Token: 0x06012021 RID: 73761 RVA: 0x002F4B98 File Offset: 0x002F2D98
		// (set) Token: 0x06012022 RID: 73762 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<ErrorBarDirectionValues> Val
		{
			get
			{
				return (EnumValue<ErrorBarDirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012024 RID: 73764 RVA: 0x002F4BA7 File Offset: 0x002F2DA7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<ErrorBarDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012025 RID: 73765 RVA: 0x002F4BC7 File Offset: 0x002F2DC7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ErrorDirection>(deep);
		}

		// Token: 0x06012026 RID: 73766 RVA: 0x002F4BD0 File Offset: 0x002F2DD0
		// Note: this type is marked as 'beforefieldinit'.
		static ErrorDirection()
		{
			byte[] array = new byte[1];
			ErrorDirection.attributeNamespaceIds = array;
		}

		// Token: 0x04007DB1 RID: 32177
		private const string tagName = "errDir";

		// Token: 0x04007DB2 RID: 32178
		private const byte tagNsId = 11;

		// Token: 0x04007DB3 RID: 32179
		internal const int ElementTypeIdConst = 10446;

		// Token: 0x04007DB4 RID: 32180
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007DB5 RID: 32181
		private static byte[] attributeNamespaceIds;
	}
}
