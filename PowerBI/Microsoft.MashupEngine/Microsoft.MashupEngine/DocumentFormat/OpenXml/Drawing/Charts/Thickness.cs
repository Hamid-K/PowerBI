using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025F6 RID: 9718
	[GeneratedCode("DomGen", "2.0")]
	internal class Thickness : OpenXmlLeafElement
	{
		// Token: 0x1700595E RID: 22878
		// (get) Token: 0x060125CC RID: 75212 RVA: 0x002FA35F File Offset: 0x002F855F
		public override string LocalName
		{
			get
			{
				return "thickness";
			}
		}

		// Token: 0x1700595F RID: 22879
		// (get) Token: 0x060125CD RID: 75213 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005960 RID: 22880
		// (get) Token: 0x060125CE RID: 75214 RVA: 0x002FA366 File Offset: 0x002F8566
		internal override int ElementTypeId
		{
			get
			{
				return 10564;
			}
		}

		// Token: 0x060125CF RID: 75215 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005961 RID: 22881
		// (get) Token: 0x060125D0 RID: 75216 RVA: 0x002FA36D File Offset: 0x002F856D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Thickness.attributeTagNames;
			}
		}

		// Token: 0x17005962 RID: 22882
		// (get) Token: 0x060125D1 RID: 75217 RVA: 0x002FA374 File Offset: 0x002F8574
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Thickness.attributeNamespaceIds;
			}
		}

		// Token: 0x17005963 RID: 22883
		// (get) Token: 0x060125D2 RID: 75218 RVA: 0x002DE388 File Offset: 0x002DC588
		// (set) Token: 0x060125D3 RID: 75219 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public ByteValue Val
		{
			get
			{
				return (ByteValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060125D5 RID: 75221 RVA: 0x002DE397 File Offset: 0x002DC597
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new ByteValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060125D6 RID: 75222 RVA: 0x002FA37B File Offset: 0x002F857B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Thickness>(deep);
		}

		// Token: 0x060125D7 RID: 75223 RVA: 0x002FA384 File Offset: 0x002F8584
		// Note: this type is marked as 'beforefieldinit'.
		static Thickness()
		{
			byte[] array = new byte[1];
			Thickness.attributeNamespaceIds = array;
		}

		// Token: 0x04007F3B RID: 32571
		private const string tagName = "thickness";

		// Token: 0x04007F3C RID: 32572
		private const byte tagNsId = 11;

		// Token: 0x04007F3D RID: 32573
		internal const int ElementTypeIdConst = 10564;

		// Token: 0x04007F3E RID: 32574
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007F3F RID: 32575
		private static byte[] attributeNamespaceIds;
	}
}
