using System;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x020000A5 RID: 165
	public abstract class BaseXEvent<T>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000B560 File Offset: 0x0000B560
		public unsafe static bool IsEnabled
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				bool flag;
				if (BaseXEvent<T>.sm_pEnabledBitmap == null)
				{
					flag = false;
				}
				else
				{
					uint num = BaseXEvent<T>.sm_bitIndex;
					XBitmap<ExternalStorage>* ptr = BaseXEvent<T>.sm_pEnabledBitmap;
					ulong num2 = (ulong)(num >> 5);
					uint num3 = 1U << (int)num;
					flag = (*(num2 * 4UL + (ulong)(*ptr)) & (int)num3) != 0;
				}
				return (flag ? 1 : 0) != 0;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B348 File Offset: 0x0000B348
		public void Publish()
		{
			BaseXEvent<T>.sm_Publish(this);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B368 File Offset: 0x0000B368
		internal static void SetPublishDelegate(PublishDelegate publisher)
		{
			BaseXEvent<T>.sm_Publish = publisher;
			BaseXEvent<T>.sm_isPublishSet = true;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B388 File Offset: 0x0000B388
		[return: MarshalAs(UnmanagedType.U1)]
		internal static bool IsPublishSet()
		{
			return BaseXEvent<T>.sm_isPublishSet;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B3A0 File Offset: 0x0000B3A0
		internal unsafe static void Init(uint bitIndex, XBitmap<ExternalStorage>* pBitmap)
		{
			BaseXEvent<T>.sm_pEnabledBitmap = pBitmap;
			BaseXEvent<T>.sm_bitIndex = bitIndex;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B3C0 File Offset: 0x0000B3C0
		private static void DefaultPublish(object evt)
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00003B34 File Offset: 0x00003B34
		public BaseXEvent()
		{
		}

		// Token: 0x04000190 RID: 400
		private static PublishDelegate sm_Publish = new PublishDelegate(BaseXEvent<T>.DefaultPublish);

		// Token: 0x04000191 RID: 401
		private static uint sm_bitIndex = 0U;

		// Token: 0x04000192 RID: 402
		private unsafe static XBitmap<ExternalStorage>* sm_pEnabledBitmap = null;

		// Token: 0x04000193 RID: 403
		private static bool sm_isPublishSet = false;

		// Token: 0x04000194 RID: 404
		private static bool sm_assemblyRegistarRunning = XEventPackageRegistrar.IsAssemblyHandlerRegistered;
	}
}
