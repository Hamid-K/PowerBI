using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002586 RID: 9606
	[GeneratedCode("DomGen", "2.0")]
	internal class LayoutTarget : OpenXmlLeafElement
	{
		// Token: 0x17005641 RID: 22081
		// (get) Token: 0x06011EFE RID: 73470 RVA: 0x002F3D64 File Offset: 0x002F1F64
		public override string LocalName
		{
			get
			{
				return "layoutTarget";
			}
		}

		// Token: 0x17005642 RID: 22082
		// (get) Token: 0x06011EFF RID: 73471 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005643 RID: 22083
		// (get) Token: 0x06011F00 RID: 73472 RVA: 0x002F3D6B File Offset: 0x002F1F6B
		internal override int ElementTypeId
		{
			get
			{
				return 10406;
			}
		}

		// Token: 0x06011F01 RID: 73473 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005644 RID: 22084
		// (get) Token: 0x06011F02 RID: 73474 RVA: 0x002F3D72 File Offset: 0x002F1F72
		internal override string[] AttributeTagNames
		{
			get
			{
				return LayoutTarget.attributeTagNames;
			}
		}

		// Token: 0x17005645 RID: 22085
		// (get) Token: 0x06011F03 RID: 73475 RVA: 0x002F3D79 File Offset: 0x002F1F79
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LayoutTarget.attributeNamespaceIds;
			}
		}

		// Token: 0x17005646 RID: 22086
		// (get) Token: 0x06011F04 RID: 73476 RVA: 0x002F3D80 File Offset: 0x002F1F80
		// (set) Token: 0x06011F05 RID: 73477 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<LayoutTargetValues> Val
		{
			get
			{
				return (EnumValue<LayoutTargetValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011F07 RID: 73479 RVA: 0x002F3D8F File Offset: 0x002F1F8F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<LayoutTargetValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011F08 RID: 73480 RVA: 0x002F3DAF File Offset: 0x002F1FAF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LayoutTarget>(deep);
		}

		// Token: 0x06011F09 RID: 73481 RVA: 0x002F3DB8 File Offset: 0x002F1FB8
		// Note: this type is marked as 'beforefieldinit'.
		static LayoutTarget()
		{
			byte[] array = new byte[1];
			LayoutTarget.attributeNamespaceIds = array;
		}

		// Token: 0x04007D53 RID: 32083
		private const string tagName = "layoutTarget";

		// Token: 0x04007D54 RID: 32084
		private const byte tagNsId = 11;

		// Token: 0x04007D55 RID: 32085
		internal const int ElementTypeIdConst = 10406;

		// Token: 0x04007D56 RID: 32086
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007D57 RID: 32087
		private static byte[] attributeNamespaceIds;
	}
}
