using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002934 RID: 10548
	[GeneratedCode("DomGen", "2.0")]
	internal class Manager : OpenXmlLeafTextElement
	{
		// Token: 0x17006B17 RID: 27415
		// (get) Token: 0x06014E52 RID: 85586 RVA: 0x003189E4 File Offset: 0x00316BE4
		public override string LocalName
		{
			get
			{
				return "Manager";
			}
		}

		// Token: 0x17006B18 RID: 27416
		// (get) Token: 0x06014E53 RID: 85587 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B19 RID: 27417
		// (get) Token: 0x06014E54 RID: 85588 RVA: 0x003189EB File Offset: 0x00316BEB
		internal override int ElementTypeId
		{
			get
			{
				return 11000;
			}
		}

		// Token: 0x06014E55 RID: 85589 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E56 RID: 85590 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Manager()
		{
		}

		// Token: 0x06014E57 RID: 85591 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Manager(string text)
			: base(text)
		{
		}

		// Token: 0x06014E58 RID: 85592 RVA: 0x003189F4 File Offset: 0x00316BF4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014E59 RID: 85593 RVA: 0x00318A0F File Offset: 0x00316C0F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Manager>(deep);
		}

		// Token: 0x04009076 RID: 36982
		private const string tagName = "Manager";

		// Token: 0x04009077 RID: 36983
		private const byte tagNsId = 3;

		// Token: 0x04009078 RID: 36984
		internal const int ElementTypeIdConst = 11000;
	}
}
