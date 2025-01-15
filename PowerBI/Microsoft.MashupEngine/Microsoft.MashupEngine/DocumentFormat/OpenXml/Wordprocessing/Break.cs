using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E56 RID: 11862
	[GeneratedCode("DomGen", "2.0")]
	internal class Break : OpenXmlLeafElement
	{
		// Token: 0x17008A42 RID: 35394
		// (get) Token: 0x06019359 RID: 103257 RVA: 0x00306C25 File Offset: 0x00304E25
		public override string LocalName
		{
			get
			{
				return "br";
			}
		}

		// Token: 0x17008A43 RID: 35395
		// (get) Token: 0x0601935A RID: 103258 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A44 RID: 35396
		// (get) Token: 0x0601935B RID: 103259 RVA: 0x0034781D File Offset: 0x00345A1D
		internal override int ElementTypeId
		{
			get
			{
				return 11543;
			}
		}

		// Token: 0x0601935C RID: 103260 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008A45 RID: 35397
		// (get) Token: 0x0601935D RID: 103261 RVA: 0x00347824 File Offset: 0x00345A24
		internal override string[] AttributeTagNames
		{
			get
			{
				return Break.attributeTagNames;
			}
		}

		// Token: 0x17008A46 RID: 35398
		// (get) Token: 0x0601935E RID: 103262 RVA: 0x0034782B File Offset: 0x00345A2B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Break.attributeNamespaceIds;
			}
		}

		// Token: 0x17008A47 RID: 35399
		// (get) Token: 0x0601935F RID: 103263 RVA: 0x00347832 File Offset: 0x00345A32
		// (set) Token: 0x06019360 RID: 103264 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "type")]
		public EnumValue<BreakValues> Type
		{
			get
			{
				return (EnumValue<BreakValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008A48 RID: 35400
		// (get) Token: 0x06019361 RID: 103265 RVA: 0x00347841 File Offset: 0x00345A41
		// (set) Token: 0x06019362 RID: 103266 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "clear")]
		public EnumValue<BreakTextRestartLocationValues> Clear
		{
			get
			{
				return (EnumValue<BreakTextRestartLocationValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06019364 RID: 103268 RVA: 0x00347850 File Offset: 0x00345A50
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<BreakValues>();
			}
			if (23 == namespaceId && "clear" == name)
			{
				return new EnumValue<BreakTextRestartLocationValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019365 RID: 103269 RVA: 0x0034788A File Offset: 0x00345A8A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Break>(deep);
		}

		// Token: 0x0400A79C RID: 42908
		private const string tagName = "br";

		// Token: 0x0400A79D RID: 42909
		private const byte tagNsId = 23;

		// Token: 0x0400A79E RID: 42910
		internal const int ElementTypeIdConst = 11543;

		// Token: 0x0400A79F RID: 42911
		private static string[] attributeTagNames = new string[] { "type", "clear" };

		// Token: 0x0400A7A0 RID: 42912
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
