using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200254B RID: 9547
	[GeneratedCode("DomGen", "2.0")]
	internal class BarDirection : OpenXmlLeafElement
	{
		// Token: 0x17005517 RID: 21783
		// (get) Token: 0x06011C51 RID: 72785 RVA: 0x002F1F8B File Offset: 0x002F018B
		public override string LocalName
		{
			get
			{
				return "barDir";
			}
		}

		// Token: 0x17005518 RID: 21784
		// (get) Token: 0x06011C52 RID: 72786 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005519 RID: 21785
		// (get) Token: 0x06011C53 RID: 72787 RVA: 0x002F1F92 File Offset: 0x002F0192
		internal override int ElementTypeId
		{
			get
			{
				return 10365;
			}
		}

		// Token: 0x06011C54 RID: 72788 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700551A RID: 21786
		// (get) Token: 0x06011C55 RID: 72789 RVA: 0x002F1F99 File Offset: 0x002F0199
		internal override string[] AttributeTagNames
		{
			get
			{
				return BarDirection.attributeTagNames;
			}
		}

		// Token: 0x1700551B RID: 21787
		// (get) Token: 0x06011C56 RID: 72790 RVA: 0x002F1FA0 File Offset: 0x002F01A0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BarDirection.attributeNamespaceIds;
			}
		}

		// Token: 0x1700551C RID: 21788
		// (get) Token: 0x06011C57 RID: 72791 RVA: 0x002F1FA7 File Offset: 0x002F01A7
		// (set) Token: 0x06011C58 RID: 72792 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<BarDirectionValues> Val
		{
			get
			{
				return (EnumValue<BarDirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011C5A RID: 72794 RVA: 0x002F1FB6 File Offset: 0x002F01B6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<BarDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011C5B RID: 72795 RVA: 0x002F1FD6 File Offset: 0x002F01D6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BarDirection>(deep);
		}

		// Token: 0x06011C5C RID: 72796 RVA: 0x002F1FE0 File Offset: 0x002F01E0
		// Note: this type is marked as 'beforefieldinit'.
		static BarDirection()
		{
			byte[] array = new byte[1];
			BarDirection.attributeNamespaceIds = array;
		}

		// Token: 0x04007C7D RID: 31869
		private const string tagName = "barDir";

		// Token: 0x04007C7E RID: 31870
		private const byte tagNsId = 11;

		// Token: 0x04007C7F RID: 31871
		internal const int ElementTypeIdConst = 10365;

		// Token: 0x04007C80 RID: 31872
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007C81 RID: 31873
		private static byte[] attributeNamespaceIds;
	}
}
