using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F0D RID: 12045
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingRestart : OpenXmlLeafElement
	{
		// Token: 0x17008E1E RID: 36382
		// (get) Token: 0x06019B93 RID: 105363 RVA: 0x00354538 File Offset: 0x00352738
		public override string LocalName
		{
			get
			{
				return "numRestart";
			}
		}

		// Token: 0x17008E1F RID: 36383
		// (get) Token: 0x06019B94 RID: 105364 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E20 RID: 36384
		// (get) Token: 0x06019B95 RID: 105365 RVA: 0x0035453F File Offset: 0x0035273F
		internal override int ElementTypeId
		{
			get
			{
				return 11683;
			}
		}

		// Token: 0x06019B96 RID: 105366 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008E21 RID: 36385
		// (get) Token: 0x06019B97 RID: 105367 RVA: 0x00354546 File Offset: 0x00352746
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberingRestart.attributeTagNames;
			}
		}

		// Token: 0x17008E22 RID: 36386
		// (get) Token: 0x06019B98 RID: 105368 RVA: 0x0035454D File Offset: 0x0035274D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberingRestart.attributeNamespaceIds;
			}
		}

		// Token: 0x17008E23 RID: 36387
		// (get) Token: 0x06019B99 RID: 105369 RVA: 0x00354554 File Offset: 0x00352754
		// (set) Token: 0x06019B9A RID: 105370 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<RestartNumberValues> Val
		{
			get
			{
				return (EnumValue<RestartNumberValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019B9C RID: 105372 RVA: 0x00354563 File Offset: 0x00352763
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<RestartNumberValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019B9D RID: 105373 RVA: 0x00354585 File Offset: 0x00352785
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingRestart>(deep);
		}

		// Token: 0x0400AA4B RID: 43595
		private const string tagName = "numRestart";

		// Token: 0x0400AA4C RID: 43596
		private const byte tagNsId = 23;

		// Token: 0x0400AA4D RID: 43597
		internal const int ElementTypeIdConst = 11683;

		// Token: 0x0400AA4E RID: 43598
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AA4F RID: 43599
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
