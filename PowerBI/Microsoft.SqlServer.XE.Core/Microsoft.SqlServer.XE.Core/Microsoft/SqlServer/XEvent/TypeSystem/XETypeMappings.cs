using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.TypeSystem
{
	// Token: 0x02000002 RID: 2
	public static class XETypeMappings
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000012D8 File Offset: 0x000012D8
		public static Type ManagedTypeFromXEType(uint typeRid)
		{
			XERelativeObjectId xerelativeObjectId;
			cpblk(ref xerelativeObjectId, ref typeRid, 4);
			uint num = xerelativeObjectId >> 28;
			if (num == 6U)
			{
				uint num2 = xerelativeObjectId & 1023;
				byte b;
				if (num2 == 0U && ((xerelativeObjectId >> 10) & 262143) >= XETypeMappings.sm_mappings.Length)
				{
					b = 0;
				}
				else
				{
					b = 1;
				}
				Debug.Assert(b != 0);
				if (num2 == 0U)
				{
					uint num3 = (xerelativeObjectId >> 10) & 262143;
					if (num3 < (uint)XETypeMappings.sm_mappings.Length)
					{
						return XETypeMappings.sm_mappings[(int)num3];
					}
				}
				return XETypeMappings.sm_mappings[22];
			}
			if (num == 3U)
			{
				return typeof(MapValue);
			}
			return null;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00001408 File Offset: 0x00001408
		public unsafe static object UnloadXeVariant(ulong source, uint typeRid, uint len)
		{
			XERelativeObjectId xerelativeObjectId;
			cpblk(ref xerelativeObjectId, ref typeRid, 4);
			object obj = null;
			if ((xerelativeObjectId & 1023) == null)
			{
				uint num = (xerelativeObjectId >> 10) & 262143;
				if (num != 22U)
				{
					if ((xerelativeObjectId & -268435456) == 1610612736)
					{
						switch (num)
						{
						case 1U:
							return (sbyte)source;
						case 2U:
							return (short)source;
						case 3U:
							return source;
						case 4U:
							return source;
						case 5U:
							return (byte)source;
						case 6U:
							return (ushort)source;
						case 7U:
							return source;
						case 8U:
						case 11U:
							return source;
						case 9U:
							return <Module>.XE_VariantUnload<float>(source);
						case 10U:
							return <Module>.XE_VariantUnload<double>(source);
						case 12U:
							return DateTime.FromFileTimeUtc(source);
						case 13U:
						case 14U:
						case 15U:
							if (len == 4)
							{
								return <Module>.XE_VariantUnload<unsigned\u0020int>(source);
							}
							if (len == 8)
							{
								return <Module>.XE_VariantUnload<unsigned\u0020__int64>(source);
							}
							Debug.Assert(false);
							return obj;
						case 16U:
						case 21U:
							if (len > 0)
							{
								return *source;
							}
							return Guid.Empty;
						case 17U:
							try
							{
								return Convert.ToChar(<Module>.XE_VariantUnload<char>(source));
							}
							catch (InvalidCastException)
							{
								return "";
							}
							break;
						case 18U:
							break;
						case 19U:
							if (len > 0)
							{
								return Marshal.PtrToStringAnsi((IntPtr)source, len);
							}
							return string.Empty;
						case 20U:
							if (len > 0)
							{
								return new string(source, 0, len >> 1);
							}
							return string.Empty;
						case 22U:
							goto IL_0291;
						case 23U:
							return new CallStack((IntPtr)source, len, 0);
						case 24U:
						case 25U:
							return Marshal.PtrToStructure((IntPtr)source, typeof(ActivityId));
						case 26U:
							if (source != null)
							{
								return true;
							}
							return false;
						case 27U:
							return (len <= 0) ? new XMLData("") : new XMLData(new string(source, 0, len >> 1));
						default:
							goto IL_0291;
						}
						return Convert.ToChar(<Module>.XE_VariantUnload<wchar_t>(source));
						IL_0291:
						throw new NotImplementedException();
					}
					return obj;
				}
			}
			byte[] array = new byte[len];
			if (len > 0)
			{
				Marshal.Copy((IntPtr)source, array, 0, len);
			}
			obj = array;
			return obj;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00001368 File Offset: 0x00001368
		public static object UnloadXeCallstack(ulong source, uint len, uint ptrSize)
		{
			return new CallStack((IntPtr)source, len, ptrSize);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000010D0 File Offset: 0x000010D0
		static XETypeMappings()
		{
			XETypeMappings.sm_mappings[0] = null;
			XETypeMappings.sm_mappings[1] = typeof(sbyte);
			XETypeMappings.sm_mappings[2] = typeof(short);
			XETypeMappings.sm_mappings[3] = typeof(int);
			XETypeMappings.sm_mappings[4] = typeof(long);
			XETypeMappings.sm_mappings[5] = typeof(byte);
			XETypeMappings.sm_mappings[6] = typeof(ushort);
			XETypeMappings.sm_mappings[7] = typeof(uint);
			XETypeMappings.sm_mappings[8] = typeof(ulong);
			XETypeMappings.sm_mappings[9] = typeof(float);
			XETypeMappings.sm_mappings[10] = typeof(double);
			XETypeMappings.sm_mappings[11] = typeof(ulong);
			XETypeMappings.sm_mappings[12] = typeof(DateTimeOffset);
			XETypeMappings.sm_mappings[13] = typeof(ulong);
			XETypeMappings.sm_mappings[14] = typeof(string);
			XETypeMappings.sm_mappings[15] = typeof(string);
			XETypeMappings.sm_mappings[16] = typeof(Guid);
			XETypeMappings.sm_mappings[17] = typeof(char);
			XETypeMappings.sm_mappings[18] = typeof(char);
			XETypeMappings.sm_mappings[19] = typeof(string);
			XETypeMappings.sm_mappings[20] = typeof(string);
			XETypeMappings.sm_mappings[21] = typeof(Guid);
			XETypeMappings.sm_mappings[22] = typeof(byte[]);
			XETypeMappings.sm_mappings[23] = typeof(CallStack);
			XETypeMappings.sm_mappings[24] = typeof(ActivityId);
			XETypeMappings.sm_mappings[25] = typeof(ActivityId);
			XETypeMappings.sm_mappings[26] = typeof(bool);
			XETypeMappings.sm_mappings[27] = typeof(XMLData);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00001388 File Offset: 0x00001388
		private static ValueType UnloadXeVariantInternal<float,float>(ulong data)
		{
			return <Module>.XE_VariantUnload<float>(data);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000013A8 File Offset: 0x000013A8
		private static ValueType UnloadXeVariantInternal<double,double>(ulong data)
		{
			return <Module>.XE_VariantUnload<double>(data);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000013C8 File Offset: 0x000013C8
		private static ValueType UnloadXeVariantInternal<unsigned\u0020int,unsigned\u0020__int64>(ulong data)
		{
			return <Module>.XE_VariantUnload<unsigned\u0020int>(data);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000013E8 File Offset: 0x000013E8
		private static ValueType UnloadXeVariantInternal<unsigned\u0020__int64,unsigned\u0020__int64>(ulong data)
		{
			return <Module>.XE_VariantUnload<unsigned\u0020__int64>(data);
		}

		// Token: 0x04000038 RID: 56
		private static Type[] sm_mappings = new Type[28];
	}
}
