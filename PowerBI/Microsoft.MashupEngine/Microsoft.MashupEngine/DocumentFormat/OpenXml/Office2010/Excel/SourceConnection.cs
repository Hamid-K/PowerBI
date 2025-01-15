using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023EF RID: 9199
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SourceConnection : OpenXmlLeafElement
	{
		// Token: 0x17004DEB RID: 19947
		// (get) Token: 0x06010C34 RID: 68660 RVA: 0x002E6D87 File Offset: 0x002E4F87
		public override string LocalName
		{
			get
			{
				return "sourceConnection";
			}
		}

		// Token: 0x17004DEC RID: 19948
		// (get) Token: 0x06010C35 RID: 68661 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DED RID: 19949
		// (get) Token: 0x06010C36 RID: 68662 RVA: 0x002E6D8E File Offset: 0x002E4F8E
		internal override int ElementTypeId
		{
			get
			{
				return 12925;
			}
		}

		// Token: 0x06010C37 RID: 68663 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DEE RID: 19950
		// (get) Token: 0x06010C38 RID: 68664 RVA: 0x002E6D95 File Offset: 0x002E4F95
		internal override string[] AttributeTagNames
		{
			get
			{
				return SourceConnection.attributeTagNames;
			}
		}

		// Token: 0x17004DEF RID: 19951
		// (get) Token: 0x06010C39 RID: 68665 RVA: 0x002E6D9C File Offset: 0x002E4F9C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SourceConnection.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DF0 RID: 19952
		// (get) Token: 0x06010C3A RID: 68666 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010C3B RID: 68667 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010C3D RID: 68669 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010C3E RID: 68670 RVA: 0x002E6DA3 File Offset: 0x002E4FA3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SourceConnection>(deep);
		}

		// Token: 0x06010C3F RID: 68671 RVA: 0x002E6DAC File Offset: 0x002E4FAC
		// Note: this type is marked as 'beforefieldinit'.
		static SourceConnection()
		{
			byte[] array = new byte[1];
			SourceConnection.attributeNamespaceIds = array;
		}

		// Token: 0x04007644 RID: 30276
		private const string tagName = "sourceConnection";

		// Token: 0x04007645 RID: 30277
		private const byte tagNsId = 53;

		// Token: 0x04007646 RID: 30278
		internal const int ElementTypeIdConst = 12925;

		// Token: 0x04007647 RID: 30279
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x04007648 RID: 30280
		private static byte[] attributeNamespaceIds;
	}
}
