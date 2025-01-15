using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C36 RID: 11318
	[GeneratedCode("DomGen", "2.0")]
	internal class ExternalReference : OpenXmlLeafElement
	{
		// Token: 0x1700811B RID: 33051
		// (get) Token: 0x06017F10 RID: 98064 RVA: 0x002A8056 File Offset: 0x002A6256
		public override string LocalName
		{
			get
			{
				return "externalReference";
			}
		}

		// Token: 0x1700811C RID: 33052
		// (get) Token: 0x06017F11 RID: 98065 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700811D RID: 33053
		// (get) Token: 0x06017F12 RID: 98066 RVA: 0x0033CD4F File Offset: 0x0033AF4F
		internal override int ElementTypeId
		{
			get
			{
				return 11300;
			}
		}

		// Token: 0x06017F13 RID: 98067 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700811E RID: 33054
		// (get) Token: 0x06017F14 RID: 98068 RVA: 0x0033CD56 File Offset: 0x0033AF56
		internal override string[] AttributeTagNames
		{
			get
			{
				return ExternalReference.attributeTagNames;
			}
		}

		// Token: 0x1700811F RID: 33055
		// (get) Token: 0x06017F15 RID: 98069 RVA: 0x0033CD5D File Offset: 0x0033AF5D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ExternalReference.attributeNamespaceIds;
			}
		}

		// Token: 0x17008120 RID: 33056
		// (get) Token: 0x06017F16 RID: 98070 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017F17 RID: 98071 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06017F19 RID: 98073 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017F1A RID: 98074 RVA: 0x0033CD64 File Offset: 0x0033AF64
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExternalReference>(deep);
		}

		// Token: 0x04009E40 RID: 40512
		private const string tagName = "externalReference";

		// Token: 0x04009E41 RID: 40513
		private const byte tagNsId = 22;

		// Token: 0x04009E42 RID: 40514
		internal const int ElementTypeIdConst = 11300;

		// Token: 0x04009E43 RID: 40515
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04009E44 RID: 40516
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
