using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F33 RID: 7987
	public static class Variant
	{
		// Token: 0x0600C3A6 RID: 50086 RVA: 0x002730FE File Offset: 0x002712FE
		public unsafe static void Init(VARIANT* variant)
		{
			variant->vt = VARTYPE.EMPTY;
			variant->reserved1 = 0;
			variant->reserved2 = 0;
			variant->reserved3 = 0;
			variant->value64 = 0UL;
		}

		// Token: 0x0600C3A7 RID: 50087 RVA: 0x00273124 File Offset: 0x00271324
		internal unsafe static void SetValue(VARIANT* variant, ComHeap heap, object value)
		{
			if (value is int)
			{
				Variant.SetValue(variant, (int)value);
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
			throw new InvalidOperationException("Unsupported variant value: " + value.ToString());
		}

		// Token: 0x0600C3A8 RID: 50088 RVA: 0x00273186 File Offset: 0x00271386
		public unsafe static void SetEmpty(VARIANT* variant)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.EMPTY;
		}

		// Token: 0x0600C3A9 RID: 50089 RVA: 0x00273195 File Offset: 0x00271395
		public unsafe static void SetNull(VARIANT* variant)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.NULL;
		}

		// Token: 0x0600C3AA RID: 50090 RVA: 0x002731A4 File Offset: 0x002713A4
		public unsafe static void SetValue(VARIANT* variant, sbyte value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.I1;
			variant->value8 = (byte)value;
		}

		// Token: 0x0600C3AB RID: 50091 RVA: 0x002731BC File Offset: 0x002713BC
		public unsafe static void SetValue(VARIANT* variant, byte value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.UI1;
			variant->value8 = value;
		}

		// Token: 0x0600C3AC RID: 50092 RVA: 0x002731D3 File Offset: 0x002713D3
		public unsafe static void SetValue(VARIANT* variant, short value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.I2;
			variant->value16 = (ushort)value;
		}

		// Token: 0x0600C3AD RID: 50093 RVA: 0x002731EA File Offset: 0x002713EA
		public unsafe static void SetValue(VARIANT* variant, ushort value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.UI2;
			variant->value16 = value;
		}

		// Token: 0x0600C3AE RID: 50094 RVA: 0x00273201 File Offset: 0x00271401
		public unsafe static void SetValue(VARIANT* variant, int value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.I4;
			variant->value32 = (uint)value;
		}

		// Token: 0x0600C3AF RID: 50095 RVA: 0x00273217 File Offset: 0x00271417
		public unsafe static void SetValue(VARIANT* variant, uint value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.UI4;
			variant->value32 = value;
		}

		// Token: 0x0600C3B0 RID: 50096 RVA: 0x0027322E File Offset: 0x0027142E
		public unsafe static void SetValue(VARIANT* variant, long value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.I8;
			variant->value64 = (ulong)value;
		}

		// Token: 0x0600C3B1 RID: 50097 RVA: 0x00273245 File Offset: 0x00271445
		public unsafe static void SetValue(VARIANT* variant, ulong value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.UI8;
			variant->value64 = value;
		}

		// Token: 0x0600C3B2 RID: 50098 RVA: 0x0027325C File Offset: 0x0027145C
		public unsafe static void SetValue(VARIANT* variant, float value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.R4;
			variant->valueFloat = value;
		}

		// Token: 0x0600C3B3 RID: 50099 RVA: 0x00273272 File Offset: 0x00271472
		public unsafe static void SetValue(VARIANT* variant, double value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.R8;
			variant->valueDouble = value;
		}

		// Token: 0x0600C3B4 RID: 50100 RVA: 0x00273288 File Offset: 0x00271488
		public unsafe static void SetValue(VARIANT* variant, bool value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.BOOL;
			variant->value16 = (value ? ushort.MaxValue : 0);
		}

		// Token: 0x0600C3B5 RID: 50101 RVA: 0x002732AC File Offset: 0x002714AC
		public unsafe static void SetValue(VARIANT* variant, string value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.BSTR;
			variant->valuePtr = Marshal.StringToBSTR(value).ToPointer();
		}

		// Token: 0x0600C3B6 RID: 50102 RVA: 0x002732DA File Offset: 0x002714DA
		public unsafe static void SetValue(VARIANT* variant, DateTime value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.DATE;
			variant->valueDouble = value.ToOADate();
		}

		// Token: 0x0600C3B7 RID: 50103 RVA: 0x002732F6 File Offset: 0x002714F6
		public unsafe static void SetDate(VARIANT* variant, double value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.DATE;
			variant->valueDouble = value;
		}

		// Token: 0x0600C3B8 RID: 50104 RVA: 0x0027330C File Offset: 0x0027150C
		public unsafe static void SetValue(VARIANT* variant, decimal value)
		{
			variant->valueDecimal = *(DecimalBlittable*)(&value);
			variant->vt = VARTYPE.DECIMAL;
		}

		// Token: 0x0600C3B9 RID: 50105 RVA: 0x00273324 File Offset: 0x00271524
		public unsafe static decimal GetDecimal(VARIANT* variant)
		{
			VARIANT variant2 = *variant;
			variant2.vt = VARTYPE.EMPTY;
			return *(decimal*)(&variant2.valueDecimal);
		}

		// Token: 0x0600C3BA RID: 50106 RVA: 0x0027334D File Offset: 0x0027154D
		internal unsafe static void SetValue(VARIANT* variant, ComHeap heap, string value)
		{
			Variant.Init(variant);
			variant->vt = VARTYPE.BSTR;
			variant->valuePtr = (void*)heap.AllocBSTR(value);
		}

		// Token: 0x0600C3BB RID: 50107 RVA: 0x00273369 File Offset: 0x00271569
		public unsafe static void Clear(VARIANT* variant)
		{
			Variant.FreeAllocatedMemory(variant);
			Variant.Init(variant);
		}

		// Token: 0x0600C3BC RID: 50108 RVA: 0x00273377 File Offset: 0x00271577
		public unsafe static void FreeAllocatedMemory(VARIANT* variant)
		{
			if (variant->vt == VARTYPE.BSTR)
			{
				Marshal.FreeBSTR(new IntPtr(variant->valuePtr));
			}
		}
	}
}
