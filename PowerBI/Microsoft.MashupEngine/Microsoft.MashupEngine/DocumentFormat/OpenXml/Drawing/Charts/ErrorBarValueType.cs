using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200259D RID: 9629
	[GeneratedCode("DomGen", "2.0")]
	internal class ErrorBarValueType : OpenXmlLeafElement
	{
		// Token: 0x170056D6 RID: 22230
		// (get) Token: 0x06012033 RID: 73779 RVA: 0x002F4C83 File Offset: 0x002F2E83
		public override string LocalName
		{
			get
			{
				return "errValType";
			}
		}

		// Token: 0x170056D7 RID: 22231
		// (get) Token: 0x06012034 RID: 73780 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056D8 RID: 22232
		// (get) Token: 0x06012035 RID: 73781 RVA: 0x002F4C8A File Offset: 0x002F2E8A
		internal override int ElementTypeId
		{
			get
			{
				return 10448;
			}
		}

		// Token: 0x06012036 RID: 73782 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170056D9 RID: 22233
		// (get) Token: 0x06012037 RID: 73783 RVA: 0x002F4C91 File Offset: 0x002F2E91
		internal override string[] AttributeTagNames
		{
			get
			{
				return ErrorBarValueType.attributeTagNames;
			}
		}

		// Token: 0x170056DA RID: 22234
		// (get) Token: 0x06012038 RID: 73784 RVA: 0x002F4C98 File Offset: 0x002F2E98
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ErrorBarValueType.attributeNamespaceIds;
			}
		}

		// Token: 0x170056DB RID: 22235
		// (get) Token: 0x06012039 RID: 73785 RVA: 0x002F4C9F File Offset: 0x002F2E9F
		// (set) Token: 0x0601203A RID: 73786 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<ErrorValues> Val
		{
			get
			{
				return (EnumValue<ErrorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601203C RID: 73788 RVA: 0x002F4CAE File Offset: 0x002F2EAE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<ErrorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601203D RID: 73789 RVA: 0x002F4CCE File Offset: 0x002F2ECE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ErrorBarValueType>(deep);
		}

		// Token: 0x0601203E RID: 73790 RVA: 0x002F4CD8 File Offset: 0x002F2ED8
		// Note: this type is marked as 'beforefieldinit'.
		static ErrorBarValueType()
		{
			byte[] array = new byte[1];
			ErrorBarValueType.attributeNamespaceIds = array;
		}

		// Token: 0x04007DBB RID: 32187
		private const string tagName = "errValType";

		// Token: 0x04007DBC RID: 32188
		private const byte tagNsId = 11;

		// Token: 0x04007DBD RID: 32189
		internal const int ElementTypeIdConst = 10448;

		// Token: 0x04007DBE RID: 32190
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007DBF RID: 32191
		private static byte[] attributeNamespaceIds;
	}
}
