using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EE6 RID: 12006
	[GeneratedCode("DomGen", "2.0")]
	internal class HorizontalMerge : OpenXmlLeafElement
	{
		// Token: 0x17008D53 RID: 36179
		// (get) Token: 0x060199F3 RID: 104947 RVA: 0x003534C0 File Offset: 0x003516C0
		public override string LocalName
		{
			get
			{
				return "hMerge";
			}
		}

		// Token: 0x17008D54 RID: 36180
		// (get) Token: 0x060199F4 RID: 104948 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D55 RID: 36181
		// (get) Token: 0x060199F5 RID: 104949 RVA: 0x003534C7 File Offset: 0x003516C7
		internal override int ElementTypeId
		{
			get
			{
				return 11652;
			}
		}

		// Token: 0x060199F6 RID: 104950 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008D56 RID: 36182
		// (get) Token: 0x060199F7 RID: 104951 RVA: 0x003534CE File Offset: 0x003516CE
		internal override string[] AttributeTagNames
		{
			get
			{
				return HorizontalMerge.attributeTagNames;
			}
		}

		// Token: 0x17008D57 RID: 36183
		// (get) Token: 0x060199F8 RID: 104952 RVA: 0x003534D5 File Offset: 0x003516D5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HorizontalMerge.attributeNamespaceIds;
			}
		}

		// Token: 0x17008D58 RID: 36184
		// (get) Token: 0x060199F9 RID: 104953 RVA: 0x003534DC File Offset: 0x003516DC
		// (set) Token: 0x060199FA RID: 104954 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<MergedCellValues> Val
		{
			get
			{
				return (EnumValue<MergedCellValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060199FC RID: 104956 RVA: 0x003534EB File Offset: 0x003516EB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<MergedCellValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060199FD RID: 104957 RVA: 0x0035350D File Offset: 0x0035170D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HorizontalMerge>(deep);
		}

		// Token: 0x0400A9BA RID: 43450
		private const string tagName = "hMerge";

		// Token: 0x0400A9BB RID: 43451
		private const byte tagNsId = 23;

		// Token: 0x0400A9BC RID: 43452
		internal const int ElementTypeIdConst = 11652;

		// Token: 0x0400A9BD RID: 43453
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A9BE RID: 43454
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
