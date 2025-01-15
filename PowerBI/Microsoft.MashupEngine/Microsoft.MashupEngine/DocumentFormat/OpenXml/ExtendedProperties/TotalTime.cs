using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002941 RID: 10561
	[GeneratedCode("DomGen", "2.0")]
	internal class TotalTime : OpenXmlLeafTextElement
	{
		// Token: 0x17006B3E RID: 27454
		// (get) Token: 0x06014EBA RID: 85690 RVA: 0x00318C78 File Offset: 0x00316E78
		public override string LocalName
		{
			get
			{
				return "TotalTime";
			}
		}

		// Token: 0x17006B3F RID: 27455
		// (get) Token: 0x06014EBB RID: 85691 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B40 RID: 27456
		// (get) Token: 0x06014EBC RID: 85692 RVA: 0x00318C7F File Offset: 0x00316E7F
		internal override int ElementTypeId
		{
			get
			{
				return 11010;
			}
		}

		// Token: 0x06014EBD RID: 85693 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014EBE RID: 85694 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public TotalTime()
		{
		}

		// Token: 0x06014EBF RID: 85695 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public TotalTime(string text)
			: base(text)
		{
		}

		// Token: 0x06014EC0 RID: 85696 RVA: 0x00318C88 File Offset: 0x00316E88
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014EC1 RID: 85697 RVA: 0x00318CA3 File Offset: 0x00316EA3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TotalTime>(deep);
		}

		// Token: 0x0400909D RID: 37021
		private const string tagName = "TotalTime";

		// Token: 0x0400909E RID: 37022
		private const byte tagNsId = 3;

		// Token: 0x0400909F RID: 37023
		internal const int ElementTypeIdConst = 11010;
	}
}
