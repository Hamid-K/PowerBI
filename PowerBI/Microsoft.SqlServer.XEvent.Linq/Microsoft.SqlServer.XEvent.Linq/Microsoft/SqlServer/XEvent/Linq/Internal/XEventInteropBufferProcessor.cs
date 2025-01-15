using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x02000032 RID: 50
	internal class XEventInteropBufferProcessor : IDisposable
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00010578 File Offset: 0x00010578
		internal unsafe XEventInteropBufferProcessor(XEventInteropMetadataManager metadata)
		{
			XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* ptr = <Module>.@new(56UL);
			XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* ptr2;
			try
			{
				if (ptr != null)
				{
					ptr2 = <Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{ctor}(ptr);
				}
				else
				{
					ptr2 = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr, 56UL);
				throw;
			}
			this.m_bufferWalker = ptr2;
			XEventInteropMetadataAdapter* ptr3 = <Module>.@new(40UL);
			XEventInteropMetadataAdapter* ptr4;
			try
			{
				if (ptr3 != null)
				{
					<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.{ctor}(ptr3);
					try
					{
						<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.{ctor}(ptr3 + 8L / (long)sizeof(XEventInteropMetadataAdapter));
						try
						{
							<Module>.std.vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>.{ctor}(ptr3 + 16L / (long)sizeof(XEventInteropMetadataAdapter));
							try
							{
								<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.=(ptr3, metadata);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(std.vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>.{dtor}), (void*)(ptr3 + 16L / (long)sizeof(XEventInteropMetadataAdapter)));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.{dtor}), (void*)(ptr3 + 8L / (long)sizeof(XEventInteropMetadataAdapter)));
							throw;
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.{dtor}), (void*)ptr3);
						throw;
					}
					ptr4 = ptr3;
				}
				else
				{
					ptr4 = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr3, 40UL);
				throw;
			}
			this.m_metadataAdapter = ptr4;
			this.m_metadataMgr = metadata;
			XEBuffer* ptr5 = <Module>.@new(40UL);
			this.m_buffer = ptr5;
			this.m_ticks = null;
			XE_ParserFactory<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>* ptr6 = <Module>.@new(8UL);
			XE_ParserFactory<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>* ptr7;
			try
			{
				if (ptr6 != null)
				{
					initblk(ptr6, 0, 8L);
					<Module>.XE_AutoP<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.{ctor}(ptr6, null);
					ptr7 = ptr6;
				}
				else
				{
					ptr7 = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr6, 8UL);
				throw;
			}
			this.m_pParserFactory = ptr7;
			GC.KeepAlive(this);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000114A4 File Offset: 0x000114A4
		private void ~XEventInteropBufferProcessor()
		{
			this.!XEventInteropBufferProcessor();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000113D4 File Offset: 0x000113D4
		private unsafe void !XEventInteropBufferProcessor()
		{
			<Module>.delete((void*)this.m_bufferWalker, 56UL);
			this.m_bufferWalker = null;
			XEventInteropMetadataAdapter* metadataAdapter = this.m_metadataAdapter;
			if (metadataAdapter != null)
			{
				<Module>.Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.__delDtor(metadataAdapter, 1U);
			}
			this.m_metadataAdapter = null;
			<Module>.delete((void*)this.m_buffer, 40UL);
			this.m_buffer = null;
			<Module>.delete((void*)this.m_ticks, 24UL);
			this.m_ticks = null;
			XE_ParserFactory<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>* pParserFactory = this.m_pParserFactory;
			if (pParserFactory != null)
			{
				<Module>.XE_AutoP<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.{dtor}(pParserFactory);
				<Module>.delete((void*)pParserFactory, 8UL);
			}
			this.m_pParserFactory = null;
			if (this.m_bufferHandle.IsAllocated)
			{
				this.m_bufferHandle.Free();
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00010730 File Offset: 0x00010730
		[return: MarshalAs(UnmanagedType.U1)]
		internal unsafe bool Reset(BufferLocator bufId, byte[] xebuffer, int offset)
		{
			bool flag = false;
			if (this.m_bufferHandle.IsAllocated)
			{
				this.m_bufferHandle.Free();
			}
			this.m_bufId = bufId;
			GCHandle gchandle = GCHandle.Alloc(xebuffer, GCHandleType.Pinned);
			this.m_bufferHandle = gchandle;
			if (<Module>.XE_DeserializedBuffer.GetXEBuffer((byte*)this.m_bufferHandle.AddrOfPinnedObject().ToPointer(), (uint)xebuffer.Length, this.m_buffer) != null)
			{
				XE_LogDefaultMetadataHeader* ptr = <Module>.XE_DeserializedBuffer.GetLogMetadataHeader(this.m_buffer);
				<Module>.Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.LocateMetadata(this.m_metadataAdapter, ptr);
				if (offset == 0)
				{
					flag = ((<Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.ResetSafe(this.m_bufferWalker, this.m_metadataAdapter, this.m_buffer) != 0) ? 1 : 0) != 0;
				}
				else
				{
					flag = ((<Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.SetCurrentSafe(this.m_bufferWalker, this.m_metadataAdapter, this.m_buffer, (uint)offset) != 0) ? 1 : 0) != 0;
				}
			}
			XE_TicksUtil* ticks = this.m_ticks;
			if (ticks != null)
			{
				<Module>.delete((void*)ticks, 24UL);
				this.m_ticks = null;
			}
			XE_TicksUtil* ptr2 = <Module>.@new(24UL);
			XE_TicksUtil* ptr3;
			try
			{
				if (ptr2 != null)
				{
					ptr3 = <Module>.XE_TicksUtil.{ctor}<class\u0020Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>(ptr2, (XEventInteropMetadataAdapter*)this.m_metadataAdapter);
				}
				else
				{
					ptr3 = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr2, 24UL);
				throw;
			}
			this.m_ticks = ptr3;
			GC.KeepAlive(this);
			return flag;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000EED8 File Offset: 0x0000EED8
		[return: MarshalAs(UnmanagedType.U1)]
		internal bool MoveNext()
		{
			int num;
			if (<Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.MoveNextSafe(this.m_bufferWalker) != null && <Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.HasNext(this.m_bufferWalker) != null)
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0001086C File Offset: 0x0001086C
		internal unsafe PublishedEvent Current
		{
			get
			{
				XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* ptr = <Module>.XE_ParserFactory<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.Create(this.m_pParserFactory, null, <Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.GetEventHeader(this.m_bufferWalker));
				if (ptr == null)
				{
					throw new OutOfMemoryException();
				}
				XE_EventLocation xe_EventLocation;
				<Module>.XE_EventLocation.{ctor}(ref xe_EventLocation);
				<Module>.XE_EventLocation.SetFileIndex(ref xe_EventLocation, this.m_bufId.m_fileId);
				<Module>.XE_EventLocation.SetBufferNumber(ref xe_EventLocation, this.m_bufId.m_bufferNum);
				<Module>.XE_EventLocation.SetEventOffset(ref xe_EventLocation, <Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.GetEventOffset(this.m_bufferWalker));
				EventLocator eventLocator = (EventLocator)Marshal.PtrToStructure((IntPtr)((void*)(&xe_EventLocation)), typeof(EventLocator));
				PublishedEvent publishedEvent = XEventInteropMetadataManager.MarshalEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>(<Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.GetEventHeader(this.m_bufferWalker), <Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.GetEventMd(this.m_bufferWalker), ptr, (XEventInteropMetadataAdapter*)this.m_metadataAdapter, this.m_metadataMgr, this.m_ticks, eventLocator);
				GC.KeepAlive(this);
				return publishedEvent;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000EF14 File Offset: 0x0000EF14
		internal unsafe void SerializeEvent(IEventSerializer serializationContext, IMetadataGeneration mdGen)
		{
			XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* ptr = <Module>.XE_ParserFactory<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.Create(this.m_pParserFactory, null, <Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.GetEventHeader(this.m_bufferWalker));
			if (ptr != null)
			{
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),XEEventBufferHeader modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),XEEvent modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), ptr, this.m_metadataAdapter, <Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.GetEventHeader(this.m_bufferWalker), <Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.GetEventMd(this.m_bufferWalker), (IntPtr)(*(*(long*)ptr + 8L)));
				IntPtr intPtr = (IntPtr)<Module>.XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.GetEventHeader(this.m_bufferWalker);
				IntPtr intPtr2 = intPtr;
				XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* ptr2 = ptr;
				serializationContext.SerializeObjectData(mdGen, intPtr2, calli(System.UInt32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 56L))));
				GC.KeepAlive(this);
				return;
			}
			throw new OutOfMemoryException();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000114C0 File Offset: 0x000114C0
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!XEventInteropBufferProcessor();
			}
			else
			{
				try
				{
					this.!XEventInteropBufferProcessor();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00011528 File Offset: 0x00011528
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001150C File Offset: 0x0001150C
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x040001AE RID: 430
		private unsafe XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* m_bufferWalker;

		// Token: 0x040001AF RID: 431
		private unsafe XEventInteropMetadataAdapter* m_metadataAdapter;

		// Token: 0x040001B0 RID: 432
		private XEventInteropMetadataManager m_metadataMgr;

		// Token: 0x040001B1 RID: 433
		private unsafe XE_ParserFactory<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>* m_pParserFactory;

		// Token: 0x040001B2 RID: 434
		private GCHandle m_bufferHandle;

		// Token: 0x040001B3 RID: 435
		private unsafe XEBuffer* m_buffer;

		// Token: 0x040001B4 RID: 436
		private BufferLocator m_bufId;

		// Token: 0x040001B5 RID: 437
		private unsafe XE_TicksUtil* m_ticks;
	}
}
