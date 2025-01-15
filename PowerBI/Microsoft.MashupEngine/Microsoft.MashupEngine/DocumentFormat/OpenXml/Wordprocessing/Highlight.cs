using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E99 RID: 11929
	[GeneratedCode("DomGen", "2.0")]
	internal class Highlight : OpenXmlLeafElement
	{
		// Token: 0x17008B51 RID: 35665
		// (get) Token: 0x06019593 RID: 103827 RVA: 0x00306352 File Offset: 0x00304552
		public override string LocalName
		{
			get
			{
				return "highlight";
			}
		}

		// Token: 0x17008B52 RID: 35666
		// (get) Token: 0x06019594 RID: 103828 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B53 RID: 35667
		// (get) Token: 0x06019595 RID: 103829 RVA: 0x00348C51 File Offset: 0x00346E51
		internal override int ElementTypeId
		{
			get
			{
				return 11599;
			}
		}

		// Token: 0x06019596 RID: 103830 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008B54 RID: 35668
		// (get) Token: 0x06019597 RID: 103831 RVA: 0x00348C58 File Offset: 0x00346E58
		internal override string[] AttributeTagNames
		{
			get
			{
				return Highlight.attributeTagNames;
			}
		}

		// Token: 0x17008B55 RID: 35669
		// (get) Token: 0x06019598 RID: 103832 RVA: 0x00348C5F File Offset: 0x00346E5F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Highlight.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B56 RID: 35670
		// (get) Token: 0x06019599 RID: 103833 RVA: 0x00348C66 File Offset: 0x00346E66
		// (set) Token: 0x0601959A RID: 103834 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<HighlightColorValues> Val
		{
			get
			{
				return (EnumValue<HighlightColorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601959C RID: 103836 RVA: 0x00348C75 File Offset: 0x00346E75
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<HighlightColorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601959D RID: 103837 RVA: 0x00348C97 File Offset: 0x00346E97
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Highlight>(deep);
		}

		// Token: 0x0400A87C RID: 43132
		private const string tagName = "highlight";

		// Token: 0x0400A87D RID: 43133
		private const byte tagNsId = 23;

		// Token: 0x0400A87E RID: 43134
		internal const int ElementTypeIdConst = 11599;

		// Token: 0x0400A87F RID: 43135
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A880 RID: 43136
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
