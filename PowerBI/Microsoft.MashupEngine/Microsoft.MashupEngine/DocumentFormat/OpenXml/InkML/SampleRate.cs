using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200308F RID: 12431
	[GeneratedCode("DomGen", "2.0")]
	internal class SampleRate : OpenXmlLeafElement
	{
		// Token: 0x17009796 RID: 38806
		// (get) Token: 0x0601B05D RID: 110685 RVA: 0x0036ACF7 File Offset: 0x00368EF7
		public override string LocalName
		{
			get
			{
				return "sampleRate";
			}
		}

		// Token: 0x17009797 RID: 38807
		// (get) Token: 0x0601B05E RID: 110686 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009798 RID: 38808
		// (get) Token: 0x0601B05F RID: 110687 RVA: 0x0036ACFE File Offset: 0x00368EFE
		internal override int ElementTypeId
		{
			get
			{
				return 12652;
			}
		}

		// Token: 0x0601B060 RID: 110688 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009799 RID: 38809
		// (get) Token: 0x0601B061 RID: 110689 RVA: 0x0036AD05 File Offset: 0x00368F05
		internal override string[] AttributeTagNames
		{
			get
			{
				return SampleRate.attributeTagNames;
			}
		}

		// Token: 0x1700979A RID: 38810
		// (get) Token: 0x0601B062 RID: 110690 RVA: 0x0036AD0C File Offset: 0x00368F0C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SampleRate.attributeNamespaceIds;
			}
		}

		// Token: 0x1700979B RID: 38811
		// (get) Token: 0x0601B063 RID: 110691 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601B064 RID: 110692 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uniform")]
		public BooleanValue Uniform
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700979C RID: 38812
		// (get) Token: 0x0601B065 RID: 110693 RVA: 0x0036A078 File Offset: 0x00368278
		// (set) Token: 0x0601B066 RID: 110694 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "value")]
		public DecimalValue Value
		{
			get
			{
				return (DecimalValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601B068 RID: 110696 RVA: 0x0036AD13 File Offset: 0x00368F13
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uniform" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "value" == name)
			{
				return new DecimalValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B069 RID: 110697 RVA: 0x0036AD49 File Offset: 0x00368F49
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SampleRate>(deep);
		}

		// Token: 0x0601B06A RID: 110698 RVA: 0x0036AD54 File Offset: 0x00368F54
		// Note: this type is marked as 'beforefieldinit'.
		static SampleRate()
		{
			byte[] array = new byte[2];
			SampleRate.attributeNamespaceIds = array;
		}

		// Token: 0x0400B298 RID: 45720
		private const string tagName = "sampleRate";

		// Token: 0x0400B299 RID: 45721
		private const byte tagNsId = 43;

		// Token: 0x0400B29A RID: 45722
		internal const int ElementTypeIdConst = 12652;

		// Token: 0x0400B29B RID: 45723
		private static string[] attributeTagNames = new string[] { "uniform", "value" };

		// Token: 0x0400B29C RID: 45724
		private static byte[] attributeNamespaceIds;
	}
}
