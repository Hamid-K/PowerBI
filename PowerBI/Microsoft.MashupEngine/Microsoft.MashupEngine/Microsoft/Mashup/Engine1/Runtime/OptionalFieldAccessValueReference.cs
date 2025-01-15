using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012F9 RID: 4857
	internal sealed class OptionalFieldAccessValueReference : FieldAccessValueReference
	{
		// Token: 0x06008072 RID: 32882 RVA: 0x001B687C File Offset: 0x001B4A7C
		public OptionalFieldAccessValueReference(IValueReference reference, string field)
			: base(reference, field)
		{
		}

		// Token: 0x06008073 RID: 32883 RVA: 0x001B68A4 File Offset: 0x001B4AA4
		protected override Value GetValue()
		{
			Value value = this.reference.Value;
			if (value.IsNull)
			{
				return Value.Null;
			}
			if (value.AsRecord.TryGetValue(this.field, out value))
			{
				return value;
			}
			return Value.Null;
		}
	}
}
