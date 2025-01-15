using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200009E RID: 158
	internal sealed class Identifier : IEquatable<Identifier>
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x000072BE File Offset: 0x000054BE
		internal Identifier(string value)
		{
			this.Value = value;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x000072CD File Offset: 0x000054CD
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x000072D5 File Offset: 0x000054D5
		public string Value { get; private set; }

		// Token: 0x060003B4 RID: 948 RVA: 0x000072DE File Offset: 0x000054DE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Identifier);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000072EC File Offset: 0x000054EC
		public bool Equals(Identifier other)
		{
			return !(other == null) && string.Equals(this.Value, other.Value, StringComparison.Ordinal);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000730B File Offset: 0x0000550B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00007318 File Offset: 0x00005518
		public override string ToString()
		{
			return "Identifier [" + this.Value + "]";
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000732F File Offset: 0x0000552F
		public static implicit operator Identifier(string value)
		{
			return new Identifier(value);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00007337 File Offset: 0x00005537
		public static bool operator ==(Identifier id1, Identifier id2)
		{
			return object.Equals(id1, id2);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00007340 File Offset: 0x00005540
		public static bool operator !=(Identifier id1, Identifier id2)
		{
			return !(id1 == id2);
		}
	}
}
