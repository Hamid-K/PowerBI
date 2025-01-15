using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200091B RID: 2331
	public interface IMqConfig<out T>
	{
		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x06004280 RID: 17024
		MqConnectionParameters ConnectionParameters { get; }

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x06004281 RID: 17025
		IDictionary<MqFunctionOption, object> Options { get; }

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x06004282 RID: 17026
		IDictionary<MqColumn, object> Filters { get; }

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x06004283 RID: 17027
		long? BatchSize { get; }

		// Token: 0x06004284 RID: 17028
		T TransformMessage(Message message);

		// Token: 0x06004285 RID: 17029
		T TransformException(Exception exception);

		// Token: 0x06004286 RID: 17030
		bool TryWrapException(Exception exception, bool isOperationException, out Exception wrappedException);

		// Token: 0x06004287 RID: 17031
		Exception WrapException(Exception exception, bool isOperationException);
	}
}
