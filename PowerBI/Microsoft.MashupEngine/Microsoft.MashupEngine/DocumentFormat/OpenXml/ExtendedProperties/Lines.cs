using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x0200293D RID: 10557
	[GeneratedCode("DomGen", "2.0")]
	internal class Lines : OpenXmlLeafTextElement
	{
		// Token: 0x17006B32 RID: 27442
		// (get) Token: 0x06014E9A RID: 85658 RVA: 0x000EF2F2 File Offset: 0x000ED4F2
		public override string LocalName
		{
			get
			{
				return "Lines";
			}
		}

		// Token: 0x17006B33 RID: 27443
		// (get) Token: 0x06014E9B RID: 85659 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B34 RID: 27444
		// (get) Token: 0x06014E9C RID: 85660 RVA: 0x00318BB0 File Offset: 0x00316DB0
		internal override int ElementTypeId
		{
			get
			{
				return 11006;
			}
		}

		// Token: 0x06014E9D RID: 85661 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E9E RID: 85662 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Lines()
		{
		}

		// Token: 0x06014E9F RID: 85663 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Lines(string text)
			: base(text)
		{
		}

		// Token: 0x06014EA0 RID: 85664 RVA: 0x00318BB8 File Offset: 0x00316DB8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014EA1 RID: 85665 RVA: 0x00318BD3 File Offset: 0x00316DD3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Lines>(deep);
		}

		// Token: 0x04009091 RID: 37009
		private const string tagName = "Lines";

		// Token: 0x04009092 RID: 37010
		private const byte tagNsId = 3;

		// Token: 0x04009093 RID: 37011
		internal const int ElementTypeIdConst = 11006;
	}
}
