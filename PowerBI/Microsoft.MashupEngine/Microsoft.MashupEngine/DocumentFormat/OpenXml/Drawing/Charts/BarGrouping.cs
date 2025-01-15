using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200254C RID: 9548
	[GeneratedCode("DomGen", "2.0")]
	internal class BarGrouping : OpenXmlLeafElement
	{
		// Token: 0x1700551D RID: 21789
		// (get) Token: 0x06011C5D RID: 72797 RVA: 0x002F1A9D File Offset: 0x002EFC9D
		public override string LocalName
		{
			get
			{
				return "grouping";
			}
		}

		// Token: 0x1700551E RID: 21790
		// (get) Token: 0x06011C5E RID: 72798 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700551F RID: 21791
		// (get) Token: 0x06011C5F RID: 72799 RVA: 0x002F200F File Offset: 0x002F020F
		internal override int ElementTypeId
		{
			get
			{
				return 10366;
			}
		}

		// Token: 0x06011C60 RID: 72800 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005520 RID: 21792
		// (get) Token: 0x06011C61 RID: 72801 RVA: 0x002F2016 File Offset: 0x002F0216
		internal override string[] AttributeTagNames
		{
			get
			{
				return BarGrouping.attributeTagNames;
			}
		}

		// Token: 0x17005521 RID: 21793
		// (get) Token: 0x06011C62 RID: 72802 RVA: 0x002F201D File Offset: 0x002F021D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BarGrouping.attributeNamespaceIds;
			}
		}

		// Token: 0x17005522 RID: 21794
		// (get) Token: 0x06011C63 RID: 72803 RVA: 0x002F2024 File Offset: 0x002F0224
		// (set) Token: 0x06011C64 RID: 72804 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<BarGroupingValues> Val
		{
			get
			{
				return (EnumValue<BarGroupingValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011C66 RID: 72806 RVA: 0x002F2033 File Offset: 0x002F0233
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<BarGroupingValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011C67 RID: 72807 RVA: 0x002F2053 File Offset: 0x002F0253
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BarGrouping>(deep);
		}

		// Token: 0x06011C68 RID: 72808 RVA: 0x002F205C File Offset: 0x002F025C
		// Note: this type is marked as 'beforefieldinit'.
		static BarGrouping()
		{
			byte[] array = new byte[1];
			BarGrouping.attributeNamespaceIds = array;
		}

		// Token: 0x04007C82 RID: 31874
		private const string tagName = "grouping";

		// Token: 0x04007C83 RID: 31875
		private const byte tagNsId = 11;

		// Token: 0x04007C84 RID: 31876
		internal const int ElementTypeIdConst = 10366;

		// Token: 0x04007C85 RID: 31877
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007C86 RID: 31878
		private static byte[] attributeNamespaceIds;
	}
}
