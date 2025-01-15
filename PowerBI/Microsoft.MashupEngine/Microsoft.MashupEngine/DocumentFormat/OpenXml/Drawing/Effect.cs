using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002708 RID: 9992
	[GeneratedCode("DomGen", "2.0")]
	internal class Effect : OpenXmlLeafElement
	{
		// Token: 0x17005EA6 RID: 24230
		// (get) Token: 0x060131A8 RID: 78248 RVA: 0x00303BC8 File Offset: 0x00301DC8
		public override string LocalName
		{
			get
			{
				return "effect";
			}
		}

		// Token: 0x17005EA7 RID: 24231
		// (get) Token: 0x060131A9 RID: 78249 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EA8 RID: 24232
		// (get) Token: 0x060131AA RID: 78250 RVA: 0x00303BCF File Offset: 0x00301DCF
		internal override int ElementTypeId
		{
			get
			{
				return 10054;
			}
		}

		// Token: 0x060131AB RID: 78251 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005EA9 RID: 24233
		// (get) Token: 0x060131AC RID: 78252 RVA: 0x00303BD6 File Offset: 0x00301DD6
		internal override string[] AttributeTagNames
		{
			get
			{
				return Effect.attributeTagNames;
			}
		}

		// Token: 0x17005EAA RID: 24234
		// (get) Token: 0x060131AD RID: 78253 RVA: 0x00303BDD File Offset: 0x00301DDD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Effect.attributeNamespaceIds;
			}
		}

		// Token: 0x17005EAB RID: 24235
		// (get) Token: 0x060131AE RID: 78254 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060131AF RID: 78255 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x060131B1 RID: 78257 RVA: 0x00303BE4 File Offset: 0x00301DE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060131B2 RID: 78258 RVA: 0x00303C04 File Offset: 0x00301E04
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Effect>(deep);
		}

		// Token: 0x060131B3 RID: 78259 RVA: 0x00303C10 File Offset: 0x00301E10
		// Note: this type is marked as 'beforefieldinit'.
		static Effect()
		{
			byte[] array = new byte[1];
			Effect.attributeNamespaceIds = array;
		}

		// Token: 0x040084AD RID: 33965
		private const string tagName = "effect";

		// Token: 0x040084AE RID: 33966
		private const byte tagNsId = 10;

		// Token: 0x040084AF RID: 33967
		internal const int ElementTypeIdConst = 10054;

		// Token: 0x040084B0 RID: 33968
		private static string[] attributeTagNames = new string[] { "ref" };

		// Token: 0x040084B1 RID: 33969
		private static byte[] attributeNamespaceIds;
	}
}
