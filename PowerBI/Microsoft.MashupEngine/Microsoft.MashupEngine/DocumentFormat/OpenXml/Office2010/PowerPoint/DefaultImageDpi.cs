using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023B1 RID: 9137
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultImageDpi : OpenXmlLeafElement
	{
		// Token: 0x17004C61 RID: 19553
		// (get) Token: 0x060108CC RID: 67788 RVA: 0x002E4A70 File Offset: 0x002E2C70
		public override string LocalName
		{
			get
			{
				return "defaultImageDpi";
			}
		}

		// Token: 0x17004C62 RID: 19554
		// (get) Token: 0x060108CD RID: 67789 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C63 RID: 19555
		// (get) Token: 0x060108CE RID: 67790 RVA: 0x002E4A77 File Offset: 0x002E2C77
		internal override int ElementTypeId
		{
			get
			{
				return 12792;
			}
		}

		// Token: 0x060108CF RID: 67791 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C64 RID: 19556
		// (get) Token: 0x060108D0 RID: 67792 RVA: 0x002E4A7E File Offset: 0x002E2C7E
		internal override string[] AttributeTagNames
		{
			get
			{
				return DefaultImageDpi.attributeTagNames;
			}
		}

		// Token: 0x17004C65 RID: 19557
		// (get) Token: 0x060108D1 RID: 67793 RVA: 0x002E4A85 File Offset: 0x002E2C85
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DefaultImageDpi.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C66 RID: 19558
		// (get) Token: 0x060108D2 RID: 67794 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060108D3 RID: 67795 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public UInt32Value Val
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060108D5 RID: 67797 RVA: 0x002E4A8C File Offset: 0x002E2C8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060108D6 RID: 67798 RVA: 0x002E4AAC File Offset: 0x002E2CAC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultImageDpi>(deep);
		}

		// Token: 0x060108D7 RID: 67799 RVA: 0x002E4AB8 File Offset: 0x002E2CB8
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultImageDpi()
		{
			byte[] array = new byte[1];
			DefaultImageDpi.attributeNamespaceIds = array;
		}

		// Token: 0x04007531 RID: 30001
		private const string tagName = "defaultImageDpi";

		// Token: 0x04007532 RID: 30002
		private const byte tagNsId = 49;

		// Token: 0x04007533 RID: 30003
		internal const int ElementTypeIdConst = 12792;

		// Token: 0x04007534 RID: 30004
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007535 RID: 30005
		private static byte[] attributeNamespaceIds;
	}
}
