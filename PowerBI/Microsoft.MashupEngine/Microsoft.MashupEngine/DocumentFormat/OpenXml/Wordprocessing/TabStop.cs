using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F2D RID: 12077
	[GeneratedCode("DomGen", "2.0")]
	internal class TabStop : OpenXmlLeafElement
	{
		// Token: 0x17008F4F RID: 36687
		// (get) Token: 0x06019E64 RID: 106084 RVA: 0x002D001B File Offset: 0x002CE21B
		public override string LocalName
		{
			get
			{
				return "tab";
			}
		}

		// Token: 0x17008F50 RID: 36688
		// (get) Token: 0x06019E65 RID: 106085 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F51 RID: 36689
		// (get) Token: 0x06019E66 RID: 106086 RVA: 0x00359128 File Offset: 0x00357328
		internal override int ElementTypeId
		{
			get
			{
				return 11721;
			}
		}

		// Token: 0x06019E67 RID: 106087 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008F52 RID: 36690
		// (get) Token: 0x06019E68 RID: 106088 RVA: 0x0035912F File Offset: 0x0035732F
		internal override string[] AttributeTagNames
		{
			get
			{
				return TabStop.attributeTagNames;
			}
		}

		// Token: 0x17008F53 RID: 36691
		// (get) Token: 0x06019E69 RID: 106089 RVA: 0x00359136 File Offset: 0x00357336
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TabStop.attributeNamespaceIds;
			}
		}

		// Token: 0x17008F54 RID: 36692
		// (get) Token: 0x06019E6A RID: 106090 RVA: 0x0035913D File Offset: 0x0035733D
		// (set) Token: 0x06019E6B RID: 106091 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<TabStopValues> Val
		{
			get
			{
				return (EnumValue<TabStopValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008F55 RID: 36693
		// (get) Token: 0x06019E6C RID: 106092 RVA: 0x0035914C File Offset: 0x0035734C
		// (set) Token: 0x06019E6D RID: 106093 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "leader")]
		public EnumValue<TabStopLeaderCharValues> Leader
		{
			get
			{
				return (EnumValue<TabStopLeaderCharValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008F56 RID: 36694
		// (get) Token: 0x06019E6E RID: 106094 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06019E6F RID: 106095 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "pos")]
		public Int32Value Position
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06019E71 RID: 106097 RVA: 0x0035915C File Offset: 0x0035735C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<TabStopValues>();
			}
			if (23 == namespaceId && "leader" == name)
			{
				return new EnumValue<TabStopLeaderCharValues>();
			}
			if (23 == namespaceId && "pos" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019E72 RID: 106098 RVA: 0x003591B9 File Offset: 0x003573B9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TabStop>(deep);
		}

		// Token: 0x0400AACB RID: 43723
		private const string tagName = "tab";

		// Token: 0x0400AACC RID: 43724
		private const byte tagNsId = 23;

		// Token: 0x0400AACD RID: 43725
		internal const int ElementTypeIdConst = 11721;

		// Token: 0x0400AACE RID: 43726
		private static string[] attributeTagNames = new string[] { "val", "leader", "pos" };

		// Token: 0x0400AACF RID: 43727
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
