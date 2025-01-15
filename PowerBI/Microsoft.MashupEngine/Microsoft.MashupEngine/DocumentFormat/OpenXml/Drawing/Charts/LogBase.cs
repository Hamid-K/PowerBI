using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025B5 RID: 9653
	[GeneratedCode("DomGen", "2.0")]
	internal class LogBase : OpenXmlLeafElement
	{
		// Token: 0x1700574C RID: 22348
		// (get) Token: 0x06012145 RID: 74053 RVA: 0x002F5554 File Offset: 0x002F3754
		public override string LocalName
		{
			get
			{
				return "logBase";
			}
		}

		// Token: 0x1700574D RID: 22349
		// (get) Token: 0x06012146 RID: 74054 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700574E RID: 22350
		// (get) Token: 0x06012147 RID: 74055 RVA: 0x002F555B File Offset: 0x002F375B
		internal override int ElementTypeId
		{
			get
			{
				return 10477;
			}
		}

		// Token: 0x06012148 RID: 74056 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700574F RID: 22351
		// (get) Token: 0x06012149 RID: 74057 RVA: 0x002F5562 File Offset: 0x002F3762
		internal override string[] AttributeTagNames
		{
			get
			{
				return LogBase.attributeTagNames;
			}
		}

		// Token: 0x17005750 RID: 22352
		// (get) Token: 0x0601214A RID: 74058 RVA: 0x002F5569 File Offset: 0x002F3769
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LogBase.attributeNamespaceIds;
			}
		}

		// Token: 0x17005751 RID: 22353
		// (get) Token: 0x0601214B RID: 74059 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x0601214C RID: 74060 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public DoubleValue Val
		{
			get
			{
				return (DoubleValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601214E RID: 74062 RVA: 0x002F2E7D File Offset: 0x002F107D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601214F RID: 74063 RVA: 0x002F5570 File Offset: 0x002F3770
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LogBase>(deep);
		}

		// Token: 0x06012150 RID: 74064 RVA: 0x002F557C File Offset: 0x002F377C
		// Note: this type is marked as 'beforefieldinit'.
		static LogBase()
		{
			byte[] array = new byte[1];
			LogBase.attributeNamespaceIds = array;
		}

		// Token: 0x04007E16 RID: 32278
		private const string tagName = "logBase";

		// Token: 0x04007E17 RID: 32279
		private const byte tagNsId = 11;

		// Token: 0x04007E18 RID: 32280
		internal const int ElementTypeIdConst = 10477;

		// Token: 0x04007E19 RID: 32281
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E1A RID: 32282
		private static byte[] attributeNamespaceIds;
	}
}
