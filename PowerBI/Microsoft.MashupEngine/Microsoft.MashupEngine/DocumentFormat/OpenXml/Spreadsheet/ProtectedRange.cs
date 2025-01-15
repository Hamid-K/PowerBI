using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BE2 RID: 11234
	[GeneratedCode("DomGen", "2.0")]
	internal class ProtectedRange : OpenXmlLeafElement
	{
		// Token: 0x17007DD7 RID: 32215
		// (get) Token: 0x060177FD RID: 96253 RVA: 0x002EA114 File Offset: 0x002E8314
		public override string LocalName
		{
			get
			{
				return "protectedRange";
			}
		}

		// Token: 0x17007DD8 RID: 32216
		// (get) Token: 0x060177FE RID: 96254 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007DD9 RID: 32217
		// (get) Token: 0x060177FF RID: 96255 RVA: 0x00337997 File Offset: 0x00335B97
		internal override int ElementTypeId
		{
			get
			{
				return 11206;
			}
		}

		// Token: 0x06017800 RID: 96256 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007DDA RID: 32218
		// (get) Token: 0x06017801 RID: 96257 RVA: 0x0033799E File Offset: 0x00335B9E
		internal override string[] AttributeTagNames
		{
			get
			{
				return ProtectedRange.attributeTagNames;
			}
		}

		// Token: 0x17007DDB RID: 32219
		// (get) Token: 0x06017802 RID: 96258 RVA: 0x003379A5 File Offset: 0x00335BA5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ProtectedRange.attributeNamespaceIds;
			}
		}

		// Token: 0x17007DDC RID: 32220
		// (get) Token: 0x06017803 RID: 96259 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x06017804 RID: 96260 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "password")]
		public HexBinaryValue Password
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007DDD RID: 32221
		// (get) Token: 0x06017805 RID: 96261 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017806 RID: 96262 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "algorithmName")]
		public StringValue AlgorithmName
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007DDE RID: 32222
		// (get) Token: 0x06017807 RID: 96263 RVA: 0x002EA13F File Offset: 0x002E833F
		// (set) Token: 0x06017808 RID: 96264 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "hashValue")]
		public Base64BinaryValue HashValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007DDF RID: 32223
		// (get) Token: 0x06017809 RID: 96265 RVA: 0x002EA14E File Offset: 0x002E834E
		// (set) Token: 0x0601780A RID: 96266 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "saltValue")]
		public Base64BinaryValue SaltValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007DE0 RID: 32224
		// (get) Token: 0x0601780B RID: 96267 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x0601780C RID: 96268 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "spinCount")]
		public UInt32Value SpinCount
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007DE1 RID: 32225
		// (get) Token: 0x0601780D RID: 96269 RVA: 0x003379AC File Offset: 0x00335BAC
		// (set) Token: 0x0601780E RID: 96270 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "sqref")]
		public ListValue<StringValue> SequenceOfReferences
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007DE2 RID: 32226
		// (get) Token: 0x0601780F RID: 96271 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06017810 RID: 96272 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007DE3 RID: 32227
		// (get) Token: 0x06017811 RID: 96273 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x06017812 RID: 96274 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "securityDescriptor")]
		public StringValue SecurityDescriptor
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x06017814 RID: 96276 RVA: 0x003379BC File Offset: 0x00335BBC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "password" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "algorithmName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hashValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "saltValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "spinCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "sqref" == name)
			{
				return new ListValue<StringValue>();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "securityDescriptor" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017815 RID: 96277 RVA: 0x00337A81 File Offset: 0x00335C81
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ProtectedRange>(deep);
		}

		// Token: 0x06017816 RID: 96278 RVA: 0x00337A8C File Offset: 0x00335C8C
		// Note: this type is marked as 'beforefieldinit'.
		static ProtectedRange()
		{
			byte[] array = new byte[8];
			ProtectedRange.attributeNamespaceIds = array;
		}

		// Token: 0x04009C8A RID: 40074
		private const string tagName = "protectedRange";

		// Token: 0x04009C8B RID: 40075
		private const byte tagNsId = 22;

		// Token: 0x04009C8C RID: 40076
		internal const int ElementTypeIdConst = 11206;

		// Token: 0x04009C8D RID: 40077
		private static string[] attributeTagNames = new string[] { "password", "algorithmName", "hashValue", "saltValue", "spinCount", "sqref", "name", "securityDescriptor" };

		// Token: 0x04009C8E RID: 40078
		private static byte[] attributeNamespaceIds;
	}
}
