using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F30 RID: 12080
	[GeneratedCode("DomGen", "2.0")]
	internal class FieldData : OpenXmlLeafTextElement
	{
		// Token: 0x17008F6A RID: 36714
		// (get) Token: 0x06019EA0 RID: 106144 RVA: 0x00359CE4 File Offset: 0x00357EE4
		public override string LocalName
		{
			get
			{
				return "fldData";
			}
		}

		// Token: 0x17008F6B RID: 36715
		// (get) Token: 0x06019EA1 RID: 106145 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F6C RID: 36716
		// (get) Token: 0x06019EA2 RID: 106146 RVA: 0x00359CEB File Offset: 0x00357EEB
		internal override int ElementTypeId
		{
			get
			{
				return 11724;
			}
		}

		// Token: 0x06019EA3 RID: 106147 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019EA4 RID: 106148 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FieldData()
		{
		}

		// Token: 0x06019EA5 RID: 106149 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FieldData(string text)
			: base(text)
		{
		}

		// Token: 0x06019EA6 RID: 106150 RVA: 0x00359CF4 File Offset: 0x00357EF4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Base64BinaryValue
			{
				InnerText = text
			};
		}

		// Token: 0x06019EA7 RID: 106151 RVA: 0x00359D0F File Offset: 0x00357F0F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FieldData>(deep);
		}

		// Token: 0x0400AADA RID: 43738
		private const string tagName = "fldData";

		// Token: 0x0400AADB RID: 43739
		private const byte tagNsId = 23;

		// Token: 0x0400AADC RID: 43740
		internal const int ElementTypeIdConst = 11724;
	}
}
