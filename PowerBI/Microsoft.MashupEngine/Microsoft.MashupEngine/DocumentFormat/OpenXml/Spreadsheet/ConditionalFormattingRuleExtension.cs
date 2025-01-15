using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C44 RID: 11332
	[ChildElementInfo(typeof(Id), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConditionalFormattingRuleExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081AF RID: 33199
		// (get) Token: 0x06018052 RID: 98386 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081B0 RID: 33200
		// (get) Token: 0x06018053 RID: 98387 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081B1 RID: 33201
		// (get) Token: 0x06018054 RID: 98388 RVA: 0x0033DB9F File Offset: 0x0033BD9F
		internal override int ElementTypeId
		{
			get
			{
				return 11313;
			}
		}

		// Token: 0x06018055 RID: 98389 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081B2 RID: 33202
		// (get) Token: 0x06018056 RID: 98390 RVA: 0x0033DBA6 File Offset: 0x0033BDA6
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormattingRuleExtension.attributeTagNames;
			}
		}

		// Token: 0x170081B3 RID: 33203
		// (get) Token: 0x06018057 RID: 98391 RVA: 0x0033DBAD File Offset: 0x0033BDAD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormattingRuleExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081B4 RID: 33204
		// (get) Token: 0x06018058 RID: 98392 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018059 RID: 98393 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601805A RID: 98394 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormattingRuleExtension()
		{
		}

		// Token: 0x0601805B RID: 98395 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormattingRuleExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601805C RID: 98396 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormattingRuleExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601805D RID: 98397 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormattingRuleExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601805E RID: 98398 RVA: 0x0033DBB4 File Offset: 0x0033BDB4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "id" == name)
			{
				return new Id();
			}
			return null;
		}

		// Token: 0x0601805F RID: 98399 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018060 RID: 98400 RVA: 0x0033DBCF File Offset: 0x0033BDCF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormattingRuleExtension>(deep);
		}

		// Token: 0x06018061 RID: 98401 RVA: 0x0033DBD8 File Offset: 0x0033BDD8
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormattingRuleExtension()
		{
			byte[] array = new byte[1];
			ConditionalFormattingRuleExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009E85 RID: 40581
		private const string tagName = "ext";

		// Token: 0x04009E86 RID: 40582
		private const byte tagNsId = 22;

		// Token: 0x04009E87 RID: 40583
		internal const int ElementTypeIdConst = 11313;

		// Token: 0x04009E88 RID: 40584
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009E89 RID: 40585
		private static byte[] attributeNamespaceIds;
	}
}
