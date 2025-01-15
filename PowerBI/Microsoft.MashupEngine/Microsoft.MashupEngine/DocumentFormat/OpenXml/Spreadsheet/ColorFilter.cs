using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CE1 RID: 11489
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorFilter : OpenXmlLeafElement
	{
		// Token: 0x17008613 RID: 34323
		// (get) Token: 0x06018AE0 RID: 101088 RVA: 0x00343EAF File Offset: 0x003420AF
		public override string LocalName
		{
			get
			{
				return "colorFilter";
			}
		}

		// Token: 0x17008614 RID: 34324
		// (get) Token: 0x06018AE1 RID: 101089 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008615 RID: 34325
		// (get) Token: 0x06018AE2 RID: 101090 RVA: 0x00343EB6 File Offset: 0x003420B6
		internal override int ElementTypeId
		{
			get
			{
				return 11471;
			}
		}

		// Token: 0x06018AE3 RID: 101091 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008616 RID: 34326
		// (get) Token: 0x06018AE4 RID: 101092 RVA: 0x00343EBD File Offset: 0x003420BD
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorFilter.attributeTagNames;
			}
		}

		// Token: 0x17008617 RID: 34327
		// (get) Token: 0x06018AE5 RID: 101093 RVA: 0x00343EC4 File Offset: 0x003420C4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorFilter.attributeNamespaceIds;
			}
		}

		// Token: 0x17008618 RID: 34328
		// (get) Token: 0x06018AE6 RID: 101094 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018AE7 RID: 101095 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dxfId")]
		public UInt32Value FormatId
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

		// Token: 0x17008619 RID: 34329
		// (get) Token: 0x06018AE8 RID: 101096 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06018AE9 RID: 101097 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cellColor")]
		public BooleanValue CellColor
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06018AEB RID: 101099 RVA: 0x00343ECB File Offset: 0x003420CB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "cellColor" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018AEC RID: 101100 RVA: 0x00343F01 File Offset: 0x00342101
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorFilter>(deep);
		}

		// Token: 0x06018AED RID: 101101 RVA: 0x00343F0C File Offset: 0x0034210C
		// Note: this type is marked as 'beforefieldinit'.
		static ColorFilter()
		{
			byte[] array = new byte[2];
			ColorFilter.attributeNamespaceIds = array;
		}

		// Token: 0x0400A146 RID: 41286
		private const string tagName = "colorFilter";

		// Token: 0x0400A147 RID: 41287
		private const byte tagNsId = 22;

		// Token: 0x0400A148 RID: 41288
		internal const int ElementTypeIdConst = 11471;

		// Token: 0x0400A149 RID: 41289
		private static string[] attributeTagNames = new string[] { "dxfId", "cellColor" };

		// Token: 0x0400A14A RID: 41290
		private static byte[] attributeNamespaceIds;
	}
}
