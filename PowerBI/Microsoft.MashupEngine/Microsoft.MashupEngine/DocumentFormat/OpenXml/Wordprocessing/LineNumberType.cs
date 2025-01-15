using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E47 RID: 11847
	[GeneratedCode("DomGen", "2.0")]
	internal class LineNumberType : OpenXmlLeafElement
	{
		// Token: 0x170089FB RID: 35323
		// (get) Token: 0x060192C6 RID: 103110 RVA: 0x00347253 File Offset: 0x00345453
		public override string LocalName
		{
			get
			{
				return "lnNumType";
			}
		}

		// Token: 0x170089FC RID: 35324
		// (get) Token: 0x060192C7 RID: 103111 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170089FD RID: 35325
		// (get) Token: 0x060192C8 RID: 103112 RVA: 0x0034725A File Offset: 0x0034545A
		internal override int ElementTypeId
		{
			get
			{
				return 11533;
			}
		}

		// Token: 0x060192C9 RID: 103113 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170089FE RID: 35326
		// (get) Token: 0x060192CA RID: 103114 RVA: 0x00347261 File Offset: 0x00345461
		internal override string[] AttributeTagNames
		{
			get
			{
				return LineNumberType.attributeTagNames;
			}
		}

		// Token: 0x170089FF RID: 35327
		// (get) Token: 0x060192CB RID: 103115 RVA: 0x00347268 File Offset: 0x00345468
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LineNumberType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008A00 RID: 35328
		// (get) Token: 0x060192CC RID: 103116 RVA: 0x0034726F File Offset: 0x0034546F
		// (set) Token: 0x060192CD RID: 103117 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "countBy")]
		public Int16Value CountBy
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

		// Token: 0x17008A01 RID: 35329
		// (get) Token: 0x060192CE RID: 103118 RVA: 0x0034727E File Offset: 0x0034547E
		// (set) Token: 0x060192CF RID: 103119 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "start")]
		public Int16Value Start
		{
			get
			{
				return (Int16Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008A02 RID: 35330
		// (get) Token: 0x060192D0 RID: 103120 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060192D1 RID: 103121 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "distance")]
		public StringValue Distance
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

		// Token: 0x17008A03 RID: 35331
		// (get) Token: 0x060192D2 RID: 103122 RVA: 0x0034728D File Offset: 0x0034548D
		// (set) Token: 0x060192D3 RID: 103123 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "restart")]
		public EnumValue<LineNumberRestartValues> Restart
		{
			get
			{
				return (EnumValue<LineNumberRestartValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060192D5 RID: 103125 RVA: 0x0034729C File Offset: 0x0034549C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "countBy" == name)
			{
				return new Int16Value();
			}
			if (23 == namespaceId && "start" == name)
			{
				return new Int16Value();
			}
			if (23 == namespaceId && "distance" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "restart" == name)
			{
				return new EnumValue<LineNumberRestartValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060192D6 RID: 103126 RVA: 0x00347311 File Offset: 0x00345511
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineNumberType>(deep);
		}

		// Token: 0x0400A766 RID: 42854
		private const string tagName = "lnNumType";

		// Token: 0x0400A767 RID: 42855
		private const byte tagNsId = 23;

		// Token: 0x0400A768 RID: 42856
		internal const int ElementTypeIdConst = 11533;

		// Token: 0x0400A769 RID: 42857
		private static string[] attributeTagNames = new string[] { "countBy", "start", "distance", "restart" };

		// Token: 0x0400A76A RID: 42858
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
