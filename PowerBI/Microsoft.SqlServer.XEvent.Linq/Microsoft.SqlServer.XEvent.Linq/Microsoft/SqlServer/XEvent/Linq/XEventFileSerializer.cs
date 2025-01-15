using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using <CppImplementationDetails>;
using Microsoft.SqlServer.XEvent.Linq.Internal;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x0200008F RID: 143
	public class XEventFileSerializer : IEventSerializer, IDisposable
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00013D1C File Offset: 0x00013D1C
		public unsafe XEventFileSerializer(string fileName)
		{
			this.m_currentFileIsInvalid = false;
			this.m_serializerPolicy = null;
			if (<Module>.XE_API.IsEnginePresent() == null)
			{
				<Module>.XE_MinApi.StaticInit();
			}
			this.m_currentFileName = fileName;
			ref byte ptr = fileName;
			if ((ref ptr) != null)
			{
				ptr = (long)RuntimeHelpers.OffsetToStringData + (ref ptr);
			}
			ref char ptr2 = ref ptr;
			LogCreateDelegate logCreateDelegate = new LogCreateDelegate(this.OnLogCreate);
			GCHandle gchandle = GCHandle.Alloc(logCreateDelegate);
			this.m_logCreateDelegateHandle = gchandle;
			IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate<LogCreateDelegate>(logCreateDelegate);
			functionPointerForDelegate.ToPointer();
			$ArrayType$$$BY01UXECustomizableAttribute@@ $ArrayType$$$BY01UXECustomizableAttribute@@;
			cpblk(ref $ArrayType$$$BY01UXECustomizableAttribute@@, ref <Module>.XET_PTR, 4);
			*((ref $ArrayType$$$BY01UXECustomizableAttribute@@) + 8) = ref <Module>.??_C@_1DA@KBKPOKBL@?$AAS?$AAe?$AAr?$AAi?$AAa?$AAl?$AAi?$AAz?$AAe?$AAr?$AAP?$AAo?$AAl?$AAi?$AAc@;
			*((ref $ArrayType$$$BY01UXECustomizableAttribute@@) + 16) = <Module>.XE_VariantLoad<void\u0020*>((void*)functionPointerForDelegate);
			initblk((ref $ArrayType$$$BY01UXECustomizableAttribute@@) + 24, 0, 32L);
			cpblk((ref $ArrayType$$$BY01UXECustomizableAttribute@@) + 56, ref <Module>.XET_WSTR, 4);
			*((ref $ArrayType$$$BY01UXECustomizableAttribute@@) + 64) = <Module>.?XE_File_LogFileName@XE_FileTargetParams@@2QEB_WEB;
			*((ref $ArrayType$$$BY01UXECustomizableAttribute@@) + 72) = <Module>.XE_VariantLoad<wchar_t\u0020const\u0020*>(ref ptr2);
			initblk((ref $ArrayType$$$BY01UXECustomizableAttribute@@) + 80, 0, 32L);
			this.m_serializedGenerations = new Hashtable();
			this.m_currentGen = new XEventMetadataSerTracker();
			this.m_currentGen.m_gen = new XEventInteropMetadataGeneration(null);
			XEventSerializerMessageHandler* ptr3 = <Module>.@new(8UL);
			XEventSerializerMessageHandler* ptr4;
			if (ptr3 != null)
			{
				initblk(ptr3, 0, 8L);
				*(long*)ptr3 = ref <Module>.??_7XE_ILogRWMessageHandler@@6B@;
				try
				{
					*(long*)ptr3 = ref <Module>.??_7XE_ILogWriteMessageHandler@@6B@;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(XE_ILogRWMessageHandler.{dtor}), (void*)ptr3);
					throw;
				}
				try
				{
					*(long*)ptr3 = ref <Module>.??_7XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@6B@;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(XE_ILogWriteMessageHandler.{dtor}), (void*)ptr3);
					throw;
				}
				ptr4 = ptr3;
			}
			else
			{
				ptr4 = null;
			}
			this.m_messageHandler = (XE_ILogWriteMessageHandler*)ptr4;
			XEventInteropMetadataAdapter* ptr5 = <Module>.@new(40UL);
			XEventInteropMetadataAdapter* ptr6;
			try
			{
				if (ptr5 != null)
				{
					ptr6 = <Module>.Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.{ctor}(ptr5, this.m_currentGen.m_gen);
				}
				else
				{
					ptr6 = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr5, 40UL);
				throw;
			}
			this.m_mdInterop = ptr6;
			this.m_pWriter = <Module>.XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.Create(null, 2, ref $ArrayType$$$BY01UXECustomizableAttribute@@, this.m_messageHandler, 1, ptr6, null);
			void* ptr7 = <Module>.new[](2097152UL);
			this.m_data = (byte*)ptr7;
			this.m_bufferHeader = (XEBufferHeader*)ptr7;
			XEBuffer* ptr8 = <Module>.@new(40UL);
			XEBuffer* ptr9;
			if (ptr8 != null)
			{
				initblk(ptr8, 0, 40L);
				ptr9 = ptr8;
			}
			else
			{
				ptr9 = null;
			}
			this.m_buffer = ptr9;
			<Module>.PrepareBufferForSerializer(ptr9, this.m_bufferHeader, 2097152U);
			<Module>.?A0x938327a2.FormatLogBufferHeader((XEBuffer*)this.m_buffer);
			this.m_mdBufferHeader = <Module>.XE_DeserializedBuffer.GetLogMetadataHeader(this.m_buffer);
			this.m_currentGen = null;
			this.m_nextEventOffset = *(long*)(this.m_buffer + 24L / (long)sizeof(XEBuffer));
			this.m_currentFileIsInvalid = true;
			GC.KeepAlive(this);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000140A0 File Offset: 0x000140A0
		private void ~XEventFileSerializer()
		{
			this.!XEventFileSerializer();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00013FB4 File Offset: 0x00013FB4
		private unsafe void !XEventFileSerializer()
		{
			GC.SuppressFinalize(this);
			try
			{
				this.Close();
			}
			finally
			{
				<Module>.XE_Delete<class\u0020XE_LogWriter<class\u0020XE_FileWriter<class\u0020Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<class\u0020XE_FileWriterDefaultPolicy<1,0>\u0020>,class\u0020Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>\u0020>(this.m_pWriter);
				this.m_pWriter = null;
				<Module>.delete((void*)this.m_data, 1UL);
				this.m_data = null;
				<Module>.delete((void*)this.m_buffer, 40UL);
				this.m_buffer = null;
				XE_ILogWriteMessageHandler* messageHandler = this.m_messageHandler;
				if (messageHandler != null)
				{
					void* ptr = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32), messageHandler, 1U, (IntPtr)(*(*(long*)messageHandler)));
				}
				this.m_messageHandler = null;
				XEventInteropMetadataAdapter* mdInterop = this.m_mdInterop;
				if (mdInterop != null)
				{
					<Module>.Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.__delDtor(mdInterop, 1U);
				}
				this.m_mdInterop = null;
				if (this.m_logCreateDelegateHandle.IsAllocated)
				{
					this.m_logCreateDelegateHandle.Free();
				}
				if (this.m_currentFileIsInvalid)
				{
					File.Delete(this.m_currentFileName);
				}
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00013A88 File Offset: 0x00013A88
		public unsafe virtual void SerializeObjectData(IMetadataGeneration generation, IntPtr rawEvent, uint rawEventLength)
		{
			XEventInteropMetadataGeneration xeventInteropMetadataGeneration = (XEventInteropMetadataGeneration)generation;
			XEventMetadataSerTracker xeventMetadataSerTracker = this.m_currentGen;
			int num;
			if (xeventMetadataSerTracker != null && xeventInteropMetadataGeneration == xeventMetadataSerTracker.m_gen)
			{
				xeventMetadataSerTracker = this.m_currentGen;
				if (xeventInteropMetadataGeneration.GenerationRevision <= xeventMetadataSerTracker.m_highestRev)
				{
					num = 0;
					goto IL_0037;
				}
			}
			num = 1;
			IL_0037:
			bool flag = (byte)num != 0;
			int num2;
			if ((xeventMetadataSerTracker == null || xeventInteropMetadataGeneration == xeventMetadataSerTracker.m_gen || this.IsBufferEmpty()) && rawEventLength + this.m_nextEventOffset == this.m_data + 2097152L)
			{
				num2 = 0;
			}
			else
			{
				num2 = 1;
			}
			bool flag2 = (byte)num2 != 0;
			if (flag2 && !this.FlushBuffer())
			{
				throw new EventFileIOException(string.Format(Resources.GetString("FileWriteExceptionString"), Marshal.GetLastWin32Error(), this.m_currentFileName));
			}
			XE_MetadataSerializer xe_MetadataSerializer;
			if (flag)
			{
				KeyValuePair<Guid, ushort> generationId = xeventInteropMetadataGeneration.GenerationId;
				*(short*)(this.m_mdBufferHeader + 16L / (long)sizeof(XE_LogDefaultMetadataHeader)) = (short)generationId.Value;
				IntPtr intPtr = (IntPtr)((void*)this.m_mdBufferHeader);
				Marshal.StructureToPtr<Guid>(generationId.Key, intPtr, false);
				XEventMetadataSerTracker xeventMetadataSerTracker2 = (XEventMetadataSerTracker)this.m_serializedGenerations[generationId];
				if (xeventMetadataSerTracker2 == null || xeventMetadataSerTracker2.m_highestRev < xeventInteropMetadataGeneration.GenerationRevision)
				{
					<Module>.Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.SetActiveGeneration(this.m_mdInterop, xeventInteropMetadataGeneration);
					<Module>.XE_MetadataSerializer.{ctor}(ref xe_MetadataSerializer);
					try
					{
						int generationRevision = xeventInteropMetadataGeneration.GenerationRevision;
						XEBufferHeader* ptr;
						if (<Module>.XE_MetadataSerializer.SerializeAllToBuffer<class\u0020Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>(ref xe_MetadataSerializer, 512U, (XE_LogDefaultMetadataHeader*)this.m_mdBufferHeader, this.m_mdInterop, &ptr, null) == null)
						{
							goto IL_01C6;
						}
						uint* ptr2 = <Module>.XE_MetadataSerializer.GetDataLength(ref xe_MetadataSerializer);
						<Module>.XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.WriteBufferDirect(this.m_pWriter, ptr, (uint)(*ptr2));
						if (xeventMetadataSerTracker2 == null)
						{
							xeventMetadataSerTracker2 = new XEventMetadataSerTracker();
							xeventMetadataSerTracker2.m_gen = xeventInteropMetadataGeneration;
							xeventMetadataSerTracker2.m_highestRev = generationRevision;
							this.m_serializedGenerations[generationId] = xeventMetadataSerTracker2;
						}
						else
						{
							xeventMetadataSerTracker2.m_highestRev = generationRevision;
						}
						this.m_currentFileIsInvalid = false;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(XE_MetadataSerializer.{dtor}), (void*)(&xe_MetadataSerializer));
						throw;
					}
					<Module>.XE_AutoRg<unsigned\u0020char>.{dtor}((ref xe_MetadataSerializer) + 8);
					goto IL_01BD;
					IL_01C6:
					try
					{
						object[] array = new object[0];
						throw new EventFileIOException(string.Format(Resources.GetString("MetadataSerializationExceptionString"), array));
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(XE_MetadataSerializer.{dtor}), (void*)(&xe_MetadataSerializer));
						throw;
					}
					goto IL_01F6;
				}
				IL_01BD:
				this.m_currentGen = xeventMetadataSerTracker2;
			}
			IL_01F6:
			cpblk(this.m_nextEventOffset, rawEvent.ToPointer(), (ulong)rawEventLength);
			this.m_nextEventOffset = rawEventLength + this.m_nextEventOffset;
			GC.KeepAlive(this);
			try
			{
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(XE_MetadataSerializer.{dtor}), (void*)(&xe_MetadataSerializer));
				throw;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000138D8 File Offset: 0x000138D8
		[return: MarshalAs(UnmanagedType.U1)]
		private unsafe bool FlushBuffer()
		{
			XE_LogBufferHeader* ptr = (XE_LogBufferHeader*)(this.m_bufferHeader + 12L / (long)sizeof(XEBufferHeader));
			uint num = (uint)(this.m_nextEventOffset - *(long*)(this.m_buffer + 24L / (long)sizeof(XEBuffer)));
			*(int*)(ptr + 12L / (long)sizeof(XE_LogBufferHeader)) = (int)num;
			*(int*)(this.m_buffer + 32L / (long)sizeof(XEBuffer)) = (int)num;
			int num2 = <Module>.XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.WriteBufferDirect(this.m_pWriter, this.m_bufferHeader, (uint)(*(int*)(ptr + 12L / (long)sizeof(XE_LogBufferHeader))));
			this.m_nextEventOffset = *(long*)(this.m_buffer + 24L / (long)sizeof(XEBuffer));
			GC.KeepAlive(this);
			return ((num2 != 0) ? 1 : 0) != 0;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0001395C File Offset: 0x0001395C
		[return: MarshalAs(UnmanagedType.U1)]
		private unsafe bool IsBufferEmpty()
		{
			return ((this.m_nextEventOffset == *(long*)(this.m_buffer + 24L / (long)sizeof(XEBuffer))) ? 1 : 0) != 0;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00013984 File Offset: 0x00013984
		private unsafe void Close()
		{
			if (this.m_nextEventOffset != *(long*)(this.m_buffer + 24L / (long)sizeof(XEBuffer)))
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000139B4 File Offset: 0x000139B4
		[return: MarshalAs(UnmanagedType.U1)]
		private unsafe bool OnLogCreate(char* logPath)
		{
			this.m_currentFileName = new string(logPath);
			this.m_currentFileIsInvalid = true;
			this.m_currentGen = null;
			this.m_serializedGenerations.Clear();
			return true;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000140BC File Offset: 0x000140BC
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!XEventFileSerializer();
			}
			else
			{
				try
				{
					this.!XEventFileSerializer();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00014124 File Offset: 0x00014124
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00014108 File Offset: 0x00014108
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x04000215 RID: 533
		private static uint BUFFER_SIZE = 2097152U;

		// Token: 0x04000216 RID: 534
		private bool m_currentFileIsInvalid;

		// Token: 0x04000217 RID: 535
		private unsafe XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>* m_pWriter;

		// Token: 0x04000218 RID: 536
		private unsafe byte* m_data;

		// Token: 0x04000219 RID: 537
		private unsafe XEBuffer* m_buffer;

		// Token: 0x0400021A RID: 538
		private unsafe XEventInteropMetadataAdapter* m_mdInterop;

		// Token: 0x0400021B RID: 539
		private unsafe XE_ILogWriteMessageHandler* m_messageHandler;

		// Token: 0x0400021C RID: 540
		private unsafe XEBufferHeader* m_bufferHeader;

		// Token: 0x0400021D RID: 541
		private unsafe XE_LogDefaultMetadataHeader* m_mdBufferHeader;

		// Token: 0x0400021E RID: 542
		private unsafe byte* m_nextEventOffset;

		// Token: 0x0400021F RID: 543
		private unsafe XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* m_serializerPolicy;

		// Token: 0x04000220 RID: 544
		private XEventMetadataSerTracker m_currentGen;

		// Token: 0x04000221 RID: 545
		private Hashtable m_serializedGenerations;

		// Token: 0x04000222 RID: 546
		private string m_currentFileName;

		// Token: 0x04000223 RID: 547
		private GCHandle m_logCreateDelegateHandle;
	}
}
