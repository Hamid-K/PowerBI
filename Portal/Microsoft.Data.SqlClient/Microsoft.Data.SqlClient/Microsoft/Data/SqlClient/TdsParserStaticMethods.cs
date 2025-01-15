using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000BA RID: 186
	internal sealed class TdsParserStaticMethods
	{
		// Token: 0x06000D66 RID: 3430 RVA: 0x000027D1 File Offset: 0x000009D1
		private TdsParserStaticMethods()
		{
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002AF50 File Offset: 0x00029150
		internal static void AliasRegistryLookup(ref string host, ref string protocol)
		{
			if (!ADP.IsEmpty(host))
			{
				string text = (string)ADP.LocalMachineRegistryValue("SOFTWARE\\Microsoft\\MSSQLServer\\Client\\ConnectTo", host);
				if (!ADP.IsEmpty(text))
				{
					int num = text.IndexOf(',');
					if (-1 != num)
					{
						string text2 = text.Substring(0, num).ToLower(CultureInfo.InvariantCulture);
						if (num + 1 < text.Length)
						{
							string text3 = text.Substring(num + 1);
							if ("dbnetlib" == text2)
							{
								num = text3.IndexOf(':');
								if (-1 != num && num + 1 < text3.Length)
								{
									text2 = text3.Substring(0, num);
									if (SqlConnectionString.ValidProtocol(text2))
									{
										protocol = text2;
										host = text3.Substring(num + 1);
										return;
									}
								}
							}
							else
							{
								protocol = (string)SqlConnectionString.NetlibMapping()[text2];
								if (protocol != null)
								{
									host = text3;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0002B01C File Offset: 0x0002921C
		internal static byte[] ObfuscatePassword(string password)
		{
			byte[] array = new byte[password.Length << 1];
			for (int i = 0; i < password.Length; i++)
			{
				int num = (int)password[i];
				byte b = (byte)(num & 255);
				byte b2 = (byte)((num >> 8) & 255);
				array[i << 1] = (byte)((((int)(b & 15) << 4) | (b >> 4)) ^ 165);
				array[(i << 1) + 1] = (byte)((((int)(b2 & 15) << 4) | (b2 >> 4)) ^ 165);
			}
			return array;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0002B09C File Offset: 0x0002929C
		internal static byte[] ObfuscatePassword(byte[] password)
		{
			for (int i = 0; i < password.Length; i++)
			{
				byte b = password[i] & 15;
				byte b2 = password[i] & 240;
				password[i] = (byte)(((b2 >> 4) | ((int)b << 4)) ^ 165);
			}
			return password;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0002B0DC File Offset: 0x000292DC
		internal static int GetCurrentProcessIdForTdsLoginOnly()
		{
			if (TdsParserStaticMethods.s_currentProcessId == -1)
			{
				int id;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					id = currentProcess.Id;
				}
				Volatile.Write(ref TdsParserStaticMethods.s_currentProcessId, id);
			}
			return TdsParserStaticMethods.s_currentProcessId;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0002B12C File Offset: 0x0002932C
		internal static int GetCurrentThreadIdForTdsLoginOnly()
		{
			return Environment.CurrentManagedThreadId;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0002B134 File Offset: 0x00029334
		internal static byte[] GetNetworkPhysicalAddressForTdsLoginOnly()
		{
			if (TdsParserStaticMethods.s_nicAddress != null)
			{
				return TdsParserStaticMethods.s_nicAddress;
			}
			byte[] array = null;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				int num = 0;
				object obj = ADP.LocalMachineRegistryValue("SOFTWARE\\Description\\Microsoft\\Rpc\\UuidTemporaryData", "NetworkAddressLocal");
				if (obj is int)
				{
					num = (int)obj;
				}
				if (num <= 0)
				{
					obj = ADP.LocalMachineRegistryValue("SOFTWARE\\Description\\Microsoft\\Rpc\\UuidTemporaryData", "NetworkAddress");
					if (obj is byte[])
					{
						array = (byte[])obj;
					}
				}
			}
			if (array == null)
			{
				array = new byte[6];
				Random random = new Random();
				random.NextBytes(array);
			}
			Interlocked.CompareExchange<byte[]>(ref TdsParserStaticMethods.s_nicAddress, array, null);
			return TdsParserStaticMethods.s_nicAddress;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0002B1CC File Offset: 0x000293CC
		internal static int GetTimeoutMilliseconds(long timeoutTime)
		{
			if (9223372036854775807L == timeoutTime)
			{
				return -1;
			}
			long num = ADP.TimerRemainingMilliseconds(timeoutTime);
			if (num < 0L)
			{
				return 0;
			}
			if (num > 2147483647L)
			{
				return int.MaxValue;
			}
			return (int)num;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0002B208 File Offset: 0x00029408
		internal static long GetTimeout(long timeoutMilliseconds)
		{
			long num;
			if (timeoutMilliseconds <= 0L)
			{
				num = long.MaxValue;
			}
			else
			{
				try
				{
					num = checked(ADP.TimerCurrent() + ADP.TimerFromMilliseconds(timeoutMilliseconds));
				}
				catch (OverflowException)
				{
					num = long.MaxValue;
				}
			}
			return num;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0002B254 File Offset: 0x00029454
		internal static long GetTimeoutSeconds(int timeout)
		{
			return TdsParserStaticMethods.GetTimeout((long)timeout * 1000L);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0002B264 File Offset: 0x00029464
		internal static bool TimeoutHasExpired(long timeoutTime)
		{
			bool flag = false;
			if (timeoutTime != 0L && 9223372036854775807L != timeoutTime)
			{
				flag = ADP.TimerHasExpired(timeoutTime);
			}
			return flag;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0002B28A File Offset: 0x0002948A
		internal static int NullAwareStringLength(string str)
		{
			if (str == null)
			{
				return 0;
			}
			return str.Length;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0002B298 File Offset: 0x00029498
		internal static int GetRemainingTimeout(int timeout, long start)
		{
			if (timeout <= 0)
			{
				return timeout;
			}
			long num = ADP.TimerRemainingSeconds(start + ADP.TimerFromSeconds(timeout));
			if (num <= 0L)
			{
				return 1;
			}
			return checked((int)num);
		}

		// Token: 0x040005DC RID: 1500
		private const int NoProcessId = -1;

		// Token: 0x040005DD RID: 1501
		private static int s_currentProcessId = -1;

		// Token: 0x040005DE RID: 1502
		private static byte[] s_nicAddress = null;
	}
}
