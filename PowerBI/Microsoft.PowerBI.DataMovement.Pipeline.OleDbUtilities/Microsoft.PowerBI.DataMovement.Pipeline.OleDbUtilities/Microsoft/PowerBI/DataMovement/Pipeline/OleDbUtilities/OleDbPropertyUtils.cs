using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbUtilities
{
	// Token: 0x0200000A RID: 10
	public static class OleDbPropertyUtils
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B4 File Offset: 0x000002B4
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal unsafe static OleDbProperty[] ConvertAndFreeNativePropertySets(uint propSetsCount, DBPROPSET* propSets)
		{
			List<OleDbProperty> list = new List<OleDbProperty>();
			try
			{
				if (propSetsCount > 0U && propSets != null)
				{
					for (uint num = 0U; num < propSetsCount; num += 1U)
					{
						DBPROPSET* ptr = propSets + (ulong)num * (ulong)((long)sizeof(DBPROPSET)) / (ulong)sizeof(DBPROPSET);
						if (ptr->PropertyCount > 0U && ptr->Properties != null)
						{
							for (uint num2 = 0U; num2 < ptr->PropertyCount; num2 += 1U)
							{
								OleDbProperty oleDbProperty = OleDbPropertyUtils.ConvertNativeProperty(ptr->Properties + (ulong)num2 * (ulong)((long)sizeof(DBPROP)) / (ulong)sizeof(DBPROP), ptr->PropertySet);
								if (oleDbProperty != null)
								{
									list.Add(oleDbProperty);
								}
							}
						}
					}
				}
			}
			finally
			{
				OleDbPropertyUtils.FreeNativePropertySets(propSetsCount, propSets);
			}
			return list.ToArray();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000215C File Offset: 0x0000035C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static OleDbProperty CreateTypedOleDbProperty(object value, Guid propertyGroup, DBPROPID propertyId, bool required)
		{
			if (value == null)
			{
				return null;
			}
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Boolean:
				return new OleDbProperty<bool>(propertyGroup, propertyId, required, (bool)value);
			case TypeCode.Char:
				return new OleDbProperty<char>(propertyGroup, propertyId, required, (char)value);
			case TypeCode.SByte:
				return new OleDbProperty<sbyte>(propertyGroup, propertyId, required, (sbyte)value);
			case TypeCode.Byte:
				return new OleDbProperty<byte>(propertyGroup, propertyId, required, (byte)value);
			case TypeCode.Int16:
				return new OleDbProperty<short>(propertyGroup, propertyId, required, (short)value);
			case TypeCode.UInt16:
				return new OleDbProperty<ushort>(propertyGroup, propertyId, required, (ushort)value);
			case TypeCode.Int32:
				return new OleDbProperty<int>(propertyGroup, propertyId, required, (int)value);
			case TypeCode.UInt32:
				return new OleDbProperty<uint>(propertyGroup, propertyId, required, (uint)value);
			case TypeCode.Int64:
				return new OleDbProperty<long>(propertyGroup, propertyId, required, (long)value);
			case TypeCode.UInt64:
				return new OleDbProperty<ulong>(propertyGroup, propertyId, required, (ulong)value);
			case TypeCode.Single:
				return new OleDbProperty<float>(propertyGroup, propertyId, required, (float)value);
			case TypeCode.Double:
				return new OleDbProperty<double>(propertyGroup, propertyId, required, (double)value);
			case TypeCode.Decimal:
				return new OleDbProperty<decimal>(propertyGroup, propertyId, required, (decimal)value);
			case TypeCode.String:
				return new OleDbProperty<string>(propertyGroup, propertyId, required, (string)value);
			}
			throw RuntimeChecks.UnsupportedCodepath("CreateTypedOleDbProperty", "/src/Gateway/Pipeline/OleDbUtilities/OleDbPropertyUtils.cs", 94, "Unsupported code path reached");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022B0 File Offset: 0x000004B0
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		private unsafe static OleDbProperty ConvertNativeProperty(DBPROP* property, Guid propertyGroup)
		{
			if (property == null)
			{
				return null;
			}
			object @object = Variant.GetObject(&property->Variant);
			if (@object == null)
			{
				return null;
			}
			bool flag = property->Options == DBPROPOPTIONS.REQUIRED;
			return OleDbPropertyUtils.CreateTypedOleDbProperty(@object, propertyGroup, property->PropertyID, flag);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022F0 File Offset: 0x000004F0
		private unsafe static void FreeNativePropertySets(uint propSetsCount, DBPROPSET* propSets)
		{
			if (propSetsCount > 0U && propSets != null)
			{
				for (uint num = 0U; num < propSetsCount; num += 1U)
				{
					DBPROPSET* ptr = propSets + (ulong)num * (ulong)((long)sizeof(DBPROPSET)) / (ulong)sizeof(DBPROPSET);
					if (ptr->PropertyCount > 0U && ptr->Properties != null)
					{
						for (uint num2 = 0U; num2 < ptr->PropertyCount; num2 += 1U)
						{
							Variant.Clear(&ptr->Properties[(ulong)num2 * (ulong)((long)sizeof(DBPROP)) / (ulong)sizeof(DBPROP)].Variant);
						}
						Marshal.FreeCoTaskMem(new IntPtr((void*)ptr->Properties));
					}
				}
				Marshal.FreeCoTaskMem(new IntPtr((void*)propSets));
			}
		}
	}
}
