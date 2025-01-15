using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E5A RID: 11866
	[GeneratedCode("DomGen", "2.0")]
	internal class FieldCode : TextType
	{
		// Token: 0x17008A52 RID: 35410
		// (get) Token: 0x06019380 RID: 103296 RVA: 0x003479A4 File Offset: 0x00345BA4
		public override string LocalName
		{
			get
			{
				return "instrText";
			}
		}

		// Token: 0x17008A53 RID: 35411
		// (get) Token: 0x06019381 RID: 103297 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A54 RID: 35412
		// (get) Token: 0x06019382 RID: 103298 RVA: 0x003479AB File Offset: 0x00345BAB
		internal override int ElementTypeId
		{
			get
			{
				return 11546;
			}
		}

		// Token: 0x06019383 RID: 103299 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019384 RID: 103300 RVA: 0x0034793A File Offset: 0x00345B3A
		public FieldCode()
		{
		}

		// Token: 0x06019385 RID: 103301 RVA: 0x00347942 File Offset: 0x00345B42
		public FieldCode(string text)
			: base(text)
		{
		}

		// Token: 0x06019386 RID: 103302 RVA: 0x003479B4 File Offset: 0x00345BB4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06019387 RID: 103303 RVA: 0x003479CF File Offset: 0x00345BCF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FieldCode>(deep);
		}

		// Token: 0x0400A7A9 RID: 42921
		private const string tagName = "instrText";

		// Token: 0x0400A7AA RID: 42922
		private const byte tagNsId = 23;

		// Token: 0x0400A7AB RID: 42923
		internal const int ElementTypeIdConst = 11546;
	}
}
