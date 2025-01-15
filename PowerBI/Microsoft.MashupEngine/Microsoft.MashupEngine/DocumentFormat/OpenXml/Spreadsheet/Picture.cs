using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C87 RID: 11399
	[GeneratedCode("DomGen", "2.0")]
	internal class Picture : OpenXmlLeafElement
	{
		// Token: 0x17008383 RID: 33667
		// (get) Token: 0x060184AD RID: 99501 RVA: 0x002D0AB9 File Offset: 0x002CECB9
		public override string LocalName
		{
			get
			{
				return "picture";
			}
		}

		// Token: 0x17008384 RID: 33668
		// (get) Token: 0x060184AE RID: 99502 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008385 RID: 33669
		// (get) Token: 0x060184AF RID: 99503 RVA: 0x0034038C File Offset: 0x0033E58C
		internal override int ElementTypeId
		{
			get
			{
				return 11378;
			}
		}

		// Token: 0x060184B0 RID: 99504 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008386 RID: 33670
		// (get) Token: 0x060184B1 RID: 99505 RVA: 0x00340393 File Offset: 0x0033E593
		internal override string[] AttributeTagNames
		{
			get
			{
				return Picture.attributeTagNames;
			}
		}

		// Token: 0x17008387 RID: 33671
		// (get) Token: 0x060184B2 RID: 99506 RVA: 0x0034039A File Offset: 0x0033E59A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Picture.attributeNamespaceIds;
			}
		}

		// Token: 0x17008388 RID: 33672
		// (get) Token: 0x060184B3 RID: 99507 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060184B4 RID: 99508 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060184B6 RID: 99510 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060184B7 RID: 99511 RVA: 0x003403A1 File Offset: 0x0033E5A1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Picture>(deep);
		}

		// Token: 0x04009FA8 RID: 40872
		private const string tagName = "picture";

		// Token: 0x04009FA9 RID: 40873
		private const byte tagNsId = 22;

		// Token: 0x04009FAA RID: 40874
		internal const int ElementTypeIdConst = 11378;

		// Token: 0x04009FAB RID: 40875
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04009FAC RID: 40876
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
