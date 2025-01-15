using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.Marshallers
{
	// Token: 0x02001FCD RID: 8141
	public class CustomVariantMarshaller : IVariantMarshaller, IMarshaller<object>, IMarshaller
	{
		// Token: 0x17003031 RID: 12337
		// (get) Token: 0x0600C6EF RID: 50927 RVA: 0x0027A144 File Offset: 0x00278344
		public unsafe int NativeSizeInBytes
		{
			get
			{
				return sizeof(VARIANT);
			}
		}

		// Token: 0x17003032 RID: 12338
		// (get) Token: 0x0600C6F0 RID: 50928 RVA: 0x001422C0 File Offset: 0x001404C0
		public VARTYPE Type
		{
			get
			{
				return VARTYPE.VARIANT;
			}
		}

		// Token: 0x0600C6F1 RID: 50929 RVA: 0x00002139 File Offset: 0x00000339
		public bool CanHandle(VARTYPE variantType)
		{
			return true;
		}

		// Token: 0x0600C6F2 RID: 50930 RVA: 0x0027A14C File Offset: 0x0027834C
		public unsafe object GetManaged(IntPtr native)
		{
			VARIANT* ptr = (VARIANT*)(void*)native;
			switch (ptr->vt)
			{
			case VARTYPE.EMPTY:
				return null;
			case VARTYPE.NULL:
				return DBNull.Value;
			case VARTYPE.I2:
				return (short)ptr->value16;
			case VARTYPE.I4:
			case VARTYPE.ERROR:
			case VARTYPE.INT:
				return (int)ptr->value32;
			case VARTYPE.R4:
				return ptr->valueFloat;
			case VARTYPE.R8:
				return ptr->valueDouble;
			case VARTYPE.CY:
				return decimal.FromOACurrency((long)ptr->value64);
			case VARTYPE.DATE:
				return DateTime.FromOADate(ptr->valueDouble);
			case VARTYPE.BSTR:
				return Marshal.PtrToStringBSTR((IntPtr)ptr->valuePtr);
			case VARTYPE.BOOL:
				return ptr->value16 == ushort.MaxValue;
			case VARTYPE.DECIMAL:
				return Variant.GetDecimal(ptr);
			case VARTYPE.I1:
				return (sbyte)ptr->value8;
			case VARTYPE.UI1:
				return ptr->value8;
			case VARTYPE.UI2:
				return ptr->value16;
			case VARTYPE.UI4:
			case VARTYPE.UINT:
				return ptr->value32;
			case VARTYPE.I8:
				return (long)ptr->value64;
			case VARTYPE.UI8:
				return ptr->value64;
			}
			return this.HandleUnknownVariantType(ptr);
		}

		// Token: 0x0600C6F3 RID: 50931 RVA: 0x0027A2B0 File Offset: 0x002784B0
		public unsafe void GetNative(object value, IntPtr native)
		{
			VARIANT* ptr = (VARIANT*)(void*)native;
			if (value == null)
			{
				Variant.SetEmpty(ptr);
				return;
			}
			if (value == DBNull.Value)
			{
				Variant.SetNull(ptr);
				return;
			}
			switch (global::System.Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Boolean:
				Variant.SetValue(ptr, (bool)value);
				return;
			case TypeCode.SByte:
				Variant.SetValue(ptr, (sbyte)value);
				return;
			case TypeCode.Byte:
				Variant.SetValue(ptr, (byte)value);
				return;
			case TypeCode.Int16:
				Variant.SetValue(ptr, (short)value);
				return;
			case TypeCode.UInt16:
				Variant.SetValue(ptr, (ushort)value);
				return;
			case TypeCode.Int32:
				Variant.SetValue(ptr, (int)value);
				return;
			case TypeCode.UInt32:
				Variant.SetValue(ptr, (uint)value);
				return;
			case TypeCode.Int64:
				Variant.SetValue(ptr, (long)value);
				return;
			case TypeCode.UInt64:
				Variant.SetValue(ptr, (ulong)value);
				return;
			case TypeCode.Single:
				Variant.SetValue(ptr, (float)value);
				return;
			case TypeCode.Double:
				Variant.SetValue(ptr, (double)value);
				return;
			case TypeCode.Decimal:
				Variant.SetValue(ptr, (decimal)value);
				return;
			case TypeCode.DateTime:
				Variant.SetValue(ptr, (DateTime)value);
				return;
			case TypeCode.String:
				Variant.SetValue(ptr, (string)value);
				return;
			}
			this.HandleUnknownObjectType(value, ptr);
		}

		// Token: 0x0600C6F4 RID: 50932 RVA: 0x0027A3F4 File Offset: 0x002785F4
		public unsafe void Cleanup(IntPtr native)
		{
			Variant.FreeAllocatedMemory((VARIANT*)(void*)native);
		}

		// Token: 0x0600C6F5 RID: 50933 RVA: 0x0027A401 File Offset: 0x00278601
		protected unsafe virtual object HandleUnknownVariantType(VARIANT* variant)
		{
			return Marshal.GetObjectForNativeVariant((IntPtr)((void*)variant));
		}

		// Token: 0x0600C6F6 RID: 50934 RVA: 0x0027A40E File Offset: 0x0027860E
		protected unsafe virtual void HandleUnknownObjectType(object obj, VARIANT* variant)
		{
			Marshal.GetNativeVariantForObject(obj, (IntPtr)((void*)variant));
		}
	}
}
