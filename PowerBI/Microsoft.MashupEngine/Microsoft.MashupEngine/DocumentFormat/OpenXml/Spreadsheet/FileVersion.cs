using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C50 RID: 11344
	[GeneratedCode("DomGen", "2.0")]
	internal class FileVersion : OpenXmlLeafElement
	{
		// Token: 0x170081F7 RID: 33271
		// (get) Token: 0x06018112 RID: 98578 RVA: 0x0033E103 File Offset: 0x0033C303
		public override string LocalName
		{
			get
			{
				return "fileVersion";
			}
		}

		// Token: 0x170081F8 RID: 33272
		// (get) Token: 0x06018113 RID: 98579 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081F9 RID: 33273
		// (get) Token: 0x06018114 RID: 98580 RVA: 0x0033E10A File Offset: 0x0033C30A
		internal override int ElementTypeId
		{
			get
			{
				return 11325;
			}
		}

		// Token: 0x06018115 RID: 98581 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081FA RID: 33274
		// (get) Token: 0x06018116 RID: 98582 RVA: 0x0033E111 File Offset: 0x0033C311
		internal override string[] AttributeTagNames
		{
			get
			{
				return FileVersion.attributeTagNames;
			}
		}

		// Token: 0x170081FB RID: 33275
		// (get) Token: 0x06018117 RID: 98583 RVA: 0x0033E118 File Offset: 0x0033C318
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FileVersion.attributeNamespaceIds;
			}
		}

		// Token: 0x170081FC RID: 33276
		// (get) Token: 0x06018118 RID: 98584 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018119 RID: 98585 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "appName")]
		public StringValue ApplicationName
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

		// Token: 0x170081FD RID: 33277
		// (get) Token: 0x0601811A RID: 98586 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601811B RID: 98587 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "lastEdited")]
		public StringValue LastEdited
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170081FE RID: 33278
		// (get) Token: 0x0601811C RID: 98588 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601811D RID: 98589 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "lowestEdited")]
		public StringValue LowestEdited
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170081FF RID: 33279
		// (get) Token: 0x0601811E RID: 98590 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601811F RID: 98591 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "rupBuild")]
		public StringValue BuildVersion
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008200 RID: 33280
		// (get) Token: 0x06018120 RID: 98592 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06018121 RID: 98593 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "codeName")]
		public StringValue CodeName
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06018123 RID: 98595 RVA: 0x0033E120 File Offset: 0x0033C320
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "appName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lastEdited" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lowestEdited" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "rupBuild" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "codeName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018124 RID: 98596 RVA: 0x0033E1A3 File Offset: 0x0033C3A3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FileVersion>(deep);
		}

		// Token: 0x06018125 RID: 98597 RVA: 0x0033E1AC File Offset: 0x0033C3AC
		// Note: this type is marked as 'beforefieldinit'.
		static FileVersion()
		{
			byte[] array = new byte[5];
			FileVersion.attributeNamespaceIds = array;
		}

		// Token: 0x04009EC1 RID: 40641
		private const string tagName = "fileVersion";

		// Token: 0x04009EC2 RID: 40642
		private const byte tagNsId = 22;

		// Token: 0x04009EC3 RID: 40643
		internal const int ElementTypeIdConst = 11325;

		// Token: 0x04009EC4 RID: 40644
		private static string[] attributeTagNames = new string[] { "appName", "lastEdited", "lowestEdited", "rupBuild", "codeName" };

		// Token: 0x04009EC5 RID: 40645
		private static byte[] attributeNamespaceIds;
	}
}
