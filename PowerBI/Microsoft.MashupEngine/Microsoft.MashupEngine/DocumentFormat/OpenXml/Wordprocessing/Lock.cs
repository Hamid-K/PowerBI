using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003004 RID: 12292
	[GeneratedCode("DomGen", "2.0")]
	internal class Lock : OpenXmlLeafElement
	{
		// Token: 0x1700962A RID: 38442
		// (get) Token: 0x0601AD18 RID: 109848 RVA: 0x002BEA3F File Offset: 0x002BCC3F
		public override string LocalName
		{
			get
			{
				return "lock";
			}
		}

		// Token: 0x1700962B RID: 38443
		// (get) Token: 0x0601AD19 RID: 109849 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700962C RID: 38444
		// (get) Token: 0x0601AD1A RID: 109850 RVA: 0x00368165 File Offset: 0x00366365
		internal override int ElementTypeId
		{
			get
			{
				return 12140;
			}
		}

		// Token: 0x0601AD1B RID: 109851 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700962D RID: 38445
		// (get) Token: 0x0601AD1C RID: 109852 RVA: 0x0036816C File Offset: 0x0036636C
		internal override string[] AttributeTagNames
		{
			get
			{
				return Lock.attributeTagNames;
			}
		}

		// Token: 0x1700962E RID: 38446
		// (get) Token: 0x0601AD1D RID: 109853 RVA: 0x00368173 File Offset: 0x00366373
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Lock.attributeNamespaceIds;
			}
		}

		// Token: 0x1700962F RID: 38447
		// (get) Token: 0x0601AD1E RID: 109854 RVA: 0x0036817A File Offset: 0x0036637A
		// (set) Token: 0x0601AD1F RID: 109855 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<LockingValues> Val
		{
			get
			{
				return (EnumValue<LockingValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601AD21 RID: 109857 RVA: 0x00368189 File Offset: 0x00366389
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<LockingValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AD22 RID: 109858 RVA: 0x003681AB File Offset: 0x003663AB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Lock>(deep);
		}

		// Token: 0x0400AE61 RID: 44641
		private const string tagName = "lock";

		// Token: 0x0400AE62 RID: 44642
		private const byte tagNsId = 23;

		// Token: 0x0400AE63 RID: 44643
		internal const int ElementTypeIdConst = 12140;

		// Token: 0x0400AE64 RID: 44644
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AE65 RID: 44645
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
