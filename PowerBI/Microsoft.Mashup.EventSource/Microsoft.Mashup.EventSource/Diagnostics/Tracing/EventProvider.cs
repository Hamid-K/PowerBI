using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Diagnostics.Contracts.Internal;
using Microsoft.Win32;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000013 RID: 19
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal class EventProvider : IDisposable
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002A41 File Offset: 0x00000C41
		internal EventProvider()
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A4C File Offset: 0x00000C4C
		[SecurityCritical]
		internal void Register(Guid providerGuid)
		{
			this.m_providerId = providerGuid;
			this.m_etwCallback = new UnsafeNativeMethods.ManifestEtw.EtwEnableCallback(this.EtwEnableCallBack);
			uint num = this.EventRegister(ref this.m_providerId, this.m_etwCallback);
			if (num != 0U)
			{
				throw new ArgumentException(Win32Native.GetMessage((int)num));
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A94 File Offset: 0x00000C94
		[SecurityCritical]
		internal unsafe int SetInformation(UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS eventInfoClass, void* data, int dataSize)
		{
			int num = 50;
			if (!EventProvider.m_setInformationMissing)
			{
				try
				{
					num = UnsafeNativeMethods.ManifestEtw.EventSetInformation(this.m_regHandle, eventInfoClass, data, dataSize);
				}
				catch (TypeLoadException)
				{
					EventProvider.m_setInformationMissing = true;
				}
			}
			return num;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AE8 File Offset: 0x00000CE8
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_enabled = false;
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (!this.m_disposed)
				{
					this.Deregister();
					this.m_disposed = true;
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B48 File Offset: 0x00000D48
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B50 File Offset: 0x00000D50
		~EventProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B80 File Offset: 0x00000D80
		[SecurityCritical]
		private void Deregister()
		{
			if (this.m_regHandle != 0L)
			{
				this.EventUnregister();
				this.m_regHandle = 0L;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B9C File Offset: 0x00000D9C
		[SecurityCritical]
		private unsafe void EtwEnableCallBack([In] ref Guid sourceId, [In] int controlCode, [In] byte setLevel, [In] long anyKeyword, [In] long allKeyword, [In] UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, [In] void* callbackContext)
		{
			try
			{
				ControllerCommand controllerCommand = ControllerCommand.Update;
				IDictionary<string, string> dictionary = null;
				bool flag = false;
				if (controlCode == 1)
				{
					this.m_enabled = true;
					this.m_level = setLevel;
					this.m_anyKeywordMask = anyKeyword;
					this.m_allKeywordMask = allKeyword;
				}
				else if (controlCode == 0)
				{
					this.m_enabled = false;
					this.m_level = 0;
					this.m_anyKeywordMask = 0L;
					this.m_allKeywordMask = 0L;
				}
				else
				{
					if (controlCode != 2)
					{
						return;
					}
					controllerCommand = ControllerCommand.SendManifest;
				}
				if (!flag)
				{
					this.OnControllerCommand(controllerCommand, dictionary, 0, 0);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C24 File Offset: 0x00000E24
		protected virtual void OnControllerCommand(ControllerCommand command, IDictionary<string, string> arguments, int sessionId, int etwSessionId)
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002C26 File Offset: 0x00000E26
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002C2E File Offset: 0x00000E2E
		protected EventLevel Level
		{
			get
			{
				return (EventLevel)this.m_level;
			}
			set
			{
				this.m_level = (byte)value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002C38 File Offset: 0x00000E38
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002C40 File Offset: 0x00000E40
		protected EventKeywords MatchAnyKeyword
		{
			get
			{
				return (EventKeywords)this.m_anyKeywordMask;
			}
			set
			{
				this.m_anyKeywordMask = (long)value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002C49 File Offset: 0x00000E49
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002C51 File Offset: 0x00000E51
		protected EventKeywords MatchAllKeyword
		{
			get
			{
				return (EventKeywords)this.m_allKeywordMask;
			}
			set
			{
				this.m_allKeywordMask = (long)value;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C5A File Offset: 0x00000E5A
		private static int FindNull(byte[] buffer, int idx)
		{
			while (idx < buffer.Length && buffer[idx] != 0)
			{
				idx++;
			}
			return idx;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C70 File Offset: 0x00000E70
		[SecurityCritical]
		private unsafe bool GetDataFromController(int etwSessionId, UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, out ControllerCommand command, out byte[] data, out int dataStart)
		{
			data = null;
			dataStart = 0;
			if (filterData != null)
			{
				if (filterData->Ptr != 0L && 0 < filterData->Size && filterData->Size <= 1024)
				{
					data = new byte[filterData->Size];
					Marshal.Copy((IntPtr)filterData->Ptr, data, 0, data.Length);
				}
				command = (ControllerCommand)filterData->Type;
				return true;
			}
			string text = "\\Microsoft\\Windows\\CurrentVersion\\Winevt\\Publishers\\{";
			Guid providerId = this.m_providerId;
			string text2 = text + providerId.ToString() + "}";
			if (Marshal.SizeOf(typeof(IntPtr)) == 8)
			{
				text2 = "HKEY_LOCAL_MACHINE\\Software\\Wow6432Node" + text2;
			}
			else
			{
				text2 = "HKEY_LOCAL_MACHINE\\Software" + text2;
			}
			string text3 = "ControllerData_Session_" + etwSessionId.ToString(CultureInfo.InvariantCulture);
			new RegistryPermission(RegistryPermissionAccess.Read, text2).Assert();
			data = Registry.GetValue(text2, text3, null) as byte[];
			if (data != null)
			{
				command = ControllerCommand.Update;
				return true;
			}
			command = ControllerCommand.Update;
			return false;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D6D File Offset: 0x00000F6D
		public bool IsEnabled()
		{
			return this.m_enabled;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D75 File Offset: 0x00000F75
		public bool IsEnabled(byte level, long keywords)
		{
			return this.m_enabled && ((level <= this.m_level || this.m_level == 0) && (keywords == 0L || ((keywords & this.m_anyKeywordMask) != 0L && (keywords & this.m_allKeywordMask) == this.m_allKeywordMask)));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DB2 File Offset: 0x00000FB2
		public static EventProvider.WriteEventErrorCode GetLastWriteEventError()
		{
			return EventProvider.s_returnCode;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DB9 File Offset: 0x00000FB9
		private static void SetLastError(int error)
		{
			if (error != 8)
			{
				if (error == 234 || error == 534)
				{
					EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
					return;
				}
			}
			else
			{
				EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.NoFreeBuffers;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DDC File Offset: 0x00000FDC
		[SecurityCritical]
		private unsafe static object EncodeObject(ref object data, ref EventProvider.EventData* dataDescriptor, ref byte* dataBuffer, ref uint totalEventSize)
		{
			string text;
			byte[] array;
			for (;;)
			{
				dataDescriptor.Reserved = 0U;
				text = data as string;
				array = null;
				if (text != null)
				{
					break;
				}
				if ((array = data as byte[]) != null)
				{
					goto Block_1;
				}
				if (data is IntPtr)
				{
					goto Block_2;
				}
				if (data is int)
				{
					goto Block_3;
				}
				if (data is long)
				{
					goto Block_4;
				}
				if (data is uint)
				{
					goto Block_5;
				}
				if (data is ulong)
				{
					goto Block_6;
				}
				if (data is char)
				{
					goto Block_7;
				}
				if (data is byte)
				{
					goto Block_8;
				}
				if (data is short)
				{
					goto Block_9;
				}
				if (data is sbyte)
				{
					goto Block_10;
				}
				if (data is ushort)
				{
					goto Block_11;
				}
				if (data is float)
				{
					goto Block_12;
				}
				if (data is double)
				{
					goto Block_13;
				}
				if (data is bool)
				{
					goto Block_14;
				}
				if (data is Guid)
				{
					goto Block_16;
				}
				if (data is decimal)
				{
					goto Block_17;
				}
				if (data is DateTime)
				{
					goto Block_18;
				}
				if (!(data is Enum))
				{
					goto IL_040C;
				}
				Type underlyingType = Enum.GetUnderlyingType(data.GetType());
				if (underlyingType == typeof(int))
				{
					data = ((IConvertible)data).ToInt32(null);
				}
				else
				{
					if (!(underlyingType == typeof(long)))
					{
						goto IL_040C;
					}
					data = ((IConvertible)data).ToInt64(null);
				}
			}
			dataDescriptor.Size = (uint)((text.Length + 1) * 2);
			goto IL_0431;
			Block_1:
			*dataBuffer = array.Length;
			dataDescriptor.Ptr = (ulong)dataBuffer;
			dataDescriptor.Size = 4U;
			totalEventSize += dataDescriptor.Size;
			dataDescriptor += (IntPtr)sizeof(EventProvider.EventData);
			dataBuffer += 16;
			dataDescriptor.Size = (uint)array.Length;
			goto IL_0431;
			Block_2:
			dataDescriptor.Size = (uint)sizeof(IntPtr);
			IntPtr* ptr = dataBuffer;
			*ptr = (IntPtr)data;
			dataDescriptor.Ptr = ptr;
			goto IL_0431;
			Block_3:
			dataDescriptor.Size = 4U;
			int* ptr2 = dataBuffer;
			*ptr2 = (int)data;
			dataDescriptor.Ptr = ptr2;
			goto IL_0431;
			Block_4:
			dataDescriptor.Size = 8U;
			long* ptr3 = dataBuffer;
			*ptr3 = (long)data;
			dataDescriptor.Ptr = ptr3;
			goto IL_0431;
			Block_5:
			dataDescriptor.Size = 4U;
			uint* ptr4 = dataBuffer;
			*ptr4 = (uint)data;
			dataDescriptor.Ptr = ptr4;
			goto IL_0431;
			Block_6:
			dataDescriptor.Size = 8U;
			ulong* ptr5 = dataBuffer;
			*ptr5 = (ulong)data;
			dataDescriptor.Ptr = ptr5;
			goto IL_0431;
			Block_7:
			dataDescriptor.Size = 2U;
			char* ptr6 = dataBuffer;
			*ptr6 = (char)data;
			dataDescriptor.Ptr = ptr6;
			goto IL_0431;
			Block_8:
			dataDescriptor.Size = 1U;
			byte* ptr7 = dataBuffer;
			*ptr7 = (byte)data;
			dataDescriptor.Ptr = ptr7;
			goto IL_0431;
			Block_9:
			dataDescriptor.Size = 2U;
			short* ptr8 = dataBuffer;
			*ptr8 = (short)data;
			dataDescriptor.Ptr = ptr8;
			goto IL_0431;
			Block_10:
			dataDescriptor.Size = 1U;
			sbyte* ptr9 = dataBuffer;
			*ptr9 = (sbyte)data;
			dataDescriptor.Ptr = ptr9;
			goto IL_0431;
			Block_11:
			dataDescriptor.Size = 2U;
			ushort* ptr10 = dataBuffer;
			*ptr10 = (ushort)data;
			dataDescriptor.Ptr = ptr10;
			goto IL_0431;
			Block_12:
			dataDescriptor.Size = 4U;
			float* ptr11 = dataBuffer;
			*ptr11 = (float)data;
			dataDescriptor.Ptr = ptr11;
			goto IL_0431;
			Block_13:
			dataDescriptor.Size = 8U;
			double* ptr12 = dataBuffer;
			*ptr12 = (double)data;
			dataDescriptor.Ptr = ptr12;
			goto IL_0431;
			Block_14:
			dataDescriptor.Size = 4U;
			int* ptr13 = dataBuffer;
			if ((bool)data)
			{
				*ptr13 = 1;
			}
			else
			{
				*ptr13 = 0;
			}
			dataDescriptor.Ptr = ptr13;
			goto IL_0431;
			Block_16:
			dataDescriptor.Size = (uint)sizeof(Guid);
			Guid* ptr14 = dataBuffer;
			*ptr14 = (Guid)data;
			dataDescriptor.Ptr = ptr14;
			goto IL_0431;
			Block_17:
			dataDescriptor.Size = 16U;
			decimal* ptr15 = dataBuffer;
			*ptr15 = (decimal)data;
			dataDescriptor.Ptr = ptr15;
			goto IL_0431;
			Block_18:
			long num = 0L;
			if (((DateTime)data).Ticks > 504911232000000000L)
			{
				num = ((DateTime)data).ToFileTimeUtc();
			}
			dataDescriptor.Size = 8U;
			long* ptr16 = dataBuffer;
			*ptr16 = num;
			dataDescriptor.Ptr = ptr16;
			goto IL_0431;
			IL_040C:
			if (data == null)
			{
				text = "";
			}
			else
			{
				text = data.ToString();
			}
			dataDescriptor.Size = (uint)((text.Length + 1) * 2);
			IL_0431:
			totalEventSize += dataDescriptor.Size;
			dataDescriptor += (IntPtr)sizeof(EventProvider.EventData);
			dataBuffer += 16;
			return text ?? array;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003240 File Offset: 0x00001440
		[SecurityCritical]
		internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* childActivityID, params object[] eventPayload)
		{
			int num = 0;
			if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				int num2 = eventPayload.Length;
				if (num2 > 32)
				{
					EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.TooManyArgs;
					return false;
				}
				uint num3 = 0U;
				int i = 0;
				List<int> list = new List<int>(8);
				List<object> list2 = new List<object>(8);
				EventProvider.EventData* ptr;
				EventProvider.EventData* ptr2;
				checked
				{
					ptr = stackalloc EventProvider.EventData[unchecked((UIntPtr)(2 * num2)) * (UIntPtr)sizeof(EventProvider.EventData)];
					ptr2 = ptr;
				}
				byte* ptr3 = stackalloc byte[(UIntPtr)(32 * num2)];
				bool flag = false;
				for (int j = 0; j < eventPayload.Length; j++)
				{
					if (eventPayload[j] == null)
					{
						EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.NullInput;
						return false;
					}
					object obj = EventProvider.EncodeObject(ref eventPayload[j], ref ptr2, ref ptr3, ref num3);
					if (obj != null)
					{
						int num4 = (int)((long)(ptr2 - ptr) - 1L);
						if (!(obj is string))
						{
							if (eventPayload.Length + num4 + 1 - j > 32)
							{
								EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.TooManyArgs;
								return false;
							}
							flag = true;
						}
						list2.Add(obj);
						list.Add(num4);
						i++;
					}
				}
				num2 = (int)((long)(ptr2 - ptr));
				if (num3 > 65482U)
				{
					EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
					return false;
				}
				if (!flag && i < 8)
				{
					while (i < 8)
					{
						list2.Add(null);
						i++;
					}
					fixed (string text = (string)list2[0])
					{
						char* ptr4 = text;
						if (ptr4 != null)
						{
							ptr4 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text2 = (string)list2[1])
						{
							char* ptr5 = text2;
							if (ptr5 != null)
							{
								ptr5 += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (string text3 = (string)list2[2])
							{
								char* ptr6 = text3;
								if (ptr6 != null)
								{
									ptr6 += RuntimeHelpers.OffsetToStringData / 2;
								}
								fixed (string text4 = (string)list2[3])
								{
									char* ptr7 = text4;
									if (ptr7 != null)
									{
										ptr7 += RuntimeHelpers.OffsetToStringData / 2;
									}
									fixed (string text5 = (string)list2[4])
									{
										char* ptr8 = text5;
										if (ptr8 != null)
										{
											ptr8 += RuntimeHelpers.OffsetToStringData / 2;
										}
										fixed (string text6 = (string)list2[5])
										{
											char* ptr9 = text6;
											if (ptr9 != null)
											{
												ptr9 += RuntimeHelpers.OffsetToStringData / 2;
											}
											fixed (string text7 = (string)list2[6])
											{
												char* ptr10 = text7;
												if (ptr10 != null)
												{
													ptr10 += RuntimeHelpers.OffsetToStringData / 2;
												}
												fixed (string text8 = (string)list2[7])
												{
													char* ptr11 = text8;
													if (ptr11 != null)
													{
														ptr11 += RuntimeHelpers.OffsetToStringData / 2;
													}
													ptr2 = ptr;
													if (list2[0] != null)
													{
														ptr2[list[0]].Ptr = ptr4;
													}
													if (list2[1] != null)
													{
														ptr2[list[1]].Ptr = ptr5;
													}
													if (list2[2] != null)
													{
														ptr2[list[2]].Ptr = ptr6;
													}
													if (list2[3] != null)
													{
														ptr2[list[3]].Ptr = ptr7;
													}
													if (list2[4] != null)
													{
														ptr2[list[4]].Ptr = ptr8;
													}
													if (list2[5] != null)
													{
														ptr2[list[5]].Ptr = ptr9;
													}
													if (list2[6] != null)
													{
														ptr2[list[6]].Ptr = ptr10;
													}
													if (list2[7] != null)
													{
														ptr2[list[7]].Ptr = ptr11;
													}
													num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, num2, ptr);
													text = null;
													text2 = null;
													text3 = null;
													text4 = null;
													text5 = null;
													text6 = null;
													text7 = null;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					ptr2 = ptr;
					GCHandle[] array = new GCHandle[i];
					for (int k = 0; k < i; k++)
					{
						array[k] = GCHandle.Alloc(list2[k], GCHandleType.Pinned);
						if (list2[k] is string)
						{
							fixed (string text8 = (string)list2[k])
							{
								char* ptr12 = text8;
								if (ptr12 != null)
								{
									ptr12 += RuntimeHelpers.OffsetToStringData / 2;
								}
								ptr2[list[k]].Ptr = ptr12;
							}
						}
						else
						{
							byte[] array2;
							byte* ptr13;
							if ((array2 = (byte[])list2[k]) == null || array2.Length == 0)
							{
								ptr13 = null;
							}
							else
							{
								ptr13 = &array2[0];
							}
							ptr2[list[k]].Ptr = ptr13;
							array2 = null;
						}
					}
					num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, num2, ptr);
					for (int l = 0; l < i; l++)
					{
						array[l].Free();
					}
				}
			}
			if (num != 0)
			{
				EventProvider.SetLastError(num);
				return false;
			}
			return true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000036FC File Offset: 0x000018FC
		[SecurityCritical]
		protected internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* childActivityID, int dataCount, IntPtr data)
		{
			if (childActivityID != null)
			{
				Contract.Assert(eventDescriptor.Opcode == 9 || eventDescriptor.Opcode == 240 || eventDescriptor.Opcode == 1 || eventDescriptor.Opcode == 2);
			}
			int num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, dataCount, (EventProvider.EventData*)(void*)data);
			if (num != 0)
			{
				EventProvider.SetLastError(num);
				return false;
			}
			return true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003764 File Offset: 0x00001964
		[SecurityCritical]
		internal unsafe bool WriteEventRaw(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* relatedActivityID, int dataCount, IntPtr data)
		{
			int num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, relatedActivityID, dataCount, (EventProvider.EventData*)(void*)data);
			if (num != 0)
			{
				EventProvider.SetLastError(num);
				return false;
			}
			return true;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003795 File Offset: 0x00001995
		[SecurityCritical]
		private uint EventRegister(ref Guid providerId, UnsafeNativeMethods.ManifestEtw.EtwEnableCallback enableCallback)
		{
			this.m_providerId = providerId;
			this.m_etwCallback = enableCallback;
			return UnsafeNativeMethods.ManifestEtw.EventRegister(ref providerId, enableCallback, null, ref this.m_regHandle);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000037B9 File Offset: 0x000019B9
		[SecurityCritical]
		private uint EventUnregister()
		{
			uint num = UnsafeNativeMethods.ManifestEtw.EventUnregister(this.m_regHandle);
			this.m_regHandle = 0L;
			return num;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000037D0 File Offset: 0x000019D0
		private static int bitcount(uint n)
		{
			int num = 0;
			while (n != 0U)
			{
				num += EventProvider.nibblebits[(int)(n & 15U)];
				n >>= 4;
			}
			return num;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000037F8 File Offset: 0x000019F8
		private static int bitindex(uint n)
		{
			Contract.Assert(EventProvider.bitcount(n) == 1);
			int num = 0;
			while (((ulong)n & (ulong)(1L << (num & 31))) == 0UL)
			{
				num++;
			}
			return num;
		}

		// Token: 0x04000025 RID: 37
		private static bool m_setInformationMissing;

		// Token: 0x04000026 RID: 38
		[SecurityCritical]
		private UnsafeNativeMethods.ManifestEtw.EtwEnableCallback m_etwCallback;

		// Token: 0x04000027 RID: 39
		private long m_regHandle;

		// Token: 0x04000028 RID: 40
		private byte m_level;

		// Token: 0x04000029 RID: 41
		private long m_anyKeywordMask;

		// Token: 0x0400002A RID: 42
		private long m_allKeywordMask;

		// Token: 0x0400002B RID: 43
		private bool m_enabled;

		// Token: 0x0400002C RID: 44
		private Guid m_providerId;

		// Token: 0x0400002D RID: 45
		internal bool m_disposed;

		// Token: 0x0400002E RID: 46
		[ThreadStatic]
		private static EventProvider.WriteEventErrorCode s_returnCode;

		// Token: 0x0400002F RID: 47
		private const int s_basicTypeAllocationBufferSize = 16;

		// Token: 0x04000030 RID: 48
		private const int s_etwMaxNumberArguments = 32;

		// Token: 0x04000031 RID: 49
		private const int s_etwAPIMaxRefObjCount = 8;

		// Token: 0x04000032 RID: 50
		private const int s_maxEventDataDescriptors = 128;

		// Token: 0x04000033 RID: 51
		private const int s_traceEventMaximumSize = 65482;

		// Token: 0x04000034 RID: 52
		private const int s_traceEventMaximumStringSize = 32724;

		// Token: 0x04000035 RID: 53
		private static int[] nibblebits = new int[]
		{
			0, 1, 1, 2, 1, 2, 2, 3, 1, 2,
			2, 3, 2, 3, 3, 4
		};

		// Token: 0x02000080 RID: 128
		public struct EventData
		{
			// Token: 0x0400018F RID: 399
			internal ulong Ptr;

			// Token: 0x04000190 RID: 400
			internal uint Size;

			// Token: 0x04000191 RID: 401
			internal uint Reserved;
		}

		// Token: 0x02000081 RID: 129
		public struct SessionInfo
		{
			// Token: 0x060002FB RID: 763 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
			internal SessionInfo(int sessionIdBit_, int etwSessionId_)
			{
				this.sessionIdBit = sessionIdBit_;
				this.etwSessionId = etwSessionId_;
			}

			// Token: 0x04000192 RID: 402
			internal int sessionIdBit;

			// Token: 0x04000193 RID: 403
			internal int etwSessionId;
		}

		// Token: 0x02000082 RID: 130
		public enum WriteEventErrorCode
		{
			// Token: 0x04000195 RID: 405
			NoError,
			// Token: 0x04000196 RID: 406
			NoFreeBuffers,
			// Token: 0x04000197 RID: 407
			EventTooBig,
			// Token: 0x04000198 RID: 408
			NullInput,
			// Token: 0x04000199 RID: 409
			TooManyArgs,
			// Token: 0x0400019A RID: 410
			Other
		}
	}
}
