using System;
using System.Reflection;
using System.Security.Permissions;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000149 RID: 329
	internal sealed class SmiContextFactory
	{
		// Token: 0x060019A9 RID: 6569 RVA: 0x0006B498 File Offset: 0x00069698
		private SmiContextFactory()
		{
			if (InOutOfProcHelper.InProc)
			{
				Type type = Type.GetType("Microsoft.SqlServer.Server.InProcLink, SqlAccess, PublicKeyToken=89845dcd8080cc91");
				if (null == type)
				{
					throw SQL.ContextUnavailableOutOfProc();
				}
				FieldInfo staticField = this.GetStaticField(type, "Instance");
				if (!(staticField != null))
				{
					throw SQL.ContextUnavailableOutOfProc();
				}
				this._smiLink = (SmiLink)this.GetValue(staticField);
				FieldInfo staticField2 = this.GetStaticField(type, "BuildVersion");
				if (staticField2 != null)
				{
					uint num = (uint)this.GetValue(staticField2);
					this._majorVersion = (byte)(num >> 24);
					this._minorVersion = (byte)((num >> 16) & 255U);
					this._buildNum = (short)(num & 65535U);
					this._serverVersion = string.Format(null, "{0:00}.{1:00}.{2:0000}", this._majorVersion, (short)this._minorVersion, this._buildNum);
				}
				else
				{
					this._serverVersion = string.Empty;
				}
				this._negotiatedSmiVersion = this._smiLink.NegotiateVersion(210UL);
				bool flag = false;
				int num2 = 0;
				while (!flag && num2 < this.__supportedSmiVersions.Length)
				{
					if (this.__supportedSmiVersions[num2] == this._negotiatedSmiVersion)
					{
						flag = true;
					}
					num2++;
				}
				if (!flag)
				{
					this._smiLink = null;
				}
				this._eventSinkForGetCurrentContext = new SmiEventSink_Default();
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x0006B605 File Offset: 0x00069805
		internal ulong NegotiatedSmiVersion
		{
			get
			{
				if (this._smiLink == null)
				{
					throw SQL.ContextUnavailableOutOfProc();
				}
				return this._negotiatedSmiVersion;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0006B61B File Offset: 0x0006981B
		internal string ServerVersion
		{
			get
			{
				if (this._smiLink == null)
				{
					throw SQL.ContextUnavailableOutOfProc();
				}
				return this._serverVersion;
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0006B634 File Offset: 0x00069834
		internal SmiContext GetCurrentContext()
		{
			if (this._smiLink == null)
			{
				throw SQL.ContextUnavailableOutOfProc();
			}
			object currentContext = this._smiLink.GetCurrentContext(this._eventSinkForGetCurrentContext);
			this._eventSinkForGetCurrentContext.ProcessMessagesAndThrow();
			if (currentContext == null)
			{
				throw SQL.ContextUnavailableWhileInProc();
			}
			return (SmiContext)currentContext;
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0006B67C File Offset: 0x0006987C
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private object GetValue(FieldInfo fieldInfo)
		{
			return fieldInfo.GetValue(null);
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0006B694 File Offset: 0x00069894
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private FieldInfo GetStaticField(Type aType, string fieldName)
		{
			return aType.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField);
		}

		// Token: 0x040009D1 RID: 2513
		public static readonly SmiContextFactory Instance = new SmiContextFactory();

		// Token: 0x040009D2 RID: 2514
		private readonly SmiLink _smiLink;

		// Token: 0x040009D3 RID: 2515
		private readonly ulong _negotiatedSmiVersion;

		// Token: 0x040009D4 RID: 2516
		private readonly byte _majorVersion;

		// Token: 0x040009D5 RID: 2517
		private readonly byte _minorVersion;

		// Token: 0x040009D6 RID: 2518
		private readonly short _buildNum;

		// Token: 0x040009D7 RID: 2519
		private readonly string _serverVersion;

		// Token: 0x040009D8 RID: 2520
		private readonly SmiEventSink_Default _eventSinkForGetCurrentContext;

		// Token: 0x040009D9 RID: 2521
		internal const ulong Sql2005Version = 100UL;

		// Token: 0x040009DA RID: 2522
		internal const ulong Sql2008Version = 210UL;

		// Token: 0x040009DB RID: 2523
		internal const ulong LatestVersion = 210UL;

		// Token: 0x040009DC RID: 2524
		private readonly ulong[] __supportedSmiVersions = new ulong[] { 100UL, 210UL };

		// Token: 0x0200026B RID: 619
		internal enum ContextKey
		{
			// Token: 0x0400173A RID: 5946
			Connection,
			// Token: 0x0400173B RID: 5947
			SqlContext
		}
	}
}
