using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x020006E2 RID: 1762
	[Serializable]
	internal sealed class OdbcException : DbException
	{
		// Token: 0x060034F0 RID: 13552 RVA: 0x000AA84D File Offset: 0x000A8A4D
		public OdbcException(Odbc32.RetCode retcode, IList<OdbcError> errors)
		{
			this.retcode = retcode;
			this.odbcErrors = errors;
			base.HResult = -2146232009;
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000AA870 File Offset: 0x000A8A70
		private OdbcException(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{
			this.odbcErrors = (IList<OdbcError>)si.GetValue("odbcErrors", typeof(ICollection<OdbcError>));
			this.retcode = (Odbc32.RetCode)si.GetValue("retcode", typeof(Odbc32.RetCode));
			base.HResult = -2146232009;
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x060034F2 RID: 13554 RVA: 0x000AA8D0 File Offset: 0x000A8AD0
		public Odbc32.RetCode ReturnCode
		{
			get
			{
				return this.retcode;
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x000AA8D8 File Offset: 0x000A8AD8
		public IList<OdbcError> Errors
		{
			get
			{
				return this.odbcErrors;
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x060034F4 RID: 13556 RVA: 0x000AA8E0 File Offset: 0x000A8AE0
		public bool IsSafe
		{
			get
			{
				foreach (OdbcError odbcError in this.Errors)
				{
					if (OdbcException.unsafeSQLGetInfoErrorCodes.Contains(odbcError.SQLState))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x000AA940 File Offset: 0x000A8B40
		public bool IsNonTransient
		{
			get
			{
				foreach (OdbcError odbcError in this.Errors)
				{
					if (!SQLStates.NonTransientErrors.Contains(odbcError.SQLState))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x060034F6 RID: 13558 RVA: 0x000AA9A0 File Offset: 0x000A8BA0
		public override string Message
		{
			get
			{
				if (this.message == null)
				{
					if (this.Errors.Count > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (OdbcError odbcError in this.Errors)
						{
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append(Environment.NewLine);
							}
							stringBuilder.Append(odbcError.ToString(this.retcode));
						}
						this.message = stringBuilder.ToString();
					}
					else
					{
						this.message = Strings.OdbcErrorWithNoDiagnosticRecord;
					}
				}
				return this.message;
			}
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000AAA54 File Offset: 0x000A8C54
		public bool HasState(string sqlState)
		{
			using (IEnumerator<OdbcError> enumerator = this.odbcErrors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.SQLState == sqlState)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000AAAB0 File Offset: 0x000A8CB0
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			if (si == null)
			{
				throw new ArgumentNullException("si");
			}
			si.AddValue("odbcErrors", this.odbcErrors, typeof(IList<OdbcError>));
			si.AddValue("retcode", this.retcode, typeof(Odbc32.RetCode));
			base.GetObjectData(si, context);
		}

		// Token: 0x04001B6C RID: 7020
		private static HashSet<string> unsafeSQLGetInfoErrorCodes = new HashSet<string> { "08003", "08S01", "22003", "HY001", "HY013", "HY090", "HYT01" };

		// Token: 0x04001B6D RID: 7021
		private const int hresult = -2146232009;

		// Token: 0x04001B6E RID: 7022
		private readonly Odbc32.RetCode retcode;

		// Token: 0x04001B6F RID: 7023
		private readonly IList<OdbcError> odbcErrors;

		// Token: 0x04001B70 RID: 7024
		private string message;
	}
}
