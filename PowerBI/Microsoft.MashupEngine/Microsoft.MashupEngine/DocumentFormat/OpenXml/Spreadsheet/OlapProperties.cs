using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CD4 RID: 11476
	[GeneratedCode("DomGen", "2.0")]
	internal class OlapProperties : OpenXmlLeafElement
	{
		// Token: 0x17008578 RID: 34168
		// (get) Token: 0x06018997 RID: 100759 RVA: 0x00342EBB File Offset: 0x003410BB
		public override string LocalName
		{
			get
			{
				return "olapPr";
			}
		}

		// Token: 0x17008579 RID: 34169
		// (get) Token: 0x06018998 RID: 100760 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700857A RID: 34170
		// (get) Token: 0x06018999 RID: 100761 RVA: 0x00342EC2 File Offset: 0x003410C2
		internal override int ElementTypeId
		{
			get
			{
				return 11457;
			}
		}

		// Token: 0x0601899A RID: 100762 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700857B RID: 34171
		// (get) Token: 0x0601899B RID: 100763 RVA: 0x00342EC9 File Offset: 0x003410C9
		internal override string[] AttributeTagNames
		{
			get
			{
				return OlapProperties.attributeTagNames;
			}
		}

		// Token: 0x1700857C RID: 34172
		// (get) Token: 0x0601899C RID: 100764 RVA: 0x00342ED0 File Offset: 0x003410D0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OlapProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700857D RID: 34173
		// (get) Token: 0x0601899D RID: 100765 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601899E RID: 100766 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "local")]
		public BooleanValue Local
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

		// Token: 0x1700857E RID: 34174
		// (get) Token: 0x0601899F RID: 100767 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060189A0 RID: 100768 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "localConnection")]
		public StringValue LocalConnection
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

		// Token: 0x1700857F RID: 34175
		// (get) Token: 0x060189A1 RID: 100769 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060189A2 RID: 100770 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "localRefresh")]
		public BooleanValue LocalRefresh
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

		// Token: 0x17008580 RID: 34176
		// (get) Token: 0x060189A3 RID: 100771 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060189A4 RID: 100772 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sendLocale")]
		public BooleanValue SendLocale
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

		// Token: 0x17008581 RID: 34177
		// (get) Token: 0x060189A5 RID: 100773 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x060189A6 RID: 100774 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "rowDrillCount")]
		public UInt32Value RowDrillCount
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008582 RID: 34178
		// (get) Token: 0x060189A7 RID: 100775 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060189A8 RID: 100776 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "serverFill")]
		public BooleanValue ServerFill
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008583 RID: 34179
		// (get) Token: 0x060189A9 RID: 100777 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060189AA RID: 100778 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "serverNumberFormat")]
		public BooleanValue ServerNumberFormat
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008584 RID: 34180
		// (get) Token: 0x060189AB RID: 100779 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060189AC RID: 100780 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "serverFont")]
		public BooleanValue ServerFont
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17008585 RID: 34181
		// (get) Token: 0x060189AD RID: 100781 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x060189AE RID: 100782 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "serverFontColor")]
		public BooleanValue ServerFontColor
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x060189B0 RID: 100784 RVA: 0x00342ED8 File Offset: 0x003410D8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "local" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "localConnection" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "localRefresh" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sendLocale" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "rowDrillCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "serverFill" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "serverNumberFormat" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "serverFont" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "serverFontColor" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060189B1 RID: 100785 RVA: 0x00342FB3 File Offset: 0x003411B3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OlapProperties>(deep);
		}

		// Token: 0x060189B2 RID: 100786 RVA: 0x00342FBC File Offset: 0x003411BC
		// Note: this type is marked as 'beforefieldinit'.
		static OlapProperties()
		{
			byte[] array = new byte[9];
			OlapProperties.attributeNamespaceIds = array;
		}

		// Token: 0x0400A101 RID: 41217
		private const string tagName = "olapPr";

		// Token: 0x0400A102 RID: 41218
		private const byte tagNsId = 22;

		// Token: 0x0400A103 RID: 41219
		internal const int ElementTypeIdConst = 11457;

		// Token: 0x0400A104 RID: 41220
		private static string[] attributeTagNames = new string[] { "local", "localConnection", "localRefresh", "sendLocale", "rowDrillCount", "serverFill", "serverNumberFormat", "serverFont", "serverFontColor" };

		// Token: 0x0400A105 RID: 41221
		private static byte[] attributeNamespaceIds;
	}
}
