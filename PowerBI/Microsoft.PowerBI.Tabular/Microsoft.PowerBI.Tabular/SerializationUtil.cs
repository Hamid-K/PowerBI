using System;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000D7 RID: 215
	internal class SerializationUtil
	{
		// Token: 0x06000DEE RID: 3566 RVA: 0x0006EB27 File Offset: 0x0006CD27
		private SerializationUtil()
		{
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0006EB30 File Offset: 0x0006CD30
		static SerializationUtil()
		{
			XmlAttributeOverrides xmlAttributeOverrides = new XmlAttributeOverrides();
			XmlAttributeOverrides xmlAttributeOverrides2 = new XmlAttributeOverrides();
			XmlAttributeOverrides xmlAttributeOverrides3 = new XmlAttributeOverrides();
			XmlAttributes xmlAttributes = new XmlAttributes();
			xmlAttributes.XmlIgnore = true;
			SerializationUtil.AddXmlAttributes(typeof(Database), "EstimatedSize", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Database), "LastUpdate", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Database), "Version", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(ImpersonationInfo), "ImpersonationInfoSecurity", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(MajorObject), "CreatedTimestamp", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(MajorObject), "LastSchemaUpdate", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(ProcessableMajorObject), "LastProcessed", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(ProcessableMajorObject), "State", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "Databases", xmlAttributes, null, xmlAttributeOverrides2, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "Edition", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "EditionID", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "ProductLevel", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "ProductName", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "Roles", xmlAttributes, null, xmlAttributeOverrides2, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "ServerMode", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "DefaultCompatibilityLevel", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "SupportedCompatibilityLevels", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "ServerLocation", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "CompatibilityMode", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "Traces", xmlAttributes, null, xmlAttributeOverrides2, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(Server), "Version", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(ServerProperty), "Type", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(ServerProperty), "DefaultValue", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(ServerProperty), "PendingValue", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(ServerProperty), "RequiresRestart", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(ServerProperty), "IsReadOnly", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(ServerProperty), "DisplayFlag", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(ServerProperty), "Category", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.AddXmlAttributes(typeof(ServerProperty), "Units", xmlAttributes, xmlAttributeOverrides, null, xmlAttributeOverrides3);
			SerializationUtil.SerializerFullWithReadOnly = new JaXmlSerializer(DesignXmlSerializerOptions.IncludeForeignComponent | DesignXmlSerializerOptions.DontForceSiteName | DesignXmlSerializerOptions.DontWriteStartDocument | DesignXmlSerializerOptions.IgnoreDesignTimeProperties);
			SerializationUtil.SerializerFullWithoutReadOnly = new JaXmlSerializer(xmlAttributeOverrides, DesignXmlSerializerOptions.IncludeForeignComponent | DesignXmlSerializerOptions.DontForceSiteName | DesignXmlSerializerOptions.DontWriteStartDocument | DesignXmlSerializerOptions.IgnoreDesignTimeProperties);
			SerializationUtil.SerializerPartialWithReadOnly = new JaXmlSerializer(xmlAttributeOverrides2, DesignXmlSerializerOptions.IncludeForeignComponent | DesignXmlSerializerOptions.DontForceSiteName | DesignXmlSerializerOptions.DontWriteStartDocument | DesignXmlSerializerOptions.IgnoreDesignTimeProperties);
			SerializationUtil.SerializerPartialWithoutReadOnly = new JaXmlSerializer(xmlAttributeOverrides3, DesignXmlSerializerOptions.IncludeForeignComponent | DesignXmlSerializerOptions.DontForceSiteName | DesignXmlSerializerOptions.DontWriteStartDocument | DesignXmlSerializerOptions.IgnoreDesignTimeProperties);
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0006EE47 File Offset: 0x0006D047
		public static SerializationUtil Utility
		{
			get
			{
				return SerializationUtil.NestedUtil.instance;
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0006EE4E File Offset: 0x0006D04E
		private static void AddXmlAttributes(Type type, string member, XmlAttributes attributes, XmlAttributeOverrides o1, XmlAttributeOverrides o2, XmlAttributeOverrides o3)
		{
			if (o1 == null)
			{
				o2.Add(type, member, attributes);
			}
			else
			{
				o1.Add(type, member, attributes);
			}
			o3.Add(type, member, attributes);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0006EE72 File Offset: 0x0006D072
		internal JaXmlSerializer GetSerializer(bool writeMajorChildren, bool writeReadOnlyProperties)
		{
			if (writeMajorChildren)
			{
				if (!writeReadOnlyProperties)
				{
					return SerializationUtil.SerializerFullWithoutReadOnly;
				}
				return SerializationUtil.SerializerFullWithReadOnly;
			}
			else
			{
				if (!writeReadOnlyProperties)
				{
					return SerializationUtil.SerializerPartialWithoutReadOnly;
				}
				return SerializationUtil.SerializerPartialWithReadOnly;
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0006EE94 File Offset: 0x0006D094
		internal JaXmlSerializer GetSerializer(ObjectExpansion expansion)
		{
			if (expansion != ObjectExpansion.Full)
			{
				return SerializationUtil.SerializerPartialWithoutReadOnly;
			}
			return SerializationUtil.SerializerFullWithoutReadOnly;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0006EEA4 File Offset: 0x0006D0A4
		internal JaXmlSerializer GetSerializerFullWithReadOnly()
		{
			return SerializationUtil.SerializerFullWithReadOnly;
		}

		// Token: 0x0400019E RID: 414
		private const DesignXmlSerializerOptions SerializerOptions = DesignXmlSerializerOptions.IncludeForeignComponent | DesignXmlSerializerOptions.DontForceSiteName | DesignXmlSerializerOptions.DontWriteStartDocument | DesignXmlSerializerOptions.IgnoreDesignTimeProperties;

		// Token: 0x0400019F RID: 415
		private static readonly JaXmlSerializer SerializerFullWithReadOnly;

		// Token: 0x040001A0 RID: 416
		private static readonly JaXmlSerializer SerializerFullWithoutReadOnly;

		// Token: 0x040001A1 RID: 417
		private static readonly JaXmlSerializer SerializerPartialWithReadOnly;

		// Token: 0x040001A2 RID: 418
		private static readonly JaXmlSerializer SerializerPartialWithoutReadOnly;

		// Token: 0x020002EB RID: 747
		private class NestedUtil
		{
			// Token: 0x04000ABC RID: 2748
			internal static readonly SerializationUtil instance = new SerializationUtil();
		}
	}
}
