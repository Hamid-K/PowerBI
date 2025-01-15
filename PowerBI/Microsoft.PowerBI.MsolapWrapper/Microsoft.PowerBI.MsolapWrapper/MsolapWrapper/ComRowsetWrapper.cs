using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using ATL;

namespace MsolapWrapper
{
	// Token: 0x0200007E RID: 126
	internal class ComRowsetWrapper : ICAccessorWrapper, IDisposable
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00007E10 File Offset: 0x00007210
		private unsafe ComRowsetWrapper(CComPtr<IRowset>* existingRowset)
		{
			try
			{
				base..ctor();
				CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* ptr = <Module>.@new(168UL);
				CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* ptr2;
				try
				{
					if (ptr != null)
					{
						ptr2 = <Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.{ctor}(ptr);
					}
					else
					{
						ptr2 = null;
					}
				}
				catch
				{
					<Module>.delete((void*)ptr, 168UL);
					throw;
				}
				this._accessor = ptr2;
				CComPtr<IRowset> ccomPtr<IRowset> = *(long*)existingRowset;
				if (ccomPtr<IRowset> != null)
				{
					uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IRowset>, (IntPtr)(*(*ccomPtr<IRowset> + 8L)));
				}
				CChapterBulkRowset<ATL::CDynamicAccessor>* ptr3;
				try
				{
					ptr3 = this._accessor + 72L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IRowset>.{dtor}), (void*)(&ccomPtr<IRowset>));
					throw;
				}
				try
				{
					<Module>.ATL.CComPtr<IRowset>.=(ptr3, ref ccomPtr<IRowset>);
					*(ptr3 + 80L) = 1;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IRowset>.{dtor}), (void*)(&ccomPtr<IRowset>));
					throw;
				}
				if (ccomPtr<IRowset> != null)
				{
					uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IRowset>, (IntPtr)(*(*ccomPtr<IRowset> + 16L)));
				}
				*(int*)(this._accessor + 60L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>)) = 1;
				GC.KeepAlive(this);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IRowset>.{dtor}), (void*)existingRowset);
				throw;
			}
			long num3 = *(long*)existingRowset;
			if (num3 != 0L)
			{
				long num4 = num3;
				uint num5 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num4, (IntPtr)(*(*num4 + 16L)));
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007F6C File Offset: 0x0000736C
		public unsafe static ComRowsetWrapper CreateAndBind(CComPtr<IRowset>* existingRowset)
		{
			ComRowsetWrapper comRowsetWrapper;
			try
			{
				CComPtr<IRowset> ccomPtr<IRowset> = *(long*)existingRowset;
				if (ccomPtr<IRowset> != null)
				{
					uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IRowset>, (IntPtr)(*(*ccomPtr<IRowset> + 8L)));
				}
				comRowsetWrapper = new ComRowsetWrapper((CComPtr<IRowset>*)(&ccomPtr<IRowset>));
				Utils.ThrowErrorIfHrFailed(<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Bind(comRowsetWrapper.GetAccessor()), "Expected to bind using existing rowset.");
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IRowset>.{dtor}), (void*)existingRowset);
				throw;
			}
			long num2 = *(long*)existingRowset;
			if (num2 != 0L)
			{
				long num3 = num2;
				uint num4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num3, (IntPtr)(*(*num3 + 16L)));
			}
			return comRowsetWrapper;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006D00 File Offset: 0x00006100
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe virtual bool IsOpen()
		{
			CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* accessor = this._accessor;
			int num;
			if (accessor != null && *(long*)(accessor + 72L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>)) != 0L)
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

		// Token: 0x060001A4 RID: 420 RVA: 0x00007BE8 File Offset: 0x00006FE8
		public unsafe virtual void Close()
		{
			CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* accessor = this._accessor;
			if (accessor != null)
			{
				<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Close(accessor);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000063A0 File Offset: 0x000057A0
		public unsafe virtual CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* GetAccessor()
		{
			return this._accessor;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000062C8 File Offset: 0x000056C8
		[return: MarshalAs(UnmanagedType.U1)]
		public virtual bool SupportsNextResult()
		{
			return false;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006D34 File Offset: 0x00006134
		[return: MarshalAs(UnmanagedType.U1)]
		public virtual bool NextResult()
		{
			this.IsOpen();
			return false;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008194 File Offset: 0x00007594
		private unsafe void !ComRowsetWrapper()
		{
			CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* accessor = this._accessor;
			if (accessor != null)
			{
				CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* ptr = accessor;
				<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.{dtor}(ptr);
				<Module>.delete((void*)ptr, 168UL);
				this._accessor = null;
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000081D4 File Offset: 0x000075D4
		private void ~ComRowsetWrapper()
		{
			this.Close();
			this.!ComRowsetWrapper();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008264 File Offset: 0x00007664
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.Close();
				this.!ComRowsetWrapper();
			}
			else
			{
				try
				{
					this.!ComRowsetWrapper();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000082F4 File Offset: 0x000076F4
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000082B8 File Offset: 0x000076B8
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x04000194 RID: 404
		private unsafe CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* _accessor;
	}
}
