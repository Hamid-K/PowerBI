using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028E5 RID: 10469
	[GeneratedCode("DomGen", "2.0")]
	internal class Theater : OpenXmlLeafTextElement
	{
		// Token: 0x17006957 RID: 26967
		// (get) Token: 0x06014A1C RID: 84508 RVA: 0x00315158 File Offset: 0x00313358
		public override string LocalName
		{
			get
			{
				return "Theater";
			}
		}

		// Token: 0x17006958 RID: 26968
		// (get) Token: 0x06014A1D RID: 84509 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006959 RID: 26969
		// (get) Token: 0x06014A1E RID: 84510 RVA: 0x0031515F File Offset: 0x0031335F
		internal override int ElementTypeId
		{
			get
			{
				return 10824;
			}
		}

		// Token: 0x06014A1F RID: 84511 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A20 RID: 84512 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Theater()
		{
		}

		// Token: 0x06014A21 RID: 84513 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Theater(string text)
			: base(text)
		{
		}

		// Token: 0x06014A22 RID: 84514 RVA: 0x00315168 File Offset: 0x00313368
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A23 RID: 84515 RVA: 0x00315183 File Offset: 0x00313383
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Theater>(deep);
		}

		// Token: 0x04008F3B RID: 36667
		private const string tagName = "Theater";

		// Token: 0x04008F3C RID: 36668
		private const byte tagNsId = 9;

		// Token: 0x04008F3D RID: 36669
		internal const int ElementTypeIdConst = 10824;
	}
}
