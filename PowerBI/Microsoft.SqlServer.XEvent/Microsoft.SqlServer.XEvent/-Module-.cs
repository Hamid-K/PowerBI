using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using <CppImplementationDetails>;
using <CrtImplementationDetails>;
using Microsoft.SqlServer.XEvent;
using Microsoft.SqlServer.XEvent.Internal;
using Microsoft.SqlServer.XEvent.TypeSystem;
using msclr._detail;
using std;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x000039E4 File Offset: 0x000039E4
	internal unsafe static Guid msclr._detail.FromGUID(_GUID* guid)
	{
		Guid guid2 = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return guid2;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00003A3C File Offset: 0x00003A3C
	internal unsafe static ICLRRuntimeHost* msclr._detail.get_clr_runtime_host()
	{
		Guid guid = <Module>.msclr._detail.FromGUID(ref <Module>.IID_ICLRRuntimeHost);
		return RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.msclr._detail.FromGUID(ref <Module>.CLSID_CLRRuntimeHost), guid).ToPointer();
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00001038 File Offset: 0x00001038
	internal unsafe static void ?A0xf3aec127.??__Eg_XEventAutoLoader@@YMXXZ()
	{
		<Module>.XEventAutoEngineLoad.{ctor}(ref <Module>.g_XEventAutoLoader);
		try
		{
			<Module>.g_XEventAutoLoader = ref <Module>.??_7XEventRegisterAutoLoader@Internal@XEvent@SqlServer@Microsoft@@6B@;
			if (<Module>.Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.StaticInit() != null)
			{
				XEventPackageRegistrar.InstallAssemblyLoadHandler();
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XEventAutoEngineLoad.{dtor}), (void*)(&<Module>.g_XEventAutoLoader));
			throw;
		}
		<Module>._atexit_m(ldftn(?A0xf3aec127.??__Fg_XEventAutoLoader@@YMXXZ));
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000EAAC File Offset: 0x0000EAAC
	internal static void ?A0xf3aec127.??__Fg_XEventAutoLoader@@YMXXZ()
	{
		<Module>.XEventAutoEngineLoad.{dtor}(ref <Module>.g_XEventAutoLoader);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00003F50 File Offset: 0x00003F50
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.GeneratedEvent.Publish(GeneratedEvent* A_0)
	{
		XE_SessionCtxtPublishEnum xe_SessionCtxtPublishEnum;
		<Module>.XE_SessionCtxtPublishEnum.{ctor}(ref xe_SessionCtxtPublishEnum);
		try
		{
			XESessionContext* ptr = <Module>.XE_SessionCtxtPublishEnum.GetFirst(ref xe_SessionCtxtPublishEnum, *(A_0 + 608L));
			*(A_0 + 584L) = ptr;
			if (ptr != null)
			{
				XESessionContext* ptr2;
				do
				{
					if (<Module>.GenericEvent.FireBegin(A_0) != null)
					{
						<Module>.XE_BufferCollector.SetDataActionCount(A_0 + 8L, *(*(A_0 + 584L) + 40L));
						*A_0 = 0;
						<Module>.GenericEvent.CallNextAction(A_0, ref <Module>.?A0xf3aec127.NULLCAD);
					}
					ptr2 = <Module>.XE_SessionCtxtPublishEnum.GetNext(ref xe_SessionCtxtPublishEnum, *(A_0 + 608L), *(A_0 + 584L));
					*(A_0 + 584L) = ptr2;
				}
				while (ptr2 != null);
			}
			else
			{
				*(A_0 + 596L) = 2;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_SessionCtxtPublishEnum.{dtor}), (void*)(&xe_SessionCtxtPublishEnum));
			throw;
		}
		<Module>.XE_SessionCtxtPublishEnum.{dtor}(ref xe_SessionCtxtPublishEnum);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000067C4 File Offset: 0x000067C4
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XE_ObjectSList.{dtor}(XE_ObjectSList* A_0)
	{
		if (<Module>.SinglyLinkedListBase.IsEmpty(A_0) == null)
		{
			do
			{
				XE_ObjectSListEntry* ptr = <Module>.TSinglyLinkedList<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.RemoveHeadFromNonEmptyList(A_0);
				long num = *(long*)(ptr + 8L / (long)sizeof(XE_ObjectSListEntry));
				uint num2 = (uint)(*(num + 4L)) >> 28;
				if (num2 != 0U)
				{
					if (num2 != 3U)
					{
						<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XEObject>(num);
					}
					else
					{
						<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEMap(num);
					}
				}
				else
				{
					<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEEvent(num);
				}
				<Module>.delete(*(long*)(ptr + 8L / (long)sizeof(XE_ObjectSListEntry)), 32UL);
			}
			while (<Module>.SinglyLinkedListBase.IsEmpty(A_0) == null);
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00005048 File Offset: 0x00005048
	internal unsafe static XEventManagedPackage* Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.{ctor}(XEventManagedPackage* A_0)
	{
		*A_0 = ref <Module>.??_7XE_IPackage@@6B@;
		try
		{
			*A_0 = ref <Module>.??_7XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@6B@;
			*(A_0 + 8L) = 0L;
			*(A_0 + 16L) = 0L;
			<Module>.ListBase.{ctor}(A_0 + 40L);
			*(A_0 + 64L) = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
			try
			{
				*(A_0 + 72L) = 0;
				*(A_0 + 80L) = 0L;
				*(A_0 + 88L) = 0L;
				*(A_0 + 96L) = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
				try
				{
					*(A_0 + 104L) = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
					try
					{
						*(A_0 + 112L) = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
						try
						{
							<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.{ctor}(A_0 + 128L);
							try
							{
								<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.{ctor}(A_0 + 136L);
								try
								{
									<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.{ctor}(A_0 + 144L);
									try
									{
										<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.{ctor}(A_0 + 152L);
										try
										{
											<Module>.ExternalStorage.Init(<Module>.XBitmap<ExternalStorage>.GetStorage(A_0 + 24L), null, 0U);
											*(A_0 + 56L) = 0L;
										}
										catch
										{
											<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}), (void*)(A_0 + (byte*)152L));
											throw;
										}
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}), (void*)(A_0 + (byte*)144L));
										throw;
									}
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}), (void*)(A_0 + (byte*)136L));
									throw;
								}
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}), (void*)(A_0 + (byte*)128L));
								throw;
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Resources::ResourceManager\u0020^>.{dtor}), (void*)(A_0 + (byte*)112L));
							throw;
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(gcroot<cli::array<System::PE$AAVString\u0020>^>.{dtor}), (void*)(A_0 + (byte*)104L));
						throw;
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(gcroot<cli::array<System::PE$AAVType\u0020>^>.{dtor}), (void*)(A_0 + (byte*)96L));
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(gcroot<cli::array<System::PE$AAVType\u0020>^>.{dtor}), (void*)(A_0 + (byte*)64L));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_IPackage.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00009A8C File Offset: 0x00009A8C
	internal unsafe static void* Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.__vecDelDtor(XEventManagedPackage* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			XEventManagedPackage* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 160UL, (ulong)(*ptr), ldftn(Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				XEventManagedPackage* ptr2 = ptr;
				<Module>.delete[](ptr2, (ulong)(*ptr2 * 160L + 8L));
			}
			return ptr;
		}
		<Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 160UL);
		}
		return A_0;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000052E4 File Offset: 0x000052E4
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.{dtor}(XEventManagedPackage* A_0)
	{
		*A_0 = ref <Module>.??_7XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@6B@;
		try
		{
			XEventManagedPackage* ptr;
			try
			{
				try
				{
					try
					{
						try
						{
							try
							{
								try
								{
									try
									{
										try
										{
											ptr = A_0 + 64L;
											Type[] array = <Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>..P$01EAPE$AAVType@System@@(ptr);
											if (array != null)
											{
												int num = 0;
												if (0 < array.Length)
												{
													do
													{
														<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.UninitEventClass(array[num]);
														num++;
													}
													while (num < array.Length);
												}
											}
											XEventManagedPackage* ptr2 = A_0 + 80L;
											if (*ptr2 != 0L)
											{
												ulong num2 = 0UL;
												XEventManagedPackage* ptr3 = A_0 + 88L;
												if (0L < *ptr3)
												{
													do
													{
														<Module>.delete[](*(num2 * 8UL + (ulong)(*ptr2)));
														num2 += 1UL;
													}
													while (num2 < (ulong)(*ptr3));
												}
												<Module>.delete[](*ptr2);
											}
										}
										catch
										{
											<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}), (void*)(A_0 + (byte*)152L));
											throw;
										}
										<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}(A_0 + 152L);
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}), (void*)(A_0 + (byte*)144L));
										throw;
									}
									<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}(A_0 + 144L);
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}), (void*)(A_0 + (byte*)136L));
									throw;
								}
								<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}(A_0 + 136L);
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}), (void*)(A_0 + (byte*)128L));
								throw;
							}
							<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}(A_0 + 128L);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Resources::ResourceManager\u0020^>.{dtor}), (void*)(A_0 + (byte*)112L));
							throw;
						}
						<Module>.gcroot<System::Resources::ResourceManager\u0020^>.{dtor}(A_0 + 112L);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(gcroot<cli::array<System::PE$AAVString\u0020>^>.{dtor}), (void*)(A_0 + (byte*)104L));
						throw;
					}
					<Module>.gcroot<cli::array<System::PE$AAVString\u0020>^>.{dtor}(A_0 + 104L);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(gcroot<cli::array<System::PE$AAVType\u0020>^>.{dtor}), (void*)(A_0 + (byte*)96L));
					throw;
				}
				<Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>.{dtor}(A_0 + 96L);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(gcroot<cli::array<System::PE$AAVType\u0020>^>.{dtor}), (void*)(A_0 + (byte*)64L));
				throw;
			}
			<Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>.{dtor}(ptr);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_IPackage.{dtor}), A_0);
			throw;
		}
		<Module>.XE_IPackage.{dtor}(A_0);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00007590 File Offset: 0x00007590
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.Destroy(XEventManagedPackage* A_0)
	{
		XEventManagedPackage* ptr = A_0 + 24L;
		<Module>.delete(*<Module>.XBitmap<ExternalStorage>.GetStorage(ptr), 4UL);
		<Module>.ExternalStorage.Init(<Module>.XBitmap<ExternalStorage>.GetStorage(ptr), null, 0U);
		calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void*), *(A_0 + 56L), (IntPtr)(*(<Module>.XE_API.ServiceAPI() + 136L)));
		XEventPackageSet* ptr2 = *(A_0 + 8L);
		if (ptr2 != null)
		{
			<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.{dtor}(ptr2);
			<Module>.delete((void*)ptr2, 352UL);
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00006828 File Offset: 0x00006828
	internal unsafe static void* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.__delDtor(XEventPackageSet* A_0, uint A_0)
	{
		<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 352UL);
		}
		return A_0;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000410C File Offset: 0x0000410C
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.DisableEvent(XEventManagedPackage* A_0, XESessionContext* pSessionContext)
	{
		int num = 0;
		return <Module>.XE_IPackage.DisableEvent<struct\u0020XBitmap<struct\u0020ExternalStorage>\u0020>(pSessionContext, A_0 + 24L, &num);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00007B14 File Offset: 0x00007B14
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.EnableEvent(XEventManagedPackage* A_0, XESessionContext* pSessionContext)
	{
		XE_SessionContextList* ptr = <Module>.XE_IPackage.GetList(pSessionContext);
		int num = 0;
		if (<Module>.SESList<XESessionContext,56>.IsInList(ptr, pSessionContext) == null)
		{
			<Module>.XE_IPackage.AggregateCustomizableAttributes(pSessionContext);
			<Module>.SESList<XESessionContext,56>.InsertRelease(ptr, pSessionContext);
		}
		try
		{
			XE_AutoEngineMutex xe_AutoEngineMutex;
			<Module>.XE_AutoEngineMutex.{ctor}(ref xe_AutoEngineMutex, *(A_0 + 56L), (XEWaitType)0);
			try
			{
				num = ((<Module>.XE_AutoEngineMutex.GetAccess(ref xe_AutoEngineMutex, uint.MaxValue) == (XEWaitResult)0) ? 1 : 0);
				if (num != 0)
				{
					XEventManagedPackage.AppDomainInfo* ptr2 = null;
					XEventManagedPackage.AppDomainInfo* ptr3 = null;
					for (;;)
					{
						ptr2 = <Module>.SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>.GetNextElem(A_0 + 40L, (XEventManagedPackage.AppDomainInfo*)ptr2);
						if (ptr2 == null)
						{
							break;
						}
						if (ptr3 != null)
						{
							<Module>.SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>.Delete(ptr3);
							ptr3 = null;
						}
						XEventManagedPackage.AppDomainInfo* ptr4 = ptr2 + 16L / (long)sizeof(XEventManagedPackage.AppDomainInfo);
						if (*(int*)ptr4 == AppDomain.CurrentDomain.Id)
						{
							goto IL_00FF;
						}
						if ((((ulong)(*(*(pSessionContext + 48L) + 4L)) >> (int)10L) & 262143UL) < (ulong)(*(A_0 + 88L)))
						{
							try
							{
								int num4;
								if (num != 0)
								{
									uint num2 = (uint)(*(int*)ptr4);
									long num3 = *(pSessionContext + 48L);
									if (<Module>.Microsoft.SqlServer.XEvent.Internal.call_in_appdomain_noassert<bool,wchar_t\u0020const\u0020*,struct\u0020XEEvent\u0020const\u0020*>(num2, <Module>.__unep@?EnableEventInAppDomain@XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@$$FCA_NPEB_WPEBUXEEvent@@@Z, *((((ulong)(*(num3 + 4L)) >> (int)10L) & 262143UL) * 8UL + (ulong)(*(A_0 + 80L))), num3) != null)
									{
										num4 = 1;
										goto IL_00EE;
									}
								}
								num4 = 0;
								IL_00EE:
								num = num4;
								continue;
							}
							catch (AppDomainUnloadedException ex)
							{
								ptr3 = ptr2;
								continue;
							}
							goto IL_00FF;
						}
						num = 0;
						continue;
						IL_00FF:
						if (*(A_0 + 72L) != AppDomain.CurrentDomain.Id)
						{
							long num5 = *(pSessionContext + 48L);
							num = ((<Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.EnableEventInAppDomain(*((((ulong)(*(num5 + 4L)) >> (int)10L) & 262143UL) * 8UL + (ulong)(*(A_0 + 80L))), num5) != 0) ? 1 : 0);
						}
						else
						{
							num = (<Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.EnableEventInternal(<Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>..P$01EAPE$AAVType@System@@(A_0 + 64L)[(int)(((uint)(*(*(pSessionContext + 48L) + 4L)) >> 10) & 262143U)], *(pSessionContext + 48L)) ? 1 : 0);
						}
					}
					if (ptr3 != null)
					{
						<Module>.SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>.Delete(ptr3);
					}
				}
				<Module>.XBitmap<ExternalStorage>.Alloc(A_0 + 24L, ((uint)(*(*(pSessionContext + 48L) + 4L)) >> 10) & 262143U);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoEngineMutex.{dtor}), (void*)(&xe_AutoEngineMutex));
				throw;
			}
			<Module>.XE_AutoEngineMutex.{dtor}(ref xe_AutoEngineMutex);
		}
		catch (Exception ex2)
		{
			Trace.TraceError(ex2.ToString());
			num = 0;
		}
		return num;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000078C0 File Offset: 0x000078C0
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.EnableEventInAppDomain(char* typeName, XEEvent* eventToEnable)
	{
		Type type = Type.GetType(new string((char*)typeName));
		return !(type != null) || <Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.EnableEventInternal(type, eventToEnable);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000075F8 File Offset: 0x000075F8
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.EnableEventInternal(Type eventType, XEEvent* eventToEnable)
	{
		bool flag = true;
		MethodInfo method = eventType.GetMethod("IsPublishSet", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		if (method == null)
		{
			flag = false;
		}
		else if (!(bool)((bool)method.Invoke(null, null)))
		{
			flag = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.CompileEventPublish(eventType, eventToEnable);
		}
		return flag;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00004130 File Offset: 0x00004130
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.IsEventEnabled(XEventManagedPackage* A_0, XESessionContext* pSessionContext, int* pEnabled)
	{
		if (<Module>.XBitmap<ExternalStorage>.IsAllocated(A_0 + 24L, ((uint)(*(*(pSessionContext + 48L) + 4L)) >> 10) & 262143U) != null)
		{
			XE_SessionContextList* ptr = <Module>.XE_IPackage.GetList(pSessionContext);
			*pEnabled = <Module>.SESList<XESessionContext,56>.IsInList(ptr, pSessionContext);
		}
		else
		{
			*pEnabled = 0;
		}
		return 1;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00005554 File Offset: 0x00005554
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.GetLocalizedStringImpl(XEventManagedPackage* XEventManagedPackage, uint locale, uint msgID, char* lpBuffer, uint cchBuffer)
	{
		int num = 0;
		string text = "";
		if (msgID >= 5)
		{
			XEventManagedPackage* ptr = XEventManagedPackage + 112L / (long)sizeof(XEventManagedPackage);
			if (<Module>.gcroot<System::Resources::ResourceManager\u0020^>..PE$AAVResourceManager@Resources@System@@(ptr) != null)
			{
				XEventManagedPackage* ptr2 = XEventManagedPackage + 104L / (long)sizeof(XEventManagedPackage);
				if (msgID < <Module>.gcroot<cli::array<System::PE$AAVString\u0020>^>.->(ptr2).Length)
				{
					CultureInfo cultureInfo = new CultureInfo(locale);
					text = <Module>.gcroot<System::Resources::ResourceManager\u0020^>.->(ptr).GetString(<Module>.gcroot<cli::array<System::PE$AAVString\u0020>^>..P$01EAPE$AAVString@System@@(ptr2)[msgID], cultureInfo);
					goto IL_0067;
				}
			}
		}
		if (msgID - 1 <= 3)
		{
			CultureInfo cultureInfo2 = new CultureInfo(locale);
			text = ResourcesMgr.GetString(ReservedResources.GetReservedResourceString(msgID), cultureInfo2);
		}
		IL_0067:
		if (!string.IsNullOrEmpty(text) && text.Length < cchBuffer)
		{
			ref byte ptr3 = text;
			if ((ref ptr3) != null)
			{
				ptr3 = (long)RuntimeHelpers.OffsetToStringData + (ref ptr3);
			}
			ref char ptr4 = ref ptr3;
			cpblk(lpBuffer, ref ptr4, (long)text.Length);
			((long)text.Length * 2L)[lpBuffer / 2] = '\0';
			num = 1;
		}
		return num;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00004178 File Offset: 0x00004178
	internal unsafe static XEPackageMetadata* Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.GetMetadata(XEventManagedPackage* A_0)
	{
		return *(A_0 + 16L);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00005614 File Offset: 0x00005614
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.SetEventTypesList(XEventManagedPackage* A_0, Type[] eventTypes)
	{
		XE_AutoEngineMutex xe_AutoEngineMutex;
		<Module>.XE_AutoEngineMutex.{ctor}(ref xe_AutoEngineMutex, *(A_0 + 56L), (XEWaitType)0);
		bool flag;
		try
		{
			if (*(A_0 + 56L) != 0L && <Module>.XE_AutoEngineMutex.GetAccess(ref xe_AutoEngineMutex, 4294967295U) != (XEWaitResult)0)
			{
				flag = false;
			}
			else
			{
				XEventManagedPackage.AppDomainInfo* ptr = <Module>.@new(24UL);
				XEventManagedPackage.AppDomainInfo* ptr2;
				try
				{
					if (ptr != null)
					{
						uint id = AppDomain.CurrentDomain.Id;
						<Module>.SEListElem.{ctor}(ptr);
						*(int*)(ptr + 16L / (long)sizeof(XEventManagedPackage.AppDomainInfo)) = id;
						ptr2 = ptr;
					}
					else
					{
						ptr2 = null;
					}
				}
				catch
				{
					<Module>.delete((void*)ptr, 24UL);
					throw;
				}
				XEventManagedPackage* ptr3 = A_0 + 40L;
				<Module>.SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>.Append(ptr3, ptr2);
				if (eventTypes != null && <Module>.SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>.GetCount(ptr3) == 1)
				{
					XEventManagedPackage* ptr4 = A_0 + 64L;
					<Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>.=(ptr4, new Type[eventTypes.Length]);
					eventTypes.CopyTo(<Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>..P$01EAPE$AAVType@System@@(ptr4), 0);
					*(A_0 + 72L) = AppDomain.CurrentDomain.Id;
					ulong num = (ulong)((long)<Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>.->(ptr4).Length);
					*(A_0 + 88L) = (long)num;
					ulong num2 = num;
					char** ptr5 = <Module>.new[]((num2 > 2305843009213693951UL) ? ulong.MaxValue : (num2 * 8UL));
					*(A_0 + 80L) = ptr5;
					initblk(ptr5, 0, *(A_0 + 88L) * 2L);
					int num3 = 0;
					if (0 < <Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>.->(ptr4).Length)
					{
						long num4 = 0L;
						do
						{
							ref byte ptr6 = <Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>..P$01EAPE$AAVType@System@@(ptr4)[num3].AssemblyQualifiedName;
							if ((ref ptr6) != null)
							{
								ptr6 = (long)RuntimeHelpers.OffsetToStringData + (ref ptr6);
							}
							ref char ptr7 = ref ptr6;
							ulong num5 = (ulong)((long)(<Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>..P$01EAPE$AAVType@System@@(ptr4)[num3].AssemblyQualifiedName.Length + 1));
							char* ptr8 = <Module>.new[]((num5 > 9223372036854775807UL) ? ulong.MaxValue : (num5 * 2UL));
							*(*(A_0 + 80L) + num4) = ptr8;
							cpblk(*(*(A_0 + 80L) + num4), ref ptr7, num5 * 2UL);
							num3++;
							num4 += 8L;
						}
						while (num3 < <Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>.->(ptr4).Length);
					}
				}
				flag = true;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoEngineMutex.{dtor}), (void*)(&xe_AutoEngineMutex));
			throw;
		}
		<Module>.XE_AutoEngineMutex.{dtor}(ref xe_AutoEngineMutex);
		return flag;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00005828 File Offset: 0x00005828
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.SetTargetTypesList(XEventManagedPackage* A_0, Type[] targetTypes)
	{
		if (targetTypes != null)
		{
			XEventManagedPackage* ptr = A_0 + 96L;
			<Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>.=(ptr, new Type[targetTypes.Length]);
			targetTypes.CopyTo(<Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>..P$01EAPE$AAVType@System@@(ptr), 0);
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00007648 File Offset: 0x00007648
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.LinkToCurrentAppDomain(XEventManagedPackage* A_0, Assembly assembly)
	{
		Type[] array = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetEventClassTypes(assembly);
		<Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.SetEventTypesList(A_0, array);
		XE_TCollection<0,0> xe_TCollection<0,0>;
		<Module>.XE_TCollection<0,0>.{ctor}(ref xe_TCollection<0,0>, *(*(A_0 + 16L) + 8L));
		XEventManagedPackage* ptr = A_0 + 24L;
		if (<Module>.XBitmap<ExternalStorage>.GetNumberAllocatedBits(ptr) > 0)
		{
			uint num = 0U;
			if (0 < <Module>.XBitmap<ExternalStorage>.GetNumberOfBits(ptr))
			{
				do
				{
					if (<Module>.XBitmap<ExternalStorage>.IsAllocated(ptr, num) != null)
					{
						<Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.CompileEventPublish(array[(int)num], <Module>.XE_TCollection<0,0>.GetTyped<struct\u0020XEEvent>(ref xe_TCollection<0,0>, num));
					}
					num += 1U;
				}
				while (num < <Module>.XBitmap<ExternalStorage>.GetNumberOfBits(ptr));
			}
		}
		int num2 = 0;
		if (0 < array.Length)
		{
			do
			{
				<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.InitEventClass(array[num2], (uint)num2, ptr);
				num2++;
			}
			while (num2 < array.Length);
		}
		return true;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00006924 File Offset: 0x00006924
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.CompileEventPublish(Type eventType, XEEvent* xevent)
	{
		bool flag = false;
		MethodInfo method = eventType.GetMethod("SetPublishDelegate", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		if (method != null)
		{
			Type[] array = new Type[] { typeof(object) };
			DynamicMethod dynamicMethod = new DynamicMethod("Publish", typeof(void), array, eventType.Module, true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			LocalBuilder localBuilder = ilgenerator.DeclareLocal(typeof(byte).MakePointerType(), true);
			LocalBuilder localBuilder2 = ilgenerator.DeclareLocal(typeof(byte).MakePointerType(), true);
			ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, 616);
			ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Conv_U);
			ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Localloc);
			ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Stloc, localBuilder2.LocalIndex);
			ushort num = *(ushort*)(xevent + 36L / (long)sizeof(XEEvent));
			if (num == 0)
			{
				MethodInfo method2 = typeof(GenericInteropEvent).GetMethod("ConstructEmptyEvent");
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc_S, localBuilder2.LocalIndex);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, 616);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I8, *(long*)(xevent + 40L / (long)sizeof(XEEvent)));
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Call, method2);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Stloc, localBuilder2.LocalIndex);
			}
			else
			{
				long num2 = (long)(num - 1) * 40L + *(long*)(xevent + 64L / (long)sizeof(XEEvent));
				XERelativeObjectId xerelativeObjectId;
				cpblk(ref xerelativeObjectId, num2, 4);
				ushort num3;
				if ((xerelativeObjectId & -268435456) == 1610612736)
				{
					num3 = <Module>.XE_GetTypeSize(*num2);
					num3 = (ushort)((uint)((num3 != 0) ? ((ulong)num3) : 8UL));
				}
				else
				{
					num3 = 4;
				}
				ushort num4 = *(*(long*)(xevent + 64L / (long)sizeof(XEEvent)) + (long)(*(ushort*)(xevent + 36L / (long)sizeof(XEEvent)) - 1) * 40L + 16L) + num3;
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, (int)num4);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Conv_U);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Localloc);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Stloc, localBuilder.LocalIndex);
				MethodInfo method3 = typeof(GenericInteropEvent).GetMethod("ConstructEvent");
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc_S, localBuilder2.LocalIndex);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, 616);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, (int)(*(ushort*)(xevent + 36L / (long)sizeof(XEEvent)) - *(ushort*)(xevent + 38L / (long)sizeof(XEEvent))));
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc_S, localBuilder.LocalIndex);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, (int)num4);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I8, *(long*)(xevent + 40L / (long)sizeof(XEEvent)));
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Call, method3);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Stloc, localBuilder2.LocalIndex);
				ushort num5 = 0;
				FieldInfo[] array2 = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetPublishedFields(eventType, ref num5);
				int num6 = 1;
				int num7 = 0;
				if (0 < array2.Length)
				{
					long num8 = 0L;
					do
					{
						Type fieldType = array2[num7].FieldType;
						if (!fieldType.IsEnum && XEventTypeMapping.IsVld(fieldType))
						{
							if (fieldType == typeof(string))
							{
								LocalBuilder localBuilder3 = ilgenerator.DeclareLocal(typeof(string), true);
								LocalBuilder localBuilder4 = ilgenerator.DeclareLocal(typeof(byte).MakePointerType());
								MethodInfo method4 = typeof(GenericInteropEvent).GetMethod("GetInteriorPtr");
								MethodInfo getMethod = typeof(string).GetProperty("Length").GetGetMethod();
								Label label = ilgenerator.DefineLabel();
								Label label2 = ilgenerator.DefineLabel();
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldarg_0);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldfld, array2[num7]);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldnull);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Beq, label);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldarg_0);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldfld, array2[num7]);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Stloc, localBuilder3.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder3.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Call, method4);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Stloc, localBuilder4.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder2.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder4.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder3.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Call, getMethod);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, 2);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Mul);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Br, label2);
								ilgenerator.MarkLabel(label);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder2.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4_0);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Conv_U);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4_0);
								ilgenerator.MarkLabel(label2);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, num7);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Conv_I2);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, num6);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Conv_I2);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Call, XEventTypeMapping.MarshalCopyMethod(typeof(string)));
							}
							else if (fieldType == typeof(Guid))
							{
								LocalBuilder localBuilder5 = ilgenerator.DeclareLocal(typeof(_GUID).MakeByRefType(), true);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldarg_0);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldflda, array2[num7]);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Stloc, localBuilder5.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder2.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder5.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, 16);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, num7);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Conv_I2);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, num6);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Conv_I2);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Call, XEventTypeMapping.MarshalCopyMethod(typeof(Guid)));
							}
							else if (fieldType == typeof(byte[]))
							{
								LocalBuilder localBuilder6 = ilgenerator.DeclareLocal(typeof(byte[]), true);
								LocalBuilder localBuilder7 = ilgenerator.DeclareLocal(typeof(byte).MakePointerType(), false);
								Label label3 = ilgenerator.DefineLabel();
								Label label4 = ilgenerator.DefineLabel();
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldarg_0);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldfld, array2[num7]);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldnull);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Beq, label3);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldarg_0);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldfld, array2[num7]);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Stloc, localBuilder6.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder6.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, 0);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldelema, typeof(byte));
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Stloc, localBuilder7.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder2.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder7.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder6.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldlen);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Br, label4);
								ilgenerator.MarkLabel(label3);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder2.LocalIndex);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4_0);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Conv_U);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4_0);
								ilgenerator.MarkLabel(label4);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, num7);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Conv_I2);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, num6);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Conv_I2);
								ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Call, XEventTypeMapping.MarshalCopyMethod(typeof(byte[])));
							}
							num6++;
						}
						else
						{
							MethodInfo methodInfo;
							if (fieldType.IsEnum)
							{
								methodInfo = XEventTypeMapping.MarshalCopyMethod(typeof(uint));
							}
							else
							{
								methodInfo = XEventTypeMapping.MarshalCopyMethod(fieldType);
							}
							ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder.LocalIndex);
							ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, (int)(*(*(long*)(xevent + 64L / (long)sizeof(XEEvent)) + num8 + 16L)));
							ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldarg_0);
							ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldfld, array2[num7]);
							ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Call, methodInfo);
						}
						num7++;
						num8 += 40L;
					}
					while (num7 < array2.Length);
				}
			}
			MethodInfo method5 = typeof(GenericInteropEvent).GetMethod("Publish", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc, localBuilder2.LocalIndex);
			ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Call, method5);
			ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ret);
			method.Invoke(null, new object[] { dynamicMethod.CreateDelegate(typeof(PublishDelegate)) });
			(Attribute.GetCustomAttribute(eventType, typeof(XEventAttribute)) as XEventAttribute).m_publishAttached = true;
			flag = true;
		}
		return flag;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00004194 File Offset: 0x00004194
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.Init(XEventManagedPackage* A_0, XEPackage* pPackage, ushort packageID)
	{
		<Module>.XE_PackageManagerUtilities.SetPackageId(packageID, 0, *(A_0 + 16L));
		return calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void**), A_0 / sizeof(void*) + 56L, (IntPtr)(*(<Module>.XE_API.ServiceAPI() + 128L)));
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00005860 File Offset: 0x00005860
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEMap(XEMap* mapToFree)
	{
		if (mapToFree != null)
		{
			int num = 0;
			if (0 < *(ushort*)(mapToFree + 34L / (long)sizeof(XEMap)))
			{
				long num2 = 0L;
				do
				{
					ulong num3 = (ulong)(*(*(long*)(mapToFree + 40L / (long)sizeof(XEMap)) + num2 + 8L));
					if (num3 != 0UL)
					{
						IntPtr intPtr = new IntPtr(num3);
						Marshal.FreeHGlobal(intPtr);
						*(*(long*)(mapToFree + 40L / (long)sizeof(XEMap)) + num2 + 8L) = 0L;
					}
					num++;
					num2 += 16L;
				}
				while (num < (int)(*(ushort*)(mapToFree + 34L / (long)sizeof(XEMap))));
			}
			<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XEObject>((XEObject*)mapToFree);
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000058CC File Offset: 0x000058CC
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEEvent(XEEvent* eventToFree)
	{
		if (eventToFree != null)
		{
			if (*(long*)(eventToFree + 56L / (long)sizeof(XEEvent)) != 0L)
			{
				int num = 0;
				if (0 < *(ushort*)(eventToFree + 34L / (long)sizeof(XEEvent)))
				{
					do
					{
						<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XECustomizableAttribute>((long)num * 56L + *(long*)(eventToFree + 56L / (long)sizeof(XEEvent)) / (long)sizeof(XECustomizableAttribute));
						num++;
					}
					while (num < (int)(*(ushort*)(eventToFree + 34L / (long)sizeof(XEEvent))));
				}
				<Module>.delete[](*(long*)(eventToFree + 56L / (long)sizeof(XEEvent)));
				*(long*)(eventToFree + 56L / (long)sizeof(XEEvent)) = 0L;
			}
			if (*(long*)(eventToFree + 64L / (long)sizeof(XEEvent)) != 0L)
			{
				int num2 = 0;
				if (0 < *(ushort*)(eventToFree + 36L / (long)sizeof(XEEvent)))
				{
					do
					{
						<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XEDataAttribute>((long)num2 * 40L + *(long*)(eventToFree + 64L / (long)sizeof(XEEvent)) / (long)sizeof(XEDataAttribute));
						num2++;
					}
					while (num2 < (int)(*(ushort*)(eventToFree + 36L / (long)sizeof(XEEvent))));
				}
				<Module>.delete[](*(long*)(eventToFree + 64L / (long)sizeof(XEEvent)));
				*(long*)(eventToFree + 64L / (long)sizeof(XEEvent)) = 0L;
			}
			if (*(long*)(eventToFree + 48L / (long)sizeof(XEEvent)) != 0L)
			{
				int num3 = 0;
				if (0 < *(ushort*)(eventToFree + 32L / (long)sizeof(XEEvent)))
				{
					long num4 = 0L;
					do
					{
						if (num3 < 3)
						{
							*(num4 + *(long*)(eventToFree + 48L / (long)sizeof(XEEvent)) + 8L) = 0L;
						}
						ulong num5 = (ulong)(*(long*)(eventToFree + 48L / (long)sizeof(XEEvent)));
						if ((*(num4 + (long)num5) & -268435456) == 1610612736)
						{
							long num6 = (long)num3 * 40L;
							if (<Module>.XE_IsRidVld(*(num6 + (long)num5)) != null)
							{
								if (<Module>.XE_Compare(*(*(long*)(eventToFree + 48L / (long)sizeof(XEEvent)) + num6), <Module>.XET_VLD_WSTR) != null)
								{
									IntPtr intPtr = new IntPtr(<Module>.XE_VariantUnload<wchar_t\u0020const\u0020*>((ulong)(*(num4 + *(long*)(eventToFree + 48L / (long)sizeof(XEEvent)) + 16L))));
									Marshal.FreeHGlobal(intPtr);
								}
								else
								{
									<Module>.delete(<Module>.XE_VariantUnload<void\u0020*>((ulong)(*(num4 + *(long*)(eventToFree + 48L / (long)sizeof(XEEvent)) + 16L))), 0UL);
								}
							}
						}
						<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XEStaticAttribute>((long)num3 * 40L + *(long*)(eventToFree + 48L / (long)sizeof(XEEvent)) / (long)sizeof(XEStaticAttribute));
						num3++;
						num4 += 40L;
					}
					while (num3 < (int)(*(ushort*)(eventToFree + 32L / (long)sizeof(XEEvent))));
				}
				<Module>.delete[](*(long*)(eventToFree + 48L / (long)sizeof(XEEvent)));
				*(long*)(eventToFree + 48L / (long)sizeof(XEEvent)) = 0L;
			}
			ulong num7 = (ulong)(*(long*)(eventToFree + 40L / (long)sizeof(XEEvent)));
			if (num7 != 0UL)
			{
				<Module>.delete(num7, 0UL);
				*(long*)(eventToFree + 40L / (long)sizeof(XEEvent)) = 0L;
			}
			<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XEObject>((XEObject*)eventToFree);
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00005A88 File Offset: 0x00005A88
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXETarget(XETarget* targetToFree)
	{
		if (targetToFree != null)
		{
			<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XEObject>((XEObject*)targetToFree);
			if (*(long*)(targetToFree + 40L / (long)sizeof(XETarget)) != 0L)
			{
				int num = 0;
				if (0 < *(ushort*)(targetToFree + 32L / (long)sizeof(XETarget)))
				{
					do
					{
						<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XECustomizableAttribute>((long)num * 56L + *(long*)(targetToFree + 40L / (long)sizeof(XETarget)) / (long)sizeof(XECustomizableAttribute));
						num++;
					}
					while (num < (int)(*(ushort*)(targetToFree + 32L / (long)sizeof(XETarget))));
				}
				<Module>.delete(*(long*)(targetToFree + 40L / (long)sizeof(XETarget)), 56UL);
				*(long*)(targetToFree + 40L / (long)sizeof(XETarget)) = 0L;
			}
		}
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00005AEC File Offset: 0x00005AEC
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.{dtor}(XEventPackageSet* A_0)
	{
		try
		{
			try
			{
				try
				{
					<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XEObject>(A_0);
					XEventPackageSet* ptr = A_0 + 96L;
					XE_TCollection<0,0> xe_TCollection<0,0>;
					<Module>.XE_TCollection<0,0>.{ctor}(ref xe_TCollection<0,0>, *ptr);
					uint num = 0U;
					if (0 < <Module>.XE_TCollection<0,0>.GetCount(ref xe_TCollection<0,0>))
					{
						do
						{
							XEEvent* ptr2 = <Module>.XE_TCollection<0,0>.GetTyped<struct\u0020XEEvent>(ref xe_TCollection<0,0>, num);
							<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEEvent(ptr2);
							<Module>.delete((void*)ptr2, 72UL);
							num += 1U;
						}
						while (num < <Module>.XE_TCollection<0,0>.GetCount(ref xe_TCollection<0,0>));
					}
					XE_TCollection<0,0> xe_TCollection<0,0>2;
					<Module>.XE_TCollection<0,0>.{ctor}(ref xe_TCollection<0,0>2, *(A_0 + 120L));
					uint num2 = 0U;
					if (0 < <Module>.XE_TCollection<0,0>.GetCount(ref xe_TCollection<0,0>2))
					{
						do
						{
							XEMap* ptr3 = <Module>.XE_TCollection<0,0>.GetTyped<struct\u0020XEMap>(ref xe_TCollection<0,0>2, num2);
							<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEMap(ptr3);
							<Module>.delete((void*)ptr3, 48UL);
							num2 += 1U;
						}
						while (num2 < <Module>.XE_TCollection<0,0>.GetCount(ref xe_TCollection<0,0>2));
					}
					XE_TCollection<0,0> xe_TCollection<0,0>3;
					<Module>.XE_TCollection<0,0>.{ctor}(ref xe_TCollection<0,0>3, *(A_0 + 112L));
					uint num3 = 0U;
					if (0 < <Module>.XE_TCollection<0,0>.GetCount(ref xe_TCollection<0,0>3))
					{
						do
						{
							XETarget* ptr4 = <Module>.XE_TCollection<0,0>.GetTyped<struct\u0020XETarget>(ref xe_TCollection<0,0>3, num3);
							<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXETarget(ptr4);
							<Module>.delete((void*)ptr4, 56UL);
							num3 += 1U;
						}
						while (num3 < <Module>.XE_TCollection<0,0>.GetCount(ref xe_TCollection<0,0>3));
					}
					XEventPackageSet* ptr5 = ptr;
					uint num4 = 8U;
					do
					{
						XEObjectCollection* ptr6 = *ptr5;
						if (ptr6 != null)
						{
							<Module>.delete((void*)ptr6, 16UL);
						}
						ptr5 += 8L;
						num4 += uint.MaxValue;
					}
					while (num4 > 0U);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Resources::ResourceManager\u0020^>.{dtor}), (void*)(A_0 + (byte*)344L));
					throw;
				}
				<Module>.gcroot<System::Resources::ResourceManager\u0020^>.{dtor}(A_0 + 344L);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.{dtor}), (void*)(A_0 + (byte*)336L));
				throw;
			}
			<Module>.gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.{dtor}(A_0 + 336L);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.{dtor}), (void*)(A_0 + (byte*)160L));
			throw;
		}
		<Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.{dtor}(A_0 + 160L);
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00005CB4 File Offset: 0x00005CB4
	internal unsafe static FieldInfo[] Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetPublishedFields(Type eventClassType, ushort* fixedCount)
	{
		FieldInfo[] fields = eventClassType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		FieldInfo[] array = new FieldInfo[0];
		if (fields.Length > 0)
		{
			List<FieldInfo> list = new List<FieldInfo>();
			List<FieldInfo> list2 = new List<FieldInfo>();
			int num = 0;
			if (0 < fields.Length)
			{
				FieldInfo fieldInfo;
				for (;;)
				{
					fieldInfo = fields[num];
					Type fieldType = fieldInfo.FieldType;
					if (!XEventTypeMapping.IsTypeSupported(fieldType) && !fieldType.IsEnum)
					{
						break;
					}
					if (!Attribute.IsDefined(fields[num], typeof(NonPublishedAttribute)))
					{
						if (!fieldType.IsEnum && XEventTypeMapping.IsVld(fieldType))
						{
							list2.Add(fieldInfo);
						}
						else
						{
							*fixedCount = (short)(*fixedCount + 1);
							list.Add(fieldInfo);
						}
					}
					num++;
					if (num >= fields.Length)
					{
						goto IL_0098;
					}
				}
				throw new XEventUnsupportedTypeException(eventClassType, fieldInfo);
			}
			IL_0098:
			array = new FieldInfo[list2.Count + list.Count];
			list.CopyTo(array);
			list2.CopyTo(array, list.Count);
		}
		return array;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00007D40 File Offset: 0x00007D40
	internal unsafe static XEEvent* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateXEEvent(XEventPackageSet* A_0, XEventPackageSet* pkgWrapper, Dictionary<Type, XEventAutoObject<XEMap>> mapTypes, Dictionary<string, uint> keywords, Type eventClassType)
	{
		XEventAutoObject<XEEvent> xeventAutoObject<XEEvent> = null;
		<Module>._Init_thread_header_m((int*)(&<Module>.?A0xf3aec127.?$TSS0@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4HA));
		if (<Module>.?A0xf3aec127.?$TSS0@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4HA == -1)
		{
			try
			{
				cpblk(ref <Module>.?A0xf3aec127.?MANDATORY_FIELD_TYPES@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4PAUXERelativeObjectId@@A, ref <Module>.XET_UUID_PTR, 4);
				cpblk((ref <Module>.?A0xf3aec127.?MANDATORY_FIELD_TYPES@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4PAUXERelativeObjectId@@A) + 4, ref <Module>.XET_UINT8, 4);
				cpblk((ref <Module>.?A0xf3aec127.?MANDATORY_FIELD_TYPES@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4PAUXERelativeObjectId@@A) + 8, ref <Module>.XET_CHANNEL_MAP, 4);
				*((ref <Module>.?A0xf3aec127.?MANDATORY_FIELD_TYPES@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4PAUXERelativeObjectId@@A) + 12) = 805307391;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(_Init_thread_abort_m), (void*)(&<Module>.?A0xf3aec127.?$TSS0@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4HA));
				throw;
			}
			<Module>._Init_thread_footer_m((int*)(&<Module>.?A0xf3aec127.?$TSS0@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4HA));
		}
		XEventAttribute xeventAttribute = Attribute.GetCustomAttribute(eventClassType, typeof(XEventAttribute)) as XEventAttribute;
		ushort num = 0;
		FieldInfo[] array = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetPublishedFields(eventClassType, ref num);
		XEventAutoObject<XEEvent> xeventAutoObject<XEEvent>2 = new XEventAutoObject<XEEvent>();
		XEEvent* ptr11;
		try
		{
			xeventAutoObject<XEEvent> = xeventAutoObject<XEEvent>2;
			uint num2 = (uint)(*(A_0 + 320L));
			uint num3 = num2;
			*(A_0 + 320L) = (int)(num2 + 1U);
			xeventAutoObject<XEEvent>.Set(<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.BuildXEObject<struct\u0020XEEvent>(A_0, 10, 0, 0, num3, xeventAttribute.Name, xeventAttribute.DescriptionKey));
			XEEvent* ptr = xeventAutoObject<XEEvent>.Get();
			ulong num4 = (ulong)((long)array.Length);
			XEDataAttribute* ptr2 = <Module>.new[]((num4 > 461168601842738790UL) ? ulong.MaxValue : (num4 * 40UL));
			*(long*)(ptr + 64L / (long)sizeof(XEEvent)) = ptr2;
			initblk(ptr2, 0, (long)array.Length * 40L);
			int num5 = 0;
			if (0 < array.Length)
			{
				long num6 = 0L;
				do
				{
					*(short*)(ptr + 36L / (long)sizeof(XEEvent)) = (short)(*(ushort*)(ptr + 36L / (long)sizeof(XEEvent)) + 1);
					Type type = array[num5].FieldType;
					XEDataAttribute* ptr3;
					if (type.IsEnum)
					{
						if (!mapTypes.ContainsKey(type))
						{
							XEventAutoObject<XEMap> xeventAutoObject<XEMap> = new XEventAutoObject<XEMap>();
							xeventAutoObject<XEMap>.Set(<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateXEMap(pkgWrapper, array[num5].FieldType));
							mapTypes.Add(array[num5].FieldType, xeventAutoObject<XEMap>);
						}
						XEventAutoObject<XEMap> xeventAutoObject<XEMap>2 = mapTypes[type];
						ptr3 = num6 + *(long*)(ptr + 64L / (long)sizeof(XEEvent));
						cpblk(ptr3, xeventAutoObject<XEMap>2.Get() + 4L / (long)sizeof(XEMap), 4);
						type = typeof(uint);
					}
					else
					{
						XERelativeObjectId xerelativeObjectId = XEventTypeMapping.XETypeFromManagedType(type);
						ptr3 = num6 + *(long*)(ptr + 64L / (long)sizeof(XEEvent));
						cpblk(ptr3, ref xerelativeObjectId, 4);
					}
					string text = array[num5].Name;
					string text2 = null;
					if (Attribute.IsDefined(array[num5], typeof(XEventFieldAttribute)))
					{
						XEventFieldAttribute xeventFieldAttribute = Attribute.GetCustomAttribute(array[num5], typeof(XEventFieldAttribute)) as XEventFieldAttribute;
						text = xeventFieldAttribute.Name;
						text2 = xeventFieldAttribute.DescriptionKey;
					}
					IntPtr intPtr = Marshal.StringToHGlobalUni(text);
					*(long*)(ptr3 + 8L / (long)sizeof(XEDataAttribute)) = intPtr.ToPointer();
					*(short*)(ptr3 + 16L / (long)sizeof(XEDataAttribute)) = (short)XEventTypeMapping.XETypeSizeFromManagedType(type);
					*(short*)(ptr3 + 18L / (long)sizeof(XEDataAttribute)) = 0;
					<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.AssignDescription<struct\u0020XEDataAttribute>(A_0, text2, ptr3);
					num5++;
					num6 += 40L;
				}
				while (num5 < array.Length);
			}
			ushort num7 = 0;
			int num8 = 0;
			if (0 < array.Length)
			{
				long num9 = 0L;
				do
				{
					long num10 = *(long*)(ptr + 64L / (long)sizeof(XEEvent)) + num9 + 16L;
					ushort num11 = *num10;
					*num10 = (short)num7;
					num7 = num11 + num7;
					num8++;
					num9 += 40L;
				}
				while (num8 < array.Length);
			}
			XE_SessionContextList* ptr4 = <Module>.@new(8UL);
			XE_SessionContextList* ptr5;
			if (ptr4 != null)
			{
				initblk(ptr4, 0, 8L);
				ptr5 = ptr4;
			}
			else
			{
				ptr5 = null;
			}
			*(long*)(ptr + 40L / (long)sizeof(XEEvent)) = ptr5;
			*(short*)(ptr + 34L / (long)sizeof(XEEvent)) = 0;
			*(long*)(ptr + 56L / (long)sizeof(XEEvent)) = 0L;
			*(short*)(ptr + 38L / (long)sizeof(XEEvent)) = (short)num;
			ushort num12 = 3;
			ulong num13 = 0UL;
			if (xeventAttribute.Keyword != null)
			{
				num12 = 4;
				if (!keywords.ContainsKey(xeventAttribute.Keyword))
				{
					uint num14 = (uint)(*(A_0 + 328L));
					if (num14 >= 2147483648U)
					{
						throw new XEventTooManyKeywordsException();
					}
					uint num15 = num14 << 1;
					*(A_0 + 328L) = (int)num15;
					keywords.Add(xeventAttribute.Keyword, num15);
				}
				num13 = (ulong)keywords[xeventAttribute.Keyword];
			}
			XEStaticAttribute* ptr6 = <Module>.new[]((ulong)num12 * 40UL);
			*(long*)(ptr + 48L / (long)sizeof(XEEvent)) = ptr6;
			*(short*)(ptr + 32L / (long)sizeof(XEEvent)) = (short)num12;
			int num16 = 0;
			int num17 = (int)num12;
			if (0 < num17)
			{
				long num18 = 0L;
				XERelativeObjectId* ptr7 = (XERelativeObjectId*)(&<Module>.?A0xf3aec127.?MANDATORY_FIELD_TYPES@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4PAUXERelativeObjectId@@A);
				char** ptr8 = ref <Module>.?A0xf3aec127.?MANDATORY_FIELD_NAMES@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4QBQEB_WB;
				do
				{
					XEStaticAttribute* ptr9 = num18 + *(long*)(ptr + 48L / (long)sizeof(XEEvent));
					*(long*)(ptr9 + 8L / (long)sizeof(XEStaticAttribute)) = *ptr8;
					cpblk(ptr9, ptr7, 4);
					*(long*)(ptr9 + 24L / (long)sizeof(XEStaticAttribute)) = ResourcesMgr.GetNativeString(ReservedResources.GetReservedResourceString((ReservedResources.ReservedResourceIds)num16));
					*(int*)(ptr9 + 32L / (long)sizeof(XEStaticAttribute)) = num16;
					num16++;
					num18 += 40L;
					ptr8 += 8L;
					ptr7 += 4L / (long)sizeof(XERelativeObjectId);
				}
				while (num16 < num17);
			}
			*(*(long*)(ptr + 48L / (long)sizeof(XEEvent)) + 56L) = <Module>.XE_VariantLoad<int>(1);
			int num19 = (int)xeventAttribute.EventChannel;
			if (num19 < 1 || num19 > 3)
			{
				num19 = 3;
			}
			*(*(long*)(ptr + 48L / (long)sizeof(XEEvent)) + 96L) = <Module>.XE_VariantLoad<int>(num19);
			_GUID* ptr10 = <Module>.@new(16UL);
			CAutoP<_GUID> cautoP<_GUID>;
			<Module>.CAutoP<_GUID>.{ctor}(ref cautoP<_GUID>, ptr10);
			try
			{
				IntPtr intPtr2 = new IntPtr(<Module>.CAutoBase<_GUID>.Get(ref cautoP<_GUID>));
				Marshal.StructureToPtr(xeventAttribute.EventGuid, intPtr2, false);
				*(*(long*)(ptr + 48L / (long)sizeof(XEEvent)) + 16L) = <Module>.XE_VariantLoad<struct\u0020_GUID\u0020*>(<Module>.CAutoBase<_GUID>.PvReturn(ref cautoP<_GUID>));
				if (num12 > 3)
				{
					num17 = (int)num12;
					*(*(long*)(ptr + 48L / (long)sizeof(XEEvent)) + (long)(num17 - 1) * 40L + 16L) = <Module>.XE_VariantLoad<unsigned\u0020__int64>(num13);
				}
				ptr11 = xeventAutoObject<XEEvent>.PvReturn();
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(CAutoP<_GUID>.{dtor}), (void*)(&cautoP<_GUID>));
				throw;
			}
			<Module>.CAutoP<_GUID>.{dtor}(ref cautoP<_GUID>);
		}
		catch
		{
			((IDisposable)xeventAutoObject<XEEvent>).Dispose();
			throw;
		}
		((IDisposable)xeventAutoObject<XEEvent>).Dispose();
		return ptr11;
		try
		{
		}
		catch
		{
			((IDisposable)xeventAutoObject<XEEvent>).Dispose();
			throw;
		}
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000041CC File Offset: 0x000041CC
	internal static Type[] Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetEventClassTypes(Assembly assembly)
	{
		List<Type> list = new List<Type>();
		Type[] types = assembly.GetTypes();
		int num = 0;
		if (0 < types.Length)
		{
			do
			{
				if (Attribute.IsDefined(types[num], typeof(XEventAttribute)))
				{
					list.Add(types[num]);
				}
				num++;
			}
			while (num < types.Length);
		}
		return list.ToArray();
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00004220 File Offset: 0x00004220
	internal static Type[] Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetMapClassTypes(Assembly assembly)
	{
		List<Type> list = new List<Type>();
		Type[] types = assembly.GetTypes();
		int num = 0;
		if (0 < types.Length)
		{
			do
			{
				if (Attribute.IsDefined(types[num], typeof(XEventMapAttribute)))
				{
					list.Add(types[num]);
				}
				num++;
			}
			while (num < types.Length);
		}
		return list.ToArray();
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00004274 File Offset: 0x00004274
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.InitEventClass(Type eventClass, uint bitIndex, XBitmap<ExternalStorage>* enabledBitmap)
	{
		MethodInfo method = eventClass.GetMethod("Init", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		if (method != null)
		{
			object[] array = new object[2];
			array[0] = bitIndex;
			IntPtr intPtr = new IntPtr((void*)enabledBitmap);
			array[1] = intPtr;
			method.Invoke(null, array);
			return;
		}
		throw new XEventInvalidEventTypeException(eventClass, "Init");
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000042D8 File Offset: 0x000042D8
	internal static void Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.UninitEventClass(Type eventClass)
	{
		MethodInfo method = eventClass.GetMethod("Init", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		if (method != null)
		{
			object[] array = new object[2];
			array[0] = null;
			IntPtr intPtr = new IntPtr(null);
			array[1] = intPtr;
			method.Invoke(null, array);
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000082E0 File Offset: 0x000082E0
	internal unsafe static XEventPackageSet* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.Create(XEventPackageAttribute pkgAttr, Assembly assembly)
	{
		XEventPackageSet* ptr = <Module>.@new(352UL);
		XEventPackageSet* ptr2;
		try
		{
			if (ptr != null)
			{
				ptr2 = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.{ctor}(ptr);
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			<Module>.delete((void*)ptr, 352UL);
			throw;
		}
		CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet> cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>;
		<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.{ctor}(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>, ptr2);
		XEventPackageSet* ptr20;
		try
		{
			XEPackage* ptr3 = <Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>);
			<Module>.gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.=(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 336L, new Dictionary<string, uint>());
			if (pkgAttr.ResourceBaseName != null)
			{
				<Module>.gcroot<System::Resources::ResourceManager\u0020^>.=(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 344L, new ResourceManager(pkgAttr.ResourceBaseName, assembly));
			}
			XE_ObjectSList* ptr4 = <Module>.@new(8UL);
			XE_ObjectSList* ptr5;
			try
			{
				if (ptr4 != null)
				{
					<Module>.SList.{ctor}(ptr4);
					ptr5 = ptr4;
				}
				else
				{
					ptr5 = null;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr4, 8UL);
				throw;
			}
			CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList> cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>;
			<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>.{ctor}(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>, ptr5);
			try
			{
				Dictionary<Type, XEventAutoObject<XEMap>> dictionary = new Dictionary<Type, XEventAutoObject<XEMap>>();
				Dictionary<string, uint> dictionary2 = new Dictionary<string, uint>();
				XEventAutoObject<XEMap> xeventAutoObject<XEMap> = new XEventAutoObject<XEMap>();
				xeventAutoObject<XEMap>.Set(<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.BuildXEObject<struct\u0020XEMap>(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>), 0, 0, 0, 0U, "keyword_map", null));
				*(long*)(xeventAutoObject<XEMap>.Get() + 16L / (long)sizeof(XEMap)) = ResourcesMgr.GetNativeString(ReservedResources.GetReservedResourceString(ReservedResources.ReservedResourceIds.KeywordColumn));
				*(int*)(xeventAutoObject<XEMap>.Get() + 24L / (long)sizeof(XEMap)) = 4;
				*(byte*)(xeventAutoObject<XEMap>.Get() + 32L / (long)sizeof(XEMap)) = 4;
				*(short*)(xeventAutoObject<XEMap>.Get() + 34L / (long)sizeof(XEMap)) = 0;
				dictionary.Add(typeof(XEventManagedPackage), xeventAutoObject<XEMap>);
				Type[] array = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetEventClassTypes(assembly);
				int num = 0;
				if (0 < array.Length)
				{
					do
					{
						XE_ObjectSListEntry* ptr6 = <Module>.@new(16UL);
						XE_ObjectSListEntry* ptr7;
						try
						{
							if (ptr6 != null)
							{
								<Module>.SList.{ctor}(ptr6);
								*(long*)(ptr6 + 8L / (long)sizeof(XE_ObjectSListEntry)) = 0L;
								ptr7 = ptr6;
							}
							else
							{
								ptr7 = null;
							}
						}
						catch
						{
							<Module>.delete((void*)ptr6, 16UL);
							throw;
						}
						CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry> cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>;
						<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.{ctor}(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>, ptr7);
						try
						{
							XEEvent* ptr8 = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateXEEvent(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>), <Module>.CAutoBase<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>..PEAVXEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>), dictionary, dictionary2, array[num]);
							*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>) + 8L) = ptr8;
							<Module>.TSinglyLinkedList<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.InsertFirst(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>), <Module>.CAutoBase<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.PvReturn(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>));
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.{dtor}), (void*)(&cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>));
							throw;
						}
						<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.{dtor}(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>);
						num++;
					}
					while (num < array.Length);
				}
				Type[] array2 = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetMapClassTypes(assembly);
				num = 0;
				if (0 < array2.Length)
				{
					do
					{
						if (Attribute.IsDefined(array2[num], typeof(XEventMapAttribute)) && !dictionary.ContainsKey(array2[num]))
						{
							XEventAutoObject<XEMap> xeventAutoObject<XEMap>2 = new XEventAutoObject<XEMap>();
							xeventAutoObject<XEMap>2.Set(<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateXEMap(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>), array2[num]));
							dictionary.Add(array2[num], xeventAutoObject<XEMap>2);
						}
						num++;
					}
					while (num < array2.Length);
				}
				uint num2 = (uint)(*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 320L));
				<Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.SetEventTypesList(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 160L, array);
				uint* ptr9 = <Module>.new[](<Module>.ExternalStorage.CalculateWordCount(num2) * 4UL);
				XEventManagedPackage* ptr10 = <Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 160L + 24L;
				<Module>.ExternalStorage.Init(<Module>.XBitmap<ExternalStorage>.GetStorage(ptr10), ptr9, num2);
				<Module>.XBitmap<ExternalStorage>.Clear(ptr10);
				<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.FillXEObjectDetails(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>), (XEObject*)ptr3, 10, 0, 4, 0U, pkgAttr.Name, pkgAttr.DescriptionKey, (XEObjectType)8);
				List<XEventAutoObject<XETarget>> list = null;
				AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
				num = 0;
				if (0 < referencedAssemblies.Length)
				{
					AssemblyName assemblyName;
					do
					{
						assemblyName = referencedAssemblies[num];
						if (string.Compare(assemblyName.Name, "Microsoft.SqlServer.XEvent.Targets") == 0)
						{
							goto IL_0319;
						}
						num++;
					}
					while (num < referencedAssemblies.Length);
					goto IL_0677;
					IL_0319:
					Type type = Assembly.Load(assemblyName).GetType("Microsoft.SqlServer.XEvent.Internal.TargetBuilder");
					MethodBase method = type.GetMethod("GetTargetsList");
					MethodInfo method2 = type.GetMethod("GetTargetInfo");
					MethodInfo method3 = type.GetMethod("GetTargetParameters");
					MethodInfo methodInfo = type.GetMethod("ProcessBuffer");
					ptr10 = <Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 160L;
					*(ptr10 + 120L) = AppDomain.CurrentDomain.Id;
					<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.=(ptr10 + 128L, methodInfo);
					methodInfo = type.GetMethod("CreateTarget");
					<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.=(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 160L + 136L, methodInfo);
					methodInfo = type.GetMethod("FinalizeTarget");
					<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.=(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 160L + 144L, methodInfo);
					methodInfo = type.GetMethod("GetSerializedState");
					<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.=(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 160L + 152L, methodInfo);
					List<Type> list2 = (List<Type>)method.Invoke(null, new object[] { assembly });
					if (list2 != null && list2.Count > 0)
					{
						list = new List<XEventAutoObject<XETarget>>();
						Type[] array3 = list2.ToArray();
						<Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.SetTargetTypesList(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 160L, array3);
						num = 0;
						if (0 < array3.Length)
						{
							Tuple<string, string, FieldInfo> tuple;
							for (;;)
							{
								object[] array4 = new object[] { array3[num] };
								KeyValuePair<string, string> keyValuePair = (KeyValuePair<string, string>)method2.Invoke(null, array4);
								XEventPackageRegistrar.CheckNamingRules(keyValuePair.Key);
								XEventAutoObject<XETarget> xeventAutoObject<XETarget> = new XEventAutoObject<XETarget>();
								xeventAutoObject<XETarget>.Set(<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.BuildXEObject<struct\u0020XETarget>(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>), 0, 0, 1, (uint)num, keyValuePair.Key, keyValuePair.Value));
								List<Tuple<string, string, FieldInfo>> list3 = (List<Tuple<string, string, FieldInfo>>)method3.Invoke(null, array4);
								XETarget* ptr11 = xeventAutoObject<XETarget>.Get();
								*(short*)(ptr11 + 32L / (long)sizeof(XETarget)) = 0;
								ulong num3 = (ulong)((long)list3.Count);
								XECustomizableAttribute* ptr12 = <Module>.new[]((num3 > 329406144173384850UL) ? ulong.MaxValue : (num3 * 56UL));
								*(long*)(ptr11 + 40L / (long)sizeof(XETarget)) = ptr12;
								initblk(*(long*)(ptr11 + 40L / (long)sizeof(XETarget)), 0, (long)list3.Count * 56L);
								List<Tuple<string, string, FieldInfo>>.Enumerator enumerator = list3.GetEnumerator();
								if (enumerator.MoveNext())
								{
									do
									{
										tuple = enumerator.Current;
										XEventPackageRegistrar.CheckNamingRules(tuple.Item1);
										if (!XEventTypeMapping.IsTypeSupported(tuple.Item3.FieldType))
										{
											goto IL_0667;
										}
										XERelativeObjectId xerelativeObjectId = XEventTypeMapping.XETypeFromManagedType(tuple.Item3.FieldType);
										if (<Module>.XE_Compare(xerelativeObjectId, <Module>.XET_VLD_WSTR) != null)
										{
											xerelativeObjectId = <Module>.XET_WSTR;
										}
										if (<Module>.XE_IsRidVld(xerelativeObjectId) != null)
										{
											goto IL_0657;
										}
										ushort num4 = *(ushort*)(ptr11 + 32L / (long)sizeof(XETarget));
										ptr12 = num4 * 56L + *(long*)(ptr11 + 40L / (long)sizeof(XETarget)) / (long)sizeof(XECustomizableAttribute);
										*(short*)(ptr11 + 32L / (long)sizeof(XETarget)) = (short)(num4 + 1);
										cpblk(ptr12, ref xerelativeObjectId, 4);
										IntPtr intPtr = Marshal.StringToHGlobalUni(tuple.Item1);
										*(long*)(ptr12 + 8L / (long)sizeof(XECustomizableAttribute)) = intPtr.ToPointer();
										*(int*)(ptr12 + 36L / (long)sizeof(XECustomizableAttribute)) = 1;
										<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.AssignDescription<struct\u0020XECustomizableAttribute>(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>), tuple.Item2, ptr12);
									}
									while (enumerator.MoveNext());
								}
								*(long*)(ptr11 + 48L / (long)sizeof(XETarget)) = <Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 160L;
								list.Add(xeventAutoObject<XETarget>);
								num++;
								if (num >= array3.Length)
								{
									goto IL_0677;
								}
							}
							IL_0657:
							throw new XEventUnsupportedTypeException(array3[num], tuple.Item3);
							IL_0667:
							throw new XEventUnsupportedTypeException(array3[num], tuple.Item3);
						}
					}
				}
				IL_0677:
				if (pkgAttr.ResourceBaseName != null)
				{
					string[] array5 = new string[<Module>.gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.->(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 336L).Count + 5];
					Dictionary<string, uint>.Enumerator enumerator2 = <Module>.gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>..PE$AAV?$Dictionary@PE$AAVString@System@@I@Generic@Collections@System@@(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 336L).GetEnumerator();
					if (enumerator2.MoveNext())
					{
						do
						{
							KeyValuePair<string, uint> keyValuePair2 = enumerator2.Current;
							array5[(int)keyValuePair2.Value] = keyValuePair2.Key;
						}
						while (enumerator2.MoveNext());
					}
					ResourceManager resourceManager = <Module>.gcroot<System::Resources::ResourceManager\u0020^>..PE$AAVResourceManager@Resources@System@@(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 344L);
					XEventManagedPackage* ptr13 = <Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 160L;
					<Module>.gcroot<cli::array<System::PE$AAVString\u0020>^>.=(ptr13 + 104L, array5);
					<Module>.gcroot<System::Resources::ResourceManager\u0020^>.=(ptr13 + 112L, resourceManager);
					<Module>.gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.=(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 336L, null);
				}
				IntPtr intPtr2 = new IntPtr((void*)(ptr3 + 32L / (long)sizeof(XEPackage)));
				Marshal.StructureToPtr(pkgAttr.PackageGuid, intPtr2, false);
				IntPtr hinstance = Marshal.GetHINSTANCE(typeof(XEventPackageRegistrar).Module);
				*(long*)(ptr3 + 72L / (long)sizeof(XEPackage)) = hinstance.ToPointer();
				cpblk(ptr3 + 56L / (long)sizeof(XEPackage), ref <Module>.XEMANAGED_MODULE_GUID, 16);
				*(long*)(ptr3 + 48L / (long)sizeof(XEPackage)) = 0L;
				*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 88L) = ptr3;
				XEObjectCollection* ptr14 = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEEvent>((int)num2);
				<Module>.new[](((ulong)num2 + 2UL) * 8UL);
				*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 96L) = ptr14;
				XE_TCollection<0,0> xe_TCollection<0,0>;
				<Module>.XE_TCollection<0,0>.{ctor}(ref xe_TCollection<0,0>, ptr14);
				*(int*)(ptr14 + 8L / (long)sizeof(XEObjectCollection)) = (int)num2;
				XBitmap<ExternalStorage>* ptr15 = <Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) / sizeof(XBitmap<ExternalStorage>) + 160L + 24L / (long)sizeof(XBitmap<ExternalStorage>);
				if (<Module>.SinglyLinkedListBase.IsEmpty(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>)) == null)
				{
					do
					{
						XE_ObjectSListEntry* ptr16 = <Module>.TSinglyLinkedList<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.RemoveHeadFromNonEmptyList(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>));
						long num5 = *(long*)(ptr16 + 8L / (long)sizeof(XE_ObjectSListEntry));
						<Module>.XE_TCollection<0,0>.Set(ref xe_TCollection<0,0>, ((uint)(*(num5 + 4L)) >> 10) & 262143U, num5);
						uint num6 = ((uint)(*(*(long*)(ptr16 + 8L / (long)sizeof(XE_ObjectSListEntry)) + 4L)) >> 10) & 262143U;
						<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.InitEventClass(array[(int)num6], num6, ptr15);
						*(long*)(ptr16 + 8L / (long)sizeof(XE_ObjectSListEntry)) = 0L;
						<Module>.delete((void*)ptr16, 16UL);
					}
					while (<Module>.SinglyLinkedListBase.IsEmpty(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>)) == null);
				}
				if (dictionary2.Count > 0)
				{
					ulong num7 = (ulong)((long)dictionary2.Count);
					XEMapEntry* ptr17 = <Module>.new[]((num7 > 1152921504606846975UL) ? ulong.MaxValue : (num7 * 16UL));
					*(long*)(xeventAutoObject<XEMap>.Get() + 40L / (long)sizeof(XEMap)) = ptr17;
					ushort count = (ushort)dictionary2.Count;
					*(short*)(xeventAutoObject<XEMap>.Get() + 34L / (long)sizeof(XEMap)) = (short)count;
					initblk(*(long*)(xeventAutoObject<XEMap>.Get() + 40L / (long)sizeof(XEMap)), 0, (long)dictionary2.Count * 16L);
					Dictionary<string, uint>.Enumerator enumerator3 = dictionary2.GetEnumerator();
					if (enumerator3.MoveNext())
					{
						do
						{
							KeyValuePair<string, uint> keyValuePair3 = enumerator3.Current;
							*(*(long*)(xeventAutoObject<XEMap>.Get() + 40L / (long)sizeof(XEMap))) = (int)keyValuePair3.Value;
							IntPtr intPtr3 = Marshal.StringToHGlobalUni(keyValuePair3.Key);
							*(*(long*)(xeventAutoObject<XEMap>.Get() + 40L / (long)sizeof(XEMap)) + 8L) = intPtr3.ToPointer();
						}
						while (enumerator3.MoveNext());
					}
				}
				ptr14 = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEMap>(dictionary.Values.Count);
				*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 120L) = ptr14;
				XE_TCollection<0,0> xe_TCollection<0,0>2;
				<Module>.XE_TCollection<0,0>.{ctor}(ref xe_TCollection<0,0>2, ptr14);
				Dictionary<Type, XEventAutoObject<XEMap>>.ValueCollection.Enumerator enumerator4 = dictionary.Values.GetEnumerator();
				if (enumerator4.MoveNext())
				{
					do
					{
						XEventAutoObject<XEMap> xeventAutoObject<XEMap>3 = enumerator4.Current;
						*(int*)(ptr14 + 8L / (long)sizeof(XEObjectCollection)) = *(int*)(ptr14 + 8L / (long)sizeof(XEObjectCollection)) + 1;
						<Module>.XE_TCollection<0,0>.Set(ref xe_TCollection<0,0>2, ((uint)(*(int*)(xeventAutoObject<XEMap>3.Get() + 4L / (long)sizeof(XEMap))) >> 10) & 262143U, (XEObject*)xeventAutoObject<XEMap>3.PvReturn());
					}
					while (enumerator4.MoveNext());
				}
				ptr14 = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XETarget>((list != null) ? list.Count : 0);
				*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 112L) = ptr14;
				XE_TCollection<0,0> xe_TCollection<0,0>3;
				<Module>.XE_TCollection<0,0>.{ctor}(ref xe_TCollection<0,0>3, ptr14);
				if (list != null)
				{
					List<XEventAutoObject<XETarget>>.Enumerator enumerator5 = list.GetEnumerator();
					if (enumerator5.MoveNext())
					{
						do
						{
							XEventAutoObject<XETarget> xeventAutoObject<XETarget>2 = enumerator5.Current;
							*(int*)(ptr14 + 8L / (long)sizeof(XEObjectCollection)) = *(int*)(ptr14 + 8L / (long)sizeof(XEObjectCollection)) + 1;
							<Module>.XE_TCollection<0,0>.Set(ref xe_TCollection<0,0>3, ((uint)(*(int*)(xeventAutoObject<XETarget>2.Get() + 4L / (long)sizeof(XETarget))) >> 10) & 262143U, (XEObject*)xeventAutoObject<XETarget>2.PvReturn());
						}
						while (enumerator5.MoveNext());
					}
				}
				*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 104L) = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEAction>(0);
				*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 128L) = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEPredicateSource>(0);
				*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 136L) = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEPredicateCompare>(0);
				*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 144L) = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEType>(0);
				*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 152L) = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEMessage>(0);
				XEPackageMetadata* ptr18 = <Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) / sizeof(XEPackageMetadata) + 88L;
				*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 160L + 16L) = ptr18;
				XEPackage* ptr19 = <Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>);
				*(<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>) + 160L + 8L) = ptr19;
				ptr20 = <Module>.CAutoBase<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.PvReturn(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>.{dtor}), (void*)(&cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>));
				throw;
			}
			<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>.{dtor}(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.{dtor}), (void*)(&cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>));
			throw;
		}
		<Module>.CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.{dtor}(ref cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>);
		return ptr20;
		try
		{
			try
			{
			}
			catch
			{
				CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList> cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>;
				<Module>.___CxxCallUnwindDtor(ldftn(CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>.{dtor}), (void*)(&cautoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.{dtor}), (void*)(&cautoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>));
			throw;
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00005D90 File Offset: 0x00005D90
	internal unsafe static XEventPackageSet* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.{ctor}(XEventPackageSet* A_0)
	{
		<Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.{ctor}(A_0 + 160L);
		try
		{
			*(A_0 + 320L) = 0;
			*(A_0 + 324L) = 1;
			*(A_0 + 328L) = 1;
			*(A_0 + 332L) = 5;
			<Module>.gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.{ctor}(A_0 + 336L);
			try
			{
				*(A_0 + 344L) = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
				try
				{
					initblk(A_0, 0, 160);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Resources::ResourceManager\u0020^>.{dtor}), (void*)(A_0 + (byte*)344L));
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.{dtor}), (void*)(A_0 + (byte*)336L));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.{dtor}), (void*)(A_0 + (byte*)160L));
			throw;
		}
		return A_0;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00005EA0 File Offset: 0x00005EA0
	internal unsafe static uint Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetResourceId(XEventPackageSet* A_0, string descriptionKey)
	{
		uint num = 0U;
		if (!string.IsNullOrEmpty(descriptionKey) && <Module>.gcroot<System::Resources::ResourceManager\u0020^>..PE$AAVResourceManager@Resources@System@@(A_0 + 344L) != null)
		{
			XEventPackageSet* ptr = A_0 + 336L;
			if (!<Module>.gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.->(ptr).TryGetValue(descriptionKey, out num))
			{
				num = (uint)(*(A_0 + 332L));
				<Module>.gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.->(ptr).Add(descriptionKey, num);
				*(A_0 + 332L) = *(A_0 + 332L) + 1;
			}
		}
		return num;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000071B0 File Offset: 0x000071B0
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.FillXEObjectDetails(XEventPackageSet* A_0, XEObject* pObj, ushort version, byte genericCapabilities, byte specificCapailities, uint objectId, string name, string descriptionKey, XEObjectType objectType)
	{
		long num = (long)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
		uint exceptionCode;
		try
		{
			*(long*)(pObj + 8L / (long)sizeof(XEObject)) = 0L;
			*(long*)(pObj + 16L / (long)sizeof(XEObject)) = 0L;
			*(short*)pObj = (short)version;
			*(byte*)(pObj + 2L / (long)sizeof(XEObject)) = genericCapabilities;
			*(byte*)(pObj + 3L / (long)sizeof(XEObject)) = specificCapailities;
			IntPtr intPtr = Marshal.StringToHGlobalUni(name);
			*(long*)(pObj + 8L / (long)sizeof(XEObject)) = intPtr.ToPointer();
			<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.AssignDescription<struct\u0020XEObject>(A_0, descriptionKey, pObj);
			*(int*)(pObj + 4L / (long)sizeof(XEObject)) = *(int*)(pObj + 4L / (long)sizeof(XEObject)) | 1023;
			uint num2 = (uint)(*(int*)(pObj + 4L / (long)sizeof(XEObject)));
			uint num3 = (((objectId << 10) ^ num2) & 268434432U) ^ num2;
			*(int*)(pObj + 4L / (long)sizeof(XEObject)) = (int)num3;
			*(int*)(pObj + 4L / (long)sizeof(XEObject)) = (int)((num3 & 268435455U) | (uint)((uint)objectType << 28));
		}
		catch when (delegate
		{
			// Failed to create a 'catch-when' expression
			exceptionCode = (uint)Marshal.GetExceptionCode();
			endfilter(<Module>.__CxxExceptionFilter(Marshal.GetExceptionPointers(), null, 0, null) != null);
		})
		{
			uint num4 = 0U;
			<Module>.__CxxRegisterExceptionObject(Marshal.GetExceptionPointers(), num);
			try
			{
				try
				{
					<Module>.Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XEObject>(pObj);
					<Module>._CxxThrowException(null, null);
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					num4 = <Module>.__CxxDetectRethrow(Marshal.GetExceptionPointers());
					endfilter(num4 != 0U);
				})
				{
				}
				if (num4 != 0U)
				{
					throw;
				}
			}
			finally
			{
				<Module>.__CxxUnregisterExceptionObject(num, (int)num4);
			}
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00007960 File Offset: 0x00007960
	internal unsafe static XEMap* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateXEMap(XEventPackageSet* A_0, Type enumType)
	{
		string text = enumType.Name;
		string text2;
		if (Attribute.IsDefined(enumType, typeof(XEventMapAttribute)))
		{
			XEventMapAttribute xeventMapAttribute = Attribute.GetCustomAttribute(enumType, typeof(XEventMapAttribute)) as XEventMapAttribute;
			text = xeventMapAttribute.Name;
			text2 = xeventMapAttribute.DescriptionKey;
		}
		else
		{
			text2 = "";
		}
		XEMapCapabilities xemapCapabilities = (Attribute.IsDefined(enumType, typeof(FlagsAttribute)) ? ((XEMapCapabilities)1) : ((XEMapCapabilities)0));
		XEventAutoObject<XEMap> xeventAutoObject<XEMap> = new XEventAutoObject<XEMap>();
		uint num = (uint)(*(A_0 + 324L));
		uint num2 = num;
		*(A_0 + 324L) = (int)(num + 1U);
		xeventAutoObject<XEMap>.Set(<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.BuildXEObject<struct\u0020XEMap>(A_0, 0, 0, (byte)xemapCapabilities, num2, text, text2));
		XEMap* ptr = xeventAutoObject<XEMap>.Get();
		string[] names = Enum.GetNames(enumType);
		Array.Sort<string>(names);
		*(byte*)(ptr + 32L / (long)sizeof(XEMap)) = 4;
		*(short*)(ptr + 34L / (long)sizeof(XEMap)) = 0;
		ulong num3 = (ulong)((long)names.Length);
		XEMapEntry* ptr2 = <Module>.new[]((num3 > 1152921504606846975UL) ? ulong.MaxValue : (num3 * 16UL));
		*(long*)(ptr + 40L / (long)sizeof(XEMap)) = ptr2;
		initblk(ptr2, 0, (long)names.Length * 16L);
		ushort num4 = 0;
		if (0 < names.Length)
		{
			int num5;
			for (;;)
			{
				num5 = (int)Enum.Parse(enumType, names[(int)num4]);
				ushort num6 = 0;
				if (0 < num4)
				{
					long num7 = *(long*)(ptr + 40L / (long)sizeof(XEMap));
					while (*((ulong)num6 * 16UL + (ulong)num7) != num5)
					{
						num6 += 1;
						if (num6 >= num4)
						{
							goto IL_0142;
						}
					}
					break;
				}
				IL_0142:
				long num8 = (long)((ulong)num4 * 16UL);
				*(num8 + *(long*)(ptr + 40L / (long)sizeof(XEMap))) = num5;
				IntPtr intPtr = Marshal.StringToHGlobalUni(names[(int)num4]);
				*(*(long*)(ptr + 40L / (long)sizeof(XEMap)) + num8 + 8L) = intPtr.ToPointer();
				*(short*)(ptr + 34L / (long)sizeof(XEMap)) = (short)(*(ushort*)(ptr + 34L / (long)sizeof(XEMap)) + 1);
				num4 += 1;
				if ((int)num4 >= names.Length)
				{
					goto IL_0198;
				}
			}
			throw new XEventInvalidMapException(enumType, text, num5);
		}
		IL_0198:
		return xeventAutoObject<XEMap>.PvReturn();
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00004328 File Offset: 0x00004328
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.StaticInit()
	{
		int num = 0;
		if (<Module>.XE_API.IsEnginePresent() != null && <Module>.?sm_initLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA == null && <Module>.?sm_targetLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA == null && <Module>.?sm_eventLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA == null)
		{
			XE_AutoResource<void\u0020*,136> xe_AutoResource<void_u0020*,136>;
			<Module>.XE_AutoResource<void\u0020*,136>.{ctor}(ref xe_AutoResource<void_u0020*,136>, null);
			try
			{
				XE_AutoResource<void\u0020*,136> xe_AutoResource<void_u0020*,136>2;
				<Module>.XE_AutoResource<void\u0020*,136>.{ctor}(ref xe_AutoResource<void_u0020*,136>2, null);
				try
				{
					XE_AutoResource<void\u0020*,136> xe_AutoResource<void_u0020*,136>3;
					<Module>.XE_AutoResource<void\u0020*,136>.{ctor}(ref xe_AutoResource<void_u0020*,136>3, null);
					try
					{
						if (calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void**), <Module>.CAutoBase<void>.&(ref xe_AutoResource<void_u0020*,136>), (IntPtr)(*(<Module>.XE_API.ServiceAPI() + 128L))) != null)
						{
							<Module>.?sm_initLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA = <Module>.CAutoBase<void>..PEAX(ref xe_AutoResource<void_u0020*,136>);
							if (calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void**), <Module>.CAutoBase<void>.&(ref xe_AutoResource<void_u0020*,136>2), (IntPtr)(*(<Module>.XE_API.ServiceAPI() + 128L))) != null && calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void**), <Module>.CAutoBase<void>.&(ref xe_AutoResource<void_u0020*,136>3), (IntPtr)(*(<Module>.XE_API.ServiceAPI() + 128L))) != null)
							{
								<Module>.?sm_eventLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA = <Module>.CAutoBase<void>..PEAX(ref xe_AutoResource<void_u0020*,136>3);
								num = 1;
								<Module>.?sm_initLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA = <Module>.CAutoBase<void>.PvReturn(ref xe_AutoResource<void_u0020*,136>);
								<Module>.?sm_targetLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA = <Module>.CAutoBase<void>.PvReturn(ref xe_AutoResource<void_u0020*,136>2);
								<Module>.?sm_eventLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA = <Module>.CAutoBase<void>.PvReturn(ref xe_AutoResource<void_u0020*,136>3);
								goto IL_010B;
							}
						}
						Trace.TraceError("XEvent Package Manager could not be initialized. Failed to allocate synchronization objects.");
						<Module>.?sm_initLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA = null;
						<Module>.?sm_targetLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA = null;
						<Module>.?sm_eventLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA = null;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoResource<void\u0020*,136>.{dtor}), (void*)(&xe_AutoResource<void_u0020*,136>3));
						throw;
					}
					IL_010B:
					<Module>.XE_AutoResource<void\u0020*,136>.{dtor}(ref xe_AutoResource<void_u0020*,136>3);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoResource<void\u0020*,136>.{dtor}), (void*)(&xe_AutoResource<void_u0020*,136>2));
					throw;
				}
				<Module>.XE_AutoResource<void\u0020*,136>.{dtor}(ref xe_AutoResource<void_u0020*,136>2);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoResource<void\u0020*,136>.{dtor}), (void*)(&xe_AutoResource<void_u0020*,136>));
				throw;
			}
			<Module>.XE_AutoResource<void\u0020*,136>.{dtor}(ref xe_AutoResource<void_u0020*,136>);
		}
		return num;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000044CC File Offset: 0x000044CC
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.InitPackage(XEPackage* package, ushort packageID, void** phPackage)
	{
		int num = 0;
		XE_AutoEngineMutex xe_AutoEngineMutex;
		<Module>.XE_AutoEngineMutex.{ctor}(ref xe_AutoEngineMutex, <Module>.?sm_initLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA, (XEWaitType)0);
		try
		{
			<Module>.XE_AutoEngineMutex.GetAccess(ref xe_AutoEngineMutex, uint.MaxValue);
			XEPackage* ptr = package + 160L;
			if (calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,XEPackage modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt16), ptr, package, packageID, (IntPtr)(*(*ptr))) != null)
			{
				*phPackage = ptr;
				num = 1;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoEngineMutex.{dtor}), (void*)(&xe_AutoEngineMutex));
			throw;
		}
		<Module>.XE_AutoEngineMutex.{dtor}(ref xe_AutoEngineMutex);
		return num;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000454C File Offset: 0x0000454C
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.FinalizePackage(void* hPackage)
	{
		XE_AutoEngineMutex xe_AutoEngineMutex;
		<Module>.XE_AutoEngineMutex.{ctor}(ref xe_AutoEngineMutex, <Module>.?sm_initLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA, (XEWaitType)0);
		try
		{
			<Module>.XE_AutoEngineMutex.GetAccess(ref xe_AutoEngineMutex, uint.MaxValue);
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), hPackage, (IntPtr)(*(*(long*)hPackage + 8L)));
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoEngineMutex.{dtor}), (void*)(&xe_AutoEngineMutex));
			throw;
		}
		<Module>.XE_AutoEngineMutex.{dtor}(ref xe_AutoEngineMutex);
		return 1;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000045B8 File Offset: 0x000045B8
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.GetMetadata(void* hPackage, XEPackageMetadata** metadata)
	{
		long num = calli(XEPackageMetadata modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), hPackage, (IntPtr)(*(*(long*)hPackage + 16L)));
		*metadata = num;
		return (num != 0L) ? 1 : 0;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000045E8 File Offset: 0x000045E8
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.EnableEvent(void* hPackage, XESessionContext* pSessionContext)
	{
		XE_AutoEngineMutex xe_AutoEngineMutex;
		<Module>.XE_AutoEngineMutex.{ctor}(ref xe_AutoEngineMutex, <Module>.?sm_eventLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA, (XEWaitType)0);
		int num;
		try
		{
			<Module>.XE_AutoEngineMutex.GetAccess(ref xe_AutoEngineMutex, uint.MaxValue);
			num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,XESessionContext* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), hPackage, pSessionContext, (IntPtr)(*(*(long*)hPackage + 24L)));
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoEngineMutex.{dtor}), (void*)(&xe_AutoEngineMutex));
			throw;
		}
		<Module>.XE_AutoEngineMutex.{dtor}(ref xe_AutoEngineMutex);
		return num;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00004658 File Offset: 0x00004658
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.IsEventEnabled(void* hPackage, XESessionContext* pSessionContext, int* pEnabled)
	{
		XE_AutoEngineMutex xe_AutoEngineMutex;
		<Module>.XE_AutoEngineMutex.{ctor}(ref xe_AutoEngineMutex, <Module>.?sm_eventLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA, (XEWaitType)0);
		int num;
		try
		{
			<Module>.XE_AutoEngineMutex.GetAccess(ref xe_AutoEngineMutex, uint.MaxValue);
			num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,XESessionContext* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.Int32* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), hPackage, pSessionContext, pEnabled, (IntPtr)(*(*(long*)hPackage + 32L)));
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoEngineMutex.{dtor}), (void*)(&xe_AutoEngineMutex));
			throw;
		}
		<Module>.XE_AutoEngineMutex.{dtor}(ref xe_AutoEngineMutex);
		return num;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000046C8 File Offset: 0x000046C8
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.DisableEvent(void* hPackage, XESessionContext* pSessionContext)
	{
		XE_AutoEngineMutex xe_AutoEngineMutex;
		<Module>.XE_AutoEngineMutex.{ctor}(ref xe_AutoEngineMutex, <Module>.?sm_eventLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA, (XEWaitType)0);
		int num;
		try
		{
			<Module>.XE_AutoEngineMutex.GetAccess(ref xe_AutoEngineMutex, uint.MaxValue);
			num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,XESessionContext* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), hPackage, pSessionContext, (IntPtr)(*(*(long*)hPackage + 40L)));
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoEngineMutex.{dtor}), (void*)(&xe_AutoEngineMutex));
			throw;
		}
		<Module>.XE_AutoEngineMutex.{dtor}(ref xe_AutoEngineMutex);
		return num;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00004738 File Offset: 0x00004738
	internal unsafe static XEError* Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.GetLastError()
	{
		return calli(XEError modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)(*(<Module>.XE_API.ServiceAPI() + 384L)));
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00005F14 File Offset: 0x00005F14
	internal unsafe static XEventTargetForwarder* Microsoft.SqlServer.XEvent.Internal.XEventTargetForwarder.{ctor}(XEventTargetForwarder* A_0, object targetInstance, List<KeyValuePair<string, object>> targetParameters, MethodInfo targetCreator, MethodInfo targetFinalizer, MethodInfo bufferProcessor, MethodInfo targetSerializer, void* hSess, uint targetAppDomain)
	{
		<Module>.gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>.{ctor}(A_0);
		try
		{
			XEventTargetForwarder* ptr = A_0 + 8L;
			<Module>.gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>.{ctor}(ptr);
			try
			{
				XEventTargetForwarder* ptr2 = A_0 + 16L;
				<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.{ctor}(ptr2);
				try
				{
					XEventTargetForwarder* ptr3 = A_0 + 32L;
					*ptr3 = 0;
					IntPtr intPtr = new IntPtr(hSess);
					<Module>.gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>.=(A_0, (BufferProcessor)Delegate.CreateDelegate(typeof(BufferProcessor), bufferProcessor));
					<Module>.gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>.=(ptr, (TargetSerializer)Delegate.CreateDelegate(typeof(TargetSerializer), targetSerializer));
					<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.=(ptr2, targetFinalizer);
					IntPtr intPtr2 = ((TargetCreator)Delegate.CreateDelegate(typeof(TargetCreator), targetCreator))(targetInstance, targetParameters, intPtr);
					*ptr3 = intPtr2;
					*(A_0 + 24L) = targetAppDomain;
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}), (void*)(A_0 + (byte*)16L));
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>.{dtor}), (void*)(A_0 + (byte*)8L));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00006054 File Offset: 0x00006054
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool Microsoft.SqlServer.XEvent.Internal.XEventTargetForwarder.GetSerializedState(XEventTargetForwarder* A_0, char* buffer, ulong bufferLen, char** pBufferEnd, ulong* pRemainingLen)
	{
		string text = <Module>.gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>..PE$AAVTargetSerializer@Internal@XEvent@SqlServer@Microsoft@@(A_0 + 8L)(*(A_0 + 32L));
		bool flag;
		if ((long)text.Length < (long)bufferLen)
		{
			ref byte ptr = text;
			if ((ref ptr) != null)
			{
				ptr = (long)RuntimeHelpers.OffsetToStringData + (ref ptr);
			}
			ref char ptr2 = ref ptr;
			char* ptr3 = ref ptr2;
			<Module>.StringCchCopyExW(buffer, bufferLen, ptr3, pBufferEnd, pRemainingLen, 0);
			flag = true;
		}
		else
		{
			if (pRemainingLen != null)
			{
				*pRemainingLen = (ulong)((long)text.Length);
			}
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000072F0 File Offset: 0x000072F0
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventTargetForwarder.{dtor}(XEventTargetForwarder* A_0)
	{
		try
		{
			try
			{
				try
				{
					if ((A_0 + 32L).ToPointer() != null)
					{
						<Module>.msclr.call_in_appdomain<class\u0020Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder\u0020*>(*(A_0 + 24L), <Module>.__unep@?FinalizeTarget@XEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@$$FCAXPEAV12345@@Z, A_0);
					}
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}), (void*)(A_0 + (byte*)16L));
					throw;
				}
				<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}(A_0 + 16L);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>.{dtor}), (void*)(A_0 + (byte*)8L));
				throw;
			}
			<Module>.gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>.{dtor}(A_0 + 8L);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>.{dtor}), A_0);
			throw;
		}
		<Module>.gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>.{dtor}(A_0);
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000060C0 File Offset: 0x000060C0
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool Microsoft.SqlServer.XEvent.Internal.XEventTargetForwarder.ProcessBufferInternal(XEBuffer* pBuffer, XEventTargetForwarder* pForwarder)
	{
		IntPtr intPtr = new IntPtr(pBuffer);
		return <Module>.gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>..PE$AAVBufferProcessor@Internal@XEvent@SqlServer@Microsoft@@(pForwarder)(*(IntPtr*)(pForwarder + 32L / (long)sizeof(XEventTargetForwarder)), intPtr);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000060F4 File Offset: 0x000060F4
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventTargetForwarder.FinalizeTarget(XEventTargetForwarder* pForwarder)
	{
		long num = (long)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
		uint exceptionCode;
		try
		{
			object[] array = new object[] { *(IntPtr*)(pForwarder + 32L / (long)sizeof(XEventTargetForwarder)) };
			<Module>.gcroot<System::Reflection::MethodInfo\u0020^>.->(pForwarder + 16L / (long)sizeof(XEventTargetForwarder)).Invoke(null, array);
		}
		catch when (delegate
		{
			// Failed to create a 'catch-when' expression
			exceptionCode = (uint)Marshal.GetExceptionCode();
			endfilter(<Module>.__CxxExceptionFilter(Marshal.GetExceptionPointers(), null, 0, null) != null);
		})
		{
			uint num2 = 0U;
			<Module>.__CxxRegisterExceptionObject(Marshal.GetExceptionPointers(), num);
			try
			{
				try
				{
					return;
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					num2 = <Module>.__CxxDetectRethrow(Marshal.GetExceptionPointers());
					endfilter(num2 != 0U);
				})
				{
				}
				if (num2 != 0U)
				{
					throw;
				}
			}
			finally
			{
				<Module>.__CxxUnregisterExceptionObject(num, (int)num2);
			}
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000061D8 File Offset: 0x000061D8
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.InitTarget(XETarget* target, void* hsess, ushort nCAttr, XECustomizableAttribute* cattrs, void** phtarget, XEErrorContext* pError)
	{
		try
		{
			XE_AutoEngineMutex xe_AutoEngineMutex;
			<Module>.XE_AutoEngineMutex.{ctor}(ref xe_AutoEngineMutex, <Module>.?sm_targetLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA, (XEWaitType)0);
			try
			{
				<Module>.XE_AutoEngineMutex.GetAccess(ref xe_AutoEngineMutex, uint.MaxValue);
				XEventManagedPackage* ptr = *(target + 48L);
				int num = (int)(((uint)(*(target + 4L)) >> 10) & 262143U);
				Type type = <Module>.gcroot<cli::array<System::PE$AAVType\u0020>^>..P$01EAPE$AAVType@System@@(ptr + 96L / (long)sizeof(XEventManagedPackage))[num];
				object obj = Activator.CreateInstance(type);
				if (obj == null)
				{
					goto IL_019E;
				}
				List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
				for (int i = 0; i < (int)nCAttr; i++)
				{
					XECustomizableAttribute* ptr2 = (long)i * 56L + cattrs / sizeof(XECustomizableAttribute);
					string text = Marshal.PtrToStringUni((IntPtr)(*(long*)(ptr2 + 8L / (long)sizeof(XECustomizableAttribute))));
					object obj2;
					if (<Module>.XE_Compare(<Module>.XET_WSTR, *(XERelativeObjectId*)ptr2) != null)
					{
						obj2 = Marshal.PtrToStringUni((IntPtr)(*(long*)(ptr2 + 16L / (long)sizeof(XECustomizableAttribute))));
					}
					else
					{
						obj2 = XETypeMappings.UnloadXeVariant(*(long*)(ptr2 + 16L / (long)sizeof(XECustomizableAttribute)), *(int*)ptr2, <Module>.XE_GetTypeSize(*(XERelativeObjectId*)ptr2));
					}
					KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(text, obj2);
					list.Add(keyValuePair);
				}
				XEventTargetForwarder* ptr3 = <Module>.@new(40UL);
				XEventTargetForwarder* ptr4;
				try
				{
					if (ptr3 != null)
					{
						uint num2 = (uint)(*(int*)(ptr + 120L / (long)sizeof(XEventManagedPackage)));
						MethodInfo methodInfo = <Module>.gcroot<System::Reflection::MethodInfo\u0020^>..PE$AAVMethodInfo@Reflection@System@@(ptr + 152L / (long)sizeof(XEventManagedPackage));
						MethodInfo methodInfo2 = <Module>.gcroot<System::Reflection::MethodInfo\u0020^>..PE$AAVMethodInfo@Reflection@System@@(ptr + 128L / (long)sizeof(XEventManagedPackage));
						MethodInfo methodInfo3 = <Module>.gcroot<System::Reflection::MethodInfo\u0020^>..PE$AAVMethodInfo@Reflection@System@@(ptr + 144L / (long)sizeof(XEventManagedPackage));
						MethodInfo methodInfo4 = <Module>.gcroot<System::Reflection::MethodInfo\u0020^>..PE$AAVMethodInfo@Reflection@System@@(ptr + 136L / (long)sizeof(XEventManagedPackage));
						ptr4 = <Module>.Microsoft.SqlServer.XEvent.Internal.XEventTargetForwarder.{ctor}(ptr3, obj, list, methodInfo4, methodInfo3, methodInfo2, methodInfo, hsess, num2);
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
				*(long*)phtarget = ptr4;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoEngineMutex.{dtor}), (void*)(&xe_AutoEngineMutex));
				throw;
			}
			<Module>.XE_AutoEngineMutex.{dtor}(ref xe_AutoEngineMutex);
			return 1;
			IL_019E:
			<Module>.XE_AutoEngineMutex.{dtor}(ref xe_AutoEngineMutex);
		}
		catch (Exception ex)
		{
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.UInt16,System.UInt16,XELogMessage modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), 2, 0, 0L, (IntPtr)(*(<Module>.XE_API.ServiceAPI() + 48L)));
			XE_ErrorContext xe_ErrorContext;
			<Module>.XE_ErrorContext.{ctor}(ref xe_ErrorContext, pError);
			if (ex.Message.Length > 0)
			{
				uint num3;
				if (512 < ex.Message.Length + 1)
				{
					num3 = 512U;
				}
				else
				{
					num3 = (uint)(ex.Message.Length + 1);
				}
				if (<Module>.XE_ErrorContext.AllocateErrorMessage(ref xe_ErrorContext, num3) != null)
				{
					ref byte ptr5 = ex.Message;
					if ((ref ptr5) != null)
					{
						ptr5 = (long)RuntimeHelpers.OffsetToStringData + (ref ptr5);
					}
					ref char ptr6 = ref ptr5;
					<Module>.StringCchCopyW(*(long*)(pError + 16L / (long)sizeof(XEErrorContext)), (ulong)(*(int*)(pError + 24L / (long)sizeof(XEErrorContext))), ref ptr6);
				}
			}
		}
		return 0;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000076D8 File Offset: 0x000076D8
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.FinalizeTarget(void* htarget)
	{
		XE_AutoEngineMutex xe_AutoEngineMutex;
		<Module>.XE_AutoEngineMutex.{ctor}(ref xe_AutoEngineMutex, <Module>.?sm_targetLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA, (XEWaitType)0);
		try
		{
			<Module>.XE_AutoEngineMutex.GetAccess(ref xe_AutoEngineMutex, uint.MaxValue);
			if (htarget != null)
			{
				<Module>.Microsoft.SqlServer.XEvent.Internal.XEventTargetForwarder.{dtor}(htarget);
				<Module>.delete(htarget, 40UL);
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_AutoEngineMutex.{dtor}), (void*)(&xe_AutoEngineMutex));
			throw;
		}
		<Module>.XE_AutoEngineMutex.{dtor}(ref xe_AutoEngineMutex);
		return 1;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000073C0 File Offset: 0x000073C0
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.ProcessBuffer(void* htarget, void* hsess, XEBuffer* pBuffer)
	{
		return (<Module>.msclr.call_in_appdomain<bool,struct\u0020XEBuffer\u0020const\u0020*,class\u0020Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder\u0020*>(*(int*)((byte*)htarget + 24L), <Module>.__unep@?ProcessBufferInternal@XEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@$$FCA_NQEBUXEBuffer@@PEAV12345@@Z, pBuffer, (XEventTargetForwarder*)htarget) != 0) ? 1 : 0;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0000478C File Offset: 0x0000478C
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.ProcessEventASync(void* htarget, void* hsess, XEEventBufferHeader* pSerializedEvent, XEEvent* __unnamed003)
	{
		return 0;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x0000478C File Offset: 0x0000478C
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.ProcessEventSync(void* htarget, void* hsess, XECollectedEvent* pCE, XEEvent* __unnamed003)
	{
		return 0;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x0000647C File Offset: 0x0000647C
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.GetSerializedState(void* htarget, char* buffer, ulong bufferLen, char** pBufferEnd, ulong* pRemainingLen)
	{
		return (<Module>.Microsoft.SqlServer.XEvent.Internal.XEventTargetForwarder.GetSerializedState(htarget, buffer, bufferLen, pBufferEnd, pRemainingLen) != 0) ? 1 : 0;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000478C File Offset: 0x0000478C
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.GetPrivateAPI(void* hTarget, uint apiId, void** pHandle, XEAPI** ppApi)
	{
		return 0;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000047A0 File Offset: 0x000047A0
	internal unsafe static uint Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.GetMaxMemory(void* hTarget)
	{
		return 0;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000047B4 File Offset: 0x000047B4
	internal unsafe static ulong Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.GetTargetBytesWritten(void* hTarget)
	{
		return 0L;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000092C0 File Offset: 0x000092C0
	internal unsafe static int Microsoft.SqlServer.XEvent.Internal.XEventRegisterAutoLoader.InitializeXE(XEventRegisterAutoLoader* A_0)
	{
		int num = 0;
		if (<Module>.XEventAutoEngineLoad.InitializeXE(A_0) != null)
		{
			if (<Module>.Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.StaticInit() != null)
			{
				XEventPackageRegistrar.InstallAssemblyLoadHandler();
				num = 1;
			}
		}
		else if (<Module>.XEventAutoEngineLoad.IsXeDllLoaded(A_0) == null)
		{
			Trace.TraceError("XEEngine could not be loaded.  XE.dll could not be found");
		}
		return num;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static TargetSerializer gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>..PE$AAVTargetSerializer@Internal@XEvent@SqlServer@Microsoft@@(gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00004810 File Offset: 0x00004810
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>* gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>.=(gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>* A_0, TargetSerializer t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x0000483C File Offset: 0x0000483C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>.{dtor}(gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000486C File Offset: 0x0000486C
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>* gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>.{ctor}(gcroot<Microsoft::SqlServer::XEvent::Internal::TargetSerializer\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static BufferProcessor gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>..PE$AAVBufferProcessor@Internal@XEvent@SqlServer@Microsoft@@(gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00004810 File Offset: 0x00004810
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>* gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>.=(gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>* A_0, BufferProcessor t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x0000483C File Offset: 0x0000483C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>.{dtor}(gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x0000486C File Offset: 0x0000486C
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>* gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>.{ctor}(gcroot<Microsoft::SqlServer::XEvent::Internal::BufferProcessor\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static Dictionary<string, uint> gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.->(gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static Dictionary<string, uint> gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>..PE$AAV?$Dictionary@PE$AAVString@System@@I@Generic@Collections@System@@(gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00004810 File Offset: 0x00004810
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>* gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.=(gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>* A_0, Dictionary<string, uint> t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x0000483C File Offset: 0x0000483C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.{dtor}(gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x0000486C File Offset: 0x0000486C
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>* gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>.{ctor}(gcroot<System::Collections::Generic::Dictionary<System::String\u0020^,unsigned\u0020int>\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static MethodInfo gcroot<System::Reflection::MethodInfo\u0020^>.->(gcroot<System::Reflection::MethodInfo\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static MethodInfo gcroot<System::Reflection::MethodInfo\u0020^>..PE$AAVMethodInfo@Reflection@System@@(gcroot<System::Reflection::MethodInfo\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00004810 File Offset: 0x00004810
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::Reflection::MethodInfo\u0020^>* gcroot<System::Reflection::MethodInfo\u0020^>.=(gcroot<System::Reflection::MethodInfo\u0020^>* A_0, MethodInfo t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000483C File Offset: 0x0000483C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void gcroot<System::Reflection::MethodInfo\u0020^>.{dtor}(gcroot<System::Reflection::MethodInfo\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x0000486C File Offset: 0x0000486C
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::Reflection::MethodInfo\u0020^>* gcroot<System::Reflection::MethodInfo\u0020^>.{ctor}(gcroot<System::Reflection::MethodInfo\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static ResourceManager gcroot<System::Resources::ResourceManager\u0020^>.->(gcroot<System::Resources::ResourceManager\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static ResourceManager gcroot<System::Resources::ResourceManager\u0020^>..PE$AAVResourceManager@Resources@System@@(gcroot<System::Resources::ResourceManager\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00004810 File Offset: 0x00004810
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::Resources::ResourceManager\u0020^>* gcroot<System::Resources::ResourceManager\u0020^>.=(gcroot<System::Resources::ResourceManager\u0020^>* A_0, ResourceManager t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x0000483C File Offset: 0x0000483C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void gcroot<System::Resources::ResourceManager\u0020^>.{dtor}(gcroot<System::Resources::ResourceManager\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static string[] gcroot<cli::array<System::PE$AAVString\u0020>^>.->(gcroot<cli::array<System::PE$AAVString\u0020>^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static string[] gcroot<cli::array<System::PE$AAVString\u0020>^>..P$01EAPE$AAVString@System@@(gcroot<cli::array<System::PE$AAVString\u0020>^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00004810 File Offset: 0x00004810
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<cli::array<System::PE$AAVString\u0020>^>* gcroot<cli::array<System::PE$AAVString\u0020>^>.=(gcroot<cli::array<System::PE$AAVString\u0020>^>* A_0, string[] t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0000483C File Offset: 0x0000483C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void gcroot<cli::array<System::PE$AAVString\u0020>^>.{dtor}(gcroot<cli::array<System::PE$AAVString\u0020>^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static Type[] gcroot<cli::array<System::PE$AAVType\u0020>^>.->(gcroot<cli::array<System::PE$AAVType\u0020>^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static Type[] gcroot<cli::array<System::PE$AAVType\u0020>^>..P$01EAPE$AAVType@System@@(gcroot<cli::array<System::PE$AAVType\u0020>^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00004810 File Offset: 0x00004810
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<cli::array<System::PE$AAVType\u0020>^>* gcroot<cli::array<System::PE$AAVType\u0020>^>.=(gcroot<cli::array<System::PE$AAVType\u0020>^>* A_0, Type[] t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000483C File Offset: 0x0000483C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void gcroot<cli::array<System::PE$AAVType\u0020>^>.{dtor}(gcroot<cli::array<System::PE$AAVType\u0020>^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000049C4 File Offset: 0x000049C4
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XEObject>(XEObject* @object)
	{
		if (@object != null)
		{
			ulong num = (ulong)(*(long*)(@object + 8L / (long)sizeof(XEObject)));
			if (num != 0UL && *(int*)(@object + 24L / (long)sizeof(XEObject)) >= 5)
			{
				IntPtr intPtr = new IntPtr(num);
				Marshal.FreeHGlobal(intPtr);
				*(long*)(@object + 8L / (long)sizeof(XEObject)) = 0L;
			}
			ulong num2 = (ulong)(*(long*)(@object + 16L / (long)sizeof(XEObject)));
			if (num2 != 0UL)
			{
				uint num3 = (uint)(*(int*)(@object + 24L / (long)sizeof(XEObject)));
				if (num3 != 0U && num3 >= 5U)
				{
					IntPtr intPtr2 = new IntPtr(num2);
					Marshal.FreeHGlobal(intPtr2);
					*(long*)(@object + 16L / (long)sizeof(XEObject)) = 0L;
				}
			}
		}
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00006500 File Offset: 0x00006500
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool Microsoft.SqlServer.XEvent.Internal.call_in_appdomain_noassert<bool,wchar_t\u0020const\u0020*,struct\u0020XEEvent\u0020const\u0020*>(uint dwAppDomainId, delegate* unmanaged[Cdecl, Cdecl]<char*, XEEvent*, byte> func, char* arg1, XEEvent* arg2)
	{
		ICLRRuntimeHost* ptr = <Module>.msclr._detail.get_clr_runtime_host();
		callback_cdecl_struct2<bool,wchar_t\u0020const\u0020*,XEEvent\u0020const\u0020*> callback_cdecl_struct2<bool,wchar_t_u0020const_u0020*,XEEvent_u0020const_u0020*> = func;
		*((ref callback_cdecl_struct2<bool,wchar_t_u0020const_u0020*,XEEvent_u0020const_u0020*>) + 16) = arg1;
		*((ref callback_cdecl_struct2<bool,wchar_t_u0020const_u0020*,XEEvent_u0020const_u0020*>) + 24) = arg2;
		int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, dwAppDomainId, <Module>.__unep@?callback@?$callback_cdecl_struct2@_NPEB_WPEBUXEEvent@@@_detail@msclr@@$$FSAJPEAX@Z, (void*)(&callback_cdecl_struct2<bool,wchar_t_u0020const_u0020*,XEEvent_u0020const_u0020*>), (IntPtr)(*(*(long*)ptr + 64L)));
		ICLRRuntimeHost* ptr2 = ptr;
		uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
		if (num < 0)
		{
			Marshal.ThrowExceptionForHR(num);
		}
		return *((ref callback_cdecl_struct2<bool,wchar_t_u0020const_u0020*,XEEvent_u0020const_u0020*>) + 8);
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00004A30 File Offset: 0x00004A30
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XECustomizableAttribute>(XECustomizableAttribute* pObj)
	{
		if (pObj != null)
		{
			ulong num = (ulong)(*(long*)(pObj + 8L / (long)sizeof(XECustomizableAttribute)));
			if (num != 0UL && *(int*)(pObj + 48L / (long)sizeof(XECustomizableAttribute)) >= 5)
			{
				IntPtr intPtr = new IntPtr(num);
				Marshal.FreeHGlobal(intPtr);
				*(long*)(pObj + 8L / (long)sizeof(XECustomizableAttribute)) = 0L;
			}
			ulong num2 = (ulong)(*(long*)(pObj + 40L / (long)sizeof(XECustomizableAttribute)));
			if (num2 != 0UL)
			{
				uint num3 = (uint)(*(int*)(pObj + 48L / (long)sizeof(XECustomizableAttribute)));
				if (num3 != 0U && num3 >= 5U)
				{
					IntPtr intPtr2 = new IntPtr(num2);
					Marshal.FreeHGlobal(intPtr2);
					*(long*)(pObj + 40L / (long)sizeof(XECustomizableAttribute)) = 0L;
				}
			}
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00004A9C File Offset: 0x00004A9C
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XEDataAttribute>(XEDataAttribute* pObj)
	{
		if (pObj != null)
		{
			ulong num = (ulong)(*(long*)(pObj + 8L / (long)sizeof(XEDataAttribute)));
			if (num != 0UL && *(int*)(pObj + 32L / (long)sizeof(XEDataAttribute)) >= 5)
			{
				IntPtr intPtr = new IntPtr(num);
				Marshal.FreeHGlobal(intPtr);
				*(long*)(pObj + 8L / (long)sizeof(XEDataAttribute)) = 0L;
			}
			ulong num2 = (ulong)(*(long*)(pObj + 24L / (long)sizeof(XEDataAttribute)));
			if (num2 != 0UL)
			{
				uint num3 = (uint)(*(int*)(pObj + 32L / (long)sizeof(XEDataAttribute)));
				if (num3 != 0U && num3 >= 5U)
				{
					IntPtr intPtr2 = new IntPtr(num2);
					Marshal.FreeHGlobal(intPtr2);
					*(long*)(pObj + 24L / (long)sizeof(XEDataAttribute)) = 0L;
				}
			}
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00004A9C File Offset: 0x00004A9C
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventObjectCleanup.FreeXEObjectFields<struct\u0020XEStaticAttribute>(XEStaticAttribute* pObj)
	{
		if (pObj != null)
		{
			ulong num = (ulong)(*(long*)(pObj + 8L / (long)sizeof(XEStaticAttribute)));
			if (num != 0UL && *(int*)(pObj + 32L / (long)sizeof(XEStaticAttribute)) >= 5)
			{
				IntPtr intPtr = new IntPtr(num);
				Marshal.FreeHGlobal(intPtr);
				*(long*)(pObj + 8L / (long)sizeof(XEStaticAttribute)) = 0L;
			}
			ulong num2 = (ulong)(*(long*)(pObj + 24L / (long)sizeof(XEStaticAttribute)));
			if (num2 != 0UL)
			{
				uint num3 = (uint)(*(int*)(pObj + 32L / (long)sizeof(XEStaticAttribute)));
				if (num3 != 0U && num3 >= 5U)
				{
					IntPtr intPtr2 = new IntPtr(num2);
					Marshal.FreeHGlobal(intPtr2);
					*(long*)(pObj + 24L / (long)sizeof(XEStaticAttribute)) = 0L;
				}
			}
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x000073EC File Offset: 0x000073EC
	internal unsafe static XEEvent* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.BuildXEObject<struct\u0020XEEvent>(XEventPackageSet* A_0, ushort version, byte genericCapabilities, byte specificCapailities, uint objectId, string name, string descriptionKey)
	{
		XEEvent* ptr = <Module>.@new(72UL);
		XEEvent* ptr2;
		if (ptr != null)
		{
			initblk(ptr, 0, 72L);
			ptr2 = ptr;
		}
		else
		{
			ptr2 = null;
		}
		CAutoP<XEEvent> cautoP<XEEvent>;
		<Module>.CAutoP<XEEvent>.{ctor}(ref cautoP<XEEvent>, ptr2);
		XEEvent* ptr3;
		try
		{
			<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.FillXEObjectDetails(A_0, <Module>.CAutoBase<XEEvent>.Get(ref cautoP<XEEvent>), version, genericCapabilities, specificCapailities, objectId, name, descriptionKey, (XEObjectType)0);
			ptr3 = <Module>.CAutoBase<XEEvent>.PvReturn(ref cautoP<XEEvent>);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(CAutoP<XEEvent>.{dtor}), (void*)(&cautoP<XEEvent>));
			throw;
		}
		<Module>.CAutoP<XEEvent>.{dtor}(ref cautoP<XEEvent>);
		return ptr3;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00006560 File Offset: 0x00006560
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.AssignDescription<struct\u0020XEDataAttribute>(XEventPackageSet* A_0, string descriptionKey, XEDataAttribute* tobj)
	{
		if (!string.IsNullOrEmpty(descriptionKey))
		{
			XEventPackageSet* ptr = A_0 + 344L;
			string text;
			if (<Module>.gcroot<System::Resources::ResourceManager\u0020^>..PE$AAVResourceManager@Resources@System@@(ptr) != null)
			{
				text = <Module>.gcroot<System::Resources::ResourceManager\u0020^>.->(ptr).GetString(descriptionKey);
				*(int*)(tobj + 32L / (long)sizeof(XEDataAttribute)) = (int)<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetResourceId(A_0, descriptionKey);
			}
			else
			{
				text = descriptionKey;
			}
			if (string.IsNullOrEmpty(text))
			{
				*(long*)(tobj + 24L / (long)sizeof(XEDataAttribute)) = ref <Module>.??_C@_11LOCGONAA@@;
				*(int*)(tobj + 32L / (long)sizeof(XEDataAttribute)) = 0;
			}
			else
			{
				IntPtr intPtr = Marshal.StringToHGlobalUni(text);
				*(long*)(tobj + 24L / (long)sizeof(XEDataAttribute)) = intPtr.ToPointer();
			}
		}
		else
		{
			*(long*)(tobj + 24L / (long)sizeof(XEDataAttribute)) = ref <Module>.??_C@_11LOCGONAA@@;
			*(int*)(tobj + 32L / (long)sizeof(XEDataAttribute)) = 0;
		}
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00007478 File Offset: 0x00007478
	internal unsafe static XEMap* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.BuildXEObject<struct\u0020XEMap>(XEventPackageSet* A_0, ushort version, byte genericCapabilities, byte specificCapailities, uint objectId, string name, string descriptionKey)
	{
		XEMap* ptr = <Module>.@new(48UL);
		XEMap* ptr2;
		if (ptr != null)
		{
			initblk(ptr, 0, 48L);
			ptr2 = ptr;
		}
		else
		{
			ptr2 = null;
		}
		CAutoP<XEMap> cautoP<XEMap>;
		<Module>.CAutoP<XEMap>.{ctor}(ref cautoP<XEMap>, ptr2);
		XEMap* ptr3;
		try
		{
			<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.FillXEObjectDetails(A_0, <Module>.CAutoBase<XEMap>.Get(ref cautoP<XEMap>), version, genericCapabilities, specificCapailities, objectId, name, descriptionKey, (XEObjectType)3);
			ptr3 = <Module>.CAutoBase<XEMap>.PvReturn(ref cautoP<XEMap>);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(CAutoP<XEMap>.{dtor}), (void*)(&cautoP<XEMap>));
			throw;
		}
		<Module>.CAutoP<XEMap>.{dtor}(ref cautoP<XEMap>);
		return ptr3;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00007504 File Offset: 0x00007504
	internal unsafe static XETarget* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.BuildXEObject<struct\u0020XETarget>(XEventPackageSet* A_0, ushort version, byte genericCapabilities, byte specificCapailities, uint objectId, string name, string descriptionKey)
	{
		XETarget* ptr = <Module>.@new(56UL);
		XETarget* ptr2;
		if (ptr != null)
		{
			initblk(ptr, 0, 56L);
			ptr2 = ptr;
		}
		else
		{
			ptr2 = null;
		}
		CAutoP<XETarget> cautoP<XETarget>;
		<Module>.CAutoP<XETarget>.{ctor}(ref cautoP<XETarget>, ptr2);
		XETarget* ptr3;
		try
		{
			<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.FillXEObjectDetails(A_0, <Module>.CAutoBase<XETarget>.Get(ref cautoP<XETarget>), version, genericCapabilities, specificCapailities, objectId, name, descriptionKey, (XEObjectType)2);
			ptr3 = <Module>.CAutoBase<XETarget>.PvReturn(ref cautoP<XETarget>);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(CAutoP<XETarget>.{dtor}), (void*)(&cautoP<XETarget>));
			throw;
		}
		<Module>.CAutoP<XETarget>.{dtor}(ref cautoP<XETarget>);
		return ptr3;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000065F0 File Offset: 0x000065F0
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.AssignDescription<struct\u0020XECustomizableAttribute>(XEventPackageSet* A_0, string descriptionKey, XECustomizableAttribute* tobj)
	{
		if (!string.IsNullOrEmpty(descriptionKey))
		{
			XEventPackageSet* ptr = A_0 + 344L;
			string text;
			if (<Module>.gcroot<System::Resources::ResourceManager\u0020^>..PE$AAVResourceManager@Resources@System@@(ptr) != null)
			{
				text = <Module>.gcroot<System::Resources::ResourceManager\u0020^>.->(ptr).GetString(descriptionKey);
				*(int*)(tobj + 48L / (long)sizeof(XECustomizableAttribute)) = (int)<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetResourceId(A_0, descriptionKey);
			}
			else
			{
				text = descriptionKey;
			}
			if (string.IsNullOrEmpty(text))
			{
				*(long*)(tobj + 40L / (long)sizeof(XECustomizableAttribute)) = ref <Module>.??_C@_11LOCGONAA@@;
				*(int*)(tobj + 48L / (long)sizeof(XECustomizableAttribute)) = 0;
			}
			else
			{
				IntPtr intPtr = Marshal.StringToHGlobalUni(text);
				*(long*)(tobj + 40L / (long)sizeof(XECustomizableAttribute)) = intPtr.ToPointer();
			}
		}
		else
		{
			*(long*)(tobj + 40L / (long)sizeof(XECustomizableAttribute)) = ref <Module>.??_C@_11LOCGONAA@@;
			*(int*)(tobj + 48L / (long)sizeof(XECustomizableAttribute)) = 0;
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00004B08 File Offset: 0x00004B08
	internal unsafe static XEObjectCollection* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEEvent>(int ObjectCount)
	{
		if (ObjectCount >= 0 && ObjectCount < 524287)
		{
			XEObjectCollection* ptr = <Module>.new[]((ulong)(((long)ObjectCount + 2L) * 8L));
			*(int*)ptr = 0;
			*(short*)(ptr + 4L / (long)sizeof(XEObjectCollection)) = 0;
			*(short*)(ptr + 6L / (long)sizeof(XEObjectCollection)) = 72;
			*(int*)(ptr + 8L / (long)sizeof(XEObjectCollection)) = 0;
			return ptr;
		}
		throw new XEventTooMayObjectsExcpeption();
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00004B5C File Offset: 0x00004B5C
	internal unsafe static XEObjectCollection* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEMap>(int ObjectCount)
	{
		if (ObjectCount >= 0 && ObjectCount < 524287)
		{
			XEObjectCollection* ptr = <Module>.new[]((ulong)(((long)ObjectCount + 2L) * 8L));
			*(int*)ptr = 3;
			*(short*)(ptr + 4L / (long)sizeof(XEObjectCollection)) = 0;
			*(short*)(ptr + 6L / (long)sizeof(XEObjectCollection)) = 48;
			*(int*)(ptr + 8L / (long)sizeof(XEObjectCollection)) = 0;
			return ptr;
		}
		throw new XEventTooMayObjectsExcpeption();
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00004BB0 File Offset: 0x00004BB0
	internal unsafe static XEObjectCollection* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XETarget>(int ObjectCount)
	{
		if (ObjectCount >= 0 && ObjectCount < 524287)
		{
			XEObjectCollection* ptr = <Module>.new[]((ulong)(((long)ObjectCount + 2L) * 8L));
			*(int*)ptr = 2;
			*(short*)(ptr + 4L / (long)sizeof(XEObjectCollection)) = 0;
			*(short*)(ptr + 6L / (long)sizeof(XEObjectCollection)) = 56;
			*(int*)(ptr + 8L / (long)sizeof(XEObjectCollection)) = 0;
			return ptr;
		}
		throw new XEventTooMayObjectsExcpeption();
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00004C04 File Offset: 0x00004C04
	internal unsafe static XEObjectCollection* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEAction>(int ObjectCount)
	{
		if (ObjectCount >= 0 && ObjectCount < 524287)
		{
			XEObjectCollection* ptr = <Module>.new[]((ulong)(((long)ObjectCount + 2L) * 8L));
			*(int*)ptr = 1;
			*(short*)(ptr + 4L / (long)sizeof(XEObjectCollection)) = 0;
			*(short*)(ptr + 6L / (long)sizeof(XEObjectCollection)) = 72;
			*(int*)(ptr + 8L / (long)sizeof(XEObjectCollection)) = 0;
			return ptr;
		}
		throw new XEventTooMayObjectsExcpeption();
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00004C58 File Offset: 0x00004C58
	internal unsafe static XEObjectCollection* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEPredicateSource>(int ObjectCount)
	{
		if (ObjectCount >= 0 && ObjectCount < 524287)
		{
			XEObjectCollection* ptr = <Module>.new[]((ulong)(((long)ObjectCount + 2L) * 8L));
			*(int*)ptr = 4;
			*(short*)(ptr + 4L / (long)sizeof(XEObjectCollection)) = 0;
			*(short*)(ptr + 6L / (long)sizeof(XEObjectCollection)) = 80;
			*(int*)(ptr + 8L / (long)sizeof(XEObjectCollection)) = 0;
			return ptr;
		}
		throw new XEventTooMayObjectsExcpeption();
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00004CAC File Offset: 0x00004CAC
	internal unsafe static XEObjectCollection* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEPredicateCompare>(int ObjectCount)
	{
		if (ObjectCount >= 0 && ObjectCount < 524287)
		{
			XEObjectCollection* ptr = <Module>.new[]((ulong)(((long)ObjectCount + 2L) * 8L));
			*(int*)ptr = 5;
			*(short*)(ptr + 4L / (long)sizeof(XEObjectCollection)) = 0;
			*(short*)(ptr + 6L / (long)sizeof(XEObjectCollection)) = 96;
			*(int*)(ptr + 8L / (long)sizeof(XEObjectCollection)) = 0;
			return ptr;
		}
		throw new XEventTooMayObjectsExcpeption();
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00004D00 File Offset: 0x00004D00
	internal unsafe static XEObjectCollection* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEType>(int ObjectCount)
	{
		if (ObjectCount >= 0 && ObjectCount < 524287)
		{
			XEObjectCollection* ptr = <Module>.new[]((ulong)(((long)ObjectCount + 2L) * 8L));
			*(int*)ptr = 6;
			*(short*)(ptr + 4L / (long)sizeof(XEObjectCollection)) = 0;
			*(short*)(ptr + 6L / (long)sizeof(XEObjectCollection)) = 40;
			*(int*)(ptr + 8L / (long)sizeof(XEObjectCollection)) = 0;
			return ptr;
		}
		throw new XEventTooMayObjectsExcpeption();
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00004D54 File Offset: 0x00004D54
	internal unsafe static XEObjectCollection* Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.CreateObjectCollection<struct\u0020XEMessage>(int ObjectCount)
	{
		if (ObjectCount >= 0 && ObjectCount < 524287)
		{
			XEObjectCollection* ptr = <Module>.new[]((ulong)(((long)ObjectCount + 2L) * 8L));
			*(int*)ptr = 7;
			*(short*)(ptr + 4L / (long)sizeof(XEObjectCollection)) = 0;
			*(short*)(ptr + 6L / (long)sizeof(XEObjectCollection)) = 32;
			*(int*)(ptr + 8L / (long)sizeof(XEObjectCollection)) = 0;
			return ptr;
		}
		throw new XEventTooMayObjectsExcpeption();
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00006680 File Offset: 0x00006680
	internal unsafe static void Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.AssignDescription<struct\u0020XEObject>(XEventPackageSet* A_0, string descriptionKey, XEObject* tobj)
	{
		if (!string.IsNullOrEmpty(descriptionKey))
		{
			XEventPackageSet* ptr = A_0 + 344L;
			string text;
			if (<Module>.gcroot<System::Resources::ResourceManager\u0020^>..PE$AAVResourceManager@Resources@System@@(ptr) != null)
			{
				text = <Module>.gcroot<System::Resources::ResourceManager\u0020^>.->(ptr).GetString(descriptionKey);
				*(int*)(tobj + 24L / (long)sizeof(XEObject)) = (int)<Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.GetResourceId(A_0, descriptionKey);
			}
			else
			{
				text = descriptionKey;
			}
			if (string.IsNullOrEmpty(text))
			{
				*(long*)(tobj + 16L / (long)sizeof(XEObject)) = ref <Module>.??_C@_11LOCGONAA@@;
				*(int*)(tobj + 24L / (long)sizeof(XEObject)) = 0;
			}
			else
			{
				IntPtr intPtr = Marshal.StringToHGlobalUni(text);
				*(long*)(tobj + 16L / (long)sizeof(XEObject)) = intPtr.ToPointer();
			}
		}
		else
		{
			*(long*)(tobj + 16L / (long)sizeof(XEObject)) = ref <Module>.??_C@_11LOCGONAA@@;
			*(int*)(tobj + 24L / (long)sizeof(XEObject)) = 0;
		}
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00006710 File Offset: 0x00006710
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool msclr.call_in_appdomain<bool,struct\u0020XEBuffer\u0020const\u0020*,class\u0020Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder\u0020*>(uint dwAppDomainId, delegate* unmanaged[Cdecl, Cdecl]<XEBuffer*, XEventTargetForwarder*, byte> func, XEBuffer* arg1, XEventTargetForwarder* arg2)
	{
		ICLRRuntimeHost* ptr = <Module>.msclr._detail.get_clr_runtime_host();
		callback_cdecl_struct2<bool,XEBuffer\u0020const\u0020*,Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder\u0020*> callback_cdecl_struct2<bool,XEBuffer_u0020const_u0020*,Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder_u0020*> = func;
		*((ref callback_cdecl_struct2<bool,XEBuffer_u0020const_u0020*,Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder_u0020*>) + 16) = arg1;
		*((ref callback_cdecl_struct2<bool,XEBuffer_u0020const_u0020*,Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder_u0020*>) + 24) = arg2;
		int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, dwAppDomainId, <Module>.__unep@?callback@?$callback_cdecl_struct2@_NPEBUXEBuffer@@PEAVXEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@@_detail@msclr@@$$FSAJPEAX@Z, (void*)(&callback_cdecl_struct2<bool,XEBuffer_u0020const_u0020*,Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder_u0020*>), (IntPtr)(*(*(long*)ptr + 64L)));
		ICLRRuntimeHost* ptr2 = ptr;
		uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
		if (num < 0)
		{
			Marshal.ThrowExceptionForHR(num);
		}
		return *((ref callback_cdecl_struct2<bool,XEBuffer_u0020const_u0020*,Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder_u0020*>) + 8);
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00006770 File Offset: 0x00006770
	internal unsafe static void msclr.call_in_appdomain<class\u0020Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder\u0020*>(uint dwAppDomainId, delegate* unmanaged[Cdecl, Cdecl]<XEventTargetForwarder*, void> func, XEventTargetForwarder* arg1)
	{
		ICLRRuntimeHost* ptr = <Module>.msclr._detail.get_clr_runtime_host();
		callback_cdecl_void_struct1<Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder\u0020*> callback_cdecl_void_struct1<Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder_u0020*> = func;
		*((ref callback_cdecl_void_struct1<Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder_u0020*>) + 8) = arg1;
		int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, dwAppDomainId, <Module>.__unep@?callback@?$callback_cdecl_void_struct1@PEAVXEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@@_detail@msclr@@$$FSAJPEAX@Z, (void*)(&callback_cdecl_void_struct1<Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder_u0020*>), (IntPtr)(*(*(long*)ptr + 64L)));
		ICLRRuntimeHost* ptr2 = ptr;
		uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
		if (num < 0)
		{
			Marshal.ThrowExceptionForHR(num);
		}
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00004DA8 File Offset: 0x00004DA8
	internal unsafe static int msclr._detail.callback_cdecl_void_struct1<Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder\u0020*>.callback(void* cookie)
	{
		if (cookie == null)
		{
			return -2147467259;
		}
		calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(Microsoft.SqlServer.XEvent.Internal.XEventTargetForwarder*), *(long*)((byte*)cookie + 8L), (IntPtr)(*(long*)cookie));
		return 0;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00004DD4 File Offset: 0x00004DD4
	internal unsafe static int msclr._detail.callback_cdecl_struct2<bool,XEBuffer\u0020const\u0020*,Microsoft::SqlServer::XEvent::Internal::XEventTargetForwarder\u0020*>.callback(void* cookie)
	{
		if (cookie == null)
		{
			return -2147467259;
		}
		((byte*)cookie)[8L] = calli(System.Byte modopt(System.Runtime.CompilerServices.CompilerMarshalOverride) modopt(System.Runtime.CompilerServices.CallConvCdecl)(XEBuffer modopt(System.Runtime.CompilerServices.IsConst)*,Microsoft.SqlServer.XEvent.Internal.XEventTargetForwarder*), *(long*)((byte*)cookie + 16L), *(long*)((byte*)cookie + 24L), (IntPtr)(*(long*)cookie));
		return 0;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00009DA8 File Offset: 0x00009DA8
	internal unsafe static int msclr._detail.callback_cdecl_struct5<int,Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage\u0020*,unsigned\u0020long,unsigned\u0020long,wchar_t\u0020*,unsigned\u0020long>.callback(void* cookie)
	{
		if (cookie == null)
		{
			return -2147467259;
		}
		*(int*)((byte*)cookie + 8L) = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage*,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Char*,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), *(long*)((byte*)cookie + 16L), *(int*)((byte*)cookie + 24L), *(int*)((byte*)cookie + 28L), *(long*)((byte*)cookie + 32L), *(int*)((byte*)cookie + 40L), (IntPtr)(*(long*)cookie));
		return 0;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00004E0C File Offset: 0x00004E0C
	internal unsafe static int msclr._detail.callback_cdecl_struct2<bool,wchar_t\u0020const\u0020*,XEEvent\u0020const\u0020*>.callback(void* cookie)
	{
		if (cookie == null)
		{
			return -2147467259;
		}
		((byte*)cookie)[8L] = calli(System.Byte modopt(System.Runtime.CompilerServices.CompilerMarshalOverride) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Char modopt(System.Runtime.CompilerServices.IsConst)*,XEEvent modopt(System.Runtime.CompilerServices.IsConst)*), *(long*)((byte*)cookie + 16L), *(long*)((byte*)cookie + 24L), (IntPtr)(*(long*)cookie));
		return 0;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x0000B330 File Offset: 0x0000B330
	internal unsafe static XEEngineClientAPI* XE_API.ClientAPI()
	{
		return ref <Module>.?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x0000B530 File Offset: 0x0000B530
	internal unsafe static int XBitmap<ExternalStorage>.IsAllocated(XBitmap<ExternalStorage>* A_0, uint bitNumber)
	{
		ulong num = (ulong)(bitNumber >> 5);
		uint num2 = 1U << (int)bitNumber;
		return ((*(num * 4UL + (ulong)(*A_0)) & (int)num2) != 0) ? 1 : 0;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x0000B5A8 File Offset: 0x0000B5A8
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDtor(delegate*<void*, void> pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000B690 File Offset: 0x0000B690
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[HandleProcessCorruptedStateExceptions]
	[SecurityCritical]
	internal unsafe static void __ehvec_dtor(void* ptr, ulong size, ulong count, delegate*<void*, void> destructor)
	{
		bool flag = false;
		ptr = (void*)(size * count + (byte*)ptr);
		try
		{
			for (;;)
			{
				long num = (long)count;
				count -= 1UL;
				if (num == 0L)
				{
					break;
				}
				ptr = (void*)((byte*)ptr - size);
				calli(System.Void(System.Void*), ptr, destructor);
			}
			flag = true;
		}
		finally
		{
			if (!flag)
			{
				<Module>.__ArrayUnwind(ptr, size, count, destructor);
			}
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000B5F4 File Offset: 0x0000B5F4
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static int ?A0x0b550e27.ArrayUnwindFilter(_EXCEPTION_POINTERS* pExPtrs)
	{
		EHExceptionRecord* ptr = *(long*)pExPtrs;
		if (*(int*)ptr != -529697949)
		{
			return 0;
		}
		*<Module>.__current_exception() = ptr;
		long num = *(long*)(pExPtrs + 8L / (long)sizeof(_EXCEPTION_POINTERS));
		*<Module>.__current_exception_context() = num;
		<Module>.terminate();
		return 0;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x0000B630 File Offset: 0x0000B630
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void __ArrayUnwind(void* ptr, ulong size, ulong count, delegate*<void*, void> destructor)
	{
		try
		{
			for (ulong num = 0UL; num != count; num += 1UL)
			{
				ptr = (void*)((byte*)ptr - size);
				calli(System.Void(System.Void*), ptr, destructor);
			}
		}
		catch when (endfilter(<Module>.?A0x0b550e27.ArrayUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x0000BE80 File Offset: 0x0000BE80
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(Exception innerException, Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x0000B8B0 File Offset: 0x0000B8B0
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x0000B8CC File Offset: 0x0000B8CC
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x0000B9FC File Offset: 0x0000B9FC
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x0600007F RID: 127 RVA: 0x000039E4 File Offset: 0x000039E4
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid guid2 = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return guid2;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x0000BA1C File Offset: 0x0000BA1C
	[SecurityCritical]
	internal unsafe static int __get_default_appdomain(IUnknown** ppUnk)
	{
		ICorRuntimeHost* ptr = null;
		int num;
		try
		{
			Guid guid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e);
			ptr = (ICorRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e), guid).ToPointer();
			goto IL_0036;
		}
		catch (Exception ex)
		{
			num = Marshal.GetHRForException(ex);
		}
		if (num < 0)
		{
			return num;
		}
		IL_0036:
		long num2 = *(*(long*)ptr + 104L);
		num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr, ppUnk, (IntPtr)num2);
		ICorRuntimeHost* ptr2 = ptr;
		uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
		return num;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x0000BAA4 File Offset: 0x0000BAA4
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, (IntPtr)(*(*(long*)ppUnk + 16L)));
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0000BAC8 File Offset: 0x0000BAC8
	[SecurityCritical]
	internal unsafe static AppDomain <CrtImplementationDetails>.GetDefaultDomain()
	{
		IUnknown* ptr = null;
		int num = <Module>.__get_default_appdomain(&ptr);
		if (num >= 0)
		{
			try
			{
				IntPtr intPtr = new IntPtr((void*)ptr);
				return (AppDomain)Marshal.GetObjectForIUnknown(intPtr);
			}
			finally
			{
				<Module>.__release_appdomain(ptr);
			}
		}
		Marshal.ThrowExceptionForHR(num);
		return null;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0000BB30 File Offset: 0x0000BB30
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.DoCallBackInDefaultDomain(delegate* unmanaged[Cdecl, Cdecl]<void*, int> function, void* cookie)
	{
		Guid guid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02);
		ICLRRuntimeHost* ptr = (ICLRRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02), guid).ToPointer();
		try
		{
			AppDomain appDomain = <Module>.<CrtImplementationDetails>.GetDefaultDomain();
			long num = *(*(long*)ptr + 64L);
			uint id = (uint)appDomain.Id;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, id, function, cookie, (IntPtr)num);
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
		}
		finally
		{
			ICLRRuntimeHost* ptr2 = ptr;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x0000BBC4 File Offset: 0x0000BBC4
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __scrt_is_safe_for_managed_code()
	{
		uint _scrt_native_dllmain_reason = <Module>.__scrt_native_dllmain_reason;
		if (_scrt_native_dllmain_reason != 0U && _scrt_native_dllmain_reason != 1U)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x0000BC04 File Offset: 0x0000BC04
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000BC28 File Offset: 0x0000BC28
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasPerProcess()
	{
		if (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xc_mp_a);
			if ((ref <Module>.__xc_mp_a) < (ref <Module>.__xc_mp_z))
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xc_mp_z))
					{
						goto IL_0035;
					}
				}
				<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_0035:
			<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000BC84 File Offset: 0x0000BC84
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasNative()
	{
		if (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xi_a);
			if ((ref <Module>.__xi_a) < (ref <Module>.__xi_z))
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xi_z))
					{
						goto IL_0035;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_0035:
			void** ptr2 = (void**)(&<Module>.__xc_a);
			if ((ref <Module>.__xc_a) < (ref <Module>.__xc_z))
			{
				while (*(long*)ptr2 == 0L)
				{
					ptr2 += 8L / (long)sizeof(void*);
					if (ptr2 >= (void**)(&<Module>.__xc_z))
					{
						goto IL_0062;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_0062:
			<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0000BD0C File Offset: 0x0000BD0C
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsInitialization()
	{
		int num;
		if ((<Module>.<CrtImplementationDetails>.DefaultDomain.HasPerProcess() != null && !<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA) || (<Module>.<CrtImplementationDetails>.DefaultDomain.HasNative() != null && !<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA && <Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0))
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		return (byte)num;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000BD4C File Offset: 0x0000BD4C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x0600008A RID: 138 RVA: 0x0000BD64 File Offset: 0x0000BD64
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x0600008B RID: 139 RVA: 0x000010AC File Offset: 0x000010AC
	internal static void ?A0xba6ca20d.??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x000010C8 File Offset: 0x000010C8
	internal static void ?A0xba6ca20d.??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000010E4 File Offset: 0x000010E4
	internal static void ?A0xba6ca20d.??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00001100 File Offset: 0x00001100
	internal static void ?A0xba6ca20d.??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x0000111C File Offset: 0x0000111C
	internal static void ?A0xba6ca20d.??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00001138 File Offset: 0x00001138
	internal static void ?A0xba6ca20d.??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00001154 File Offset: 0x00001154
	internal static void ?A0xba6ca20d.??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x0000BEDC File Offset: 0x0000BEDC
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xi_vt_a), (delegate*<void*>*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0000BF18 File Offset: 0x0000BF18
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0000BF3C File Offset: 0x0000BF3C
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeNative(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during native initialization.\n");
		<Module>.__security_init_cookie();
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		if (<Module>.__scrt_is_safe_for_managed_code() == null)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)1)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0)
		{
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)1;
			if (<Module>._initterm_e((delegate* unmanaged[Cdecl, Cdecl]<int>*)(&<Module>.__xi_a), (delegate* unmanaged[Cdecl, Cdecl]<int>*)(&<Module>.__xi_z)) != 0)
			{
				<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0));
			}
			<Module>._initterm((delegate* unmanaged[Cdecl, Cdecl]<void>*)(&<Module>.__xc_a), (delegate* unmanaged[Cdecl, Cdecl]<void>*)(&<Module>.__xc_z));
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)2;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		}
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0000BFD0 File Offset: 0x0000BFD0
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerProcess(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during process initialization.\n");
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_m();
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xc_mp_a), (delegate*<void*>*)(&<Module>.__xc_mp_z));
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0000C018 File Offset: 0x0000C018
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during appdomain initialization.\n");
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xc_ma_a), (delegate*<void*>*)(&<Module>.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x0000C058 File Offset: 0x0000C058
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x06000098 RID: 152 RVA: 0x0000C088 File Offset: 0x0000C088
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport._Initialize(LanguageSupport* A_0)
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		<Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA = <Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA || <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
		void* ptr = <Module>._getFiberPtrId();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			while (num2 == 0)
			{
				try
				{
				}
				finally
				{
					void* ptr2 = Interlocked.CompareExchange(ref <Module>.__scrt_native_startup_lock, ptr, 0L);
					if (ptr2 == null)
					{
						num2 = 1;
					}
					else if (ptr2 == ptr)
					{
						num = 1;
						num2 = 1;
					}
				}
				if (num2 == 0)
				{
					<Module>.Sleep(1000);
				}
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeVtables(A_0);
			if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeNative(A_0);
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerProcess(A_0);
			}
			else
			{
				num3 = ((<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsInitialization() != 0) ? 1 : num3);
			}
		}
		finally
		{
			if (num == 0)
			{
				Interlocked.Exchange(ref <Module>.__scrt_native_startup_lock, 0L);
			}
		}
		if (num3 != 0)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(A_0);
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(A_0);
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 1;
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(A_0);
	}

	// Token: 0x06000099 RID: 153 RVA: 0x0000BD84 File Offset: 0x0000BD84
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x0600009A RID: 154 RVA: 0x0000BD9C File Offset: 0x0000BD9C
	[SecurityCritical]
	internal unsafe static int <CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(void* cookie)
	{
		<Module>._exit_callback();
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		if (<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA)
		{
			<Module>._cexit();
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)0;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		}
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		return 0;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0000BDDC File Offset: 0x0000BDDC
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain()
	{
		if (<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsUninitialization() != null)
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(null);
			}
			else
			{
				<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
			}
		}
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000BE18 File Offset: 0x0000BE18
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[PrePrepareMethod]
	internal static void <CrtImplementationDetails>.LanguageSupport.DomainUnload(object A_0, EventArgs A_1)
	{
		if (<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA != 0 && Interlocked.Exchange(ref <Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA, 1) == 0)
		{
			byte b = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0);
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (b != 0)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000C194 File Offset: 0x0000C194
	[SecurityCritical]
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Cleanup(LanguageSupport* A_0, Exception innerException)
	{
		try
		{
			bool flag = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0) != 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
		catch (Exception ex)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, ex);
		}
		catch (object obj)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, null);
		}
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000C210 File Offset: 0x0000C210
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x0000C230 File Offset: 0x0000C230
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x0000C24C File Offset: 0x0000C24C
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Initialize(LanguageSupport* A_0)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load.\n");
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				Interlocked.Increment(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA);
				flag = true;
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport._Initialize(A_0);
		}
		catch (Exception ex)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, ex);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), ex);
		}
		catch (object obj)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, null);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), null);
		}
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000C310 File Offset: 0x0000C310
	[DebuggerStepThrough]
	[SecurityCritical]
	static unsafe <Module>()
	{
		LanguageSupport languageSupport;
		<Module>.<CrtImplementationDetails>.LanguageSupport.{ctor}(ref languageSupport);
		try
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Initialize(ref languageSupport);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(<CrtImplementationDetails>.LanguageSupport.{dtor}), (void*)(&languageSupport));
			throw;
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.{dtor}(ref languageSupport);
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x000047E4 File Offset: 0x000047E4
	[SecuritySafeCritical]
	internal unsafe static string gcroot<System::String\u0020^>..PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00004810 File Offset: 0x00004810
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.=(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x0000483C File Offset: 0x0000483C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void gcroot<System::String\u0020^>.{dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x0000486C File Offset: 0x0000486C
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.{ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000C390 File Offset: 0x0000C390
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void _Init_thread_header_m(int* pOnce)
	{
		if (*(int*)pOnce >= -1)
		{
			<Module>._Init_thread_lock();
			if (*(int*)pOnce == 0)
			{
				*(int*)pOnce = -1;
			}
			else if (*(int*)pOnce == -1)
			{
				do
				{
					<Module>._Init_thread_wait(100);
					if (*(int*)pOnce == 0)
					{
						goto IL_0036;
					}
				}
				while (*(int*)pOnce == -1);
				goto IL_0042;
				IL_0036:
				*(int*)pOnce = -1;
				<Module>._Init_thread_unlock();
				return;
			}
			IL_0042:
			<Module>._Init_thread_unlock();
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x0000C3EC File Offset: 0x0000C3EC
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void _Init_thread_abort_m(int* pOnce)
	{
		<Module>._Init_thread_lock();
		*(int*)pOnce = 0;
		<Module>._Init_thread_notify();
		<Module>._Init_thread_unlock();
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000C414 File Offset: 0x0000C414
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void _Init_thread_footer_m(int* pOnce)
	{
		<Module>._Init_thread_lock();
		<Module>._Init_global_epoch++;
		*(int*)pOnce = <Module>._Init_global_epoch;
		<Module>._Init_thread_notify();
		<Module>._Init_thread_unlock();
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000C44C File Offset: 0x0000C44C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static ValueType <CrtImplementationDetails>.AtExitLock._handle()
	{
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr intPtr = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			return GCHandle.FromIntPtr(intPtr);
		}
		return null;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x0000C974 File Offset: 0x0000C974
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x060000AB RID: 171 RVA: 0x0000C480 File Offset: 0x0000C480
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Set(object value)
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType == null)
		{
			valueType = GCHandle.Alloc(value);
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = GCHandle.ToIntPtr((GCHandle)valueType).ToPointer();
		}
		else
		{
			((GCHandle)valueType).Target = value;
		}
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000C4D4 File Offset: 0x0000C4D4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static object <CrtImplementationDetails>.AtExitLock._lock_Get()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			return ((GCHandle)valueType).Target;
		}
		return null;
	}

	// Token: 0x060000AD RID: 173 RVA: 0x0000C500 File Offset: 0x0000C500
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			((GCHandle)valueType).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		}
	}

	// Token: 0x060000AE RID: 174 RVA: 0x0000C530 File Offset: 0x0000C530
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x0000C994 File Offset: 0x0000C994
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() == null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Construct(new object());
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000C550 File Offset: 0x0000C550
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x0000C57C File Offset: 0x0000C57C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.Enter()
	{
		Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x0000C59C File Offset: 0x0000C59C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.Exit()
	{
		Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x0000C5BC File Offset: 0x0000C5BC
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool ?A0x8e112e2a.__global_lock()
	{
		bool flag = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Enter();
			flag = true;
		}
		return flag;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x0000C5E0 File Offset: 0x0000C5E0
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool ?A0x8e112e2a.__global_unlock()
	{
		bool flag = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Exit();
			flag = true;
		}
		return flag;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x0000C9CC File Offset: 0x0000C9CC
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool ?A0x8e112e2a.__alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x0000C604 File Offset: 0x0000C604
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void ?A0x8e112e2a.__dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x0000C61C File Offset: 0x0000C61C
	[SecurityCritical]
	internal unsafe static int _atexit_helper(delegate*<void> func, ulong* __pexit_list_size, delegate*<void>** __ponexitend_e, delegate*<void>** __ponexitbegin_e)
	{
		delegate*<void> system.Void_u0020() = 0L;
		if (func == null)
		{
			return -1;
		}
		if (<Module>.?A0x8e112e2a.__global_lock() == 1)
		{
			try
			{
				delegate*<void>* ptr = (delegate*<void>*)<Module>.DecodePointer(*(long*)__ponexitbegin_e);
				delegate*<void>* ptr2 = (delegate*<void>*)<Module>.DecodePointer(*(long*)__ponexitend_e);
				long num = (long)(ptr2 - ptr);
				if (*__pexit_list_size - 1UL < (ulong)num >> 3)
				{
					try
					{
						ulong num2 = *__pexit_list_size * 8UL;
						ulong num3 = ((num2 < 4096UL) ? num2 : 4096UL);
						IntPtr intPtr = new IntPtr((int)(num2 + num3));
						IntPtr intPtr2 = new IntPtr((void*)ptr);
						IntPtr intPtr3 = Marshal.ReAllocHGlobal(intPtr2, intPtr);
						ptr2 = (delegate*<void>*)((byte*)intPtr3.ToPointer() + num);
						ptr = (delegate*<void>*)intPtr3.ToPointer();
						ulong num4 = *__pexit_list_size;
						ulong num5 = ((512UL < num4) ? 512UL : num4);
						*__pexit_list_size = num4 + num5;
					}
					catch (OutOfMemoryException)
					{
						IntPtr intPtr4 = new IntPtr((int)(*__pexit_list_size * 8UL + 12UL));
						IntPtr intPtr5 = new IntPtr((void*)ptr);
						IntPtr intPtr6 = Marshal.ReAllocHGlobal(intPtr5, intPtr4);
						ptr2 = (intPtr6.ToPointer() - ptr) / (IntPtr)sizeof(delegate*<void>) + ptr2;
						ptr = (delegate*<void>*)intPtr6.ToPointer();
						*__pexit_list_size += 4UL;
					}
				}
				*(long*)ptr2 = func;
				ptr2 += 8L / (long)sizeof(delegate*<void>);
				system.Void_u0020() = func;
				*(long*)__ponexitbegin_e = <Module>.EncodePointer((void*)ptr);
				*(long*)__ponexitend_e = <Module>.EncodePointer((void*)ptr2);
			}
			catch (OutOfMemoryException)
			{
			}
			finally
			{
				<Module>.?A0x8e112e2a.__global_unlock();
			}
			if (system.Void_u0020() != null)
			{
				return 0;
			}
		}
		return -1;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x0000C79C File Offset: 0x0000C79C
	[SecurityCritical]
	internal unsafe static void _exit_callback()
	{
		if (<Module>.?A0x8e112e2a.__exit_list_size != 0UL)
		{
			delegate*<void>* ptr = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x8e112e2a.__onexitbegin_m);
			delegate*<void>* ptr2 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x8e112e2a.__onexitend_m);
			if (ptr != -1L && ptr != null && ptr2 != null)
			{
				delegate*<void>* ptr3 = ptr;
				delegate*<void>* ptr4 = ptr2;
				for (;;)
				{
					ptr2 -= 8L / (long)sizeof(delegate*<void>);
					if (ptr2 < ptr)
					{
						break;
					}
					if (*(long*)ptr2 != <Module>.EncodePointer(null))
					{
						IntPtr intPtr = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), intPtr);
						delegate*<void>* ptr5 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x8e112e2a.__onexitbegin_m);
						delegate*<void>* ptr6 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x8e112e2a.__onexitend_m);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr5;
							ptr4 = ptr6;
							ptr2 = ptr6;
						}
					}
				}
				IntPtr intPtr2 = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(intPtr2);
			}
			<Module>.?A0x8e112e2a.__dealloc_global_lock();
		}
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0000C9EC File Offset: 0x0000C9EC
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initatexit_m()
	{
		int num = 0;
		if (<Module>.?A0x8e112e2a.__alloc_global_lock() == 1)
		{
			<Module>.?A0x8e112e2a.__onexitbegin_m = (delegate*<void>*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.?A0x8e112e2a.__onexitend_m = <Module>.?A0x8e112e2a.__onexitbegin_m;
			<Module>.?A0x8e112e2a.__exit_list_size = 32UL;
			num = 1;
		}
		return num;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x0000C850 File Offset: 0x0000C850
	[SecurityCritical]
	internal unsafe static int _atexit_m(delegate*<void> func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.?A0x8e112e2a.__exit_list_size, &<Module>.?A0x8e112e2a.__onexitend_m, &<Module>.?A0x8e112e2a.__onexitbegin_m);
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000CA38 File Offset: 0x0000CA38
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initatexit_app_domain()
	{
		if (<Module>.?A0x8e112e2a.__alloc_global_lock() == 1)
		{
			<Module>.__onexitbegin_app_domain = (delegate*<void>*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.__onexitend_app_domain = <Module>.__onexitbegin_app_domain;
			<Module>.__exit_list_size_app_domain = 32UL;
		}
		return 1;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x0000C880 File Offset: 0x0000C880
	[HandleProcessCorruptedStateExceptions]
	[SecurityCritical]
	internal unsafe static void _app_exit_callback()
	{
		if (<Module>.__exit_list_size_app_domain != 0UL)
		{
			delegate*<void>* ptr = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
			delegate*<void>* ptr2 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
			try
			{
				if (ptr != -1L && ptr != null && ptr2 != null)
				{
					delegate*<void>* ptr3 = ptr;
					delegate*<void>* ptr4 = ptr2;
					for (;;)
					{
						do
						{
							ptr2 -= 8L / (long)sizeof(delegate*<void>);
						}
						while (ptr2 >= ptr && *(long*)ptr2 == <Module>.EncodePointer(null));
						if (ptr2 < ptr)
						{
							break;
						}
						delegate*<void> system.Void_u0020() = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), system.Void_u0020());
						delegate*<void>* ptr5 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
						delegate*<void>* ptr6 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr5;
							ptr4 = ptr6;
							ptr2 = ptr6;
						}
					}
				}
			}
			finally
			{
				IntPtr intPtr = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(intPtr);
				<Module>.?A0x8e112e2a.__dealloc_global_lock();
			}
		}
	}

	// Token: 0x060000BD RID: 189
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x060000BE RID: 190
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x060000BF RID: 191 RVA: 0x0000CA84 File Offset: 0x0000CA84
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initterm_e(delegate* unmanaged[Cdecl, Cdecl]<int>* pfbegin, delegate* unmanaged[Cdecl, Cdecl]<int>* pfend)
	{
		int num = 0;
		if (pfbegin < pfend)
		{
			while (num == 0)
			{
				ulong num2 = (ulong)(*(long*)pfbegin);
				if (num2 != 0UL)
				{
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)num2);
				}
				pfbegin += 8L / (long)sizeof(delegate* unmanaged[Cdecl, Cdecl]<int>);
				if (pfbegin >= pfend)
				{
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x0000CAB8 File Offset: 0x0000CAB8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void _initterm(delegate* unmanaged[Cdecl, Cdecl]<void>* pfbegin, delegate* unmanaged[Cdecl, Cdecl]<void>* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)num);
				}
				pfbegin += 8L / (long)sizeof(delegate* unmanaged[Cdecl, Cdecl]<void>);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000CAE8 File Offset: 0x0000CAE8
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000CB44 File Offset: 0x0000CB44
	[DebuggerStepThrough]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void _initterm_m(delegate*<void*>* pfbegin, delegate*<void*>* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					void* ptr = calli(System.Void modopt(System.Runtime.CompilerServices.IsConst)*(), <Module>.<CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(num));
				}
				pfbegin += 8L / (long)sizeof(delegate*<void*>);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000CB10 File Offset: 0x0000CB10
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static delegate*<void*> <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(delegate*<void*> methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer()
			.ToPointer();
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000CD2C File Offset: 0x0000CD2C
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete(void*, ulong);

	// Token: 0x060000C5 RID: 197 RVA: 0x0000DF26 File Offset: 0x0000DF26
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __std_exception_copy(__std_exception_data*, __std_exception_data*);

	// Token: 0x060000C6 RID: 198 RVA: 0x0000CD20 File Offset: 0x0000CD20
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete[](void*, ulong);

	// Token: 0x060000C7 RID: 199 RVA: 0x0000DF3E File Offset: 0x0000DF3E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __std_exception_destroy(__std_exception_data*);

	// Token: 0x060000C8 RID: 200 RVA: 0x00001A80 File Offset: 0x00001A80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int IsEqualGUID(_GUID*, _GUID*);

	// Token: 0x060000C9 RID: 201 RVA: 0x00001AB0 File Offset: 0x00001AB0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int StringCchCopyW(char*, ulong, char*);

	// Token: 0x060000CA RID: 202 RVA: 0x00009BA0 File Offset: 0x00009BA0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int StringCchCopyExW(char*, ulong, char*, char**, ulong*, uint);

	// Token: 0x060000CB RID: 203 RVA: 0x0000A2B0 File Offset: 0x0000A2B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern uint ExternalStorage.CalculateWordCount(uint);

	// Token: 0x060000CC RID: 204 RVA: 0x00009950 File Offset: 0x00009950
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void ExternalStorage.Init(ExternalStorage*, uint*, uint);

	// Token: 0x060000CD RID: 205 RVA: 0x00009D80 File Offset: 0x00009D80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int SinglyLinkedListBase.IsEmpty(SinglyLinkedListBase*);

	// Token: 0x060000CE RID: 206 RVA: 0x000095D0 File Offset: 0x000095D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern SEListElem* SEListElem.{ctor}(SEListElem*);

	// Token: 0x060000CF RID: 207 RVA: 0x00009830 File Offset: 0x00009830
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ListBase* ListBase.{ctor}(ListBase*);

	// Token: 0x060000D0 RID: 208 RVA: 0x000095F0 File Offset: 0x000095F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern SList* SList.{ctor}(SList*);

	// Token: 0x060000D1 RID: 209 RVA: 0x00001170 File Offset: 0x00001170
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEEngineServicesAPI* XE_API.ServiceAPI();

	// Token: 0x060000D2 RID: 210 RVA: 0x000015C0 File Offset: 0x000015C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEEngineRegisterAPI* XE_API.RegistrationAPI();

	// Token: 0x060000D3 RID: 211 RVA: 0x0000A1B0 File Offset: 0x0000A1B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ulong XE_VariantLoad<struct\u0020_GUID\u0020*>(_GUID*);

	// Token: 0x060000D4 RID: 212 RVA: 0x00009B70 File Offset: 0x00009B70
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* XE_VariantUnload<void\u0020*>(ulong);

	// Token: 0x060000D5 RID: 213 RVA: 0x00009B60 File Offset: 0x00009B60
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern char* XE_VariantUnload<wchar_t\u0020const\u0020*>(ulong);

	// Token: 0x060000D6 RID: 214 RVA: 0x00009AF0 File Offset: 0x00009AF0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_IPackage.{dtor}(XE_IPackage*);

	// Token: 0x060000D7 RID: 215 RVA: 0x000096D0 File Offset: 0x000096D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_BufferCollector.Set(XE_BufferCollector*, void*, uint, ushort, ushort);

	// Token: 0x060000D8 RID: 216 RVA: 0x00009790 File Offset: 0x00009790
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_BufferCollector.SetDataActionCount(XE_BufferCollector*, ushort);

	// Token: 0x060000D9 RID: 217 RVA: 0x00009680 File Offset: 0x00009680
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern GenericEvent* GenericEvent.{ctor}(GenericEvent*);

	// Token: 0x060000DA RID: 218 RVA: 0x00009610 File Offset: 0x00009610
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern GenericEvent* GenericEvent.{ctor}(GenericEvent*, ushort, void*, uint);

	// Token: 0x060000DB RID: 219 RVA: 0x00009700 File Offset: 0x00009700
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int GenericEvent.FireBegin(GenericEvent*);

	// Token: 0x060000DC RID: 220 RVA: 0x000096C0 File Offset: 0x000096C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* @new(ulong, void*);

	// Token: 0x060000DD RID: 221 RVA: 0x00009D90 File Offset: 0x00009D90
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_ObjectSListEntry* TSinglyLinkedList<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.RemoveHeadFromNonEmptyList(TSinglyLinkedList<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>*);

	// Token: 0x060000DE RID: 222 RVA: 0x0000A280 File Offset: 0x0000A280
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void TSinglyLinkedList<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.InsertFirst(TSinglyLinkedList<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>*, XE_ObjectSListEntry*);

	// Token: 0x060000DF RID: 223 RVA: 0x00009410 File Offset: 0x00009410
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{dtor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>*);

	// Token: 0x060000E0 RID: 224 RVA: 0x000093C0 File Offset: 0x000093C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>* std.basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>.{ctor}(basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>*, basic_string<char,std::char_traits<char>,std::allocator<char>\u0020>*);

	// Token: 0x060000E1 RID: 225 RVA: 0x00001980 File Offset: 0x00001980
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_TCollection<0,0>* XE_TCollection<0,0>.{ctor}(XE_TCollection<0,0>*, XEObjectCollection*);

	// Token: 0x060000E2 RID: 226 RVA: 0x00001990 File Offset: 0x00001990
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint XE_TCollection<0,0>.GetCount(XE_TCollection<0,0>*);

	// Token: 0x060000E3 RID: 227 RVA: 0x0000A2D0 File Offset: 0x0000A2D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_TCollection<0,0>.Set(XE_TCollection<0,0>*, uint, XEObject*);

	// Token: 0x060000E4 RID: 228 RVA: 0x000019B0 File Offset: 0x000019B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* CAutoBase<void>..PEAX(CAutoBase<void>*);

	// Token: 0x060000E5 RID: 229 RVA: 0x000019C0 File Offset: 0x000019C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void** CAutoBase<void>.&(CAutoBase<void>*);

	// Token: 0x060000E6 RID: 230 RVA: 0x000019D0 File Offset: 0x000019D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* CAutoBase<void>.PvReturn(CAutoBase<void>*);

	// Token: 0x060000E7 RID: 231 RVA: 0x000019F0 File Offset: 0x000019F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_AutoResource<void\u0020*,136>* XE_AutoResource<void\u0020*,136>.{ctor}(XE_AutoResource<void\u0020*,136>*, void*);

	// Token: 0x060000E8 RID: 232 RVA: 0x00001A00 File Offset: 0x00001A00
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoResource<void\u0020*,136>.{dtor}(XE_AutoResource<void\u0020*,136>*);

	// Token: 0x060000E9 RID: 233 RVA: 0x0000A0B0 File Offset: 0x0000A0B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void SESList<XESessionContext,56>.InsertRelease(SESList<XESessionContext,56>*, XESessionContext*);

	// Token: 0x060000EA RID: 234 RVA: 0x00009910 File Offset: 0x00009910
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int SESList<XESessionContext,56>.IsInList(SESList<XESessionContext,56>*, XESessionContext*);

	// Token: 0x060000EB RID: 235 RVA: 0x00009960 File Offset: 0x00009960
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XBitmap<ExternalStorage>.Clear(XBitmap<ExternalStorage>*);

	// Token: 0x060000EC RID: 236 RVA: 0x0000A0A0 File Offset: 0x0000A0A0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint XBitmap<ExternalStorage>.GetNumberOfBits(XBitmap<ExternalStorage>*);

	// Token: 0x060000ED RID: 237 RVA: 0x0000A040 File Offset: 0x0000A040
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint XBitmap<ExternalStorage>.GetNumberAllocatedBits(XBitmap<ExternalStorage>*);

	// Token: 0x060000EE RID: 238 RVA: 0x0000A110 File Offset: 0x0000A110
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XBitmap<ExternalStorage>.Alloc(XBitmap<ExternalStorage>*, uint);

	// Token: 0x060000EF RID: 239 RVA: 0x00009940 File Offset: 0x00009940
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ExternalStorage* XBitmap<ExternalStorage>.GetStorage(XBitmap<ExternalStorage>*);

	// Token: 0x060000F0 RID: 240 RVA: 0x00009B10 File Offset: 0x00009B10
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>.Append(SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>*, XEventManagedPackage.AppDomainInfo*);

	// Token: 0x060000F1 RID: 241 RVA: 0x0000A0F0 File Offset: 0x0000A0F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>.Delete(XEventManagedPackage.AppDomainInfo*);

	// Token: 0x060000F2 RID: 242 RVA: 0x0000A0D0 File Offset: 0x0000A0D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEventManagedPackage.AppDomainInfo* SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>.GetNextElem(SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>*, XEventManagedPackage.AppDomainInfo*);

	// Token: 0x060000F3 RID: 243 RVA: 0x00009B30 File Offset: 0x00009B30
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>.GetCount(SEList<Microsoft::SqlServer::XEvent::Internal::XEventManagedPackage::AppDomainInfo,0>*);

	// Token: 0x060000F4 RID: 244 RVA: 0x00009850 File Offset: 0x00009850
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_IPackage.DisableEvent<struct\u0020XBitmap<struct\u0020ExternalStorage>\u0020>(XESessionContext*, XBitmap<ExternalStorage>*, int*);

	// Token: 0x060000F5 RID: 245 RVA: 0x00001A40 File Offset: 0x00001A40
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEEvent* XE_TCollection<0,0>.GetTyped<struct\u0020XEEvent>(XE_TCollection<0,0>*, uint);

	// Token: 0x060000F6 RID: 246 RVA: 0x00009B80 File Offset: 0x00009B80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEMap* XE_TCollection<0,0>.GetTyped<struct\u0020XEMap>(XE_TCollection<0,0>*, uint);

	// Token: 0x060000F7 RID: 247 RVA: 0x00001A60 File Offset: 0x00001A60
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XETarget* XE_TCollection<0,0>.GetTyped<struct\u0020XETarget>(XE_TCollection<0,0>*, uint);

	// Token: 0x060000F8 RID: 248 RVA: 0x0000A160 File Offset: 0x0000A160
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern ulong XE_VariantLoad<int>(int);

	// Token: 0x060000F9 RID: 249 RVA: 0x0000A170 File Offset: 0x0000A170
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern CAutoP<_GUID>* CAutoP<_GUID>.{ctor}(CAutoP<_GUID>*, _GUID*);

	// Token: 0x060000FA RID: 250 RVA: 0x0000A1D0 File Offset: 0x0000A1D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void CAutoP<_GUID>.{dtor}(CAutoP<_GUID>*);

	// Token: 0x060000FB RID: 251 RVA: 0x0000A190 File Offset: 0x0000A190
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern _GUID* CAutoBase<_GUID>.PvReturn(CAutoBase<_GUID>*);

	// Token: 0x060000FC RID: 252 RVA: 0x0000A180 File Offset: 0x0000A180
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern _GUID* CAutoBase<_GUID>.Get(CAutoBase<_GUID>*);

	// Token: 0x060000FD RID: 253 RVA: 0x0000A1C0 File Offset: 0x0000A1C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern ulong XE_VariantLoad<unsigned\u0020__int64>(ulong);

	// Token: 0x060000FE RID: 254 RVA: 0x0000A1F0 File Offset: 0x0000A1F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>* CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.{ctor}(CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>*, XEventPackageSet*);

	// Token: 0x060000FF RID: 255 RVA: 0x0000A350 File Offset: 0x0000A350
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.{dtor}(CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>*);

	// Token: 0x06000100 RID: 256 RVA: 0x0000A200 File Offset: 0x0000A200
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEventPackageSet* CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.->(CAutoP<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>*);

	// Token: 0x06000101 RID: 257 RVA: 0x0000A230 File Offset: 0x0000A230
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEventPackageSet* CAutoBase<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>..PEAVXEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@(CAutoBase<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>*);

	// Token: 0x06000102 RID: 258 RVA: 0x0000A2F0 File Offset: 0x0000A2F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEventPackageSet* CAutoBase<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>.PvReturn(CAutoBase<Microsoft::SqlServer::XEvent::Internal::XEventPackageSet>*);

	// Token: 0x06000103 RID: 259 RVA: 0x0000A210 File Offset: 0x0000A210
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>* CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>.{ctor}(CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>*, XE_ObjectSList*);

	// Token: 0x06000104 RID: 260 RVA: 0x0000A310 File Offset: 0x0000A310
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>.{dtor}(CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>*);

	// Token: 0x06000105 RID: 261 RVA: 0x0000A250 File Offset: 0x0000A250
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_ObjectSList* CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>.->(CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSList>*);

	// Token: 0x06000106 RID: 262 RVA: 0x0000A220 File Offset: 0x0000A220
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>* CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.{ctor}(CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>*, XE_ObjectSListEntry*);

	// Token: 0x06000107 RID: 263 RVA: 0x0000A290 File Offset: 0x0000A290
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.{dtor}(CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>*);

	// Token: 0x06000108 RID: 264 RVA: 0x0000A240 File Offset: 0x0000A240
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_ObjectSListEntry* CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.->(CAutoP<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>*);

	// Token: 0x06000109 RID: 265 RVA: 0x0000A260 File Offset: 0x0000A260
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_ObjectSListEntry* CAutoBase<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>.PvReturn(CAutoBase<Microsoft::SqlServer::XEvent::Internal::XE_ObjectSListEntry>*);

	// Token: 0x0600010A RID: 266 RVA: 0x00009F20 File Offset: 0x00009F20
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern CAutoP<XEEvent>* CAutoP<XEEvent>.{ctor}(CAutoP<XEEvent>*, XEEvent*);

	// Token: 0x0600010B RID: 267 RVA: 0x00009F60 File Offset: 0x00009F60
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void CAutoP<XEEvent>.{dtor}(CAutoP<XEEvent>*);

	// Token: 0x0600010C RID: 268 RVA: 0x00009F40 File Offset: 0x00009F40
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEEvent* CAutoBase<XEEvent>.PvReturn(CAutoBase<XEEvent>*);

	// Token: 0x0600010D RID: 269 RVA: 0x00009F30 File Offset: 0x00009F30
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEEvent* CAutoBase<XEEvent>.Get(CAutoBase<XEEvent>*);

	// Token: 0x0600010E RID: 270 RVA: 0x00009F80 File Offset: 0x00009F80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern CAutoP<XEMap>* CAutoP<XEMap>.{ctor}(CAutoP<XEMap>*, XEMap*);

	// Token: 0x0600010F RID: 271 RVA: 0x00009FC0 File Offset: 0x00009FC0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void CAutoP<XEMap>.{dtor}(CAutoP<XEMap>*);

	// Token: 0x06000110 RID: 272 RVA: 0x00009FA0 File Offset: 0x00009FA0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEMap* CAutoBase<XEMap>.PvReturn(CAutoBase<XEMap>*);

	// Token: 0x06000111 RID: 273 RVA: 0x00009F90 File Offset: 0x00009F90
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEMap* CAutoBase<XEMap>.Get(CAutoBase<XEMap>*);

	// Token: 0x06000112 RID: 274 RVA: 0x00009FE0 File Offset: 0x00009FE0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern CAutoP<XETarget>* CAutoP<XETarget>.{ctor}(CAutoP<XETarget>*, XETarget*);

	// Token: 0x06000113 RID: 275 RVA: 0x0000A020 File Offset: 0x0000A020
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void CAutoP<XETarget>.{dtor}(CAutoP<XETarget>*);

	// Token: 0x06000114 RID: 276 RVA: 0x0000A000 File Offset: 0x0000A000
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XETarget* CAutoBase<XETarget>.PvReturn(CAutoBase<XETarget>*);

	// Token: 0x06000115 RID: 277 RVA: 0x00009FF0 File Offset: 0x00009FF0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XETarget* CAutoBase<XETarget>.Get(CAutoBase<XETarget>*);

	// Token: 0x06000116 RID: 278 RVA: 0x000013C0 File Offset: 0x000013C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_IPackage.AggregateCustomizableAttributes(XESessionContext*);

	// Token: 0x06000117 RID: 279 RVA: 0x00001460 File Offset: 0x00001460
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_SessionContextList* XE_IPackage.GetList(XESessionContext*);

	// Token: 0x06000118 RID: 280 RVA: 0x0000E287 File Offset: 0x0000E287
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern int __CxxQueryExceptionSize();

	// Token: 0x06000119 RID: 281 RVA: 0x0000DF62 File Offset: 0x0000DF62
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void _CxxThrowException(void*, _s__ThrowInfo*);

	// Token: 0x0600011A RID: 282 RVA: 0x000011C0 File Offset: 0x000011C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void GenericEvent.CallNextAction(void*, XECollectedActionData*);

	// Token: 0x0600011B RID: 283 RVA: 0x0000E27C File Offset: 0x0000E27C
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* new[](ulong);

	// Token: 0x0600011C RID: 284 RVA: 0x0000E2AB File Offset: 0x0000E2AB
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxDetectRethrow(void*);

	// Token: 0x0600011D RID: 285 RVA: 0x0000E2B7 File Offset: 0x0000E2B7
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __CxxUnregisterExceptionObject(void*, int);

	// Token: 0x0600011E RID: 286 RVA: 0x0000E293 File Offset: 0x0000E293
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxExceptionFilter(void*, void*, int, void*);

	// Token: 0x0600011F RID: 287 RVA: 0x000020D0 File Offset: 0x000020D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern int XE_IsRidVld(XERelativeObjectId);

	// Token: 0x06000120 RID: 288 RVA: 0x00001470 File Offset: 0x00001470
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_SessionCtxtPublishEnum* XE_SessionCtxtPublishEnum.{ctor}(XE_SessionCtxtPublishEnum*);

	// Token: 0x06000121 RID: 289 RVA: 0x00001490 File Offset: 0x00001490
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_SessionCtxtPublishEnum.{dtor}(XE_SessionCtxtPublishEnum*);

	// Token: 0x06000122 RID: 290 RVA: 0x000014D0 File Offset: 0x000014D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XESessionContext* XE_SessionCtxtPublishEnum.GetFirst(XE_SessionCtxtPublishEnum*, XE_SessionContextList*);

	// Token: 0x06000123 RID: 291 RVA: 0x00001570 File Offset: 0x00001570
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XESessionContext* XE_SessionCtxtPublishEnum.GetNext(XE_SessionCtxtPublishEnum*, XE_SessionContextList*, XESessionContext*);

	// Token: 0x06000124 RID: 292 RVA: 0x000020B0 File Offset: 0x000020B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern ushort XE_GetTypeSize(XERelativeObjectId);

	// Token: 0x06000125 RID: 293 RVA: 0x00001D30 File Offset: 0x00001D30
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_AutoEngineMutex* XE_AutoEngineMutex.{ctor}(XE_AutoEngineMutex*, void*, XEWaitType);

	// Token: 0x06000126 RID: 294 RVA: 0x00001D50 File Offset: 0x00001D50
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEWaitResult XE_AutoEngineMutex.GetAccess(XE_AutoEngineMutex*, uint);

	// Token: 0x06000127 RID: 295 RVA: 0x00001D90 File Offset: 0x00001D90
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoEngineMutex.{dtor}(XE_AutoEngineMutex*);

	// Token: 0x06000128 RID: 296 RVA: 0x00001690 File Offset: 0x00001690
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_PackageManagerUtilities.SetPackageId(ushort, int, XEPackageMetadata*);

	// Token: 0x06000129 RID: 297 RVA: 0x00002250 File Offset: 0x00002250
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XEventAutoEngineLoad.IsXeDllLoaded(XEventAutoEngineLoad*);

	// Token: 0x0600012A RID: 298 RVA: 0x00002100 File Offset: 0x00002100
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEventAutoEngineLoad* XEventAutoEngineLoad.{ctor}(XEventAutoEngineLoad*);

	// Token: 0x0600012B RID: 299 RVA: 0x00002140 File Offset: 0x00002140
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XEventAutoEngineLoad.{dtor}(XEventAutoEngineLoad*);

	// Token: 0x0600012C RID: 300 RVA: 0x00002830 File Offset: 0x00002830
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEError* XEventAutoEngineLoad.GetInitFailureReason(XEventAutoEngineLoad*, XEventAutoEngineLoad.InitFailureReasons*);

	// Token: 0x0600012D RID: 301 RVA: 0x00002260 File Offset: 0x00002260
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XEventAutoEngineLoad.InitializeXE(XEventAutoEngineLoad*);

	// Token: 0x0600012E RID: 302 RVA: 0x0000CE08 File Offset: 0x0000CE08
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* @new(ulong);

	// Token: 0x0600012F RID: 303 RVA: 0x00002050 File Offset: 0x00002050
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern int XE_API.IsEnginePresent();

	// Token: 0x06000130 RID: 304 RVA: 0x00001DB0 File Offset: 0x00001DB0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_ErrorContext* XE_ErrorContext.{ctor}(XE_ErrorContext*, XEErrorContext*);

	// Token: 0x06000131 RID: 305 RVA: 0x00001DC0 File Offset: 0x00001DC0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_ErrorContext.AllocateErrorMessage(XE_ErrorContext*, uint);

	// Token: 0x06000132 RID: 306 RVA: 0x0000E29F File Offset: 0x0000E29F
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxRegisterExceptionObject(void*, void*);

	// Token: 0x06000133 RID: 307 RVA: 0x00002080 File Offset: 0x00002080
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern int XE_Compare(XERelativeObjectId, XERelativeObjectId);

	// Token: 0x06000134 RID: 308 RVA: 0x0000D814 File Offset: 0x0000D814
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete[](void*);

	// Token: 0x06000135 RID: 309 RVA: 0x00001B20 File Offset: 0x00001B20
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_PackageEnumerator* XE_PackageEnumerator.{ctor}(XE_PackageEnumerator*);

	// Token: 0x06000136 RID: 310 RVA: 0x00001B40 File Offset: 0x00001B40
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_PackageEnumerator.{dtor}(XE_PackageEnumerator*);

	// Token: 0x06000137 RID: 311 RVA: 0x00001B50 File Offset: 0x00001B50
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_PackageEnumerator.Begin(XE_PackageEnumerator*);

	// Token: 0x06000138 RID: 312 RVA: 0x00001C90 File Offset: 0x00001C90
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_PackageEnumerator.GetNextPackage(XE_PackageEnumerator*, XEPackageMetadata**);

	// Token: 0x06000139 RID: 313 RVA: 0x0000E2C3 File Offset: 0x0000E2C3
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x0600013A RID: 314 RVA: 0x0000DF92 File Offset: 0x0000DF92
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void** __current_exception_context();

	// Token: 0x0600013B RID: 315 RVA: 0x0000DFC2 File Offset: 0x0000DFC2
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void terminate();

	// Token: 0x0600013C RID: 316 RVA: 0x0000DF86 File Offset: 0x0000DF86
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void** __current_exception();

	// Token: 0x0600013D RID: 317 RVA: 0x0000BBF0 File Offset: 0x0000BBF0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x0600013E RID: 318 RVA: 0x0000E05E File Offset: 0x0000E05E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _cexit();

	// Token: 0x0600013F RID: 319 RVA: 0x0000E264 File Offset: 0x0000E264
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void Sleep(uint);

	// Token: 0x06000140 RID: 320 RVA: 0x0000E2CF File Offset: 0x0000E2CF
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void abort();

	// Token: 0x06000141 RID: 321 RVA: 0x0000DA20 File Offset: 0x0000DA20
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern void __security_init_cookie();

	// Token: 0x06000142 RID: 322 RVA: 0x0000D7A8 File Offset: 0x0000D7A8
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _Init_thread_wait(uint);

	// Token: 0x06000143 RID: 323 RVA: 0x0000D74C File Offset: 0x0000D74C
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _Init_thread_notify();

	// Token: 0x06000144 RID: 324 RVA: 0x0000D794 File Offset: 0x0000D794
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _Init_thread_unlock();

	// Token: 0x06000145 RID: 325 RVA: 0x0000D738 File Offset: 0x0000D738
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _Init_thread_lock();

	// Token: 0x04000001 RID: 1 RVA: 0x00010450 File Offset: 0x00010450
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BC@$$CBD ??_C@_0BC@EOODALEL@Unknown?5exception@;

	// Token: 0x04000002 RID: 2 RVA: 0x00026F48 File Offset: 0x00026F48
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4exception@std@@6B@;

	// Token: 0x04000003 RID: 3 RVA: 0x00027018 File Offset: 0x00027018
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2bad_array_new_length@std@@8;

	// Token: 0x04000004 RID: 4 RVA: 0x0002A5B0 File Offset: 0x0002A5B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVbad_alloc@std@@@8;

	// Token: 0x04000005 RID: 5 RVA: 0x00010D48 File Offset: 0x00010D48
	// Note: this field is marked with 'hasfieldrva'.
	internal static XERelativeObjectId XET_CHANNEL_MAP;

	// Token: 0x04000006 RID: 6 RVA: 0x0002A050 File Offset: 0x0002A050
	// Note: this field is marked with 'hasfieldrva'.
	internal static XECollectedActionData ?A0x804d7b89.NULLCAD;

	// Token: 0x04000007 RID: 7 RVA: 0x00010D4C File Offset: 0x00010D4C
	// Note: this field is marked with 'hasfieldrva'.
	internal static XERelativeObjectId XET_UINT8;

	// Token: 0x04000008 RID: 8 RVA: 0x00026F98 File Offset: 0x00026F98
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_alloc@std@@8;

	// Token: 0x04000009 RID: 9 RVA: 0x0002A038 File Offset: 0x0002A038
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_array_new_length@std@@6B@;

	// Token: 0x0400000A RID: 10 RVA: 0x00026EF8 File Offset: 0x00026EF8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@exception@std@@8;

	// Token: 0x0400000B RID: 11 RVA: 0x00026F70 File Offset: 0x00026F70
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_alloc@std@@8;

	// Token: 0x0400000C RID: 12 RVA: 0x00010D50 File Offset: 0x00010D50
	// Note: this field is marked with 'hasfieldrva'.
	internal static XERelativeObjectId XET_VLD_WSTR;

	// Token: 0x0400000D RID: 13 RVA: 0x0002A020 File Offset: 0x0002A020
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_alloc@std@@6B@;

	// Token: 0x0400000E RID: 14 RVA: 0x00026F20 File Offset: 0x00026F20
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2exception@std@@8;

	// Token: 0x0400000F RID: 15 RVA: 0x0002A008 File Offset: 0x0002A008
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7exception@std@@6B@;

	// Token: 0x04000010 RID: 16 RVA: 0x00026F30 File Offset: 0x00026F30
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3exception@std@@8;

	// Token: 0x04000011 RID: 17 RVA: 0x0002A048 File Offset: 0x0002A048
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEActionDataDescriptor ?A0x804d7b89.NULLADD;

	// Token: 0x04000012 RID: 18 RVA: 0x00026FC8 File Offset: 0x00026FC8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_alloc@std@@6B@;

	// Token: 0x04000013 RID: 19 RVA: 0x00027038 File Offset: 0x00027038
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_array_new_length@std@@8;

	// Token: 0x04000014 RID: 20 RVA: 0x00026FB0 File Offset: 0x00026FB0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_alloc@std@@8;

	// Token: 0x04000015 RID: 21 RVA: 0x00010D54 File Offset: 0x00010D54
	// Note: this field is marked with 'hasfieldrva'.
	internal static XERelativeObjectId XET_UUID_PTR;

	// Token: 0x04000016 RID: 22 RVA: 0x00027050 File Offset: 0x00027050
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_array_new_length@std@@6B@;

	// Token: 0x04000017 RID: 23 RVA: 0x00010D58 File Offset: 0x00010D58
	// Note: this field is marked with 'hasfieldrva'.
	internal static XERelativeObjectId XET_WSTR;

	// Token: 0x04000018 RID: 24 RVA: 0x00026FF0 File Offset: 0x00026FF0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_array_new_length@std@@8;

	// Token: 0x04000019 RID: 25 RVA: 0x00010D60 File Offset: 0x00010D60
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEActivityId XEAID_FAIL;

	// Token: 0x0400001A RID: 26 RVA: 0x0002A588 File Offset: 0x0002A588
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVexception@std@@@8;

	// Token: 0x0400001B RID: 27 RVA: 0x0002A5D8 File Offset: 0x0002A5D8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_31 ??_R0?AVbad_array_new_length@std@@@8;

	// Token: 0x0400001C RID: 28 RVA: 0x0002A068 File Offset: 0x0002A068
	// Note: this field is marked with 'hasfieldrva'.
	internal static XECollectedActionData ?A0x309efa37.NULLCAD;

	// Token: 0x0400001D RID: 29 RVA: 0x0002A060 File Offset: 0x0002A060
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEActionDataDescriptor ?A0x309efa37.NULLADD;

	// Token: 0x0400001E RID: 30 RVA: 0x00010580 File Offset: 0x00010580
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0N@$$CB_W ??_C@_1BK@JHDDLJON@?$AAX?$AAE?$AAP?$AAa?$AAc?$AAk?$AAa?$AAg?$AAe?$AAA?$AAP?$AAI@;

	// Token: 0x0400001F RID: 31 RVA: 0x000105A0 File Offset: 0x000105A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0M@$$CB_W ??_C@_1BI@DECJJGGP@?$AAX?$AAE?$AAT?$AAa?$AAr?$AAg?$AAe?$AAt?$AAA?$AAP?$AAI@;

	// Token: 0x04000020 RID: 32 RVA: 0x00010DA4 File Offset: 0x00010DA4
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00$$CB_W ??_C@_11LOCGONAA@@;

	// Token: 0x04000021 RID: 33 RVA: 0x000108F8 File Offset: 0x000108F8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BF@$$CBD ??_C@_0BF@KINCDENJ@bad?5array?5new?5length@;

	// Token: 0x04000022 RID: 34 RVA: 0x00010AA0 File Offset: 0x00010AA0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BA@$$CBD ??_C@_0BA@JFNIOLAK@string?5too?5long@;

	// Token: 0x04000023 RID: 35 RVA: 0x000109F0 File Offset: 0x000109F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02$$CBD ??_C@_02LMMGGCAJ@?3?5@;

	// Token: 0x04000024 RID: 36 RVA: 0x00010A70 File Offset: 0x00010A70
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY07$$CBD ??_C@_07DCLBNMLN@generic@;

	// Token: 0x04000025 RID: 37 RVA: 0x00010960 File Offset: 0x00010960
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY06$$CBD ??_C@_06FHFOAHML@system@;

	// Token: 0x04000026 RID: 38 RVA: 0x00010698 File Offset: 0x00010698
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY04$$CB_W ??_C@_19GCKPLDOJ@?$AAU?$AAU?$AAI?$AAD@;

	// Token: 0x04000027 RID: 39 RVA: 0x000106A8 File Offset: 0x000106A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY07$$CB_W ??_C@_1BA@HMOCDLBP@?$AAV?$AAE?$AAR?$AAS?$AAI?$AAO?$AAN@;

	// Token: 0x04000028 RID: 40 RVA: 0x000106B8 File Offset: 0x000106B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY07$$CB_W ??_C@_1BA@PPOHHPGM@?$AAC?$AAH?$AAA?$AAN?$AAN?$AAE?$AAL@;

	// Token: 0x04000029 RID: 41 RVA: 0x000106C8 File Offset: 0x000106C8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY07$$CB_W ??_C@_1BA@PNGMLLAK@?$AAK?$AAE?$AAY?$AAW?$AAO?$AAR?$AAD@;

	// Token: 0x0400002A RID: 42 RVA: 0x00010810 File Offset: 0x00010810
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BK@$$CBD ??_C@_0BK@FLIBCE@ExecuteInAppDomain?5failed@;

	// Token: 0x0400002B RID: 43 RVA: 0x0002A608 File Offset: 0x0002A608
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_24 ??_R0?AVruntime_error@std@@@8;

	// Token: 0x0400002C RID: 44 RVA: 0x000106D8 File Offset: 0x000106D8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY03QEB_W ?A0xf3aec127.?MANDATORY_FIELD_NAMES@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4QBQEB_WB;

	// Token: 0x0400002D RID: 45 RVA: 0x000271A8 File Offset: 0x000271A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_32 ??_R2system_error@std@@8;

	// Token: 0x0400002E RID: 46 RVA: 0x00027180 File Offset: 0x00027180
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@system_error@std@@8;

	// Token: 0x0400002F RID: 47 RVA: 0x000288D0 File Offset: 0x000288D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__ThrowInfo _TI4?AVsystem_error@std@@;

	// Token: 0x04000030 RID: 48 RVA: 0x00010570 File Offset: 0x00010570
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID XEMANAGED_MODULE_GUID;

	// Token: 0x04000031 RID: 49 RVA: 0x00027308 File Offset: 0x00027308
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XEventRegisterAutoLoader@Internal@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x04000032 RID: 50 RVA: 0x0002A158 File Offset: 0x0002A158
	// Note: this field is marked with 'hasfieldrva'.
	internal static XECollectedActionData ?A0xf3aec127.NULLCAD;

	// Token: 0x04000033 RID: 51 RVA: 0x000270D0 File Offset: 0x000270D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4runtime_error@std@@6B@;

	// Token: 0x04000034 RID: 52 RVA: 0x00027480 File Offset: 0x00027480
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4_System_error_category@std@@6B@;

	// Token: 0x04000035 RID: 53 RVA: 0x00027158 File Offset: 0x00027158
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4_System_error@std@@6B@;

	// Token: 0x04000036 RID: 54 RVA: 0x00027500 File Offset: 0x00027500
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4_Generic_error_category@std@@6B@;

	// Token: 0x04000037 RID: 55 RVA: 0x0002A4C0 File Offset: 0x0002A4C0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY06Q6AXXZ ??_7_Generic_error_category@std@@6B@;

	// Token: 0x04000038 RID: 56 RVA: 0x0002A7F0 File Offset: 0x0002A7F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_25 ??_R0?AVerror_category@std@@@8;

	// Token: 0x04000039 RID: 57 RVA: 0x00027078 File Offset: 0x00027078
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@runtime_error@std@@8;

	// Token: 0x0400003A RID: 58 RVA: 0x0002A880 File Offset: 0x0002A880
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static XE_PackageManager.ModuleMapEntry* ?sm_staticPackageModules@XE_PackageManager@@0PEAUModuleMapEntry@1@EA;

	// Token: 0x0400003B RID: 59 RVA: 0x00027120 File Offset: 0x00027120
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2_System_error@std@@8;

	// Token: 0x0400003C RID: 60 RVA: 0x0002A658 File Offset: 0x0002A658
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_23 ??_R0?AVsystem_error@std@@@8;

	// Token: 0x0400003D RID: 61 RVA: 0x0002A7B8 File Offset: 0x0002A7B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_33 ??_R0?AV_System_error_category@std@@@8;

	// Token: 0x0400003E RID: 62 RVA: 0x00028940 File Offset: 0x00028940
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__CatchableTypeArray$_extraBytes_24 _CTA3?AVbad_array_new_length@std@@;

	// Token: 0x0400003F RID: 63 RVA: 0x000271E8 File Offset: 0x000271E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4system_error@std@@6B@;

	// Token: 0x04000040 RID: 64 RVA: 0x000274A8 File Offset: 0x000274A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@_Generic_error_category@std@@8;

	// Token: 0x04000041 RID: 65 RVA: 0x0002B1E0 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEventRegisterAutoLoader g_XEventAutoLoader;

	// Token: 0x04000042 RID: 66 RVA: 0x00010380 File Offset: 0x00010380
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xf3aec127.g_XEventAutoLoader$initializer$;

	// Token: 0x04000043 RID: 67 RVA: 0x000272E0 File Offset: 0x000272E0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@6B@;

	// Token: 0x04000044 RID: 68 RVA: 0x00027358 File Offset: 0x00027358
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XEventAutoEngineLoad@@8;

	// Token: 0x04000045 RID: 69 RVA: 0x0002A6B0 File Offset: 0x0002A6B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_18 ??_R0?AVXE_IPackage@@@8;

	// Token: 0x04000046 RID: 70 RVA: 0x00027288 File Offset: 0x00027288
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x04000047 RID: 71 RVA: 0x00027140 File Offset: 0x00027140
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3_System_error@std@@8;

	// Token: 0x04000048 RID: 72 RVA: 0x0002A820 File Offset: 0x0002A820
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_34 ??_R0?AV_Generic_error_category@std@@@8;

	// Token: 0x04000049 RID: 73 RVA: 0x00028858 File Offset: 0x00028858
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVruntime_error@std@@@8??0runtime_error@std@@$$FQEAA@AEBV01@@Z24;

	// Token: 0x0400004A RID: 74 RVA: 0x00028808 File Offset: 0x00028808
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVsystem_error@std@@@8??0system_error@std@@$$FQEAA@AEBV01@@Z40;

	// Token: 0x0400004B RID: 75 RVA: 0x00027450 File Offset: 0x00027450
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2_System_error_category@std@@8;

	// Token: 0x0400004C RID: 76 RVA: 0x0002B210 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '9460301'.
	internal static int ?A0xf3aec127.?$TSS0@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4HA;

	// Token: 0x0400004D RID: 77 RVA: 0x00010968 File Offset: 0x00010968
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0O@$$CBD ?_Unknown_error@?4??message@_System_error_category@std@@UEBA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@3@H@Z@4QBDB;

	// Token: 0x0400004E RID: 78 RVA: 0x00010560 File Offset: 0x00010560
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID CLSID_CLRRuntimeHost;

	// Token: 0x0400004F RID: 79 RVA: 0x000273B0 File Offset: 0x000273B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4XEventRegisterAutoLoader@Internal@XEvent@SqlServer@Microsoft@@6B@;

	// Token: 0x04000050 RID: 80 RVA: 0x000274E8 File Offset: 0x000274E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3_Generic_error_category@std@@8;

	// Token: 0x04000051 RID: 81 RVA: 0x0002B218 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY03UXERelativeObjectId@@ ?A0xf3aec127.?MANDATORY_FIELD_TYPES@?1??CreateXEEvent@XEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@AEAMPEAUXEEvent@@PEAV23456@PE$AAV?$Dictionary@PE$AAVType@System@@PE$AAV?$XEventAutoObject@UXEMap@@@Internal@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@PE$AAV?$Dictionary@PE$AAVString@System@@I@9Collections@System@@PE$AAVType@System@@@Z@4PAUXERelativeObjectId@@A;

	// Token: 0x04000052 RID: 82 RVA: 0x00027400 File Offset: 0x00027400
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2error_category@std@@8;

	// Token: 0x04000053 RID: 83 RVA: 0x000288A8 File Offset: 0x000288A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__CatchableTypeArray$_extraBytes_32 _CTA4?AVsystem_error@std@@;

	// Token: 0x04000054 RID: 84 RVA: 0x0002B238 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01U?$atomic@_K@std@@ ?_Storage@?1???$_Immortalize_memcpy_image@V_Generic_error_category@std@@@std@@YAAEBV_Generic_error_category@1@XZ@4PAU?$atomic@_K@1@A;

	// Token: 0x04000055 RID: 85 RVA: 0x000272B0 File Offset: 0x000272B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x04000056 RID: 86 RVA: 0x0002A1B8 File Offset: 0x0002A1B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY08Q6AXXZ ??_7XE_IPackage@@6B@;

	// Token: 0x04000057 RID: 87 RVA: 0x00027248 File Offset: 0x00027248
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XE_IPackage@@8;

	// Token: 0x04000058 RID: 88 RVA: 0x0002A170 File Offset: 0x0002A170
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7runtime_error@std@@6B@;

	// Token: 0x04000059 RID: 89 RVA: 0x00027330 File Offset: 0x00027330
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2XEventAutoEngineLoad@@8;

	// Token: 0x0400005A RID: 90 RVA: 0x0002A188 File Offset: 0x0002A188
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7_System_error@std@@6B@;

	// Token: 0x0400005B RID: 91 RVA: 0x0002A080 File Offset: 0x0002A080
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEPackageAPI g_ipackage;

	// Token: 0x0400005C RID: 92 RVA: 0x0002A788 File Offset: 0x0002A788
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_27 ??_R0?AVXEventAutoEngineLoad@@@8;

	// Token: 0x0400005D RID: 93 RVA: 0x000272C8 File Offset: 0x000272C8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x0400005E RID: 94 RVA: 0x0002A0F0 File Offset: 0x0002A0F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static XETargetAPI g_itarget;

	// Token: 0x0400005F RID: 95 RVA: 0x0002A150 File Offset: 0x0002A150
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEActionDataDescriptor ?A0xf3aec127.NULLADD;

	// Token: 0x04000060 RID: 96 RVA: 0x000288F0 File Offset: 0x000288F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVbad_array_new_length@std@@@8??0bad_array_new_length@std@@$$FQEAA@AEBV01@@Z24;

	// Token: 0x04000061 RID: 97 RVA: 0x0002B228 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01U?$atomic@_K@std@@ ?_Storage@?1???$_Immortalize_memcpy_image@V_System_error_category@std@@@std@@YAAEBV_System_error_category@1@XZ@4PAU?$atomic@_K@1@A;

	// Token: 0x04000062 RID: 98 RVA: 0x00010558 File Offset: 0x00010558
	// Note: this field is marked with 'hasfieldrva'.
	internal static _Fake_allocator std.?A0xf3aec127._Fake_alloc;

	// Token: 0x04000063 RID: 99 RVA: 0x000274D0 File Offset: 0x000274D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2_Generic_error_category@std@@8;

	// Token: 0x04000064 RID: 100 RVA: 0x000270F8 File Offset: 0x000270F8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@_System_error@std@@8;

	// Token: 0x04000065 RID: 101 RVA: 0x00027410 File Offset: 0x00027410
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3error_category@std@@8;

	// Token: 0x04000066 RID: 102 RVA: 0x00027260 File Offset: 0x00027260
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4XE_IPackage@@6B@;

	// Token: 0x04000067 RID: 103 RVA: 0x00027380 File Offset: 0x00027380
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2XEventRegisterAutoLoader@Internal@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x04000068 RID: 104 RVA: 0x0002A6E0 File Offset: 0x0002A6E0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_63 ??_R0?AVXEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@@8;

	// Token: 0x04000069 RID: 105 RVA: 0x00027468 File Offset: 0x00027468
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3_System_error_category@std@@8;

	// Token: 0x0400006A RID: 106 RVA: 0x0002A888 File Offset: 0x0002A888
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '0'.
	internal static uint ?sm_nStaticPackageModules@XE_PackageManager@@0IA;

	// Token: 0x0400006B RID: 107 RVA: 0x0002A730 File Offset: 0x0002A730
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_67 ??_R0?AVXEventRegisterAutoLoader@Internal@XEvent@SqlServer@Microsoft@@@8;

	// Token: 0x0400006C RID: 108 RVA: 0x000270A0 File Offset: 0x000270A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2runtime_error@std@@8;

	// Token: 0x0400006D RID: 109 RVA: 0x0002A488 File Offset: 0x0002A488
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY06Q6AXXZ ??_7_System_error_category@std@@6B@;

	// Token: 0x0400006E RID: 110 RVA: 0x00027210 File Offset: 0x00027210
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XE_IPackage@@8;

	// Token: 0x0400006F RID: 111 RVA: 0x0002A890 File Offset: 0x0002A890
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static void* ?sm_initLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA;

	// Token: 0x04000070 RID: 112 RVA: 0x0002A8A0 File Offset: 0x0002A8A0
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static void* ?sm_eventLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA;

	// Token: 0x04000071 RID: 113 RVA: 0x0002A898 File Offset: 0x0002A898
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static void* ?sm_targetLock@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@0PEAXEA;

	// Token: 0x04000072 RID: 114 RVA: 0x0002A630 File Offset: 0x0002A630
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_24 ??_R0?AV_System_error@std@@@8;

	// Token: 0x04000073 RID: 115 RVA: 0x0002A3D8 File Offset: 0x0002A3D8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01Q6AXXZ ??_7XEventRegisterAutoLoader@Internal@XEvent@SqlServer@Microsoft@@6B@;

	// Token: 0x04000074 RID: 116 RVA: 0x000105B8 File Offset: 0x000105B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID IID_ICLRRuntimeHost;

	// Token: 0x04000075 RID: 117 RVA: 0x00028918 File Offset: 0x00028918
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVbad_alloc@std@@@8??0bad_alloc@std@@$$FQEAA@AEBV01@@Z24;

	// Token: 0x04000076 RID: 118 RVA: 0x00028880 File Offset: 0x00028880
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVexception@std@@@8??0exception@std@@$$FQEAA@AEBV01@@Z24;

	// Token: 0x04000077 RID: 119 RVA: 0x0002A300 File Offset: 0x0002A300
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY08Q6AXXZ ??_7XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@6B@;

	// Token: 0x04000078 RID: 120 RVA: 0x000271D0 File Offset: 0x000271D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3system_error@std@@8;

	// Token: 0x04000079 RID: 121 RVA: 0x00028960 File Offset: 0x00028960
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__ThrowInfo _TI3?AVbad_array_new_length@std@@;

	// Token: 0x0400007A RID: 122 RVA: 0x00027238 File Offset: 0x00027238
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2XE_IPackage@@8;

	// Token: 0x0400007B RID: 123 RVA: 0x000273D8 File Offset: 0x000273D8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@_System_error_category@std@@8;

	// Token: 0x0400007C RID: 124 RVA: 0x00027340 File Offset: 0x00027340
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XEventAutoEngineLoad@@8;

	// Token: 0x0400007D RID: 125 RVA: 0x0002A1A0 File Offset: 0x0002A1A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7system_error@std@@6B@;

	// Token: 0x0400007E RID: 126 RVA: 0x000270B8 File Offset: 0x000270B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3runtime_error@std@@8;

	// Token: 0x0400007F RID: 127 RVA: 0x00027398 File Offset: 0x00027398
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XEventRegisterAutoLoader@Internal@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x04000080 RID: 128 RVA: 0x00028830 File Offset: 0x00028830
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AV_System_error@std@@@8??0_System_error@std@@$$FQEAA@AEBV01@@Z40;

	// Token: 0x04000081 RID: 129 RVA: 0x00027428 File Offset: 0x00027428
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@error_category@std@@8;

	// Token: 0x04000082 RID: 130 RVA: 0x0002A3F0 File Offset: 0x0002A3F0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<ICLRRuntimeHost*> __m2mep@?get_clr_runtime_host@_detail@msclr@@$$FYAPEAUICLRRuntimeHost@@XZ;

	// Token: 0x04000083 RID: 131 RVA: 0x0002A370 File Offset: 0x0002A370
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_ObjectSList*, void> __m2mep@??1XE_ObjectSList@Internal@XEvent@SqlServer@Microsoft@@$$FQEAA@XZ;

	// Token: 0x04000084 RID: 132 RVA: 0x0002A430 File Offset: 0x0002A430
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventManagedPackage*, uint, void*> __m2mep@??_EXEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@$$FUEAAPEAXI@Z;

	// Token: 0x04000085 RID: 133 RVA: 0x0002A390 File Offset: 0x0002A390
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventManagedPackage*, void> __m2mep@?Destroy@XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@$$FUEAAXXZ;

	// Token: 0x04000086 RID: 134 RVA: 0x0002A460 File Offset: 0x0002A460
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventPackageSet*, uint, void*> __m2mep@??_GXEventPackageSet@Internal@XEvent@SqlServer@Microsoft@@$$FQEAAPEAXI@Z;

	// Token: 0x04000087 RID: 135 RVA: 0x0002A1F8 File Offset: 0x0002A1F8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventManagedPackage*, XESessionContext*, int> __m2mep@?DisableEvent@XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@$$FUEAAHQEAUXESessionContext@@@Z;

	// Token: 0x04000088 RID: 136 RVA: 0x0002A3C0 File Offset: 0x0002A3C0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventManagedPackage*, XESessionContext*, int> __m2mep@?EnableEvent@XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@$$FUEAAHQEAUXESessionContext@@@Z;

	// Token: 0x04000089 RID: 137 RVA: 0x0002A3B0 File Offset: 0x0002A3B0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, XEEvent*, bool> __m2mep@?EnableEventInAppDomain@XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@$$FCA_NPEB_WPEBUXEEvent@@@Z;

	// Token: 0x0400008A RID: 138 RVA: 0x0002A208 File Offset: 0x0002A208
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventManagedPackage*, XESessionContext*, int*, int> __m2mep@?IsEventEnabled@XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@$$FUEAAHQEAUXESessionContext@@QEAH@Z;

	// Token: 0x0400008B RID: 139 RVA: 0x0002A340 File Offset: 0x0002A340
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventManagedPackage*, uint, uint, char*, uint, int> __m2mep@?GetLocalizedStringImpl@XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAV12345@KKPEA_WK@Z;

	// Token: 0x0400008C RID: 140 RVA: 0x0002A218 File Offset: 0x0002A218
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventManagedPackage*, XEPackageMetadata*> __m2mep@?GetMetadata@XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@$$FUEBAQEBUXEPackageMetadata@@XZ;

	// Token: 0x0400008D RID: 141 RVA: 0x0002A228 File Offset: 0x0002A228
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventManagedPackage*, XEPackage*, ushort, int> __m2mep@?Init@XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@$$FUEAAHPEBUXEPackage@@G@Z;

	// Token: 0x0400008E RID: 142 RVA: 0x0002A238 File Offset: 0x0002A238
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEPackage*, ushort, void**, int> __m2mep@?InitPackage@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHQEBUXEPackage@@GQEAPEAX@Z;

	// Token: 0x0400008F RID: 143 RVA: 0x0002A248 File Offset: 0x0002A248
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?FinalizePackage@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAX@Z;

	// Token: 0x04000090 RID: 144 RVA: 0x0002A258 File Offset: 0x0002A258
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, XEPackageMetadata**, int> __m2mep@?GetMetadata@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAXQEAPEBUXEPackageMetadata@@@Z;

	// Token: 0x04000091 RID: 145 RVA: 0x0002A268 File Offset: 0x0002A268
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, XESessionContext*, int> __m2mep@?EnableEvent@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAXQEAUXESessionContext@@@Z;

	// Token: 0x04000092 RID: 146 RVA: 0x0002A278 File Offset: 0x0002A278
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, XESessionContext*, int*, int> __m2mep@?IsEventEnabled@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAXQEAUXESessionContext@@QEAH@Z;

	// Token: 0x04000093 RID: 147 RVA: 0x0002A288 File Offset: 0x0002A288
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, XESessionContext*, int> __m2mep@?DisableEvent@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAXQEAUXESessionContext@@@Z;

	// Token: 0x04000094 RID: 148 RVA: 0x0002A298 File Offset: 0x0002A298
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEError*> __m2mep@?GetLastError@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAQEBUXEError@@XZ;

	// Token: 0x04000095 RID: 149 RVA: 0x0002A440 File Offset: 0x0002A440
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEBuffer*, XEventTargetForwarder*, bool> __m2mep@?ProcessBufferInternal@XEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@$$FCA_NQEBUXEBuffer@@PEAV12345@@Z;

	// Token: 0x04000096 RID: 150 RVA: 0x0002A450 File Offset: 0x0002A450
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventTargetForwarder*, void> __m2mep@?FinalizeTarget@XEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@$$FCAXPEAV12345@@Z;

	// Token: 0x04000097 RID: 151 RVA: 0x0002A350 File Offset: 0x0002A350
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XETarget*, void*, ushort, XECustomizableAttribute*, void**, XEErrorContext*, int> __m2mep@?InitTarget@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHQEBUXETarget@@PEAXGQEBUXECustomizableAttribute@@PEAPEAXPEAUXEErrorContext@@@Z;

	// Token: 0x04000098 RID: 152 RVA: 0x0002A3A0 File Offset: 0x0002A3A0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?FinalizeTarget@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAX@Z;

	// Token: 0x04000099 RID: 153 RVA: 0x0002A380 File Offset: 0x0002A380
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void*, XEBuffer*, int> __m2mep@?ProcessBuffer@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAX0QEBUXEBuffer@@@Z;

	// Token: 0x0400009A RID: 154 RVA: 0x0002A2A8 File Offset: 0x0002A2A8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void*, XEEventBufferHeader*, XEEvent*, int> __m2mep@?ProcessEventASync@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAX0QEFBUXEEventBufferHeader@@QEBUXEEvent@@@Z;

	// Token: 0x0400009B RID: 155 RVA: 0x0002A2B8 File Offset: 0x0002A2B8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void*, XECollectedEvent*, XEEvent*, int> __m2mep@?ProcessEventSync@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAX0QEBUXECollectedEvent@@QEBUXEEvent@@@Z;

	// Token: 0x0400009C RID: 156 RVA: 0x0002A360 File Offset: 0x0002A360
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, char*, ulong, char**, ulong*, int> __m2mep@?GetSerializedState@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAXPEA_W_KPEAPEA_WPEA_K@Z;

	// Token: 0x0400009D RID: 157 RVA: 0x0002A2C8 File Offset: 0x0002A2C8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, uint, void**, XEAPI**, int> __m2mep@?GetPrivateAPI@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAHPEAXIPEAPEAXPEAPEBUXEAPI@@@Z;

	// Token: 0x0400009E RID: 158 RVA: 0x0002A2D8 File Offset: 0x0002A2D8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, uint> __m2mep@?GetMaxMemory@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSAIPEAX@Z;

	// Token: 0x0400009F RID: 159 RVA: 0x0002A2E8 File Offset: 0x0002A2E8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, ulong> __m2mep@?GetTargetBytesWritten@XEventInteropPackageManager@Internal@XEvent@SqlServer@Microsoft@@$$FSA_KPEAX@Z;

	// Token: 0x040000A0 RID: 160 RVA: 0x0002A3E0 File Offset: 0x0002A3E0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventRegisterAutoLoader*, int> __m2mep@?InitializeXE@XEventRegisterAutoLoader@Internal@XEvent@SqlServer@Microsoft@@$$FUEAAHXZ;

	// Token: 0x040000A1 RID: 161 RVA: 0x0002A400 File Offset: 0x0002A400
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?callback@?$callback_cdecl_void_struct1@PEAVXEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@@_detail@msclr@@$$FSAJPEAX@Z;

	// Token: 0x040000A2 RID: 162 RVA: 0x0002A410 File Offset: 0x0002A410
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?callback@?$callback_cdecl_struct2@_NPEBUXEBuffer@@PEAVXEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@@_detail@msclr@@$$FSAJPEAX@Z;

	// Token: 0x040000A3 RID: 163 RVA: 0x0002A470 File Offset: 0x0002A470
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?callback@?$callback_cdecl_struct5@HPEAVXEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@KKPEA_WK@_detail@msclr@@$$FSAJPEAX@Z;

	// Token: 0x040000A4 RID: 164 RVA: 0x0002A420 File Offset: 0x0002A420
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?callback@?$callback_cdecl_struct2@_NPEB_WPEBUXEEvent@@@_detail@msclr@@$$FSAJPEAX@Z;

	// Token: 0x040000A5 RID: 165 RVA: 0x00010650 File Offset: 0x00010650
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?callback@?$callback_cdecl_void_struct1@PEAVXEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@@_detail@msclr@@$$FSAJPEAX@Z;

	// Token: 0x040000A6 RID: 166 RVA: 0x00010648 File Offset: 0x00010648
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?callback@?$callback_cdecl_struct2@_NPEBUXEBuffer@@PEAVXEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@@_detail@msclr@@$$FSAJPEAX@Z;

	// Token: 0x040000A7 RID: 167 RVA: 0x00010640 File Offset: 0x00010640
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?callback@?$callback_cdecl_struct2@_NPEB_WPEBUXEEvent@@@_detail@msclr@@$$FSAJPEAX@Z;

	// Token: 0x040000A8 RID: 168 RVA: 0x00010680 File Offset: 0x00010680
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?ProcessBufferInternal@XEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@$$FCA_NQEBUXEBuffer@@PEAV12345@@Z;

	// Token: 0x040000A9 RID: 169 RVA: 0x00010688 File Offset: 0x00010688
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?FinalizeTarget@XEventTargetForwarder@Internal@XEvent@SqlServer@Microsoft@@$$FCAXPEAV12345@@Z;

	// Token: 0x040000AA RID: 170 RVA: 0x00010690 File Offset: 0x00010690
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?EnableEventInAppDomain@XEventManagedPackage@Internal@XEvent@SqlServer@Microsoft@@$$FCA_NPEB_WPEBUXEEvent@@@Z;

	// Token: 0x040000AB RID: 171 RVA: 0x0002A4F8 File Offset: 0x0002A4F8
	// Note: this field is marked with 'hasfieldrva'.
	internal static XECollectedActionData ?A0x0c12843d.NULLCAD;

	// Token: 0x040000AC RID: 172 RVA: 0x0002A4F0 File Offset: 0x0002A4F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEActionDataDescriptor ?A0x0c12843d.NULLADD;

	// Token: 0x040000AD RID: 173 RVA: 0x00010AC0 File Offset: 0x00010AC0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x040000AE RID: 174 RVA: 0x00010AB0 File Offset: 0x00010AB0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x040000AF RID: 175
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x040000B0 RID: 176 RVA: 0x00010340 File Offset: 0x00010340
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000B1 RID: 177
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x040000B2 RID: 178 RVA: 0x00010358 File Offset: 0x00010358
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000B3 RID: 179 RVA: 0x00010AD0 File Offset: 0x00010AD0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x040000B4 RID: 180 RVA: 0x00010AE0 File Offset: 0x00010AE0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x040000B5 RID: 181
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x040000B6 RID: 182 RVA: 0x0002B24C File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x040000B7 RID: 183 RVA: 0x0002A50C File Offset: 0x0002A50C
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x040000B8 RID: 184 RVA: 0x0002B24F File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x040000B9 RID: 185 RVA: 0x0002B248 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '9460301'.
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x040000BA RID: 186
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x040000BB RID: 187 RVA: 0x0002B24E File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x040000BC RID: 188
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x040000BD RID: 189
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x040000BE RID: 190 RVA: 0x0002B24D File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x040000BF RID: 191
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x040000C0 RID: 192 RVA: 0x0002A508 File Offset: 0x0002A508
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x040000C1 RID: 193 RVA: 0x00010388 File Offset: 0x00010388
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x040000C2 RID: 194 RVA: 0x00010398 File Offset: 0x00010398
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x040000C3 RID: 195 RVA: 0x00010360 File Offset: 0x00010360
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000C4 RID: 196 RVA: 0x00010330 File Offset: 0x00010330
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x040000C5 RID: 197 RVA: 0x00010370 File Offset: 0x00010370
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x040000C6 RID: 198 RVA: 0x00010368 File Offset: 0x00010368
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000C7 RID: 199 RVA: 0x00010390 File Offset: 0x00010390
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x040000C8 RID: 200 RVA: 0x00010338 File Offset: 0x00010338
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000C9 RID: 201 RVA: 0x00010378 File Offset: 0x00010378
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x040000CA RID: 202 RVA: 0x00010350 File Offset: 0x00010350
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000CB RID: 203 RVA: 0x00010348 File Offset: 0x00010348
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000CC RID: 204 RVA: 0x0002A510 File Offset: 0x0002A510
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x040000CD RID: 205 RVA: 0x0002A520 File Offset: 0x0002A520
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x040000CE RID: 206 RVA: 0x00010AF0 File Offset: 0x00010AF0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x040000CF RID: 207 RVA: 0x00010AF8 File Offset: 0x00010AF8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x040000D0 RID: 208 RVA: 0x0002B3B8 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void>* ?A0x8e112e2a.__onexitbegin_m;

	// Token: 0x040000D1 RID: 209 RVA: 0x0002B3B0 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '12894362189'.
	internal static ulong ?A0x8e112e2a.__exit_list_size;

	// Token: 0x040000D2 RID: 210
	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitend_app_domain;

	// Token: 0x040000D3 RID: 211
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x040000D4 RID: 212
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x040000D5 RID: 213 RVA: 0x0002B3C0 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void>* ?A0x8e112e2a.__onexitend_m;

	// Token: 0x040000D6 RID: 214
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x040000D7 RID: 215
	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitbegin_app_domain;

	// Token: 0x040000D8 RID: 216 RVA: 0x00010C08 File Offset: 0x00010C08
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01Q6AXXZ ??_7type_info@@6B@;

	// Token: 0x040000D9 RID: 217 RVA: 0x00031A10 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEEngineServicesAPI ?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A;

	// Token: 0x040000DA RID: 218 RVA: 0x00031CB0 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEEngineClientAPI ?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A;

	// Token: 0x040000DB RID: 219 RVA: 0x00031C88 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEEngineRegisterAPI ?sm_RegistrationAPI@XE_API@@2UXEEngineRegisterAPI@@A;

	// Token: 0x040000DC RID: 220 RVA: 0x000102F8 File Offset: 0x000102F8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x040000DD RID: 221 RVA: 0x0002B3D0 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static __scrt_native_startup_state __scrt_current_native_startup_state;

	// Token: 0x040000DE RID: 222 RVA: 0x0002B3D8 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static void* __scrt_native_startup_lock;

	// Token: 0x040000DF RID: 223 RVA: 0x000102D0 File Offset: 0x000102D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x040000E0 RID: 224 RVA: 0x000102E8 File Offset: 0x000102E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x040000E1 RID: 225 RVA: 0x0002A550 File Offset: 0x0002A550
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '4294967295'.
	internal static uint __scrt_native_dllmain_reason;

	// Token: 0x040000E2 RID: 226 RVA: 0x000102E0 File Offset: 0x000102E0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;

	// Token: 0x040000E3 RID: 227 RVA: 0x0002A554 File Offset: 0x0002A554
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '-2147483648'.
	internal static int _Init_global_epoch;
}
