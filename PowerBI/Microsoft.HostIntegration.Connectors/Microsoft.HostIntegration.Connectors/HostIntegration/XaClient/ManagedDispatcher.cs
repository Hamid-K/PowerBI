using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x02000704 RID: 1796
	public class ManagedDispatcher
	{
		// Token: 0x060038EA RID: 14570 RVA: 0x000BEA30 File Offset: 0x000BCC30
		static ManagedDispatcher()
		{
			AssemblyName assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().ToString());
			string text = "Microsoft.HostIntegration.MqClient.Automatons";
			string text2 = "Microsoft.HostIntegration.MqClient.Automatons.XaRecovery";
			assemblyName.Name = text;
			string text3 = text2 + ", " + assemblyName.FullName;
			string fullName = typeof(IXaRecoveryEnlistment).FullName;
			try
			{
				ManagedDispatcher.mqClassType = Type.GetType(text3);
				if (ManagedDispatcher.mqClassType != null && ManagedDispatcher.mqClassType.GetInterface(fullName) == null)
				{
					ManagedDispatcher.mqClassType = null;
				}
			}
			catch (Exception)
			{
			}
			text = "Microsoft.HostIntegration.Drda.Requester";
			string text4 = "Microsoft.HostIntegration.Drda.Requester.XaRecoveryEnlistment";
			assemblyName.Name = text;
			text3 = text4 + ", " + assemblyName.FullName;
			try
			{
				ManagedDispatcher.drdaClassType = Type.GetType(text3);
				if (ManagedDispatcher.drdaClassType != null && ManagedDispatcher.drdaClassType.GetInterface(fullName) == null)
				{
					ManagedDispatcher.drdaClassType = null;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000BEB40 File Offset: 0x000BCD40
		internal static void AddInformation(int cookie, IXaClientEnlistment iXaClientStartEnlistment)
		{
			Dictionary<int, ClientEnlistmentInformation> dictionary = ManagedDispatcher.cookieToEnlistmentInformation;
			lock (dictionary)
			{
				ManagedDispatcher.cookieToEnlistmentInformation.Add(cookie, new ClientEnlistmentInformation(cookie, iXaClientStartEnlistment));
			}
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x000BEB8C File Offset: 0x000BCD8C
		internal static void AddInformation(int cookie, IXaClientStartEnlistment iXaClientStartEnlistment)
		{
			Dictionary<int, ClientEnlistmentInformation> dictionary = ManagedDispatcher.cookieToEnlistmentInformation;
			lock (dictionary)
			{
				ManagedDispatcher.cookieToEnlistmentInformation.Add(cookie, new ClientEnlistmentInformation(cookie, iXaClientStartEnlistment));
			}
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000BEBD8 File Offset: 0x000BCDD8
		internal static void RemoveInformation(int cookie)
		{
			Dictionary<int, ClientEnlistmentInformation> dictionary = ManagedDispatcher.cookieToEnlistmentInformation;
			lock (dictionary)
			{
				ManagedDispatcher.cookieToEnlistmentInformation.Remove(cookie);
			}
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000BEC20 File Offset: 0x000BCE20
		public static int TransactionEnlistmentAsyncCallback(int typeOfCallback, int cookie)
		{
			return ManagedDispatcher.InternalAsyncCallback(typeOfCallback, false, cookie);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000BEC2A File Offset: 0x000BCE2A
		public static int TransactionEnlistmentAsyncCallbackOnePhase(int typeOfCallback, int cookie)
		{
			return ManagedDispatcher.InternalAsyncCallback(typeOfCallback, true, cookie);
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000BEC34 File Offset: 0x000BCE34
		private static int InternalAsyncCallback(int typeOfCallback, bool singlePhase, int cookie)
		{
			if (typeOfCallback > 3)
			{
				throw new ArgumentOutOfRangeException("typeOfCallback");
			}
			ClientEnlistmentInformation clientEnlistmentInformation = null;
			Dictionary<int, ClientEnlistmentInformation> dictionary = ManagedDispatcher.cookieToEnlistmentInformation;
			lock (dictionary)
			{
				if (!ManagedDispatcher.cookieToEnlistmentInformation.TryGetValue(cookie, out clientEnlistmentInformation))
				{
					throw new ArgumentOutOfRangeException("cookie");
				}
			}
			XaReturnCode xaReturnCode = XaReturnCode.Ok;
			if (typeOfCallback == 3)
			{
				ManagedDispatcher.RemoveInformation(cookie);
				return (int)xaReturnCode;
			}
			try
			{
				if (clientEnlistmentInformation.IXaClientEnlistment != null)
				{
					switch (typeOfCallback)
					{
					case 0:
						xaReturnCode = clientEnlistmentInformation.IXaClientEnlistment.Prepare(singlePhase);
						if (!singlePhase && xaReturnCode == XaReturnCode.CommitedSinglePhase)
						{
							throw new InvalidOperationException();
						}
						break;
					case 1:
						clientEnlistmentInformation.IXaClientEnlistment.Commit();
						break;
					case 2:
						clientEnlistmentInformation.IXaClientEnlistment.Rollback();
						break;
					}
				}
				if (clientEnlistmentInformation.IXaClientStartEnlistment != null)
				{
					switch (typeOfCallback)
					{
					case 0:
						xaReturnCode = clientEnlistmentInformation.IXaClientStartEnlistment.Prepare(singlePhase);
						if (!singlePhase && xaReturnCode == XaReturnCode.CommitedSinglePhase)
						{
							throw new InvalidOperationException();
						}
						break;
					case 1:
						clientEnlistmentInformation.IXaClientStartEnlistment.Commit();
						break;
					case 2:
						clientEnlistmentInformation.IXaClientStartEnlistment.Rollback();
						break;
					}
				}
			}
			catch (Exception)
			{
				xaReturnCode = XaReturnCode.ResourceManagerError;
			}
			return (int)xaReturnCode;
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x000BED64 File Offset: 0x000BCF64
		public static int RecoveryOpenClose(int typeOfRecovery, string xaInfo, int resourceManagerId, long flags)
		{
			if (typeOfRecovery > 1)
			{
				throw new ArgumentOutOfRangeException("typeOfRecovery");
			}
			RecoveryEnlistmentInformation recoveryEnlistmentInformation = null;
			bool flag = false;
			Dictionary<int, RecoveryEnlistmentInformation> dictionary = ManagedDispatcher.rmidToEnlistmentInformation;
			lock (dictionary)
			{
				flag = ManagedDispatcher.rmidToEnlistmentInformation.TryGetValue(resourceManagerId, out recoveryEnlistmentInformation);
			}
			if ((typeOfRecovery == 1 && !flag) || (typeOfRecovery == 0 && flag))
			{
				throw new ArgumentOutOfRangeException("resourceManagerId");
			}
			XaReturnCode xaReturnCode = XaReturnCode.Ok;
			try
			{
				if (typeOfRecovery != 0)
				{
					if (typeOfRecovery != 1)
					{
						goto IL_010D;
					}
				}
				else
				{
					IXaRecoveryEnlistment ixaRecoveryEnlistment = ManagedDispatcher.GetIXaRecoveryEnlistment(ref xaInfo);
					if (ixaRecoveryEnlistment == null)
					{
						throw new ArgumentOutOfRangeException("xaInfo");
					}
					ixaRecoveryEnlistment.ResourceManagerId = resourceManagerId;
					recoveryEnlistmentInformation = new RecoveryEnlistmentInformation(resourceManagerId, ixaRecoveryEnlistment);
					xaReturnCode = recoveryEnlistmentInformation.IXaRecoveryEnlistment.Open(xaInfo, (XaFlags)flags);
					dictionary = ManagedDispatcher.rmidToEnlistmentInformation;
					lock (dictionary)
					{
						ManagedDispatcher.rmidToEnlistmentInformation.Add(resourceManagerId, recoveryEnlistmentInformation);
						goto IL_010D;
					}
				}
				dictionary = ManagedDispatcher.rmidToEnlistmentInformation;
				lock (dictionary)
				{
					ManagedDispatcher.rmidToEnlistmentInformation.Remove(resourceManagerId);
				}
				recoveryEnlistmentInformation.IXaRecoveryEnlistment.Close(xaInfo, (XaFlags)flags);
				IL_010D:;
			}
			catch (Exception)
			{
				xaReturnCode = XaReturnCode.ResourceManagerError;
			}
			return (int)xaReturnCode;
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x000BEEBC File Offset: 0x000BD0BC
		private static IXaRecoveryEnlistment GetIXaRecoveryEnlistment(ref string xaInfo)
		{
			Type type;
			if (xaInfo.StartsWith("MQ>>", StringComparison.Ordinal))
			{
				type = ManagedDispatcher.mqClassType;
				xaInfo = xaInfo.Substring(4);
			}
			else
			{
				type = ManagedDispatcher.drdaClassType;
			}
			if (type == null)
			{
				return null;
			}
			return Activator.CreateInstance(type) as IXaRecoveryEnlistment;
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000BEF08 File Offset: 0x000BD108
		public unsafe static int RecoveryTransactionProcessing(int typeOfRecovery, long xidAddress, int resourceManagerId, long flags)
		{
			if (typeOfRecovery - 2 > 4 && typeOfRecovery != 8)
			{
				throw new ArgumentOutOfRangeException("typeOfRecovery");
			}
			RecoveryEnlistmentInformation recoveryEnlistmentInformation = null;
			Dictionary<int, RecoveryEnlistmentInformation> dictionary = ManagedDispatcher.rmidToEnlistmentInformation;
			lock (dictionary)
			{
				if (!ManagedDispatcher.rmidToEnlistmentInformation.TryGetValue(resourceManagerId, out recoveryEnlistmentInformation))
				{
					throw new ArgumentOutOfRangeException("resourceManagerId");
				}
			}
			XaReturnCode xaReturnCode = XaReturnCode.Ok;
			int* ptr = xidAddress;
			int num = *(ptr++);
			int num2 = *(ptr++);
			int num3 = *(ptr++);
			byte[] array = new byte[num2 + num3];
			byte* ptr2 = (byte*)ptr;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = *(ptr2++);
			}
			Xid xid = new Xid(num, num2, num3, array);
			try
			{
				switch (typeOfRecovery)
				{
				case 2:
					xaReturnCode = recoveryEnlistmentInformation.IXaRecoveryEnlistment.Start(xid, (XaFlags)flags);
					break;
				case 3:
					xaReturnCode = recoveryEnlistmentInformation.IXaRecoveryEnlistment.End(xid, (XaFlags)flags);
					break;
				case 4:
					xaReturnCode = recoveryEnlistmentInformation.IXaRecoveryEnlistment.Prepare(xid, (XaFlags)flags);
					break;
				case 5:
					xaReturnCode = recoveryEnlistmentInformation.IXaRecoveryEnlistment.Commit(xid, (XaFlags)flags);
					break;
				case 6:
					xaReturnCode = recoveryEnlistmentInformation.IXaRecoveryEnlistment.Rollback(xid, (XaFlags)flags);
					break;
				case 8:
					xaReturnCode = recoveryEnlistmentInformation.IXaRecoveryEnlistment.Forget(xid, (XaFlags)flags);
					break;
				}
			}
			catch (Exception)
			{
				xaReturnCode = XaReturnCode.ResourceManagerError;
			}
			return (int)xaReturnCode;
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000BF07C File Offset: 0x000BD27C
		public unsafe static int RecoveryRecover(int typeOfRecovery, long dataAddress, int capacityOfArray, int resourceManagerId, long flags)
		{
			if (typeOfRecovery != 7)
			{
				throw new ArgumentOutOfRangeException("typeOfRecovery");
			}
			RecoveryEnlistmentInformation recoveryEnlistmentInformation = null;
			Dictionary<int, RecoveryEnlistmentInformation> dictionary = ManagedDispatcher.rmidToEnlistmentInformation;
			lock (dictionary)
			{
				if (!ManagedDispatcher.rmidToEnlistmentInformation.TryGetValue(resourceManagerId, out recoveryEnlistmentInformation))
				{
					throw new ArgumentOutOfRangeException("resourceManagerId");
				}
			}
			XaReturnCode xaReturnCode = XaReturnCode.Ok;
			Xid[] array = null;
			try
			{
				xaReturnCode = recoveryEnlistmentInformation.IXaRecoveryEnlistment.Recover((XaFlags)flags, capacityOfArray, out array);
			}
			catch (Exception)
			{
				xaReturnCode = XaReturnCode.ResourceManagerError;
			}
			if (array == null || array.Length == 0)
			{
				return (int)xaReturnCode;
			}
			byte* ptr = dataAddress;
			for (int i = 0; i < array.Length; i++)
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = array[i].FormatId;
				int transactionIdLength = array[i].TransactionIdLength;
				int branchQualifierLength = array[i].BranchQualifierLength;
				*(ptr2++) = transactionIdLength;
				*(ptr2++) = branchQualifierLength;
				int num = transactionIdLength + branchQualifierLength;
				ptr = (byte*)ptr2;
				for (int j = 0; j < num; j++)
				{
					*(ptr++) = array[i].Data[j];
				}
				int num2 = 128 - num;
				if (num2 != 0)
				{
					for (int k = 0; k < num2; k++)
					{
						*(ptr++) = 0;
					}
				}
			}
			return array.Length;
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x000BF1C8 File Offset: 0x000BD3C8
		public static int RecoveryComplete(int typeOfRecovery, int handle, int resourceManagerId, long flags, out int returnValue)
		{
			if (typeOfRecovery != 9)
			{
				throw new ArgumentOutOfRangeException("typeOfRecovery");
			}
			RecoveryEnlistmentInformation recoveryEnlistmentInformation = null;
			Dictionary<int, RecoveryEnlistmentInformation> dictionary = ManagedDispatcher.rmidToEnlistmentInformation;
			lock (dictionary)
			{
				if (!ManagedDispatcher.rmidToEnlistmentInformation.TryGetValue(resourceManagerId, out recoveryEnlistmentInformation))
				{
					throw new ArgumentOutOfRangeException("resourceManagerId");
				}
			}
			XaReturnCode xaReturnCode = XaReturnCode.Ok;
			returnValue = 0;
			try
			{
				xaReturnCode = recoveryEnlistmentInformation.IXaRecoveryEnlistment.Complete(handle, (XaFlags)flags, out returnValue);
			}
			catch (Exception)
			{
				xaReturnCode = XaReturnCode.ResourceManagerError;
			}
			return (int)xaReturnCode;
		}

		// Token: 0x04002136 RID: 8502
		private static Dictionary<int, ClientEnlistmentInformation> cookieToEnlistmentInformation = new Dictionary<int, ClientEnlistmentInformation>();

		// Token: 0x04002137 RID: 8503
		private static Dictionary<int, RecoveryEnlistmentInformation> rmidToEnlistmentInformation = new Dictionary<int, RecoveryEnlistmentInformation>();

		// Token: 0x04002138 RID: 8504
		private static Type mqClassType;

		// Token: 0x04002139 RID: 8505
		private static Type drdaClassType;
	}
}
