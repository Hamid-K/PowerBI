using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C66 RID: 11366
	[GeneratedCode("DomGen", "2.0")]
	internal class OleItem : OpenXmlLeafElement
	{
		// Token: 0x170082A3 RID: 33443
		// (get) Token: 0x0601829A RID: 98970 RVA: 0x002E6833 File Offset: 0x002E4A33
		public override string LocalName
		{
			get
			{
				return "oleItem";
			}
		}

		// Token: 0x170082A4 RID: 33444
		// (get) Token: 0x0601829B RID: 98971 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082A5 RID: 33445
		// (get) Token: 0x0601829C RID: 98972 RVA: 0x0033F15E File Offset: 0x0033D35E
		internal override int ElementTypeId
		{
			get
			{
				return 11347;
			}
		}

		// Token: 0x0601829D RID: 98973 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170082A6 RID: 33446
		// (get) Token: 0x0601829E RID: 98974 RVA: 0x0033F165 File Offset: 0x0033D365
		internal override string[] AttributeTagNames
		{
			get
			{
				return OleItem.attributeTagNames;
			}
		}

		// Token: 0x170082A7 RID: 33447
		// (get) Token: 0x0601829F RID: 98975 RVA: 0x0033F16C File Offset: 0x0033D36C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OleItem.attributeNamespaceIds;
			}
		}

		// Token: 0x170082A8 RID: 33448
		// (get) Token: 0x060182A0 RID: 98976 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060182A1 RID: 98977 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170082A9 RID: 33449
		// (get) Token: 0x060182A2 RID: 98978 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060182A3 RID: 98979 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "icon")]
		public BooleanValue Icon
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170082AA RID: 33450
		// (get) Token: 0x060182A4 RID: 98980 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060182A5 RID: 98981 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "advise")]
		public BooleanValue Advise
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170082AB RID: 33451
		// (get) Token: 0x060182A6 RID: 98982 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060182A7 RID: 98983 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "preferPic")]
		public BooleanValue PreferPicture
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060182A9 RID: 98985 RVA: 0x0033F174 File Offset: 0x0033D374
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "icon" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "advise" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "preferPic" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060182AA RID: 98986 RVA: 0x0033F1E1 File Offset: 0x0033D3E1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleItem>(deep);
		}

		// Token: 0x060182AB RID: 98987 RVA: 0x0033F1EC File Offset: 0x0033D3EC
		// Note: this type is marked as 'beforefieldinit'.
		static OleItem()
		{
			byte[] array = new byte[4];
			OleItem.attributeNamespaceIds = array;
		}

		// Token: 0x04009F1D RID: 40733
		private const string tagName = "oleItem";

		// Token: 0x04009F1E RID: 40734
		private const byte tagNsId = 22;

		// Token: 0x04009F1F RID: 40735
		internal const int ElementTypeIdConst = 11347;

		// Token: 0x04009F20 RID: 40736
		private static string[] attributeTagNames = new string[] { "name", "icon", "advise", "preferPic" };

		// Token: 0x04009F21 RID: 40737
		private static byte[] attributeNamespaceIds;
	}
}
