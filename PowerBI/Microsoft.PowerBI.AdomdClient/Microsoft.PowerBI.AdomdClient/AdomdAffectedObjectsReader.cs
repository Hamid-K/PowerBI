using System;
using System.Data;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000054 RID: 84
	public sealed class AdomdAffectedObjectsReader : AdomdDataReader
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001F89F File Offset: 0x0001DA9F
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x0001F8A7 File Offset: 0x0001DAA7
		public int BaseVersion { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0001F8B0 File Offset: 0x0001DAB0
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0001F8B8 File Offset: 0x0001DAB8
		public int CurrentVersion { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0001F8C1 File Offset: 0x0001DAC1
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0001F8C9 File Offset: 0x0001DAC9
		public string Database { get; private set; }

		// Token: 0x0600052F RID: 1327 RVA: 0x0001F8D2 File Offset: 0x0001DAD2
		internal AdomdAffectedObjectsReader(XmlReader xmlReader, CommandBehavior commandBehavior, AdomdConnection connection)
			: base(xmlReader, commandBehavior, connection, true)
		{
			this.GetAttributes();
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001F8E4 File Offset: 0x0001DAE4
		private void GetAttributes()
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			if (base.XmlaDataReader.TopLevelAttributes != null)
			{
				base.XmlaDataReader.TopLevelAttributes.TryGetValue("BaseVersion", out text);
				base.XmlaDataReader.TopLevelAttributes.TryGetValue("CurrentVersion", out text2);
				base.XmlaDataReader.TopLevelAttributes.TryGetValue("name", out text3);
			}
			if (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2))
			{
				if (string.IsNullOrEmpty(text3))
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "Missing name attribute");
				}
				int num;
				if (string.IsNullOrEmpty(text) || !int.TryParse(text, out num))
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "Invalid or missing BaseVersion attribute");
				}
				int num2;
				if (string.IsNullOrEmpty(text2) || !int.TryParse(text2, out num2))
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "Invalid or missing CurrentVersion attribute");
				}
				this.Database = text3;
				this.BaseVersion = num;
				this.CurrentVersion = num2;
			}
		}
	}
}
