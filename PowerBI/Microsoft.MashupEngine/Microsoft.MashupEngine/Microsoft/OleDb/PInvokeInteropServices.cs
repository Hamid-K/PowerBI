using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.OleDb.PInvokeInterop;
using Microsoft.OleDb.PInvokeInterop.Test;

namespace Microsoft.OleDb
{
	// Token: 0x02001F1A RID: 7962
	public class PInvokeInteropServices : IInteropServices
	{
		// Token: 0x0600C2F4 RID: 49908 RVA: 0x00271598 File Offset: 0x0026F798
		public unsafe PInvokeInteropServices(IntPtr getErrorInfo, IntPtr setErrorInfo, KeyValuePair<Guid, IInterfaceTypeInfo>[] guidToTypeInfos = null)
		{
			this.getVTableCallback = (Guid iid, int size, IntPtr* vtable, int includeSupportsInterface) => this.GetVTable(iid, size, vtable, includeSupportsInterface != 0);
			this.releaseCallback = delegate(IntPtr objHandle)
			{
				MarshalledObjectHandle.FromIntPtr(objHandle).Release();
			};
			this.interopData.getVtableCallback = Marshal.GetFunctionPointerForDelegate<PInvokeInteropServices.GetVTableCallback>(this.getVTableCallback);
			this.interopData.releaseCallback = Marshal.GetFunctionPointerForDelegate<PInvokeInteropServices.ReleaseCallback>(this.releaseCallback);
			this.interopData.getErrorInfo = getErrorInfo;
			this.interopData.setErrorInfo = setErrorInfo;
			if (guidToTypeInfos != null)
			{
				foreach (KeyValuePair<Guid, IInterfaceTypeInfo> keyValuePair in guidToTypeInfos)
				{
					this.guidToTypeInfo.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600C2F5 RID: 49909 RVA: 0x002717E5 File Offset: 0x0026F9E5
		public int AggregateDataSource(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			return this.AggregateObject(new PInvokeInteropServices.AggregateObjectDelegate(PInvokeInteropServices.Imports.PInvokeAggregateDataSource), punkOuter, obj, ref iid, out ppv);
		}

		// Token: 0x0600C2F6 RID: 49910 RVA: 0x002717FE File Offset: 0x0026F9FE
		public int AggregateSession(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			return this.AggregateObject(new PInvokeInteropServices.AggregateObjectDelegate(PInvokeInteropServices.Imports.PInvokeAggregateSession), punkOuter, obj, ref iid, out ppv);
		}

		// Token: 0x0600C2F7 RID: 49911 RVA: 0x00271817 File Offset: 0x0026FA17
		public int AggregateCommand(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			return this.AggregateObject(new PInvokeInteropServices.AggregateObjectDelegate(PInvokeInteropServices.Imports.PInvokeAggregateCommand), punkOuter, obj, ref iid, out ppv);
		}

		// Token: 0x0600C2F8 RID: 49912 RVA: 0x00271830 File Offset: 0x0026FA30
		public int AggregateRowset(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			return this.AggregateObject(new PInvokeInteropServices.AggregateObjectDelegate(PInvokeInteropServices.Imports.PInvokeAggregateRowset), punkOuter, obj, ref iid, out ppv);
		}

		// Token: 0x0600C2F9 RID: 49913 RVA: 0x00271849 File Offset: 0x0026FA49
		public int AggregateMultipleResults(IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			return this.AggregateObject(new PInvokeInteropServices.AggregateObjectDelegate(PInvokeInteropServices.Imports.PInvokeAggregateMultipleResults), punkOuter, obj, ref iid, out ppv);
		}

		// Token: 0x0600C2FA RID: 49914 RVA: 0x00271864 File Offset: 0x0026FA64
		public int QueryInterface(object obj, ref Guid iid, out IntPtr ppv)
		{
			IntPtr iunknownForObject = this.GetIUnknownForObject(obj);
			int num = PInvokeInteropServices.Imports.PInvokeQueryInterface(iunknownForObject, ref iid, out ppv);
			this.Release(iunknownForObject);
			return num;
		}

		// Token: 0x0600C2FB RID: 49915 RVA: 0x00271889 File Offset: 0x0026FA89
		public int QueryInterface(IntPtr pUnknown, ref Guid iid, out IntPtr ppv)
		{
			return PInvokeInteropServices.Imports.PInvokeQueryInterface(pUnknown, ref iid, out ppv);
		}

		// Token: 0x0600C2FC RID: 49916 RVA: 0x00271893 File Offset: 0x0026FA93
		public int AddRef(IntPtr pUnknown)
		{
			if (pUnknown == IntPtr.Zero)
			{
				throw new ArgumentNullException();
			}
			return PInvokeInteropServices.Imports.PInvokeAddRef(pUnknown);
		}

		// Token: 0x0600C2FD RID: 49917 RVA: 0x002718AE File Offset: 0x0026FAAE
		public int Release(IntPtr pUnknown)
		{
			if (pUnknown == IntPtr.Zero)
			{
				throw new ArgumentNullException();
			}
			return PInvokeInteropServices.Imports.PInvokeRelease(pUnknown);
		}

		// Token: 0x0600C2FE RID: 49918 RVA: 0x002718CC File Offset: 0x0026FACC
		public int ReleaseComObject(object obj)
		{
			IntPtr iunknownForObject = this.GetIUnknownForObject(obj);
			this.Release(iunknownForObject);
			return this.Release(iunknownForObject);
		}

		// Token: 0x0600C2FF RID: 49919 RVA: 0x002718F0 File Offset: 0x0026FAF0
		public int GetErrorInfo(uint dwReserved, out IntPtr errorInfoUnmanaged)
		{
			return PInvokeInteropServices.Imports.PInvokeGetErrorInfo(this.interopData.getErrorInfo, 0U, out errorInfoUnmanaged);
		}

		// Token: 0x0600C300 RID: 49920 RVA: 0x00271904 File Offset: 0x0026FB04
		public int GetErrorInfo(uint dwReserved, out IErrorInfo errorInfoManaged)
		{
			IntPtr intPtr;
			int errorInfo = this.GetErrorInfo(dwReserved, out intPtr);
			if (errorInfo != 0)
			{
				errorInfoManaged = null;
				return errorInfo;
			}
			int num;
			try
			{
				errorInfoManaged = (IErrorInfo)PInvokeInteropServices.GetObjectForIUnknown(intPtr);
				this.AddRef(((IManagedInterop)errorInfoManaged).PUnknown);
				num = 0;
			}
			catch (NotImplementedException)
			{
				PInvokeInteropServices.Imports.PInvokeSetErrorInfo(this.interopData.setErrorInfo, 0U, intPtr);
				errorInfoManaged = null;
				num = -2147467263;
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				errorInfoManaged = null;
				num = Marshal.GetHRForException(ex);
			}
			finally
			{
				this.Release(intPtr);
			}
			return num;
		}

		// Token: 0x0600C301 RID: 49921 RVA: 0x002719B0 File Offset: 0x0026FBB0
		public int SetErrorInfo(IErrorInfo errorInfo)
		{
			IManagedInterop managedInterop = errorInfo as IManagedInterop;
			if (managedInterop != null && managedInterop.PUnknown != IntPtr.Zero)
			{
				return PInvokeInteropServices.Imports.PInvokeSetErrorInfo(this.interopData.setErrorInfo, 0U, managedInterop.PUnknown);
			}
			MarshalledObjectHandle marshalledObjectHandle;
			IntPtr intPtr;
			int num = PInvokeInteropServices.Imports.PInvokeWrapSetErrorInfo(0U, this.GetInteropData(errorInfo, out marshalledObjectHandle), out intPtr);
			if (num == 0)
			{
				marshalledObjectHandle.AttachPUnknown(intPtr);
			}
			return num;
		}

		// Token: 0x0600C302 RID: 49922 RVA: 0x00271A0C File Offset: 0x0026FC0C
		public IntPtr GetIUnknownForObject(object obj)
		{
			IManagedInterop managedInterop = obj as IManagedInterop;
			if (managedInterop != null)
			{
				IntPtr punknown = managedInterop.PUnknown;
				this.AddRef(punknown);
				return punknown;
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600C303 RID: 49923 RVA: 0x00271A39 File Offset: 0x0026FC39
		internal static void CleanupManagedRefs(IntPtr pUnknown)
		{
			PInvokeInteropServices.Imports.PInvokeCleanupManagedRefs(pUnknown);
		}

		// Token: 0x0600C304 RID: 49924 RVA: 0x00271A42 File Offset: 0x0026FC42
		internal static ITestAccessorPInvokeInteropServices GetTestAccessor()
		{
			return new PInvokeInteropServices.TestAccessorPInvokeInteropServices();
		}

		// Token: 0x0600C305 RID: 49925 RVA: 0x00271A4C File Offset: 0x0026FC4C
		private static object GetObjectForIUnknown(IntPtr pUnknown)
		{
			IntPtr zero = IntPtr.Zero;
			if (PInvokeInteropServices.Imports.PInvokeGetManagedObjectHandle(pUnknown, out zero) != 0)
			{
				throw new NotImplementedException();
			}
			return MarshalledObjectHandle.FromIntPtr<object>(zero);
		}

		// Token: 0x0600C306 RID: 49926 RVA: 0x00271A75 File Offset: 0x0026FC75
		private MarshalledInteropData GetInteropData(object obj, out MarshalledObjectHandle marshalledObjectHandle)
		{
			marshalledObjectHandle = new MarshalledObjectHandle(this, obj);
			return new MarshalledInteropData(marshalledObjectHandle.ToIntPtr(), this.interopData);
		}

		// Token: 0x0600C307 RID: 49927 RVA: 0x00271A94 File Offset: 0x0026FC94
		private unsafe int GetVTable(Guid iid, int size, IntPtr* vtable, bool includeSupportsInterfaceCallback)
		{
			IInterfaceTypeInfo interfaceTypeInfo;
			if (this.guidToTypeInfo.TryGetValue(iid, out interfaceTypeInfo))
			{
				return interfaceTypeInfo.GetVTable(size, vtable, includeSupportsInterfaceCallback);
			}
			return -2147467262;
		}

		// Token: 0x0600C308 RID: 49928 RVA: 0x00271AC4 File Offset: 0x0026FCC4
		private int AggregateObject(PInvokeInteropServices.AggregateObjectDelegate aggregateObject, IntPtr punkOuter, object obj, ref Guid iid, out IntPtr ppv)
		{
			if (punkOuter != IntPtr.Zero && iid != IID.IUnknown)
			{
				ppv = IntPtr.Zero;
				return -2147217886;
			}
			MarshalledObjectHandle marshalledObjectHandle;
			int num = aggregateObject(punkOuter, this.GetInteropData(obj, out marshalledObjectHandle), ref iid, out ppv);
			if (num == 0)
			{
				marshalledObjectHandle.AttachPUnknown(ppv);
			}
			return num;
		}

		// Token: 0x0600C309 RID: 49929 RVA: 0x00271B20 File Offset: 0x0026FD20
		private static MarshalledObjectHandle GetMarshalledObjectHandle(IntPtr pUnknown)
		{
			IntPtr zero = IntPtr.Zero;
			PInvokeInteropServices.Imports.PInvokeGetManagedObjectHandle(pUnknown, out zero);
			return MarshalledObjectHandle.FromIntPtr(zero);
		}

		// Token: 0x04006468 RID: 25704
		private readonly PInvokeInteropServices.GetVTableCallback getVTableCallback;

		// Token: 0x04006469 RID: 25705
		private readonly PInvokeInteropServices.ReleaseCallback releaseCallback;

		// Token: 0x0400646A RID: 25706
		private readonly MarshalledInteropData interopData;

		// Token: 0x0400646B RID: 25707
		private readonly Dictionary<Guid, IInterfaceTypeInfo> guidToTypeInfo = new Dictionary<Guid, IInterfaceTypeInfo>
		{
			{
				IID.IDBCreateSession,
				new DBCreateSessionTypeInfo()
			},
			{
				IID.IDBInfo,
				new DBInfoTypeInfo()
			},
			{
				IID.IDBInitialize,
				new DBInitializeTypeInfo()
			},
			{
				IID.IDBProperties,
				new DBPropertiesTypeInfo()
			},
			{
				IID.IPersist,
				new PersistTypeInfo()
			},
			{
				IID.ISupportErrorInfo,
				new SupportErrorInfoTypeInfo()
			},
			{
				IID.IDBCreateCommand,
				new DBCreateCommandTypeInfo()
			},
			{
				IID.IDBSchemaRowset,
				new DBSchemaRowsetTypeInfo()
			},
			{
				IID.IGetDataSource,
				new GetDataSourceTypeInfo()
			},
			{
				IID.IOpenRowset,
				new OpenRowsetTypeInfo()
			},
			{
				IID.ISessionProperties,
				new SessionPropertiesTypeInfo()
			},
			{
				IID.IAccessor,
				new AccessorTypeInfo()
			},
			{
				IID.IColumnsInfo,
				new ColumnsInfoTypeInfo()
			},
			{
				IID.ICommand,
				new CommandTypeInfo()
			},
			{
				IID.ICommandProperties,
				new CommandPropertiesTypeInfo()
			},
			{
				IID.ICommandText,
				new CommandTextTypeInfo()
			},
			{
				IID.IConvertType,
				new ConvertTypeTypeInfo()
			},
			{
				IID.IDBAsynchStatus,
				new DBAsynchStatusTypeInfo()
			},
			{
				IID.IRowset,
				new RowsetTypeInfo()
			},
			{
				IID.IRowsetInfo,
				new RowsetInfoTypeInfo()
			},
			{
				IID.IStructuredEntityRowset,
				new StructuredEntityRowsetTypeInfo()
			},
			{
				IID.IMultipleResults,
				new MultipleResultsTypeInfo()
			},
			{
				IID.IErrorInfo,
				new ErrorInfoTypeInfo()
			},
			{
				IID.IErrorReported,
				new ErrorReportedTypeInfo()
			}
		};

		// Token: 0x02001F1B RID: 7963
		// (Invoke) Token: 0x0600C30C RID: 49932
		private unsafe delegate int GetVTableCallback(Guid iid, int size, IntPtr* vtable, int includeSupportsInterfaceCallback);

		// Token: 0x02001F1C RID: 7964
		// (Invoke) Token: 0x0600C310 RID: 49936
		private delegate void ReleaseCallback(IntPtr objHandle);

		// Token: 0x02001F1D RID: 7965
		// (Invoke) Token: 0x0600C314 RID: 49940
		private delegate int AggregateObjectDelegate(IntPtr punkOuter, MarshalledInteropData marshalledInteropData, ref Guid iid, out IntPtr ppv);

		// Token: 0x02001F1E RID: 7966
		private class TestAccessorPInvokeInteropServices : ITestAccessorPInvokeInteropServices
		{
			// Token: 0x0600C317 RID: 49943 RVA: 0x00271B52 File Offset: 0x0026FD52
			public int Initialize(IntPtr pUnknown)
			{
				return PInvokeInteropServices.TestAccessorPInvokeInteropServices.PInvokeTestIDBInitialize(pUnknown, 1);
			}

			// Token: 0x0600C318 RID: 49944 RVA: 0x00271B5B File Offset: 0x0026FD5B
			public int Uninitialize(IntPtr pUnknown)
			{
				return PInvokeInteropServices.TestAccessorPInvokeInteropServices.PInvokeTestIDBInitialize(pUnknown, 0);
			}

			// Token: 0x0600C319 RID: 49945 RVA: 0x00271B64 File Offset: 0x0026FD64
			public MarshalledObjectHandle GetMarshalledObjectHandle(IntPtr pUnknown)
			{
				return PInvokeInteropServices.GetMarshalledObjectHandle(pUnknown);
			}

			// Token: 0x0600C31A RID: 49946 RVA: 0x00271B6C File Offset: 0x0026FD6C
			public object GetObjectForIUnknown(IntPtr pUnknown)
			{
				return PInvokeInteropServices.GetObjectForIUnknown(pUnknown);
			}

			// Token: 0x0600C31B RID: 49947
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			private static extern int PInvokeTestIDBInitialize(IntPtr punk, int initialize);
		}

		// Token: 0x02001F1F RID: 7967
		private static class Imports
		{
			// Token: 0x0600C31D RID: 49949
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeAggregateDataSource(IntPtr punkOuter, MarshalledInteropData marshalledInteropData, ref Guid iid, out IntPtr ppv);

			// Token: 0x0600C31E RID: 49950
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeAggregateSession(IntPtr punkOuter, MarshalledInteropData marshalledInteropData, ref Guid iid, out IntPtr ppv);

			// Token: 0x0600C31F RID: 49951
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeAggregateCommand(IntPtr punkOuter, MarshalledInteropData marshalledInteropData, ref Guid iid, out IntPtr ppv);

			// Token: 0x0600C320 RID: 49952
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeAggregateRowset(IntPtr punkOuter, MarshalledInteropData marshalledInteropData, ref Guid iid, out IntPtr ppv);

			// Token: 0x0600C321 RID: 49953
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeAggregateMultipleResults(IntPtr punkOuter, MarshalledInteropData marshalledInteropData, ref Guid iid, out IntPtr ppv);

			// Token: 0x0600C322 RID: 49954
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeWrapSetErrorInfo(uint dwReserved, MarshalledInteropData marshalledInteropData, out IntPtr pErrorInfo);

			// Token: 0x0600C323 RID: 49955
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeSetErrorInfo(IntPtr pfnSetErrorInfo, uint dwReserved, IntPtr pErrorInfo);

			// Token: 0x0600C324 RID: 49956
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeGetErrorInfo(IntPtr pfnGetErrorInfo, uint dwReserved, out IntPtr ppv);

			// Token: 0x0600C325 RID: 49957
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeCleanupManagedRefs(IntPtr punk);

			// Token: 0x0600C326 RID: 49958
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeQueryInterface(IntPtr punk, ref Guid iid, out IntPtr ppunk);

			// Token: 0x0600C327 RID: 49959
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeRelease(IntPtr punk);

			// Token: 0x0600C328 RID: 49960
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeAddRef(IntPtr punk);

			// Token: 0x0600C329 RID: 49961
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			public static extern int PInvokeGetManagedObjectHandle(IntPtr punk, out IntPtr managedObject);
		}
	}
}
