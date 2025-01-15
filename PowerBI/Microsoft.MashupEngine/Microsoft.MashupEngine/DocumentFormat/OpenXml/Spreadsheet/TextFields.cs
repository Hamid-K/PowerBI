using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B4C RID: 11084
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TextField))]
	internal class TextFields : OpenXmlCompositeElement
	{
		// Token: 0x1700781A RID: 30746
		// (get) Token: 0x06016B9D RID: 93085 RVA: 0x0032E613 File Offset: 0x0032C813
		public override string LocalName
		{
			get
			{
				return "textFields";
			}
		}

		// Token: 0x1700781B RID: 30747
		// (get) Token: 0x06016B9E RID: 93086 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700781C RID: 30748
		// (get) Token: 0x06016B9F RID: 93087 RVA: 0x0032E61A File Offset: 0x0032C81A
		internal override int ElementTypeId
		{
			get
			{
				return 11067;
			}
		}

		// Token: 0x06016BA0 RID: 93088 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700781D RID: 30749
		// (get) Token: 0x06016BA1 RID: 93089 RVA: 0x0032E621 File Offset: 0x0032C821
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextFields.attributeTagNames;
			}
		}

		// Token: 0x1700781E RID: 30750
		// (get) Token: 0x06016BA2 RID: 93090 RVA: 0x0032E628 File Offset: 0x0032C828
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextFields.attributeNamespaceIds;
			}
		}

		// Token: 0x1700781F RID: 30751
		// (get) Token: 0x06016BA3 RID: 93091 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016BA4 RID: 93092 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06016BA5 RID: 93093 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextFields()
		{
		}

		// Token: 0x06016BA6 RID: 93094 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextFields(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016BA7 RID: 93095 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextFields(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016BA8 RID: 93096 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextFields(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016BA9 RID: 93097 RVA: 0x0032E62F File Offset: 0x0032C82F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "textField" == name)
			{
				return new TextField();
			}
			return null;
		}

		// Token: 0x06016BAA RID: 93098 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016BAB RID: 93099 RVA: 0x0032E64A File Offset: 0x0032C84A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextFields>(deep);
		}

		// Token: 0x06016BAC RID: 93100 RVA: 0x0032E654 File Offset: 0x0032C854
		// Note: this type is marked as 'beforefieldinit'.
		static TextFields()
		{
			byte[] array = new byte[1];
			TextFields.attributeNamespaceIds = array;
		}

		// Token: 0x040099B8 RID: 39352
		private const string tagName = "textFields";

		// Token: 0x040099B9 RID: 39353
		private const byte tagNsId = 22;

		// Token: 0x040099BA RID: 39354
		internal const int ElementTypeIdConst = 11067;

		// Token: 0x040099BB RID: 39355
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x040099BC RID: 39356
		private static byte[] attributeNamespaceIds;
	}
}
