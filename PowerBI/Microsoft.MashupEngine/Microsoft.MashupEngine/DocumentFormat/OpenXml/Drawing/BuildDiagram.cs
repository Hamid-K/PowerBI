using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200279F RID: 10143
	[GeneratedCode("DomGen", "2.0")]
	internal class BuildDiagram : OpenXmlLeafElement
	{
		// Token: 0x17006269 RID: 25193
		// (get) Token: 0x06013A1F RID: 80415 RVA: 0x0030A1CF File Offset: 0x003083CF
		public override string LocalName
		{
			get
			{
				return "bldDgm";
			}
		}

		// Token: 0x1700626A RID: 25194
		// (get) Token: 0x06013A20 RID: 80416 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700626B RID: 25195
		// (get) Token: 0x06013A21 RID: 80417 RVA: 0x0030A1D6 File Offset: 0x003083D6
		internal override int ElementTypeId
		{
			get
			{
				return 10176;
			}
		}

		// Token: 0x06013A22 RID: 80418 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700626C RID: 25196
		// (get) Token: 0x06013A23 RID: 80419 RVA: 0x0030A1DD File Offset: 0x003083DD
		internal override string[] AttributeTagNames
		{
			get
			{
				return BuildDiagram.attributeTagNames;
			}
		}

		// Token: 0x1700626D RID: 25197
		// (get) Token: 0x06013A24 RID: 80420 RVA: 0x0030A1E4 File Offset: 0x003083E4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BuildDiagram.attributeNamespaceIds;
			}
		}

		// Token: 0x1700626E RID: 25198
		// (get) Token: 0x06013A25 RID: 80421 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013A26 RID: 80422 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "bld")]
		public StringValue Build
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

		// Token: 0x1700626F RID: 25199
		// (get) Token: 0x06013A27 RID: 80423 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06013A28 RID: 80424 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "rev")]
		public BooleanValue ReverseAnimation
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06013A2A RID: 80426 RVA: 0x0030A1EB File Offset: 0x003083EB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bld" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "rev" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013A2B RID: 80427 RVA: 0x0030A221 File Offset: 0x00308421
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BuildDiagram>(deep);
		}

		// Token: 0x06013A2C RID: 80428 RVA: 0x0030A22C File Offset: 0x0030842C
		// Note: this type is marked as 'beforefieldinit'.
		static BuildDiagram()
		{
			byte[] array = new byte[2];
			BuildDiagram.attributeNamespaceIds = array;
		}

		// Token: 0x0400870D RID: 34573
		private const string tagName = "bldDgm";

		// Token: 0x0400870E RID: 34574
		private const byte tagNsId = 10;

		// Token: 0x0400870F RID: 34575
		internal const int ElementTypeIdConst = 10176;

		// Token: 0x04008710 RID: 34576
		private static string[] attributeTagNames = new string[] { "bld", "rev" };

		// Token: 0x04008711 RID: 34577
		private static byte[] attributeNamespaceIds;
	}
}
