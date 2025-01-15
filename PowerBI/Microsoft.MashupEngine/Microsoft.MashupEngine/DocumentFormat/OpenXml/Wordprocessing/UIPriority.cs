using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FA7 RID: 12199
	[GeneratedCode("DomGen", "2.0")]
	internal class UIPriority : OpenXmlLeafElement
	{
		// Token: 0x170092E9 RID: 37609
		// (get) Token: 0x0601A628 RID: 108072 RVA: 0x00361800 File Offset: 0x0035FA00
		public override string LocalName
		{
			get
			{
				return "uiPriority";
			}
		}

		// Token: 0x170092EA RID: 37610
		// (get) Token: 0x0601A629 RID: 108073 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170092EB RID: 37611
		// (get) Token: 0x0601A62A RID: 108074 RVA: 0x00361807 File Offset: 0x0035FA07
		internal override int ElementTypeId
		{
			get
			{
				return 11899;
			}
		}

		// Token: 0x0601A62B RID: 108075 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170092EC RID: 37612
		// (get) Token: 0x0601A62C RID: 108076 RVA: 0x0036180E File Offset: 0x0035FA0E
		internal override string[] AttributeTagNames
		{
			get
			{
				return UIPriority.attributeTagNames;
			}
		}

		// Token: 0x170092ED RID: 37613
		// (get) Token: 0x0601A62D RID: 108077 RVA: 0x00361815 File Offset: 0x0035FA15
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UIPriority.attributeNamespaceIds;
			}
		}

		// Token: 0x170092EE RID: 37614
		// (get) Token: 0x0601A62E RID: 108078 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601A62F RID: 108079 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public Int32Value Val
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

		// Token: 0x0601A631 RID: 108081 RVA: 0x00346792 File Offset: 0x00344992
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A632 RID: 108082 RVA: 0x0036181C File Offset: 0x0035FA1C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UIPriority>(deep);
		}

		// Token: 0x0400ACD4 RID: 44244
		private const string tagName = "uiPriority";

		// Token: 0x0400ACD5 RID: 44245
		private const byte tagNsId = 23;

		// Token: 0x0400ACD6 RID: 44246
		internal const int ElementTypeIdConst = 11899;

		// Token: 0x0400ACD7 RID: 44247
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ACD8 RID: 44248
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
