using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.LongProperties
{
	// Token: 0x020022B7 RID: 8887
	[GeneratedCode("DomGen", "2.0")]
	internal class LongProperty : OpenXmlLeafTextElement
	{
		// Token: 0x1700416D RID: 16749
		// (get) Token: 0x0600F165 RID: 61797 RVA: 0x002D143A File Offset: 0x002CF63A
		public override string LocalName
		{
			get
			{
				return "LongProp";
			}
		}

		// Token: 0x1700416E RID: 16750
		// (get) Token: 0x0600F166 RID: 61798 RVA: 0x002D140B File Offset: 0x002CF60B
		internal override byte NamespaceId
		{
			get
			{
				return 40;
			}
		}

		// Token: 0x1700416F RID: 16751
		// (get) Token: 0x0600F167 RID: 61799 RVA: 0x002D1441 File Offset: 0x002CF641
		internal override int ElementTypeId
		{
			get
			{
				return 12641;
			}
		}

		// Token: 0x0600F168 RID: 61800 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17004170 RID: 16752
		// (get) Token: 0x0600F169 RID: 61801 RVA: 0x002D1448 File Offset: 0x002CF648
		internal override string[] AttributeTagNames
		{
			get
			{
				return LongProperty.attributeTagNames;
			}
		}

		// Token: 0x17004171 RID: 16753
		// (get) Token: 0x0600F16A RID: 61802 RVA: 0x002D144F File Offset: 0x002CF64F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LongProperty.attributeNamespaceIds;
			}
		}

		// Token: 0x17004172 RID: 16754
		// (get) Token: 0x0600F16B RID: 61803 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F16C RID: 61804 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0600F16D RID: 61805 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public LongProperty()
		{
		}

		// Token: 0x0600F16E RID: 61806 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public LongProperty(string text)
			: base(text)
		{
		}

		// Token: 0x0600F16F RID: 61807 RVA: 0x002D1458 File Offset: 0x002CF658
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F170 RID: 61808 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F171 RID: 61809 RVA: 0x002D1493 File Offset: 0x002CF693
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LongProperty>(deep);
		}

		// Token: 0x0600F172 RID: 61810 RVA: 0x002D149C File Offset: 0x002CF69C
		// Note: this type is marked as 'beforefieldinit'.
		static LongProperty()
		{
			byte[] array = new byte[1];
			LongProperty.attributeNamespaceIds = array;
		}

		// Token: 0x040070C3 RID: 28867
		private const string tagName = "LongProp";

		// Token: 0x040070C4 RID: 28868
		private const byte tagNsId = 40;

		// Token: 0x040070C5 RID: 28869
		internal const int ElementTypeIdConst = 12641;

		// Token: 0x040070C6 RID: 28870
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x040070C7 RID: 28871
		private static byte[] attributeNamespaceIds;
	}
}
