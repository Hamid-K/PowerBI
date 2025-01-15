using System;
using System.Runtime.InteropServices;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using RSRemoteRpcClient;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000040 RID: 64
	internal sealed class RpcEncryption : Encryption
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00009FC0 File Offset: 0x000081C0
		public static RpcEncryption Instance
		{
			get
			{
				return RpcEncryption._instance;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009FC7 File Offset: 0x000081C7
		private RpcEncryption()
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009FCF File Offset: 0x000081CF
		protected override bool IsEncryptionChecked()
		{
			return this._encryptionChecked;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009FD7 File Offset: 0x000081D7
		protected override void SetEncryptionChecked()
		{
			this._encryptionChecked = true;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009FE0 File Offset: 0x000081E0
		protected override byte[] EncryptInternal(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			string rpcEndpointName = Globals.Configuration.RpcEndpoint;
			string serviceName = Globals.Configuration.WindowsServiceName;
			byte[] encryptedData = null;
			RevertImpersonationContext.Run(delegate
			{
				try
				{
					encryptedData = RemoteLogon.CatalogEncrypt(rpcEndpointName, data);
				}
				catch (UnauthorizedAccessException)
				{
					throw new AccessDeniedToSecureDataException();
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2147023181 || ex.ErrorCode == -2147023169)
					{
						throw new ReportServerServiceUnavailableException(serviceName);
					}
					throw new RPCException(ex);
				}
				catch (Exception ex2)
				{
					throw new RPCException(ex2);
				}
			});
			return encryptedData;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A044 File Offset: 0x00008244
		public override byte[] Decrypt(byte[] data, bool useSalt = true)
		{
			if (data == null)
			{
				return null;
			}
			string rpcEndpointName = Globals.Configuration.RpcEndpoint;
			string serviceName = Globals.Configuration.WindowsServiceName;
			byte[] dataFromRPC = null;
			RevertImpersonationContext.Run(delegate
			{
				try
				{
					dataFromRPC = RemoteLogon.CatalogDecrypt(rpcEndpointName, data, useSalt);
				}
				catch (UnauthorizedAccessException)
				{
					throw new AccessDeniedToSecureDataException();
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2147023181 || ex.ErrorCode == -2147023169)
					{
						throw new ReportServerServiceUnavailableException(serviceName);
					}
					throw new RPCException(ex);
				}
				catch (Exception ex2)
				{
					throw new RPCException(ex2);
				}
			});
			return MachineKeyEncryption.Instance.Decrypt(dataFromRPC, true);
		}

		// Token: 0x0400018A RID: 394
		private static readonly RpcEncryption _instance = new RpcEncryption();

		// Token: 0x0400018B RID: 395
		private bool _encryptionChecked;
	}
}
