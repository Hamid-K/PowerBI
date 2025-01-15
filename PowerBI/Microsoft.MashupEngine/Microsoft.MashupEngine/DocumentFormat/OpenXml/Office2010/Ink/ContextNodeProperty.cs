using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Ink
{
	// Token: 0x02002267 RID: 8807
	[GeneratedCode("DomGen", "2.0")]
	internal class ContextNodeProperty : OpenXmlLeafTextElement
	{
		// Token: 0x17003CCC RID: 15564
		// (get) Token: 0x0600E787 RID: 59271 RVA: 0x002C85F5 File Offset: 0x002C67F5
		public override string LocalName
		{
			get
			{
				return "property";
			}
		}

		// Token: 0x17003CCD RID: 15565
		// (get) Token: 0x0600E788 RID: 59272 RVA: 0x002C826A File Offset: 0x002C646A
		internal override byte NamespaceId
		{
			get
			{
				return 45;
			}
		}

		// Token: 0x17003CCE RID: 15566
		// (get) Token: 0x0600E789 RID: 59273 RVA: 0x002C85FC File Offset: 0x002C67FC
		internal override int ElementTypeId
		{
			get
			{
				return 12688;
			}
		}

		// Token: 0x0600E78A RID: 59274 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003CCF RID: 15567
		// (get) Token: 0x0600E78B RID: 59275 RVA: 0x002C8603 File Offset: 0x002C6803
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContextNodeProperty.attributeTagNames;
			}
		}

		// Token: 0x17003CD0 RID: 15568
		// (get) Token: 0x0600E78C RID: 59276 RVA: 0x002C860A File Offset: 0x002C680A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContextNodeProperty.attributeNamespaceIds;
			}
		}

		// Token: 0x17003CD1 RID: 15569
		// (get) Token: 0x0600E78D RID: 59277 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E78E RID: 59278 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public StringValue Type
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

		// Token: 0x0600E78F RID: 59279 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ContextNodeProperty()
		{
		}

		// Token: 0x0600E790 RID: 59280 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ContextNodeProperty(string text)
			: base(text)
		{
		}

		// Token: 0x0600E791 RID: 59281 RVA: 0x002C8614 File Offset: 0x002C6814
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new HexBinaryValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600E792 RID: 59282 RVA: 0x002C862F File Offset: 0x002C682F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E793 RID: 59283 RVA: 0x002C864F File Offset: 0x002C684F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContextNodeProperty>(deep);
		}

		// Token: 0x0600E794 RID: 59284 RVA: 0x002C8658 File Offset: 0x002C6858
		// Note: this type is marked as 'beforefieldinit'.
		static ContextNodeProperty()
		{
			byte[] array = new byte[1];
			ContextNodeProperty.attributeNamespaceIds = array;
		}

		// Token: 0x04006F50 RID: 28496
		private const string tagName = "property";

		// Token: 0x04006F51 RID: 28497
		private const byte tagNsId = 45;

		// Token: 0x04006F52 RID: 28498
		internal const int ElementTypeIdConst = 12688;

		// Token: 0x04006F53 RID: 28499
		private static string[] attributeTagNames = new string[] { "type" };

		// Token: 0x04006F54 RID: 28500
		private static byte[] attributeNamespaceIds;
	}
}
