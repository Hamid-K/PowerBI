using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F49 RID: 12105
	[GeneratedCode("DomGen", "2.0")]
	internal class PhoneticGuideRaise : OpenXmlLeafElement
	{
		// Token: 0x17008FF5 RID: 36853
		// (get) Token: 0x06019FCA RID: 106442 RVA: 0x0035A900 File Offset: 0x00358B00
		public override string LocalName
		{
			get
			{
				return "hpsRaise";
			}
		}

		// Token: 0x17008FF6 RID: 36854
		// (get) Token: 0x06019FCB RID: 106443 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FF7 RID: 36855
		// (get) Token: 0x06019FCC RID: 106444 RVA: 0x0035A907 File Offset: 0x00358B07
		internal override int ElementTypeId
		{
			get
			{
				return 11754;
			}
		}

		// Token: 0x06019FCD RID: 106445 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008FF8 RID: 36856
		// (get) Token: 0x06019FCE RID: 106446 RVA: 0x0035A90E File Offset: 0x00358B0E
		internal override string[] AttributeTagNames
		{
			get
			{
				return PhoneticGuideRaise.attributeTagNames;
			}
		}

		// Token: 0x17008FF9 RID: 36857
		// (get) Token: 0x06019FCF RID: 106447 RVA: 0x0035A915 File Offset: 0x00358B15
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PhoneticGuideRaise.attributeNamespaceIds;
			}
		}

		// Token: 0x17008FFA RID: 36858
		// (get) Token: 0x06019FD0 RID: 106448 RVA: 0x0034726F File Offset: 0x0034546F
		// (set) Token: 0x06019FD1 RID: 106449 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public Int16Value Val
		{
			get
			{
				return (Int16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019FD3 RID: 106451 RVA: 0x0035A444 File Offset: 0x00358644
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019FD4 RID: 106452 RVA: 0x0035A91C File Offset: 0x00358B1C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PhoneticGuideRaise>(deep);
		}

		// Token: 0x0400AB43 RID: 43843
		private const string tagName = "hpsRaise";

		// Token: 0x0400AB44 RID: 43844
		private const byte tagNsId = 23;

		// Token: 0x0400AB45 RID: 43845
		internal const int ElementTypeIdConst = 11754;

		// Token: 0x0400AB46 RID: 43846
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AB47 RID: 43847
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
