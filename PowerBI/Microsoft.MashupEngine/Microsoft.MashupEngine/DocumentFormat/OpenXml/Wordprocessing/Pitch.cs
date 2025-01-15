using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FB4 RID: 12212
	[GeneratedCode("DomGen", "2.0")]
	internal class Pitch : OpenXmlLeafElement
	{
		// Token: 0x17009395 RID: 37781
		// (get) Token: 0x0601A78E RID: 108430 RVA: 0x00362C80 File Offset: 0x00360E80
		public override string LocalName
		{
			get
			{
				return "pitch";
			}
		}

		// Token: 0x17009396 RID: 37782
		// (get) Token: 0x0601A78F RID: 108431 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009397 RID: 37783
		// (get) Token: 0x0601A790 RID: 108432 RVA: 0x00362C87 File Offset: 0x00360E87
		internal override int ElementTypeId
		{
			get
			{
				return 11920;
			}
		}

		// Token: 0x0601A791 RID: 108433 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009398 RID: 37784
		// (get) Token: 0x0601A792 RID: 108434 RVA: 0x00362C8E File Offset: 0x00360E8E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Pitch.attributeTagNames;
			}
		}

		// Token: 0x17009399 RID: 37785
		// (get) Token: 0x0601A793 RID: 108435 RVA: 0x00362C95 File Offset: 0x00360E95
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Pitch.attributeNamespaceIds;
			}
		}

		// Token: 0x1700939A RID: 37786
		// (get) Token: 0x0601A794 RID: 108436 RVA: 0x00362C9C File Offset: 0x00360E9C
		// (set) Token: 0x0601A795 RID: 108437 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<FontPitchValues> Val
		{
			get
			{
				return (EnumValue<FontPitchValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A797 RID: 108439 RVA: 0x00362CAB File Offset: 0x00360EAB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<FontPitchValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A798 RID: 108440 RVA: 0x00362CCD File Offset: 0x00360ECD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Pitch>(deep);
		}

		// Token: 0x0400AD19 RID: 44313
		private const string tagName = "pitch";

		// Token: 0x0400AD1A RID: 44314
		private const byte tagNsId = 23;

		// Token: 0x0400AD1B RID: 44315
		internal const int ElementTypeIdConst = 11920;

		// Token: 0x0400AD1C RID: 44316
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AD1D RID: 44317
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
