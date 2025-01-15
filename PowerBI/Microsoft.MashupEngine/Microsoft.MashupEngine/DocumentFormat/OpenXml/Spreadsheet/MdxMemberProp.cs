using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C01 RID: 11265
	[GeneratedCode("DomGen", "2.0")]
	internal class MdxMemberProp : OpenXmlLeafElement
	{
		// Token: 0x17007F45 RID: 32581
		// (get) Token: 0x06017B0E RID: 97038 RVA: 0x002EA9F7 File Offset: 0x002E8BF7
		public override string LocalName
		{
			get
			{
				return "p";
			}
		}

		// Token: 0x17007F46 RID: 32582
		// (get) Token: 0x06017B0F RID: 97039 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F47 RID: 32583
		// (get) Token: 0x06017B10 RID: 97040 RVA: 0x00339FA7 File Offset: 0x003381A7
		internal override int ElementTypeId
		{
			get
			{
				return 11244;
			}
		}

		// Token: 0x06017B11 RID: 97041 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F48 RID: 32584
		// (get) Token: 0x06017B12 RID: 97042 RVA: 0x00339FAE File Offset: 0x003381AE
		internal override string[] AttributeTagNames
		{
			get
			{
				return MdxMemberProp.attributeTagNames;
			}
		}

		// Token: 0x17007F49 RID: 32585
		// (get) Token: 0x06017B13 RID: 97043 RVA: 0x00339FB5 File Offset: 0x003381B5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MdxMemberProp.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F4A RID: 32586
		// (get) Token: 0x06017B14 RID: 97044 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017B15 RID: 97045 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "n")]
		public UInt32Value NameIndex
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

		// Token: 0x17007F4B RID: 32587
		// (get) Token: 0x06017B16 RID: 97046 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017B17 RID: 97047 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "np")]
		public UInt32Value PropertyNameIndex
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

		// Token: 0x06017B19 RID: 97049 RVA: 0x00339FBC File Offset: 0x003381BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "n" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "np" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017B1A RID: 97050 RVA: 0x00339FF2 File Offset: 0x003381F2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MdxMemberProp>(deep);
		}

		// Token: 0x06017B1B RID: 97051 RVA: 0x00339FFC File Offset: 0x003381FC
		// Note: this type is marked as 'beforefieldinit'.
		static MdxMemberProp()
		{
			byte[] array = new byte[2];
			MdxMemberProp.attributeNamespaceIds = array;
		}

		// Token: 0x04009D2C RID: 40236
		private const string tagName = "p";

		// Token: 0x04009D2D RID: 40237
		private const byte tagNsId = 22;

		// Token: 0x04009D2E RID: 40238
		internal const int ElementTypeIdConst = 11244;

		// Token: 0x04009D2F RID: 40239
		private static string[] attributeTagNames = new string[] { "n", "np" };

		// Token: 0x04009D30 RID: 40240
		private static byte[] attributeNamespaceIds;
	}
}
