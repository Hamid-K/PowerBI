using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200007F RID: 127
	[Serializable]
	public sealed class SqlException : DbException
	{
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001FF2B File Offset: 0x0001E12B
		private SqlException(string message, SqlErrorCollection errorCollection, Exception innerException, Guid conId)
			: base(message, innerException)
		{
			base.HResult = -2146232060;
			this._errors = errorCollection;
			this._clientConnectionId = conId;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001FF5C File Offset: 0x0001E15C
		private SqlException(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{
			this._errors = (SqlErrorCollection)si.GetValue("Errors", typeof(SqlErrorCollection));
			base.HResult = -2146232060;
			foreach (SerializationEntry serializationEntry in si)
			{
				if ("ClientConnectionId" == serializationEntry.Name)
				{
					this._clientConnectionId = (Guid)si.GetValue("ClientConnectionId", typeof(Guid));
					return;
				}
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001FFF4 File Offset: 0x0001E1F4
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
			si.AddValue("Errors", null);
			si.AddValue("ClientConnectionId", this._clientConnectionId, typeof(object));
			for (int i = 0; i < this.Errors.Count; i++)
			{
				string text = "SqlError " + (i + 1).ToString();
				if (this.Data.Contains(text))
				{
					this.Data.Remove(text);
				}
				this.Data.Add(text, this.Errors[i].ToString());
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00020098 File Offset: 0x0001E298
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SqlErrorCollection Errors
		{
			get
			{
				return this._errors ?? new SqlErrorCollection();
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x000200A9 File Offset: 0x0001E2A9
		public Guid ClientConnectionId
		{
			get
			{
				return this._clientConnectionId;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x000200B1 File Offset: 0x0001E2B1
		public byte Class
		{
			get
			{
				if (this.Errors.Count <= 0)
				{
					return 0;
				}
				return this.Errors[0].Class;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000200D4 File Offset: 0x0001E2D4
		public int LineNumber
		{
			get
			{
				if (this.Errors.Count <= 0)
				{
					return 0;
				}
				return this.Errors[0].LineNumber;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x000200F7 File Offset: 0x0001E2F7
		public int Number
		{
			get
			{
				if (this.Errors.Count <= 0)
				{
					return 0;
				}
				return this.Errors[0].Number;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0002011A File Offset: 0x0001E31A
		public string Procedure
		{
			get
			{
				if (this.Errors.Count <= 0)
				{
					return null;
				}
				return this.Errors[0].Procedure;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0002013D File Offset: 0x0001E33D
		public string Server
		{
			get
			{
				if (this.Errors.Count <= 0)
				{
					return null;
				}
				return this.Errors[0].Server;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00020160 File Offset: 0x0001E360
		public byte State
		{
			get
			{
				if (this.Errors.Count <= 0)
				{
					return 0;
				}
				return this.Errors[0].State;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00020183 File Offset: 0x0001E383
		public override string Source
		{
			get
			{
				return "Framework Microsoft SqlClient Data Provider";
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002018C File Offset: 0x0001E38C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat(SQLMessage.ExClientConnectionId(), this._clientConnectionId);
			if (this.Errors.Count > 0 && this.Number != 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat(SQLMessage.ExErrorNumberStateClass(), this.Number, this.State, this.Class);
			}
			if (this.Data.Contains("OriginalClientConnectionId"))
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat(SQLMessage.ExOriginalClientConnectionId(), this.Data["OriginalClientConnectionId"]);
			}
			if (this.Data.Contains("RoutingDestination"))
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat(SQLMessage.ExRoutingDestination(), this.Data["RoutingDestination"]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002027D File Offset: 0x0001E47D
		internal static SqlException CreateException(SqlErrorCollection errorCollection, string serverVersion)
		{
			return SqlException.CreateException(errorCollection, serverVersion, Guid.Empty, null);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002028C File Offset: 0x0001E48C
		internal static SqlException CreateException(SqlErrorCollection errorCollection, string serverVersion, SqlInternalConnectionTds internalConnection, Exception innerException = null)
		{
			Guid guid = ((internalConnection == null) ? Guid.Empty : internalConnection._clientConnectionId);
			SqlException ex = SqlException.CreateException(errorCollection, serverVersion, guid, innerException);
			if (internalConnection != null)
			{
				if (internalConnection.OriginalClientConnectionId != Guid.Empty && internalConnection.OriginalClientConnectionId != internalConnection.ClientConnectionId)
				{
					ex.Data.Add("OriginalClientConnectionId", internalConnection.OriginalClientConnectionId);
				}
				if (!string.IsNullOrEmpty(internalConnection.RoutingDestination))
				{
					ex.Data.Add("RoutingDestination", internalConnection.RoutingDestination);
				}
			}
			return ex;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002031C File Offset: 0x0001E51C
		internal static SqlException CreateException(SqlErrorCollection errorCollection, string serverVersion, Guid conId, Exception innerException = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < errorCollection.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(errorCollection[i].Message);
			}
			if (innerException == null && errorCollection[0].Win32ErrorCode != 0 && errorCollection[0].Win32ErrorCode != -1)
			{
				innerException = new Win32Exception(errorCollection[0].Win32ErrorCode);
			}
			SqlException ex = new SqlException(stringBuilder.ToString(), errorCollection, innerException, conId);
			ex.Data.Add("HelpLink.ProdName", "Microsoft SQL Server");
			if (!string.IsNullOrEmpty(serverVersion))
			{
				ex.Data.Add("HelpLink.ProdVer", serverVersion);
			}
			ex.Data.Add("HelpLink.EvtSrc", "MSSQLServer");
			ex.Data.Add("HelpLink.EvtID", errorCollection[0].Number.ToString(CultureInfo.InvariantCulture));
			ex.Data.Add("HelpLink.BaseHelpUrl", "https://go.microsoft.com/fwlink");
			ex.Data.Add("HelpLink.LinkId", "20476");
			return ex;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002043C File Offset: 0x0001E63C
		internal SqlException InternalClone()
		{
			SqlException ex = new SqlException(this.Message, this._errors, base.InnerException, this._clientConnectionId);
			if (this.Data != null)
			{
				foreach (object obj in this.Data)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					ex.Data.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
			ex._doNotReconnect = this._doNotReconnect;
			return ex;
		}

		// Token: 0x040002A2 RID: 674
		private const string OriginalClientConnectionIdKey = "OriginalClientConnectionId";

		// Token: 0x040002A3 RID: 675
		private const string RoutingDestinationKey = "RoutingDestination";

		// Token: 0x040002A4 RID: 676
		private const int SqlExceptionHResult = -2146232060;

		// Token: 0x040002A5 RID: 677
		private readonly SqlErrorCollection _errors;

		// Token: 0x040002A6 RID: 678
		[OptionalField(VersionAdded = 4)]
		private Guid _clientConnectionId = Guid.Empty;

		// Token: 0x040002A7 RID: 679
		internal bool _doNotReconnect;
	}
}
