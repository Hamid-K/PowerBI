using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using <CppImplementationDetails>;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x02000033 RID: 51
	internal class XEventInteropFileReader : IDisposable
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00010DE0 File Offset: 0x00010DE0
		internal unsafe XEventInteropFileReader(string[] fileList, string[] mdFiles)
		{
			if (<Module>.XE_API.IsEnginePresent() == null)
			{
				<Module>.XE_MinApi.StaticInit();
			}
			this.m_fileDecoder = null;
			this.m_mdDecoder = null;
			this.m_nativeMd = null;
			ref byte ptr = XEventInteropFileReader.FileListToXePartionPath(fileList);
			if ((ref ptr) != null)
			{
				ptr = (long)RuntimeHelpers.OffsetToStringData + (ref ptr);
			}
			ref char ptr2 = ref ptr;
			ref byte ptr3 = XEventInteropFileReader.FileListToXePartionPath(mdFiles);
			if ((ref ptr3) != null)
			{
				ptr3 = (long)RuntimeHelpers.OffsetToStringData + (ref ptr3);
			}
			ref char ptr4 = ref ptr3;
			$ArrayType$$$BY02UXECustomizableAttribute@@ $ArrayType$$$BY02UXECustomizableAttribute@@;
			cpblk(ref $ArrayType$$$BY02UXECustomizableAttribute@@, ref <Module>.XET_WSTR, 4);
			*((ref $ArrayType$$$BY02UXECustomizableAttribute@@) + 8) = <Module>.?XE_File_LogFileName@XE_FileTargetParams@@2QEB_WEB;
			*((ref $ArrayType$$$BY02UXECustomizableAttribute@@) + 16) = <Module>.XE_VariantLoad<wchar_t\u0020const\u0020*>(ref ptr2);
			initblk((ref $ArrayType$$$BY02UXECustomizableAttribute@@) + 24, 0, 32L);
			cpblk((ref $ArrayType$$$BY02UXECustomizableAttribute@@) + 56, ref <Module>.XET_WSTR, 4);
			*((ref $ArrayType$$$BY02UXECustomizableAttribute@@) + 64) = <Module>.?XE_File_MetadataFileName@XE_FileTargetParams@@2QEB_WEB;
			*((ref $ArrayType$$$BY02UXECustomizableAttribute@@) + 72) = <Module>.XE_VariantLoad<wchar_t\u0020const\u0020*>(ref ptr4);
			initblk((ref $ArrayType$$$BY02UXECustomizableAttribute@@) + 80, 0, 32L);
			cpblk((ref $ArrayType$$$BY02UXECustomizableAttribute@@) + 112, ref <Module>.XET_INT32, 4);
			*((ref $ArrayType$$$BY02UXECustomizableAttribute@@) + 120) = <Module>.?XE_File_SortOptions@XE_FileTargetParams@@2QEB_WEB;
			*((ref $ArrayType$$$BY02UXECustomizableAttribute@@) + 128) = <Module>.XE_VariantLoad<enum\u0020XE_BufferMap::SortOptions>((XE_BufferMap.SortOptions)1);
			initblk((ref $ArrayType$$$BY02UXECustomizableAttribute@@) + 136, 0, 32L);
			XEventFileReaderMessageHandler* ptr5 = <Module>.@new(16UL);
			XEventFileReaderMessageHandler* ptr7;
			try
			{
				if (ptr5 != null)
				{
					*(long*)ptr5 = ref <Module>.??_7XE_ILogRWMessageHandler@@6B@;
					try
					{
						*(long*)ptr5 = ref <Module>.??_7XE_ILogReadMessageHandler@@6B@;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(XE_ILogRWMessageHandler.{dtor}), (void*)ptr5);
						throw;
					}
					try
					{
						*(long*)ptr5 = ref <Module>.??_7XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@6B@;
						XEventFileReaderMessageHandler* ptr6 = ptr5 + 8L / (long)sizeof(XEventFileReaderMessageHandler);
						<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>.{ctor}(ptr6);
						try
						{
							<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>.=(ptr6, this);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>.{dtor}), (void*)(ptr5 + 8L / (long)sizeof(XEventFileReaderMessageHandler)));
							throw;
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(XE_ILogReadMessageHandler.{dtor}), (void*)ptr5);
						throw;
					}
					ptr7 = ptr5;
				}
				else
				{
					ptr7 = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr5, 16UL);
				throw;
			}
			this.m_messageHandler = (XE_ILogReadMessageHandler*)ptr7;
			this.m_metaData = new XEventInteropMetadataManager();
			this.m_positionLock = new Mutex();
			XEEngineServicesAPI* ptr8 = <Module>.XE_API.ServiceAPI();
			XE_FileReader<XE_FileReaderDefaultPolicy>* ptr9 = <Module>.@new(72UL, ptr8, null);
			XE_FileReader<XE_FileReaderDefaultPolicy>* ptr10;
			try
			{
				if (ptr9 != null)
				{
					ptr10 = <Module>.XE_FileReader<XE_FileReaderDefaultPolicy>.{ctor}(ptr9, <Module>.XE_BufferMap.Create());
				}
				else
				{
					ptr10 = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr9, ptr8, null);
				throw;
			}
			this.m_fileDecoder = ptr10;
			if (ptr10 != null)
			{
				<Module>.XE_FileReader<XE_FileReaderDefaultPolicy>.SetIsSequentialScan(ptr10, 0);
				XE_LogDefaultMetadataDecoder* ptr11 = <Module>.@new(32UL);
				XE_LogDefaultMetadataDecoder* ptr12;
				try
				{
					if (ptr11 != null)
					{
						ptr12 = <Module>.XE_LogDefaultMetadataDecoder.{ctor}(ptr11, (XE_IDecoder*)this.m_fileDecoder);
					}
					else
					{
						ptr12 = null;
					}
				}
				catch
				{
					<Module>.delete((void*)ptr11, 32UL);
					throw;
				}
				this.m_mdDecoder = ptr12;
				XE_MetadataStore* ptr13 = <Module>.@new(56UL);
				XE_MetadataStore* ptr14;
				try
				{
					if (ptr13 != null)
					{
						ptr14 = <Module>.XE_MetadataStore.{ctor}(ptr13, this.m_messageHandler);
					}
					else
					{
						ptr14 = null;
					}
				}
				catch
				{
					<Module>.delete((void*)ptr13, 56UL);
					throw;
				}
				this.m_nativeMd = ptr14;
				XE_LogDefaultMetadataDecoder* mdDecoder = this.m_mdDecoder;
				if (calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16,XECustomizableAttribute modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),XE_ILogReadMessageHandler*,XE_MetadataStore*,FCBFileDirectives*,XE_ILogReadAccessHandler* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), mdDecoder, 3, ref $ArrayType$$$BY02UXECustomizableAttribute@@, this.m_messageHandler, ptr14, null, 0L, (IntPtr)(*(*(long*)mdDecoder + 8L))) == null)
				{
					string text = Marshal.PtrToStringUni((IntPtr)(ref ptr2));
					uint lastError = <Module>.GetLastError();
					throw new EventFileIOException(string.Format(Resources.GetString("UnknownFileOpenExceptionString"), lastError, text), lastError, text);
				}
				if (*<Module>.XE_FileReader<XE_FileReaderDefaultPolicy>.GetFileCount(this.m_fileDecoder) > 32767)
				{
					throw new EventFileIOException(string.Format(Resources.GetString("TooManyFilesExceptionString"), 32767));
				}
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00011180 File Offset: 0x00011180
		private void ~XEventInteropFileReader()
		{
			XEventInteropMetadataManager metaData = this.m_metaData;
			if (metaData != null)
			{
				((IDisposable)metaData).Dispose();
			}
			this.m_metaData = null;
			this.!XEventInteropFileReader();
			this.m_disposed = true;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00010930 File Offset: 0x00010930
		private unsafe void !XEventInteropFileReader()
		{
			XE_LogDefaultMetadataDecoder* mdDecoder = this.m_mdDecoder;
			if (mdDecoder != null)
			{
				void* ptr = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32), mdDecoder, 1U, (IntPtr)(*(*(long*)mdDecoder)));
			}
			this.m_mdDecoder = null;
			XE_MetadataStore* nativeMd = this.m_nativeMd;
			if (nativeMd != null)
			{
				<Module>.XE_MetadataStore.{dtor}(nativeMd);
				<Module>.delete((void*)nativeMd, 56UL);
			}
			this.m_nativeMd = null;
			XE_ILogReadMessageHandler* messageHandler = this.m_messageHandler;
			if (messageHandler != null)
			{
				void* ptr2 = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32), messageHandler, 1U, (IntPtr)(*(*(long*)messageHandler)));
			}
			this.m_messageHandler = null;
			GC.KeepAlive(this);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000E364 File Offset: 0x0000E364
		internal bool Disposed
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_disposed;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000F008 File Offset: 0x0000F008
		internal ReadOnlyCollection<IMetadataGeneration> MetadataGenerations
		{
			get
			{
				return this.m_metaData.MetadataGenerations;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000F028 File Offset: 0x0000F028
		internal XEventInteropMetadataManager InteropMetadata
		{
			get
			{
				return this.m_metaData;
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00010A8C File Offset: 0x00010A8C
		internal byte[] GetBuffer(BufferLocator bufLoc)
		{
			BufferLocator bufferLocator = null;
			byte[] array = null;
			uint num;
			if (this.BufferLocationToBufferNumber(bufLoc, ref num))
			{
				bufferLocator = null;
				array = this.ReadBuffer(num, out bufferLocator);
			}
			return array;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00010ABC File Offset: 0x00010ABC
		internal byte[] GetNextBuffer(BufferLocator prevBuf, out BufferLocator bufLoc)
		{
			byte[] array = null;
			bufLoc = null;
			uint num;
			if (this.BufferLocationToBufferNumber(prevBuf, ref num))
			{
				num += 1U;
				array = this.ReadBuffer(num, out bufLoc);
			}
			return array;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00010AEC File Offset: 0x00010AEC
		internal byte[] GetFirstBuf(out BufferLocator bufLoc)
		{
			return this.ReadBuffer(0U, out bufLoc);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000111B8 File Offset: 0x000111B8
		internal unsafe void OnPackageDeserialize(XEPackageMetadata* packageMd, XE_LogDefaultMetadataPackageHeader* mdHeader)
		{
			this.m_metaData.MarshalPackageMetadata(packageMd, mdHeader);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000EFA0 File Offset: 0x0000EFA0
		private unsafe static string FileListToXePartionPath(string[] fileNames)
		{
			if (fileNames == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = Marshal.PtrToStringUni((IntPtr)((void*)<Module>.XE_File_PartitionSeparator));
			int num = 0;
			if (0 < fileNames.Length - 1)
			{
				do
				{
					stringBuilder.AppendFormat("{0}{1}", fileNames[num], text);
					num++;
				}
				while (num < fileNames.Length - 1);
			}
			stringBuilder.Append(fileNames[fileNames.Length - 1]);
			return stringBuilder.ToString();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000F044 File Offset: 0x0000F044
		[return: MarshalAs(UnmanagedType.U1)]
		private unsafe bool BufferLocationToBufferNumber(BufferLocator loc, uint* bufNo)
		{
			XE_EventLocation xe_EventLocation;
			<Module>.XE_EventLocation.{ctor}(ref xe_EventLocation);
			<Module>.XE_EventLocation.SetFileIndex(ref xe_EventLocation, loc.m_fileId);
			<Module>.XE_EventLocation.SetBufferNumber(ref xe_EventLocation, loc.m_bufferNum);
			XE_FileReader<XE_FileReaderDefaultPolicy>* fileDecoder = this.m_fileDecoder;
			byte b = ((calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,XE_EventLocation modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.UInt32* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced)), fileDecoder, ref xe_EventLocation, bufNo, (IntPtr)(*(*(long*)fileDecoder + 24L))) != 0) ? 1 : 0);
			GC.KeepAlive(this);
			return b != 0;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000109A4 File Offset: 0x000109A4
		private unsafe byte[] ReadBuffer(uint bufNo, out BufferLocator bufLoc)
		{
			byte[] array = null;
			bufLoc = null;
			XE_FileReader<XE_FileReaderDefaultPolicy>* ptr = this.m_fileDecoder;
			XE_FileReader<XE_FileReaderDefaultPolicy>* ptr2 = ptr;
			if (bufNo < calli(System.UInt32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 40L))))
			{
				XE_LogBufferPosition xe_LogBufferPosition;
				<Module>.XE_EventLocation.{ctor}((ref xe_LogBufferPosition) + 8);
				this.m_positionLock.WaitOne();
				try
				{
					ptr = this.m_fileDecoder;
					XEBuffer xebuffer;
					XE_LogDefaultMetadataHeader xe_LogDefaultMetadataHeader;
					if (calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32,XEBuffer* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),XE_LogBufferPosition* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),XE_LogDefaultMetadataHeader* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced)), ptr, bufNo, ref xebuffer, ref xe_LogBufferPosition, ref xe_LogDefaultMetadataHeader, (IntPtr)(*(*(long*)ptr + 16L))) != null)
					{
						byte[] array2 = new byte[*((ref xebuffer) + 16)];
						IntPtr intPtr = new IntPtr(*((ref xebuffer) + 8));
						Marshal.Copy(intPtr, array2, 0, *((ref xebuffer) + 16));
						array = array2;
						bufLoc = new BufferLocator
						{
							m_fileId = <Module>.XE_EventLocation.GetFileIndex((ref xe_LogBufferPosition) + 8),
							m_bufferNum = <Module>.XE_EventLocation.GetBufferNumber((ref xe_LogBufferPosition) + 8)
						};
					}
				}
				finally
				{
					this.m_positionLock.ReleaseMutex();
				}
			}
			GC.KeepAlive(this);
			return array;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0001121C File Offset: 0x0001121C
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~XEventInteropFileReader();
			}
			else
			{
				try
				{
					this.!XEventInteropFileReader();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00011484 File Offset: 0x00011484
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00011268 File Offset: 0x00011268
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x040001B6 RID: 438
		private unsafe XE_FileReader<XE_FileReaderDefaultPolicy>* m_fileDecoder;

		// Token: 0x040001B7 RID: 439
		private unsafe XE_LogDefaultMetadataDecoder* m_mdDecoder;

		// Token: 0x040001B8 RID: 440
		private unsafe XE_MetadataStore* m_nativeMd;

		// Token: 0x040001B9 RID: 441
		private unsafe XE_ILogReadMessageHandler* m_messageHandler;

		// Token: 0x040001BA RID: 442
		private XEventInteropMetadataManager m_metaData;

		// Token: 0x040001BB RID: 443
		private bool m_disposed;

		// Token: 0x040001BC RID: 444
		private Mutex m_positionLock;
	}
}
