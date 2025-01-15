using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012E6 RID: 4838
	public class EnumeratorFunctionValue : NativeFunctionValue0, IDisposable
	{
		// Token: 0x06008023 RID: 32803 RVA: 0x001B52E6 File Offset: 0x001B34E6
		public EnumeratorFunctionValue(IEnumerator<IValueReference> enumerator)
		{
			this.enumerator = enumerator;
			this.enumerated = false;
		}

		// Token: 0x06008024 RID: 32804 RVA: 0x001B52FC File Offset: 0x001B34FC
		public override Value Invoke()
		{
			if (this.enumerated)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.FunctionAlreadyEnumerated, null, null);
			}
			Value value;
			if (this.enumerator.MoveNext())
			{
				value = EnumeratorFunctionValue.CreateRecordValue(this.enumerator.Current, new EnumeratorFunctionValue(this.enumerator));
			}
			else
			{
				this.enumerator.Dispose();
				value = Value.Null;
			}
			this.enumerated = true;
			return value;
		}

		// Token: 0x06008025 RID: 32805 RVA: 0x001B5362 File Offset: 0x001B3562
		void IDisposable.Dispose()
		{
			this.enumerator.Dispose();
		}

		// Token: 0x06008026 RID: 32806 RVA: 0x001B536F File Offset: 0x001B356F
		private static Value CreateRecordValue(IValueReference element, FunctionValue next)
		{
			return RecordValue.New(EnumeratorFunctionValue.fields, new IValueReference[] { element, next });
		}

		// Token: 0x040045CA RID: 17866
		private static readonly Keys fields = Keys.New("Value", "Next");

		// Token: 0x040045CB RID: 17867
		private readonly IEnumerator<IValueReference> enumerator;

		// Token: 0x040045CC RID: 17868
		private bool enumerated;
	}
}
