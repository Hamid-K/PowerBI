using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F41 RID: 12097
	[GeneratedCode("DomGen", "2.0")]
	internal class TextBoxFormFieldType : OpenXmlLeafElement
	{
		// Token: 0x17008FB7 RID: 36791
		// (get) Token: 0x06019F4A RID: 106314 RVA: 0x0031CE60 File Offset: 0x0031B060
		public override string LocalName
		{
			get
			{
				return "type";
			}
		}

		// Token: 0x17008FB8 RID: 36792
		// (get) Token: 0x06019F4B RID: 106315 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FB9 RID: 36793
		// (get) Token: 0x06019F4C RID: 106316 RVA: 0x0035A3A2 File Offset: 0x003585A2
		internal override int ElementTypeId
		{
			get
			{
				return 11743;
			}
		}

		// Token: 0x06019F4D RID: 106317 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008FBA RID: 36794
		// (get) Token: 0x06019F4E RID: 106318 RVA: 0x0035A3A9 File Offset: 0x003585A9
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextBoxFormFieldType.attributeTagNames;
			}
		}

		// Token: 0x17008FBB RID: 36795
		// (get) Token: 0x06019F4F RID: 106319 RVA: 0x0035A3B0 File Offset: 0x003585B0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextBoxFormFieldType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008FBC RID: 36796
		// (get) Token: 0x06019F50 RID: 106320 RVA: 0x0035A3B7 File Offset: 0x003585B7
		// (set) Token: 0x06019F51 RID: 106321 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<TextBoxFormFieldValues> Val
		{
			get
			{
				return (EnumValue<TextBoxFormFieldValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019F53 RID: 106323 RVA: 0x0035A3C6 File Offset: 0x003585C6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<TextBoxFormFieldValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019F54 RID: 106324 RVA: 0x0035A3E8 File Offset: 0x003585E8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextBoxFormFieldType>(deep);
		}

		// Token: 0x0400AB17 RID: 43799
		private const string tagName = "type";

		// Token: 0x0400AB18 RID: 43800
		private const byte tagNsId = 23;

		// Token: 0x0400AB19 RID: 43801
		internal const int ElementTypeIdConst = 11743;

		// Token: 0x0400AB1A RID: 43802
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AB1B RID: 43803
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
