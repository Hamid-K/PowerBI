using System;
using System.Data;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000054 RID: 84
	public sealed class AdomdAffectedObjectsReader : AdomdDataReader
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001FBCF File Offset: 0x0001DDCF
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0001FBD7 File Offset: 0x0001DDD7
		public int BaseVersion { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001FBE0 File Offset: 0x0001DDE0
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0001FBE8 File Offset: 0x0001DDE8
		public int CurrentVersion { get; private set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001FBF1 File Offset: 0x0001DDF1
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0001FBF9 File Offset: 0x0001DDF9
		public string Database { get; private set; }

		// Token: 0x0600053C RID: 1340 RVA: 0x0001FC02 File Offset: 0x0001DE02
		internal AdomdAffectedObjectsReader(XmlReader xmlReader, CommandBehavior commandBehavior, AdomdConnection connection)
			: base(xmlReader, commandBehavior, connection, true)
		{
			this.GetAttributes();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001FC14 File Offset: 0x0001DE14
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
