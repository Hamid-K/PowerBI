using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002935 RID: 10549
	[GeneratedCode("DomGen", "2.0")]
	internal class Company : OpenXmlLeafTextElement
	{
		// Token: 0x17006B1A RID: 27418
		// (get) Token: 0x06014E5A RID: 85594 RVA: 0x00318A18 File Offset: 0x00316C18
		public override string LocalName
		{
			get
			{
				return "Company";
			}
		}

		// Token: 0x17006B1B RID: 27419
		// (get) Token: 0x06014E5B RID: 85595 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B1C RID: 27420
		// (get) Token: 0x06014E5C RID: 85596 RVA: 0x00318A1F File Offset: 0x00316C1F
		internal override int ElementTypeId
		{
			get
			{
				return 11001;
			}
		}

		// Token: 0x06014E5D RID: 85597 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E5E RID: 85598 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Company()
		{
		}

		// Token: 0x06014E5F RID: 85599 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Company(string text)
			: base(text)
		{
		}

		// Token: 0x06014E60 RID: 85600 RVA: 0x00318A28 File Offset: 0x00316C28
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014E61 RID: 85601 RVA: 0x00318A43 File Offset: 0x00316C43
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Company>(deep);
		}

		// Token: 0x04009079 RID: 36985
		private const string tagName = "Company";

		// Token: 0x0400907A RID: 36986
		private const byte tagNsId = 3;

		// Token: 0x0400907B RID: 36987
		internal const int ElementTypeIdConst = 11001;
	}
}
