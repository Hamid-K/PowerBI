using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FED RID: 12269
	[GeneratedCode("DomGen", "2.0")]
	internal class NoLineBreaksAfterKinsoku : OpenXmlLeafElement
	{
		// Token: 0x1700952A RID: 38186
		// (get) Token: 0x0601AAF3 RID: 109299 RVA: 0x00365E1C File Offset: 0x0036401C
		public override string LocalName
		{
			get
			{
				return "noLineBreaksAfter";
			}
		}

		// Token: 0x1700952B RID: 38187
		// (get) Token: 0x0601AAF4 RID: 109300 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700952C RID: 38188
		// (get) Token: 0x0601AAF5 RID: 109301 RVA: 0x00365E23 File Offset: 0x00364023
		internal override int ElementTypeId
		{
			get
			{
				return 12021;
			}
		}

		// Token: 0x0601AAF6 RID: 109302 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700952D RID: 38189
		// (get) Token: 0x0601AAF7 RID: 109303 RVA: 0x00365E2A File Offset: 0x0036402A
		internal override string[] AttributeTagNames
		{
			get
			{
				return NoLineBreaksAfterKinsoku.attributeTagNames;
			}
		}

		// Token: 0x1700952E RID: 38190
		// (get) Token: 0x0601AAF8 RID: 109304 RVA: 0x00365E31 File Offset: 0x00364031
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NoLineBreaksAfterKinsoku.attributeNamespaceIds;
			}
		}

		// Token: 0x1700952F RID: 38191
		// (get) Token: 0x0601AAF9 RID: 109305 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AAFA RID: 109306 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "lang")]
		public StringValue Language
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

		// Token: 0x17009530 RID: 38192
		// (get) Token: 0x0601AAFB RID: 109307 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AAFC RID: 109308 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601AAFE RID: 109310 RVA: 0x00365E38 File Offset: 0x00364038
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "lang" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AAFF RID: 109311 RVA: 0x00365E72 File Offset: 0x00364072
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoLineBreaksAfterKinsoku>(deep);
		}

		// Token: 0x0400AE02 RID: 44546
		private const string tagName = "noLineBreaksAfter";

		// Token: 0x0400AE03 RID: 44547
		private const byte tagNsId = 23;

		// Token: 0x0400AE04 RID: 44548
		internal const int ElementTypeIdConst = 12021;

		// Token: 0x0400AE05 RID: 44549
		private static string[] attributeTagNames = new string[] { "lang", "val" };

		// Token: 0x0400AE06 RID: 44550
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
