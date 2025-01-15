using System;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x0200074E RID: 1870
	public sealed class ODataSchemaItem : IEquatable<ODataSchemaItem>, IComparable<ODataSchemaItem>
	{
		// Token: 0x06003753 RID: 14163 RVA: 0x000B0DFB File Offset: 0x000AEFFB
		public ODataSchemaItem(string name, string signature)
		{
			this.name = name;
			this.signature = signature;
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06003754 RID: 14164 RVA: 0x000B0E11 File Offset: 0x000AF011
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x000B0E19 File Offset: 0x000AF019
		public string Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000B0E21 File Offset: 0x000AF021
		public override int GetHashCode()
		{
			return this.name.GetHashCode() ^ this.signature.GetHashCode();
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000B0E3C File Offset: 0x000AF03C
		public override bool Equals(object obj)
		{
			ODataSchemaItem odataSchemaItem = obj as ODataSchemaItem;
			return odataSchemaItem != null && this.Equals(odataSchemaItem);
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000B0E5C File Offset: 0x000AF05C
		public bool Equals(ODataSchemaItem other)
		{
			return other != null && this.name == other.name && this.signature == other.signature;
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000B0E8C File Offset: 0x000AF08C
		public int CompareTo(ODataSchemaItem other)
		{
			int num = string.Compare(this.name, other.name, StringComparison.InvariantCulture);
			if (num != 0)
			{
				return num;
			}
			return string.Compare(this.signature, other.signature, StringComparison.InvariantCulture);
		}

		// Token: 0x04001C86 RID: 7302
		public const string TableSignature = "table";

		// Token: 0x04001C87 RID: 7303
		public const string RecordSignature = "record";

		// Token: 0x04001C88 RID: 7304
		public const string SingletonSignature = "singleton";

		// Token: 0x04001C89 RID: 7305
		private readonly string name;

		// Token: 0x04001C8A RID: 7306
		private readonly string signature;
	}
}
