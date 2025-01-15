using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000760 RID: 1888
	[Serializable]
	internal class ODataTestConnectionFallbackException : Exception
	{
		// Token: 0x0600379F RID: 14239 RVA: 0x000B1FF3 File Offset: 0x000B01F3
		public ODataTestConnectionFallbackException(ValueException innerException)
			: base(innerException.Message, innerException)
		{
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x00005F45 File Offset: 0x00004145
		protected ODataTestConnectionFallbackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000B2002 File Offset: 0x000B0202
		public Value ToTestConnectionTable(ODataUserSettings userSettings, Func<ODataUserSettings, Value> getFeedFunction)
		{
			if (userSettings.UseODataLib7)
			{
				throw base.InnerException;
			}
			userSettings = new ODataUserSettings(userSettings);
			userSettings.UseODataLib7 = true;
			return new ODataTestConnectionFallbackException.TestConnectionTableValue(userSettings, getFeedFunction, base.InnerException as ValueException);
		}

		// Token: 0x02000761 RID: 1889
		private sealed class TestConnectionTableValue : TableValue
		{
			// Token: 0x060037A2 RID: 14242 RVA: 0x000B2034 File Offset: 0x000B0234
			public TestConnectionTableValue(ODataUserSettings userSettings, Func<ODataUserSettings, Value> getFeedFunction, ValueException originalException)
			{
				this.userSettings = userSettings;
				this.getFeedFunction = getFeedFunction;
				this.originalException = originalException;
			}

			// Token: 0x17001315 RID: 4885
			// (get) Token: 0x060037A3 RID: 14243 RVA: 0x000B2051 File Offset: 0x000B0251
			public override TypeValue Type
			{
				get
				{
					throw this.originalException;
				}
			}

			// Token: 0x060037A4 RID: 14244 RVA: 0x000B2051 File Offset: 0x000B0251
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				throw this.originalException;
			}

			// Token: 0x060037A5 RID: 14245 RVA: 0x000B2059 File Offset: 0x000B0259
			public override void TestConnection()
			{
				this.getFeedFunction(this.userSettings).TestConnection();
			}

			// Token: 0x04001CCB RID: 7371
			private readonly ODataUserSettings userSettings;

			// Token: 0x04001CCC RID: 7372
			private readonly Func<ODataUserSettings, Value> getFeedFunction;

			// Token: 0x04001CCD RID: 7373
			private readonly ValueException originalException;
		}
	}
}
