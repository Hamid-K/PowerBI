using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FFA RID: 12282
	[GeneratedCode("DomGen", "2.0")]
	internal class ReadModeInkLockDown : OpenXmlLeafElement
	{
		// Token: 0x170095BE RID: 38334
		// (get) Token: 0x0601AC36 RID: 109622 RVA: 0x0036743B File Offset: 0x0036563B
		public override string LocalName
		{
			get
			{
				return "readModeInkLockDown";
			}
		}

		// Token: 0x170095BF RID: 38335
		// (get) Token: 0x0601AC37 RID: 109623 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095C0 RID: 38336
		// (get) Token: 0x0601AC38 RID: 109624 RVA: 0x00367442 File Offset: 0x00365642
		internal override int ElementTypeId
		{
			get
			{
				return 12049;
			}
		}

		// Token: 0x0601AC39 RID: 109625 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170095C1 RID: 38337
		// (get) Token: 0x0601AC3A RID: 109626 RVA: 0x00367449 File Offset: 0x00365649
		internal override string[] AttributeTagNames
		{
			get
			{
				return ReadModeInkLockDown.attributeTagNames;
			}
		}

		// Token: 0x170095C2 RID: 38338
		// (get) Token: 0x0601AC3B RID: 109627 RVA: 0x00367450 File Offset: 0x00365650
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ReadModeInkLockDown.attributeNamespaceIds;
			}
		}

		// Token: 0x170095C3 RID: 38339
		// (get) Token: 0x0601AC3C RID: 109628 RVA: 0x002EBFC4 File Offset: 0x002EA1C4
		// (set) Token: 0x0601AC3D RID: 109629 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "actualPg")]
		public OnOffValue UseActualPages
		{
			get
			{
				return (OnOffValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170095C4 RID: 38340
		// (get) Token: 0x0601AC3E RID: 109630 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601AC3F RID: 109631 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "w")]
		public UInt32Value Width
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170095C5 RID: 38341
		// (get) Token: 0x0601AC40 RID: 109632 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x0601AC41 RID: 109633 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "h")]
		public UInt32Value Height
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170095C6 RID: 38342
		// (get) Token: 0x0601AC42 RID: 109634 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601AC43 RID: 109635 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "fontSz")]
		public StringValue FontSize
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0601AC45 RID: 109637 RVA: 0x00367458 File Offset: 0x00365658
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "actualPg" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "w" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "h" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "fontSz" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AC46 RID: 109638 RVA: 0x003674CD File Offset: 0x003656CD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ReadModeInkLockDown>(deep);
		}

		// Token: 0x0400AE36 RID: 44598
		private const string tagName = "readModeInkLockDown";

		// Token: 0x0400AE37 RID: 44599
		private const byte tagNsId = 23;

		// Token: 0x0400AE38 RID: 44600
		internal const int ElementTypeIdConst = 12049;

		// Token: 0x0400AE39 RID: 44601
		private static string[] attributeTagNames = new string[] { "actualPg", "w", "h", "fontSz" };

		// Token: 0x0400AE3A RID: 44602
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
