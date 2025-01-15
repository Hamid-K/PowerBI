using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public abstract class MashupSecurityException : MashupException
	{
		// Token: 0x0600033A RID: 826 RVA: 0x0000CE25 File Offset: 0x0000B025
		protected MashupSecurityException(string message)
			: base(message)
		{
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000CE2E File Offset: 0x0000B02E
		protected MashupSecurityException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000CE38 File Offset: 0x0000B038
		protected MashupSecurityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600033D RID: 829
		public abstract string Reason { get; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600033E RID: 830
		public abstract IEnumerable<DataSource> DataSources { get; }

		// Token: 0x0600033F RID: 831 RVA: 0x0000CE44 File Offset: 0x0000B044
		protected void InitData()
		{
			this.Data["Microsoft.Data.Mashup.MashupSecurityException.Reason"] = this.Reason;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = 1048576;
			javaScriptSerializer.RecursionLimit = 10;
			string text = javaScriptSerializer.Serialize(this.DataSources.Select((DataSource dataSource) => new
			{
				kind = dataSource.Kind,
				path = dataSource.Path
			}));
			this.Data["Microsoft.Data.Mashup.MashupSecurityException.DataSources"] = text;
		}

		// Token: 0x040001A2 RID: 418
		private const int RecursionLimit = 10;

		// Token: 0x040001A3 RID: 419
		private const int MaxJsonStringLength = 1048576;

		// Token: 0x040001A4 RID: 420
		internal const string ReasonField = "Microsoft.Data.Mashup.MashupSecurityException.Reason";

		// Token: 0x040001A5 RID: 421
		internal const string DataSourcesField = "Microsoft.Data.Mashup.MashupSecurityException.DataSources";
	}
}
