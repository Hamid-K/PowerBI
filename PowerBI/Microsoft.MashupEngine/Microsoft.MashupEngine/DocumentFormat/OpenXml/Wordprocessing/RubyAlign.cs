using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F48 RID: 12104
	[GeneratedCode("DomGen", "2.0")]
	internal class RubyAlign : OpenXmlLeafElement
	{
		// Token: 0x17008FEF RID: 36847
		// (get) Token: 0x06019FBE RID: 106430 RVA: 0x0035A874 File Offset: 0x00358A74
		public override string LocalName
		{
			get
			{
				return "rubyAlign";
			}
		}

		// Token: 0x17008FF0 RID: 36848
		// (get) Token: 0x06019FBF RID: 106431 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FF1 RID: 36849
		// (get) Token: 0x06019FC0 RID: 106432 RVA: 0x0035A87B File Offset: 0x00358A7B
		internal override int ElementTypeId
		{
			get
			{
				return 11752;
			}
		}

		// Token: 0x06019FC1 RID: 106433 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008FF2 RID: 36850
		// (get) Token: 0x06019FC2 RID: 106434 RVA: 0x0035A882 File Offset: 0x00358A82
		internal override string[] AttributeTagNames
		{
			get
			{
				return RubyAlign.attributeTagNames;
			}
		}

		// Token: 0x17008FF3 RID: 36851
		// (get) Token: 0x06019FC3 RID: 106435 RVA: 0x0035A889 File Offset: 0x00358A89
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RubyAlign.attributeNamespaceIds;
			}
		}

		// Token: 0x17008FF4 RID: 36852
		// (get) Token: 0x06019FC4 RID: 106436 RVA: 0x0035A890 File Offset: 0x00358A90
		// (set) Token: 0x06019FC5 RID: 106437 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<RubyAlignValues> Val
		{
			get
			{
				return (EnumValue<RubyAlignValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019FC7 RID: 106439 RVA: 0x0035A89F File Offset: 0x00358A9F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<RubyAlignValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019FC8 RID: 106440 RVA: 0x0035A8C1 File Offset: 0x00358AC1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RubyAlign>(deep);
		}

		// Token: 0x0400AB3E RID: 43838
		private const string tagName = "rubyAlign";

		// Token: 0x0400AB3F RID: 43839
		private const byte tagNsId = 23;

		// Token: 0x0400AB40 RID: 43840
		internal const int ElementTypeIdConst = 11752;

		// Token: 0x0400AB41 RID: 43841
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AB42 RID: 43842
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
