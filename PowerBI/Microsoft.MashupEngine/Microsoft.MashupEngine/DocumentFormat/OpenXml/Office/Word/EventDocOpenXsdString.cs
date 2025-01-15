using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200246B RID: 9323
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocOpenXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x170050F4 RID: 20724
		// (get) Token: 0x06011312 RID: 70418 RVA: 0x002EB8B8 File Offset: 0x002E9AB8
		public override string LocalName
		{
			get
			{
				return "eventDocOpen";
			}
		}

		// Token: 0x170050F5 RID: 20725
		// (get) Token: 0x06011313 RID: 70419 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050F6 RID: 20726
		// (get) Token: 0x06011314 RID: 70420 RVA: 0x002EB8BF File Offset: 0x002E9ABF
		internal override int ElementTypeId
		{
			get
			{
				return 12550;
			}
		}

		// Token: 0x06011315 RID: 70421 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011316 RID: 70422 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocOpenXsdString()
		{
		}

		// Token: 0x06011317 RID: 70423 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocOpenXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011318 RID: 70424 RVA: 0x002EB8C8 File Offset: 0x002E9AC8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011319 RID: 70425 RVA: 0x002EB8E3 File Offset: 0x002E9AE3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocOpenXsdString>(deep);
		}

		// Token: 0x04007895 RID: 30869
		private const string tagName = "eventDocOpen";

		// Token: 0x04007896 RID: 30870
		private const byte tagNsId = 33;

		// Token: 0x04007897 RID: 30871
		internal const int ElementTypeIdConst = 12550;
	}
}
