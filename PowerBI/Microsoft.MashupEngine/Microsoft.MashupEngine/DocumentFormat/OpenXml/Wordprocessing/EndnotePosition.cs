using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F67 RID: 12135
	[GeneratedCode("DomGen", "2.0")]
	internal class EndnotePosition : OpenXmlLeafElement
	{
		// Token: 0x170090C2 RID: 37058
		// (get) Token: 0x0601A19F RID: 106911 RVA: 0x0030BA47 File Offset: 0x00309C47
		public override string LocalName
		{
			get
			{
				return "pos";
			}
		}

		// Token: 0x170090C3 RID: 37059
		// (get) Token: 0x0601A1A0 RID: 106912 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090C4 RID: 37060
		// (get) Token: 0x0601A1A1 RID: 106913 RVA: 0x0035D7D5 File Offset: 0x0035B9D5
		internal override int ElementTypeId
		{
			get
			{
				return 11793;
			}
		}

		// Token: 0x0601A1A2 RID: 106914 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170090C5 RID: 37061
		// (get) Token: 0x0601A1A3 RID: 106915 RVA: 0x0035D7DC File Offset: 0x0035B9DC
		internal override string[] AttributeTagNames
		{
			get
			{
				return EndnotePosition.attributeTagNames;
			}
		}

		// Token: 0x170090C6 RID: 37062
		// (get) Token: 0x0601A1A4 RID: 106916 RVA: 0x0035D7E3 File Offset: 0x0035B9E3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EndnotePosition.attributeNamespaceIds;
			}
		}

		// Token: 0x170090C7 RID: 37063
		// (get) Token: 0x0601A1A5 RID: 106917 RVA: 0x0035D7EA File Offset: 0x0035B9EA
		// (set) Token: 0x0601A1A6 RID: 106918 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<EndnotePositionValues> Val
		{
			get
			{
				return (EnumValue<EndnotePositionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A1A8 RID: 106920 RVA: 0x0035D7F9 File Offset: 0x0035B9F9
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<EndnotePositionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A1A9 RID: 106921 RVA: 0x0035D81B File Offset: 0x0035BA1B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndnotePosition>(deep);
		}

		// Token: 0x0400ABC6 RID: 43974
		private const string tagName = "pos";

		// Token: 0x0400ABC7 RID: 43975
		private const byte tagNsId = 23;

		// Token: 0x0400ABC8 RID: 43976
		internal const int ElementTypeIdConst = 11793;

		// Token: 0x0400ABC9 RID: 43977
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ABCA RID: 43978
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
