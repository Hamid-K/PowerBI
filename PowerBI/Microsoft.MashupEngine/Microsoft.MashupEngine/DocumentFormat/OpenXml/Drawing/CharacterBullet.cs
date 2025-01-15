using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002752 RID: 10066
	[GeneratedCode("DomGen", "2.0")]
	internal class CharacterBullet : OpenXmlLeafElement
	{
		// Token: 0x170060A1 RID: 24737
		// (get) Token: 0x060135F3 RID: 79347 RVA: 0x0030665B File Offset: 0x0030485B
		public override string LocalName
		{
			get
			{
				return "buChar";
			}
		}

		// Token: 0x170060A2 RID: 24738
		// (get) Token: 0x060135F4 RID: 79348 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060A3 RID: 24739
		// (get) Token: 0x060135F5 RID: 79349 RVA: 0x00306662 File Offset: 0x00304862
		internal override int ElementTypeId
		{
			get
			{
				return 10111;
			}
		}

		// Token: 0x060135F6 RID: 79350 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170060A4 RID: 24740
		// (get) Token: 0x060135F7 RID: 79351 RVA: 0x00306669 File Offset: 0x00304869
		internal override string[] AttributeTagNames
		{
			get
			{
				return CharacterBullet.attributeTagNames;
			}
		}

		// Token: 0x170060A5 RID: 24741
		// (get) Token: 0x060135F8 RID: 79352 RVA: 0x00306670 File Offset: 0x00304870
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CharacterBullet.attributeNamespaceIds;
			}
		}

		// Token: 0x170060A6 RID: 24742
		// (get) Token: 0x060135F9 RID: 79353 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060135FA RID: 79354 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "char")]
		public StringValue Char
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

		// Token: 0x060135FC RID: 79356 RVA: 0x00306677 File Offset: 0x00304877
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "char" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060135FD RID: 79357 RVA: 0x00306697 File Offset: 0x00304897
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CharacterBullet>(deep);
		}

		// Token: 0x060135FE RID: 79358 RVA: 0x003066A0 File Offset: 0x003048A0
		// Note: this type is marked as 'beforefieldinit'.
		static CharacterBullet()
		{
			byte[] array = new byte[1];
			CharacterBullet.attributeNamespaceIds = array;
		}

		// Token: 0x040085E9 RID: 34281
		private const string tagName = "buChar";

		// Token: 0x040085EA RID: 34282
		private const byte tagNsId = 10;

		// Token: 0x040085EB RID: 34283
		internal const int ElementTypeIdConst = 10111;

		// Token: 0x040085EC RID: 34284
		private static string[] attributeTagNames = new string[] { "char" };

		// Token: 0x040085ED RID: 34285
		private static byte[] attributeNamespaceIds;
	}
}
