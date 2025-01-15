using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027A0 RID: 10144
	[GeneratedCode("DomGen", "2.0")]
	internal class BuildChart : OpenXmlLeafElement
	{
		// Token: 0x17006270 RID: 25200
		// (get) Token: 0x06013A2D RID: 80429 RVA: 0x0030A263 File Offset: 0x00308463
		public override string LocalName
		{
			get
			{
				return "bldChart";
			}
		}

		// Token: 0x17006271 RID: 25201
		// (get) Token: 0x06013A2E RID: 80430 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006272 RID: 25202
		// (get) Token: 0x06013A2F RID: 80431 RVA: 0x0030A26A File Offset: 0x0030846A
		internal override int ElementTypeId
		{
			get
			{
				return 10177;
			}
		}

		// Token: 0x06013A30 RID: 80432 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006273 RID: 25203
		// (get) Token: 0x06013A31 RID: 80433 RVA: 0x0030A271 File Offset: 0x00308471
		internal override string[] AttributeTagNames
		{
			get
			{
				return BuildChart.attributeTagNames;
			}
		}

		// Token: 0x17006274 RID: 25204
		// (get) Token: 0x06013A32 RID: 80434 RVA: 0x0030A278 File Offset: 0x00308478
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BuildChart.attributeNamespaceIds;
			}
		}

		// Token: 0x17006275 RID: 25205
		// (get) Token: 0x06013A33 RID: 80435 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013A34 RID: 80436 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17006276 RID: 25206
		// (get) Token: 0x06013A35 RID: 80437 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06013A36 RID: 80438 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "animBg")]
		public BooleanValue AnimateBackground
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

		// Token: 0x06013A38 RID: 80440 RVA: 0x0030A27F File Offset: 0x0030847F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bld" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "animBg" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013A39 RID: 80441 RVA: 0x0030A2B5 File Offset: 0x003084B5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BuildChart>(deep);
		}

		// Token: 0x06013A3A RID: 80442 RVA: 0x0030A2C0 File Offset: 0x003084C0
		// Note: this type is marked as 'beforefieldinit'.
		static BuildChart()
		{
			byte[] array = new byte[2];
			BuildChart.attributeNamespaceIds = array;
		}

		// Token: 0x04008712 RID: 34578
		private const string tagName = "bldChart";

		// Token: 0x04008713 RID: 34579
		private const byte tagNsId = 10;

		// Token: 0x04008714 RID: 34580
		internal const int ElementTypeIdConst = 10177;

		// Token: 0x04008715 RID: 34581
		private static string[] attributeTagNames = new string[] { "bld", "animBg" };

		// Token: 0x04008716 RID: 34582
		private static byte[] attributeNamespaceIds;
	}
}
