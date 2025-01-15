using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021FD RID: 8701
	[GeneratedCode("DomGen", "2.0")]
	internal class ScriptLocation : OpenXmlLeafTextElement
	{
		// Token: 0x17003804 RID: 14340
		// (get) Token: 0x0600DD9A RID: 56730 RVA: 0x002BD40C File Offset: 0x002BB60C
		public override string LocalName
		{
			get
			{
				return "ScriptLocation";
			}
		}

		// Token: 0x17003805 RID: 14341
		// (get) Token: 0x0600DD9B RID: 56731 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003806 RID: 14342
		// (get) Token: 0x0600DD9C RID: 56732 RVA: 0x002BD413 File Offset: 0x002BB613
		internal override int ElementTypeId
		{
			get
			{
				return 12502;
			}
		}

		// Token: 0x0600DD9D RID: 56733 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD9E RID: 56734 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ScriptLocation()
		{
		}

		// Token: 0x0600DD9F RID: 56735 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ScriptLocation(string text)
			: base(text)
		{
		}

		// Token: 0x0600DDA0 RID: 56736 RVA: 0x002BD41C File Offset: 0x002BB61C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DDA1 RID: 56737 RVA: 0x002BD437 File Offset: 0x002BB637
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScriptLocation>(deep);
		}

		// Token: 0x04006D2B RID: 27947
		private const string tagName = "ScriptLocation";

		// Token: 0x04006D2C RID: 27948
		private const byte tagNsId = 29;

		// Token: 0x04006D2D RID: 27949
		internal const int ElementTypeIdConst = 12502;
	}
}
