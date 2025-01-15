using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MsolapWrapper
{
	// Token: 0x02000002 RID: 2
	internal class WrapperContract
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x0000302C File Offset: 0x0000242C
		public static void RetailAssert([MarshalAs(UnmanagedType.U1)] bool condition, string message, object arg1, object arg2, object arg3)
		{
			if (!condition)
			{
				throw new InvalidOperationException(WrapperContract.GetMessage(message, CultureInfo.InvariantCulture, arg1, arg2, arg3));
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003000 File Offset: 0x00002400
		public static void RetailAssert([MarshalAs(UnmanagedType.U1)] bool condition, string message, object arg1, object arg2)
		{
			if (!condition)
			{
				throw new InvalidOperationException(WrapperContract.GetMessage(message, CultureInfo.InvariantCulture, arg1, arg2, null));
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002FD4 File Offset: 0x000023D4
		public static void RetailAssert([MarshalAs(UnmanagedType.U1)] bool condition, string message, object arg1)
		{
			if (!condition)
			{
				throw new InvalidOperationException(WrapperContract.GetMessage(message, CultureInfo.InvariantCulture, arg1, null, null));
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002FB4 File Offset: 0x000023B4
		public static void RetailAssert([MarshalAs(UnmanagedType.U1)] bool condition, string message)
		{
			if (!condition)
			{
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002784 File Offset: 0x00001B84
		public static void Fail(string msg)
		{
			throw new InvalidOperationException(msg);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00001730 File Offset: 0x00000B30
		[Conditional("DEBUG")]
		public static void Assert([MarshalAs(UnmanagedType.U1)] bool f, string msg, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000171C File Offset: 0x00000B1C
		[Conditional("DEBUG")]
		public static void Assert([MarshalAs(UnmanagedType.U1)] bool f, string msg, object arg1, object arg2)
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00001708 File Offset: 0x00000B08
		[Conditional("DEBUG")]
		public static void Assert([MarshalAs(UnmanagedType.U1)] bool f, string msg, object arg1)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000016F4 File Offset: 0x00000AF4
		[Conditional("DEBUG")]
		public static void Assert([MarshalAs(UnmanagedType.U1)] bool f, string msg)
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00001744 File Offset: 0x00000B44
		[Conditional("DEBUG")]
		public static void AssertNonEmpty(string s, string msg)
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000027E8 File Offset: 0x00001BE8
		public static string GetMessageInvariant(string msg, object arg1, object arg2, object arg3)
		{
			return WrapperContract.GetMessage(msg, CultureInfo.InvariantCulture, arg1, arg2, arg3);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000027C4 File Offset: 0x00001BC4
		public static string GetMessageInvariant(string msg, object arg1, object arg2)
		{
			return WrapperContract.GetMessage(msg, CultureInfo.InvariantCulture, arg1, arg2, null);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000027A0 File Offset: 0x00001BA0
		public static string GetMessageInvariant(string msg, object arg1)
		{
			return WrapperContract.GetMessage(msg, CultureInfo.InvariantCulture, arg1, null, null);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00001758 File Offset: 0x00000B58
		private static string GetMessage(string msg, CultureInfo cultureInfo, object arg1, object arg2, object arg3)
		{
			try
			{
				msg = string.Format(cultureInfo, msg, new object[] { arg1, arg2, arg3 });
			}
			catch (FormatException ex)
			{
				"Resource format string arg mismatch: " + ex.Message;
			}
			return msg;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000017C0 File Offset: 0x00000BC0
		private static void FailCore(string msg)
		{
			throw new InvalidOperationException(msg);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006414 File Offset: 0x00005814
		[Conditional("DEBUG")]
		public static void AssertValue<MsolapWrapper::ICAccessorWrapper\u0020^>(ICAccessorWrapper val, string name)
		{
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006414 File Offset: 0x00005814
		[Conditional("DEBUG")]
		public static void AssertValue<MsolapWrapper::ChapterHandle\u0020^>(ChapterHandle val, string name)
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006428 File Offset: 0x00005828
		[Conditional("DEBUG")]
		public static void AssertValue<MsolapWrapper::Connection\u0020^>(Connection val, string name)
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006414 File Offset: 0x00005814
		[Conditional("DEBUG")]
		public static void AssertValue<MsolapWrapper::Command\u0020^>(Command val, string name)
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006414 File Offset: 0x00005814
		[Conditional("DEBUG")]
		public static void AssertValue<System::String\u0020^>(string val, string name)
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006414 File Offset: 0x00005814
		[Conditional("DEBUG")]
		public static void AssertValue<cli::array<System::PE$AAVObject\u0020>^>(object[] val, string name)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006414 File Offset: 0x00005814
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<MsolapWrapper::MsolapTracerBase\u0020^>(MsolapTracerBase val, string name)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00009488 File Offset: 0x00008888
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<System::String\u0020^>(string val, string name)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006414 File Offset: 0x00005814
		[Conditional("DEBUG")]
		public static void AssertNonEmpty<System::Collections::Generic::IReadOnlyDictionary<System::String\u0020^,System::Object\u0020^>\u0020>(IReadOnlyDictionary<string, object> val, string name)
		{
		}
	}
}
