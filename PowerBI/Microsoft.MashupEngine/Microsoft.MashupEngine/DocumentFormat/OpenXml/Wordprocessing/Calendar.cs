using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F50 RID: 12112
	[GeneratedCode("DomGen", "2.0")]
	internal class Calendar : OpenXmlLeafElement
	{
		// Token: 0x17009019 RID: 36889
		// (get) Token: 0x0601A01F RID: 106527 RVA: 0x0035B194 File Offset: 0x00359394
		public override string LocalName
		{
			get
			{
				return "calendar";
			}
		}

		// Token: 0x1700901A RID: 36890
		// (get) Token: 0x0601A020 RID: 106528 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700901B RID: 36891
		// (get) Token: 0x0601A021 RID: 106529 RVA: 0x0035B19B File Offset: 0x0035939B
		internal override int ElementTypeId
		{
			get
			{
				return 11763;
			}
		}

		// Token: 0x0601A022 RID: 106530 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700901C RID: 36892
		// (get) Token: 0x0601A023 RID: 106531 RVA: 0x0035B1A2 File Offset: 0x003593A2
		internal override string[] AttributeTagNames
		{
			get
			{
				return Calendar.attributeTagNames;
			}
		}

		// Token: 0x1700901D RID: 36893
		// (get) Token: 0x0601A024 RID: 106532 RVA: 0x0035B1A9 File Offset: 0x003593A9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Calendar.attributeNamespaceIds;
			}
		}

		// Token: 0x1700901E RID: 36894
		// (get) Token: 0x0601A025 RID: 106533 RVA: 0x0035B1B0 File Offset: 0x003593B0
		// (set) Token: 0x0601A026 RID: 106534 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<CalendarValues> Val
		{
			get
			{
				return (EnumValue<CalendarValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A028 RID: 106536 RVA: 0x0035B1BF File Offset: 0x003593BF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<CalendarValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A029 RID: 106537 RVA: 0x0035B1E1 File Offset: 0x003593E1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Calendar>(deep);
		}

		// Token: 0x0400AB5D RID: 43869
		private const string tagName = "calendar";

		// Token: 0x0400AB5E RID: 43870
		private const byte tagNsId = 23;

		// Token: 0x0400AB5F RID: 43871
		internal const int ElementTypeIdConst = 11763;

		// Token: 0x0400AB60 RID: 43872
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AB61 RID: 43873
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
