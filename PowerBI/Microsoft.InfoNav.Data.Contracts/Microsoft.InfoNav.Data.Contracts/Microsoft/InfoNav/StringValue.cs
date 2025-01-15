using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x0200006C RID: 108
	[ImmutableObject(true)]
	public sealed class StringValue : DataValue<string>
	{
		// Token: 0x06000221 RID: 545 RVA: 0x000065F5 File Offset: 0x000047F5
		public StringValue(string value)
			: base(value)
		{
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000065FE File Offset: 0x000047FE
		internal override DataType Type
		{
			get
			{
				return DataType.Text;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006601 File Offset: 0x00004801
		public new static implicit operator StringValue(string value)
		{
			if (!(value == string.Empty))
			{
				return new StringValue(value);
			}
			return StringValue.Empty;
		}

		// Token: 0x04000164 RID: 356
		internal static readonly StringValue Empty = new StringValue(string.Empty);
	}
}
