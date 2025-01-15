using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002548 RID: 9544
	[GeneratedCode("DomGen", "2.0")]
	internal class Grouping : OpenXmlLeafElement
	{
		// Token: 0x17005502 RID: 21762
		// (get) Token: 0x06011C21 RID: 72737 RVA: 0x002F1A9D File Offset: 0x002EFC9D
		public override string LocalName
		{
			get
			{
				return "grouping";
			}
		}

		// Token: 0x17005503 RID: 21763
		// (get) Token: 0x06011C22 RID: 72738 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005504 RID: 21764
		// (get) Token: 0x06011C23 RID: 72739 RVA: 0x002F1AA4 File Offset: 0x002EFCA4
		internal override int ElementTypeId
		{
			get
			{
				return 10360;
			}
		}

		// Token: 0x06011C24 RID: 72740 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005505 RID: 21765
		// (get) Token: 0x06011C25 RID: 72741 RVA: 0x002F1AAB File Offset: 0x002EFCAB
		internal override string[] AttributeTagNames
		{
			get
			{
				return Grouping.attributeTagNames;
			}
		}

		// Token: 0x17005506 RID: 21766
		// (get) Token: 0x06011C26 RID: 72742 RVA: 0x002F1AB2 File Offset: 0x002EFCB2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Grouping.attributeNamespaceIds;
			}
		}

		// Token: 0x17005507 RID: 21767
		// (get) Token: 0x06011C27 RID: 72743 RVA: 0x002F1AB9 File Offset: 0x002EFCB9
		// (set) Token: 0x06011C28 RID: 72744 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<GroupingValues> Val
		{
			get
			{
				return (EnumValue<GroupingValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011C2A RID: 72746 RVA: 0x002F1AC8 File Offset: 0x002EFCC8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<GroupingValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011C2B RID: 72747 RVA: 0x002F1AE8 File Offset: 0x002EFCE8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Grouping>(deep);
		}

		// Token: 0x06011C2C RID: 72748 RVA: 0x002F1AF4 File Offset: 0x002EFCF4
		// Note: this type is marked as 'beforefieldinit'.
		static Grouping()
		{
			byte[] array = new byte[1];
			Grouping.attributeNamespaceIds = array;
		}

		// Token: 0x04007C70 RID: 31856
		private const string tagName = "grouping";

		// Token: 0x04007C71 RID: 31857
		private const byte tagNsId = 11;

		// Token: 0x04007C72 RID: 31858
		internal const int ElementTypeIdConst = 10360;

		// Token: 0x04007C73 RID: 31859
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007C74 RID: 31860
		private static byte[] attributeNamespaceIds;
	}
}
