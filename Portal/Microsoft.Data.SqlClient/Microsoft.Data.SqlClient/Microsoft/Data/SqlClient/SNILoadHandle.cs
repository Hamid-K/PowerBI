using System;
using System.Runtime.InteropServices;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000B4 RID: 180
	internal sealed class SNILoadHandle : SafeHandle
	{
		// Token: 0x06000CD4 RID: 3284 RVA: 0x00027358 File Offset: 0x00025558
		private SNILoadHandle()
			: base(IntPtr.Zero, true)
		{
			try
			{
			}
			finally
			{
				this._sniStatus = SNINativeMethodWrapper.SNIInitialize();
				this.handle = (IntPtr)1;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x000273C8 File Offset: 0x000255C8
		public bool ClientOSEncryptionSupport
		{
			get
			{
				bool? clientOSEncryptionSupport = this._clientOSEncryptionSupport;
				if (clientOSEncryptionSupport == null && this._sniStatus == 0U)
				{
					try
					{
						uint num = 0U;
						SNINativeMethodWrapper.SNIQueryInfo(SNINativeMethodWrapper.QTypes.SNI_QUERY_CLIENT_ENCRYPT_POSSIBLE, ref num);
						this._clientOSEncryptionSupport = new bool?(num > 0U);
					}
					catch (Exception ex)
					{
						SqlClientEventSource.Log.TryTraceEvent<string>("<sc.SNILoadHandle.EncryptClientPossible|SEC> Exception occurs during resolving encryption possibility: {0}", ex.Message);
					}
				}
				return this._clientOSEncryptionSupport.Value;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002743C File Offset: 0x0002563C
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002744E File Offset: 0x0002564E
		protected override bool ReleaseHandle()
		{
			if (this.handle != IntPtr.Zero)
			{
				if (this._sniStatus == 0U)
				{
					LocalDBAPI.ReleaseDLLHandles();
					SNINativeMethodWrapper.SNITerminate();
				}
				this.handle = IntPtr.Zero;
			}
			return true;
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00027481 File Offset: 0x00025681
		public uint Status
		{
			get
			{
				return this._sniStatus;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00027489 File Offset: 0x00025689
		public EncryptionOptions Options
		{
			get
			{
				return this._encryptionOption;
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00027494 File Offset: 0x00025694
		private static void ReadDispatcher(IntPtr key, IntPtr packet, uint error)
		{
			if (IntPtr.Zero != key)
			{
				TdsParserStateObject tdsParserStateObject = (TdsParserStateObject)((GCHandle)key).Target;
				if (tdsParserStateObject != null)
				{
					tdsParserStateObject.ReadAsyncCallback(IntPtr.Zero, packet, error);
				}
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x000274D4 File Offset: 0x000256D4
		private static void WriteDispatcher(IntPtr key, IntPtr packet, uint error)
		{
			if (IntPtr.Zero != key)
			{
				TdsParserStateObject tdsParserStateObject = (TdsParserStateObject)((GCHandle)key).Target;
				if (tdsParserStateObject != null)
				{
					tdsParserStateObject.WriteAsyncCallback(IntPtr.Zero, packet, error);
				}
			}
		}

		// Token: 0x04000572 RID: 1394
		internal static readonly SNILoadHandle SingletonInstance = new SNILoadHandle();

		// Token: 0x04000573 RID: 1395
		internal readonly SNINativeMethodWrapper.SqlAsyncCallbackDelegate ReadAsyncCallbackDispatcher = new SNINativeMethodWrapper.SqlAsyncCallbackDelegate(SNILoadHandle.ReadDispatcher);

		// Token: 0x04000574 RID: 1396
		internal readonly SNINativeMethodWrapper.SqlAsyncCallbackDelegate WriteAsyncCallbackDispatcher = new SNINativeMethodWrapper.SqlAsyncCallbackDelegate(SNILoadHandle.WriteDispatcher);

		// Token: 0x04000575 RID: 1397
		private readonly uint _sniStatus = uint.MaxValue;

		// Token: 0x04000576 RID: 1398
		private readonly EncryptionOptions _encryptionOption;

		// Token: 0x04000577 RID: 1399
		private bool? _clientOSEncryptionSupport;
	}
}
