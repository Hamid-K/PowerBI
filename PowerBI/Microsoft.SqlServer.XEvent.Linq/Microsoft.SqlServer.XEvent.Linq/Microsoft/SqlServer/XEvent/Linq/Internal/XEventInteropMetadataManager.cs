using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.SqlServer.XEvent.TypeSystem;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x02000030 RID: 48
	internal class XEventInteropMetadataManager : IMetadataAccess, IDisposable
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000FF70 File Offset: 0x0000FF70
		public unsafe virtual IEventMetadata GetEventMetadata(IntPtr hEvent)
		{
			return this.GetMetadataObject<XEEvent,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropEventMetadata>((XEEvent*)hEvent.ToPointer(), this.m_eventMetadata);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000FF98 File Offset: 0x0000FF98
		public unsafe virtual IMapMetadata GetMapMetadata(IntPtr hMap)
		{
			return this.GetMetadataObject<XEMap,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMapMetadata>((XEMap*)hMap.ToPointer(), this.m_mapMetadata);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000FFC0 File Offset: 0x0000FFC0
		public unsafe virtual IActionMetadata GetActionMetadata(IntPtr hAction)
		{
			return this.GetMetadataObject<XEAction,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropActionMetadata>((XEAction*)hAction.ToPointer(), this.m_actionMetadata);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000FE24 File Offset: 0x0000FE24
		internal unsafe XEventInteropMetadataManager()
		{
			if (<Module>.XE_API.IsEnginePresent() == null)
			{
				<Module>.XE_MinApi.StaticInit();
			}
			SEList<XE_LogDeserializedPackage,0>* ptr = <Module>.@new(16UL);
			SEList<XE_LogDeserializedPackage,0>* ptr2;
			try
			{
				if (ptr != null)
				{
					initblk(ptr, 0, 16L);
					<Module>.ListBase.{ctor}(ptr);
					ptr2 = ptr;
				}
				else
				{
					ptr2 = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr, 16UL);
				throw;
			}
			this.m_packages = ptr2;
			SEList<XE_LogDeserializedPackage,0>* ptr3 = <Module>.@new(16UL);
			SEList<XE_LogDeserializedPackage,0>* ptr4;
			try
			{
				if (ptr3 != null)
				{
					initblk(ptr3, 0, 16L);
					<Module>.ListBase.{ctor}(ptr3);
					ptr4 = ptr3;
				}
				else
				{
					ptr4 = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr3, 16UL);
				throw;
			}
			this.m_packagesIncremental = ptr4;
			this.m_generations = Hashtable.Synchronized(new Hashtable());
			this.m_eventMetadata = new Dictionary<IntPtr, object>();
			this.m_mapMetadata = new Dictionary<IntPtr, object>();
			this.m_actionMetadata = new Dictionary<IntPtr, object>();
			GC.KeepAlive(this);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000FF20 File Offset: 0x0000FF20
		private void ~XEventInteropMetadataManager()
		{
			this.!XEventInteropMetadataManager();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000EC00 File Offset: 0x0000EC00
		private unsafe void !XEventInteropMetadataManager()
		{
			if (<Module>.SEList<XE_LogDeserializedPackage,0>.IsEmpty(this.m_packages) == null)
			{
				do
				{
					XE_LogDeserializedPackage* ptr = <Module>.SEList<XE_LogDeserializedPackage,0>.Head(this.m_packages);
					<Module>.SEList<XE_LogDeserializedPackage,0>.Delete(ptr);
					<Module>.XE_Delete<class\u0020XE_LogDeserializedPackage>(ptr);
				}
				while (<Module>.SEList<XE_LogDeserializedPackage,0>.IsEmpty(this.m_packages) == null);
			}
			<Module>.delete((void*)this.m_packages, 16UL);
			if (<Module>.SEList<XE_LogDeserializedPackage,0>.IsEmpty(this.m_packagesIncremental) == null)
			{
				do
				{
					XE_LogDeserializedPackage* ptr2 = <Module>.SEList<XE_LogDeserializedPackage,0>.Head(this.m_packagesIncremental);
					<Module>.SEList<XE_LogDeserializedPackage,0>.Delete(ptr2);
					<Module>.XE_Delete<class\u0020XE_LogDeserializedPackage>(ptr2);
				}
				while (<Module>.SEList<XE_LogDeserializedPackage,0>.IsEmpty(this.m_packagesIncremental) == null);
			}
			<Module>.delete((void*)this.m_packagesIncremental, 16UL);
			GC.KeepAlive(this);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000EBE8 File Offset: 0x0000EBE8
		static XEventInteropMetadataManager()
		{
			<Module>.XE_MinApi.StaticInit();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000EBE8 File Offset: 0x0000EBE8
		internal static void StaticInit()
		{
			<Module>.XE_MinApi.StaticInit();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000EC9C File Offset: 0x0000EC9C
		[return: MarshalAs(UnmanagedType.U1)]
		internal unsafe bool DeserializeMetadataBuffer(byte[] mdBuffer)
		{
			ref byte ptr = ref mdBuffer[0];
			XEBuffer xebuffer;
			int num = <Module>.XE_DeserializedBuffer.GetXEBuffer(ref ptr, (uint)mdBuffer.Length, ref xebuffer);
			if (num != 0)
			{
				byte* ptr2 = *((ref xebuffer) + 24);
				XE_LogBufferHeader* ptr3 = <Module>.XE_DeserializedBuffer.GetLogBufferHeader(ref xebuffer);
				int num3;
				if (ptr2 != (ref ptr))
				{
					ulong num2 = (ulong)((long)mdBuffer.Length);
					if ((ref ptr) + num2 >= (ulong)(*(int*)(ptr3 + 12L / (long)sizeof(XE_LogBufferHeader))) / (ulong)sizeof(byte) + ptr2)
					{
						num3 = 1;
						goto IL_0048;
					}
				}
				num3 = 0;
				IL_0048:
				GCHandle gchandle = GCHandle.Alloc(this);
				IntPtr intPtr = GCHandle.ToIntPtr(gchandle);
				if (num3 != 0 && <Module>.XE_LogDeserializedPackage.DeserializeMetadataBlock(ptr2, (uint)(*(int*)(ptr3 + 12L / (long)sizeof(XE_LogBufferHeader))), intPtr.ToPointer(), <Module>.__unep@?DeserializePackageCallback@?A0xd4db69fe@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FYAHPEAXPEAVXE_LogDeserializedPackage@@@Z) != null)
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
				gchandle.Free();
			}
			GC.KeepAlive(this);
			return ((num != 0) ? 1 : 0) != 0;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00010B70 File Offset: 0x00010B70
		internal unsafe void MarshalPackageMetadata(XEPackageMetadata* packageMd, XE_LogDefaultMetadataPackageHeader* mdHeader)
		{
			XE_LogDefaultMetadataPackageHeader* ptr = mdHeader + 8L;
			XEventInteropMetadataGeneration xeventInteropMetadataGeneration = this.GetGeneration(ptr, *(mdHeader + 24L));
			if (xeventInteropMetadataGeneration == null)
			{
				xeventInteropMetadataGeneration = new XEventInteropMetadataGeneration(mdHeader);
				KeyValuePair<Guid, ushort> generationId = XEventInteropMetadataManager.GetGenerationId(ptr, *(mdHeader + 24L));
				this.m_generations[generationId] = xeventInteropMetadataGeneration;
			}
			else
			{
				xeventInteropMetadataGeneration.IncrementRevision();
			}
			XEventInteropPackage xeventInteropPackage = xeventInteropMetadataGeneration.GetPackage((ushort)(*(*packageMd + 4L) & 1023));
			if (xeventInteropPackage == null)
			{
				xeventInteropPackage = new XEventInteropPackage(this, xeventInteropMetadataGeneration, packageMd);
				xeventInteropMetadataGeneration.AddPackage((ushort)(*(*packageMd + 4L) & 1023), xeventInteropPackage);
			}
			xeventInteropPackage.MergeCollections(packageMd);
			this.BuildHashFromObjectArray<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropEventMetadata,Microsoft::SqlServer::XEvent::IEventMetadata>(xeventInteropPackage.m_events, this.m_eventMetadata);
			this.BuildHashFromObjectArray<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropActionMetadata,Microsoft::SqlServer::XEvent::IActionMetadata>(xeventInteropPackage.m_actions, this.m_actionMetadata);
			this.BuildHashFromObjectArray<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMapMetadata,Microsoft::SqlServer::XEvent::IMapMetadata>(xeventInteropPackage.m_maps, this.m_mapMetadata);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000ED88 File Offset: 0x0000ED88
		internal ReadOnlyCollection<IMetadataGeneration> MetadataGenerations
		{
			get
			{
				IMetadataGeneration[] array = new IMetadataGeneration[this.m_generations.Count];
				try
				{
					Monitor.Enter(this.m_generations.SyncRoot);
					this.m_generations.Values.CopyTo(array, 0);
				}
				finally
				{
					Monitor.Exit(this.m_generations.SyncRoot);
				}
				return Array.AsReadOnly<IMetadataGeneration>(array);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000FF3C File Offset: 0x0000FF3C
		internal unsafe XEventInteropMetadataGeneration GetGeneration(_GUID* signature, ushort generationNum)
		{
			KeyValuePair<Guid, ushort> generationId = XEventInteropMetadataManager.GetGenerationId(signature, generationNum);
			return (XEventInteropMetadataGeneration)this.m_generations[generationId];
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00011284 File Offset: 0x00011284
		internal unsafe static void DeserializeMetadataCallback(void* mdCookie, XE_LogDeserializedPackage* deser)
		{
			IntPtr intPtr = new IntPtr(mdCookie);
			XEventInteropMetadataManager xeventInteropMetadataManager = (XEventInteropMetadataManager)GCHandle.FromIntPtr(intPtr).Target;
			bool flag = false;
			XE_LogDeserializedPackage* ptr = <Module>.SEList<XE_LogDeserializedPackage,0>.Head(xeventInteropMetadataManager.m_packages);
			if (ptr != null)
			{
				do
				{
					if (<Module>.IsEqualGUID(<Module>.XE_LogDeserializedPackage.GetHeader(ptr) + 8L, <Module>.XE_LogDeserializedPackage.GetHeader(deser) + 8L) != null)
					{
						ref ushort ptr2 = <Module>.XE_LogDeserializedPackage.GetHeader(ptr) + 24L;
						XE_LogDefaultMetadataPackageHeader* ptr3 = <Module>.XE_LogDeserializedPackage.GetHeader(deser) + 24L;
						if (ptr2 <= *ptr3 && <Module>.XE_Compare(<Module>.XE_LogDeserializedPackage.GetMetadata(ptr), <Module>.XE_LogDeserializedPackage.GetMetadata(deser)) != null)
						{
							<Module>.XE_DeserializedMetadata.Merge(<Module>.XE_LogDeserializedPackage.GetMetadata(ptr), <Module>.XE_LogDeserializedPackage.GetMetadata(deser));
							flag = true;
						}
					}
					ptr = <Module>.SEList<XE_LogDeserializedPackage,0>.GetNext(xeventInteropMetadataManager.m_packages, (XE_LogDeserializedPackage*)ptr);
				}
				while (ptr != null);
				if (flag)
				{
					<Module>.SEList<XE_LogDeserializedPackage,0>.Insert(xeventInteropMetadataManager.m_packagesIncremental, deser);
					goto IL_00BF;
				}
			}
			<Module>.SEList<XE_LogDeserializedPackage,0>.Insert(xeventInteropMetadataManager.m_packages, deser);
			IL_00BF:
			xeventInteropMetadataManager.MarshalPackageMetadata(<Module>.XE_LogDeserializedPackage.GetMetadata(deser), <Module>.XE_LogDeserializedPackage.GetHeader(deser));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000F898 File Offset: 0x0000F898
		internal unsafe static PublishedEvent MarshalEvent<XE_ILiveSessionMetadata>(XEEventBufferHeader* data, XEEvent* eventType, XE_ISerializedEvent<XE_ILiveSessionMetadata>* parser, XE_ILiveSessionMetadata* pMetadata, IMetadataAccess interopMetadata, XE_TicksUtil* pTc, EventLocator position)
		{
			IntPtr intPtr = new IntPtr((void*)eventType);
			IEventMetadata eventMetadata = interopMetadata.GetEventMetadata(intPtr);
			int pointerSize = eventMetadata.Package.Generation.PointerSize;
			PublishedEvent publishedEvent = new PublishedEvent((int)(*(ushort*)(eventType + 36L / (long)sizeof(XEEvent))), eventMetadata);
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,XE_ILiveSessionMetadata modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),XEEventBufferHeader modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),XEEvent modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), parser, pMetadata, data, eventType, (IntPtr)(*(*(long*)parser + 8L)));
			long num;
			<Module>.XE_TicksUtil.ConvertToFileTime(pTc, (ulong)(*(long*)(data + 12L / (long)sizeof(XEEventBufferHeader))), (_FILETIME*)(&num));
			DateTimeOffset dateTimeOffset = DateTime.FromFileTimeUtc(num);
			publishedEvent.Timestamp = dateTimeOffset;
			publishedEvent.Location = position;
			XE_PublishedDescriptor xe_PublishedDescriptor;
			uint num2 = calli(System.UInt16 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,XE_PublishedDescriptor* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced)), parser, ref xe_PublishedDescriptor, (IntPtr)(*(*(long*)parser + 32L)));
			if (num2 != 65535U)
			{
				do
				{
					ulong num3 = <Module>.XE_PublishedDescriptor.GetData(ref xe_PublishedDescriptor);
					XERelativeObjectId xerelativeObjectId;
					cpblk(ref xerelativeObjectId, *((ref xe_PublishedDescriptor) + 8) + 4L, 4);
					if (xe_PublishedDescriptor != 2)
					{
						uint num4 = xerelativeObjectId >> 28;
						if (num4 == 6U)
						{
							object obj;
							if (<Module>.Microsoft.SqlServer.XEvent.Linq.Internal.XE_CompareManaged(xerelativeObjectId, <Module>.XET_VLD_CALLSTACK) != null)
							{
								obj = XETypeMappings.UnloadXeCallstack(num3, *((ref xe_PublishedDescriptor) + 40), pointerSize);
							}
							else
							{
								obj = XETypeMappings.UnloadXeVariant(num3, xerelativeObjectId, *((ref xe_PublishedDescriptor) + 40));
							}
							publishedEvent.SetField((int)num2, obj);
						}
						else if (num4 == 3U)
						{
							XEMap* ptr = calli(XEObject modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,XERelativeObjectId), pMetadata, xerelativeObjectId, (IntPtr)(*(*(long*)pMetadata + 16L)));
							IntPtr intPtr2 = new IntPtr(ptr);
							IMapMetadata mapMetadata = interopMetadata.GetMapMetadata(intPtr2);
							uint num5 = (uint)num3;
							MapValue mapValue = null;
							if ((*(ptr + 3L) & 1) != 0)
							{
								StringBuilder stringBuilder = new StringBuilder("", 50);
								uint num6 = num5;
								uint num7 = 0U;
								if (num5 == 0U)
								{
									mapValue = mapMetadata[0U];
								}
								else if (num5 > 0U)
								{
									do
									{
										if ((num6 & 1U) != 0U)
										{
											MapValue mapValue2 = mapMetadata[1U << (int)num7];
											if (mapValue2 != null)
											{
												stringBuilder.AppendFormat("{0}, ", mapValue2.Value);
											}
										}
										num7 += 1U;
										num6 >>= 1;
									}
									while (num6 > 0U);
								}
								if (stringBuilder.Length > 2)
								{
									StringBuilder stringBuilder2 = stringBuilder;
									stringBuilder2.Remove(stringBuilder2.Length - 2, 2);
								}
								if (mapValue == null)
								{
									mapValue = new MapValue(num5, stringBuilder.ToString());
								}
							}
							else
							{
								mapValue = mapMetadata[num5];
							}
							if (mapValue == null)
							{
								uint num8 = num5;
								mapValue = new MapValue(num5, num8.ToString());
							}
							publishedEvent.SetField((int)num2, mapValue);
						}
					}
					else
					{
						IntPtr intPtr3 = new IntPtr(*((ref xe_PublishedDescriptor) + 16));
						IActionMetadata actionMetadata = interopMetadata.GetActionMetadata(intPtr3);
						object obj2;
						if (<Module>.Microsoft.SqlServer.XEvent.Linq.Internal.XE_CompareManaged(xerelativeObjectId, <Module>.XET_VLD_CALLSTACK) != null)
						{
							obj2 = XETypeMappings.UnloadXeCallstack(num3, *((ref xe_PublishedDescriptor) + 40), pointerSize);
						}
						else
						{
							obj2 = XETypeMappings.UnloadXeVariant(num3, xerelativeObjectId, *((ref xe_PublishedDescriptor) + 40));
						}
						PublishedAction publishedAction = new PublishedAction(actionMetadata, obj2);
						publishedEvent.AddAction(publishedAction);
					}
					num2 = calli(System.UInt16 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,XE_PublishedDescriptor* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced)), parser, ref xe_PublishedDescriptor, (IntPtr)(*(*(long*)parser + 32L)));
				}
				while (num2 != 65535U);
			}
			return publishedEvent;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000FB44 File Offset: 0x0000FB44
		internal unsafe static PublishedEvent MarshalEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>(XEEventBufferHeader* data, XEEvent* eventType, XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* parser, XEventInteropMetadataAdapter* pMetadata, IMetadataAccess interopMetadata, XE_TicksUtil* pTc, EventLocator position)
		{
			IntPtr intPtr = new IntPtr((void*)eventType);
			IEventMetadata eventMetadata = interopMetadata.GetEventMetadata(intPtr);
			int pointerSize = eventMetadata.Package.Generation.PointerSize;
			PublishedEvent publishedEvent = new PublishedEvent((int)(*(ushort*)(eventType + 36L / (long)sizeof(XEEvent))), eventMetadata);
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),XEEventBufferHeader modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),XEEvent modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), parser, pMetadata, data, eventType, (IntPtr)(*(*(long*)parser + 8L)));
			long num;
			<Module>.XE_TicksUtil.ConvertToFileTime(pTc, (ulong)(*(long*)(data + 12L / (long)sizeof(XEEventBufferHeader))), (_FILETIME*)(&num));
			DateTimeOffset dateTimeOffset = DateTime.FromFileTimeUtc(num);
			publishedEvent.Timestamp = dateTimeOffset;
			publishedEvent.Location = position;
			XE_PublishedDescriptor xe_PublishedDescriptor;
			uint num2 = calli(System.UInt16 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,XE_PublishedDescriptor* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced)), parser, ref xe_PublishedDescriptor, (IntPtr)(*(*(long*)parser + 32L)));
			if (num2 != 65535U)
			{
				do
				{
					ulong num3 = <Module>.XE_PublishedDescriptor.GetData(ref xe_PublishedDescriptor);
					XERelativeObjectId xerelativeObjectId;
					cpblk(ref xerelativeObjectId, *((ref xe_PublishedDescriptor) + 8) + 4L, 4);
					if (xe_PublishedDescriptor != 2)
					{
						uint num4 = xerelativeObjectId >> 28;
						if (num4 == 6U)
						{
							object obj;
							if (<Module>.Microsoft.SqlServer.XEvent.Linq.Internal.XE_CompareManaged(xerelativeObjectId, <Module>.XET_VLD_CALLSTACK) != null)
							{
								obj = XETypeMappings.UnloadXeCallstack(num3, *((ref xe_PublishedDescriptor) + 40), pointerSize);
							}
							else
							{
								obj = XETypeMappings.UnloadXeVariant(num3, xerelativeObjectId, *((ref xe_PublishedDescriptor) + 40));
							}
							publishedEvent.SetField((int)num2, obj);
						}
						else if (num4 == 3U)
						{
							XEMap* ptr = <Module>.Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.GetObj(pMetadata, xerelativeObjectId);
							IntPtr intPtr2 = new IntPtr(ptr);
							IMapMetadata mapMetadata = interopMetadata.GetMapMetadata(intPtr2);
							uint num5 = (uint)num3;
							MapValue mapValue = null;
							if ((*(ptr + 3L) & 1) != 0)
							{
								StringBuilder stringBuilder = new StringBuilder("", 50);
								uint num6 = num5;
								uint num7 = 0U;
								if (num5 == 0U)
								{
									mapValue = mapMetadata[0U];
								}
								else if (num5 > 0U)
								{
									do
									{
										if ((num6 & 1U) != 0U)
										{
											MapValue mapValue2 = mapMetadata[1U << (int)num7];
											if (mapValue2 != null)
											{
												stringBuilder.AppendFormat("{0}, ", mapValue2.Value);
											}
										}
										num7 += 1U;
										num6 >>= 1;
									}
									while (num6 > 0U);
								}
								if (stringBuilder.Length > 2)
								{
									StringBuilder stringBuilder2 = stringBuilder;
									stringBuilder2.Remove(stringBuilder2.Length - 2, 2);
								}
								if (mapValue == null)
								{
									mapValue = new MapValue(num5, stringBuilder.ToString());
								}
							}
							else
							{
								mapValue = mapMetadata[num5];
							}
							if (mapValue == null)
							{
								uint num8 = num5;
								mapValue = new MapValue(num5, num8.ToString());
							}
							publishedEvent.SetField((int)num2, mapValue);
						}
					}
					else
					{
						IntPtr intPtr3 = new IntPtr(*((ref xe_PublishedDescriptor) + 16));
						IActionMetadata actionMetadata = interopMetadata.GetActionMetadata(intPtr3);
						object obj2;
						if (<Module>.Microsoft.SqlServer.XEvent.Linq.Internal.XE_CompareManaged(xerelativeObjectId, <Module>.XET_VLD_CALLSTACK) != null)
						{
							obj2 = XETypeMappings.UnloadXeCallstack(num3, *((ref xe_PublishedDescriptor) + 40), pointerSize);
						}
						else
						{
							obj2 = XETypeMappings.UnloadXeVariant(num3, xerelativeObjectId, *((ref xe_PublishedDescriptor) + 40));
						}
						PublishedAction publishedAction = new PublishedAction(actionMetadata, obj2);
						publishedEvent.AddAction(publishedAction);
					}
					num2 = calli(System.UInt16 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,XE_PublishedDescriptor* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced)), parser, ref xe_PublishedDescriptor, (IntPtr)(*(*(long*)parser + 32L)));
				}
				while (num2 != 65535U);
			}
			return publishedEvent;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000FFE8 File Offset: 0x0000FFE8
		internal unsafe static PublishedEvent MarshalEventExternal(IntPtr hData, IntPtr hEventType, IntPtr hParser, IntPtr hMetadata, IntPtr hTicks, IMetadataAccess metadataAccess)
		{
			XEEventBufferHeader* ptr = (XEEventBufferHeader*)hData.ToPointer();
			XEEvent* ptr2 = (XEEvent*)hEventType.ToPointer();
			XE_SerializedEvent<XE_ILiveSessionMetadata,XE_VersionConfig>* ptr3 = (XE_SerializedEvent<XE_ILiveSessionMetadata,XE_VersionConfig>*)hParser.ToPointer();
			XE_ILiveSessionMetadata* ptr4 = (XE_ILiveSessionMetadata*)hMetadata.ToPointer();
			XE_TicksUtil* ptr5 = (XE_TicksUtil*)hTicks.ToPointer();
			return XEventInteropMetadataManager.MarshalEvent<XE_ILiveSessionMetadata>(ptr, (XEEvent*)ptr2, (XE_ISerializedEvent<XE_ILiveSessionMetadata>*)ptr3, (XE_ILiveSessionMetadata*)ptr4, metadataAccess, ptr5, new EventLocator
			{
				m_Handle1 = 0UL,
				m_Handle2 = 0U
			});
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000F7CC File Offset: 0x0000F7CC
		private unsafe XEventInteropEventMetadata GetMetadataObject<XEEvent,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropEventMetadata>(XEEvent* ntv, Dictionary<IntPtr, object> hashtable)
		{
			IntPtr key = XEventInteropEventMetadata.GetKey(ntv);
			XEventInteropEventMetadata xeventInteropEventMetadata = (XEventInteropEventMetadata)hashtable[key];
			if (xeventInteropEventMetadata == null)
			{
				xeventInteropEventMetadata = new XEventInteropEventMetadata(ntv, this, null);
				IntPtr key2 = xeventInteropEventMetadata.GetKey();
				hashtable[key2] = xeventInteropEventMetadata;
			}
			return xeventInteropEventMetadata;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000F810 File Offset: 0x0000F810
		private unsafe XEventInteropMapMetadata GetMetadataObject<XEMap,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMapMetadata>(XEMap* ntv, Dictionary<IntPtr, object> hashtable)
		{
			IntPtr key = XEventInteropMapMetadata.GetKey(ntv);
			XEventInteropMapMetadata xeventInteropMapMetadata = (XEventInteropMapMetadata)hashtable[key];
			if (xeventInteropMapMetadata == null)
			{
				xeventInteropMapMetadata = new XEventInteropMapMetadata(ntv, this, null);
				IntPtr key2 = xeventInteropMapMetadata.GetKey();
				hashtable[key2] = xeventInteropMapMetadata;
			}
			return xeventInteropMapMetadata;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000F854 File Offset: 0x0000F854
		private unsafe XEventInteropActionMetadata GetMetadataObject<XEAction,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropActionMetadata>(XEAction* ntv, Dictionary<IntPtr, object> hashtable)
		{
			IntPtr key = XEventInteropActionMetadata.GetKey(ntv);
			XEventInteropActionMetadata xeventInteropActionMetadata = (XEventInteropActionMetadata)hashtable[key];
			if (xeventInteropActionMetadata == null)
			{
				xeventInteropActionMetadata = new XEventInteropActionMetadata(ntv, this, null);
				IntPtr key2 = xeventInteropActionMetadata.GetKey();
				hashtable[key2] = xeventInteropActionMetadata;
			}
			return xeventInteropActionMetadata;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000ED48 File Offset: 0x0000ED48
		private unsafe static KeyValuePair<Guid, ushort> GetGenerationId(_GUID* signature, ushort generationNum)
		{
			Guid guid = (Guid)Marshal.PtrToStructure((IntPtr)((void*)signature), typeof(Guid));
			KeyValuePair<Guid, ushort> keyValuePair = new KeyValuePair<Guid, ushort>(guid, generationNum);
			return keyValuePair;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000F6F4 File Offset: 0x0000F6F4
		private void BuildHashFromObjectArray<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropEventMetadata,Microsoft::SqlServer::XEvent::IEventMetadata>(List<IEventMetadata> source, Dictionary<IntPtr, object> hashtable)
		{
			List<IEventMetadata>.Enumerator enumerator = source.GetEnumerator();
			if (enumerator.MoveNext())
			{
				do
				{
					XEventInteropEventMetadata xeventInteropEventMetadata = (XEventInteropEventMetadata)enumerator.Current;
					IntPtr key = xeventInteropEventMetadata.GetKey();
					hashtable[key] = xeventInteropEventMetadata;
				}
				while (enumerator.MoveNext());
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000F73C File Offset: 0x0000F73C
		private void BuildHashFromObjectArray<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropActionMetadata,Microsoft::SqlServer::XEvent::IActionMetadata>(List<IActionMetadata> source, Dictionary<IntPtr, object> hashtable)
		{
			List<IActionMetadata>.Enumerator enumerator = source.GetEnumerator();
			if (enumerator.MoveNext())
			{
				do
				{
					XEventInteropActionMetadata xeventInteropActionMetadata = (XEventInteropActionMetadata)enumerator.Current;
					IntPtr key = xeventInteropActionMetadata.GetKey();
					hashtable[key] = xeventInteropActionMetadata;
				}
				while (enumerator.MoveNext());
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000F784 File Offset: 0x0000F784
		private void BuildHashFromObjectArray<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMapMetadata,Microsoft::SqlServer::XEvent::IMapMetadata>(List<IMapMetadata> source, Dictionary<IntPtr, object> hashtable)
		{
			List<IMapMetadata>.Enumerator enumerator = source.GetEnumerator();
			if (enumerator.MoveNext())
			{
				do
				{
					XEventInteropMapMetadata xeventInteropMapMetadata = (XEventInteropMapMetadata)enumerator.Current;
					IntPtr key = xeventInteropMapMetadata.GetKey();
					hashtable[key] = xeventInteropMapMetadata;
				}
				while (enumerator.MoveNext());
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00010B08 File Offset: 0x00010B08
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!XEventInteropMetadataManager();
			}
			else
			{
				try
				{
					this.!XEventInteropMetadataManager();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000111FC File Offset: 0x000111FC
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00010B54 File Offset: 0x00010B54
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x0400019D RID: 413
		private unsafe SEList<XE_LogDeserializedPackage,0>* m_packages;

		// Token: 0x0400019E RID: 414
		private unsafe SEList<XE_LogDeserializedPackage,0>* m_packagesIncremental;

		// Token: 0x0400019F RID: 415
		private Dictionary<IntPtr, object> m_eventMetadata;

		// Token: 0x040001A0 RID: 416
		private Dictionary<IntPtr, object> m_mapMetadata;

		// Token: 0x040001A1 RID: 417
		private Dictionary<IntPtr, object> m_actionMetadata;

		// Token: 0x040001A2 RID: 418
		private Hashtable m_generations;

		// Token: 0x040001A3 RID: 419
		private Mutex m_generationMutex;
	}
}
