using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A0E RID: 10766
	[GeneratedCode("DomGen", "2.0")]
	internal class TimePercentage : OpenXmlLeafElement
	{
		// Token: 0x17006FA8 RID: 28584
		// (get) Token: 0x06015893 RID: 88211 RVA: 0x003204EB File Offset: 0x0031E6EB
		public override string LocalName
		{
			get
			{
				return "tmPct";
			}
		}

		// Token: 0x17006FA9 RID: 28585
		// (get) Token: 0x06015894 RID: 88212 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006FAA RID: 28586
		// (get) Token: 0x06015895 RID: 88213 RVA: 0x003204F2 File Offset: 0x0031E6F2
		internal override int ElementTypeId
		{
			get
			{
				return 12192;
			}
		}

		// Token: 0x06015896 RID: 88214 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006FAB RID: 28587
		// (get) Token: 0x06015897 RID: 88215 RVA: 0x003204F9 File Offset: 0x0031E6F9
		internal override string[] AttributeTagNames
		{
			get
			{
				return TimePercentage.attributeTagNames;
			}
		}

		// Token: 0x17006FAC RID: 28588
		// (get) Token: 0x06015898 RID: 88216 RVA: 0x00320500 File Offset: 0x0031E700
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TimePercentage.attributeNamespaceIds;
			}
		}

		// Token: 0x17006FAD RID: 28589
		// (get) Token: 0x06015899 RID: 88217 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601589A RID: 88218 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601589C RID: 88220 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601589D RID: 88221 RVA: 0x00320507 File Offset: 0x0031E707
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TimePercentage>(deep);
		}

		// Token: 0x0601589E RID: 88222 RVA: 0x00320510 File Offset: 0x0031E710
		// Note: this type is marked as 'beforefieldinit'.
		static TimePercentage()
		{
			byte[] array = new byte[1];
			TimePercentage.attributeNamespaceIds = array;
		}

		// Token: 0x040093C4 RID: 37828
		private const string tagName = "tmPct";

		// Token: 0x040093C5 RID: 37829
		private const byte tagNsId = 24;

		// Token: 0x040093C6 RID: 37830
		internal const int ElementTypeIdConst = 12192;

		// Token: 0x040093C7 RID: 37831
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040093C8 RID: 37832
		private static byte[] attributeNamespaceIds;
	}
}
