using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002711 RID: 10001
	[GeneratedCode("DomGen", "2.0")]
	internal class BiLevel : OpenXmlLeafElement
	{
		// Token: 0x17005EDD RID: 24285
		// (get) Token: 0x0601321A RID: 78362 RVA: 0x00303FE7 File Offset: 0x003021E7
		public override string LocalName
		{
			get
			{
				return "biLevel";
			}
		}

		// Token: 0x17005EDE RID: 24286
		// (get) Token: 0x0601321B RID: 78363 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EDF RID: 24287
		// (get) Token: 0x0601321C RID: 78364 RVA: 0x00303FEE File Offset: 0x003021EE
		internal override int ElementTypeId
		{
			get
			{
				return 10063;
			}
		}

		// Token: 0x0601321D RID: 78365 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005EE0 RID: 24288
		// (get) Token: 0x0601321E RID: 78366 RVA: 0x00303FF5 File Offset: 0x003021F5
		internal override string[] AttributeTagNames
		{
			get
			{
				return BiLevel.attributeTagNames;
			}
		}

		// Token: 0x17005EE1 RID: 24289
		// (get) Token: 0x0601321F RID: 78367 RVA: 0x00303FFC File Offset: 0x003021FC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BiLevel.attributeNamespaceIds;
			}
		}

		// Token: 0x17005EE2 RID: 24290
		// (get) Token: 0x06013220 RID: 78368 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013221 RID: 78369 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06013223 RID: 78371 RVA: 0x00303C5B File Offset: 0x00301E5B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "thresh" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013224 RID: 78372 RVA: 0x00304003 File Offset: 0x00302203
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BiLevel>(deep);
		}

		// Token: 0x06013225 RID: 78373 RVA: 0x0030400C File Offset: 0x0030220C
		// Note: this type is marked as 'beforefieldinit'.
		static BiLevel()
		{
			byte[] array = new byte[1];
			BiLevel.attributeNamespaceIds = array;
		}

		// Token: 0x040084D6 RID: 34006
		private const string tagName = "biLevel";

		// Token: 0x040084D7 RID: 34007
		private const byte tagNsId = 10;

		// Token: 0x040084D8 RID: 34008
		internal const int ElementTypeIdConst = 10063;

		// Token: 0x040084D9 RID: 34009
		private static string[] attributeTagNames = new string[] { "thresh" };

		// Token: 0x040084DA RID: 34010
		private static byte[] attributeNamespaceIds;
	}
}
