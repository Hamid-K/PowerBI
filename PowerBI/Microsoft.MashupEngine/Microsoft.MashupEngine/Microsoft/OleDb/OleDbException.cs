using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.OleDb
{
	// Token: 0x02001F17 RID: 7959
	[Serializable]
	public class OleDbException : DbException
	{
		// Token: 0x0600C2E7 RID: 49895 RVA: 0x002712C0 File Offset: 0x0026F4C0
		public OleDbException(int errorCode, string message, string source, IList<OleDbError> errors = null)
			: base(message, errorCode)
		{
			this.source = source;
			this.errors = new ReadOnlyCollection<OleDbError>(errors ?? new OleDbError[0]);
		}

		// Token: 0x0600C2E8 RID: 49896 RVA: 0x002712E8 File Offset: 0x0026F4E8
		protected OleDbException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.source = info.GetString("Source");
			this.errors = (ReadOnlyCollection<OleDbError>)info.GetValue("Errors", typeof(ReadOnlyCollection<OleDbError>));
		}

		// Token: 0x17002FA5 RID: 12197
		// (get) Token: 0x0600C2E9 RID: 49897 RVA: 0x00271323 File Offset: 0x0026F523
		public override string Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17002FA6 RID: 12198
		// (get) Token: 0x0600C2EA RID: 49898 RVA: 0x0027132B File Offset: 0x0026F52B
		public ICollection<OleDbError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0600C2EB RID: 49899 RVA: 0x00271333 File Offset: 0x0026F533
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Source", this.source);
			info.AddValue("Errors", this.errors, typeof(ReadOnlyCollection<OleDbError>));
		}

		// Token: 0x0600C2EC RID: 49900 RVA: 0x00271369 File Offset: 0x0026F569
		public static void ThrowExceptionForHR(int errorCode, object obj, Type comInterface, IOleDbCustomErrorHandler customHandler = null)
		{
			if (errorCode >= 0)
			{
				return;
			}
			OleDbException.ThrowExceptionWithErrorInfo(errorCode, obj as ISupportErrorInfo, comInterface, customHandler);
			throw Marshal.GetExceptionForHR(errorCode);
		}

		// Token: 0x0600C2ED RID: 49901 RVA: 0x00271384 File Offset: 0x0026F584
		private static void ThrowExceptionWithErrorInfo(int errorCode, ISupportErrorInfo supportErrorInfo, Type comInterface, IOleDbCustomErrorHandler customHandler)
		{
			if (customHandler != null && supportErrorInfo != null)
			{
				Guid guid = comInterface.GUID;
				if (supportErrorInfo.InterfaceSupportsErrorInfo(ref guid) == 0)
				{
					IErrorInfo errorInfo = ComInteropServices.GetErrorInfo();
					if (errorInfo != null)
					{
						object obj = new OleDbException.ExceptionBuilder(customHandler).Build(errorCode, errorInfo);
						Marshal.ReleaseComObject(errorInfo);
						throw obj;
					}
				}
			}
		}

		// Token: 0x04006461 RID: 25697
		private const string sourceTag = "Source";

		// Token: 0x04006462 RID: 25698
		private const string errorsTag = "Errors";

		// Token: 0x04006463 RID: 25699
		private readonly string source;

		// Token: 0x04006464 RID: 25700
		private readonly ReadOnlyCollection<OleDbError> errors;

		// Token: 0x02001F18 RID: 7960
		private class ExceptionBuilder
		{
			// Token: 0x0600C2EE RID: 49902 RVA: 0x002713C6 File Offset: 0x0026F5C6
			public ExceptionBuilder(IOleDbCustomErrorHandler customHandler)
			{
				this.defaultLcid = OleDbException.NativeMethods.GetUserDefaultLCID();
				this.currentLcid = CultureInfo.CurrentCulture.LCID;
				this.customHandler = customHandler;
			}

			// Token: 0x0600C2EF RID: 49903 RVA: 0x002713F0 File Offset: 0x0026F5F0
			public OleDbException Build(int errorCode, IErrorInfo errorInfo)
			{
				string text;
				errorInfo.GetDescription(out text);
				string text2;
				errorInfo.GetSource(out text2);
				if (text == null)
				{
					text = Strings.DefaultOleDbExceptionMessage(errorCode);
				}
				List<OleDbError> list = null;
				IErrorRecords errorRecords = errorInfo as IErrorRecords;
				if (errorRecords != null)
				{
					list = this.GetErrors(errorRecords);
				}
				return new OleDbException(errorCode, text, text2, list);
			}

			// Token: 0x0600C2F0 RID: 49904 RVA: 0x0027143C File Offset: 0x0026F63C
			private List<OleDbError> GetErrors(IErrorRecords errorRecords)
			{
				uint num;
				if (errorRecords.GetRecordCount(out num) < 0 || num > 2147483647U)
				{
					return null;
				}
				List<OleDbError> list = new List<OleDbError>((int)num);
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					OleDbError oleDbError = this.MakeError(errorRecords, num2);
					if (oleDbError != null)
					{
						list.Add(oleDbError);
					}
				}
				return list;
			}

			// Token: 0x0600C2F1 RID: 49905 RVA: 0x00271488 File Offset: 0x0026F688
			private OleDbError MakeError(IErrorRecords errorRecords, uint offset)
			{
				string text = null;
				string text2 = null;
				IErrorInfo errorInfo = null;
				errorRecords.GetErrorInfo(offset, this.currentLcid, out errorInfo);
				if (errorInfo != null && errorInfo.GetDescription(out text) == -2147217855)
				{
					Marshal.ReleaseComObject(errorInfo);
					errorRecords.GetErrorInfo(offset, this.defaultLcid, out errorInfo);
					if (errorInfo != null)
					{
						errorInfo.GetDescription(out text);
					}
				}
				if (errorInfo != null && errorInfo.GetSource(out text2) == -2147217855)
				{
					Marshal.ReleaseComObject(errorInfo);
					errorRecords.GetErrorInfo(offset, this.defaultLcid, out errorInfo);
					if (errorInfo != null)
					{
						errorInfo.GetSource(out text2);
					}
				}
				OleDbError oleDbError = null;
				if (this.customHandler != null)
				{
					IntPtr zero = IntPtr.Zero;
					Guid interfaceID = this.customHandler.InterfaceID;
					if (errorRecords.GetCustomErrorObject(offset, ref interfaceID, out zero) >= 0 && zero != IntPtr.Zero)
					{
						object objectForIUnknown = Marshal.GetObjectForIUnknown(zero);
						Marshal.Release(zero);
						oleDbError = this.customHandler.GetError(text2, text, objectForIUnknown);
						Marshal.ReleaseComObject(objectForIUnknown);
					}
				}
				if (errorInfo != null)
				{
					Marshal.ReleaseComObject(errorInfo);
				}
				if (text == null && oleDbError == null)
				{
					return null;
				}
				return oleDbError ?? new OleDbError(text2, text, null, -1);
			}

			// Token: 0x04006465 RID: 25701
			private readonly int currentLcid;

			// Token: 0x04006466 RID: 25702
			private readonly int defaultLcid;

			// Token: 0x04006467 RID: 25703
			private readonly IOleDbCustomErrorHandler customHandler;
		}

		// Token: 0x02001F19 RID: 7961
		private class NativeMethods
		{
			// Token: 0x0600C2F2 RID: 49906
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern int GetUserDefaultLCID();
		}
	}
}
