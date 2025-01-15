using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Internal
{
	// Token: 0x0200000C RID: 12
	internal static class XEventPackageRegistrar
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000047CC File Offset: 0x000047CC
		internal static bool IsAssemblyHandlerRegistered
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return XEventPackageRegistrar.sm_assemblyLoadHandlerRegistered;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000064A4 File Offset: 0x000064A4
		internal static void CheckNamingRules(string name)
		{
			char[] array = new char[] { ' ', '.' };
			if (string.IsNullOrWhiteSpace(name) || name.Length > 128 || name.IndexOfAny(array) > 0)
			{
				throw new XEventNamingException(name, 128);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000091A0 File Offset: 0x000091A0
		internal static void InstallAssemblyLoadHandler()
		{
			if (XEventPackageRegistrar.sm_IsXELoaded && !XEventPackageRegistrar.sm_assemblyLoadHandlerRegistered)
			{
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					try
					{
						if (Attribute.IsDefined(assembly, typeof(XEventPackageAttribute)))
						{
							XEventPackageRegistrar.RegisterAssembly(assembly);
						}
					}
					catch (FileNotFoundException ex)
					{
						Trace.TraceError("Unable to load dependent assembly '{0}' for assembly '{1}'. Assembly cannot be registered.", new object[] { ex.FileName, assembly.FullName });
					}
					catch (Exception ex2)
					{
						Trace.TraceError("Unable to load assembly. Assembly '{0}' cannot be registered. Error Message: '{1}'", new object[] { assembly.FullName, ex2.Message });
					}
				}
				AppDomain.CurrentDomain.AssemblyLoad += XEventPackageRegistrar.PackageAssemblyLoadEventHandler;
				XEventPackageRegistrar.sm_assemblyLoadHandlerRegistered = true;
			}
			if (!XEventPackageRegistrar.sm_IsXELoaded)
			{
				Trace.TraceError("XEEngine is not present, unable to register packages.");
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00009300 File Offset: 0x00009300
		unsafe static XEventPackageRegistrar()
		{
			if (<Module>.XE_API.IsEnginePresent() == null || calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)(*(<Module>.XE_API.ClientAPI() + 40L))) == null)
			{
				XEventPackageRegistrar.sm_IsXELoaded = ((calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ref <Module>.g_XEventAutoLoader, (IntPtr)(*<Module>.g_XEventAutoLoader)) != 0) ? 1 : 0) != 0;
			}
			if (XEventPackageRegistrar.sm_IsXELoaded)
			{
				<Module>.Microsoft.SqlServer.XEvent.Internal.XEventInteropPackageManager.StaticInit();
				XEventPackageRegistrar.InstallAssemblyLoadHandler();
			}
			else
			{
				XEventAutoEngineLoad.InitFailureReasons initFailureReasons;
				XEError* ptr = <Module>.XEventAutoEngineLoad.GetInitFailureReason(ref <Module>.g_XEventAutoLoader, ref initFailureReasons);
				if (initFailureReasons == (XEventAutoEngineLoad.InitFailureReasons)1)
				{
					Trace.TraceError("No suitable XEvent engine was found in the process.  Ensure that XE.dll is present and accessible. Package registration is disabled");
				}
				else
				{
					ushort num = *(ushort*)(ptr + 4L / (long)sizeof(XEError));
					Trace.TraceError(string.Format("The XEvent engine failed to start with error {0}.{1} state {2}.", *(ushort*)(ptr + 2L / (long)sizeof(XEError)), num, num));
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000911C File Offset: 0x0000911C
		internal static void PackageAssemblyLoadEventHandler(object sender, AssemblyLoadEventArgs args)
		{
			try
			{
				if (Attribute.IsDefined(args.LoadedAssembly, typeof(XEventPackageAttribute)))
				{
					XEventPackageRegistrar.RegisterAssembly(args.LoadedAssembly);
				}
			}
			catch (FileNotFoundException ex)
			{
				Trace.TraceError("Unable to load dependent assembly '{0}' for assembly '{1}'. Assembly cannot be registered.", new object[]
				{
					ex.FileName,
					args.LoadedAssembly.FullName
				});
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008F2C File Offset: 0x00008F2C
		internal unsafe static void RegisterAssembly(Assembly assembly)
		{
			XE_PackageEnumerator xe_PackageEnumerator;
			if (<Module>.XE_API.IsEnginePresent() != null || calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)(*(<Module>.XE_API.ClientAPI() + 40L))) != null)
			{
				XEventPackageAttribute xeventPackageAttribute = Attribute.GetCustomAttribute(assembly, typeof(XEventPackageAttribute)) as XEventPackageAttribute;
				if (!xeventPackageAttribute.IsRegistered)
				{
					<Module>.XE_PackageEnumerator.{ctor}(ref xe_PackageEnumerator);
					try
					{
						if (<Module>.XE_PackageEnumerator.Begin(ref xe_PackageEnumerator) == null)
						{
							goto IL_00E0;
						}
						XEPackageMetadata* ptr = null;
						_GUID guid;
						IntPtr intPtr = new IntPtr((void*)(&guid));
						Marshal.StructureToPtr(xeventPackageAttribute.PackageGuid, intPtr, false);
						if (<Module>.XE_PackageEnumerator.GetNextPackage(ref xe_PackageEnumerator, ref ptr) == null)
						{
							goto IL_00E0;
						}
						while (ptr != null)
						{
							if (<Module>.IsEqualGUID(*(long*)ptr + 32L, ref guid) != null && <Module>.IsEqualGUID(ref <Module>.XEMANAGED_MODULE_GUID, *(long*)ptr + 56L) != null)
							{
								<Module>.Microsoft.SqlServer.XEvent.Internal.XEventManagedPackage.LinkToCurrentAppDomain(*(long*)ptr + 160L, assembly);
								xeventPackageAttribute.IsRegistered = true;
								goto IL_00D4;
							}
							if (<Module>.XE_PackageEnumerator.GetNextPackage(ref xe_PackageEnumerator, ref ptr) == null)
							{
								break;
							}
						}
						goto IL_00E0;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(XE_PackageEnumerator.{dtor}), (void*)(&xe_PackageEnumerator));
						throw;
					}
					IL_00D4:
					<Module>.XE_PackageEnumerator.{dtor}(ref xe_PackageEnumerator);
					goto IL_0164;
					IL_00E0:
					try
					{
						if (calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(XEPackage modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),XEPackageAPI modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),XETargetAPI modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), <Module>.Microsoft.SqlServer.XEvent.Internal.XEventPackageSet.Create(xeventPackageAttribute, assembly), ref <Module>.g_ipackage, ref <Module>.g_itarget, (IntPtr)(*(<Module>.XE_API.RegistrationAPI() + 16L))) == null)
						{
							goto IL_0125;
						}
						xeventPackageAttribute.IsRegistered = true;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(XE_PackageEnumerator.{dtor}), (void*)(&xe_PackageEnumerator));
						throw;
					}
					<Module>.XE_PackageEnumerator.{dtor}(ref xe_PackageEnumerator);
					goto IL_0164;
					IL_0125:
					try
					{
						XEError* ptr2 = calli(XEError modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)(*(<Module>.XE_API.ClientAPI() + 384L)));
						throw new XEventPackageRegistrationFailure(xeventPackageAttribute.Name, (int)(*(ushort*)(ptr2 + 2L / (long)sizeof(XEError))), (int)(*(ushort*)(ptr2 + 4L / (long)sizeof(XEError))), (int)(*(ushort*)(ptr2 + 6L / (long)sizeof(XEError))));
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(XE_PackageEnumerator.{dtor}), (void*)(&xe_PackageEnumerator));
						throw;
					}
				}
			}
			IL_0164:
			try
			{
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(XE_PackageEnumerator.{dtor}), (void*)(&xe_PackageEnumerator));
				throw;
			}
		}

		// Token: 0x040000F6 RID: 246
		private static bool sm_assemblyLoadHandlerRegistered = false;

		// Token: 0x040000F7 RID: 247
		private static bool sm_IsXELoaded = true;
	}
}
