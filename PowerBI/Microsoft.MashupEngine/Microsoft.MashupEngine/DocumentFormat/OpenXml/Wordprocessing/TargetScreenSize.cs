using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F86 RID: 12166
	[GeneratedCode("DomGen", "2.0")]
	internal class TargetScreenSize : OpenXmlLeafElement
	{
		// Token: 0x170091A4 RID: 37284
		// (get) Token: 0x0601A381 RID: 107393 RVA: 0x0035F36C File Offset: 0x0035D56C
		public override string LocalName
		{
			get
			{
				return "targetScreenSz";
			}
		}

		// Token: 0x170091A5 RID: 37285
		// (get) Token: 0x0601A382 RID: 107394 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091A6 RID: 37286
		// (get) Token: 0x0601A383 RID: 107395 RVA: 0x0035F373 File Offset: 0x0035D573
		internal override int ElementTypeId
		{
			get
			{
				return 11846;
			}
		}

		// Token: 0x0601A384 RID: 107396 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170091A7 RID: 37287
		// (get) Token: 0x0601A385 RID: 107397 RVA: 0x0035F37A File Offset: 0x0035D57A
		internal override string[] AttributeTagNames
		{
			get
			{
				return TargetScreenSize.attributeTagNames;
			}
		}

		// Token: 0x170091A8 RID: 37288
		// (get) Token: 0x0601A386 RID: 107398 RVA: 0x0035F381 File Offset: 0x0035D581
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TargetScreenSize.attributeNamespaceIds;
			}
		}

		// Token: 0x170091A9 RID: 37289
		// (get) Token: 0x0601A387 RID: 107399 RVA: 0x0035F388 File Offset: 0x0035D588
		// (set) Token: 0x0601A388 RID: 107400 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<TargetScreenSizeValues> Val
		{
			get
			{
				return (EnumValue<TargetScreenSizeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A38A RID: 107402 RVA: 0x0035F397 File Offset: 0x0035D597
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<TargetScreenSizeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A38B RID: 107403 RVA: 0x0035F3B9 File Offset: 0x0035D5B9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TargetScreenSize>(deep);
		}

		// Token: 0x0400AC3F RID: 44095
		private const string tagName = "targetScreenSz";

		// Token: 0x0400AC40 RID: 44096
		private const byte tagNsId = 23;

		// Token: 0x0400AC41 RID: 44097
		internal const int ElementTypeIdConst = 11846;

		// Token: 0x0400AC42 RID: 44098
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AC43 RID: 44099
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
