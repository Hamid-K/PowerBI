using System;
using System.Data;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x02000016 RID: 22
	public class ConnectionWrapper : BaseDataWrapper, Microsoft.ReportingServices.DataProcessing.IDbConnection, IDisposable, IExtension
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00004D51 File Offset: 0x00002F51
		public ConnectionWrapper(global::System.Data.IDbConnection underlyingConnection)
			: base(underlyingConnection)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004D5A File Offset: 0x00002F5A
		public virtual void Open()
		{
			this.UnderlyingConnection.Open();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004D67 File Offset: 0x00002F67
		public virtual void Close()
		{
			this.UnderlyingConnection.Close();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004D74 File Offset: 0x00002F74
		public virtual Microsoft.ReportingServices.DataProcessing.IDbCommand CreateCommand()
		{
			return new CommandWrapper(this.UnderlyingConnection.CreateCommand());
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004D86 File Offset: 0x00002F86
		public virtual Microsoft.ReportingServices.DataProcessing.IDbTransaction BeginTransaction()
		{
			return new TransactionWrapper(this.UnderlyingConnection.BeginTransaction());
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004D98 File Offset: 0x00002F98
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004DA5 File Offset: 0x00002FA5
		public virtual string ConnectionString
		{
			get
			{
				return this.UnderlyingConnection.ConnectionString;
			}
			set
			{
				this.UnderlyingConnection.ConnectionString = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004DB3 File Offset: 0x00002FB3
		public virtual int ConnectionTimeout
		{
			get
			{
				return this.UnderlyingConnection.ConnectionTimeout;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004DC0 File Offset: 0x00002FC0
		public global::System.Data.IDbConnection UnderlyingConnection
		{
			get
			{
				RSTrace.DataExtensionTracer.Assert(base.UnderlyingObject != null, "If the underlying connection is not provided in the constructor it must be set before accessing it.");
				return (global::System.Data.IDbConnection)base.UnderlyingObject;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004DE5 File Offset: 0x00002FE5
		public virtual string LocalizedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public virtual void SetConfiguration(string configInfo)
		{
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004DEA File Offset: 0x00002FEA
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004DF2 File Offset: 0x00002FF2
		public bool WrappedManagedProvider
		{
			get
			{
				return this.m_wrappedManagedProvider;
			}
			internal set
			{
				this.m_wrappedManagedProvider = value;
			}
		}

		// Token: 0x04000084 RID: 132
		protected bool m_wrappedManagedProvider;
	}
}
