using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using <CppImplementationDetails>;
using ATL;

namespace MsolapWrapper
{
	// Token: 0x02000084 RID: 132
	public class Connection : IDisposable
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00008AF8 File Offset: 0x00007EF8
		public unsafe Connection(string connString, MsolapTracerBase t)
		{
			CDataSource* ptr = <Module>.@new(8UL);
			CDataSource* ptr2;
			if (ptr != null)
			{
				initblk(ptr, 0, 8L);
				*(long*)ptr = 0L;
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
			this._connection = ptr2;
			this._connectionString = connString;
			NativeProxyTracer* ptr5;
			if (t != null)
			{
				NativeProxyTracer* ptr3 = <Module>.@new(24UL);
				NativeProxyTracer* ptr4;
				try
				{
					if (ptr3 != null)
					{
						*(long*)ptr3 = ref <Module>.??_7NativeProxyTracer@MsolapWrapper@@6B@;
						*(long*)(ptr3 + 8L / (long)sizeof(NativeProxyTracer)) = ((IntPtr)GCHandle.Alloc(t)).ToPointer();
						ptr4 = ptr3;
					}
					else
					{
						ptr4 = null;
					}
				}
				catch
				{
					<Module>.delete((void*)ptr3, 24UL);
					throw;
				}
				ptr5 = ptr4;
			}
			else
			{
				ptr5 = null;
			}
			this._tracer = (IMsolapTracer*)ptr5;
			GC.KeepAlive(this);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009158 File Offset: 0x00008558
		public Connection(string connString)
			: this(connString, null)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009178 File Offset: 0x00008578
		public Command CreateCommand(string commandText)
		{
			return new Command(this, commandText);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009194 File Offset: 0x00008594
		public SchemaCommand CreateSchemaCommand()
		{
			return new SchemaCommand(this);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008708 File Offset: 0x00007B08
		public unsafe void Close()
		{
			CDataSource* connection = this._connection;
			if (connection != null)
			{
				CDataSource* ptr = connection;
				IDBInitialize* ptr2 = *ptr;
				if (ptr2 != null)
				{
					*ptr = 0L;
					IDBInitialize* ptr3 = ptr2;
					uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, (IntPtr)(*(*(long*)ptr3 + 16L)));
				}
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008BB4 File Offset: 0x00007FB4
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool IsOpen()
		{
			CDataSource* connection = this._connection;
			int num;
			if (connection != null && ((((*(long*)connection == 0L) ? 1 : 0) == 0) ? 1 : 0) != 0)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			GC.KeepAlive(this);
			return (byte)num != 0;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000091B0 File Offset: 0x000085B0
		public unsafe void Open()
		{
			this.IsOpen();
			CComPtr<IDBInitialize> ccomPtr<IDBInitialize>;
			CComPtr<IDBInitialize>* ptr = MsolapClassFactory.CreateDbInitialize(&ccomPtr<IDBInitialize>);
			try
			{
				<Module>.ATL.CComPtr<IDBInitialize>.=(this._connection, ptr);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IDBInitialize>.{dtor}), (void*)(&ccomPtr<IDBInitialize>));
				throw;
			}
			if (ccomPtr<IDBInitialize> != null)
			{
				uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IDBInitialize>, (IntPtr)(*(*ccomPtr<IDBInitialize> + 16L)));
			}
			this.SetTracerForDbInit(this._connection);
			this.SetConnectionProperties(this._connection);
			long num2 = *(long*)this._connection;
			Utils.ThrowErrorIfHrFailed(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 24L))), "Could not initialize connection");
			GC.KeepAlive(this);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008BEC File Offset: 0x00007FEC
		public unsafe IDBInitialize* GetDBInitialize()
		{
			this.IsOpen();
			return *(long*)this._connection;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00009260 File Offset: 0x00008660
		private unsafe void !Connection()
		{
			CDataSource* connection = this._connection;
			if (connection != null)
			{
				CDataSource* ptr = connection;
				ulong num = (ulong)(*(long*)ptr);
				if (num != 0UL)
				{
					ulong num2 = num;
					uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
				}
				<Module>.delete((void*)ptr, 8UL);
				this._connection = null;
			}
			IMsolapTracer* tracer = this._tracer;
			if (tracer != null)
			{
				<Module>.delete((void*)tracer, 8UL);
				this._tracer = null;
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000092C8 File Offset: 0x000086C8
		private void ~Connection()
		{
			this.Close();
			this.!Connection();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000084E8 File Offset: 0x000078E8
		internal unsafe CDataSource* GetConnection()
		{
			return this._connection;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008C10 File Offset: 0x00008010
		private unsafe void SetTracerForDbInit(CComPtr<IDBInitialize>* dbInit)
		{
			CComPtr<IASTracerContext> ccomPtr<IASTracerContext> = 0L;
			try
			{
				long num = *dbInit;
				if (calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), (IntPtr)num, ref <Module>._GUID_62b9e9ad_e0e5_4ab2_9626_249e06fa5b9e, (void**)(&ccomPtr<IASTracerContext>), (IntPtr)(*(*num))) >= 0 && this._tracer != null)
				{
					CComBSTR ccomBSTR;
					<Module>.ATL.CComBSTR.{ctor}(ref ccomBSTR, (char*)(&<Module>.??_C@_1BG@NDPOPHON@?$AAS?$AAe?$AAt?$AA?5?$AAT?$AAr?$AAa?$AAc?$AAe?$AAr@));
					try
					{
						IMsolapTracer* tracer = this._tracer;
						calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,__MIDL_IMsolapTracer_0001,System.Char* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), tracer, (__MIDL_IMsolapTracer_0001)3, ccomBSTR, (IntPtr)(*(*(long*)tracer + 24L)));
						Utils.ThrowErrorIfHrFailed(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IMsolapTracer*), ccomPtr<IASTracerContext>, this._tracer, (IntPtr)(*(*ccomPtr<IASTracerContext> + 24L))), "Failed to set tracer");
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR));
						throw;
					}
					<Module>.SysFreeString(ccomBSTR);
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IASTracerContext>.{dtor}), (void*)(&ccomPtr<IASTracerContext>));
				throw;
			}
			if (ccomPtr<IASTracerContext> != null)
			{
				uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IASTracerContext>, (IntPtr)(*(*ccomPtr<IASTracerContext> + 16L)));
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008D08 File Offset: 0x00008108
		private unsafe void SetConnectionProperties(CComPtr<IDBInitialize>* dbInit)
		{
			ref byte ptr = this._connectionString;
			if ((ref ptr) != null)
			{
				ptr = (long)RuntimeHelpers.OffsetToStringData + (ref ptr);
			}
			ref char ptr2 = ref ptr;
			CComBSTR ccomBSTR;
			<Module>.ATL.CComBSTR.{ctor}(ref ccomBSTR, ref ptr2);
			try
			{
				$ArrayType$$$BY00UtagDBPROP@@ $ArrayType$$$BY00UtagDBPROP@@;
				<Module>.VariantInit((ref $ArrayType$$$BY00UtagDBPROP@@) + 48);
				*((ref $ArrayType$$$BY00UtagDBPROP@@) + 4) = 0;
				cpblk((ref $ArrayType$$$BY00UtagDBPROP@@) + 16, ref <Module>.DB_NULLID, 32);
				$ArrayType$$$BY00UtagDBPROP@@ = 160;
				*((ref $ArrayType$$$BY00UtagDBPROP@@) + 48) = 8;
				*((ref $ArrayType$$$BY00UtagDBPROP@@) + 56) = ccomBSTR;
				$ArrayType$$$BY00UtagDBPROPSET@@ $ArrayType$$$BY00UtagDBPROPSET@@ = ref $ArrayType$$$BY00UtagDBPROP@@;
				*((ref $ArrayType$$$BY00UtagDBPROPSET@@) + 8) = 1;
				cpblk((ref $ArrayType$$$BY00UtagDBPROPSET@@) + 12, ref <Module>.DBPROPSET_DBINIT, 16);
				CComPtr<IDBProperties> ccomPtr<IDBProperties> = 0L;
				try
				{
					long num = *dbInit;
					Utils.ThrowErrorIfHrFailed(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), (IntPtr)num, ref <Module>._GUID_0c733a8a_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<IDBProperties>), (IntPtr)(*(*num))), "Failed to get IDBProperties");
					Utils.ThrowErrorIfHrFailed(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),tagDBPROPSET* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), ccomPtr<IDBProperties>, 1, ref $ArrayType$$$BY00UtagDBPROPSET@@, (IntPtr)(*(*ccomPtr<IDBProperties> + 40L))), "Failed to set connection properties");
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IDBProperties>.{dtor}), (void*)(&ccomPtr<IDBProperties>));
					throw;
				}
				if (ccomPtr<IDBProperties> != null)
				{
					uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IDBProperties>, (IntPtr)(*(*ccomPtr<IDBProperties> + 16L)));
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR));
				throw;
			}
			<Module>.SysFreeString(ccomBSTR);
			GC.KeepAlive(this);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006050 File Offset: 0x00005450
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~Connection();
			}
			else
			{
				try
				{
					this.!Connection();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006518 File Offset: 0x00005918
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000609C File Offset: 0x0000549C
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x0400020F RID: 527
		private unsafe CDataSource* _connection;

		// Token: 0x04000210 RID: 528
		private string _connectionString;

		// Token: 0x04000211 RID: 529
		private unsafe IMsolapTracer* _tracer;
	}
}
