using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using ATL;

namespace MsolapWrapper
{
	// Token: 0x0200007C RID: 124
	internal class CDbSchemaRowsetWrapper : ICAccessorWrapper, IDisposable
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00007CD0 File Offset: 0x000070D0
		public unsafe CDbSchemaRowsetWrapper(Connection connection)
		{
			CDbSchemaRowset* ptr = <Module>.@new(176UL);
			CDbSchemaRowset* ptr4;
			try
			{
				if (ptr != null)
				{
					CDataSource* connection2 = connection.GetConnection();
					<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.{ctor}(ptr);
					try
					{
						CDbSchemaRowset* ptr2 = ptr + 168L / (long)sizeof(CDbSchemaRowset);
						CDataSource* ptr3 = ptr2;
						*ptr3 = 0L;
						try
						{
							<Module>.ATL.CComPtr<IDBInitialize>.=(ptr2, connection2);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(ATL.CDataSource.{dtor}), (void*)(ptr + 168L / (long)sizeof(CDbSchemaRowset)));
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
				<Module>.delete((void*)ptr, 176UL);
				throw;
			}
			this._accessor = ptr4;
			GC.KeepAlive(this);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007948 File Offset: 0x00006D48
		public unsafe void ExecuteSchemaRowset(Guid schema, object[] restrictions, PropertySetCollection propSets)
		{
			if (restrictions == null)
			{
				WrapperContract.Fail("Restrictions must be specified for schema call");
			}
			ulong num = (ulong)((long)restrictions.Length);
			tagVARIANT* ptr = <Module>.new[]((num > 768614336404564650UL) ? ulong.MaxValue : (num * 24UL));
			int num2 = 0;
			if (0 < restrictions.Length)
			{
				do
				{
					tagVARIANT* ptr2 = (long)num2 * 24L + ptr / sizeof(tagVARIANT);
					<Module>.VariantInit(ptr2);
					IntPtr intPtr = (IntPtr)((void*)ptr2);
					Marshal.GetNativeVariantForObject(restrictions[num2], intPtr);
					num2++;
				}
				while (num2 < restrictions.Length);
			}
			tagDBPROPSET* ptr3 = propSets.ToDbPropSet();
			_GUID guid = Utils.ToGUID(schema);
			int num3 = <Module>.MsolapWrapper.CDbSchemaRowset.GetRowset(this._accessor, guid, ptr, restrictions.Length, ptr3, propSets.Size);
			int num4 = 0;
			if (0 < restrictions.Length)
			{
				do
				{
					<Module>.VariantClear((long)num4 * 24L + ptr / sizeof(tagVARIANT));
					num4++;
				}
				while (num4 < restrictions.Length);
			}
			<Module>.delete[]((void*)ptr);
			Utils.ThrowErrorIfHrFailed(num3, "Failure encountered while getting schema");
			GC.KeepAlive(this);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006BD4 File Offset: 0x00005FD4
		public unsafe ulong GetSchemaRestrictionMask(Guid schema)
		{
			CSchemasWrapper cschemasWrapper = 0;
			*((ref cschemasWrapper) + 8) = 0L;
			*((ref cschemasWrapper) + 16) = 0L;
			ulong num2;
			try
			{
				Utils.ThrowErrorIfHrFailed(<Module>.MsolapWrapper.CDbSchemaRowset.GetSchemas(this._accessor, (uint*)(&cschemasWrapper), (ref cschemasWrapper) + 8, (ref cschemasWrapper) + 16), "Failure encountered while getting schemas restrictions");
				_GUID guid = Utils.ToGUID(schema);
				uint num = 0;
				if (0 < cschemasWrapper)
				{
					do
					{
						_GUID* ptr = num * 16UL + (ulong)(*((ref cschemasWrapper) + 8));
						if (((<Module>.IsEqualGUID(ref guid, ptr) != 0) ? 1 : 0) != 0)
						{
							goto IL_006D;
						}
						num++;
					}
					while (num < cschemasWrapper);
					goto IL_0084;
					IL_006D:
					GC.KeepAlive(this);
					num2 = (ulong)(*(num * 4UL + (ulong)(*((ref cschemasWrapper) + 16))));
					goto IL_00B5;
				}
				IL_0084:
				Utils.ThrowError(WrapperErrorSource.PowerBI, "Schema {0} was not found", schema);
				GC.KeepAlive(this);
				num2 = ulong.MaxValue;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(MsolapWrapper.CSchemasWrapper.{dtor}), (void*)(&cschemasWrapper));
				throw;
			}
			IL_00B5:
			<Module>.CoTaskMemFree(*((ref cschemasWrapper) + 8));
			<Module>.CoTaskMemFree(*((ref cschemasWrapper) + 16));
			return num2;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006CD0 File Offset: 0x000060D0
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe virtual bool IsOpen()
		{
			byte b = ((*(long*)(this._accessor + 72L / (long)sizeof(CDbSchemaRowset)) != 0L) ? 1 : 0);
			GC.KeepAlive(this);
			return b != 0;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007BBC File Offset: 0x00006FBC
		public unsafe virtual void Close()
		{
			CDbSchemaRowset* accessor = this._accessor;
			if (accessor != null)
			{
				<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Close(accessor);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000062AC File Offset: 0x000056AC
		public unsafe virtual CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* GetAccessor()
		{
			return (CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>*)this._accessor;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000062C8 File Offset: 0x000056C8
		[return: MarshalAs(UnmanagedType.U1)]
		public virtual bool SupportsNextResult()
		{
			return false;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000062C8 File Offset: 0x000056C8
		[return: MarshalAs(UnmanagedType.U1)]
		public virtual bool NextResult()
		{
			return false;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008134 File Offset: 0x00007534
		private unsafe void !CDbSchemaRowsetWrapper()
		{
			CDbSchemaRowset* accessor = this._accessor;
			if (accessor != null)
			{
				CDbSchemaRowset* ptr = accessor;
				<Module>.MsolapWrapper.CDbSchemaRowset.{dtor}(ptr);
				<Module>.delete((void*)ptr, 176UL);
				this._accessor = null;
			}
			GC.KeepAlive(this);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008174 File Offset: 0x00007574
		private void ~CDbSchemaRowsetWrapper()
		{
			this.Close();
			this.!CDbSchemaRowsetWrapper();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000081F4 File Offset: 0x000075F4
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.Close();
				this.!CDbSchemaRowsetWrapper();
			}
			else
			{
				try
				{
					this.!CDbSchemaRowsetWrapper();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000082D4 File Offset: 0x000076D4
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008248 File Offset: 0x00007648
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x04000192 RID: 402
		private unsafe CDbSchemaRowset* _accessor;
	}
}
