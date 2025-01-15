using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002981 RID: 10625
	[GeneratedCode("DomGen", "2.0")]
	internal class Transparent : OnOffType
	{
		// Token: 0x17006C8E RID: 27790
		// (get) Token: 0x060151AA RID: 86442 RVA: 0x0031B5FC File Offset: 0x003197FC
		public override string LocalName
		{
			get
			{
				return "transp";
			}
		}

		// Token: 0x17006C8F RID: 27791
		// (get) Token: 0x060151AB RID: 86443 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C90 RID: 27792
		// (get) Token: 0x060151AC RID: 86444 RVA: 0x0031B603 File Offset: 0x00319803
		internal override int ElementTypeId
		{
			get
			{
				return 10933;
			}
		}

		// Token: 0x060151AD RID: 86445 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060151AF RID: 86447 RVA: 0x0031B60A File Offset: 0x0031980A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transparent>(deep);
		}

		// Token: 0x0400918F RID: 37263
		private const string tagName = "transp";

		// Token: 0x04009190 RID: 37264
		private const byte tagNsId = 21;

		// Token: 0x04009191 RID: 37265
		internal const int ElementTypeIdConst = 10933;
	}
}
