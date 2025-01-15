using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Microsoft.InfoNav
{
	// Token: 0x0200006A RID: 106
	[ImmutableObject(true)]
	public sealed class NullValue : DataValue
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000659D File Offset: 0x0000479D
		private NullValue()
		{
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000065A5 File Offset: 0x000047A5
		internal override DataType Type
		{
			get
			{
				return DataType.Null;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000065AC File Offset: 0x000047AC
		public override bool Equals(DataValue other)
		{
			return this == other;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000065B2 File Offset: 0x000047B2
		protected override int GetHashCodeCore()
		{
			return RuntimeHelpers.GetHashCode(this);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000065BA File Offset: 0x000047BA
		internal override object GetValueAsObject()
		{
			return null;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000065BD File Offset: 0x000047BD
		public override string ToString()
		{
			if (this != NullValue.Null)
			{
				return "empty";
			}
			return "null";
		}

		// Token: 0x04000162 RID: 354
		public static readonly NullValue Null = new NullValue();

		// Token: 0x04000163 RID: 355
		public static readonly NullValue Empty = new NullValue();
	}
}
