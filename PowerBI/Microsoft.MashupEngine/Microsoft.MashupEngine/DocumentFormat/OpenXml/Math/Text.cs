using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002989 RID: 10633
	[GeneratedCode("DomGen", "2.0")]
	internal class Text : OpenXmlLeafTextElement
	{
		// Token: 0x17006CAE RID: 27822
		// (get) Token: 0x060151EC RID: 86508 RVA: 0x00300F6A File Offset: 0x002FF16A
		public override string LocalName
		{
			get
			{
				return "t";
			}
		}

		// Token: 0x17006CAF RID: 27823
		// (get) Token: 0x060151ED RID: 86509 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CB0 RID: 27824
		// (get) Token: 0x060151EE RID: 86510 RVA: 0x0031B858 File Offset: 0x00319A58
		internal override int ElementTypeId
		{
			get
			{
				return 10869;
			}
		}

		// Token: 0x060151EF RID: 86511 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006CB1 RID: 27825
		// (get) Token: 0x060151F0 RID: 86512 RVA: 0x0031B85F File Offset: 0x00319A5F
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocumentFormat.OpenXml.Math.Text.attributeTagNames;
			}
		}

		// Token: 0x17006CB2 RID: 27826
		// (get) Token: 0x060151F1 RID: 86513 RVA: 0x0031B866 File Offset: 0x00319A66
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocumentFormat.OpenXml.Math.Text.attributeNamespaceIds;
			}
		}

		// Token: 0x17006CB3 RID: 27827
		// (get) Token: 0x060151F2 RID: 86514 RVA: 0x0031B86D File Offset: 0x00319A6D
		// (set) Token: 0x060151F3 RID: 86515 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(1, "space")]
		public EnumValue<SpaceProcessingModeValues> Space
		{
			get
			{
				return (EnumValue<SpaceProcessingModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060151F4 RID: 86516 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Text()
		{
		}

		// Token: 0x060151F5 RID: 86517 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Text(string text)
			: base(text)
		{
		}

		// Token: 0x060151F6 RID: 86518 RVA: 0x0031B87C File Offset: 0x00319A7C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060151F7 RID: 86519 RVA: 0x0031B897 File Offset: 0x00319A97
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "space" == name)
			{
				return new EnumValue<SpaceProcessingModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060151F8 RID: 86520 RVA: 0x0031B8B8 File Offset: 0x00319AB8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Text>(deep);
		}

		// Token: 0x040091AB RID: 37291
		private const string tagName = "t";

		// Token: 0x040091AC RID: 37292
		private const byte tagNsId = 21;

		// Token: 0x040091AD RID: 37293
		internal const int ElementTypeIdConst = 10869;

		// Token: 0x040091AE RID: 37294
		private static string[] attributeTagNames = new string[] { "space" };

		// Token: 0x040091AF RID: 37295
		private static byte[] attributeNamespaceIds = new byte[] { 1 };
	}
}
