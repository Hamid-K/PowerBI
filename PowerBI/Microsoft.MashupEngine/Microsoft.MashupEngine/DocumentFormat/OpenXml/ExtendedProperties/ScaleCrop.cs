using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002946 RID: 10566
	[GeneratedCode("DomGen", "2.0")]
	internal class ScaleCrop : OpenXmlLeafTextElement
	{
		// Token: 0x17006B4D RID: 27469
		// (get) Token: 0x06014EE2 RID: 85730 RVA: 0x00318D7C File Offset: 0x00316F7C
		public override string LocalName
		{
			get
			{
				return "ScaleCrop";
			}
		}

		// Token: 0x17006B4E RID: 27470
		// (get) Token: 0x06014EE3 RID: 85731 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B4F RID: 27471
		// (get) Token: 0x06014EE4 RID: 85732 RVA: 0x00318D83 File Offset: 0x00316F83
		internal override int ElementTypeId
		{
			get
			{
				return 11013;
			}
		}

		// Token: 0x06014EE5 RID: 85733 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014EE6 RID: 85734 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ScaleCrop()
		{
		}

		// Token: 0x06014EE7 RID: 85735 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ScaleCrop(string text)
			: base(text)
		{
		}

		// Token: 0x06014EE8 RID: 85736 RVA: 0x00318D8C File Offset: 0x00316F8C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new BooleanValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014EE9 RID: 85737 RVA: 0x00318DA7 File Offset: 0x00316FA7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScaleCrop>(deep);
		}

		// Token: 0x040090AC RID: 37036
		private const string tagName = "ScaleCrop";

		// Token: 0x040090AD RID: 37037
		private const byte tagNsId = 3;

		// Token: 0x040090AE RID: 37038
		internal const int ElementTypeIdConst = 11013;
	}
}
