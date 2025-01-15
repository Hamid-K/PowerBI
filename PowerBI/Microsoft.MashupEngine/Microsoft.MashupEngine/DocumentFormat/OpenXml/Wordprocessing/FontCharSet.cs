using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FB2 RID: 12210
	[GeneratedCode("DomGen", "2.0")]
	internal class FontCharSet : OpenXmlLeafElement
	{
		// Token: 0x17009389 RID: 37769
		// (get) Token: 0x0601A776 RID: 108406 RVA: 0x0033377A File Offset: 0x0033197A
		public override string LocalName
		{
			get
			{
				return "charset";
			}
		}

		// Token: 0x1700938A RID: 37770
		// (get) Token: 0x0601A777 RID: 108407 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700938B RID: 37771
		// (get) Token: 0x0601A778 RID: 108408 RVA: 0x00362BA8 File Offset: 0x00360DA8
		internal override int ElementTypeId
		{
			get
			{
				return 11917;
			}
		}

		// Token: 0x0601A779 RID: 108409 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700938C RID: 37772
		// (get) Token: 0x0601A77A RID: 108410 RVA: 0x00362BAF File Offset: 0x00360DAF
		internal override string[] AttributeTagNames
		{
			get
			{
				return FontCharSet.attributeTagNames;
			}
		}

		// Token: 0x1700938D RID: 37773
		// (get) Token: 0x0601A77B RID: 108411 RVA: 0x00362BB6 File Offset: 0x00360DB6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FontCharSet.attributeNamespaceIds;
			}
		}

		// Token: 0x1700938E RID: 37774
		// (get) Token: 0x0601A77C RID: 108412 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A77D RID: 108413 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
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

		// Token: 0x0601A77F RID: 108415 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A780 RID: 108416 RVA: 0x00362BBD File Offset: 0x00360DBD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontCharSet>(deep);
		}

		// Token: 0x0400AD0F RID: 44303
		private const string tagName = "charset";

		// Token: 0x0400AD10 RID: 44304
		private const byte tagNsId = 23;

		// Token: 0x0400AD11 RID: 44305
		internal const int ElementTypeIdConst = 11917;

		// Token: 0x0400AD12 RID: 44306
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AD13 RID: 44307
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
