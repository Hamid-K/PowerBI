using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021CB RID: 8651
	[GeneratedCode("DomGen", "2.0")]
	internal class Visible : OpenXmlLeafTextElement
	{
		// Token: 0x1700376E RID: 14190
		// (get) Token: 0x0600DC0A RID: 56330 RVA: 0x002BC9E4 File Offset: 0x002BABE4
		public override string LocalName
		{
			get
			{
				return "Visible";
			}
		}

		// Token: 0x1700376F RID: 14191
		// (get) Token: 0x0600DC0B RID: 56331 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003770 RID: 14192
		// (get) Token: 0x0600DC0C RID: 56332 RVA: 0x002BC9EB File Offset: 0x002BABEB
		internal override int ElementTypeId
		{
			get
			{
				return 12461;
			}
		}

		// Token: 0x0600DC0D RID: 56333 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC0E RID: 56334 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Visible()
		{
		}

		// Token: 0x0600DC0F RID: 56335 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Visible(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC10 RID: 56336 RVA: 0x002BC9F4 File Offset: 0x002BABF4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC11 RID: 56337 RVA: 0x002BCA0F File Offset: 0x002BAC0F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Visible>(deep);
		}

		// Token: 0x04006C95 RID: 27797
		private const string tagName = "Visible";

		// Token: 0x04006C96 RID: 27798
		private const byte tagNsId = 29;

		// Token: 0x04006C97 RID: 27799
		internal const int ElementTypeIdConst = 12461;
	}
}
