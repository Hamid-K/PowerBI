using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FB5 RID: 12213
	[GeneratedCode("DomGen", "2.0")]
	internal class FontSignature : OpenXmlLeafElement
	{
		// Token: 0x1700939B RID: 37787
		// (get) Token: 0x0601A79A RID: 108442 RVA: 0x002A34DB File Offset: 0x002A16DB
		public override string LocalName
		{
			get
			{
				return "sig";
			}
		}

		// Token: 0x1700939C RID: 37788
		// (get) Token: 0x0601A79B RID: 108443 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700939D RID: 37789
		// (get) Token: 0x0601A79C RID: 108444 RVA: 0x00362D0C File Offset: 0x00360F0C
		internal override int ElementTypeId
		{
			get
			{
				return 11921;
			}
		}

		// Token: 0x0601A79D RID: 108445 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700939E RID: 37790
		// (get) Token: 0x0601A79E RID: 108446 RVA: 0x00362D13 File Offset: 0x00360F13
		internal override string[] AttributeTagNames
		{
			get
			{
				return FontSignature.attributeTagNames;
			}
		}

		// Token: 0x1700939F RID: 37791
		// (get) Token: 0x0601A79F RID: 108447 RVA: 0x00362D1A File Offset: 0x00360F1A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FontSignature.attributeNamespaceIds;
			}
		}

		// Token: 0x170093A0 RID: 37792
		// (get) Token: 0x0601A7A0 RID: 108448 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x0601A7A1 RID: 108449 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "usb0")]
		public HexBinaryValue UnicodeSignature0
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170093A1 RID: 37793
		// (get) Token: 0x0601A7A2 RID: 108450 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x0601A7A3 RID: 108451 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "usb1")]
		public HexBinaryValue UnicodeSignature1
		{
			get
			{
				return (HexBinaryValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170093A2 RID: 37794
		// (get) Token: 0x0601A7A4 RID: 108452 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x0601A7A5 RID: 108453 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "usb2")]
		public HexBinaryValue UnicodeSignature2
		{
			get
			{
				return (HexBinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170093A3 RID: 37795
		// (get) Token: 0x0601A7A6 RID: 108454 RVA: 0x002EB434 File Offset: 0x002E9634
		// (set) Token: 0x0601A7A7 RID: 108455 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "usb3")]
		public HexBinaryValue UnicodeSignature3
		{
			get
			{
				return (HexBinaryValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170093A4 RID: 37796
		// (get) Token: 0x0601A7A8 RID: 108456 RVA: 0x002EB784 File Offset: 0x002E9984
		// (set) Token: 0x0601A7A9 RID: 108457 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "csb0")]
		public HexBinaryValue CodePageSignature0
		{
			get
			{
				return (HexBinaryValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170093A5 RID: 37797
		// (get) Token: 0x0601A7AA RID: 108458 RVA: 0x003137E6 File Offset: 0x003119E6
		// (set) Token: 0x0601A7AB RID: 108459 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "csb1")]
		public HexBinaryValue CodePageSignature1
		{
			get
			{
				return (HexBinaryValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x0601A7AD RID: 108461 RVA: 0x00362D24 File Offset: 0x00360F24
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "usb0" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "usb1" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "usb2" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "usb3" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "csb0" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "csb1" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A7AE RID: 108462 RVA: 0x00362DC9 File Offset: 0x00360FC9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontSignature>(deep);
		}

		// Token: 0x0400AD1E RID: 44318
		private const string tagName = "sig";

		// Token: 0x0400AD1F RID: 44319
		private const byte tagNsId = 23;

		// Token: 0x0400AD20 RID: 44320
		internal const int ElementTypeIdConst = 11921;

		// Token: 0x0400AD21 RID: 44321
		private static string[] attributeTagNames = new string[] { "usb0", "usb1", "usb2", "usb3", "csb0", "csb1" };

		// Token: 0x0400AD22 RID: 44322
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23 };
	}
}
