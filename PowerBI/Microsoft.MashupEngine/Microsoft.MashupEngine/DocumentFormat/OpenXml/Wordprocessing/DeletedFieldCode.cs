using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E5B RID: 11867
	[GeneratedCode("DomGen", "2.0")]
	internal class DeletedFieldCode : TextType
	{
		// Token: 0x17008A55 RID: 35413
		// (get) Token: 0x06019388 RID: 103304 RVA: 0x003479D8 File Offset: 0x00345BD8
		public override string LocalName
		{
			get
			{
				return "delInstrText";
			}
		}

		// Token: 0x17008A56 RID: 35414
		// (get) Token: 0x06019389 RID: 103305 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A57 RID: 35415
		// (get) Token: 0x0601938A RID: 103306 RVA: 0x003479DF File Offset: 0x00345BDF
		internal override int ElementTypeId
		{
			get
			{
				return 11547;
			}
		}

		// Token: 0x0601938B RID: 103307 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601938C RID: 103308 RVA: 0x0034793A File Offset: 0x00345B3A
		public DeletedFieldCode()
		{
		}

		// Token: 0x0601938D RID: 103309 RVA: 0x00347942 File Offset: 0x00345B42
		public DeletedFieldCode(string text)
			: base(text)
		{
		}

		// Token: 0x0601938E RID: 103310 RVA: 0x003479E8 File Offset: 0x00345BE8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601938F RID: 103311 RVA: 0x00347A03 File Offset: 0x00345C03
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DeletedFieldCode>(deep);
		}

		// Token: 0x0400A7AC RID: 42924
		private const string tagName = "delInstrText";

		// Token: 0x0400A7AD RID: 42925
		private const byte tagNsId = 23;

		// Token: 0x0400A7AE RID: 42926
		internal const int ElementTypeIdConst = 11547;
	}
}
