using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002938 RID: 10552
	[GeneratedCode("DomGen", "2.0")]
	internal class Application : OpenXmlLeafTextElement
	{
		// Token: 0x17006B23 RID: 27427
		// (get) Token: 0x06014E72 RID: 85618 RVA: 0x00318AB4 File Offset: 0x00316CB4
		public override string LocalName
		{
			get
			{
				return "Application";
			}
		}

		// Token: 0x17006B24 RID: 27428
		// (get) Token: 0x06014E73 RID: 85619 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B25 RID: 27429
		// (get) Token: 0x06014E74 RID: 85620 RVA: 0x00318ABB File Offset: 0x00316CBB
		internal override int ElementTypeId
		{
			get
			{
				return 11023;
			}
		}

		// Token: 0x06014E75 RID: 85621 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E76 RID: 85622 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Application()
		{
		}

		// Token: 0x06014E77 RID: 85623 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Application(string text)
			: base(text)
		{
		}

		// Token: 0x06014E78 RID: 85624 RVA: 0x00318AC4 File Offset: 0x00316CC4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014E79 RID: 85625 RVA: 0x00318ADF File Offset: 0x00316CDF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Application>(deep);
		}

		// Token: 0x04009082 RID: 36994
		private const string tagName = "Application";

		// Token: 0x04009083 RID: 36995
		private const byte tagNsId = 3;

		// Token: 0x04009084 RID: 36996
		internal const int ElementTypeIdConst = 11023;
	}
}
