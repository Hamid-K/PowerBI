using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core.DomainModel.Serialization;
using Microsoft.PowerBI.Lucia;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200006D RID: 109
	public sealed class ReportLinguisticSchema
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00009E25 File Offset: 0x00008025
		private ReportLinguisticSchema(LinguisticSchemaContainer container)
		{
			this._container = container;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00009E34 File Offset: 0x00008034
		public bool IsSupportedVersion
		{
			get
			{
				return this._container != null;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00009E3F File Offset: 0x0000803F
		public bool IsLegacyXml
		{
			get
			{
				return this.SchemaContainer.IsLegacyXml;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00009E4C File Offset: 0x0000804C
		private LinguisticSchemaContainer SchemaContainer
		{
			get
			{
				LinguisticSchemaContainer container = this._container;
				if (container == null)
				{
					throw new InvalidOperationException("Not allowed on schema that is not a supported version.");
				}
				return container;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00009E63 File Offset: 0x00008063
		public static ReportLinguisticSchema CreateEmpty(bool useLegacyXml = false, bool useLsdlDocument = false)
		{
			LinguisticSchemaContainer linguisticSchemaContainer = LinguisticSchemaContainer.CreateEmpty(useLegacyXml);
			linguisticSchemaContainer.UpgradeIfNeeded(null, null, null);
			return new ReportLinguisticSchema(linguisticSchemaContainer);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009E7C File Offset: 0x0000807C
		public static ReportLinguisticSchema FromJson(string content, bool useLsdlDocument = false)
		{
			if (string.IsNullOrEmpty(content))
			{
				return ReportLinguisticSchema.CreateEmpty(false, useLsdlDocument);
			}
			LinguisticSchemaContainer linguisticSchemaContainer = LinguisticSchemaContainer.FromJson(content);
			if (linguisticSchemaContainer.IsSupportedVersion(null))
			{
				linguisticSchemaContainer.UpgradeIfNeeded(null, null, null);
			}
			else
			{
				linguisticSchemaContainer = null;
			}
			return new ReportLinguisticSchema(linguisticSchemaContainer);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00009EC0 File Offset: 0x000080C0
		public static ReportLinguisticSchema FromLegacyXml(string content, bool writeLegacyXml = false, bool useLsdlDocument = false)
		{
			if (string.IsNullOrEmpty(content))
			{
				return ReportLinguisticSchema.CreateEmpty(writeLegacyXml, useLsdlDocument);
			}
			LinguisticSchemaContainer linguisticSchemaContainer = null;
			try
			{
				linguisticSchemaContainer = LinguisticSchemaContainer.FromLegacyXml(content, writeLegacyXml);
				if (linguisticSchemaContainer.IsSupportedVersion(null))
				{
					LinguisticSchemaContainer linguisticSchemaContainer2 = linguisticSchemaContainer;
					Func<IConceptualSchema> func = null;
					string text = null;
					Func<XDocument, bool> func2;
					if ((func2 = ReportLinguisticSchema.<>O.<0>__UpgradeXmlDynamicImprovement) == null)
					{
						func2 = (ReportLinguisticSchema.<>O.<0>__UpgradeXmlDynamicImprovement = new Func<XDocument, bool>(ReportLinguisticSchema.UpgradeXmlDynamicImprovement));
					}
					linguisticSchemaContainer2.UpgradeIfNeeded(func, text, func2);
				}
				else
				{
					linguisticSchemaContainer = null;
				}
			}
			catch (InvalidOperationException)
			{
				return ReportLinguisticSchema.CreateEmpty(writeLegacyXml, useLsdlDocument);
			}
			return new ReportLinguisticSchema(linguisticSchemaContainer);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00009F40 File Offset: 0x00008140
		public bool HasSignificantContent()
		{
			return this.SchemaContainer.HasSignificantContent();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00009F4D File Offset: 0x0000814D
		public string GetJsonContent(string version)
		{
			return this.SchemaContainer.GetJsonContent(Version.Parse(version));
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00009F60 File Offset: 0x00008160
		public string GetPersistedJsonContent()
		{
			return this.SchemaContainer.GetJsonContent(LsdlVersion.BasePublic);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00009F72 File Offset: 0x00008172
		public void SetJsonContent(string value)
		{
			this.SchemaContainer.SetJsonContent(value);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00009F80 File Offset: 0x00008180
		public string GetLegacyXmlContent()
		{
			return this.SchemaContainer.GetLegacyXmlContent();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009F8D File Offset: 0x0000818D
		public string GetLegacyJsonContent()
		{
			return this.SchemaContainer.GetLegacyJsonContent();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009F9A File Offset: 0x0000819A
		public void SetLegacyJsonContent(string value)
		{
			this.SchemaContainer.SetLegacyJsonContent(value);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00009FA8 File Offset: 0x000081A8
		public bool TryGetPodTerms(string conceptualEntityName, out IReadOnlyList<string> terms)
		{
			return this.SchemaContainer.TryGetPodTerms(conceptualEntityName, ref terms);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00009FB7 File Offset: 0x000081B7
		public void SetPodTerms(string conceptualEntityName, IEnumerable<string> terms)
		{
			this.SchemaContainer.SetPodTerms(conceptualEntityName, terms);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00009FC6 File Offset: 0x000081C6
		private static bool UpgradeXmlDynamicImprovement(XDocument doc)
		{
			if (doc.Root.Attribute("DynamicImprovement") == null)
			{
				doc.Root.SetAttributeValue("DynamicImprovement", "Default");
				return true;
			}
			return false;
		}

		// Token: 0x04000163 RID: 355
		private readonly LinguisticSchemaContainer _container;

		// Token: 0x020000CF RID: 207
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040002C3 RID: 707
			public static Func<XDocument, bool> <0>__UpgradeXmlDynamicImprovement;
		}
	}
}
