using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F32 RID: 12082
	[GeneratedCode("DomGen", "2.0")]
	internal class FormFieldName : OpenXmlLeafElement
	{
		// Token: 0x17008F70 RID: 36720
		// (get) Token: 0x06019EB2 RID: 106162 RVA: 0x002F15F0 File Offset: 0x002EF7F0
		public override string LocalName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x17008F71 RID: 36721
		// (get) Token: 0x06019EB3 RID: 106163 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F72 RID: 36722
		// (get) Token: 0x06019EB4 RID: 106164 RVA: 0x00359E2F File Offset: 0x0035802F
		internal override int ElementTypeId
		{
			get
			{
				return 11726;
			}
		}

		// Token: 0x06019EB5 RID: 106165 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008F73 RID: 36723
		// (get) Token: 0x06019EB6 RID: 106166 RVA: 0x00359E36 File Offset: 0x00358036
		internal override string[] AttributeTagNames
		{
			get
			{
				return FormFieldName.attributeTagNames;
			}
		}

		// Token: 0x17008F74 RID: 36724
		// (get) Token: 0x06019EB7 RID: 106167 RVA: 0x00359E3D File Offset: 0x0035803D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FormFieldName.attributeNamespaceIds;
			}
		}

		// Token: 0x17008F75 RID: 36725
		// (get) Token: 0x06019EB8 RID: 106168 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019EB9 RID: 106169 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019EBB RID: 106171 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019EBC RID: 106172 RVA: 0x00359E44 File Offset: 0x00358044
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormFieldName>(deep);
		}

		// Token: 0x0400AAE0 RID: 43744
		private const string tagName = "name";

		// Token: 0x0400AAE1 RID: 43745
		private const byte tagNsId = 23;

		// Token: 0x0400AAE2 RID: 43746
		internal const int ElementTypeIdConst = 11726;

		// Token: 0x0400AAE3 RID: 43747
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AAE4 RID: 43748
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
