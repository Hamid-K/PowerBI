using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000039 RID: 57
	public abstract class DataSource : IPersist, IDBInitialize, IDBCreateSession, IDBProperties, IDBInfo, ISupportErrorInfo
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001F4 RID: 500
		public abstract Guid ClassID { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001F5 RID: 501
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract IDBProperties Properties
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001F6 RID: 502
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract IDBInfo DbInfo
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x060001F7 RID: 503
		public abstract void Initialize();

		// Token: 0x060001F8 RID: 504
		public abstract void Uninitialize();

		// Token: 0x060001F9 RID: 505
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public abstract Session CreateSession();

		// Token: 0x060001FA RID: 506 RVA: 0x00006197 File Offset: 0x00004397
		void IPersist.GetClassID(out Guid clsid)
		{
			clsid = this.ClassID;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000061A5 File Offset: 0x000043A5
		unsafe int IDBProperties.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			return this.Properties.GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000061B7 File Offset: 0x000043B7
		unsafe int IDBProperties.GetPropertyInfo(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertyInfoSets, out DBPROPINFOSET* nativePropertyInfoSets, char** descriptions)
		{
			return this.Properties.GetPropertyInfo(countPropertyIDSets, nativePropertyIDSets, out countPropertyInfoSets, out nativePropertyInfoSets, descriptions);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000061CB File Offset: 0x000043CB
		unsafe int IDBProperties.SetProperties(uint countPropertySets, DBPROPSET* nativePropertySets)
		{
			return this.Properties.SetProperties(countPropertySets, nativePropertySets);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000061DA File Offset: 0x000043DA
		unsafe void IDBInfo.GetKeywords(out char* keywords)
		{
			this.DbInfo.GetKeywords(out keywords);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000061E8 File Offset: 0x000043E8
		unsafe int IDBInfo.GetLiteralInfo(uint literalCount, DBLITERAL* nativeLiterals, out uint literalInfoCount, out DBLITERALINFO* nativeLiteralInfo, out char* strings)
		{
			return this.DbInfo.GetLiteralInfo(literalCount, nativeLiterals, out literalInfoCount, out nativeLiteralInfo, out strings);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000061FC File Offset: 0x000043FC
		int IDBCreateSession.CreateSession(IntPtr punkOuter, ref Guid iid, out IntPtr ppv)
		{
			Session session = this.CreateSession();
			return Aggregator.AggregateSession(punkOuter, session, ref iid, out ppv);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006219 File Offset: 0x00004419
		int ISupportErrorInfo.InterfaceSupportsErrorInfo(ref Guid iid)
		{
			if (iid == IID.IDBInitialize || iid == IID.IDBCreateSession)
			{
				return 0;
			}
			return 1;
		}
	}
}
