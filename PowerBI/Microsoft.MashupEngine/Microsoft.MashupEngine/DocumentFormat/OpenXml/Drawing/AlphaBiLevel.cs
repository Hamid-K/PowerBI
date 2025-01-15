using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002709 RID: 9993
	[GeneratedCode("DomGen", "2.0")]
	internal class AlphaBiLevel : OpenXmlLeafElement
	{
		// Token: 0x17005EAC RID: 24236
		// (get) Token: 0x060131B4 RID: 78260 RVA: 0x00303C3F File Offset: 0x00301E3F
		public override string LocalName
		{
			get
			{
				return "alphaBiLevel";
			}
		}

		// Token: 0x17005EAD RID: 24237
		// (get) Token: 0x060131B5 RID: 78261 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EAE RID: 24238
		// (get) Token: 0x060131B6 RID: 78262 RVA: 0x00303C46 File Offset: 0x00301E46
		internal override int ElementTypeId
		{
			get
			{
				return 10055;
			}
		}

		// Token: 0x060131B7 RID: 78263 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005EAF RID: 24239
		// (get) Token: 0x060131B8 RID: 78264 RVA: 0x00303C4D File Offset: 0x00301E4D
		internal override string[] AttributeTagNames
		{
			get
			{
				return AlphaBiLevel.attributeTagNames;
			}
		}

		// Token: 0x17005EB0 RID: 24240
		// (get) Token: 0x060131B9 RID: 78265 RVA: 0x00303C54 File Offset: 0x00301E54
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AlphaBiLevel.attributeNamespaceIds;
			}
		}

		// Token: 0x17005EB1 RID: 24241
		// (get) Token: 0x060131BA RID: 78266 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060131BB RID: 78267 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "thresh")]
		public Int32Value Threshold
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060131BD RID: 78269 RVA: 0x00303C5B File Offset: 0x00301E5B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "thresh" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060131BE RID: 78270 RVA: 0x00303C7B File Offset: 0x00301E7B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlphaBiLevel>(deep);
		}

		// Token: 0x060131BF RID: 78271 RVA: 0x00303C84 File Offset: 0x00301E84
		// Note: this type is marked as 'beforefieldinit'.
		static AlphaBiLevel()
		{
			byte[] array = new byte[1];
			AlphaBiLevel.attributeNamespaceIds = array;
		}

		// Token: 0x040084B2 RID: 33970
		private const string tagName = "alphaBiLevel";

		// Token: 0x040084B3 RID: 33971
		private const byte tagNsId = 10;

		// Token: 0x040084B4 RID: 33972
		internal const int ElementTypeIdConst = 10055;

		// Token: 0x040084B5 RID: 33973
		private static string[] attributeTagNames = new string[] { "thresh" };

		// Token: 0x040084B6 RID: 33974
		private static byte[] attributeNamespaceIds;
	}
}
