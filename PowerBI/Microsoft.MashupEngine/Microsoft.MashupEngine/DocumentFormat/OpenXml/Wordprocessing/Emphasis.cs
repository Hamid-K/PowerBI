using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EAC RID: 11948
	[GeneratedCode("DomGen", "2.0")]
	internal class Emphasis : OpenXmlLeafElement
	{
		// Token: 0x17008BA6 RID: 35750
		// (get) Token: 0x0601963E RID: 103998 RVA: 0x0034921C File Offset: 0x0034741C
		public override string LocalName
		{
			get
			{
				return "em";
			}
		}

		// Token: 0x17008BA7 RID: 35751
		// (get) Token: 0x0601963F RID: 103999 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008BA8 RID: 35752
		// (get) Token: 0x06019640 RID: 104000 RVA: 0x00349223 File Offset: 0x00347423
		internal override int ElementTypeId
		{
			get
			{
				return 11607;
			}
		}

		// Token: 0x06019641 RID: 104001 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008BA9 RID: 35753
		// (get) Token: 0x06019642 RID: 104002 RVA: 0x0034922A File Offset: 0x0034742A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Emphasis.attributeTagNames;
			}
		}

		// Token: 0x17008BAA RID: 35754
		// (get) Token: 0x06019643 RID: 104003 RVA: 0x00349231 File Offset: 0x00347431
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Emphasis.attributeNamespaceIds;
			}
		}

		// Token: 0x17008BAB RID: 35755
		// (get) Token: 0x06019644 RID: 104004 RVA: 0x00349238 File Offset: 0x00347438
		// (set) Token: 0x06019645 RID: 104005 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<EmphasisMarkValues> Val
		{
			get
			{
				return (EnumValue<EmphasisMarkValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019647 RID: 104007 RVA: 0x00349247 File Offset: 0x00347447
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<EmphasisMarkValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019648 RID: 104008 RVA: 0x00349269 File Offset: 0x00347469
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Emphasis>(deep);
		}

		// Token: 0x0400A8BE RID: 43198
		private const string tagName = "em";

		// Token: 0x0400A8BF RID: 43199
		private const byte tagNsId = 23;

		// Token: 0x0400A8C0 RID: 43200
		internal const int ElementTypeIdConst = 11607;

		// Token: 0x0400A8C1 RID: 43201
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A8C2 RID: 43202
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
