using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025AD RID: 9645
	[GeneratedCode("DomGen", "2.0")]
	internal class SplitType : OpenXmlLeafElement
	{
		// Token: 0x17005719 RID: 22297
		// (get) Token: 0x060120D7 RID: 73943 RVA: 0x002F5143 File Offset: 0x002F3343
		public override string LocalName
		{
			get
			{
				return "splitType";
			}
		}

		// Token: 0x1700571A RID: 22298
		// (get) Token: 0x060120D8 RID: 73944 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700571B RID: 22299
		// (get) Token: 0x060120D9 RID: 73945 RVA: 0x002F514A File Offset: 0x002F334A
		internal override int ElementTypeId
		{
			get
			{
				return 10463;
			}
		}

		// Token: 0x060120DA RID: 73946 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700571C RID: 22300
		// (get) Token: 0x060120DB RID: 73947 RVA: 0x002F5151 File Offset: 0x002F3351
		internal override string[] AttributeTagNames
		{
			get
			{
				return SplitType.attributeTagNames;
			}
		}

		// Token: 0x1700571D RID: 22301
		// (get) Token: 0x060120DC RID: 73948 RVA: 0x002F5158 File Offset: 0x002F3358
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SplitType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700571E RID: 22302
		// (get) Token: 0x060120DD RID: 73949 RVA: 0x002F515F File Offset: 0x002F335F
		// (set) Token: 0x060120DE RID: 73950 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<SplitValues> Val
		{
			get
			{
				return (EnumValue<SplitValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060120E0 RID: 73952 RVA: 0x002F516E File Offset: 0x002F336E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<SplitValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060120E1 RID: 73953 RVA: 0x002F518E File Offset: 0x002F338E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SplitType>(deep);
		}

		// Token: 0x060120E2 RID: 73954 RVA: 0x002F5198 File Offset: 0x002F3398
		// Note: this type is marked as 'beforefieldinit'.
		static SplitType()
		{
			byte[] array = new byte[1];
			SplitType.attributeNamespaceIds = array;
		}

		// Token: 0x04007DF0 RID: 32240
		private const string tagName = "splitType";

		// Token: 0x04007DF1 RID: 32241
		private const byte tagNsId = 11;

		// Token: 0x04007DF2 RID: 32242
		internal const int ElementTypeIdConst = 10463;

		// Token: 0x04007DF3 RID: 32243
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007DF4 RID: 32244
		private static byte[] attributeNamespaceIds;
	}
}
