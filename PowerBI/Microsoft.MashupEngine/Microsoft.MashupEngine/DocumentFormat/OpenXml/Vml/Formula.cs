using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002258 RID: 8792
	[GeneratedCode("DomGen", "2.0")]
	internal class Formula : OpenXmlLeafElement
	{
		// Token: 0x17003CAC RID: 15532
		// (get) Token: 0x0600E743 RID: 59203 RVA: 0x002C81ED File Offset: 0x002C63ED
		public override string LocalName
		{
			get
			{
				return "f";
			}
		}

		// Token: 0x17003CAD RID: 15533
		// (get) Token: 0x0600E744 RID: 59204 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003CAE RID: 15534
		// (get) Token: 0x0600E745 RID: 59205 RVA: 0x002C81F4 File Offset: 0x002C63F4
		internal override int ElementTypeId
		{
			get
			{
				return 12528;
			}
		}

		// Token: 0x0600E746 RID: 59206 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003CAF RID: 15535
		// (get) Token: 0x0600E747 RID: 59207 RVA: 0x002C81FB File Offset: 0x002C63FB
		internal override string[] AttributeTagNames
		{
			get
			{
				return Formula.attributeTagNames;
			}
		}

		// Token: 0x17003CB0 RID: 15536
		// (get) Token: 0x0600E748 RID: 59208 RVA: 0x002C8202 File Offset: 0x002C6402
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Formula.attributeNamespaceIds;
			}
		}

		// Token: 0x17003CB1 RID: 15537
		// (get) Token: 0x0600E749 RID: 59209 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E74A RID: 59210 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "eqn")]
		public StringValue Equation
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0600E74C RID: 59212 RVA: 0x002C8209 File Offset: 0x002C6409
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "eqn" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E74D RID: 59213 RVA: 0x002C8229 File Offset: 0x002C6429
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Formula>(deep);
		}

		// Token: 0x0600E74E RID: 59214 RVA: 0x002C8234 File Offset: 0x002C6434
		// Note: this type is marked as 'beforefieldinit'.
		static Formula()
		{
			byte[] array = new byte[1];
			Formula.attributeNamespaceIds = array;
		}

		// Token: 0x04006F00 RID: 28416
		private const string tagName = "f";

		// Token: 0x04006F01 RID: 28417
		private const byte tagNsId = 26;

		// Token: 0x04006F02 RID: 28418
		internal const int ElementTypeIdConst = 12528;

		// Token: 0x04006F03 RID: 28419
		private static string[] attributeTagNames = new string[] { "eqn" };

		// Token: 0x04006F04 RID: 28420
		private static byte[] attributeNamespaceIds;
	}
}
