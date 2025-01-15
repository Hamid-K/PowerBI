using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E8E RID: 7822
	public abstract class DataSource : IPersist, IDBInitialize, IDBCreateSession, IDBProperties, IDBInfo, ISupportErrorInfo
	{
		// Token: 0x0600C152 RID: 49490 RVA: 0x0026E0AF File Offset: 0x0026C2AF
		protected DataSource(IInteropServices interopServices, IManagedDataConvert dataConvert)
		{
			this.interopServices = interopServices;
			this.dataConvert = dataConvert;
		}

		// Token: 0x17002F38 RID: 12088
		// (get) Token: 0x0600C153 RID: 49491
		public abstract Guid ClassID { get; }

		// Token: 0x17002F39 RID: 12089
		// (get) Token: 0x0600C154 RID: 49492
		public abstract IDBProperties Properties { get; }

		// Token: 0x17002F3A RID: 12090
		// (get) Token: 0x0600C155 RID: 49493
		public abstract IDBInfo DbInfo { get; }

		// Token: 0x0600C156 RID: 49494
		public abstract void Initialize();

		// Token: 0x0600C157 RID: 49495
		public abstract void Uninitialize();

		// Token: 0x0600C158 RID: 49496
		public abstract Session CreateSession();

		// Token: 0x17002F3B RID: 12091
		// (get) Token: 0x0600C159 RID: 49497 RVA: 0x0026E0C5 File Offset: 0x0026C2C5
		public IInteropServices InteropServices
		{
			get
			{
				return this.interopServices;
			}
		}

		// Token: 0x17002F3C RID: 12092
		// (get) Token: 0x0600C15A RID: 49498 RVA: 0x0026E0CD File Offset: 0x0026C2CD
		public IManagedDataConvert DataConvert
		{
			get
			{
				return this.dataConvert;
			}
		}

		// Token: 0x0600C15B RID: 49499 RVA: 0x0026E0D5 File Offset: 0x0026C2D5
		void IPersist.GetClassID(out Guid clsid)
		{
			clsid = this.ClassID;
		}

		// Token: 0x0600C15C RID: 49500 RVA: 0x0026E0E3 File Offset: 0x0026C2E3
		unsafe int IDBProperties.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			return this.Properties.GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets);
		}

		// Token: 0x0600C15D RID: 49501 RVA: 0x0026E0F5 File Offset: 0x0026C2F5
		unsafe int IDBProperties.GetPropertyInfo(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertyInfoSets, out DBPROPINFOSET* nativePropertyInfoSets, char** descriptions)
		{
			return this.Properties.GetPropertyInfo(countPropertyIDSets, nativePropertyIDSets, out countPropertyInfoSets, out nativePropertyInfoSets, descriptions);
		}

		// Token: 0x0600C15E RID: 49502 RVA: 0x0026E109 File Offset: 0x0026C309
		unsafe int IDBProperties.SetProperties(uint countPropertySets, DBPROPSET* nativePropertySets)
		{
			return this.Properties.SetProperties(countPropertySets, nativePropertySets);
		}

		// Token: 0x0600C15F RID: 49503 RVA: 0x0026E118 File Offset: 0x0026C318
		unsafe void IDBInfo.GetKeywords(out char* keywords)
		{
			this.DbInfo.GetKeywords(out keywords);
		}

		// Token: 0x0600C160 RID: 49504 RVA: 0x0026E126 File Offset: 0x0026C326
		unsafe int IDBInfo.GetLiteralInfo(uint cLiterals, DBLITERAL* nativeLiterals, out uint cLiteralInfo, out DBLITERALINFO* nativeLiteralInfo, out char* strings)
		{
			return this.DbInfo.GetLiteralInfo(cLiterals, nativeLiterals, out cLiteralInfo, out nativeLiteralInfo, out strings);
		}

		// Token: 0x0600C161 RID: 49505 RVA: 0x0026E13C File Offset: 0x0026C33C
		int IDBCreateSession.CreateSession(IntPtr punkOuter, ref Guid iid, out IntPtr ppv)
		{
			Session session = this.CreateSession();
			return this.InteropServices.AggregateSession(punkOuter, session, ref iid, out ppv);
		}

		// Token: 0x0600C162 RID: 49506 RVA: 0x0026E15F File Offset: 0x0026C35F
		int ISupportErrorInfo.InterfaceSupportsErrorInfo(ref Guid iid)
		{
			if (iid == IID.IDBInitialize || iid == IID.IDBCreateSession)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x04006191 RID: 24977
		private readonly IInteropServices interopServices;

		// Token: 0x04006192 RID: 24978
		private readonly IManagedDataConvert dataConvert;
	}
}
