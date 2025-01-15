using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029F5 RID: 10741
	[GeneratedCode("DomGen", "2.0")]
	internal class KioskSlideMode : OpenXmlLeafElement
	{
		// Token: 0x17006E82 RID: 28290
		// (get) Token: 0x060155F8 RID: 87544 RVA: 0x0031E457 File Offset: 0x0031C657
		public override string LocalName
		{
			get
			{
				return "kiosk";
			}
		}

		// Token: 0x17006E83 RID: 28291
		// (get) Token: 0x060155F9 RID: 87545 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E84 RID: 28292
		// (get) Token: 0x060155FA RID: 87546 RVA: 0x0031E45E File Offset: 0x0031C65E
		internal override int ElementTypeId
		{
			get
			{
				return 12168;
			}
		}

		// Token: 0x060155FB RID: 87547 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006E85 RID: 28293
		// (get) Token: 0x060155FC RID: 87548 RVA: 0x0031E465 File Offset: 0x0031C665
		internal override string[] AttributeTagNames
		{
			get
			{
				return KioskSlideMode.attributeTagNames;
			}
		}

		// Token: 0x17006E86 RID: 28294
		// (get) Token: 0x060155FD RID: 87549 RVA: 0x0031E46C File Offset: 0x0031C66C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return KioskSlideMode.attributeNamespaceIds;
			}
		}

		// Token: 0x17006E87 RID: 28295
		// (get) Token: 0x060155FE RID: 87550 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060155FF RID: 87551 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "restart")]
		public UInt32Value Restart
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

		// Token: 0x06015601 RID: 87553 RVA: 0x0031E473 File Offset: 0x0031C673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "restart" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015602 RID: 87554 RVA: 0x0031E493 File Offset: 0x0031C693
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<KioskSlideMode>(deep);
		}

		// Token: 0x06015603 RID: 87555 RVA: 0x0031E49C File Offset: 0x0031C69C
		// Note: this type is marked as 'beforefieldinit'.
		static KioskSlideMode()
		{
			byte[] array = new byte[1];
			KioskSlideMode.attributeNamespaceIds = array;
		}

		// Token: 0x0400933E RID: 37694
		private const string tagName = "kiosk";

		// Token: 0x0400933F RID: 37695
		private const byte tagNsId = 24;

		// Token: 0x04009340 RID: 37696
		internal const int ElementTypeIdConst = 12168;

		// Token: 0x04009341 RID: 37697
		private static string[] attributeTagNames = new string[] { "restart" };

		// Token: 0x04009342 RID: 37698
		private static byte[] attributeNamespaceIds;
	}
}
