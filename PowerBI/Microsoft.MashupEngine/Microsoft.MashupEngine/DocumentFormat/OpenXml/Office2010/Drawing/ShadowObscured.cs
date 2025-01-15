using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002347 RID: 9031
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ShadowObscured : OpenXmlLeafElement
	{
		// Token: 0x1700498A RID: 18826
		// (get) Token: 0x060102A0 RID: 66208 RVA: 0x002E067B File Offset: 0x002DE87B
		public override string LocalName
		{
			get
			{
				return "shadowObscured";
			}
		}

		// Token: 0x1700498B RID: 18827
		// (get) Token: 0x060102A1 RID: 66209 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x1700498C RID: 18828
		// (get) Token: 0x060102A2 RID: 66210 RVA: 0x002E0682 File Offset: 0x002DE882
		internal override int ElementTypeId
		{
			get
			{
				return 12716;
			}
		}

		// Token: 0x060102A3 RID: 66211 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700498D RID: 18829
		// (get) Token: 0x060102A4 RID: 66212 RVA: 0x002E0689 File Offset: 0x002DE889
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShadowObscured.attributeTagNames;
			}
		}

		// Token: 0x1700498E RID: 18830
		// (get) Token: 0x060102A5 RID: 66213 RVA: 0x002E0690 File Offset: 0x002DE890
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShadowObscured.attributeNamespaceIds;
			}
		}

		// Token: 0x1700498F RID: 18831
		// (get) Token: 0x060102A6 RID: 66214 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060102A7 RID: 66215 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public BooleanValue Val
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

		// Token: 0x060102A9 RID: 66217 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060102AA RID: 66218 RVA: 0x002E0697 File Offset: 0x002DE897
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShadowObscured>(deep);
		}

		// Token: 0x060102AB RID: 66219 RVA: 0x002E06A0 File Offset: 0x002DE8A0
		// Note: this type is marked as 'beforefieldinit'.
		static ShadowObscured()
		{
			byte[] array = new byte[1];
			ShadowObscured.attributeNamespaceIds = array;
		}

		// Token: 0x0400735E RID: 29534
		private const string tagName = "shadowObscured";

		// Token: 0x0400735F RID: 29535
		private const byte tagNsId = 48;

		// Token: 0x04007360 RID: 29536
		internal const int ElementTypeIdConst = 12716;

		// Token: 0x04007361 RID: 29537
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007362 RID: 29538
		private static byte[] attributeNamespaceIds;
	}
}
