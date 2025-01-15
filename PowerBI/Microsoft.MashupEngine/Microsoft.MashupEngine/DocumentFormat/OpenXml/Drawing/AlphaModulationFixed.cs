using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200270E RID: 9998
	[GeneratedCode("DomGen", "2.0")]
	internal class AlphaModulationFixed : OpenXmlLeafElement
	{
		// Token: 0x17005ECB RID: 24267
		// (get) Token: 0x060131F6 RID: 78326 RVA: 0x00303E88 File Offset: 0x00302088
		public override string LocalName
		{
			get
			{
				return "alphaModFix";
			}
		}

		// Token: 0x17005ECC RID: 24268
		// (get) Token: 0x060131F7 RID: 78327 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005ECD RID: 24269
		// (get) Token: 0x060131F8 RID: 78328 RVA: 0x00303E8F File Offset: 0x0030208F
		internal override int ElementTypeId
		{
			get
			{
				return 10060;
			}
		}

		// Token: 0x060131F9 RID: 78329 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005ECE RID: 24270
		// (get) Token: 0x060131FA RID: 78330 RVA: 0x00303E96 File Offset: 0x00302096
		internal override string[] AttributeTagNames
		{
			get
			{
				return AlphaModulationFixed.attributeTagNames;
			}
		}

		// Token: 0x17005ECF RID: 24271
		// (get) Token: 0x060131FB RID: 78331 RVA: 0x00303E9D File Offset: 0x0030209D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AlphaModulationFixed.attributeNamespaceIds;
			}
		}

		// Token: 0x17005ED0 RID: 24272
		// (get) Token: 0x060131FC RID: 78332 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060131FD RID: 78333 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "amt")]
		public Int32Value Amount
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060131FF RID: 78335 RVA: 0x00303EA4 File Offset: 0x003020A4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "amt" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013200 RID: 78336 RVA: 0x00303EC4 File Offset: 0x003020C4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlphaModulationFixed>(deep);
		}

		// Token: 0x06013201 RID: 78337 RVA: 0x00303ED0 File Offset: 0x003020D0
		// Note: this type is marked as 'beforefieldinit'.
		static AlphaModulationFixed()
		{
			byte[] array = new byte[1];
			AlphaModulationFixed.attributeNamespaceIds = array;
		}

		// Token: 0x040084C7 RID: 33991
		private const string tagName = "alphaModFix";

		// Token: 0x040084C8 RID: 33992
		private const byte tagNsId = 10;

		// Token: 0x040084C9 RID: 33993
		internal const int ElementTypeIdConst = 10060;

		// Token: 0x040084CA RID: 33994
		private static string[] attributeTagNames = new string[] { "amt" };

		// Token: 0x040084CB RID: 33995
		private static byte[] attributeNamespaceIds;
	}
}
