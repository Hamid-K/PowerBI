using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F00 RID: 12032
	[GeneratedCode("DomGen", "2.0")]
	internal class DivId : OpenXmlLeafElement
	{
		// Token: 0x17008DC0 RID: 36288
		// (get) Token: 0x06019AD2 RID: 105170 RVA: 0x00353C48 File Offset: 0x00351E48
		public override string LocalName
		{
			get
			{
				return "divId";
			}
		}

		// Token: 0x17008DC1 RID: 36289
		// (get) Token: 0x06019AD3 RID: 105171 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DC2 RID: 36290
		// (get) Token: 0x06019AD4 RID: 105172 RVA: 0x00353C4F File Offset: 0x00351E4F
		internal override int ElementTypeId
		{
			get
			{
				return 11660;
			}
		}

		// Token: 0x06019AD5 RID: 105173 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008DC3 RID: 36291
		// (get) Token: 0x06019AD6 RID: 105174 RVA: 0x00353C56 File Offset: 0x00351E56
		internal override string[] AttributeTagNames
		{
			get
			{
				return DivId.attributeTagNames;
			}
		}

		// Token: 0x17008DC4 RID: 36292
		// (get) Token: 0x06019AD7 RID: 105175 RVA: 0x00353C5D File Offset: 0x00351E5D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DivId.attributeNamespaceIds;
			}
		}

		// Token: 0x17008DC5 RID: 36293
		// (get) Token: 0x06019AD8 RID: 105176 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019AD9 RID: 105177 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06019ADB RID: 105179 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019ADC RID: 105180 RVA: 0x00353C64 File Offset: 0x00351E64
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DivId>(deep);
		}

		// Token: 0x0400AA11 RID: 43537
		private const string tagName = "divId";

		// Token: 0x0400AA12 RID: 43538
		private const byte tagNsId = 23;

		// Token: 0x0400AA13 RID: 43539
		internal const int ElementTypeIdConst = 11660;

		// Token: 0x0400AA14 RID: 43540
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AA15 RID: 43541
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
