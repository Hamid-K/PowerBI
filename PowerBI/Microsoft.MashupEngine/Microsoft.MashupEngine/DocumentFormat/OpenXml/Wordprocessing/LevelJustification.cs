using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F99 RID: 12185
	[GeneratedCode("DomGen", "2.0")]
	internal class LevelJustification : OpenXmlLeafElement
	{
		// Token: 0x17009204 RID: 37380
		// (get) Token: 0x0601A44B RID: 107595 RVA: 0x0035FC6C File Offset: 0x0035DE6C
		public override string LocalName
		{
			get
			{
				return "lvlJc";
			}
		}

		// Token: 0x17009205 RID: 37381
		// (get) Token: 0x0601A44C RID: 107596 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009206 RID: 37382
		// (get) Token: 0x0601A44D RID: 107597 RVA: 0x0035FC73 File Offset: 0x0035DE73
		internal override int ElementTypeId
		{
			get
			{
				return 11871;
			}
		}

		// Token: 0x0601A44E RID: 107598 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009207 RID: 37383
		// (get) Token: 0x0601A44F RID: 107599 RVA: 0x0035FC7A File Offset: 0x0035DE7A
		internal override string[] AttributeTagNames
		{
			get
			{
				return LevelJustification.attributeTagNames;
			}
		}

		// Token: 0x17009208 RID: 37384
		// (get) Token: 0x0601A450 RID: 107600 RVA: 0x0035FC81 File Offset: 0x0035DE81
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LevelJustification.attributeNamespaceIds;
			}
		}

		// Token: 0x17009209 RID: 37385
		// (get) Token: 0x0601A451 RID: 107601 RVA: 0x0035FC88 File Offset: 0x0035DE88
		// (set) Token: 0x0601A452 RID: 107602 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<LevelJustificationValues> Val
		{
			get
			{
				return (EnumValue<LevelJustificationValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A454 RID: 107604 RVA: 0x0035FC97 File Offset: 0x0035DE97
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<LevelJustificationValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A455 RID: 107605 RVA: 0x0035FCB9 File Offset: 0x0035DEB9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LevelJustification>(deep);
		}

		// Token: 0x0400AC86 RID: 44166
		private const string tagName = "lvlJc";

		// Token: 0x0400AC87 RID: 44167
		private const byte tagNsId = 23;

		// Token: 0x0400AC88 RID: 44168
		internal const int ElementTypeIdConst = 11871;

		// Token: 0x0400AC89 RID: 44169
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AC8A RID: 44170
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
