using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012E5 RID: 4837
	internal sealed class OptionalElementAccessValueReference : ElementAccessValueReference
	{
		// Token: 0x06008021 RID: 32801 RVA: 0x001B526E File Offset: 0x001B346E
		public OptionalElementAccessValueReference(IValueReference reference, int index)
			: base(reference, index)
		{
		}

		// Token: 0x06008022 RID: 32802 RVA: 0x001B5298 File Offset: 0x001B3498
		protected override Value GetValue()
		{
			Value value = this.reference.Value;
			if (value.IsNull)
			{
				return Value.Null;
			}
			ListValue asList = value.AsList;
			if (this.index < asList.Count)
			{
				return asList[this.index];
			}
			return Value.Null;
		}
	}
}
