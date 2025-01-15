using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BA1 RID: 11169
	[GeneratedCode("DomGen", "2.0")]
	internal class FontSize : OpenXmlLeafElement
	{
		// Token: 0x17007B45 RID: 31557
		// (get) Token: 0x06017288 RID: 94856 RVA: 0x0033352F File Offset: 0x0033172F
		public override string LocalName
		{
			get
			{
				return "sz";
			}
		}

		// Token: 0x17007B46 RID: 31558
		// (get) Token: 0x06017289 RID: 94857 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B47 RID: 31559
		// (get) Token: 0x0601728A RID: 94858 RVA: 0x00333536 File Offset: 0x00331736
		internal override int ElementTypeId
		{
			get
			{
				return 11144;
			}
		}

		// Token: 0x0601728B RID: 94859 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007B48 RID: 31560
		// (get) Token: 0x0601728C RID: 94860 RVA: 0x0033353D File Offset: 0x0033173D
		internal override string[] AttributeTagNames
		{
			get
			{
				return FontSize.attributeTagNames;
			}
		}

		// Token: 0x17007B49 RID: 31561
		// (get) Token: 0x0601728D RID: 94861 RVA: 0x00333544 File Offset: 0x00331744
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FontSize.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B4A RID: 31562
		// (get) Token: 0x0601728E RID: 94862 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x0601728F RID: 94863 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public DoubleValue Val
		{
			get
			{
				return (DoubleValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06017291 RID: 94865 RVA: 0x002F2E7D File Offset: 0x002F107D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017292 RID: 94866 RVA: 0x0033354B File Offset: 0x0033174B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontSize>(deep);
		}

		// Token: 0x06017293 RID: 94867 RVA: 0x00333554 File Offset: 0x00331754
		// Note: this type is marked as 'beforefieldinit'.
		static FontSize()
		{
			byte[] array = new byte[1];
			FontSize.attributeNamespaceIds = array;
		}

		// Token: 0x04009B56 RID: 39766
		private const string tagName = "sz";

		// Token: 0x04009B57 RID: 39767
		private const byte tagNsId = 22;

		// Token: 0x04009B58 RID: 39768
		internal const int ElementTypeIdConst = 11144;

		// Token: 0x04009B59 RID: 39769
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009B5A RID: 39770
		private static byte[] attributeNamespaceIds;
	}
}
