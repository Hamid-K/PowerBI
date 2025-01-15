using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A2E RID: 10798
	[GeneratedCode("DomGen", "2.0")]
	internal class StringVariantValue : OpenXmlLeafElement
	{
		// Token: 0x170070BD RID: 28861
		// (get) Token: 0x06015AF1 RID: 88817 RVA: 0x00321FF3 File Offset: 0x003201F3
		public override string LocalName
		{
			get
			{
				return "strVal";
			}
		}

		// Token: 0x170070BE RID: 28862
		// (get) Token: 0x06015AF2 RID: 88818 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070BF RID: 28863
		// (get) Token: 0x06015AF3 RID: 88819 RVA: 0x00321FFA File Offset: 0x003201FA
		internal override int ElementTypeId
		{
			get
			{
				return 12220;
			}
		}

		// Token: 0x06015AF4 RID: 88820 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170070C0 RID: 28864
		// (get) Token: 0x06015AF5 RID: 88821 RVA: 0x00322001 File Offset: 0x00320201
		internal override string[] AttributeTagNames
		{
			get
			{
				return StringVariantValue.attributeTagNames;
			}
		}

		// Token: 0x170070C1 RID: 28865
		// (get) Token: 0x06015AF6 RID: 88822 RVA: 0x00322008 File Offset: 0x00320208
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StringVariantValue.attributeNamespaceIds;
			}
		}

		// Token: 0x170070C2 RID: 28866
		// (get) Token: 0x06015AF7 RID: 88823 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015AF8 RID: 88824 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public StringValue Val
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

		// Token: 0x06015AFA RID: 88826 RVA: 0x002E6B2F File Offset: 0x002E4D2F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015AFB RID: 88827 RVA: 0x0032200F File Offset: 0x0032020F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StringVariantValue>(deep);
		}

		// Token: 0x06015AFC RID: 88828 RVA: 0x00322018 File Offset: 0x00320218
		// Note: this type is marked as 'beforefieldinit'.
		static StringVariantValue()
		{
			byte[] array = new byte[1];
			StringVariantValue.attributeNamespaceIds = array;
		}

		// Token: 0x04009462 RID: 37986
		private const string tagName = "strVal";

		// Token: 0x04009463 RID: 37987
		private const byte tagNsId = 24;

		// Token: 0x04009464 RID: 37988
		internal const int ElementTypeIdConst = 12220;

		// Token: 0x04009465 RID: 37989
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009466 RID: 37990
		private static byte[] attributeNamespaceIds;
	}
}
