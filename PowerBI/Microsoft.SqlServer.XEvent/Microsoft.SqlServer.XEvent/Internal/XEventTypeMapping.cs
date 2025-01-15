using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Internal
{
	// Token: 0x02000048 RID: 72
	public static class XEventTypeMapping
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00003504 File Offset: 0x00003504
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool IsTypeSupported(Type type)
		{
			XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes = default(XEventTypeMapping.XEventTypeAttributes);
			return XEventTypeMapping.sm_mappings.TryGetValue(type, out xeventTypeAttributes);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003404 File Offset: 0x00003404
		internal static XERelativeObjectId XETypeFromManagedType(Type type)
		{
			return ((XEventTypeMapping.sm_mappings[type].m_rid & 262143U) | 1572864U) << 10;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003440 File Offset: 0x00003440
		internal static ushort XETypeSizeFromManagedType(Type type)
		{
			return XEventTypeMapping.sm_mappings[type].m_size;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00003468 File Offset: 0x00003468
		internal static MethodInfo MarshalCopyMethod(Type type)
		{
			return XEventTypeMapping.sm_mappings[type].m_rawCopyMethod;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00003490 File Offset: 0x00003490
		[return: MarshalAs(UnmanagedType.U1)]
		internal static bool IsVld(XERelativeObjectId rid)
		{
			uint num = (rid >> 10) & 262143;
			return num == 20U || num == 21U || num == 22U;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000034C4 File Offset: 0x000034C4
		[return: MarshalAs(UnmanagedType.U1)]
		internal static bool IsVld(Type type)
		{
			XERelativeObjectId xerelativeObjectId = XEventTypeMapping.XETypeFromManagedType(type);
			uint num = (xerelativeObjectId >> 10) & 262143;
			byte b;
			if (num != 20U && num != 21U && num != 22U)
			{
				b = 0;
			}
			else
			{
				b = 1;
			}
			return b != 0;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000035E4 File Offset: 0x000035E4
		static XEventTypeMapping()
		{
			XEventTypeMapping.BuildTypeMappings();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00002F70 File Offset: 0x00002F70
		private static void BuildTypeMappings()
		{
			if (XEventTypeMapping.sm_mappings == null)
			{
				XEventTypeMapping.sm_mappings = new Dictionary<Type, XEventTypeMapping.XEventTypeAttributes>();
				Type typeFromHandle = typeof(Marshal);
				Type typeFromHandle2 = typeof(XEventTypeMapping);
				Type typeFromHandle3 = typeof(GenericInteropEvent);
				MethodInfo method = typeFromHandle.GetMethod("WriteByte", new Type[]
				{
					typeof(IntPtr),
					typeof(int),
					typeof(byte)
				});
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes = new XEventTypeMapping.XEventTypeAttributes(1U, 1, method);
				XEventTypeMapping.sm_mappings.Add(typeof(sbyte), xeventTypeAttributes);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes2 = new XEventTypeMapping.XEventTypeAttributes(5U, 1, method);
				XEventTypeMapping.sm_mappings.Add(typeof(byte), xeventTypeAttributes2);
				MethodInfo method2 = typeFromHandle.GetMethod("WriteInt16", new Type[]
				{
					typeof(IntPtr),
					typeof(int),
					typeof(short)
				});
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes3 = new XEventTypeMapping.XEventTypeAttributes(2U, 2, method2);
				XEventTypeMapping.sm_mappings.Add(typeof(short), xeventTypeAttributes3);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes4 = new XEventTypeMapping.XEventTypeAttributes(6U, 2, method2);
				XEventTypeMapping.sm_mappings.Add(typeof(ushort), xeventTypeAttributes4);
				MethodInfo method3 = typeFromHandle.GetMethod("WriteInt32", new Type[]
				{
					typeof(IntPtr),
					typeof(int),
					typeof(int)
				});
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes5 = new XEventTypeMapping.XEventTypeAttributes(3U, 4, method3);
				XEventTypeMapping.sm_mappings.Add(typeof(int), xeventTypeAttributes5);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes6 = new XEventTypeMapping.XEventTypeAttributes(7U, 4, method3);
				XEventTypeMapping.sm_mappings.Add(typeof(uint), xeventTypeAttributes6);
				MethodInfo method4 = typeFromHandle2.GetMethod("MarshalFloat", BindingFlags.Static | BindingFlags.NonPublic);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes7 = new XEventTypeMapping.XEventTypeAttributes(9U, 4, method4);
				XEventTypeMapping.sm_mappings.Add(typeof(float), xeventTypeAttributes7);
				MethodInfo method5 = typeFromHandle.GetMethod("WriteInt64", new Type[]
				{
					typeof(IntPtr),
					typeof(int),
					typeof(long)
				});
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes8 = new XEventTypeMapping.XEventTypeAttributes(4U, 8, method5);
				XEventTypeMapping.sm_mappings.Add(typeof(long), xeventTypeAttributes8);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes9 = new XEventTypeMapping.XEventTypeAttributes(8U, 8, method5);
				XEventTypeMapping.sm_mappings.Add(typeof(ulong), xeventTypeAttributes9);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes10 = new XEventTypeMapping.XEventTypeAttributes(13U, (ushort)IntPtr.Size, method5);
				XEventTypeMapping.sm_mappings.Add(typeof(IntPtr), xeventTypeAttributes10);
				MethodInfo method6 = typeFromHandle2.GetMethod("MarshalDouble", BindingFlags.Static | BindingFlags.NonPublic);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes11 = new XEventTypeMapping.XEventTypeAttributes(10U, 8, method6);
				XEventTypeMapping.sm_mappings.Add(typeof(double), xeventTypeAttributes11);
				MethodInfo method7 = typeFromHandle.GetMethod("WriteInt16", new Type[]
				{
					typeof(IntPtr),
					typeof(int),
					typeof(char)
				});
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes12 = new XEventTypeMapping.XEventTypeAttributes(18U, 2, method7);
				XEventTypeMapping.sm_mappings.Add(typeof(char), xeventTypeAttributes12);
				MethodInfo method8 = typeFromHandle2.GetMethod("MarshalBoolean", BindingFlags.Static | BindingFlags.NonPublic);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes13 = new XEventTypeMapping.XEventTypeAttributes(26U, 1, method8);
				XEventTypeMapping.sm_mappings.Add(typeof(bool), xeventTypeAttributes13);
				MethodInfo method9 = typeFromHandle2.GetMethod("MarshalDateTime", BindingFlags.Static | BindingFlags.NonPublic);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes14 = new XEventTypeMapping.XEventTypeAttributes(12U, 8, method9);
				XEventTypeMapping.sm_mappings.Add(typeof(DateTime), xeventTypeAttributes14);
				MethodInfo method10 = typeFromHandle2.GetMethod("MarshalDateTimeOffset", BindingFlags.Static | BindingFlags.NonPublic);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes15 = new XEventTypeMapping.XEventTypeAttributes(12U, 8, method10);
				XEventTypeMapping.sm_mappings.Add(typeof(DateTimeOffset), xeventTypeAttributes15);
				MethodInfo method11 = typeFromHandle3.GetMethod("Set", new Type[]
				{
					typeof(void).MakePointerType(),
					typeof(void).MakePointerType(),
					typeof(uint),
					typeof(ushort),
					typeof(ushort)
				});
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes16 = new XEventTypeMapping.XEventTypeAttributes(20U, 8, method11);
				XEventTypeMapping.sm_mappings.Add(typeof(string), xeventTypeAttributes16);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes17 = new XEventTypeMapping.XEventTypeAttributes(22U, 8, method11);
				XEventTypeMapping.sm_mappings.Add(typeof(byte[]), xeventTypeAttributes17);
				XEventTypeMapping.XEventTypeAttributes xeventTypeAttributes18 = new XEventTypeMapping.XEventTypeAttributes(21U, 8, method11);
				XEventTypeMapping.sm_mappings.Add(typeof(Guid), xeventTypeAttributes18);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00003534 File Offset: 0x00003534
		private static void MarshalDateTime(IntPtr ptr, int ofs, DateTime val)
		{
			ulong num = (ulong)val.ToFileTimeUtc();
			Marshal.WriteInt64(ptr, ofs, (long)num);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00003584 File Offset: 0x00003584
		private static void MarshalBoolean(IntPtr ptr, int ofs, [MarshalAs(UnmanagedType.U1)] bool val)
		{
			int num = (val ? 1 : 0);
			Marshal.WriteByte(ptr, ofs, (byte)num);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00003558 File Offset: 0x00003558
		private static void MarshalDateTimeOffset(IntPtr ptr, int ofs, DateTimeOffset val)
		{
			ulong num = (ulong)val.ToUniversalTime().ToFileTime();
			Marshal.WriteInt64(ptr, ofs, (long)num);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000035AC File Offset: 0x000035AC
		private unsafe static void MarshalFloat(byte* @base, int offset, float val)
		{
			offset[@base / 4] = val;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000035C8 File Offset: 0x000035C8
		private unsafe static void MarshalDouble(byte* @base, int offset, double val)
		{
			offset[@base / 8] = val;
		}

		// Token: 0x04000137 RID: 311
		private static Dictionary<Type, XEventTypeMapping.XEventTypeAttributes> sm_mappings = null;

		// Token: 0x02000049 RID: 73
		private struct XEventTypeAttributes
		{
			// Token: 0x0600017B RID: 379 RVA: 0x00002F48 File Offset: 0x00002F48
			public XEventTypeAttributes(uint rid, ushort size, MethodInfo rawCopyMethod)
			{
				this.m_rid = rid;
				this.m_size = size;
				this.m_rawCopyMethod = rawCopyMethod;
			}

			// Token: 0x04000138 RID: 312
			public uint m_rid;

			// Token: 0x04000139 RID: 313
			public ushort m_size;

			// Token: 0x0400013A RID: 314
			public MethodInfo m_rawCopyMethod;
		}
	}
}
