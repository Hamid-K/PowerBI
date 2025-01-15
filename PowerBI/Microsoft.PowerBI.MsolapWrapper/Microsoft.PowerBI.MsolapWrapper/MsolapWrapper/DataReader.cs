using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using ATL;

namespace MsolapWrapper
{
	// Token: 0x0200007B RID: 123
	public class DataReader : IDisposable
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00007A24 File Offset: 0x00006E24
		public void Close()
		{
			ICAccessorWrapper accessor = this._accessor;
			if (accessor != null)
			{
				if (accessor.SupportsNextResult())
				{
					if (!this._accessor.IsOpen())
					{
						goto IL_0058;
					}
					try
					{
						do
						{
							try
							{
								while (this.MoveNext())
								{
								}
							}
							catch (MsolapWrapperException)
							{
							}
						}
						while (this.NextResult());
						goto IL_0058;
					}
					catch (MsolapWrapperException)
					{
						goto IL_0058;
					}
				}
				if (this._accessor.IsOpen())
				{
					try
					{
						while (this.MoveNext())
						{
						}
					}
					catch (MsolapWrapperException)
					{
					}
				}
				IL_0058:
				this._accessor.Close();
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000060B8 File Offset: 0x000054B8
		[return: MarshalAs(UnmanagedType.U1)]
		public bool IsOpen()
		{
			return this._accessor.IsOpen();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000763C File Offset: 0x00006A3C
		public string[] GetColumnNames()
		{
			uint num = this.InternalGetColumnCount();
			uint num2;
			if (num > 0U && this._hasBookmarks)
			{
				num2 = num - 1U;
			}
			else
			{
				num2 = num;
			}
			string[] array = new string[num2];
			uint num3 = 0U;
			if (0U < num2)
			{
				do
				{
					array[(int)num3] = this.GetColumnName(num3);
					num3 += 1U;
				}
				while (num3 < num2);
			}
			return array;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006A0C File Offset: 0x00005E0C
		public uint GetColumnCount()
		{
			uint num = this.InternalGetColumnCount();
			if (num > 0U && this._hasBookmarks)
			{
				return num - 1U;
			}
			return num;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006A38 File Offset: 0x00005E38
		public uint GetOrdinal(string name)
		{
			this._accessor.GetAccessor();
			uint num;
			if (this.GetOrdinalCaseInsensitive(name, ref num))
			{
				GC.KeepAlive(this);
				return num - 1U;
			}
			Utils.ThrowError(WrapperErrorSource.PowerBI, "The column wasn't found in the result set");
			GC.KeepAlive(this);
			return 0U;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006D84 File Offset: 0x00006184
		public unsafe string GetColumnName(uint index)
		{
			this._accessor.GetAccessor();
			ulong num = this.NormalizeColumnIndex(index);
			CDynamicAccessor* accessor = this._accessor.GetAccessor();
			char* ptr;
			if (<Module>.ATL.CDynamicAccessor.TranslateColumnNo(accessor, ref num) != null)
			{
				ptr = *(num * 80UL + (ulong)(*(accessor + 40L)));
				if (ptr != null)
				{
					goto IL_0076;
				}
			}
			else
			{
				ptr = null;
			}
			uint num2 = this.InternalGetColumnCount();
			uint num3;
			if (num2 > 0U && this._hasBookmarks)
			{
				num3 = num2 - 1U;
			}
			else
			{
				num3 = num2;
			}
			Utils.ThrowError(WrapperErrorSource.PowerBI, "The column at index {0} wasn't found in the result set. Column count {1}", index, num3);
			IL_0076:
			GC.KeepAlive(this);
			return new string(ptr);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006E1C File Offset: 0x0000621C
		public Type GetColumnType(uint index)
		{
			this._accessor.GetAccessor();
			ulong num = this.NormalizeColumnIndex(index);
			ushort num2 = this.InternalGetColumnType(num);
			int num3 = (int)num2;
			if (num3 <= 16512)
			{
				if (num3 == 16512)
				{
					GC.KeepAlive(this);
					return typeof(byte[]);
				}
				switch (num3 + -2)
				{
				case 0:
					GC.KeepAlive(this);
					return typeof(short);
				case 1:
					GC.KeepAlive(this);
					return typeof(int);
				case 2:
					GC.KeepAlive(this);
					return typeof(double);
				case 3:
					GC.KeepAlive(this);
					return typeof(double);
				case 4:
					GC.KeepAlive(this);
					return typeof(decimal);
				case 5:
					GC.KeepAlive(this);
					return typeof(DateTime);
				case 9:
					GC.KeepAlive(this);
					return typeof(bool);
				case 10:
					GC.KeepAlive(this);
					return typeof(object);
				case 15:
					GC.KeepAlive(this);
					return typeof(ushort);
				case 16:
					GC.KeepAlive(this);
					return typeof(ushort);
				case 18:
					GC.KeepAlive(this);
					return typeof(long);
				case 70:
					GC.KeepAlive(this);
					return typeof(Guid);
				case 134:
					GC.KeepAlive(this);
					return typeof(ChapterHandle);
				}
			}
			else if (num3 == 16514)
			{
				GC.KeepAlive(this);
				return typeof(string);
			}
			Utils.ThrowError(WrapperErrorSource.PowerBI, "GetColumnType: encountered unsupported data type at index {0}: {1}", num, num2);
			object[] array = new object[] { num, num2 };
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "GetColumnType: Encountered unsupported data type at index {0}: {1}", array));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000768C File Offset: 0x00006A8C
		public unsafe object GetValue(uint index)
		{
			ulong num = this.NormalizeColumnIndex(index);
			uint num2;
			Utils.ThrowErrorIfHrFailed(<Module>.ATL.CDynamicAccessor.GetStatus(this._accessor.GetAccessor(), num, &num2), "Failure encountered while getting the column status for the column ordinal {0}.", num);
			if (num2 == 3)
			{
				GC.KeepAlive(this);
				return null;
			}
			if (*(int*)(this._accessor.GetAccessor() + 72L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>) + 80L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>)) == 1)
			{
				Utils.ThrowError(WrapperErrorSource.PowerBI, "Expected state ChapterSet before using chapter rowset.");
			}
			ushort num3 = this.InternalGetColumnType(num);
			ulong num4 = num;
			CDynamicAccessor* accessor = this._accessor.GetAccessor();
			void* ptr;
			if (<Module>.ATL.CDynamicAccessor.TranslateColumnNo(accessor, ref num4) != null)
			{
				ptr = *(num4 * 80UL + (ulong)(*(accessor + 40L)) + 8UL) + *(accessor + 16L);
			}
			else
			{
				ptr = null;
			}
			GC.KeepAlive(this);
			return this.ReadData(num3, ptr, num);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007FFC File Offset: 0x000073FC
		public unsafe DataReader GetChildReader(uint index)
		{
			ulong num = this.NormalizeColumnIndex(index);
			this.InternalGetColumnType(num);
			CComPtr<IParentRowset> ccomPtr<IParentRowset> = 0L;
			DataReader dataReader;
			try
			{
				IRowset* ptr = *(long*)(this._accessor.GetAccessor() + 72L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>));
				Utils.ThrowErrorIfHrFailed(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>._GUID_0c733aaa_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<IParentRowset>), (IntPtr)(*(*(long*)ptr))), "Expected to find parent rowset");
				CComPtr<IRowset> ccomPtr<IRowset> = 0L;
				try
				{
					Utils.ThrowErrorIfHrFailed(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,System.UInt64,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),IUnknown**), ccomPtr<IParentRowset>, null, num, ref <Module>.IID_IRowset, (IUnknown**)(&ccomPtr<IRowset>), (IntPtr)(*(*ccomPtr<IParentRowset> + 24L))), "Expected to find child rowset");
					CComPtr<IRowset> ccomPtr<IRowset>2 = ccomPtr<IRowset>;
					if (ccomPtr<IRowset> != null)
					{
						uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IRowset>, (IntPtr)(*(*ccomPtr<IRowset> + 8L)));
					}
					ICAccessorWrapper icaccessorWrapper = ComRowsetWrapper.CreateAndBind((CComPtr<IRowset>*)(&ccomPtr<IRowset>2));
					GC.KeepAlive(this);
					dataReader = new DataReader(icaccessorWrapper, true);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IRowset>.{dtor}), (void*)(&ccomPtr<IRowset>));
					throw;
				}
				if (ccomPtr<IRowset> != null)
				{
					uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IRowset>, (IntPtr)(*(*ccomPtr<IRowset> + 16L)));
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IParentRowset>.{dtor}), (void*)(&ccomPtr<IParentRowset>));
				throw;
			}
			if (ccomPtr<IParentRowset> != null)
			{
				uint num4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IParentRowset>, (IntPtr)(*(*ccomPtr<IParentRowset> + 16L)));
			}
			return dataReader;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007AE8 File Offset: 0x00006EE8
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool SynchronizeChapter(ChapterHandle chapterHandle)
		{
			ulong nativeKey = chapterHandle.NativeKey;
			CChapterBulkRowset<ATL::CDynamicAccessor>* ptr = this._accessor.GetAccessor() + 72L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>);
			*(ptr + 88L) = (long)nativeKey;
			*(ptr + 80L) = 2;
			GC.KeepAlive(this);
			return this.MoveFirst();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007864 File Offset: 0x00006C64
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool MoveNext()
		{
			this._accessor.IsOpen();
			if (*(long*)(this._accessor.GetAccessor() + 72L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>)) == 0L)
			{
				GC.KeepAlive(this);
				return false;
			}
			int num = <Module>.MsolapWrapper.CChapterBulkRowset<ATL::CDynamicAccessor>.MoveNext(this._accessor.GetAccessor() + 72L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>), 0L, true);
			if (num == 265926)
			{
				GC.KeepAlive(this);
				return false;
			}
			Utils.ThrowErrorIfHrFailed(num, "Failure encountered while getting the next row in the result-set");
			GC.KeepAlive(this);
			return true;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000071DC File Offset: 0x000065DC
		[return: MarshalAs(UnmanagedType.U1)]
		public bool NextResult()
		{
			bool flag = this._accessor.NextResult();
			if (flag)
			{
				this.ResetIndexToTypeMapping();
			}
			return flag;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000060D8 File Offset: 0x000054D8
		private void !DataReader()
		{
			ICAccessorWrapper accessor = this._accessor;
			if (accessor != null)
			{
				IDisposable disposable = accessor as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				this._accessor = null;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007B2C File Offset: 0x00006F2C
		private void ~DataReader()
		{
			this.Close();
			this.!DataReader();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006D50 File Offset: 0x00006150
		internal DataReader(ICAccessorWrapper accessor, [MarshalAs(UnmanagedType.U1)] bool hasBookmarks)
		{
			accessor.IsOpen();
			this._accessor = accessor;
			this._hasBookmarks = hasBookmarks;
			this.ResetIndexToTypeMapping();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000078D8 File Offset: 0x00006CD8
		[return: MarshalAs(UnmanagedType.U1)]
		private unsafe bool MoveFirst()
		{
			this._accessor.IsOpen();
			if (*(long*)(this._accessor.GetAccessor() + 72L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>)) == 0L)
			{
				GC.KeepAlive(this);
				return false;
			}
			int num = <Module>.MsolapWrapper.CChapterBulkRowset<ATL::CDynamicAccessor>.MoveFirst(this._accessor.GetAccessor() + 72L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>));
			if (num == 265926)
			{
				GC.KeepAlive(this);
				return false;
			}
			Utils.ThrowErrorIfHrFailed(num, "Failure encountered while getting the next row in the result-set");
			GC.KeepAlive(this);
			return true;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006538 File Offset: 0x00005938
		private unsafe ushort InternalGetColumnType(ulong ordinal)
		{
			int num = (int)ordinal;
			ushort num2 = this._indexToTypeMapping[num];
			ushort num3;
			if (num2 != 0)
			{
				num3 = num2;
			}
			else
			{
				ulong num4 = ordinal;
				CDynamicAccessor* accessor = this._accessor.GetAccessor();
				byte b;
				if (<Module>.ATL.CDynamicAccessor.TranslateColumnNo(accessor, ref num4) != null)
				{
					num3 = *(*(accessor + 40L) + (long)(num4 * 80UL) + 40L);
					b = 1;
				}
				else
				{
					b = 0;
				}
				Utils.ThrowErrorIfHrFailed(b, "Failure encountered while getting the column type for the column ordinal {0}.", ordinal);
				this._indexToTypeMapping[num] = num3;
			}
			GC.KeepAlive(this);
			return num3;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000069D4 File Offset: 0x00005DD4
		private unsafe uint InternalGetColumnCount()
		{
			this._accessor.GetAccessor();
			uint num = (uint)(*(long*)(this._accessor.GetAccessor() + 24L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>)));
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007208 File Offset: 0x00006608
		private unsafe object ReadData(ushort type, void* value, ulong ordinal)
		{
			if (type <= 16512)
			{
				if (type == 16512)
				{
					return this.ReadBinary(value, ordinal);
				}
				switch (type)
				{
				case 2:
					return *(short*)value;
				case 3:
					return *(int*)value;
				case 4:
					return (double)(*(float*)value);
				case 5:
					return *(double*)value;
				case 6:
					return decimal.FromOACurrency(*(long*)value);
				case 7:
					return this.ReadDate(*(double*)value);
				case 11:
					return ((*(byte*)value != 0) ? 1 : 0) != 0;
				case 12:
					return this.ReadVariant(value);
				case 17:
					return *(ushort*)value;
				case 18:
					return *(ushort*)value;
				case 20:
					return *(long*)value;
				case 72:
					return this.ReadGuid(*(_GUID*)value);
				case 136:
					return new ChapterHandle((ulong)(*(long*)value));
				}
			}
			else if (type == 16514)
			{
				return new string(*(long*)value);
			}
			Utils.ThrowError(WrapperErrorSource.PowerBI, "ReadData: encountered unsupported data type at index {0}: {1}", ordinal, type);
			return null;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000610C File Offset: 0x0000550C
		[return: MarshalAs(UnmanagedType.U1)]
		private unsafe bool GetOrdinalCaseInsensitive(string name, uint* ordinal)
		{
			ref byte ptr = name;
			if ((ref ptr) != null)
			{
				ptr = (long)RuntimeHelpers.OffsetToStringData + (ref ptr);
			}
			ref char ptr2 = ref ptr;
			int length = name.Length;
			ulong num = 0UL;
			if (0L < *(long*)(this._accessor.GetAccessor() + 24L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>)))
			{
				long num2 = 0L;
				while (*(*(long*)(this._accessor.GetAccessor() + 40L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>)) + num2) == 0L || <Module>.CompareStringOrdinal(*(*(long*)(this._accessor.GetAccessor() + 40L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>)) + num2), -1, ref ptr2, length, 1) != 2)
				{
					num += 1UL;
					num2 += 80L;
					if (num >= (ulong)(*(long*)(this._accessor.GetAccessor() + 24L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>))))
					{
						goto IL_00B0;
					}
				}
				*ordinal = (int)((uint)(*(num * 80UL + (ulong)(*(long*)(this._accessor.GetAccessor() + 40L / (long)sizeof(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>))) + 16UL)));
				GC.KeepAlive(this);
				return true;
			}
			IL_00B0:
			*ordinal = 0;
			GC.KeepAlive(this);
			return false;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000065BC File Offset: 0x000059BC
		private unsafe object ReadVariant(void* value)
		{
			ushort num = *(ushort*)value;
			switch (num)
			{
			case 0:
				return null;
			case 5:
				return *(double*)((byte*)value + 8L);
			case 6:
				return decimal.FromOACurrency(*(long*)((byte*)value + 8L));
			case 7:
				return this.ReadDate(*(double*)((byte*)value + 8L));
			case 8:
				return new string(*(long*)((byte*)value + 8L));
			case 11:
				return ((*(short*)((byte*)value + 8L) != 0) ? 1 : 0) != 0;
			case 14:
			{
				ValueType valueType = 0m;
				(decimal)valueType = new decimal(*(int*)((byte*)value + 8L), *(int*)((byte*)value + 12L), *(int*)((byte*)value + 4L), Convert.ToBoolean(((byte*)value)[3L]), ((byte*)value)[2L]);
				return valueType;
			}
			case 20:
				return *(long*)((byte*)value + 8L);
			}
			Utils.ThrowError(WrapperErrorSource.PowerBI, "ReadVariant: encountered unsupported data type: {0}", num);
			return null;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006B38 File Offset: 0x00005F38
		private unsafe object ReadBinary(void* value, ulong ordinal)
		{
			ulong num;
			Utils.ThrowErrorIfHrFailed(<Module>.ATL.CDynamicAccessor.GetLength(this._accessor.GetAccessor(), ordinal, &num), "Failure while getting the length for the binary value at index {0}.", ordinal);
			byte* ptr = *(long*)value;
			byte[] array = new byte[(int)num];
			ref byte ptr2 = ref array[0];
			int num2 = <Module>.?A0x47c6cda6.memcpy_s(ref ptr2, (long)array.Length, ptr, num);
			if (num2 != 0)
			{
				Utils.ThrowError(WrapperErrorSource.PowerBI, "Could not copy binary buffer: {0}.", num2);
			}
			GC.KeepAlive(this);
			return array;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006BAC File Offset: 0x00005FAC
		private void ResetIndexToTypeMapping()
		{
			this._indexToTypeMapping = new ushort[this.InternalGetColumnCount() + 1U];
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000061DC File Offset: 0x000055DC
		private ValueType ReadDate(double date)
		{
			ValueType valueType;
			try
			{
				valueType = DateTime.FromOADate(date);
			}
			catch (ArgumentException ex)
			{
				string text = string.Format("Invalid OADate value '{0}'. Accepted values are between -657435.0 and 2958465.99999999", date);
				Utils.ThrowError(WrapperErrorCodes.InvalidDateTimeValue, text, WrapperErrorSource.User, ex, true);
				throw;
			}
			return valueType;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006240 File Offset: 0x00005640
		private unsafe ValueType ReadGuid(_GUID guid)
		{
			ValueType valueType = default(Guid);
			(Guid)valueType = new Guid(guid, *((ref guid) + 4), *((ref guid) + 6), *((ref guid) + 8), *((ref guid) + 9), *((ref guid) + 10), *((ref guid) + 11), *((ref guid) + 12), *((ref guid) + 13), *((ref guid) + 14), *((ref guid) + 15));
			return valueType;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006A80 File Offset: 0x00005E80
		private ulong NormalizeColumnIndex(uint index)
		{
			uint num = this.InternalGetColumnCount();
			uint num2;
			if (num > 0U && this._hasBookmarks)
			{
				num2 = num - 1U;
			}
			else
			{
				num2 = num;
			}
			if (index >= num2)
			{
				uint num3 = this.InternalGetColumnCount();
				uint num4;
				if (num3 > 0U && this._hasBookmarks)
				{
					num4 = num3 - 1U;
				}
				else
				{
					num4 = num3;
				}
				Utils.ThrowError(WrapperErrorSource.PowerBI, "Column index out of bounds. Column count: {0}", num4);
			}
			return (ulong)(index + 1U);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007B4C File Offset: 0x00006F4C
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.Close();
				this.!DataReader();
			}
			else
			{
				try
				{
					this.!DataReader();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007CB0 File Offset: 0x000070B0
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007BA0 File Offset: 0x00006FA0
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x0400018F RID: 399
		private ICAccessorWrapper _accessor;

		// Token: 0x04000190 RID: 400
		private bool _hasBookmarks;

		// Token: 0x04000191 RID: 401
		private ushort[] _indexToTypeMapping;
	}
}
