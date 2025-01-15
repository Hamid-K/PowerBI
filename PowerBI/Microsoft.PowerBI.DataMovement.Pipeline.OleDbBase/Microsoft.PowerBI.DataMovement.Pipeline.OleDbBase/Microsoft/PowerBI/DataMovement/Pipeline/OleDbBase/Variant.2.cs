using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000CA RID: 202
	internal static class Variant
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0000A630 File Offset: 0x00008830
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public unsafe static object GetObject(VARIANT* variant)
		{
			object objectForNativeVariant = Marshal.GetObjectForNativeVariant((IntPtr)((void*)variant));
			if (variant->Type == VARTYPE.CY)
			{
				return new Currency((decimal)objectForNativeVariant);
			}
			return objectForNativeVariant;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000A664 File Offset: 0x00008864
		public unsafe static void Init(VARIANT* variant)
		{
			variant->Type = VARTYPE.EMPTY;
			variant->Reserved1 = 0;
			variant->Reserved2 = 0;
			variant->Reserved3 = 0;
			variant->Value64 = 0UL;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000A68C File Offset: 0x0000888C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public unsafe static void SetValue([global::System.Runtime.CompilerServices.Nullable(0)] VARIANT* variant, ComHeap heap, object value)
		{
			if (value is int)
			{
				Variant.SetValue(variant, (int)value);
				return;
			}
			if (value is short)
			{
				Variant.SetValue(variant, (short)value);
				return;
			}
			if (value is ushort)
			{
				Variant.SetValue(variant, (ushort)value);
				return;
			}
			if (value is bool)
			{
				Variant.SetValue(variant, (bool)value);
				return;
			}
			if (value is string)
			{
				Variant.SetValue(variant, heap, (string)value);
				return;
			}
			throw new InvalidOperationException(("Unsupported variant value: " + ((value != null) ? value.ToString() : null) != null) ? value.ToString() : "null");
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000A72D File Offset: 0x0000892D
		public unsafe static void SetNull(VARIANT* variant)
		{
			Variant.Init(variant);
			variant->Type = VARTYPE.NULL;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000A73C File Offset: 0x0000893C
		public unsafe static void SetValue(VARIANT* variant, int value)
		{
			Variant.Init(variant);
			variant->Type = VARTYPE.I4;
			variant->Value32 = (uint)value;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000A752 File Offset: 0x00008952
		public unsafe static void SetValue(VARIANT* variant, short value)
		{
			Variant.Init(variant);
			variant->Type = VARTYPE.I2;
			variant->Value32 = (uint)value;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000A768 File Offset: 0x00008968
		public unsafe static void SetValue(VARIANT* variant, ushort value)
		{
			Variant.Init(variant);
			variant->Type = VARTYPE.UI2;
			variant->Value32 = (uint)value;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000A77F File Offset: 0x0000897F
		public unsafe static void SetValue(VARIANT* variant, bool value)
		{
			Variant.Init(variant);
			variant->Type = VARTYPE.BOOL;
			variant->Value32 = (value ? 65535U : 0U);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000A7A0 File Offset: 0x000089A0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public unsafe static void SetValue([global::System.Runtime.CompilerServices.Nullable(0)] VARIANT* variant, ComHeap heap, string value)
		{
			Variant.Init(variant);
			variant->Type = VARTYPE.BSTR;
			variant->ValuePointer = (void*)heap.AllocBSTR(value);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000A7BC File Offset: 0x000089BC
		public unsafe static void Clear(VARIANT* variant)
		{
			if (variant->Type == VARTYPE.BSTR)
			{
				Marshal.FreeBSTR(new IntPtr(variant->ValuePointer));
			}
			Variant.Init(variant);
		}
	}
}
