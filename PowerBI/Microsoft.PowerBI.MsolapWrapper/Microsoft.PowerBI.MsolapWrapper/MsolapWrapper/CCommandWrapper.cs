using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using ATL;
using msclr;

namespace MsolapWrapper
{
	// Token: 0x02000003 RID: 3
	internal class CCommandWrapper : IDisposable
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00004FF0 File Offset: 0x000043F0
		public unsafe CCommandWrapper()
		{
			CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* ptr = <Module>.@new(192UL);
			CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* ptr4;
			try
			{
				if (ptr != null)
				{
					initblk(ptr, 0, 192L);
					<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.{ctor}(ptr);
					try
					{
						CCommandBase* ptr2 = ptr + 168L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>);
						*ptr2 = 0L;
						try
						{
							*(ptr2 + 8L) = 0L;
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<ICommand>.{dtor}), ptr2);
							throw;
						}
						try
						{
							CMultipleResults* ptr3 = ptr + 184L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>);
							*ptr3 = 0L;
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(ATL.CCommandBase.{dtor}), (void*)(ptr + 168L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>)));
							throw;
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.{dtor}), (void*)ptr);
						throw;
					}
					ptr4 = ptr;
				}
				else
				{
					ptr4 = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr, 192UL);
				throw;
			}
			this._command = ptr4;
			this._isCancelled = false;
			this._lock = new object();
			GC.KeepAlive(this);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00001FC4 File Offset: 0x000013C4
		public unsafe CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* GetCommand()
		{
			return this._command;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000511C File Offset: 0x0000451C
		public unsafe int OpenWithMultipleResultsOnly(CSession* session, string wszCommand, tagDBPROPSET* pPropSet, uint ulPropSets)
		{
			ref byte ptr = wszCommand;
			if ((ref ptr) != null)
			{
				ptr = (long)RuntimeHelpers.OffsetToStringData + (ref ptr);
			}
			ref char ptr2 = ref ptr;
			int num = <Module>.ATL.CCommandBase.Create(this._command + 168L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>), session, ref ptr2, ref <Module>.DBGUID_DEFAULT);
			if (num < 0)
			{
				GC.KeepAlive(this);
				return num;
			}
			if (this._isCancelled)
			{
				GC.KeepAlive(this);
				return -2147217842;
			}
			num = this.ExecuteMultipleResults(null, pPropSet, null, ulPropSets);
			if (num < 0)
			{
				GC.KeepAlive(this);
				return num;
			}
			CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* command = this._command;
			if (*(long*)(command + 72L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>)) == 0L)
			{
				GC.KeepAlive(this);
				return num;
			}
			int num2 = <Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Bind(command);
			GC.KeepAlive(this);
			return num2;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002AB4 File Offset: 0x00001EB4
		public unsafe int Cancel()
		{
			@lock @lock = null;
			this._isCancelled = true;
			int num = 0;
			@lock lock2 = new @lock(this._lock);
			try
			{
				@lock = lock2;
				CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* command = this._command;
				if (command != null)
				{
					long num2 = *(long*)(command + 168L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>));
					if (num2 != 0L)
					{
						long num3 = num2;
						num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num3, (IntPtr)(*(*num3 + 24L)));
					}
				}
				GC.KeepAlive(this);
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			return num;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004B58 File Offset: 0x00003F58
		public void Close()
		{
			@lock @lock = null;
			if (this._command != null)
			{
				@lock lock2 = new @lock(this._lock);
				try
				{
					@lock = lock2;
					<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Close(this._command);
				}
				catch
				{
					((IDisposable)@lock).Dispose();
					throw;
				}
				((IDisposable)@lock).Dispose();
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000030F8 File Offset: 0x000024F8
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool IsOpen()
		{
			CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* command = this._command;
			int num;
			if (command != null && ((((*(long*)(command + 168L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>)) == 0L) ? 1 : 0) == 0) ? 1 : 0) != 0)
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

		// Token: 0x060000E8 RID: 232 RVA: 0x000051F8 File Offset: 0x000045F8
		private void ~CCommandWrapper()
		{
			this.Close();
			this.!CCommandWrapper();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000051B8 File Offset: 0x000045B8
		private unsafe void !CCommandWrapper()
		{
			CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* command = this._command;
			if (command != null)
			{
				CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* ptr = command;
				<Module>.ATL.CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>.{dtor}(ptr);
				<Module>.delete((void*)ptr, 192UL);
				this._command = null;
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004DD8 File Offset: 0x000041D8
		private unsafe int ExecuteMultipleResults(tagDBPARAMS* pParams, tagDBPROPSET* pPropSet, long* pRowsAffected, uint ulPropSets)
		{
			int num;
			if (pPropSet != null)
			{
				CComPtr<ICommandProperties> ccomPtr<ICommandProperties> = 0L;
				try
				{
					_NoAddRefReleaseOnCComPtr<ICommand>* ptr = *(long*)(this._command + 168L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>));
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>._GUID_0c733a79_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<ICommandProperties>), (IntPtr)(*(*(long*)ptr)));
					if (num < 0)
					{
						GC.KeepAlive(this);
					}
					else
					{
						num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),tagDBPROPSET* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), ccomPtr<ICommandProperties>, ulPropSets, pPropSet, (IntPtr)(*(*ccomPtr<ICommandProperties> + 32L)));
						if (num >= 0)
						{
							goto IL_0084;
						}
						GC.KeepAlive(this);
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<ICommandProperties>.{dtor}), (void*)(&ccomPtr<ICommandProperties>));
					throw;
				}
				if (ccomPtr<ICommandProperties> != null)
				{
					uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<ICommandProperties>, (IntPtr)(*(*ccomPtr<ICommandProperties> + 16L)));
				}
				return num;
				IL_0084:
				if (ccomPtr<ICommandProperties> != null)
				{
					uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<ICommandProperties>, (IntPtr)(*(*ccomPtr<ICommandProperties> + 16L)));
				}
			}
			long num4;
			long* ptr2 = ((pRowsAffected != null) ? pRowsAffected : (&num4));
			CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* command = this._command;
			long num5 = *(long*)(command + 168L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>));
			IMultipleResults** ptr3 = (IMultipleResults**)(command + 184L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>));
			num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),tagDBPARAMS*,System.Int64*,IUnknown**), (IntPtr)num5, null, ref <Module>._GUID_0c733a90_2a1c_11ce_ade5_00aa0044773d, pParams, ptr2, (IUnknown**)ptr3, (IntPtr)(*(*num5 + 32L)));
			if (num >= 0)
			{
				num = <Module>.ATL.CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>.GetNextResult(this._command, ptr2, false);
				if (num >= 0)
				{
					<Module>.ATL.CRowset<ATL::CDynamicAccessor>.SetupOptionalRowsetInterfaces(this._command + 72L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>));
				}
			}
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005218 File Offset: 0x00004618
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.Close();
				this.!CCommandWrapper();
			}
			else
			{
				try
				{
					this.!CCommandWrapper();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005288 File Offset: 0x00004688
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000526C File Offset: 0x0000466C
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x04000076 RID: 118
		private unsafe CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* _command;

		// Token: 0x04000077 RID: 119
		private bool _isCancelled;

		// Token: 0x04000078 RID: 120
		private object _lock;
	}
}
