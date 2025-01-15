using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200291B RID: 10523
	[GeneratedCode("DomGen", "2.0")]
	internal class VTInteger : OpenXmlLeafTextElement
	{
		// Token: 0x17006AAC RID: 27308
		// (get) Token: 0x06014D4B RID: 85323 RVA: 0x00317EAC File Offset: 0x003160AC
		public override string LocalName
		{
			get
			{
				return "int";
			}
		}

		// Token: 0x17006AAD RID: 27309
		// (get) Token: 0x06014D4C RID: 85324 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AAE RID: 27310
		// (get) Token: 0x06014D4D RID: 85325 RVA: 0x00317EB3 File Offset: 0x003160B3
		internal override int ElementTypeId
		{
			get
			{
				return 10974;
			}
		}

		// Token: 0x06014D4E RID: 85326 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D4F RID: 85327 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTInteger()
		{
		}

		// Token: 0x06014D50 RID: 85328 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTInteger(string text)
			: base(text)
		{
		}

		// Token: 0x06014D51 RID: 85329 RVA: 0x00317EBC File Offset: 0x003160BC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014D52 RID: 85330 RVA: 0x00317ED7 File Offset: 0x003160D7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTInteger>(deep);
		}

		// Token: 0x04009003 RID: 36867
		private const string tagName = "int";

		// Token: 0x04009004 RID: 36868
		private const byte tagNsId = 5;

		// Token: 0x04009005 RID: 36869
		internal const int ElementTypeIdConst = 10974;
	}
}
