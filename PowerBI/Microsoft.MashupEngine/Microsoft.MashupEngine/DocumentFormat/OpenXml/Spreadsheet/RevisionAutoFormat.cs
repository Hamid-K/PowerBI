using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BB8 RID: 11192
	[GeneratedCode("DomGen", "2.0")]
	internal class RevisionAutoFormat : OpenXmlLeafElement
	{
		// Token: 0x17007C08 RID: 31752
		// (get) Token: 0x06017423 RID: 95267 RVA: 0x00334963 File Offset: 0x00332B63
		public override string LocalName
		{
			get
			{
				return "raf";
			}
		}

		// Token: 0x17007C09 RID: 31753
		// (get) Token: 0x06017424 RID: 95268 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C0A RID: 31754
		// (get) Token: 0x06017425 RID: 95269 RVA: 0x0033496A File Offset: 0x00332B6A
		internal override int ElementTypeId
		{
			get
			{
				return 11163;
			}
		}

		// Token: 0x06017426 RID: 95270 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007C0B RID: 31755
		// (get) Token: 0x06017427 RID: 95271 RVA: 0x00334971 File Offset: 0x00332B71
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionAutoFormat.attributeTagNames;
			}
		}

		// Token: 0x17007C0C RID: 31756
		// (get) Token: 0x06017428 RID: 95272 RVA: 0x00334978 File Offset: 0x00332B78
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionAutoFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x17007C0D RID: 31757
		// (get) Token: 0x06017429 RID: 95273 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601742A RID: 95274 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "sheetId")]
		public UInt32Value SheetId
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007C0E RID: 31758
		// (get) Token: 0x0601742B RID: 95275 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601742C RID: 95276 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "autoFormatId")]
		public UInt32Value AutoFormatId
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

		// Token: 0x17007C0F RID: 31759
		// (get) Token: 0x0601742D RID: 95277 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601742E RID: 95278 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "applyNumberFormats")]
		public BooleanValue ApplyNumberFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007C10 RID: 31760
		// (get) Token: 0x0601742F RID: 95279 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017430 RID: 95280 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "applyBorderFormats")]
		public BooleanValue ApplyBorderFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007C11 RID: 31761
		// (get) Token: 0x06017431 RID: 95281 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017432 RID: 95282 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "applyFontFormats")]
		public BooleanValue ApplyFontFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007C12 RID: 31762
		// (get) Token: 0x06017433 RID: 95283 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017434 RID: 95284 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "applyPatternFormats")]
		public BooleanValue ApplyPatternFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007C13 RID: 31763
		// (get) Token: 0x06017435 RID: 95285 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017436 RID: 95286 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "applyAlignmentFormats")]
		public BooleanValue ApplyAlignmentFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007C14 RID: 31764
		// (get) Token: 0x06017437 RID: 95287 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017438 RID: 95288 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "applyWidthHeightFormats")]
		public BooleanValue ApplyWidthHeightFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007C15 RID: 31765
		// (get) Token: 0x06017439 RID: 95289 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601743A RID: 95290 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "ref")]
		public StringValue Reference
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x0601743C RID: 95292 RVA: 0x00334980 File Offset: 0x00332B80
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "sheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "autoFormatId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "applyNumberFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyBorderFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyFontFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyPatternFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyAlignmentFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyWidthHeightFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601743D RID: 95293 RVA: 0x00334A5B File Offset: 0x00332C5B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionAutoFormat>(deep);
		}

		// Token: 0x0601743E RID: 95294 RVA: 0x00334A64 File Offset: 0x00332C64
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionAutoFormat()
		{
			byte[] array = new byte[9];
			RevisionAutoFormat.attributeNamespaceIds = array;
		}

		// Token: 0x04009BBF RID: 39871
		private const string tagName = "raf";

		// Token: 0x04009BC0 RID: 39872
		private const byte tagNsId = 22;

		// Token: 0x04009BC1 RID: 39873
		internal const int ElementTypeIdConst = 11163;

		// Token: 0x04009BC2 RID: 39874
		private static string[] attributeTagNames = new string[] { "sheetId", "autoFormatId", "applyNumberFormats", "applyBorderFormats", "applyFontFormats", "applyPatternFormats", "applyAlignmentFormats", "applyWidthHeightFormats", "ref" };

		// Token: 0x04009BC3 RID: 39875
		private static byte[] attributeNamespaceIds;
	}
}
