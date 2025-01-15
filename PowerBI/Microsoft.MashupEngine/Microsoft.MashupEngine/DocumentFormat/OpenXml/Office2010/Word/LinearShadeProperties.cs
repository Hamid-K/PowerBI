using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002498 RID: 9368
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class LinearShadeProperties : OpenXmlLeafElement
	{
		// Token: 0x170051A3 RID: 20899
		// (get) Token: 0x060114B9 RID: 70841 RVA: 0x002ECE00 File Offset: 0x002EB000
		public override string LocalName
		{
			get
			{
				return "lin";
			}
		}

		// Token: 0x170051A4 RID: 20900
		// (get) Token: 0x060114BA RID: 70842 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051A5 RID: 20901
		// (get) Token: 0x060114BB RID: 70843 RVA: 0x002ECE07 File Offset: 0x002EB007
		internal override int ElementTypeId
		{
			get
			{
				return 12844;
			}
		}

		// Token: 0x060114BC RID: 70844 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170051A6 RID: 20902
		// (get) Token: 0x060114BD RID: 70845 RVA: 0x002ECE0E File Offset: 0x002EB00E
		internal override string[] AttributeTagNames
		{
			get
			{
				return LinearShadeProperties.attributeTagNames;
			}
		}

		// Token: 0x170051A7 RID: 20903
		// (get) Token: 0x060114BE RID: 70846 RVA: 0x002ECE15 File Offset: 0x002EB015
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LinearShadeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170051A8 RID: 20904
		// (get) Token: 0x060114BF RID: 70847 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060114C0 RID: 70848 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "ang")]
		public Int32Value Angle
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

		// Token: 0x170051A9 RID: 20905
		// (get) Token: 0x060114C1 RID: 70849 RVA: 0x002ECE1C File Offset: 0x002EB01C
		// (set) Token: 0x060114C2 RID: 70850 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(52, "scaled")]
		public EnumValue<OnOffValues> Scaled
		{
			get
			{
				return (EnumValue<OnOffValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060114C4 RID: 70852 RVA: 0x002ECE2B File Offset: 0x002EB02B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "ang" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "scaled" == name)
			{
				return new EnumValue<OnOffValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060114C5 RID: 70853 RVA: 0x002ECE65 File Offset: 0x002EB065
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LinearShadeProperties>(deep);
		}

		// Token: 0x04007925 RID: 31013
		private const string tagName = "lin";

		// Token: 0x04007926 RID: 31014
		private const byte tagNsId = 52;

		// Token: 0x04007927 RID: 31015
		internal const int ElementTypeIdConst = 12844;

		// Token: 0x04007928 RID: 31016
		private static string[] attributeTagNames = new string[] { "ang", "scaled" };

		// Token: 0x04007929 RID: 31017
		private static byte[] attributeNamespaceIds = new byte[] { 52, 52 };
	}
}
