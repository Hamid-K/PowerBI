using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B9F RID: 11167
	[GeneratedCode("DomGen", "2.0")]
	internal class Underline : OpenXmlLeafElement
	{
		// Token: 0x17007B39 RID: 31545
		// (get) Token: 0x06017270 RID: 94832 RVA: 0x00333427 File Offset: 0x00331627
		public override string LocalName
		{
			get
			{
				return "u";
			}
		}

		// Token: 0x17007B3A RID: 31546
		// (get) Token: 0x06017271 RID: 94833 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B3B RID: 31547
		// (get) Token: 0x06017272 RID: 94834 RVA: 0x0033342E File Offset: 0x0033162E
		internal override int ElementTypeId
		{
			get
			{
				return 11142;
			}
		}

		// Token: 0x06017273 RID: 94835 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007B3C RID: 31548
		// (get) Token: 0x06017274 RID: 94836 RVA: 0x00333435 File Offset: 0x00331635
		internal override string[] AttributeTagNames
		{
			get
			{
				return Underline.attributeTagNames;
			}
		}

		// Token: 0x17007B3D RID: 31549
		// (get) Token: 0x06017275 RID: 94837 RVA: 0x0033343C File Offset: 0x0033163C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Underline.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B3E RID: 31550
		// (get) Token: 0x06017276 RID: 94838 RVA: 0x00333443 File Offset: 0x00331643
		// (set) Token: 0x06017277 RID: 94839 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<UnderlineValues> Val
		{
			get
			{
				return (EnumValue<UnderlineValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06017279 RID: 94841 RVA: 0x00333452 File Offset: 0x00331652
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<UnderlineValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601727A RID: 94842 RVA: 0x00333472 File Offset: 0x00331672
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Underline>(deep);
		}

		// Token: 0x0601727B RID: 94843 RVA: 0x0033347C File Offset: 0x0033167C
		// Note: this type is marked as 'beforefieldinit'.
		static Underline()
		{
			byte[] array = new byte[1];
			Underline.attributeNamespaceIds = array;
		}

		// Token: 0x04009B4C RID: 39756
		private const string tagName = "u";

		// Token: 0x04009B4D RID: 39757
		private const byte tagNsId = 22;

		// Token: 0x04009B4E RID: 39758
		internal const int ElementTypeIdConst = 11142;

		// Token: 0x04009B4F RID: 39759
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009B50 RID: 39760
		private static byte[] attributeNamespaceIds;
	}
}
