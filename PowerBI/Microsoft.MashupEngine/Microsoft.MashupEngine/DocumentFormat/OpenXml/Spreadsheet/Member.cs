using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B81 RID: 11137
	[GeneratedCode("DomGen", "2.0")]
	internal class Member : OpenXmlLeafElement
	{
		// Token: 0x17007A43 RID: 31299
		// (get) Token: 0x06017056 RID: 94294 RVA: 0x00331CB1 File Offset: 0x0032FEB1
		public override string LocalName
		{
			get
			{
				return "member";
			}
		}

		// Token: 0x17007A44 RID: 31300
		// (get) Token: 0x06017057 RID: 94295 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A45 RID: 31301
		// (get) Token: 0x06017058 RID: 94296 RVA: 0x00331CB8 File Offset: 0x0032FEB8
		internal override int ElementTypeId
		{
			get
			{
				return 11115;
			}
		}

		// Token: 0x06017059 RID: 94297 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A46 RID: 31302
		// (get) Token: 0x0601705A RID: 94298 RVA: 0x00331CBF File Offset: 0x0032FEBF
		internal override string[] AttributeTagNames
		{
			get
			{
				return Member.attributeTagNames;
			}
		}

		// Token: 0x17007A47 RID: 31303
		// (get) Token: 0x0601705B RID: 94299 RVA: 0x00331CC6 File Offset: 0x0032FEC6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Member.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A48 RID: 31304
		// (get) Token: 0x0601705C RID: 94300 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601705D RID: 94301 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601705F RID: 94303 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017060 RID: 94304 RVA: 0x00331CCD File Offset: 0x0032FECD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Member>(deep);
		}

		// Token: 0x06017061 RID: 94305 RVA: 0x00331CD8 File Offset: 0x0032FED8
		// Note: this type is marked as 'beforefieldinit'.
		static Member()
		{
			byte[] array = new byte[1];
			Member.attributeNamespaceIds = array;
		}

		// Token: 0x04009AC6 RID: 39622
		private const string tagName = "member";

		// Token: 0x04009AC7 RID: 39623
		private const byte tagNsId = 22;

		// Token: 0x04009AC8 RID: 39624
		internal const int ElementTypeIdConst = 11115;

		// Token: 0x04009AC9 RID: 39625
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x04009ACA RID: 39626
		private static byte[] attributeNamespaceIds;
	}
}
