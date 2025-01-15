using System;
using System.Threading;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200003E RID: 62
	internal class SimpleEventTypes<T> : TraceLoggingEventTypes
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x0000C372 File Offset: 0x0000A572
		private SimpleEventTypes(TraceLoggingTypeInfo<T> typeInfo)
			: base(typeInfo.Name, typeInfo.Tags, new TraceLoggingTypeInfo[] { typeInfo })
		{
			this.typeInfo = typeInfo;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000C397 File Offset: 0x0000A597
		public static SimpleEventTypes<T> Instance
		{
			get
			{
				return SimpleEventTypes<T>.instance ?? SimpleEventTypes<T>.InitInstance();
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
		private static SimpleEventTypes<T> InitInstance()
		{
			SimpleEventTypes<T> simpleEventTypes = new SimpleEventTypes<T>(TraceLoggingTypeInfo<T>.Instance);
			Interlocked.CompareExchange<SimpleEventTypes<T>>(ref SimpleEventTypes<T>.instance, simpleEventTypes, null);
			return SimpleEventTypes<T>.instance;
		}

		// Token: 0x040000FD RID: 253
		private static SimpleEventTypes<T> instance;

		// Token: 0x040000FE RID: 254
		internal readonly TraceLoggingTypeInfo<T> typeInfo;
	}
}
