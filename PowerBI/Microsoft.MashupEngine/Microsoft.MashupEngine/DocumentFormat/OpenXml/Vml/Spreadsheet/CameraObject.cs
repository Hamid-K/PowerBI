using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021D7 RID: 8663
	[GeneratedCode("DomGen", "2.0")]
	internal class CameraObject : OpenXmlLeafTextElement
	{
		// Token: 0x17003792 RID: 14226
		// (get) Token: 0x0600DC6A RID: 56426 RVA: 0x002BCC54 File Offset: 0x002BAE54
		public override string LocalName
		{
			get
			{
				return "Camera";
			}
		}

		// Token: 0x17003793 RID: 14227
		// (get) Token: 0x0600DC6B RID: 56427 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003794 RID: 14228
		// (get) Token: 0x0600DC6C RID: 56428 RVA: 0x002BCC5B File Offset: 0x002BAE5B
		internal override int ElementTypeId
		{
			get
			{
				return 12494;
			}
		}

		// Token: 0x0600DC6D RID: 56429 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC6E RID: 56430 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CameraObject()
		{
		}

		// Token: 0x0600DC6F RID: 56431 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CameraObject(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC70 RID: 56432 RVA: 0x002BCC64 File Offset: 0x002BAE64
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC71 RID: 56433 RVA: 0x002BCC7F File Offset: 0x002BAE7F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CameraObject>(deep);
		}

		// Token: 0x04006CB9 RID: 27833
		private const string tagName = "Camera";

		// Token: 0x04006CBA RID: 27834
		private const byte tagNsId = 29;

		// Token: 0x04006CBB RID: 27835
		internal const int ElementTypeIdConst = 12494;
	}
}
