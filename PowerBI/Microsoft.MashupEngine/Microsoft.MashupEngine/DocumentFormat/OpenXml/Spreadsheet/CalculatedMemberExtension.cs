using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C4A RID: 11338
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CalculatedMember), FileFormatVersions.Office2010)]
	internal class CalculatedMemberExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081D3 RID: 33235
		// (get) Token: 0x060180B2 RID: 98482 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081D4 RID: 33236
		// (get) Token: 0x060180B3 RID: 98483 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081D5 RID: 33237
		// (get) Token: 0x060180B4 RID: 98484 RVA: 0x0033DE93 File Offset: 0x0033C093
		internal override int ElementTypeId
		{
			get
			{
				return 11319;
			}
		}

		// Token: 0x060180B5 RID: 98485 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081D6 RID: 33238
		// (get) Token: 0x060180B6 RID: 98486 RVA: 0x0033DE9A File Offset: 0x0033C09A
		internal override string[] AttributeTagNames
		{
			get
			{
				return CalculatedMemberExtension.attributeTagNames;
			}
		}

		// Token: 0x170081D7 RID: 33239
		// (get) Token: 0x060180B7 RID: 98487 RVA: 0x0033DEA1 File Offset: 0x0033C0A1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CalculatedMemberExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081D8 RID: 33240
		// (get) Token: 0x060180B8 RID: 98488 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060180B9 RID: 98489 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
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

		// Token: 0x060180BA RID: 98490 RVA: 0x00293ECF File Offset: 0x002920CF
		public CalculatedMemberExtension()
		{
		}

		// Token: 0x060180BB RID: 98491 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CalculatedMemberExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180BC RID: 98492 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CalculatedMemberExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180BD RID: 98493 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CalculatedMemberExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060180BE RID: 98494 RVA: 0x0033DEA8 File Offset: 0x0033C0A8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "calculatedMember" == name)
			{
				return new CalculatedMember();
			}
			return null;
		}

		// Token: 0x060180BF RID: 98495 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060180C0 RID: 98496 RVA: 0x0033DEC3 File Offset: 0x0033C0C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculatedMemberExtension>(deep);
		}

		// Token: 0x060180C1 RID: 98497 RVA: 0x0033DECC File Offset: 0x0033C0CC
		// Note: this type is marked as 'beforefieldinit'.
		static CalculatedMemberExtension()
		{
			byte[] array = new byte[1];
			CalculatedMemberExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009EA3 RID: 40611
		private const string tagName = "ext";

		// Token: 0x04009EA4 RID: 40612
		private const byte tagNsId = 22;

		// Token: 0x04009EA5 RID: 40613
		internal const int ElementTypeIdConst = 11319;

		// Token: 0x04009EA6 RID: 40614
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009EA7 RID: 40615
		private static byte[] attributeNamespaceIds;
	}
}
