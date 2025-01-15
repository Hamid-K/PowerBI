using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C82 RID: 11394
	[GeneratedCode("DomGen", "2.0")]
	internal class Drawing : OpenXmlLeafElement
	{
		// Token: 0x1700835C RID: 33628
		// (get) Token: 0x0601845E RID: 99422 RVA: 0x002A7FB6 File Offset: 0x002A61B6
		public override string LocalName
		{
			get
			{
				return "drawing";
			}
		}

		// Token: 0x1700835D RID: 33629
		// (get) Token: 0x0601845F RID: 99423 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700835E RID: 33630
		// (get) Token: 0x06018460 RID: 99424 RVA: 0x0034001D File Offset: 0x0033E21D
		internal override int ElementTypeId
		{
			get
			{
				return 11374;
			}
		}

		// Token: 0x06018461 RID: 99425 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700835F RID: 33631
		// (get) Token: 0x06018462 RID: 99426 RVA: 0x00340024 File Offset: 0x0033E224
		internal override string[] AttributeTagNames
		{
			get
			{
				return Drawing.attributeTagNames;
			}
		}

		// Token: 0x17008360 RID: 33632
		// (get) Token: 0x06018463 RID: 99427 RVA: 0x0034002B File Offset: 0x0033E22B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Drawing.attributeNamespaceIds;
			}
		}

		// Token: 0x17008361 RID: 33633
		// (get) Token: 0x06018464 RID: 99428 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018465 RID: 99429 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x06018467 RID: 99431 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018468 RID: 99432 RVA: 0x00340032 File Offset: 0x0033E232
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Drawing>(deep);
		}

		// Token: 0x04009F96 RID: 40854
		private const string tagName = "drawing";

		// Token: 0x04009F97 RID: 40855
		private const byte tagNsId = 22;

		// Token: 0x04009F98 RID: 40856
		internal const int ElementTypeIdConst = 11374;

		// Token: 0x04009F99 RID: 40857
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04009F9A RID: 40858
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
