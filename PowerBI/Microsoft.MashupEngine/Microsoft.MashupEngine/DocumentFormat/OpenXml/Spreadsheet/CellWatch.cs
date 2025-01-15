using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BE3 RID: 11235
	[GeneratedCode("DomGen", "2.0")]
	internal class CellWatch : OpenXmlLeafElement
	{
		// Token: 0x17007DE4 RID: 32228
		// (get) Token: 0x06017817 RID: 96279 RVA: 0x00337AF3 File Offset: 0x00335CF3
		public override string LocalName
		{
			get
			{
				return "cellWatch";
			}
		}

		// Token: 0x17007DE5 RID: 32229
		// (get) Token: 0x06017818 RID: 96280 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007DE6 RID: 32230
		// (get) Token: 0x06017819 RID: 96281 RVA: 0x00337AFA File Offset: 0x00335CFA
		internal override int ElementTypeId
		{
			get
			{
				return 11207;
			}
		}

		// Token: 0x0601781A RID: 96282 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007DE7 RID: 32231
		// (get) Token: 0x0601781B RID: 96283 RVA: 0x00337B01 File Offset: 0x00335D01
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellWatch.attributeTagNames;
			}
		}

		// Token: 0x17007DE8 RID: 32232
		// (get) Token: 0x0601781C RID: 96284 RVA: 0x00337B08 File Offset: 0x00335D08
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellWatch.attributeNamespaceIds;
			}
		}

		// Token: 0x17007DE9 RID: 32233
		// (get) Token: 0x0601781D RID: 96285 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601781E RID: 96286 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "r")]
		public StringValue CellReference
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

		// Token: 0x06017820 RID: 96288 RVA: 0x00336AE0 File Offset: 0x00334CE0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017821 RID: 96289 RVA: 0x00337B0F File Offset: 0x00335D0F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellWatch>(deep);
		}

		// Token: 0x06017822 RID: 96290 RVA: 0x00337B18 File Offset: 0x00335D18
		// Note: this type is marked as 'beforefieldinit'.
		static CellWatch()
		{
			byte[] array = new byte[1];
			CellWatch.attributeNamespaceIds = array;
		}

		// Token: 0x04009C8F RID: 40079
		private const string tagName = "cellWatch";

		// Token: 0x04009C90 RID: 40080
		private const byte tagNsId = 22;

		// Token: 0x04009C91 RID: 40081
		internal const int ElementTypeIdConst = 11207;

		// Token: 0x04009C92 RID: 40082
		private static string[] attributeTagNames = new string[] { "r" };

		// Token: 0x04009C93 RID: 40083
		private static byte[] attributeNamespaceIds;
	}
}
