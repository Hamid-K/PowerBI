using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023EB RID: 9195
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class IconFilter : OpenXmlLeafElement
	{
		// Token: 0x17004DCC RID: 19916
		// (get) Token: 0x06010BF2 RID: 68594 RVA: 0x002E6A70 File Offset: 0x002E4C70
		public override string LocalName
		{
			get
			{
				return "iconFilter";
			}
		}

		// Token: 0x17004DCD RID: 19917
		// (get) Token: 0x06010BF3 RID: 68595 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DCE RID: 19918
		// (get) Token: 0x06010BF4 RID: 68596 RVA: 0x002E6A77 File Offset: 0x002E4C77
		internal override int ElementTypeId
		{
			get
			{
				return 12921;
			}
		}

		// Token: 0x06010BF5 RID: 68597 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DCF RID: 19919
		// (get) Token: 0x06010BF6 RID: 68598 RVA: 0x002E6A7E File Offset: 0x002E4C7E
		internal override string[] AttributeTagNames
		{
			get
			{
				return IconFilter.attributeTagNames;
			}
		}

		// Token: 0x17004DD0 RID: 19920
		// (get) Token: 0x06010BF7 RID: 68599 RVA: 0x002E6A85 File Offset: 0x002E4C85
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return IconFilter.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DD1 RID: 19921
		// (get) Token: 0x06010BF8 RID: 68600 RVA: 0x002E6A8C File Offset: 0x002E4C8C
		// (set) Token: 0x06010BF9 RID: 68601 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "iconSet")]
		public EnumValue<IconSetTypeValues> IconSet
		{
			get
			{
				return (EnumValue<IconSetTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004DD2 RID: 19922
		// (get) Token: 0x06010BFA RID: 68602 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06010BFB RID: 68603 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "iconId")]
		public UInt32Value IconId
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010BFD RID: 68605 RVA: 0x002E6A9B File Offset: 0x002E4C9B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "iconSet" == name)
			{
				return new EnumValue<IconSetTypeValues>();
			}
			if (namespaceId == 0 && "iconId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010BFE RID: 68606 RVA: 0x002E6AD1 File Offset: 0x002E4CD1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IconFilter>(deep);
		}

		// Token: 0x06010BFF RID: 68607 RVA: 0x002E6ADC File Offset: 0x002E4CDC
		// Note: this type is marked as 'beforefieldinit'.
		static IconFilter()
		{
			byte[] array = new byte[2];
			IconFilter.attributeNamespaceIds = array;
		}

		// Token: 0x04007630 RID: 30256
		private const string tagName = "iconFilter";

		// Token: 0x04007631 RID: 30257
		private const byte tagNsId = 53;

		// Token: 0x04007632 RID: 30258
		internal const int ElementTypeIdConst = 12921;

		// Token: 0x04007633 RID: 30259
		private static string[] attributeTagNames = new string[] { "iconSet", "iconId" };

		// Token: 0x04007634 RID: 30260
		private static byte[] attributeNamespaceIds;
	}
}
