using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F71 RID: 12145
	[GeneratedCode("DomGen", "2.0")]
	internal class MailMergeSource : OpenXmlLeafElement
	{
		// Token: 0x170090EF RID: 37103
		// (get) Token: 0x0601A1FD RID: 107005 RVA: 0x0031CE60 File Offset: 0x0031B060
		public override string LocalName
		{
			get
			{
				return "type";
			}
		}

		// Token: 0x170090F0 RID: 37104
		// (get) Token: 0x0601A1FE RID: 107006 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090F1 RID: 37105
		// (get) Token: 0x0601A1FF RID: 107007 RVA: 0x0035DB5C File Offset: 0x0035BD5C
		internal override int ElementTypeId
		{
			get
			{
				return 11808;
			}
		}

		// Token: 0x0601A200 RID: 107008 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170090F2 RID: 37106
		// (get) Token: 0x0601A201 RID: 107009 RVA: 0x0035DB63 File Offset: 0x0035BD63
		internal override string[] AttributeTagNames
		{
			get
			{
				return MailMergeSource.attributeTagNames;
			}
		}

		// Token: 0x170090F3 RID: 37107
		// (get) Token: 0x0601A202 RID: 107010 RVA: 0x0035DB6A File Offset: 0x0035BD6A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MailMergeSource.attributeNamespaceIds;
			}
		}

		// Token: 0x170090F4 RID: 37108
		// (get) Token: 0x0601A203 RID: 107011 RVA: 0x0035DB71 File Offset: 0x0035BD71
		// (set) Token: 0x0601A204 RID: 107012 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<MailMergeSourceValues> Val
		{
			get
			{
				return (EnumValue<MailMergeSourceValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A206 RID: 107014 RVA: 0x0035DB80 File Offset: 0x0035BD80
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<MailMergeSourceValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A207 RID: 107015 RVA: 0x0035DBA2 File Offset: 0x0035BDA2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MailMergeSource>(deep);
		}

		// Token: 0x0400ABEA RID: 44010
		private const string tagName = "type";

		// Token: 0x0400ABEB RID: 44011
		private const byte tagNsId = 23;

		// Token: 0x0400ABEC RID: 44012
		internal const int ElementTypeIdConst = 11808;

		// Token: 0x0400ABED RID: 44013
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ABEE RID: 44014
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
