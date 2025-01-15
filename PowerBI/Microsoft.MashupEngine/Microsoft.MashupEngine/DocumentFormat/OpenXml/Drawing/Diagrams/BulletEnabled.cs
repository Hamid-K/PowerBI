using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002683 RID: 9859
	[GeneratedCode("DomGen", "2.0")]
	internal class BulletEnabled : OpenXmlLeafElement
	{
		// Token: 0x17005CB3 RID: 23731
		// (get) Token: 0x06012D71 RID: 77169 RVA: 0x002FFECF File Offset: 0x002FE0CF
		public override string LocalName
		{
			get
			{
				return "bulletEnabled";
			}
		}

		// Token: 0x17005CB4 RID: 23732
		// (get) Token: 0x06012D72 RID: 77170 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CB5 RID: 23733
		// (get) Token: 0x06012D73 RID: 77171 RVA: 0x002FFED6 File Offset: 0x002FE0D6
		internal override int ElementTypeId
		{
			get
			{
				return 10674;
			}
		}

		// Token: 0x06012D74 RID: 77172 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005CB6 RID: 23734
		// (get) Token: 0x06012D75 RID: 77173 RVA: 0x002FFEDD File Offset: 0x002FE0DD
		internal override string[] AttributeTagNames
		{
			get
			{
				return BulletEnabled.attributeTagNames;
			}
		}

		// Token: 0x17005CB7 RID: 23735
		// (get) Token: 0x06012D76 RID: 77174 RVA: 0x002FFEE4 File Offset: 0x002FE0E4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BulletEnabled.attributeNamespaceIds;
			}
		}

		// Token: 0x17005CB8 RID: 23736
		// (get) Token: 0x06012D77 RID: 77175 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06012D78 RID: 77176 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public BooleanValue Val
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012D7A RID: 77178 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012D7B RID: 77179 RVA: 0x002FFEEB File Offset: 0x002FE0EB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BulletEnabled>(deep);
		}

		// Token: 0x06012D7C RID: 77180 RVA: 0x002FFEF4 File Offset: 0x002FE0F4
		// Note: this type is marked as 'beforefieldinit'.
		static BulletEnabled()
		{
			byte[] array = new byte[1];
			BulletEnabled.attributeNamespaceIds = array;
		}

		// Token: 0x040081D5 RID: 33237
		private const string tagName = "bulletEnabled";

		// Token: 0x040081D6 RID: 33238
		private const byte tagNsId = 14;

		// Token: 0x040081D7 RID: 33239
		internal const int ElementTypeIdConst = 10674;

		// Token: 0x040081D8 RID: 33240
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040081D9 RID: 33241
		private static byte[] attributeNamespaceIds;
	}
}
