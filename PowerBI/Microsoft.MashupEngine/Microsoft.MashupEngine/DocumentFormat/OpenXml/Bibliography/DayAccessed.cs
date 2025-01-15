using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028C9 RID: 10441
	[GeneratedCode("DomGen", "2.0")]
	internal class DayAccessed : OpenXmlLeafTextElement
	{
		// Token: 0x17006903 RID: 26883
		// (get) Token: 0x0601493C RID: 84284 RVA: 0x00314BA8 File Offset: 0x00312DA8
		public override string LocalName
		{
			get
			{
				return "DayAccessed";
			}
		}

		// Token: 0x17006904 RID: 26884
		// (get) Token: 0x0601493D RID: 84285 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006905 RID: 26885
		// (get) Token: 0x0601493E RID: 84286 RVA: 0x00314BAF File Offset: 0x00312DAF
		internal override int ElementTypeId
		{
			get
			{
				return 10795;
			}
		}

		// Token: 0x0601493F RID: 84287 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014940 RID: 84288 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public DayAccessed()
		{
		}

		// Token: 0x06014941 RID: 84289 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public DayAccessed(string text)
			: base(text)
		{
		}

		// Token: 0x06014942 RID: 84290 RVA: 0x00314BB8 File Offset: 0x00312DB8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014943 RID: 84291 RVA: 0x00314BD3 File Offset: 0x00312DD3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DayAccessed>(deep);
		}

		// Token: 0x04008EE7 RID: 36583
		private const string tagName = "DayAccessed";

		// Token: 0x04008EE8 RID: 36584
		private const byte tagNsId = 9;

		// Token: 0x04008EE9 RID: 36585
		internal const int ElementTypeIdConst = 10795;
	}
}
