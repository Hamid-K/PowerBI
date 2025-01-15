using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002824 RID: 10276
	[GeneratedCode("DomGen", "2.0")]
	internal class SpacingPercent : OpenXmlLeafElement
	{
		// Token: 0x170065C9 RID: 26057
		// (get) Token: 0x060141F1 RID: 82417 RVA: 0x0030F93F File Offset: 0x0030DB3F
		public override string LocalName
		{
			get
			{
				return "spcPct";
			}
		}

		// Token: 0x170065CA RID: 26058
		// (get) Token: 0x060141F2 RID: 82418 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065CB RID: 26059
		// (get) Token: 0x060141F3 RID: 82419 RVA: 0x0030F946 File Offset: 0x0030DB46
		internal override int ElementTypeId
		{
			get
			{
				return 10309;
			}
		}

		// Token: 0x060141F4 RID: 82420 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170065CC RID: 26060
		// (get) Token: 0x060141F5 RID: 82421 RVA: 0x0030F94D File Offset: 0x0030DB4D
		internal override string[] AttributeTagNames
		{
			get
			{
				return SpacingPercent.attributeTagNames;
			}
		}

		// Token: 0x170065CD RID: 26061
		// (get) Token: 0x060141F6 RID: 82422 RVA: 0x0030F954 File Offset: 0x0030DB54
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SpacingPercent.attributeNamespaceIds;
			}
		}

		// Token: 0x170065CE RID: 26062
		// (get) Token: 0x060141F7 RID: 82423 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060141F8 RID: 82424 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public Int32Value Val
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

		// Token: 0x060141FA RID: 82426 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060141FB RID: 82427 RVA: 0x0030F95B File Offset: 0x0030DB5B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SpacingPercent>(deep);
		}

		// Token: 0x060141FC RID: 82428 RVA: 0x0030F964 File Offset: 0x0030DB64
		// Note: this type is marked as 'beforefieldinit'.
		static SpacingPercent()
		{
			byte[] array = new byte[1];
			SpacingPercent.attributeNamespaceIds = array;
		}

		// Token: 0x0400891B RID: 35099
		private const string tagName = "spcPct";

		// Token: 0x0400891C RID: 35100
		private const byte tagNsId = 10;

		// Token: 0x0400891D RID: 35101
		internal const int ElementTypeIdConst = 10309;

		// Token: 0x0400891E RID: 35102
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400891F RID: 35103
		private static byte[] attributeNamespaceIds;
	}
}
